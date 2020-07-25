using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace TestParser.Structures
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

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void test(char[] array, int size);

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

        //private void Copy(ref byte[] source, , ref byte[] destination, int length)
        //{
            
        //    //while (length != 0)
        //    //{
        //    //    length--;
        //    //    destination
        //    //}
        //}

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
            Console.WriteLine("{0} bytes", compressedSize);

            // Next read the 5 byte header
            byte[] header = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                header[i] = sourceBytes[i + 4];
            }
            // First 2 bytes should be the QFS identifier
            Console.WriteLine("{0}{1}", header[0].ToString("X"), header[1].ToString("X"));
            // Next 3 bytes should be the uncompressed size of file
            // (we do this by byte shifting (from most significant byte to least))
            // the last 3 bytes of the header to make a number)
            uint uncompressedSize = Convert.ToUInt32((long)(header[2] << 16) + (header[3] << 8) + header[4]); ;
            Console.WriteLine("{0} bytes", uncompressedSize);

            // Create our destination array
            destinationBytes = new byte[uncompressedSize];

            // Next set our position in the file
            // (The if checks if the first 4 bytes are the size of the file
            // if so our start position is 4 bytes + 5 byte header if not then our
            // offset is just the header (5 bytes))
            if ((sourceBytes[0] & 0x01) != 0)
            {
                //////////////////////////////////////////////////////////
                /* I think these positions are too short
                so add 1 to reach the correct index of uncompressed data
                but the next condition seemed to have it right..
                this might be intentional, check back later */
                //////////////////////////////////////////////////////////
                sourcePosition = 9;//8;
            }
            else
            {
                sourcePosition = 5;
            }

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            byte a = 0;
            byte b = 0;
            byte c = 0;

            // Main decoding loop
            // Keep decoding while sourcePosition is in source array and position isn't 0xFC?
            byte controlCharacter = 0;
            int length = 0;
            int offset = 0;
            while ((sourcePosition < sourceBytes.Length) && (sourceBytes[sourcePosition] < 0xFC))
            {
                //Console.WriteLine("pos={0} source_len={1} | current_source_byte={2}",
                //    sourcePosition,
                //    sourceBytes.Length,
                //    sourceBytes[sourcePosition].ToString("X"));

                // Read our packcode/control character
                controlCharacter = sourceBytes[sourcePosition];

                //Console.WriteLine("current_control_character={0}", sourceBytes[sourcePosition].ToString("X"));
              

                a = sourceBytes[sourcePosition + 1];
                b = sourceBytes[sourcePosition + 2];

                //Console.WriteLine(@"proceeding bytes [int a={0} b={1}] [byte a={4} b={5}]/[byte a={2} b={3}]",
                //    a,
                //    b,
                //    aa,
                //    bb,
                //    aa.ToString(),
                //    bb.ToString());


                //TODO: CHECK THE INDEX MODIFICATIONS AFTER THE FACT THEY MIGHT BE OFF BY ONE
                if ((controlCharacter & 0x80) == 0)
                {
                    length = controlCharacter & 0x03; //controlCharacter & 3;
                    Buffer.BlockCopy(sourceBytes, sourcePosition + 2, destinationBytes, destinationPosition, length);
                    sourcePosition += length + 2;
                    destinationPosition += length;
                    length = ((controlCharacter & 0x1C) >> 2) + 3;//((controlCharacter & 0x1C) >> 2) + 3;
                    offset = ((controlCharacter & 0x60) << 3) + a + 1;//((controlCharacter >> 5) << 8) + a + 1;
                    Buffer.BlockCopy(destinationBytes, destinationPosition - offset, destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                    count1++;
                }
                else if ((controlCharacter & 0x40) == 0)
                {
                    length = (a & 0xC0) >> 6;//(a >> 6) & 3;
                    Buffer.BlockCopy(sourceBytes, sourcePosition + 3, destinationBytes, destinationPosition, length);
                    sourcePosition += length + 3;
                    destinationPosition += length;
                    length = (controlCharacter & 0x3F) + 4;//(controlCharacter & 0x3F) + 4;
                    offset = ((a & 0x3F) << 8) + b + 1;//(a & 0x3F) * 256 + b + 1;
                    Buffer.BlockCopy(destinationBytes, destinationPosition - offset, destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                    count2++;
                }
                else if ((controlCharacter & 0x20) == 0)
                {
                    c = sourceBytes[sourcePosition + 3];
                    length = controlCharacter & 0x03;//& 3;
                    Buffer.BlockCopy(sourceBytes, sourcePosition + 4, destinationBytes, destinationPosition, length);
                    sourcePosition += length + 4;
                    destinationPosition += length;
                    length = ((controlCharacter & 0x0C) << 6) + c + 5;//((controlCharacter >> 2) & 3) * 256 + c + 5;
                    offset = ((controlCharacter & 0x10) << 12) + (a << 8) + b + 1;//((controlCharacter & 0x10) << 12) + 256 * a + b + 1;

                    //Console.WriteLine("------------------------------");
                    //byte[] copy = new byte[length];
                    //Array.Copy(destinationBytes, offset, copy, 0, length);
                    //Console.WriteLine("destpos={0}/destsize{1} + lenght={2}", (destinationPosition - offset).ToString("X"), destinationPosition, length);
                    //foreach (var seg in copy)
                    //{
                    //    Console.WriteLine(seg.ToString("X"));
                    //}

                    Buffer.BlockCopy(destinationBytes, destinationPosition - offset, destinationBytes, destinationPosition, length);
                    destinationPosition += length;
                    count3++;
                }
                else
                {
                    length = (controlCharacter - 0xDF) << 2;//(controlCharacter & 0x1F) * 4 + 4;
                    Buffer.BlockCopy(sourceBytes, sourcePosition + 1, destinationBytes, destinationPosition, length);
                    sourcePosition += length + 1;
                    destinationPosition += length;
                    count4++;
                }
            }

            // Add trailing bytes
            if ((sourcePosition < sourceBytes.Length) && (destinationPosition < destinationBytes.Length))
            {
                Buffer.BlockCopy(sourceBytes, sourcePosition + 1, destinationBytes, destinationPosition, sourceBytes[sourcePosition] & 3);
                destinationPosition += sourceBytes[sourcePosition] & 3;
            }

            if (destinationPosition != destinationBytes.Length)
            {
                Console.WriteLine("Warning bad length, {0} instead of {1}", destinationPosition, destinationBytes.Length);
            }
            Console.WriteLine("1: {0}, 2: {1}, 3: {2}, 4: {3}", count1, count2, count3, count4);


            return destinationBytes;
        }

        private byte[] Uncompress2(byte[] data, uint targetSize, int offset)
        {
            byte[] uncdata = null;
            int index = offset;

            try
            {
                uncdata = new byte[targetSize];
            }
            catch (Exception)
            {
                uncdata = new byte[0];
            }

            int uncindex = 0;
            int plaincount = 0;
            int copycount = 0;
            int copyoffset = 0;
            byte cc = 0;
            byte cc1 = 0;
            byte cc2 = 0;
            byte cc3 = 0;
            int source;

            //try
            //{
            while ((index < data.Length) && (data[index] < 0xfc))
            {
                cc = data[index++];

                if ((cc & 0x80) == 0)
                {
                    cc1 = data[index++];
                    plaincount = (cc & 0x03);
                    copycount = ((cc & 0x1C) >> 2) + 3;
                    copyoffset = ((cc & 0x60) << 3) + cc1 + 1;
                }
                else if ((cc & 0x40) == 0)
                {
                    cc1 = data[index++];
                    cc2 = data[index++];
                    plaincount = (cc1 & 0xC0) >> 6;
                    copycount = (cc & 0x3F) + 4;
                    copyoffset = ((cc1 & 0x3F) << 8) + cc2 + 1;
                }
                else if ((cc & 0x20) == 0)
                {
                    cc1 = data[index++];
                    cc2 = data[index++];
                    cc3 = data[index++];
                    plaincount = (cc & 0x03);
                    copycount = ((cc & 0x0C) << 6) + cc3 + 5;
                    copyoffset = ((cc & 0x10) << 12) + (cc1 << 8) + cc2 + 1;
                }
                else
                {
                    plaincount = (cc - 0xDF) << 2;
                    copycount = 0;
                    copyoffset = 0;
                }

                for (int i = 0; i < plaincount; i++) uncdata[uncindex++] = data[index++];

                source = uncindex - copyoffset;
                for (int i = 0; i < copycount; i++) uncdata[uncindex++] = uncdata[source++];
            }//while
            //} //try
            //catch (Exception ex)
            //{
            //    //Helper.ExceptionMessage("", ex);
            //    throw ex;
            //}


            if (index < data.Length)
            {
                plaincount = (data[index++] & 0x03);
                for (int i = 0; i < plaincount; i++)
                {
                    if (uncindex >= uncdata.Length) break;
                    uncdata[uncindex++] = data[index++];
                }
            }
            return uncdata;
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
