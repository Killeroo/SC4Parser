using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    class NetworkTile
    {
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort ZotWord;
        public byte AppearanceFlag;
        public byte MinTractX;
        public byte MinTractZ;
        public byte MaxTractX;
        public byte MaxTractZ;
        public ushort TractSizeX;
        public ushort TractSizeZ;
        public uint SaveGamePropertyCount;
        public List<SaveGameProperty> SaveGamePropertyEntries = new List<SaveGameProperty>();
        public uint GroupID;
        public uint TypeID;
        public uint InstanceID;
        public byte UnknownFlag;

        // Real representation vs what is easy to use? :/
        public byte NetworkType;
        public byte WestConnected;
        public byte NorthConnected;
        public byte EastConnected;
        public byte SouthConnected;
        public float MinSizeX;
        public float MaxSizeX;
        public float MinSizeY;
        public float MaxSizeY;
        public float MinSizeZ;
        public float MaxSizeZ;

        public TypeGroupInstance TGI = new TypeGroupInstance();

        public void Parse(byte[] buffer, uint offset)
        {
            Size = BitConverter.ToUInt32(buffer, 0);
            CRC = BitConverter.ToUInt32(buffer, 4);
            Memory = BitConverter.ToUInt32(buffer, 8);
            MajorVersion = BitConverter.ToUInt16(buffer, 12);
            MinorVersion = BitConverter.ToUInt16(buffer, 14);
            ZotWord = BitConverter.ToUInt16(buffer, 16);
            AppearanceFlag = buffer[19];
            MinTractX = buffer[24];
            MinTractZ = buffer[25];
            MaxTractX = buffer[26];
            MaxTractZ = buffer[27];
            TractSizeX = BitConverter.ToUInt16(buffer, 28);
            TractSizeZ = BitConverter.ToUInt16(buffer, 30);
            SaveGamePropertyCount = BitConverter.ToUInt32(buffer, 32);

            uint saveGamePropertiesOffset = 36;
            if (SaveGamePropertyCount > 0)
            {
                SaveGamePropertyEntries = SaveGameProperty.ExtractFromBuffer(buffer, SaveGamePropertyCount, ref saveGamePropertiesOffset);
            }

            GroupID = BitConverter.ToUInt32(buffer, (int)saveGamePropertiesOffset + 0);
            TypeID = BitConverter.ToUInt32(buffer, (int)saveGamePropertiesOffset + 4);
            InstanceID = BitConverter.ToUInt32(buffer, (int)saveGamePropertiesOffset + 8);
            TGI = new TypeGroupInstance(TypeID, GroupID, InstanceID);
            UnknownFlag = buffer[saveGamePropertiesOffset + 12];
            saveGamePropertiesOffset += 134; // Skip over a bunch of unknown/not needed fields
            NetworkType = buffer[saveGamePropertiesOffset]; 
            WestConnected = buffer[saveGamePropertiesOffset + 1];
            NorthConnected = buffer[saveGamePropertiesOffset + 2];
            EastConnected = buffer[saveGamePropertiesOffset + 3];
            SouthConnected = buffer[saveGamePropertiesOffset + 4];
            saveGamePropertiesOffset += 4;
            MinSizeX = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 5);
            MaxSizeX = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 9);
            MinSizeY = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 13);
            MaxSizeY = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 17);
            MinSizeZ = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 21);
            MaxSizeZ = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 25);

        }

        public void Dump()
        {
            //Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("X"));
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("X"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("X"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("X"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Zot Word: {0}", ZotWord);
            Console.WriteLine("Appearance Flag: 0x{0}", AppearanceFlag.ToString("X"));
            Console.WriteLine("MinTractX: {0} (0x{1}) MaxTractX: {2} (0x{3})",
                MinTractX,
                MinTractX.ToString("X"),
                MaxTractX,
                MaxTractX.ToString("X"));
            Console.WriteLine("MinTractZ: {0} (0x{1}) MaxTractZ: {2} (0x{3})",
                MinTractZ,
                MinTractZ.ToString("X"),
                MaxTractZ,
                MaxTractZ.ToString("X"));
            Console.WriteLine("TractSizeX: {0}", TractSizeX);
            Console.WriteLine("TractSizeZ: {0}", TractSizeZ);
            Console.WriteLine("SaveGame Properties: {0}", SaveGamePropertyCount);

            // Dump any savegame properties if they are present
            if (SaveGamePropertyCount > 0)
            {
                for (int i = 0; i < SaveGamePropertyCount; i++)
                {
                    Console.WriteLine("==================");
                    SaveGamePropertyEntries[i].Dump();
                }
            }

            Console.WriteLine("Group ID: {0} [0x{1}]", GroupID, GroupID.ToString("X"));
            Console.WriteLine("Type ID: {0} [0x{1}]", TypeID, TypeID.ToString("X"));
            Console.WriteLine("Instance ID: {0} [0x{1}]", InstanceID, InstanceID.ToString("X"));
            Console.WriteLine("NetworkType: {0} [0x{1}]", NetworkType, NetworkType.ToString("X"));

            Console.WriteLine("ConnectedWest: {0} [0x{1}]", WestConnected, WestConnected.ToString("X"));
            Console.WriteLine("ConnectedNorth: {0} [0x{1}]", NorthConnected, NorthConnected.ToString("X"));
            Console.WriteLine("ConnectedEast: {0} [0x{1}]", EastConnected, EastConnected.ToString("X"));
            Console.WriteLine("ConnectedSouth: {0} [0x{1}]", SouthConnected, SouthConnected.ToString("X"));

            Console.WriteLine("X: {0} - {1}", MinSizeX, MaxSizeX);
            Console.WriteLine("Y: {0} - {1}", MinSizeY, MaxSizeY);
            Console.WriteLine("Z: {0} - {1}", MinSizeZ, MaxSizeZ);
        }
    }
}
