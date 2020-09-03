using System;
using System.Collections.Generic;
using System.IO;

namespace SC4Parser.Structures
{
    class DatabasePackedFile
    {
        DatabasePackedFileHeader Header;
        DatabaseDirectoryFile DBDFFile;
        List<IndexEntry> IndexEntries;

        string FilePath;
        MemoryStream RawFile;

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

        public void Load(string path)
        {
            try
            {
                Logger.Info("Reading DBDF...");

                // Open file as a file stream
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // Save path for reference later
                    FilePath = path;

                    // Read header
                    byte[] buffer = new byte[100];
                    stream.Read(buffer, 0, 96);
                    Header.Parse(buffer);

                    Logger.Info("Header read", ConsoleColor.Green);
                    
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

                    Logger.Info("Index Entries read", ConsoleColor.Green);

                    // loop through indexes and find DBDF file
                    foreach (IndexEntry entry in IndexEntries)
                    {
                        if (entry.TGI == Constants.DATABASE_DIRECTORY_FILE_TGI)
                        {
                            DBDFFile = new DatabaseDirectoryFile(entry);
                            break;
                        }
                    }

                    Logger.Info("DBDF file found", ConsoleColor.Green);

                    // Seek to DBDF location and parse resources
                    stream.Seek(DBDFFile.FileLocation, SeekOrigin.Begin);
                    for (int i = 0; i < DBDFFile.ResourceCount; i++)
                    {
                        byte[] resourceBuffer = new byte[16];
                        stream.Read(resourceBuffer, 0, 16);
                        DBDFFile.ParseResource(resourceBuffer);
                    }

                    Logger.Info("DBDF Resources read", ConsoleColor.Green);

                    // Save a copy of the stream so we can access stuff after we close the file stream
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(RawFile);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Hit exception while reading save file: [" + ex.GetType().ToString() + ":  " + ex.Message + "]");
            }
        }
        
        public byte[] LoadIndexEntry(TypeGroupInstance tgi)
        {
            // First find IndexEntry
            IndexEntry entry = FindIndexEntry(tgi);
            if (entry == null)
            {
                return null;
            }

            bool compressed = IsIndexEntryCompressed(entry);
            byte[] sourceBytes = RetrieveRawIndexEntryData(entry);

            if (compressed)
            {
                return QFS.UncompressData(sourceBytes);
            }
            return sourceBytes;
        }
        
        public IndexEntry FindIndexEntry(TypeGroupInstance tgi)
        {
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI == tgi)
                {
                    foundEntry = entry;
                    Logger.Info(string.Format("{0} found", tgi.ToString()));
                    
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Error(string.Format("Could not find tgi ({0}) in IndexEntries", tgi.ToString()));
            }

            return foundEntry;
        }
        public IndexEntry FindIndexEntryWithType(string type_id)
        {
            // Find IndexEntry with the specified TypeID
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI.TypeID.ToString("X") == type_id)
                {
                    foundEntry = entry;
                    Logger.Info(string.Format("{0} found: {1}", type_id, entry.TGI.ToString()));
                    
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Error("Could not find tgi with TypeID " + type_id);
            }

            return foundEntry;
        }

        public DatabaseDirectoryResource FindDatabaseDirectoryResource(IndexEntry entry)
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

        private byte[] RetrieveRawIndexEntryData(IndexEntry entry)
        {
            byte[] buffer = null;
            int fileSize = 0;

            // We need to convert out file size from signed to unsigned
            // this should probably be fine because a file so big that it
            // overflows will probably be a bigger file than we will ever handle
            // but makes sense to just account for it
            try
            {
                fileSize = Convert.ToInt32(entry.FileSize);
            }
            catch (OverflowException)
            {
                Logger.Error("Uncompressed entry could not be loaded, " +
                    "overflow occured while converting IndexEntry's file size" +
                    " (TGI = " + entry.TGI.ToString() + ") (" + entry.FileSize + "bytes)");
                return null;
            }

            try
            {
                // Seek to IndexEntry's position and read the data
                buffer = new byte[entry.FileSize];
                RawFile.Seek(entry.FileLocation, SeekOrigin.Begin);
                RawFile.Read(buffer, 0, fileSize);
                
                Logger.Info(string.Format("Index Entry (tgi {0}, size {1} bytes) loaded",
                    entry.TGI.ToString(),
                    entry.FileSize));
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Exception ({1}) occured while loading IndexEntry (tgi {0}). msg={2} trace={3}",
                    entry.TGI.ToString(),
                    e.GetType().ToString(),
                    e.Message,
                    e.StackTrace));
            }

            return buffer;
        }
        private bool IsIndexEntryCompressed(IndexEntry entry)
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
