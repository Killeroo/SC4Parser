using System;
using System.Collections.Generic;

using SC4Parser.Types;
using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Representation of Simcity 4 building data, as it is present in an SC4 save
    /// (Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile)
    /// </summary>
    public class Building
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
        public byte Unknown2;
        public uint GroupID;
        public uint TypeID;
        public uint InstanceID;
        public uint InstanceIDOnAppearance; // The value given when building appeared
        public float MinCoordinateX;
        public float MinCoordinateY;
        public float MinCoordinateZ;
        public float MaxCoordinateX;
        public float MaxCoordinateY;
        public float MaxCoordinateZ;
        public byte Orientation;
        public float ScaffoldingHeight;

        // Same as typeid, groupid and instanceid from this file
        // just included it for accessibility
        public TypeGroupInstance PropExemplarReference = new TypeGroupInstance();

        /// <summary>
        /// Load a building from a byte array
        /// </summary>
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

            Unknown2 = buffer[saveGamePropertiesOffset + 0];
            GroupID = BitConverter.ToUInt32(buffer, (int) saveGamePropertiesOffset + 1);
            TypeID = BitConverter.ToUInt32(buffer, (int) saveGamePropertiesOffset + 5);
            InstanceID = BitConverter.ToUInt32(buffer, (int) saveGamePropertiesOffset + 9);
            PropExemplarReference = new TypeGroupInstance(TypeID, GroupID, InstanceID);
            InstanceIDOnAppearance = BitConverter.ToUInt32(buffer, (int) saveGamePropertiesOffset + 13);
            MinCoordinateX = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 17);
            MinCoordinateY = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 21);
            MinCoordinateZ = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 25);
            MaxCoordinateX = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 29);
            MaxCoordinateY = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 33);
            MaxCoordinateZ = BitConverter.ToSingle(buffer, (int) saveGamePropertiesOffset + 37);
            Orientation = buffer[saveGamePropertiesOffset + 41];
            ScaffoldingHeight = BitConverter.ToSingle(buffer, (int)saveGamePropertiesOffset + 42);

            // Sanity check out current offset to make sure we haven't missed anything
            if (saveGamePropertiesOffset + 46 != Size)
            {
                Logger.Log(LogLevel.Warning, "Building was not properly parsed ({0}/{1} read)",
                    saveGamePropertiesOffset + 46,
                    Size);
            }
        }

        /// <summary>
        /// Dumps contents of the building
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("X"));
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("X"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("X"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("X"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Zot Word: {0}", ZotWord);
            Console.WriteLine("Unknown1: {0}", Unknown1);
            Console.WriteLine("Appearance Flag: 0x{0}", AppearanceFlag.ToString("X"));
            Console.WriteLine("0x278128A0: 0x{0}", x278128A0.ToString("X"));
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

            Console.WriteLine("Unknown2: {0}", Unknown2);
            Console.WriteLine("Group ID: {0} [0x{1}]", GroupID, GroupID.ToString("X"));
            Console.WriteLine("Type ID: {0} [0x{1}]", TypeID, TypeID.ToString("X"));
            Console.WriteLine("Instance ID: {0} [0x{1}]", InstanceID, InstanceID.ToString("X"));
            Console.WriteLine("Instance ID (on appearance): {0} [0x{1}]", InstanceIDOnAppearance, InstanceIDOnAppearance.ToString("X"));
            Console.WriteLine("Min Coordinate X: {0}", MinCoordinateX);
            Console.WriteLine("Min Coordinate Y: {0}", MinCoordinateY);
            Console.WriteLine("Min Coordinate Z: {0}", MinCoordinateZ);
            Console.WriteLine("Max Coordinate X: {0}", MaxCoordinateX);
            Console.WriteLine("Max Coordinate Y: {0}", MaxCoordinateY);
            Console.WriteLine("Max Coordinate Z: {0}", MaxCoordinateZ);
            Console.WriteLine("Orientation: {0} [{1}]", Orientation, Constants.ORIENTATIONS[Orientation]);
            Console.WriteLine("Scaffolding Height: {0}", ScaffoldingHeight);
            Console.WriteLine("Prop Exemplar Reference: {0}", PropExemplarReference.ToString());
        }
    }
}
