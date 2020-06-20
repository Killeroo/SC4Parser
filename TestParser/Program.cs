using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

// Key:
// ----
// File format: https://www.wiki.sc4devotion.com/index.php?title=DBPF
// Header format: https://www.wiki.sc4devotion.com/images/e/e8/DBPF_File_Format_v1.1.png
// Save game format: https://www.wiki.sc4devotion.com/index.php?title=Savegame
// Lot subsection: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile

// code examples
// --------
// https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/rgnReader.py#L156
//https://github.com/wouanagaine/SC4Mapper-2013/tree/master/Modules
//https://community.simtropolis.com/forums/topic/758258-modifying-sc4-savegames-it-is-possible/?page=2

// Tools:
// ------
// https://sc4devotion.com/forums/index.php?topic=7400
// https://sc4devotion.com/forums/index.php?topic=15455.0
// https://sc4devotion.com/csxlex/lex_filedesc.php?lotGET=731
//https://www.sc4devotion.com/csxlex/lex_filedesc.php?lotGET=2021
// https://sourceforge.net/p/ilive-reader/code/HEAD/tree/trunk/
//https://sc4devotion.com/forums/index.php?topic=10417.0

// Future
// (we want roads, builds and terrain at some point?)
//https://www.wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Index_Subfile_.286A0F82B2.29

namespace TestParser
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string savePath = "Fulham.sc4";


            try
            {
                // Open file as a file stream
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    // Read header bytes
                    byte[] buffer = new byte[100];
                    stream.Read(buffer, 0, 96);

                    // Read header
                    DBPFHeader header = new DBPFHeader();
                    header.Parse(buffer);
                    header.Dump();

                    // Seek to the first index entry and read index entries
                    stream.Seek(header.FirstIndexOffset, SeekOrigin.Begin);
                    List<IndexEntry> entries = new List<IndexEntry>();
                    for (int i = 0; i < header.IndexCount; i++)
                    {
                        byte[] indexBuffer = new byte[20];
                        stream.Read(buffer, 0, 20);

                        IndexEntry entry = new IndexEntry();
                        entry.Parse(buffer);
                        entry.Dump();
                        entries.Add(entry);
                        Console.WriteLine("----------------------------");

                    }

                    // loop through indexes and find DBDF file
                    DBDFFile DBDF = new DBDFFile();
                    foreach (IndexEntry entry in entries)
                    {
                        if (entry.TGI == Constants.DATABASE_DIRECTORY_FILE_TGI)
                        {
                            DBDF = new DBDFFile(entry);
                            Console.WriteLine("DBDF found.");
                            break;
                        }
                    }

                    // Seek to DBDF location and parse resources
                    stream.Seek(DBDF.FileLocation, SeekOrigin.Begin);
                    for (int i = 0; i < DBDF.ResourceCount; i++)
                    {
                        byte[] resourceBuffer = new byte[16];
                        stream.Read(resourceBuffer, 0, 16);
                        DBDF.ParseResource(resourceBuffer);
                    }
                    DBDF.Dump();




                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hit exception: " + ex.GetType().ToString());
            }
        }
    }

    class DBPFHeader
    {
        public string Identifier;
        public uint MajorVersion;
        public uint MinorVersion;
        public DateTime DateCreated;
        public DateTime DateModified;
        public uint IndexMajorVersion;
        public uint IndexCount;
        public uint FirstIndexOffset;
        public uint IndexSize;
        public uint HoleCount;
        public uint HoleOffset;
        public uint HoleSize;
        public uint IndexMinorVersion;

        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 96)
            {
                Console.WriteLine("DBPF buffer is too small to parse");
                return;
            }

            // Read header contents
            // (https://www.wiki.sc4devotion.com/index.php?title=DBPF#Header)
            Identifier = Encoding.ASCII.GetString(buffer, 0, 4);
            MajorVersion = BitConverter.ToUInt32(buffer, 4);
            MinorVersion = BitConverter.ToUInt32(buffer, 8);
            DateCreated = Utils.UnixTimestampToDateTime(BitConverter.ToUInt32(buffer, 24));
            DateModified = Utils.UnixTimestampToDateTime(BitConverter.ToUInt32(buffer, 28));
            IndexMajorVersion = BitConverter.ToUInt32(buffer, 32);
            IndexCount = BitConverter.ToUInt32(buffer, 36);
            FirstIndexOffset = BitConverter.ToUInt32(buffer, 40);
            IndexSize = BitConverter.ToUInt32(buffer, 44);
            HoleCount = BitConverter.ToUInt32(buffer, 48);
            HoleOffset = BitConverter.ToUInt32(buffer, 52);
            HoleSize = BitConverter.ToUInt32(buffer, 56);
            IndexMinorVersion = BitConverter.ToUInt32(buffer, 60);
        }

        public void Dump()
        {
            Console.WriteLine("Identifier: {0}", Identifier);
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Date Created: {0}", DateCreated);
            Console.WriteLine("Date Modified: {0}", DateModified);
            Console.WriteLine("Index Major Version: {0}", IndexMajorVersion);
            Console.WriteLine("Index Minor Version: {0}", IndexMinorVersion);
            Console.WriteLine("Index Entry Count: {0}", IndexCount);
            Console.WriteLine("First Index Offset: {0}", FirstIndexOffset);
            Console.WriteLine("Index Table Size: {0} bytes", IndexSize);
            Console.WriteLine("Hole Count: {0}", HoleCount);
            Console.WriteLine("Hole Offset: {0}", HoleOffset);
            Console.WriteLine("Hole Size: {0} bytes", HoleSize);
        }

    }

    class IndexEntry
    {
        public uint TypeID;
        public uint GroupID;
        public uint InstanceID;
        public uint FileLocation;
        public uint FileSize;

        public TGI TGI;

        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 20)
            {
                Console.WriteLine("Index entry buffer is too small to parse");
                return;
            }

            TypeID = BitConverter.ToUInt32(buffer, 0);
            GroupID = BitConverter.ToUInt32(buffer, 4);
            InstanceID = BitConverter.ToUInt32(buffer, 8);
            FileLocation = BitConverter.ToUInt32(buffer, 12);
            FileSize = BitConverter.ToUInt32(buffer, 16);

            TGI = new TGI(TypeID, GroupID, InstanceID);
        }

        public virtual void Dump()
        {
            Console.WriteLine("TypeID: {0}", TypeID.ToString("X"));
            Console.WriteLine("GroupID: {0}", GroupID.ToString("X"));
            Console.WriteLine("InstanceID: {0}", InstanceID.ToString("X"));
            Console.WriteLine("File Location: {0}", FileLocation);
            Console.WriteLine("File Size: {0}", FileSize);
        }
    }

    class DBDFFile : IndexEntry
    {
        public uint ResourceCount;
        public List<DBDFResource> Resources;

        public DBDFFile() { }
        public DBDFFile(IndexEntry entry)
        {
            TypeID = entry.TypeID;
            GroupID = entry.GroupID;
            InstanceID = entry.InstanceID;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;
            TGI = entry.TGI;

            ResourceCount = FileSize / 16;
            Resources = new List<DBDFResource>();
        }

        public void ParseResource(byte[] buffer)
        {
            DBDFResource resource = new DBDFResource();
            resource.Parse(buffer);
            Resources.Add(resource);
        }

        public override void Dump()
        {
            base.Dump();

            foreach (DBDFResource resource in Resources)
            {
                Console.WriteLine("--------------");
                resource.Dump();
            }
        }
    }

    class DBDFResource 
    {
        uint TypeID;
        uint GroupID;
        uint InstanceID;
        uint DecompressedFileSize;

        TGI TGI;

        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 16)
            {
                Console.WriteLine("DBDF entry is too small to parse");
                return;
            }

            TypeID = BitConverter.ToUInt32(buffer, 0);
            GroupID = BitConverter.ToUInt32(buffer, 4);
            InstanceID = BitConverter.ToUInt32(buffer, 8);
            DecompressedFileSize = BitConverter.ToUInt32(buffer, 12);

            TGI = new TGI(TypeID, GroupID, InstanceID);
        }

        public void Dump()
        {
            Console.WriteLine("TypeID: {0}", TypeID.ToString("X"));
            Console.WriteLine("GroupID: {0}", GroupID.ToString("X"));
            Console.WriteLine("InstanceID: {0}", InstanceID.ToString("X"));
            Console.WriteLine("Decompressed File Size: {0} bytes", DecompressedFileSize);
        }
    }

    class Utils
    {
        // Based on https://stackoverflow.com/a/250400
        // Could use DateTimeOffset.FromUnixTimeSeconds from .NET 4.6 > but thought it was new enough
        // That I would ensure a bit of backwards compatability
        public static DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            DateTime convertedDateTime = unixDateTime.AddSeconds(unixTimestamp);
            return convertedDateTime;
        }
    }

    struct TGI : IEquatable<TGI>
    {
        public uint TypeID { get; set; }
        public uint GroupID { get; set; }
        public uint InstanceID { get; set; }

        public TGI (uint type_id, uint group_id, uint instance_id)
            : this()
        {
            TypeID = type_id;
            GroupID = group_id;
            InstanceID = instance_id;
        }

        public TGI (string type_id_hex, string group_id_hex, string instance_id_hex)
            : this()
        {
            TypeID = uint.Parse(type_id_hex, System.Globalization.NumberStyles.HexNumber);
            GroupID = uint.Parse(group_id_hex, System.Globalization.NumberStyles.HexNumber);
            InstanceID = uint.Parse(instance_id_hex, System.Globalization.NumberStyles.HexNumber);
        }

        public override bool Equals(object obj)
        {
            if (obj is TGI)
            {
                return this.Equals((TGI)obj);
            }
            return false;
        }

        public bool Equals(TGI tgi)
        {
            return (TypeID == tgi.TypeID) && (GroupID == tgi.GroupID) && (InstanceID == tgi.InstanceID);
        }

        public override int GetHashCode()
        {
            return (int)(TypeID + GroupID + InstanceID);
        }

        public static bool operator == (TGI lhs, TGI rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator != (TGI lhs, TGI rhs)
        {
            return !(lhs.Equals(rhs));
        }

        public void Dump()
        {
            Console.WriteLine("{0} {1} {2}",
                TypeID.ToString("X"),
                GroupID.ToString("X"),
                InstanceID.ToString("X"));
        }
    }
}
