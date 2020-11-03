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
    /// Database Packed File (DBPF) is the file format used by maxis for savegames. They are compressed archive files and contain
    /// multiple files related to a save, some of which are compressed using QFS/refpack.
    /// A detailed spec and layout of the file format can be found here: 
    /// - https://wiki.sc4devotion.com/index.php?title=DBPF
    /// - http://wiki.niotso.org/DBPF
    /// </summary>
    public class DatabasePackedFile
    {
        public DatabasePackedFileHeader Header { get; private set; }
        public DatabaseDirectoryFile DBDFFile { get; private set; }
        public List<IndexEntry> IndexEntries { get; private set; }
        public string FilePath { get; private set; }

        private MemoryStream RawFile;

        public DatabasePackedFile()
        {
            Header = new DatabasePackedFileHeader();
            IndexEntries = new List<IndexEntry>();
            DBDFFile = new DatabaseDirectoryFile();
            RawFile = new MemoryStream();
            FilePath = "";
        }
        public DatabasePackedFile(string path)
            : this()
        {
            Load(path);
        }

        /// <summary>
        /// Loads a DBPF/SimCity 4 save file 
        /// </summary>
        /// <exception cref="SC4Parser.DBPFParsingException">Thrown when an exception occurs while loading the DBPF file</exception>
        public void Load(string path)
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
        /// Returns the bytes of an IndexEntry using the entry's TGI
        /// </summary>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
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
        /// Returns the bytes of an IndexEntry using the referring IndexEntry
        /// </summary>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
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
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
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

            bool compressed = IsIndexEntryCompressed(entry);
            byte[] sourceBytes = ReadRawIndexEntryData(entry);

            return sourceBytes;
        }

        /// <summary>
        /// Checks if an IndexEntry is compressed
        /// </summary>
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
        /// Internal lookup methods for finding IndexEntries or DirectoryResources
        /// </summary>
        /// 
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
            return resource;
        }

        /// <summary>
        /// Retrives an IndexEntry from the file.
        /// NOTE: Does not decompress file contents just reads raw data
        /// </summary>
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
        /// Dumps contents of DBPF file
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
