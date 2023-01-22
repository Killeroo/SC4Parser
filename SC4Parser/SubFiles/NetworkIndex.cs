using System;
using System.Collections.Generic;

namespace SC4Parser
{

    /// <summary>
    /// Network tile reference, this is the representation of a network tile that is 
    /// stored in the Network Index Subfile
    /// </summary>
    /// <see cref="SC4Parser.NetworkIndex"/>
    public class NetworkTileReference
    {
        /// <summary>
        /// The tile's count in the city
        /// </summary>
        /// <remarks>
        /// Tile numbering starts in the NW corner, which is tile number 0x00000000.
        /// - Tile number 0x00000001 is to the east of that tile.
        /// - In a small city, the first tile in the second row is 0x00000040.
        /// - In a medium city, the first tile in the second row is 0x00000080.
        /// - In a large city, the first tile in the second row is 0x00000100.
        /// - In a small city, the last tile in the last row is 0x00000FFF.
        /// - In a medium city, the last tile in the last row is 0x00003FFF.
        /// - In a large city, the last tile in the last row is 0x0000FFFF.
        /// (info from https://wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Index_Subfile_Body)
        /// </remarks>
        public uint TileNumber { private set; get; }
        /// <summary>
        /// Memory address of the network tile
        /// </summary>
        /// <see cref="SC4Parser.NetworkTile1.Memory"/>
        /// <see cref="SC4Parser.NetworkTile2.Memory"/>
        /// <see cref="SC4Parser.BridgeNetworkTile.Memory"/>
        public uint MemoryAddressRef { private set; get; }
        /// <summary>
        /// ID of the subfile that stores the network tile
        /// </summary>
        /// <see cref="SC4Parser.Constants.NETWORK_SUBFILE_1_TYPE"/>
        /// <see cref="SC4Parser.Constants.NETWORK_SUBFILE_2_TYPE"/>
        /// <see cref="SC4Parser.Constants.BRIDGE_NETWORK_SUBFILE_TYPE"/>
        /// <see cref="SC4Parser.Constants.TUNNEL_NETWORK_SUBFILE_TYPE"/>
        public uint SubfileTypeIDRef { private set; get; }

        /// <summary>
        /// Parses a single Network Tile reference. Returns offset after block has been parsed
        /// </summary>
        /// <param name="buffer">Data to parse block from</param>
        /// <param name="offset">Where to start parsing block</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, ref uint offset)
        {
            TileNumber = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            MemoryAddressRef = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            SubfileTypeIDRef = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref offset), 0);
        }

        /// <summary>
        /// Prints out the contents of the network block
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Tile Number: 0x{0}", TileNumber.ToString("x8"));
            Console.WriteLine("Tile Memory Reference: 0x{0}", MemoryAddressRef.ToString("x8"));
            Console.WriteLine("Tile Subfile: 0x{0}", SubfileTypeIDRef.ToString("x8"));
        }
    }

    /// <summary>
    /// Incomplete Network Index implementation.
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Index_Subfile_Body
    /// </summary>
    /// <remarks>
    /// Partially completed. DO NOT USE, will probably crash.
    /// </remarks>
    public class NetworkIndex
    {
        /// <summary>
        /// Size of subfile
        /// </summary>
        public uint SubfileSize { get; private set; }
        /// <summary>
        /// Subfile's CRC
        /// </summary>
        public uint CRC { get; private set; }
        /// <summary>
        /// Subfile's memory address
        /// </summary>
        public uint MemoryAddress { get; private set; }
        /// <summary>
        /// Major version of subfile
        /// </summary>
        public ushort MajorVersion { get; private set; }
        /// <summary>
        /// Number of tiles in city
        /// </summary>
        public uint CityTileCount { get; private set; }
        /// <summary>
        /// Number of network tiles in city
        /// </summary>
        public uint NetworkTileCount { get; private set; }

        /// <summary>
        /// List of all network tiles stored in the index file
        /// </summary>
        /// <see cref="SC4Parser.NetworkTileReference"/>
        List<NetworkTileReference> NetworkTileReferences = new List<NetworkTileReference>();

        /// <summary>
        /// Parses Network Index Subfile 
        /// </summary>
        /// <param name="buffer">Buffer to read file from</param>
        /// <remarks>
        /// Incompleted. DO NOT USE, will probably crash.
        /// </remarks>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer)
        {
            uint internalOffset = 0;

            SubfileSize = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CRC = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MemoryAddress = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MajorVersion = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            CityTileCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkTileCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

            for (int index = 0; index < NetworkTileCount; index++)
            {
                NetworkTileReference reference = new NetworkTileReference();

                reference.Parse(buffer, ref internalOffset);
                NetworkTileReferences.Add(reference); // Add to list

                // Trying to parse some unknown reference stuff
                uint BlockCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

                // Traverse over this unknown block structure, we would skip it but has a very variable size so we just
                // go through it so we correctly have the offset incremented
                for (int blocknum = 0; blocknum < BlockCount; blocknum++)
                {
                    uint BlockNumber = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                    uint Count = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

                    //Console.WriteLine("=========");
                    //Console.WriteLine(blocknum);
                    //Console.WriteLine(Count);
                    for (int blockchunks = 0; blockchunks < Count; blockchunks++)
                    {
                        //Console.WriteLine(blockchunks);
                        internalOffset += 8;
                    }
                }

                // Have to traverse over these because we don't know how big the index references are 
                byte UnknownByte = Extensions.ReadByte(buffer, ref internalOffset);
                uint UnknownUint1 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                uint UnknownUint2 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                uint UnknownUint3 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                uint UnknownUint4 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

                float UnknownFloat1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort1 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort2 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort3 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort4 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                float UnknownFloat2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort5 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort6 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort7 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort8 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                float UnknownFloat3 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort9 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort10 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort11 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort12 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                float UnknownFloat4 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort13 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort14 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort15 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort16 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);

                // there are a variable number of counts at the end, one of these might be a count 
                ushort UnknownShort17 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                uint UnknownUint5 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                if (UnknownUint5 == 0) // Skip over this :/
                {
                    continue;
                }

                uint UnknownUint6 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                float UnknownFloat5 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort18 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort19 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                ushort UnknownShort20 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                float UnknownFloat6 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                ushort UnknownShort21 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);

                //Console.WriteLine("#######################################");
                //Console.WriteLine(TileNumber.ToString("x8"));
                //Console.WriteLine(MemoryAddressRef.ToString("x8"));
                //Console.WriteLine(SubfileTypeIDRef.ToString("x8"));
                //Console.WriteLine(BlockCount);

                //Console.WriteLine(UnknownByte);
                //Console.WriteLine(UnknownUint1);
                //Console.WriteLine(UnknownUint2);
                //Console.WriteLine(UnknownUint3);
                //Console.WriteLine(UnknownUint4);

                //Console.WriteLine(UnknownFloat1);
                //Console.WriteLine(UnknownShort1);
                //Console.WriteLine(UnknownShort2);
                //Console.WriteLine(UnknownShort3);
                //Console.WriteLine(UnknownShort4);
                //Console.WriteLine(UnknownFloat2);
                //Console.WriteLine(UnknownShort5);
                //Console.WriteLine(UnknownShort6);
                //Console.WriteLine(UnknownShort7);
                //Console.WriteLine(UnknownShort8);
                //Console.WriteLine(UnknownFloat3);
                //Console.WriteLine(UnknownShort9);
                //Console.WriteLine(UnknownShort10);
                //Console.WriteLine(UnknownShort11);
                //Console.WriteLine(UnknownShort12);
                //Console.WriteLine(UnknownFloat4);
                //Console.WriteLine(UnknownShort13);
                //Console.WriteLine(UnknownShort14);
                //Console.WriteLine(UnknownShort15);
                //Console.WriteLine(UnknownShort16);

                //Console.WriteLine("->");
                //Console.WriteLine(UnknownShort17);
                //Console.WriteLine(UnknownUint5);
                //Console.WriteLine(UnknownUint6);
                //Console.WriteLine(UnknownFloat5);
                //Console.WriteLine(UnknownShort18);
                //Console.WriteLine(UnknownShort19);
                //Console.WriteLine(UnknownShort20);
                //Console.WriteLine(UnknownFloat6);
                //Console.WriteLine(UnknownShort21);



            }

        }

        /// <summary>
        /// Prints out the contents of the file
        /// </summary>
        public void Dump()
        {
            Console.WriteLine(SubfileSize);
            Console.WriteLine(CRC.ToString("X"));
            Console.WriteLine(MemoryAddress.ToString("X"));
            Console.WriteLine(MajorVersion);
            Console.WriteLine(CityTileCount);
            Console.WriteLine(NetworkTileCount);
            Console.WriteLine();

            foreach (var reference in NetworkTileReferences)
            {
                Console.WriteLine("======================");
                reference.Dump();
            }
        }

    }
}
