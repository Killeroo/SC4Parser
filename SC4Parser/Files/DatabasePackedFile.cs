using System;
using System.Collections.Generic;
using System.IO;

using SC4Parser.DataStructures;
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
    /// <example>
    /// <c>
    /// // Basic usage
    /// 
    /// // Load save game
    /// DatabasePackedFile savegame;
    /// try
    /// {
    ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
    /// }
    /// catch (DBPFParsingException)
    /// {
    ///     Console.Writeline("Issue occured while parsing DBPF");
    ///     return;
    /// }
    ///  
    /// // Get DBPF file version
    /// Console.WriteLine("DBPF Version {0}.{1}",
    ///     savegame.Header.MajorVersion,
    ///     savegame.Header.MinorVersion);
    /// </c>
    /// </example>
    public class DatabasePackedFile : IDisposable
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
        /// Stream which contains copy of the raw Database Packed File (DBPF) in memory,
        /// used to load resources after file has been initially parsed
        /// </summary>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// IndexEntry entry = null
        /// try
        /// {
        ///     // Find flora file
        ///     entry = save.FindIndexEntryWithType("A9C05C85"); 
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find flora file);
        ///     return;
        /// }
        /// 
        /// // Get a copy of the DBPF file
        /// var data = save.RawFile;
        /// 
        /// // Read the file from the DBPF into our buffer
        /// buffer = new byte[entry.FileSize];
        /// data.Seek(entry.FileLocation, SeekOrigin.Begin);
        /// data.Read(buffer, 0, fileSize);
        /// 
        /// //.. do what we want with it
        /// </c>
        /// </example>
        public MemoryStream RawFile { get; private set; }

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
        /// Copy constructor, used to create a new Database Packed File (DBPF) object using the values
        /// of another object without passing by reference
        /// </summary>
        /// <param name="databasePackedFile">Object you want to use to create the new object</param>
        public DatabasePackedFile(DatabasePackedFile databasePackedFile)
        {
            Header = databasePackedFile.Header;
            IndexEntries = databasePackedFile.IndexEntries;
            DBDFFile = databasePackedFile.DBDFFile;
            RawFile = databasePackedFile.RawFile;
            FilePath = databasePackedFile.FilePath;
        }
        /// <summary>
        /// Constructor for Database Packed File (DBPF) that loads a DBPF
        /// from a file at a given path
        /// </summary>
        /// <param name="path">Path to DBPF file</param>
        /// <exception cref="SC4Parser.DBPFParsingException">Thrown when an exception occurs while loading the DBPF file</exception>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // You can now access and load data from the save game
        /// // using LoadIndexEntry or accessing the Index Entries directly:
        /// foreach (IndexEntry entry in savegame.IndexEntries)
        /// {
        ///     Console.WriteLine(entry.TGI);
        /// }
        /// </c>
        /// </example>
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
        /// <example>
        /// <c>
        /// DatabasePackedFile savegame = new DatabasePackedFile();
        /// 
        /// try
        /// {
        ///     savegame.Parse(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// </c>
        /// </example>
        public void Parse(string path)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Reading DBPF @ {0} ...", path);

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

                    // Find DBDF file
                    IndexEntry DBDFEntry = FindIndexEntry(Constants.DATABASE_DIRECTORY_FILE_TGI);
                    DBDFFile = new DatabaseDirectoryFile(DBDFEntry);
                    Logger.Log(LogLevel.Info, "Database Directory File found");

                    // Load Database Directory Resources
                    stream.Seek(DBDFFile.FileLocation, SeekOrigin.Begin);
                    for (int i = 0; i < DBDFFile.ResourceCount; i++)
                    {
                        byte[] resourceBuffer = new byte[16];
                        DatabaseDirectoryResource resource = new DatabaseDirectoryResource();
                        stream.Read(resourceBuffer, 0, 16);
                        resource.Parse(resourceBuffer);
                        DBDFFile.AddResource(resource);
                    }
                    Logger.Log(LogLevel.Info, "Database Directory Resources read");

                    // Save a copy of the stream so we can access stuff after we close the file stream
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(RawFile);
                }

                Logger.Log(LogLevel.Info, "DBPF loaded");
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
        /// <param name="type_id">The TypeGroupInstance (TGI) used to find the index entry</param>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
        /// <returns>Returns the (possibly uncompressed) bytes of the first IndexEntry with the given Type Id</returns>
        /// <remarks>
        /// The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
        /// in the Database Directory file (DBDF/DIR)
        /// </remarks>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // load terrain map subfile from DBPF
        /// try
        /// {
        ///     byte[] data = save.LoadIndexEntry(3384630602); 
        /// } 
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// catch (IndexEntryLoadingException)
        /// {
        ///     Console.Writeline("Issue loading Index Entry");
        ///     return;
        /// }
        /// catch (QFSDecompressionException)
        /// {
        ///     Console.Writeline("Issue decompressing data from DBPF");
        ///     return;
        /// }
        /// 
        /// // Do something with the terrain data...
        /// </c>
        /// </example>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Compression.QFS"/>
        public byte[] LoadIndexEntry(uint type_id)
        {
            Logger.Log(LogLevel.Info, "Searching for IndexEntry with TypeId={0}...", type_id);

            // First find IndexEntry
            IndexEntry entry = FindIndexEntryWithType(type_id);

            // Then load the IndexEntry
            return LoadIndexEntry(entry);
        }
        /// <summary>
        /// Loads the contents of an Index Entry from the Database Packed File (DBPF)
        /// </summary>
        /// <param name="type_id">The Type Id used to find the index entry</param>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when IndexEntry doesn't exist in save game</exception>
        /// <exception cref="SC4Parser.IndexEntryLoadingException">Thrown when exception occurs when loading IndexEntry</exception>
        /// <exception cref="SC4Parser.QFSDecompressionException">Thrown when exception occurs while decompressing IndexEntry data</exception>
        /// <returns>Returns the(possibly uncompressed) bytes of the first IndexEntry with the given Type Id</returns>
        /// <remarks>
        /// The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
        /// in the Database Directory file (DBDF/DIR)
        /// </remarks>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // load terrain map subfile from DBPF
        /// try
        /// {
        ///     byte[] data = save.LoadIndexEntry("A9DD6FF4"); 
        /// } 
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// catch (IndexEntryLoadingException)
        /// {
        ///     Console.Writeline("Issue loading Index Entry");
        ///     return;
        /// }
        /// catch (QFSDecompressionException)
        /// {
        ///     Console.Writeline("Issue decompressing data from DBPF");
        ///     return;
        /// }
        /// 
        /// // Do something with the terrain data...
        /// </c>
        /// </example>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Compression.QFS"/>
        public byte[] LoadIndexEntry(string type_id)
        {
            Logger.Log(LogLevel.Info, "Searching for IndexEntry with TypeId={0}...", type_id);

            // First find IndexEntry
            IndexEntry entry = FindIndexEntryWithType(type_id);

            // Then load the IndexEntry
            return LoadIndexEntry(entry);
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
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Terrain map subfile TGI
        /// TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
        /// 
        /// // load terrain map subfile from DBPF
        /// try
        /// {
        ///     byte[] data = save.LoadIndexEntry(terrainTGI); 
        /// } 
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// catch (IndexEntryLoadingException)
        /// {
        ///     Console.Writeline("Issue loading Index Entry");
        ///     return;
        /// }
        /// catch (QFSDecompressionException)
        /// {
        ///     Console.Writeline("Issue decompressing data from DBPF");
        ///     return;
        /// }
        /// 
        /// // Do something with the terrain data...
        /// </c>
        /// </example>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(IndexEntry)"/>
        /// <seealso cref="SC4Parser.Compression.QFS"/>
        public byte[] LoadIndexEntry(TypeGroupInstance tgi)
        {
            Logger.Log(LogLevel.Info, "Searching for IndexEntry with TGI={0}...", tgi.ToString());

            // First find IndexEntry
            IndexEntry entry = FindIndexEntry(tgi);

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
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Terrain map subfile TGI
        /// TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
        /// 
        /// // Find terrain map subfile's Index Entry in DBPF
        /// IndexEntry entry = save.FindIndexEntry(terrainTGI); 
        /// 
        /// // Load the Index Entry
        /// byte[] terrainData = null;
        /// try
        /// {
        ///     terrainData = save.LoadIndexEntryRaw(entry);
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// catch (IndexEntryLoadingException)
        /// {
        ///     Console.Writeline("Issue loading Index Entry");
        ///     return;
        /// }
        /// catch (QFSDecompressionException)
        /// {
        ///     Console.Writeline("Issue decompressing data from DBPF");
        ///     return;
        /// }
        /// 
        /// // Do something with the terrain data...
        /// </c>
        /// </example>
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
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Find Lot Subfile's Index Entry from save
        /// IndexEntry entry = save.FindIndexEntryWithType("C9BD5D4A"); 
        /// 
        /// // Load the compressed lot subfile 
        /// byte[] lotData = null;
        /// try
        /// {
        ///     lotData = save.LoadIndexEntryRaw(entry);
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// catch (IndexEntryLoadingException)
        /// {
        ///     Console.Writeline("Issue loading Index Entry");
        ///     return;
        /// }
        /// 
        /// // Do something with the compressed data;
        /// SuperAwesomeCustomQFSDecompressionMethod(lotData);
        /// </c>
        /// </example>
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
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Find Lot Subfile's Index Entry from save
        /// IndexEntry entry = null;
        /// try
        /// {
        ///     entry = save.FindIndexEntryWithType("C9BD5D4A"); 
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// 
        /// // Check if entry is compressed
        /// if (savegame.IsEntryCompressed == true)
        /// {
        ///     Console.Writeline("Lot data is compressed"); 
        /// }
        /// else 
        /// {
        ///     Console.WriteLine("Lot data is not compressed");
        /// }
        /// </c>
        /// </example>
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
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(uint)"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Terrain map subfile TGI
        /// TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
        /// 
        /// // Find terrain map subfile's Index Entry in DBPF
        /// IndexEntry entry = null;
        /// try 
        /// {
        ///     entry = save.FindIndexEntry(terrainTGI);
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// </c>
        /// </example>
        public IndexEntry FindIndexEntry(TypeGroupInstance tgi)
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
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(string)"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Find Lot Subfile's Index Entry from save
        /// IndexEntry entry = null
        /// try
        /// {
        ///     entry = save.FindIndexEntryWithType(3384630602); 
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// </c>
        /// </example>
        public IndexEntry FindIndexEntryWithType(uint type_id)
        {
            return FindIndexEntryWithType(type_id.ToString("X"));
        }
        /// <summary>
        /// Finds and returns an Index Entry with a given Type ID
        /// </summary>
        /// <param name="type_id">The Type ID used to find Index Entry</param>
        /// <returns>The first Index Entry with the given Type ID</returns>
        /// <exception cref="SC4Parser.IndexEntryNotFoundException">Thrown when Index Entry cannot be found</exception>
        /// <see cref="SC4Parser.DataStructures.IndexEntry"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntry(TypeGroupInstance)"/>
        /// <seealso cref="SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(uint)"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Find Lot Subfile's Index Entry from save
        /// IndexEntry entry = null
        /// try
        /// {
        ///     entry = save.FindIndexEntryWithType("C9BD5D4A"); 
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// </c>
        /// </example>
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
        /// <example>
        /// <c>
        /// // Load save game
        /// DatabasePackedFile savegame;
        /// try
        /// {
        ///     savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // Terrain map subfile TGI
        /// TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
        /// 
        /// // Find terrain map subfile's Index Entry in DBPF
        /// IndexEntry entry = null;
        /// try 
        /// {
        ///     save.FindIndexEntry(terrainTGI);
        /// }
        /// catch (IndexEntryNotFoundException)
        /// {
        ///     Console.Writeline("Could not find Index Entry");
        ///     return;
        /// }
        /// 
        /// // Try and find the Database Directory Resource of the Index Entry
        /// try
        /// {
        ///     var resource = savegame.FindDatabaseDirectoryResource(entry);
        /// } 
        /// catch (DatabaseDirectoryResourceNotFoundException)
        /// {
        ///     Console.Writeline("Resource for Index Entry cannot be found");
        ///     return;
        /// }
        /// </c>
        /// </example>
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


        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    RawFile.Dispose();
                }

                Header = null;
                IndexEntries.Clear();
                IndexEntries = null;
                DBDFFile = null;
                FilePath = null;

                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
