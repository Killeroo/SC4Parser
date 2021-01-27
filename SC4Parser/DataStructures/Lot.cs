using System;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Representation of a Simcity 4 lot as it is stored in a save game
    /// </summary>
    /// <remarks>
    /// This implementation is not complete.
    /// 
    /// Implemented from https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile
    /// </remarks>
    /// <example>
    /// <c>
    /// How to read and use lot data using library
    /// // (this is effectively what is done in SC4Save.GetLotSubfile())
    /// 
    /// // Load save game
    /// SC4SaveFile savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
    /// 
    /// // load Lot Subfile from save
    /// LotSubfile lotSubfile = new LotSubfile();
    /// IndexEntry lotEntry = savegame.FindIndexEntryWithType("C9BD5D4A")
    /// byte[] lotSubfileData = savegame.LoadIndexEntry(lotEntry.TGI);
    /// lotSubfile.Parse(lotSubfileData, lotSubfileData.Length);
    /// 
    /// // loop through lots and print out their sizes
    /// foreach (Lot lot in lotSubfile.Lots)
    /// {
    ///     Console.Writeline(lot.SizeX + "x" + lot.SizeZ);
    /// }
    /// </c>
    /// </example>
    /// <see cref="SC4Parser.Subfiles.LotSubfile"/>
    /// <seealso cref="SC4Parser.DataStructures.Building"/>
    public class Lot
    {
        /// <summary>
        /// Position of lot within DBPf file
        /// </summary>
        public uint Offset { get; private set; }
        /// <summary>
        /// Size of lot
        /// </summary>
        public uint Size { get; private set; }
        /// <summary>
        /// Lot data's crc
        /// </summary>
        public uint CRC { get; private set; }
        /// <summary>
        /// Lot's memory
        /// </summary>
        public uint Memory { get; private set; }
        /// <summary>
        /// Lot's spec major version
        /// </summary>
        public ushort MajorVersion { get; private set; }
        /// <summary>
        /// Instance ID of the lot
        /// </summary>
        public uint LotInstanceID { get; private set; }
        /// <summary>
        /// Lot Flag byte 1 , can have one of the following values:
        ///     0x01 - Might have to do with road access
        ///     0x02 - Might have to do with road(job?) access
        ///     0x04 - Might have to do with road access
        ///     0x08 - Means the lot is watered
        ///     0x10 - Means the lot is powered
        ///     0x20 - Means the lot is marked historical
        ///     0x40 - Might mean the lot is built
        /// </summary>
        /// <remarks>
        /// Data from https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_1_-_Flag_Byte_1
        /// </remarks>
        public byte FlagByte1 { get; private set; }
        /// <summary>
        /// Minimum tile X coordinate for lot
        /// </summary>
        public byte MinTileX { get; private set; }
        /// <summary>
        /// Minimum tile Z coordinate for lot
        /// </summary>
        public byte MinTileZ { get; private set; }
        /// <summary>
        /// Maximum tile X coordinate for lot
        /// </summary>
        public byte MaxTileX { get; private set; }
        /// <summary>
        /// Maximum tile Z coordinate for lot
        /// </summary>
        public byte MaxTileZ { get; private set; }
        /// <summary>
        /// Lot's commute tile X
        /// </summary>
        public byte CommuteTileX { get; private set; }
        /// <summary>
        /// Lot's commute tile Z
        /// </summary>
        public byte CommuteTileZ { get; private set; }
        /// <summary>
        /// Lot's Y position
        /// </summary>
        public float PositionY { get; private set; }
        /// <summary>
        /// Lot's Y coordinate if slope is conforming
        /// </summary>
        public float Slope1Y { get; private set; }
        /// <summary>
        /// Lot's Y coordinate if slope is conforming 
        /// </summary>
        public float Slope2Y { get; private set; }
        /// <summary>
        /// Lot width
        /// </summary>
        public byte SizeX { get; private set; }
        /// <summary>
        /// Lot depth
        /// </summary>
        public byte SizeZ { get; private set; }
        /// <summary>
        /// Lot's orientation
        /// </summary>
        /// <see cref="SC4Parser.Constants.ORIENTATION_NORTH"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_EAST"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_SOUTH"/>
        /// <see cref="SC4Parser.Constants.ORIENTATION_WEST"/>
        public byte Orientation { get; private set; }
        /// <summary>
        /// Lot flag byte 2, can be one of the following files:
        ///     0x01 - Flag Byte 1 = 0x10 - Powered (empty growable zones)
        ///     0x02 - Flag Byte 1 = 0x50 - Powered and built
        ///     0x03 - Flag Byte 1 = 0x58 - Powered, watered and built
        ///     0x04 - Seen it once on a tall office under construction
        ///     0x06 - Seen it once on a water tower without power
        /// </summary>
        /// <remarks>
        /// Information from: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_2_-_Flag_Byte_2
        /// </remarks>
        public byte FlagByte2 { get; private set; }
        /// <summary>
        /// Lot flag byte 3, unknown use
        /// </summary>
        /// <remarks>
        /// More information here: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_3_-_Flag_Byte_3
        /// </remarks>
        public byte FlagByte3 { get; private set; }
        /// <summary>
        /// Lot's zone type
        /// </summary>
        /// <see cref="SC4Parser.Constants.LOT_ZONE_TYPE_STRINGS"/>
        public byte ZoneType { get; private set; }
        /// <summary>
        /// Lot's zone wealth
        /// </summary>
        /// <see cref="SC4Parser.Constants.LOT_ZONE_WEALTH_STRINGS"/>
        public byte ZoneWealth { get; private set; }
        /// <summary>
        /// Date (in game?) that lot grew or was plopped
        /// </summary>
        public uint DateLotAppeared { get; private set; }
        /// <summary>
        /// Lot's associated building Instance ID
        /// </summary>
        public uint BuildingInstanceID { get; private set; }
        /// <summary>
        /// Unknown lot value
        /// </summary>
        public byte Unknown { get; private set; }

        /// <summary>
        /// Read an individual lot object from a byte array
        /// </summary>
        /// <param name="buffer">Data to read lot from</param>
        /// <param name="offset">Position in data to read lot from</param>
        /// <remarks>
        /// This implementation is not complete
        /// </remarks>
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
            LotInstanceID = BitConverter.ToUInt32(buffer, 14);
            FlagByte1 = buffer[18];
            MinTileX = buffer[19];
            MinTileZ = buffer[20];
            MaxTileX = buffer[21];
            MaxTileZ = buffer[22];
            CommuteTileX = buffer[23];
            CommuteTileZ = buffer[24];
            PositionY = BitConverter.ToSingle(buffer, 25);
            Slope1Y = BitConverter.ToSingle(buffer, 29);
            Slope2Y = BitConverter.ToSingle(buffer, 33);
            SizeX = buffer[37];
            SizeZ = buffer[38];
            Orientation = buffer[39];
            FlagByte2 = buffer[40];
            FlagByte3 = buffer[41];
            ZoneType = buffer[42];
            ZoneWealth = buffer[43];
        }

        /// <summary>
        /// Prints out the values of the lot
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("x8"));
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("x8"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("x8"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("x8"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Lot IID: 0x{0}", LotInstanceID.ToString("x8"));
            Console.WriteLine("Flag Byte 1: 0x{0}", FlagByte1.ToString("x8"));
            Console.WriteLine("Min Tile X: 0x{0} ({1})", MinTileX.ToString("x8"), MinTileX);
            Console.WriteLine("Min Tile Z: 0x{0} ({1})", MinTileZ.ToString("x8"), MinTileZ);
            Console.WriteLine("Max Tile X: 0x{0} ({1})", MaxTileX.ToString("x8"), MaxTileX);
            Console.WriteLine("Max Tile Z: 0x{0} ({1})", MaxTileZ.ToString("x8"), MaxTileZ);
            Console.WriteLine("Commute Tile X: 0x{0} ({1})", MaxTileX.ToString("x8"), MaxTileX);
            Console.WriteLine("Commute Tile Z: 0x{0} ({1})", MaxTileZ.ToString("x8"), MaxTileZ);
            Console.WriteLine("Position Y: {0}", PositionY);
            Console.WriteLine("Slope 1 Y: {0}", Slope1Y);
            Console.WriteLine("Slope 2 Y: {0}", Slope2Y);
            Console.WriteLine("Lot Width: 0x{0} ({1})", SizeX.ToString("x8"), SizeX);
            Console.WriteLine("Lot Depth: 0x{0} ({1})", SizeZ.ToString("x8"), SizeZ);
            Console.WriteLine("Lot Orientation: 0x{0} ({1})", Orientation.ToString("x8"), Constants.ORIENTATION_STRINGS[Orientation]);
            Console.WriteLine("Flag Byte 2: 0x{0} ({1})", FlagByte2.ToString("x8"), FlagByte2);
            Console.WriteLine("Flag Byte 3: 0x{0} ({1})", FlagByte3.ToString("x8"), FlagByte3);
            Console.WriteLine("Zone Type: 0x{0} ({1})", ZoneType.ToString("x8"), Constants.LOT_ZONE_TYPE_STRINGS[ZoneType]);
            Console.WriteLine("Zone Wealth: 0x{0} ({1})", ZoneWealth.ToString("x8"), Constants.LOT_ZONE_WEALTH_STRINGS[ZoneWealth]);
        }

    }
}
