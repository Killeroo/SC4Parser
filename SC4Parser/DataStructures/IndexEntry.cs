using System;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    public class IndexEntry
    {
        public TypeGroupInstance TGI;
        public uint FileLocation;
        public uint FileSize;
        public bool Compressed = false;

        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 20)
            {
                Console.WriteLine("Index entry buffer is too small to parse");
                return;
            }

            TGI = new TypeGroupInstance(
                BitConverter.ToUInt32(buffer, 0),
                BitConverter.ToUInt32(buffer, 4),
                BitConverter.ToUInt32(buffer, 8)
            );
            FileLocation = BitConverter.ToUInt32(buffer, 12);
            FileSize = BitConverter.ToUInt32(buffer, 16);
            
        }

        public virtual void Dump()
        {
            Console.WriteLine("TypeID: {0}", TGI.Type.ToString("X"));
            Console.WriteLine("GroupID: {0}", TGI.Group.ToString("X"));
            Console.WriteLine("InstanceID: {0}", TGI.Instance.ToString("X"));
            Console.WriteLine("File Location: {0}", FileLocation);
            Console.WriteLine("File Size: {0}", FileSize);
        }
    }
}
