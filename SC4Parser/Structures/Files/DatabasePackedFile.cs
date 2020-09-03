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
        
        // All very temp, look at comment below
        public byte[] LoadEntry(TypeGroupInstance tgi)
        {
            // First find IndexEntry
            IndexEntry entry = FindIndexEntry(tgi);
            if (entry == null)
            {
                return null;
            }

            bool compressed = IsIndexEntryCompressed(entry);

            byte[] sourceBytes = LoadIndexEntry(entry);
            SaveDataToFile(sourceBytes, "", entry.TGI);

            if (compressed)
            {
                //UncompressData(entryBytes);
                DatabaseDirectoryResource resource = FindDatabaseDirectoryResource(entry);

                byte[] uncompressedData = UncompressData(sourceBytes);
               
                SaveDataToFile(uncompressedData, @"", entry.TGI);
            }
            return sourceBytes;
        }

        // TODO: We need to think about how we use and structure the operations below, right
        // now they are only used in a particular way, they arent that flexible so refactor police
        // stop here after everything
        // (Too much null checking in other files, too much repeating)
        ////////////////////////////////////////////////////////////////////////////////////////////////
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
        public IndexEntry FindIndexEntry(string type_id)
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
        ////////////////////////////////////////////////////////////////////////////////////////////////

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

        private void LZCompliantCopy(ref byte[] source, int sourceOffset, ref byte[] destination, int destinationOffset, int length)
        {
            if (length != 0)
            {
                Buffer.BlockCopy(source, sourceOffset, destination, destinationOffset, 1);

                length = length - 1;
                sourceOffset++;
                destinationOffset++;

                LZCompliantCopy(ref source, sourceOffset, ref destination, destinationOffset, length);
            }
        }

        // QFS/quickref decompression implementation
        // Source: https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/Modules/qfs.c#L25
        private byte[] UncompressData(byte[] data)
        {
            byte[] sourceBytes = data;
            byte[] destinationBytes;
            int sourcePosition = 0;
            int destinationPosition = 0;

            // Check first 4 bytes (size of header + compressed data)
            uint compressedSize = BitConverter.ToUInt32(sourceBytes, 0);

            // Next read the 5 byte header
            byte[] header = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                header[i] = sourceBytes[i + 4];
            }

            // First 2 bytes should be the QFS identifier
            // Next 3 bytes should be the uncompressed size of file
            // (we do this by byte shifting (from most significant byte to least))
            // the last 3 bytes of the header to make a number)
            uint uncompressedSize = Convert.ToUInt32((long)(header[2] << 16) + (header[3] << 8) + header[4]); ;

            // Create our destination array
            destinationBytes = new byte[uncompressedSize];

            // Next set our position in the file
            // (The if checks if the first 4 bytes are the size of the file
            // if so our start position is 4 bytes + 5 byte header if not then our
            // offset is just the header (5 bytes))
            if ((sourceBytes[0] & 0x01) != 0)
            {
                sourcePosition = 9;//8;
            }
            else
            {
                sourcePosition = 5;
            }

            // In QFS the control character tells us what type of decompression operation we are going to perform (there are 4)
            // Most involve using the bytes proceeding the control byte to determine the amount of data that should be copied from what
            // offset. These bytes are labled a, b and c. Some operations only use 1 proceeding byte, others can use 3
            byte controlCharacter = 0;
            byte a = 0;
            byte b = 0;
            byte c = 0;
            int length = 0;
            int offset = 0;

            // Main decoding loop
            // Keep decoding while sourcePosition is in source array and position isn't 0xFC?
            while ((sourcePosition < sourceBytes.Length) && (sourceBytes[sourcePosition] < 0xFC))
            {
                // Read our packcode/control character
                controlCharacter = sourceBytes[sourcePosition];
                
                a = sourceBytes[sourcePosition + 1];
                b = sourceBytes[sourcePosition + 2];
                
                if ((controlCharacter & 0x80) == 0)
                {
                    length = controlCharacter & 0x03; //controlCharacter & 3;
                    //Buffer.BlockCopy(sourceBytes, sourcePosition + 2, destinationBytes, destinationPosition, length);
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 2, ref destinationBytes, destinationPosition, length);
                    sourcePosition += length + 2;
                    destinationPosition += length;
                    length = ((controlCharacter & 0x1C) >> 2) + 3;//((controlCharacter & 0x1C) >> 2) + 3;
                    offset = ((controlCharacter & 0x60) << 3) + a + 1;//((controlCharacter >> 5) << 8) + a + 1;
                    //Buffer.BlockCopy(destinationBytes, destinationPosition - offset, destinationBytes, destinationPosition, length);
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                }
                else if ((controlCharacter & 0x40) == 0)
                {
                    length = (a & 0xC0) >> 6;//(a >> 6) & 3;
                    //Buffer.BlockCopy(sourceBytes, sourcePosition + 3, destinationBytes, destinationPosition, length);
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 3, ref destinationBytes, destinationPosition, length);
                    sourcePosition += length + 3;
                    destinationPosition += length;
                    length = (controlCharacter & 0x3F) + 4;//(controlCharacter & 0x3F) + 4;
                    offset = ((a & 0x3F) << 8) + b + 1;//(a & 0x3F) * 256 + b + 1;
                    //Buffer.BlockCopy(destinationBytes, destinationPosition - offset, destinationBytes, destinationPosition, length);
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                }
                else if ((controlCharacter & 0x20) == 0)
                {
                    c = sourceBytes[sourcePosition + 3];
                    length = controlCharacter & 0x03;//& 3;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 4, ref destinationBytes, destinationPosition, length);
                    sourcePosition += length + 4;
                    destinationPosition += length;
                    length = ((controlCharacter & 0x0C) << 6) + c + 5;//((controlCharacter >> 2) & 3) * 256 + c + 5;
                    offset = ((controlCharacter & 0x10) << 12) + (a << 8) + b + 1;//((controlCharacter & 0x10) << 12) + 256 * a + b + 1;
                    
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                }
                else
                {
                    length = (controlCharacter - 0xDF) << 2;//(controlCharacter & 0x1F) * 4 + 4;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 1, ref destinationBytes, destinationPosition, length);
                    sourcePosition += length + 1;
                    destinationPosition += length;
                }
            }

            // Add trailing bytes
            if ((sourcePosition < sourceBytes.Length) && (destinationPosition < destinationBytes.Length))
            {
                LZCompliantCopy(ref sourceBytes, sourcePosition + 1, ref destinationBytes, destinationPosition, sourceBytes[sourcePosition] & 3);
                destinationPosition += sourceBytes[sourcePosition] & 3;
            }

            if (destinationPosition != destinationBytes.Length)
            {
                Console.WriteLine("QFS bad length, {0} instead of {1}", destinationPosition, destinationBytes.Length);
            }

            return destinationBytes;
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

                Logger.Info(string.Format("Data (tgi={0}, size {1} bytes) written to path: {2}",
                    tgi.ToString(),
                    data.Length,
                    Path.Combine(path, filename)));
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
