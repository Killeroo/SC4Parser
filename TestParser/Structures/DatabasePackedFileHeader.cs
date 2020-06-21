﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures
{
    class DatabasePackedFileHeader
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
}
