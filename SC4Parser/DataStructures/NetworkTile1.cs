using System;
using System.Collections.Generic;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    public class NetworkTile1
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
            //Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("x8"));

            Console.WriteLine("{0} {1} {2} | {3} {4}-{5} {6}-{7}",
                Size.ToString("x8"),
                CRC.ToString("x8"),
                Memory.ToString("x8"),
                NetworkType.ToString("x8"),
                Math.Truncate(MinSizeX / 16) * 5,
                Math.Truncate(MaxSizeX / 16) * 5,
                Math.Truncate(MinSizeZ / 16) * 5,
                Math.Truncate(MaxSizeZ / 16) * 5);



            //Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("x8"));
            //Console.WriteLine("CRC: 0x{0}", CRC.ToString("x8"));
            //Console.WriteLine("Memory address: 0x{0}", Memory.ToString("x8"));
            //Console.WriteLine("Major Version: {0}", MajorVersion);
            //Console.WriteLine("Minor Version: {0}", MinorVersion);
            //Console.WriteLine("Zot Word: {0}", ZotWord);
            //Console.WriteLine("Appearance Flag: 0x{0}", AppearanceFlag.ToString("x8"));
            //Console.WriteLine("MinTractX: {0} (0x{1}) MaxTractX: {2} (0x{3})",
            //    MinTractX,
            //    MinTractX.ToString("x8"),
            //    MaxTractX,
            //    MaxTractX.ToString("x8"));
            //Console.WriteLine("MinTractZ: {0} (0x{1}) MaxTractZ: {2} (0x{3})",
            //    MinTractZ,
            //    MinTractZ.ToString("x8"),
            //    MaxTractZ,
            //    MaxTractZ.ToString("x8"));
            //Console.WriteLine("TractSizeX: {0}", TractSizeX);
            //Console.WriteLine("TractSizeZ: {0}", TractSizeZ);
            //Console.WriteLine("SaveGame Properties: {0}", SaveGamePropertyCount);

            //// Dump any savegame properties if they are present
            //if (SaveGamePropertyCount > 0)
            //{
            //    for (int i = 0; i < SaveGamePropertyCount; i++)
            //    {
            //        Console.WriteLine("==================");
            //        SaveGamePropertyEntries[i].Dump();
            //    }
            //}

            //Console.WriteLine("Group ID: {0} [0x{1}]", GroupID, GroupID.ToString("x8"));
            //Console.WriteLine("Type ID: {0} [0x{1}]", TypeID, TypeID.ToString("x8"));
            //Console.WriteLine("Instance ID: {0} [0x{1}]", InstanceID, InstanceID.ToString("x8"));
            //Console.WriteLine("NetworkType: {0} [0x{1}]", NetworkType, NetworkType.ToString("x8"));

            //Console.WriteLine("ConnectedWest: {0} [0x{1}]", WestConnected, WestConnected.ToString("x8"));
            //Console.WriteLine("ConnectedNorth: {0} [0x{1}]", NorthConnected, NorthConnected.ToString("x8"));
            //Console.WriteLine("ConnectedEast: {0} [0x{1}]", EastConnected, EastConnected.ToString("x8"));
            //Console.WriteLine("ConnectedSouth: {0} [0x{1}]", SouthConnected, SouthConnected.ToString("x8"));

            //Console.WriteLine("X: {0} - {1}", Math.Truncate(MinSizeX / 16) * 5, Math.Truncate(MaxSizeX / 16) * 5);
            //Console.WriteLine("Z: {0} - {1}", Math.Truncate(MinSizeZ / 16) * 5, Math.Truncate(MaxSizeZ / 16) * 5);
        }
    }
}
