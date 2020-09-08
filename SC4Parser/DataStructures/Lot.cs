using System;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Representation of Simcity 4 log data, as it is present in an SC4 save
    /// Implemented using the following spec:
    /// https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile
    /// </summary>
    public class Lot
    {
        public uint Offset;
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public uint LotID;
        public byte FlagByte1;
        public byte MinTileX;
        public byte MinTileZ;
        public byte MaxTileX;
        public byte MaxTileZ;
        public byte CommuteTileX;
        public byte CommuteTileZ;
        public float PositionY;
        public float Slope1Y;
        public float Slope2Y;
        public byte SizeX;
        public byte SizeZ;
        public byte Orientation;
        public byte FlagByte2;
        public byte FlagByte3;
        public byte ZoneType;
        public byte ZoneWealth;
        public uint DateLotAppeared;
        public uint BuildingID;
        public byte Unknown;

        /// <summary>
        /// Read an individual lot object from a byte array
        /// </summary>
        public void Parse(byte[] buffer, uint offset)
        {
            Offset = offset;
            Size = BitConverter.ToUInt32(buffer, 0);
            CRC = BitConverter.ToUInt32(buffer, 4);
            Memory = BitConverter.ToUInt32(buffer, 8);
            MajorVersion = BitConverter.ToUInt16(buffer, 12);
            LotID = BitConverter.ToUInt32(buffer, 14);
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
        /// Dump the contents of a lot
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Offset: {0} (0x{1})", Offset, Offset.ToString("X"));
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("X"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("X"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("X"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Lot IID: 0x{0}", LotID.ToString("X"));
            Console.WriteLine("Flag Byte 1: 0x{0}", FlagByte1.ToString("X"));
            Console.WriteLine("Min Tile X: 0x{0} ({1})", MinTileX.ToString("X"), MinTileX);
            Console.WriteLine("Min Tile Z: 0x{0} ({1})", MinTileZ.ToString("X"), MinTileZ);
            Console.WriteLine("Max Tile X: 0x{0} ({1})", MaxTileX.ToString("X"), MaxTileX);
            Console.WriteLine("Max Tile Z: 0x{0} ({1})", MaxTileZ.ToString("X"), MaxTileZ);
            Console.WriteLine("Commute Tile X: 0x{0} ({1})", MaxTileX.ToString("X"), MaxTileX);
            Console.WriteLine("Commute Tile Z: 0x{0} ({1})", MaxTileZ.ToString("X"), MaxTileZ);
            Console.WriteLine("Position Y: {0}", PositionY);
            Console.WriteLine("Slope 1 Y: {0}", Slope1Y);
            Console.WriteLine("Slope 2 Y: {0}", Slope2Y);
            Console.WriteLine("Lot Width: 0x{0} ({1})", SizeX.ToString("X"), SizeX);
            Console.WriteLine("Lot Depth: 0x{0} ({1})", SizeZ.ToString("X"), SizeZ);
            Console.WriteLine("Lot Orientation: 0x{0} ({1})", Orientation.ToString("X"), Constants.ORIENTATIONS[Orientation]);
            Console.WriteLine("Flag Byte 2: 0x{0} ({1})", FlagByte2.ToString("X"), FlagByte2);
            Console.WriteLine("Flag Byte 3: 0x{0} ({1})", FlagByte3.ToString("X"), FlagByte3);
            Console.WriteLine("Zone Type: 0x{0} ({1})", ZoneType.ToString("X"), Constants.LOT_ZONE_TYPES[ZoneType]);
            Console.WriteLine("Zone Wealth: 0x{0} ({1})", ZoneWealth.ToString("X"), Constants.LOT_ZONE_WEALTHS[ZoneWealth]);
        }

    }
}
