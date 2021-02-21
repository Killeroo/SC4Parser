using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implementation of Network Subfile 1. Network subfile 1 seems to contain all the network tiles in a city that are at ground level.
    /// </summary>
    /// <remarks>
    /// Actual implementation of tiles found in this file can be found in DataStructure\NetworkTile1.cs
    /// 
    /// Implemented and references additional data from https://wiki.sc4devotion.com/index.php?title=Network_Subfiles.
    /// </remarks>
    /// <seealso cref="SC4Parser.DataStructures.NetworkTile1"/>
    /// <seealso cref="SC4Parser.Subfiles.NetworkSubfile2"/>
    public class NetworkSubfile1
    {
        /// <summary>
        /// Contains all network tiles in the network subfile
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkTile1"/>
        public List<NetworkTile1> NetworkTiles { get; private set; } = new List<NetworkTile1>();

        /// <summary>
        /// Read network subfile 1 from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="size">Size of the subfile</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, int size)
        {
            Logger.Log(LogLevel.Info, "Parsing Network subfile 1...");

            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            uint OldRecordSize = 0;
            uint OldCRC = 0;
            uint OldMemory = 0;
            ushort OldUnknownUShort1 = 0;
            ushort OldUnknownVersion1 = 0;
            ushort OldUnknownVersion2 = 0;
            ushort OldZotBytes = 0;
            byte OldUnknownByte1 = 0;
            byte OldAppearanceFlag = 0;
            uint OldC772BF98 = 0;
            byte OldMinTractXCoordinate = 0;
            byte OldMinTractZCoordinate = 0;
            byte OldMaxTractXCoordinate = 0;
            byte OldMaxTractZCoordinate = 0;
            ushort OldTractSizeX = 0;
            ushort OldTractSizeZ = 0;
            uint OldSavegamePropertyCount = 0;
            float OldMaxSizeX1 = 0;
            float OldMaxSizeY1 = 0;
            float OldMaxSizeZ1 = 0;
            float OldMinSizeX1 = 0;
            float OldMinSizeY1 = 0;
            float OldMinSizeZ1 = 0;
            float OldUnknownFloat1 = 0;
            float OldUnknownFloat2 = 0;
            uint OldUnknownUint1 = 0;
            float OldPosX1 = 0;
            float OldPosY1 = 0;
            float OldPosZ1 = 0;
            float OldUnknownFloat3 = 0;
            float OldUnknownFloat4 = 0;
            uint OldUnknownUint2 = 0;
            float OldPosX2 = 0;
            float OldPosY2 = 0;
            float OldPosZ2 = 0;
            float OldUnknownFloat5 = 0;
            float OldUnknownFloat6 = 0;
            uint OldUnknownUint3 = 0;
            float OldPosX3 = 0;
            float OldPosY3 = 0;
            float OldPosZ3 = 0;
            float OldUnknownFloat7 = 0;
            float OldUnknownFloat8 = 0;
            uint OldUnknownUint4 = 0;


            // Loop through each byte in the subfile
            while (bytesToRead > 0)
            {
                // Work out the current tile size 
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                // Copy tile data out into it's own array
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);

                uint internalOffset = 0;

                uint RecordSize = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint CRC = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint Memory = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                ushort UnknownUShort1 = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                ushort UnknownVersion1 = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                ushort UnknownVersion2 = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                ushort ZotBytes = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                byte UnknownByte1 = Extensions.ReadByte(tileBuffer, ref internalOffset);
                byte AppearanceFlag = Extensions.ReadByte(tileBuffer, ref internalOffset);
                uint C772BF98 = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                byte MinTractXCoordinate = Extensions.ReadByte(tileBuffer, ref internalOffset);
                byte MinTractZCoordinate = Extensions.ReadByte(tileBuffer, ref internalOffset);
                byte MaxTractXCoordinate = Extensions.ReadByte(tileBuffer, ref internalOffset);
                byte MaxTractZCoordinate = Extensions.ReadByte(tileBuffer, ref internalOffset);;
                ushort TractSizeX = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                ushort TractSizeZ = BitConverter.ToUInt16(Extensions.ReadBytes(tileBuffer, 2, ref internalOffset), 0);
                uint SaveGamePropertyCount = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                List<SaveGameProperty> saveGameProperties = new List<SaveGameProperty>();
                if (SaveGamePropertyCount > 0)
                    saveGameProperties = SaveGameProperty.ExtractFromBuffer(tileBuffer, SaveGamePropertyCount, ref internalOffset);
                internalOffset += 13;
                float MaxSizeX1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float MaxSizeY1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float MaxSizeZ1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float MinSizeX1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float MinSizeY1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float MinSizeZ1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat2 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint UnknownUint1 = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosX1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosY1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0); 
                float PosZ1 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat3 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat4 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint UnknownUint2 = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosX2 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosY2 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosZ2 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat5 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat6 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint UnknownUint3 = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosX3 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosY3 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float PosZ3 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat7 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                float UnknownFloat8 = BitConverter.ToSingle(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);
                uint UnknownUint4 = BitConverter.ToUInt32(Extensions.ReadBytes(tileBuffer, 4, ref internalOffset), 0);

                if (RecordSize == OldRecordSize)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Record Size      => {0}", RecordSize);
                if (CRC == OldCRC)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CRC              => {0}", CRC);
                if (Memory == OldMemory)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Memory           => {0}", Memory);
                if (UnknownUShort1 == OldUnknownUShort1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownUShort1   => {0}", UnknownUShort1); // Always 4
                if (UnknownVersion1 == OldUnknownVersion1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Major Version    => {0}", UnknownVersion1); // Always 8
                if (UnknownVersion2 == OldUnknownVersion2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Minor Version    => {0}", UnknownVersion2); // Always 4
                if (ZotBytes == OldZotBytes)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Zot Bytes        => {0}", ZotBytes); // ALways 0
                if (UnknownByte1 == OldUnknownByte1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownByte1     => {0}", UnknownByte1); // Always 0;
                if (AppearanceFlag == OldAppearanceFlag)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Appearance Flag  => {0}", AppearanceFlag); // Always 5;
                if (C772BF98 == OldC772BF98)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0xC772BF98       => 0x{0}", C772BF98.ToString("x")); // Always same
                if (MinTractXCoordinate == OldMinTractXCoordinate)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Min Tract X      => 0x{0}", MinTractXCoordinate.ToString("x"));
                if (MinTractZCoordinate == OldMinTractZCoordinate)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Min Tract Z      => 0x{0}", MinTractZCoordinate.ToString("x"));
                if (MaxTractXCoordinate == OldMaxTractXCoordinate)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Max Tract X      => 0x{0}", MaxTractXCoordinate.ToString("x"));
                if (MaxTractZCoordinate == OldMaxTractZCoordinate)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Max Tract Z      => 0x{0}", MaxTractZCoordinate.ToString("x"));
                if (TractSizeX == OldTractSizeX)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tract Size X     => {0}", TractSizeX); // Always 2
                if (TractSizeZ == OldTractSizeZ)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tract Size Z     => {0}", TractSizeZ); // Always 2
                if (SaveGamePropertyCount == OldSavegamePropertyCount)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Properties Count => {0}", SaveGamePropertyCount); // Between 1 and 2 (1 seems to be height)
                Console.ResetColor();
                saveGameProperties.ForEach(x => x.Dump());
                if (MaxSizeX1 == OldMaxSizeX1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Max Size X (1)   => {0}", MaxSizeX1);
                if (MaxSizeY1 == OldMaxSizeY1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Max Size Y (1)   => {0}", MaxSizeY1);
                if (MaxSizeZ1 == OldMaxSizeZ1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Max Size Z (1)   => {0}", MaxSizeZ1);
                if (MinSizeX1 == OldMinSizeX1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Min Size X (1)   => {0}", MinSizeX1);
                if (MinSizeY1 == OldMinSizeY1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Min Size Y (1)   => {0}", MinSizeY1); 
                if (MinSizeZ1 == OldMinSizeZ1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Min Size Z (1)   => {0}", MinSizeZ1);
                if (UnknownFloat1 == OldUnknownFloat1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat1    => {0}", UnknownFloat1);// Usually 0 or 1
                if (UnknownFloat2 == OldUnknownFloat2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat2    => {0}", UnknownFloat2); // Usually 0 or 1
                if (UnknownUint1 == OldUnknownUint1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownUint1    => {0}", UnknownUint1.ToString("X"));
                if (PosX1 == OldPosX1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos X (1)       => {0}", PosX1);
                if (PosY1 == OldPosY1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Y (1)       => {0}", PosY1);
                if (PosZ1 == OldPosZ1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Z (1)       => {0}", PosZ1);
                if (UnknownFloat3 == OldUnknownFloat3)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat3    => {0}", UnknownFloat3);// Usually 0 or 1
                if (UnknownFloat4 == OldUnknownFloat4)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat4    => {0}", UnknownFloat4); // Usually 0 or 1
                if (UnknownUint2 == OldUnknownUint2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownUint2    => {0}", UnknownUint2.ToString("X"));
                if (PosX2 == OldPosX2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos X (2)       => {0}", PosX2);
                if (PosY2 == OldPosY2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Y (2)       => {0}", PosY2);
                if (PosZ2 == OldPosZ2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Z (2)       => {0}", PosZ2);
                if (UnknownFloat5 == OldUnknownFloat5)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat5    => {0}", UnknownFloat5);// Usually 0 or 1
                if (UnknownFloat6 == OldUnknownFloat6)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat6    => {0}", UnknownFloat6); // Usually 0 or 1
                if (UnknownUint3 == OldUnknownUint3)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownUint3    => {0}", UnknownUint3.ToString("X"));
                if (PosX3 == OldPosX3)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos X (3)       => {0}", PosX3);
                if (PosY3 == OldPosY3)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Y (3)       => {0}", PosY3);
                if (PosZ3 == OldPosZ3)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pos Z (3)       => {0}", PosZ3);
                if (UnknownFloat7 == OldUnknownFloat7)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat7    => {0}", UnknownFloat7);// Usually 0 or 1
                if (UnknownFloat8 == OldUnknownFloat8)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownFloat8    => {0}", UnknownFloat8); // Usually 0 or 1
                if (UnknownUint4 == OldUnknownUint4)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnknownUint4    => {0}", UnknownUint4.ToString("X"));

                Console.ResetColor();
                Console.WriteLine("Offset: 0x{0}", internalOffset.ToString("x"));
                Console.WriteLine("=======================================");

                // offset 17: always 0000 uint
                // Parse and add to list
                //NetworkTile1 tile = new NetworkTile1();
                //tile.Parse(tileBuffer, 0);
                //NetworkTiles.Add(tile);

                OldRecordSize = RecordSize;
                OldCRC = CRC;
                OldMemory = Memory;
                OldUnknownUShort1 = UnknownUShort1;
                OldUnknownVersion1 = UnknownVersion1;
                OldUnknownVersion2 = UnknownVersion2;
                OldZotBytes = ZotBytes;
                OldUnknownByte1 = UnknownByte1;
                OldAppearanceFlag = AppearanceFlag;
                OldC772BF98 = C772BF98;
                OldMinTractXCoordinate = MinTractXCoordinate;
                OldMinTractZCoordinate = MinTractZCoordinate; 
                OldMaxTractXCoordinate = MaxTractXCoordinate;
                OldMaxTractZCoordinate = MaxTractZCoordinate;
                OldTractSizeX = TractSizeX;
                OldTractSizeZ = TractSizeZ;
                OldSavegamePropertyCount = SaveGamePropertyCount;
                OldMaxSizeX1 = MaxSizeX1;
                OldMaxSizeY1 = MaxSizeY1;
                OldMaxSizeZ1 = MaxSizeZ1;
                OldMinSizeX1 = MinSizeX1;
                OldMinSizeY1 = MinSizeY1;
                OldMinSizeZ1 = MinSizeZ1;
                OldUnknownFloat1 = UnknownFloat1;
                OldUnknownFloat2 = UnknownFloat2;
                OldUnknownUint1 = UnknownUint1;
                OldPosX1 = PosX1;
                OldPosY1 = PosY1;
                OldPosZ1 = PosZ1;
                OldUnknownFloat3 = UnknownFloat3;
                OldUnknownFloat4 = UnknownFloat4;
                OldUnknownUint2 = UnknownUint2;
                OldPosX2 = PosX2;
                OldPosY2 = PosY2;
                OldPosZ2 = PosZ2;
                OldUnknownFloat5 = UnknownFloat5;
                OldUnknownFloat6 = UnknownFloat6;
                OldUnknownUint3 = UnknownUint3;
                OldPosX3 = PosX3;
                OldPosY3 = PosY3;
                OldPosZ3 = PosZ3;
                OldUnknownFloat7 = UnknownFloat7;
                OldUnknownFloat8 = UnknownFloat8;
                OldUnknownUint4 = UnknownUint4;

                // Record how much we have read and how far we have gone and move on
                // (deep)
                offset += recordSize;
                bytesToRead -= recordSize;

                Console.WriteLine("offset: {0} size: {1}", offset.ToString("x"), recordSize);

                
                //Logger.Log(LogLevel.Debug, $"Network tile read ({size}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Network Subfile 1 ({bytesToRead} left)");
            }

            Logger.Log(LogLevel.Info, "Network subfile 1 parsed");
        }

        public NetworkTile1 FindTile(uint memoryReference)
        {
            foreach (var tile in NetworkTiles)
            {
                if (tile.Memory == memoryReference)
                {
                    Console.WriteLine("found reference");
                    return tile;
                }
            }

            return null;
        }

        /// <summary>
        /// Prints out the contents of the subfile
        /// </summary>
        public void Dump()
        {
            foreach (var tile in NetworkTiles)
            {
                Console.WriteLine("--------------------");
                tile.Dump();
            }
        }
    }
}
