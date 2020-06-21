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

        MemoryStream RawFile;
        bool Verbose = true;

        public DatabasePackedFile()
        {
            Header = new DatabasePackedFileHeader();
            IndexEntries = new List<IndexEntry>();
            DBDFFile = new DatabaseDirectoryFile();
            RawFile = new MemoryStream();
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
                Console.WriteLine("Hit exception: {0} {1}", ex.GetType().ToString(), ex.GetType().ToString());
            }
        }

        public void LoadLotFile()
        {
            // First find IndexEntry
            TypeGroupInstance target = new TypeGroupInstance("8A2482B9", "4A2482BB", "00000004");
            IndexEntry foundEntry = new IndexEntry();
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI == target)//.TypeID == 3384630602)
                {
                    foundEntry = entry;
                    Console.WriteLine("Entry found");
                    break;
                }
            }

            // Check if entry's TGI is present in DBDF
            bool compressed = false;
            foreach (DatabaseDirectoryResource resource in DBDFFile.Resources)
            {
                if (resource.TGI == foundEntry.TGI)
                {
                    Console.WriteLine("Entry exists in DBDF");
                    compressed = true;
                }
            }

            if (compressed)
            {
                //LoadUncompressedEntry(foundEntry);
            }
            else
            {
                LoadUncompressedEntry(foundEntry);
            }
        }

        public void LoadUncompressedEntry(IndexEntry entry)
        {
            int fileSize = 0;
            try
            {
                fileSize = Convert.ToInt32(entry.FileSize);
            }
            catch (OverflowException)
            {
                Logger.Error("Uncompressed entry could not be loaded, " +
                    "overflow occured while converting IndexEntry's file size" +
                    " (TGI = " + entry.TGI.ToString() + ") (" + entry.FileSize + "bytes)");
                return;
            }

            // First read file from our save file and store in buffer
            byte[] buffer = new byte[entry.FileSize];
            RawFile.Seek(entry.FileLocation, SeekOrigin.Begin);
            RawFile.Read(buffer, 0, fileSize);

            using (FileStream stream = new FileStream("tmp.png", FileMode.OpenOrCreate))
            {
                stream.Write(buffer, 0, fileSize);
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
