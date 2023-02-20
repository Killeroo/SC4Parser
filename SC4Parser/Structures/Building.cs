using System;
using System.Collections.Generic;

using SC4Parser.Logging;

namespace SC4Parser
{
    /// <summary>
    /// Representation of a building in Simcity 4, as it is stored in a save game
    /// </summary>
    /// <remarks>
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile
    /// </remarks>
    /// <see cref="SC4Parser.BuildingSubfile"/>
    /// <seealso cref="SC4Parser.Lot"/>
    /// <example>
    /// <c>
    /// // How to read and use building data using library
    /// // (this is effectively what is done in SC4Save.GetBuildingSubfile())
    /// 
    /// // Load save game
    /// SC4SaveFile savegame = null;
    /// try
    /// {
    ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
    /// }
    /// catch (DBPFParsingException)
    /// {
    ///     Console.Writeline("Issue occured while parsing DBPF");
    ///     return;
    /// }
    /// 
    /// // load Building Subfile from save
    /// BuildingSubfile buildingSubfile = new BuildingSubfile();
    /// try
    /// {
    ///     IndexEntry buildingEntry = savegame.FindIndexEntryWithType("A9BD882D")
    ///     byte[] buildingSubfileData = savegame.LoadIndexEntry(buildingEntry.TGI);
    ///     buildingSubfile.Parse(buildingSubfileData, buildingSubfileData.Length     
    /// }
    /// catch (Exception)
    /// {
    ///     Console.Writeline("Error loading building subfile);
    /// }
    /// 
    /// // loop through buildings and print out their TGIs
    /// foreach (Building building in buildingsSubfile.Buildings)
    /// {
    ///     Console.Writeline(building.TGI.ToString();
    /// }
    /// </c>
    /// </example>
    public class Building
    {
        /// <summary>
        /// Offset of building data within Building subfile
        /// </summary>
        public uint Offset { get; private set; }
        /// <summary>
        /// Size of building data
        /// </summary>
        public uint Size { get; private set; }
        /// <summary>
        /// CRC of building data
        /// </summary>
        public uint CRC { get; private set; }
        /// <summary>
        /// Memory reference of building data
        /// </summary>
        public uint Memory { get; private set; }
        /// <summary>
        /// Major version of building spec
        /// </summary>
        public ushort MajorVersion { get; private set; }
        /// <summary>
        /// Minor version of building spec
        /// </summary>
        public ushort MinorVersion { get; private set; }
        /// <summary>
        /// Zot word for building
        /// </summary>
        public ushort ZotWord { get; private set; }
        /// <summary>
        /// Unknown field 
        /// </summary>
        public byte Unknown1 { get; private set; }
        /// <summary>
        /// Appearance flag for building. Can be one of the following values:
        ///     0x01 (00000001b) - Building that appears in the game (if this is off, the building has been deleted).
        ///     0x02 (00000010b) - ? (unused).
        ///     0x04 (00000100b) - ? (always on).
        ///     0x08 (00001000b) - Flora
        ///     0x40 (01000000b) - Burnt
        /// </summary>
        public byte AppearanceFlag { get; private set; }
        /// <summary>
        /// Unknown value, is always the same for all buildings
        /// </summary>
        public uint x278128A0 { get; private set; }
        /// <summary>
        /// Minimum tract X coordinate for building
        /// </summary>
        public byte MinTractX { get; private set; }
        /// <summary>
        /// Minimum tract Z coordinate for building 
        /// </summary>
        public byte MinTractZ { get; private set; }
        /// <summary>
        /// Maximum tract X coordinate for building
        /// </summary>
        public byte MaxTractX { get; private set; }
        /// <summary>
        /// Maximum tract Z coordinate for building
        /// </summary>
        public byte MaxTractZ { get; private set; }
        /// <summary>
        /// Tract size on the X axis for building
        /// </summary>
        public ushort TractSizeX { get; private set; }
        /// <summary>
        /// Tract size on the Z axis for building
        /// </summary>
        public ushort TractSizeZ { get; private set; }
        /// <summary>
        /// Number of save game properties (SIGProps) associated with building
        /// </summary>
        /// <see cref="SC4Parser.SaveGameProperty"/>
        public uint SaveGamePropertyCount { get; private set; }
        /// <summary>
        /// Save game properties (SIGProps) of building
        /// </summary>
        /// <see cref="SC4Parser.SaveGameProperty"/>
        /// <remarks>
        /// For a list of all possible building SIGPROPs visit the following:
        /// https://wiki.sc4devotion.com/index.php?title=Building_Subfile#Savegame_Properties_.28SGProps.29
        /// </remarks>
        public List<SaveGameProperty> SaveGamePropertyEntries { get; private set; } = new List<SaveGameProperty>();
        /// <summary>
        /// Unknown field
        /// </summary>
        public byte Unknown2 { get; private set; }
        /// <summary>
        /// The Building's Group ID
        /// </summary>
        public uint GroupID { get; private set; }
        /// <summary>
        /// The Building's Type ID
        /// </summary>
        public uint TypeID { get; private set; }
        /// <summary>
        /// The Building's Instance ID
        /// </summary>
        public uint InstanceID { get; private set; }
        /// <summary>
        /// Building's Instance Id when the the building appears
        /// </summary>
        public uint InstanceIDOnAppearance { get; private set; }
        /// <summary>
        /// Minimum X coordinate of building
        /// </summary>
        public float MinCoordinateX { get; private set; }
        /// <summary>
        /// Minimum Y coordinate of building
        /// </summary>
        public float MinCoordinateY { get; private set; }
        /// <summary>
        /// Minimum Z coordinate of building
        /// </summary>
        public float MinCoordinateZ { get; private set; }
        /// <summary>
        /// Maximum Z coordinate of building
        /// </summary>
        public float MaxCoordinateX { get; private set; }
        /// <summary>
        /// Maximum Y coordinate of building
        /// </summary>
        public float MaxCoordinateY { get; private set; }
        /// <summary>
        /// Maximum Z coordinate of building
        /// </summary>
        public float MaxCoordinateZ { get; private set; }
        /// <summary>
        /// Building's orientation
        /// </summary>
        /// <see cref="SC4Parser.Constants.ORIENTATION_NORTH"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_EAST"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_SOUTH"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_WEST"/>
        public byte Orientation { get; private set; }
        /// <summary>
        /// Building's scaffolding height
        /// </summary>
        public float ScaffoldingHeight { get; private set; }

        /// <summary>
        /// TypeGroupInstance (TGI) of building, reference to prop exemplar
        /// </summary>
        /// <remarks>
        /// Same as typeid, groupid and instanceid from this file. Just included it for accessibility
        /// </remarks>
        /// <see cref="SC4Parser.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; private set; } = new TypeGroupInstance();

        /// <summary>
        /// Load a building from a byte array
        /// </summary>
        /// <param name="buffer">Data to load building from</param>
        /// <param name="offset">Position in data to read building from</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
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
            TGI = new TypeGroupInstance(TypeID, GroupID, InstanceID);
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
        /// Prints out the contents of the building
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("x8"));
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("x8"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("x8"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("x8"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Zot Word: {0}", ZotWord);
            Console.WriteLine("Unknown1: {0}", Unknown1);
            Console.WriteLine("Appearance Flag: 0x{0}", AppearanceFlag.ToString("x8"));
            Console.WriteLine("0x278128A0: 0x{0}", x278128A0.ToString("x8"));
            Console.WriteLine("MinTractX: {0} (0x{1}) MaxTractX: {2} (0x{3})", 
                MinTractX,
                MinTractX.ToString("x8"),
                MaxTractX,
                MaxTractX.ToString("x8"));
            Console.WriteLine("MinTractZ: {0} (0x{1}) MaxTractZ: {2} (0x{3})",
                MinTractZ,
                MinTractZ.ToString("x8"),
                MaxTractZ,
                MaxTractZ.ToString("x8"));
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
            Console.WriteLine("Group ID: {0} (0x{1})", GroupID, GroupID.ToString("x8"));
            Console.WriteLine("Type ID: {0} (0x{1})", TypeID, TypeID.ToString("x8"));
            Console.WriteLine("Instance ID: {0} (0x{1})", InstanceID, InstanceID.ToString("x8"));
            Console.WriteLine("Instance ID (on appearance): {0} (0x{1})", InstanceIDOnAppearance, InstanceIDOnAppearance.ToString("x8"));
            Console.WriteLine("Min Coordinate X: {0}", MinCoordinateX);
            Console.WriteLine("Min Coordinate Y: {0}", MinCoordinateY);
            Console.WriteLine("Min Coordinate Z: {0}", MinCoordinateZ);
            Console.WriteLine("Max Coordinate X: {0}", MaxCoordinateX);
            Console.WriteLine("Max Coordinate Y: {0}", MaxCoordinateY);
            Console.WriteLine("Max Coordinate Z: {0}", MaxCoordinateZ);
            Console.WriteLine("Orientation: {0} ({1})", Orientation, Constants.ORIENTATION_STRINGS[Orientation]);
            Console.WriteLine("Scaffolding Height: {0}", ScaffoldingHeight);
            Console.WriteLine("Prop Exemplar Reference: {0}", TGI.ToString());
        }
    }
}
