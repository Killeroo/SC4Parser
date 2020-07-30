using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures.Files.SubFiles
{
    //https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile
    class Lot
    {
        public uint Offset;
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public uint LotIID;
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
        public byte LotWidth;
        public byte LotDepth;
        public byte LotOrientation;
        public byte FlagByte2;
        public byte FlagByte3;
        public byte ZoneType;
        public byte ZoneWealth;
        public uint DateLotAppeared;
        public uint BuildingIID;
        public byte Unknown;

        public void Parse(byte[] buffer, uint offset)
        {
            Offset = offset;
            Size = BitConverter.ToUInt32(buffer, 0);
            CRC = BitConverter.ToUInt32(buffer, 4);
            Memory = BitConverter.ToUInt32(buffer, 8);
            MajorVersion = BitConverter.ToUInt16(buffer, 12);
            LotIID = BitConverter.ToUInt32(buffer, 14);
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
            LotWidth = buffer[37];
            LotDepth = buffer[38];
            FlagByte2 = buffer[39];
            FlagByte3 = buffer[40];
            ZoneType = buffer[41];
            ZoneWealth = buffer[42];
        }

        public void Dump()
        {
            Console.WriteLine("Offset: {0}", Offset);
            Console.WriteLine("Size: {0}", Size);
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("X"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("X"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Lot IID: {0}", LotIID);
            Console.WriteLine("FlagByte1: 0x{0}", FlagByte1.ToString("X"));
            Console.WriteLine("Min Tile X: 0x{0}", MinTileX.ToString("X"));
            Console.WriteLine("Min Tile Z: 0x{0}", MinTileZ.ToString("X"));
            Console.WriteLine("Max Tile X: 0x{0}", MaxTileX.ToString("X"));
            Console.WriteLine("Max Tile Z: 0x{0}", MaxTileZ.ToString("X"));
            Console.WriteLine("Commute Tile X: 0x{0}", MaxTileX.ToString("X"));
            Console.WriteLine("Commute Tile Z: 0x{0}", MaxTileZ.ToString("X"));
        }

    }
}
