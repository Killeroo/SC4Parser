using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures.SubFiles
{
    //https://wiki.sc4devotion.com/index.php?title=Building_Subfile
    class Building
    {
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort ZotWord;
        public byte AppearanceFlag;
        
        public void Parse(byte[] buffer)
        {
            Size = BitConverter.ToUInt32(buffer, 0);
            CRC = BitConverter.ToUInt32(buffer, 4);
            Memory = BitConverter.ToUInt32(buffer, 8);
            MajorVersion = BitConverter.ToUInt16(buffer, 12);
            MinorVersion = BitConverter.ToUInt16(buffer, 14);
            ZotWord = BitConverter.ToUInt16(buffer, 16);
            AppearanceFlag = buffer[19];

        }

        public void Dump()
        {
            Console.WriteLine("Size: {0}", Size);
            Console.WriteLine("CRC: {0}", CRC);
            Console.WriteLine("Memory: {0}", Memory);
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Zot Word: {0}", ZotWord);
            Console.WriteLine("Appearance Flag: {0}", AppearanceFlag.ToString("X"));
        }
    }
}
