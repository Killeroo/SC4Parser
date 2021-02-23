using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Represenation of a city's bridge tiles which are found in the bridge network subfile
    /// </summary>
    /// <remarks>
    /// As the name suggests the bridge network subfile contains every bridge tile in a city.
    /// This was reverse engineered by me, it follows a similar structure to the other network tiles.
    /// 
    /// This implementation is not complete (these tiles are big and they vary A LOT in size and I am not sure
    /// why)
    /// </remarks>
    /// <see cref="SC4Parser.Subfiles.BridgeNetworkSubfile"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkTile1"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkTile2"/>
    public class BridgeNetworkTile
    {
        uint RecordSize;
        uint CRC;
        uint Memory;
        ushort UnknownVersion1;
        ushort UnknownVersion2;
        ushort ZotBytes;
        byte AppearanceFlag;
        uint C772BF98;
        byte MinTractXCoordinate;
        byte MinTractZCoordinate;
        byte MaxTractXCoordinate;
        byte MaxTractZCoordinate;
        ushort TractSizeX;
        ushort TractSizeZ;
        uint TextureID;
        byte Orientation;
        byte NetworkType;
        byte WestConnection;
        byte NorthConnection;
        byte EastConnection;
        byte SouthConnection;

        uint SaveGamePropertyCount;
        List<SaveGameProperty> SaveGamePropertyEntries = new List<SaveGameProperty>();

        float MaxSizeX;
        float MaxSizeY;
        float MaxSizeZ;
        float MinSizeX;
        float MinSizeY;
        float MinSizeZ;

        float PosX1;
        float PosY1;
        float PosZ1;
                   
        float PosX2;
        float PosY2;
        float PosZ2;

        float PosX3;
        float PosY3;
        float PosZ3;

        float PosX4;
        float PosY4;
        float PosZ4;

        /// <summary>
        /// Parses a bridge network tile (from Bridge network subfile) from a byte array
        /// </summary>
        /// <param name="buffer">Buffer to read tile from</param>
        /// <param name="offset">Position in the buffer to start reading data from</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, uint offset)
        {
            uint internalOffset = 0;

            RecordSize = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CRC = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Memory = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //ushort UnknownUShort1 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            internalOffset += 2;
            UnknownVersion1 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            UnknownVersion2 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            ZotBytes = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            //byte UnknownByte1 = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset++;
            AppearanceFlag = Extensions.ReadByte(buffer, ref internalOffset);
            C772BF98 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinTractXCoordinate = Extensions.ReadByte(buffer, ref internalOffset);
            MinTractZCoordinate = Extensions.ReadByte(buffer, ref internalOffset);
            MaxTractXCoordinate = Extensions.ReadByte(buffer, ref internalOffset);
            MaxTractZCoordinate = Extensions.ReadByte(buffer, ref internalOffset);
            TractSizeX = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            TractSizeZ = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            SaveGamePropertyCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            if (SaveGamePropertyCount > 0)
                SaveGamePropertyEntries = SaveGameProperty.ExtractFromBuffer(buffer, SaveGamePropertyCount, ref internalOffset);
            internalOffset += 13;
            MaxSizeX = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeY = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeZ = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeX = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeY = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeZ = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //uint UnknownUint1 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 12;
            PosX1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosY1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosZ1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat3 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat4 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //uint UnknownUint2 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 12;
            PosX2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosY2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosZ2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat5 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat6 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //uint UnknownUint3 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 12;
            PosX3 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosY3 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosZ3 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat7 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //float UnknownFloat8 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            //uint UnknownUint4 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 12;
            TextureID = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 5;
            Orientation = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset += 3;
            NetworkType = Extensions.ReadByte(buffer, ref internalOffset);
            WestConnection = Extensions.ReadByte(buffer, ref internalOffset);
            NorthConnection = Extensions.ReadByte(buffer, ref internalOffset);
            EastConnection = Extensions.ReadByte(buffer, ref internalOffset);
            SouthConnection = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset += 8;
            PosX4 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosY4 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            PosZ4 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
        }

        /// <summary>
        /// Prints out the contents of the networktile 
        /// </summary>
        public void Dump()
        {

            Console.WriteLine("Record Size: {0}", RecordSize.ToString("X"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("x"));
            Console.WriteLine("Memory: 0x{0}", Memory);
            Console.WriteLine("Major Version: {0}", UnknownVersion1); // Always 8
            Console.WriteLine("Minor Version: {0}", UnknownVersion2); // Always 4
            Console.WriteLine("Zot Bytes: {0}", ZotBytes); // ALways 0
            Console.WriteLine("Appearance Flag: {0}", AppearanceFlag); // Always 5;
            Console.WriteLine("0xC772BF98: 0x{0}", C772BF98.ToString("x")); // Always same
            Console.WriteLine("Min Tract X: 0x{0}", MinTractXCoordinate.ToString("x"));
            Console.WriteLine("Min Tract Z: 0x{0}", MinTractZCoordinate.ToString("x"));
            Console.WriteLine("Max Tract X: 0x{0}", MaxTractXCoordinate.ToString("x"));
            Console.WriteLine("Max Tract Z: 0x{0}", MaxTractZCoordinate.ToString("x"));
            Console.WriteLine("Tract Size X: {0}", TractSizeX); // Always 2
            Console.WriteLine("Tract Size Z: {0}", TractSizeZ); // Always 2
            Console.WriteLine("Properties Count: {0}", SaveGamePropertyCount); // Between 1 and 2 (1 seems to be a height)
            
            // Dump any savegame properties if they are present
            if (SaveGamePropertyCount > 0)
            {
                for (int i = 0; i < SaveGamePropertyCount; i++)
                {
                    Console.WriteLine("==================");
                    SaveGamePropertyEntries[i].Dump();
                }
            }

            Console.WriteLine("Max Size X: {0}", MaxSizeX);
            Console.WriteLine("Max Size Y: {0}", MaxSizeY);
            Console.WriteLine("Max Size Z: {0}", MaxSizeZ);
            Console.WriteLine("Min Size X: {0}", MinSizeX);
            Console.WriteLine("Min Size Y: {0}", MinSizeY);
            Console.WriteLine("Min Size Z: {0}", MinSizeZ);
            Console.WriteLine("Pos X (1): {0}", PosX1);
            Console.WriteLine("Pos Y (1): {0}", PosY1);
            Console.WriteLine("Pos Z (1): {0}", PosZ1);
            Console.WriteLine("Pos X (2): {0}", PosX2);
            Console.WriteLine("Pos Y (2): {0}", PosY2);
            Console.WriteLine("Pos Z (2): {0}", PosZ2);
            Console.WriteLine("Pos X (3): {0}", PosX3);
            Console.WriteLine("Pos Y (3): {0}", PosY3);
            Console.WriteLine("Pos Z (3): {0}", PosZ3);
            Console.WriteLine("TextureID: {0}", TextureID.ToString("X"));
            Console.WriteLine("Orientation: {0}", Orientation.ToString("X"));
            Console.WriteLine("Network Type: {0}", NetworkType.ToString("X"));
            Console.WriteLine("West Connection: {0} ", WestConnection.ToString("X"));
            Console.WriteLine("North Connection: {0} ", NorthConnection.ToString("X"));
            Console.WriteLine("East Connection: {0} ", EastConnection.ToString("X"));
            Console.WriteLine("South Connection: {0} ", SouthConnection.ToString("X"));
            Console.WriteLine("Pos X (4): {0}", PosX4);
            Console.WriteLine("Pos Y (4): {0}", PosY4);
            Console.WriteLine("Pos Z (4): {0}", PosZ4);
        }
    }
}
