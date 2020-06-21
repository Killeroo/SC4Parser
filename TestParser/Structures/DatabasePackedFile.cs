using System;
using System.Collections.Generic;
using System.IO;

namespace TestParser.Structures
{
    class DatabasePackedFile
    {
        DatabasePackedFileHeader Header;
        DatabaseDirectoryFile DBDFFile;
        List<IndexEntry> IndexEntries;

        string FilePath;
        MemoryStream RawFile;
        bool Verbose = true;

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
                // Open file as a file stream
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // Save path for reference later
                    FilePath = path;

                    if (Verbose)
                    {
                        Console.Write("Reading header ... ");
                    }

                    // Read header
                    byte[] buffer = new byte[100];
                    stream.Read(buffer, 0, 96);
                    Header.Parse(buffer);

                    if (Verbose)
                    {
                        Logger.Info("Done", ConsoleColor.Green);
                    }

                    if (Verbose)
                    {
                        Console.Write("Reading Index Entries ... ");
                    }

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

                    if (Verbose)
                    {
                        Logger.Info("Done", ConsoleColor.Green);
                    }

                    if (Verbose)
                    {
                        Console.Write("Finding DBDF file ... ");
                    }

                    // loop through indexes and find DBDF file
                    foreach (IndexEntry entry in IndexEntries)
                    {
                        if (entry.TGI == Constants.DATABASE_DIRECTORY_FILE_TGI)
                        {
                            DBDFFile = new DatabaseDirectoryFile(entry);
                            break;
                        }
                    }

                    if (Verbose)
                    {
                        Logger.Info("Done", ConsoleColor.Green);
                    }

                    if (Verbose)
                    {
                        Console.Write("Reading DBDF Resources ... ");
                    }

                    // Seek to DBDF location and parse resources
                    stream.Seek(DBDFFile.FileLocation, SeekOrigin.Begin);
                    for (int i = 0; i < DBDFFile.ResourceCount; i++)
                    {
                        byte[] resourceBuffer = new byte[16];
                        stream.Read(resourceBuffer, 0, 16);
                        DBDFFile.ParseResource(resourceBuffer);
                    }

                    if (Verbose)
                    {
                        Logger.Info("Done", ConsoleColor.Green);
                    }

                    // Save a copy of the stream so we can access stuff after we close the file stream
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(RawFile);
                }
            }
            catch (Exception ex)
            {
                if (Verbose)
                {
                    Logger.Info("Failed", ConsoleColor.Red);
                }
                Logger.Error("Hit exception while reading save file: [" + ex.GetType().ToString() + "] " + ex.GetType().ToString());
            }
        }
        
        public byte[] LoadEntry(TypeGroupInstance tgi)
        {
            // First find IndexEntry
            IndexEntry entry = FindIndexEntry(tgi);
            if (entry == null)
            {
                return null;
            }

            bool compressed = IsIndexEntryCompressed(entry);

            byte[] entryBytes = LoadIndexEntry(entry);

            if (compressed)
            {
                UncompressData(entryBytes);
            }
            return entryBytes;
        }

        public IndexEntry FindIndexEntry(TypeGroupInstance tgi)
        {
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI == tgi)
                {
                    foundEntry = entry;
                    if (Verbose)
                    {
                        Logger.Info(string.Format("{0} found", tgi.ToString()));
                    }
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Error(string.Format("Could not find tgi ({0}) in IndexEntries", tgi.ToString()));
            }

            return foundEntry;
        }
        public IndexEntry FindIndexEntry(string type_id)
        {
            // Find IndexEntry with the specified TypeID
            IndexEntry foundEntry = null;
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI.TypeID.ToString("X") == type_id)
                {
                    foundEntry = entry;
                    if (Verbose)
                    {
                        Logger.Info(string.Format("{0} found: {1}", type_id, entry.TGI.ToString()));
                    }
                    break;
                }
            }

            if (foundEntry == null)
            {
                Logger.Error("Could not find tgi with TypeID " + type_id);
            }

            return foundEntry;
        }

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

        private byte[] LoadIndexEntry(IndexEntry entry)
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

                if (Verbose)
                {
                    Logger.Info(string.Format("Index Entry (tgi {0}, size {1} bytes) loaded",
                        entry.TGI.ToString(),
                        entry.FileSize));
                }
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

        // QFS/quickref decompression implementation
        // Source: https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/Modules/qfs.c#L25
        private byte[] UncompressData(byte[] data)
        {
            byte[] outBuffer = null;
            byte[] inBuffer = data;
            int inPosition = 0;
            int outPosition = 0;
            int inLength = inBuffer.Length;
            int outLength = 0;
            int offset = 0;
            int length = 0;
            int a, b, c, d;

            // Work out uncompressed length of data
            outLength = (inBuffer[2] << 16) + (inBuffer[3] << 8) + inBuffer[4];
            Console.WriteLine(inBuffer.Length);
            Console.WriteLine(outLength);
            outBuffer = new byte[outLength];

            //for (int i=0; i< 4; i++)
            //{
            //    outLength |=()
            //}
            return null;
        }
        
        public void SaveDataToFile(byte[] data, string path, TypeGroupInstance tgi)
        {
            string filename = Path.GetFileName(FilePath).Split('.')[0] + "_" + tgi.ToString().Replace(" ", "-");

            try
            {
                // Write buffer to specified path
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }

                if (Verbose)
                {
                    Logger.Info(string.Format("Data (tgi={0}, size {1} bytes) written to path: {2}",
                        tgi.ToString(),
                        data.Length,
                        Path.Combine(path, filename)));
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Exception ({0}) occured while trying to save Index Entry ({4}) to path {1}. msg={2} trace={3}",
                    e.GetType().ToString(),
                    Path.Combine(path),
                    e.Message,
                    e.StackTrace,
                    tgi.ToString()));
            }
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
