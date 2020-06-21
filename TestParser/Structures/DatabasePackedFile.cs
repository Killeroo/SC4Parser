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

        Stream RawContents = null;
        bool Verbose = true;

        public DatabasePackedFile()
        {
            Header = new DatabasePackedFileHeader();
            IndexEntries = new List<IndexEntry>();
            DBDFFile = new DatabaseDirectoryFile();
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
                    RawContents = stream;
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
            IndexEntry foundEntry = new IndexEntry();
            foreach (IndexEntry entry in IndexEntries)
            {
                if (entry.TGI.TypeID == 3384630602)
                {
                    foundEntry = entry;
                    break;
                }
            }

            // Check if entry's TGI is present in DBDF
            foreach (DatabaseDirectoryResource resource in DBDFFile.Resources)
            {
                if (resource.TGI == foundEntry.TGI)
                {
                    Console.WriteLine("Entry exists in DBDF");
                }
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


        }
    }
}
