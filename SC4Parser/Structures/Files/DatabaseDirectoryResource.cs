using System;

namespace TestParser.Structures
{
    class DatabaseDirectoryResource
    {
        public TypeGroupInstance TGI;
        public uint DecompressedFileSize;

        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 16)
            {
                Console.WriteLine("DBDF entry is too small to parse");
                return;
            }

            TGI = new TypeGroupInstance(
                BitConverter.ToUInt32(buffer, 0),
                BitConverter.ToUInt32(buffer, 4),
                BitConverter.ToUInt32(buffer, 8)
            );
            DecompressedFileSize = BitConverter.ToUInt32(buffer, 12);
        }

        public void Dump()
        {
            Console.WriteLine("TypeID: {0}", TGI.TypeID.ToString("X"));
            Console.WriteLine("GroupID: {0}", TGI.GroupID.ToString("X"));
            Console.WriteLine("InstanceID: {0}", TGI.InstanceID.ToString("X"));
            Console.WriteLine("Decompressed File Size: {0} bytes", DecompressedFileSize);
        }
    }
}
