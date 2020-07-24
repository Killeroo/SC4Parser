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
        public uint Offset;
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort ZotWord;
        public byte Unknown1;
        public byte AppearanceFlag;
        public uint x278128A0;
        public byte MinTractX;
        public byte MinTractZ;
        public byte MaxTractX;
        public byte MaxTractZ;
        public ushort TractSizeX;
        public ushort TractSizeZ;
        public uint SaveGamePropertyCount;
        public List<SaveGameProperty> SaveGamePropertyEntries = new List<SaveGameProperty>();

        public void Parse(byte[] buffer, uint offset)
        {
            Offset = offset;
            Size = BitConverter.ToUInt32(buffer, 0);
            CRC = BitConverter.ToUInt32(buffer, 4);
            Memory = BitConverter.ToUInt32(buffer, 8);
            MajorVersion = BitConverter.ToUInt16(buffer, 12);
            MinorVersion = BitConverter.ToUInt16(buffer, 14);
            ZotWord = BitConverter.ToUInt16(buffer, 16);
            Unknown1 = buffer[18];
            AppearanceFlag = buffer[19]; // TODO: this is always 5 (at the byte level) it is supposed to be 4..
            x278128A0 = BitConverter.ToUInt32(buffer, 20);
            MinTractX = buffer[24];
            MinTractZ = buffer[25];
            MaxTractX = buffer[26];
            MaxTractZ = buffer[27];
            TractSizeX = BitConverter.ToUInt16(buffer, 28);
            TractSizeZ = BitConverter.ToUInt16(buffer, 30);
            SaveGamePropertyCount = BitConverter.ToUInt32(buffer, 32);

            // This represents the offset where data resumes after the SaveGame Properties entries (SGPROPs)
            // ExtractFromBuffer (if called) will update it to the offset after the SGPROPs
            uint saveGamePropertiesOffset = 36;

            if (SaveGamePropertyCount > 0)
            {
                SaveGamePropertyEntries = SaveGameProperty.ExtractFromBuffer(buffer, SaveGamePropertyCount, ref saveGamePropertiesOffset);
            }
        }

        public void Dump()
        {
            Console.WriteLine("Size: {0}", Size);
            Console.WriteLine("CRC: {0}", CRC.ToString("X"));
            Console.WriteLine("Memory address: {0}", Memory.ToString("X"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Zot Word: {0}", ZotWord);
            Console.WriteLine("Unknown1: {0}", Unknown1);
            Console.WriteLine("Appearance Flag: {0}", AppearanceFlag.ToString("X"));
            Console.WriteLine("0x278128A0: {0}", x278128A0.ToString("X"));
            Console.WriteLine("MinTractX: {0} ({1}) MaxTractX: {2} ({3})", 
                MinTractX,
                MinTractX.ToString("X"),
                MaxTractX,
                MaxTractX.ToString("X"));
            Console.WriteLine("MinTractZ: {0} ({1}) MaxTractZ: {2} ({3})",
                MinTractZ,
                MinTractZ.ToString("X"),
                MaxTractZ,
                MaxTractZ.ToString("X"));
            Console.WriteLine("TractSizeX: {0}", TractSizeX);
            Console.WriteLine("TractSizeZ: {0}", TractSizeZ);
            Console.WriteLine("SaveGame Properties: {0}", SaveGamePropertyCount);
            
            // Dump any save game properties if they are present
            if (SaveGamePropertyCount > 0)
            {
                for (int i = 0; i < SaveGamePropertyCount; i++)
                {
                    Console.WriteLine("--------------------");
                    SaveGamePropertyEntries[i].Dump();
                }
            }
        }
    }
}
