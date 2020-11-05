using System;
using System.Collections.Generic;
using System.IO;

using SC4Parser.DataStructures;
using SC4Parser.Subfiles;
using SC4Parser.Types;
using SC4Parser.Compression;
using SC4Parser.Logging;

namespace SC4Parser.Files
{
    /// <summary>
    /// Implementation of Database Packed File (DBPF).
    /// 
    /// A Database Packed File (DBPF) is the file format used by maxis for savegames. They are compressed archive files and contain
    /// multiple files related to a save, some of which are compressed using QFS/refpack.
    /// </summary>
    /// <remarks>
    /// This implementation is primarily focused on the DBPF version used in SimCity 4
    /// DBPF version 1.1
    /// 
    /// A detailed spec and layout of the file format can be found here: 
    /// - https://wiki.sc4devotion.com/index.php?title=DBPF
    /// - http://wiki.niotso.org/DBPF
    /// </remarks>
    public class DatabasePackedFile
    {
        /// <summary>
        /// Database Packed File's (DBPF) header file
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.DatabasePackedFileHeader"/>
        public DatabasePackedFileHeader Header { get; private set; }
        /// <summary>
        /// Database Packed File's (DBPF) Database Directory File (DBDI/DIR)
        /// which contains all the DBPF's compressed index entries
        /// </summary>
        /// <see cref="SC4Parser.Files.DatabaseDirectoryFile"/>
        /// <seealso cref="SC4Parser.DataStructures.DatabaseDirectoryResource"/>
        public DatabaseDirectoryFile DBDFFile { get; private set; }
        /// <summary>
        /// List of all index entries in Database Packed File (DBPF)
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        public List<IndexEntry> IndexEntries { get; private set; }
        /// <summary>
        /// File path that the Database Packed File (DBPF) was loaded from
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Stream which contains copy of the Database Packed File (DBPF) in memory,
        /// used to load resources after file has been initially parsed
        /// </summary>
        private MemoryStream RawFile;

        /// <summary>
        /// Default constructor for Database Packed File (DBPF)
        /// Sets up default values for all internal objects
        /// </summary>
        public DatabasePackedFile()
        {
            Header = new DatabasePackedFileHeader();
            IndexEntries = new List<IndexEntry>();
            DBDFFile = new DatabaseDirectoryFile();
            RawFile = new MemoryStream();
            FilePath = "";
        }
        /// <summary>
        /// Constructor for Database Packed File (DBPF) that loads a DBPF
        /// from a file at a given path
        /// </summary>
        /// <param name="path">Path to DBPF file</param>
        public DatabasePackedFile(string path)
            : this()
        {
            Parse(path);
        }

        /// <summary>
        /// Parses a DBPF/SimCity 4 save file at a path
        /// </summary>
        /// <param name="path">Path to DBPF file</param>
        /// <exception cref="SC4Parser.DBPFParsingException">Thrown when an exception occurs while loading the DBPF file</exception>
        public void Parse(string path)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Reading DBDF @ {0} ...", path);

                // Open file as a file stream
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // Save path for reference later
                    FilePath = path;

                    // Read header
                    byte[] buffer = new byte[100];
                    stream.Read(buffer, 0, 96);
                    Header.Parse(buffer);
                    Logger.Log(LogLevel.Info, "DBPF header read");
                    
                    // Seek to the first index entry and read index entries
                    stream.Seek(Header.FirstIndexOffset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.IndexCount; i++)
                    {
                        byte[] indexBuffer = new byte[20];
                        stream.Read(buffer, 0, 20);

                        IndexEntry entry = new IndexEntry();
                        entry.Parse(buffer);
                        IndexEntries.Add(entry);
                    }
                    Logger.Log(LogLevel.Info, "Index Entries read");

                    // loop through indexes and find DBDF file
                    // TODO: replace with find command
                    // TODO: error check
                    foreach (IndexEntry entry in IndexEntries)
                    {
                        if (entry.TGI == Constants.DATABASE_DIRECTORY_FILE_TGI)
                        {
                            DBDFFile = new DatabaseDirectoryFile(entry);
                            break;
                        }
                    }
                    Logger.Log(LogLevel.Info, "DBDF file found");

                    // Seek to DBDF location and parse resources
                    // TODO: move to DatabaseDirectoryFile parse
                    stream.Seek(DBDFFile.FileLocation, SeekOrigin.Begin);
                    for (int i = 0; i < DBDFFile.ResourceCount; i++)
                    {
                        byte[] resourceBuffer = new byte[16];
                        stream.Read(resourceBuffer, 0, 16);
                        DBDFFile.ParseResource(resourceBuffer);
                    }
                    Logger.Log(LogLevel.Info, "DBDF Resources read");

                    // Save a copy of the stream so we can access stuff after we close the file stream
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(RawFile);
                }

                Logger.Log(LogLevel.Info, "DBDF loaded");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Fatal, "Hit exception while reading save file: [{0}: {1}]",
                    ex.GetType().ToString(),
                    ex.Message);

                throw new DBPFParsingException($"Could not read save game file {path}", ex);
            }
        }

        /// <summary>
        /// Loads the contents of an Index Entry from the Database Packed File (DBPF)
        /// </summary>
        /// <param name="tgi">The TypeGroupInstance (TGI) used to find the index entry</param>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
        /// <returns>Returns the (possibly uncompressed) bytes of an IndexEntry</returns>
        /// <remarks>
        /// The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
        /// in the Database Directory file (DBDF/DIR)
        /// </remarks>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Compression.QFS"/>
        public byte[] LoadIndexEntry(TypeGroupInstance tgi)
        {
            Logger.Log(LogLevel.Info, "Searching for IndexEntry with TGI={0}...", tgi.ToString());

            // First find IndexEntry
            IndexEntry entry = FindIndexEntry(tgi);
            if (entry == null)
            {
                throw new IndexEntryNotFoundException();
            }

            // Then load the IndexEntry
            return LoadIndexEntry(entry);
        }
        /// <summary>
        /// Loads the contents of an Index Entry from the Database Packed File (DBPF)
        /// </summary>
        /// <param name="entry">The Index Entry used to load data</param>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
        /// <returns>Returns the (possibly uncompressed) bytes of an IndexEntry</returns>
        /// <remarks>
        /// The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
        /// in the Database Directory file (DBDF/DIR)
        /// </remarks>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Compression.QFS"/>
        public byte[] LoadIndexEntry(IndexEntry entry)
        {
            Logger.Log(LogLevel.Info, "Loading IndexEntry ({2}) size={0} loc={1}...",
                entry.FileSize,
                entry.FileLocation,
                entry.TGI.ToString());

            if (!IndexEntries.Contains(entry))
            {
                Logger.Log(LogLevel.Error, "IndexEntry could not be loaded, could not be found in list of index entries");
                throw new IndexEntryNotFoundException();
            }

            bool compressed = IsIndexEntryCompressed(entry);
            byte[] sourceBytes = ReadRawIndexEntryData(entry);

            if (compressed)
            {
                Logger.Log(LogLevel.Info, "Entry is compressed, decompressing...");

                byte[] decompressedData = null;
                try
                {
                    decompressedData = QFS.UncompressData(sourceBytes);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Exception occured while decompressing entry, Exception={0} Message={1}",
                        e.GetType().ToString(),
                        e.Message);
                    throw new QFSDecompressionException("Error occured while decompressing entry", e);
                }
                return decompressedData; 
            }
            return sourceBytes;
        }
        /// <summary>
        /// Returns the raw bytes of an IndexEntry using the referring IndexEntry, does not attempt to decompress entry if it is compressed.
        /// </summary>
        /// <param name="entry">The entry to load</param>
        /// <returns>Return the raw data of the Index Entry from the DBPF file in a byte array</returns>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(TypeGroupInstance)"/>
        /// <remarks>
        /// Will load the raw data of an Index Entry, this is the data as it appears in the DBPF so maybe in a compressed format
        /// </remarks>
        public byte[] LoadIndexEntryRaw(IndexEntry entry)
        {
            Logger.Log(LogLevel.Info, "Loading IndexEntry ({2}) size={0} loc={1}...",
                entry.FileSize,
                entry.FileLocation,
                entry.TGI.ToString());

            if (!IndexEntries.Contains(entry))
            {
                Logger.Log(LogLevel.Error, "IndexEntry could not be loaded, could not be found in list of index entries");
                throw new IndexEntryNotFoundException();
            }
            
            byte[] sourceBytes = ReadRawIndexEntryData(entry);

            return sourceBytes;
        }

        /// <summary>
        /// Checks if an IndexEntry is compressed
        /// </summary>
        /// <param name="entry">Index Entry to check</param>
        /// <returns>
        /// Returns <c>true</c> if the Index Entry is compressed, <c>false</c> if it is uncompressed
        /// </returns>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        public bool IsIndexEntryCompressed(IndexEntry entry)
        {
            // Check if entry's TGI is present in DBDF
            // (if it is present then it has been compressed)
            foreach (DatabaseDirectoryResource resource in DBDFFile.Resources)
            {
                if (resource.TGI == entry.TGI)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Finds and returns an Index Entry with a given TypeGroupInstance (TGI) with in the Database Packed File (DBPF)
        /// </summary>
        /// <param name="tgi">The TypeGroupInstance (TGI) of the Index Entry</param>
        /// <returns>Returns the found Index Entry with the matching TypeGroupInstance (TGI)</returns>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when Index Entry cannot be found</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(string)"/>
        private IndexEntry FindIndexEntry(TypeGroupInstance tgi)
        {
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI == tgi)
                {
                    foundEntry = entry;
                    Logger.Log(LogLevel.Info, "{0} found", tgi.ToString());
                    
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Log(LogLevel.Error, "Could not find tgi ({0}) in IndexEntries", tgi.ToString());
                throw new IndexEntryNotFoundException();
            }

            return foundEntry;
        }
        /// <summary>
        /// Finds and returns an Index Entry with a given Type ID
        /// </summary>
        /// <param name="type_id">The Type ID used to find Index Entry</param>
        /// <returns>The Index Entry with the given Type ID</returns>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when Index Entry cannot be found</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntry(TypeGroupInstance)"/>
        public IndexEntry FindIndexEntryWithType(string type_id)
        {
            // Find IndexEntry with the specified TypeID
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI.Type.ToString("X") == type_id)
                {
                    foundEntry = entry;
                    Logger.Log(LogLevel.Info, "Index with type {0} found ({1})", type_id, entry.TGI.ToString());
                    
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Log(LogLevel.Error, "Could not find IndexEntry with TypeID " + type_id);
                throw new IndexEntryNotFoundException();
            }

            return foundEntry;
        }
        /// <summary>
        /// Finds a Database Directory Resource inside the Database Packed File's (DBPF) Database Directory File (DBDF/DIR)
        /// </summary>
        /// <param name="entry">Entry to try and find in DIR file</param>
        /// <returns>Returns the found resource </returns>
        /// <exception cref="SC4Parser.DatabaseDirectoryResourceNotFoundException">Thrown when the Index Entry's resource cannot be found</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <see cref="SC4Parser.DataStructures.DatabaseDirectoryResource"/>
        /// <seealso cref="SC4Parser.Files.DatabaseDirectoryFile"/>
        private DatabaseDirectoryResource FindDatabaseDirectoryResource(IndexEntry entry)
        {
            DatabaseDirectoryResource resource = null;
            foreach (DatabaseDirectoryResource r in DBDFFile.Resources)
            {
                if (r.TGI == entry.TGI)
                {
                    resource = r;
                }
            }

            if (resource == null)
            {
                throw new DatabaseDirectoryResourceNotFoundException();
            }

            return resource;
        }

        /// <summary>
        /// Reads an IndexEntry's raw (possibly compressed) data from the Database Packed File (DBPF)
        /// 
        /// Internal function used to load data for other Index Entry loading functions
        /// </summary>
        /// <param name="entry">The entry to load</param>
        /// <returns>Return the raw data of the Index Entry from the DBPF file in a byte array</returns>
        /// <exception cref="IndexEntryLoadingException">Thrown when there is an error while loading the Index Entry</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(TypeGroupInstance)"/>
        /// <remarks>
        /// Will load the raw data of an Index Entry, this is the data as it appears in the DBPF so maybe in a compressed format
        /// </remarks>
        private byte[] ReadRawIndexEntryData(IndexEntry entry)
        {
            byte[] buffer = null;
            int fileSize = 0;

            if (entry.FileLocation > RawFile.Length)
            {
                Logger.Log(LogLevel.Error, "File location too big for DBPF size (file location={0}, DBPF length={1})",
                    RawFile.Length,
                    entry.FileLocation);
                throw new IndexEntryLoadingException("File location too big for DBPF size");
            }

            // We need to convert out file size from signed to unsigned
            // this should probably be fine because a file so big that it
            // overflows will probably be a bigger file than we will ever handle
            // but makes sense to just account for it
            try
            {
                fileSize = Convert.ToInt32(entry.FileSize);
            }
            catch (OverflowException e)
            {
                Logger.Log(LogLevel.Error, "Uncompressed entry could not be loaded, " +
                    "overflow occured while converting IndexEntry's file size" +
                    " (TGI = {0}) ({1} bytes)",
                    entry.TGI.ToString(),
                    entry.FileSize);
                throw new IndexEntryLoadingException("Overflow occured while converting IndexEntry's file size", e);
            }

            try
            {
                // Seek to IndexEntry's position and read the data
                buffer = new byte[entry.FileSize];
                RawFile.Seek(entry.FileLocation, SeekOrigin.Begin);
                RawFile.Read(buffer, 0, fileSize);
                
                Logger.Log(LogLevel.Info, "Index Entry (tgi {0}, size {1} bytes) loaded",
                    entry.TGI.ToString(),
                    entry.FileSize);
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, "Exception ({1}) occured while loading IndexEntry (tgi {0}). msg={2} trace={3}",
                    entry.TGI.ToString(),
                    e.GetType().ToString(),
                    e.Message,
                    e.StackTrace);
                throw new IndexEntryLoadingException("Error reading IndexEntry data from MemoryStream", e);
            }

            return buffer;
        }

        /// <summary>
        /// Prints out the contents of the Database Packed File (DBPF)
        /// </summary>
        public void Dump()
        {
            Header.Dump();
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach (IndexEntry entry in IndexEntries)
            {
                entry.Dump();
                Console.WriteLine(new string('-', Console.WindowWidth));
            }

            Console.WriteLine();
            DBDFFile.Dump();
        }
    }
}
