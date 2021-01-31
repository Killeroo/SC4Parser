using System;
using System.IO;
using System.Collections.Generic;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Network blocks found in with in Network subfile 2 entries
    /// </summary>
    /// <remarks>
    /// Purpose and usage is unknown.
    /// </remarks>
    /// <see cref="SC4Parser.DataStructures.NetworkTile2"/>
    public class NetworkBlock
    {
        /// <summary>
        /// X coordinate of network block
        /// </summary>
        public float X { get; private set; }
        /// <summary>
        /// Y coordinate of network block
        /// </summary>
        public float Y { get; private set; }
        /// <summary>
        /// Z coordinate of network block
        /// </summary>
        public float Z { get; private set; }
        /// <summary>
        /// Unknown float 1
        /// </summary>
        public float Unknown1 { get; private set; }
        /// <summary>
        /// Unknown float 2
        /// </summary>
        public float Unknown2 { get; private set; }
        /// <summary>
        /// Unknown uint 
        /// </summary>
        public uint Unknown3 { get; private set; }

        /// <summary>
        /// Parses a single network block. Returns offset after block has been parsed
        /// </summary>
        /// <param name="buffer">Data to parse block from</param>
        /// <param name="offset">Where to start parsing block</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, ref uint offset)
        {
            X = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            Y = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            Z = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            Unknown1 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            Unknown2 = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref offset), 0);
            Unknown3 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref offset), 0);
        }

        /// <summary>
        /// Prints out the contents of the network block
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("X: {0} Y: {1} Z: {2}", X, Y, Z);
            Console.WriteLine("Unknown1: {0}", Unknown1);
            Console.WriteLine("Unknown2: {0}", Unknown2);
            Console.WriteLine("Unknown3: {0} (0x{1})", Unknown3, Unknown3.ToString("x8"));
        }
    }

    /// <summary>
    /// Representation of a city's network tiles which are found in Network Subfile 2.
    /// </summary>
    /// <remarks>
    /// Network Subfile 2 contains network tiles that are below or above ground, so stuff like underground roads,
    /// subways or road bridges.
    /// 
    /// It is similarly structured but slightly bigger than those network tiles found in Network subfile 1.
    /// 
    /// Some unknown fields with no names have been skipped from tile implementation.
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Subfile_2_Structure
    /// </remarks>
    /// <see cref="SC4Parser.Subfiles.NetworkSubfile2"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkTile1"/>
    public class NetworkTile2
    {
        /// <summary>
        /// Size of network tile entry
        /// </summary>
        public uint Size { get; private set; }
        /// <summary>
        /// Network tile's crc
        /// </summary>
        public uint CRC { get; private set; }
        /// <summary>
        /// Network tile's memory
        /// </summary>
        public uint Memory { get; private set; }
        /// <summary>
        /// Network tile's major version
        /// </summary>
        public ushort MajorVersion { get; private set; }
        /// <summary>
        /// Network tile's minor version
        /// </summary>
        public ushort MinorVersion { get; private set; }
        /// <summary>
        /// Unknown version 
        /// </summary>
        public ushort UnknownVersion1 { get; private set; }
        /// <summary>
        /// Unknown version
        /// </summary>
        public ushort UnknownVersion2 { get; private set; }
        /// <summary>
        /// Appearance flag of betwork tile
        /// </summary>
        /// <remarks>
        /// Network tile can have the following appearance flag values:
        ///     0x01 (00000001b) - Network that appears in the game (if this is off, the network has been deleted)
        ///     0x02 (00000010b) - ? (unused)
        ///     0x04 (00000100b) - ? (always on)
        ///     0x08 (00001000b) - ? (unused)
        ///     0x40 (01000000b) - The network is burnt
        ///     0x80 (10000000b) - ? (unused)
        /// </remarks>
        public byte AppearanceFlag { get; private set; }
        /// <summary>
        /// Network tile's min x tract coordinate
        /// </summary>
        public byte MinTractX { get; private set; }
        /// <summary>
        /// Network tile's min z tract coordinate
        /// </summary>
        public byte MinTractZ { get; private set; }
        /// <summary>
        /// Network tile's max x tract coordinate
        /// </summary>
        public byte MaxTractX { get; private set; }
        /// <summary>
        /// Network tile's max z tract coordinate
        /// </summary>
        public byte MaxTractZ { get; private set; }
        /// <summary>
        /// Network tile's x tract size
        /// </summary>
        public ushort TractSizeX { get; private set; }
        /// <summary>
        /// Network tile's z tract size
        /// </summary>
        public ushort TractSizeZ { get; private set; }
        /// <summary>
        /// Number of save game properties (sigprops) attached to the network tile
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty"/>
        public uint SaveGamePropertyCount { get; private set; }
        /// <summary>
        /// Network tile save game properties (if any)
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty"/>
        public List<SaveGameProperty> SaveGamePropertyEntries { get; private set; } = new List<SaveGameProperty>();
        /// <summary>
        /// Network tile's Group ID
        /// </summary>
        public uint GroupID { get; private set; }
        /// <summary>
        /// Network tile's Type ID
        /// </summary>
        public uint TypeID { get; private set; }
        /// <summary>
        /// Network tile's Instance ID
        /// </summary>
        public uint InstanceID { get; private set; }
        /// <summary>
        /// Unknown flag 1
        /// </summary>
        public byte UnknownFlag1 { get; private set; }
        /// <summary>
        /// Network tile's Texture ID
        /// </summary>
        public uint TextureID { get; private set; }
        /// <summary>
        /// Network tile's orientation
        /// </summary>
        /// <seealso cref="Constants.ORIENTATION_STRINGS"/>
        public byte Orientation { get; private set; }

        /// <summary>
        /// Minimum x size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeX2"/>
        public float MinSizeX1 { get; private set; }
        /// <summary>
        /// Maximum x size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeX2"/>
        public float MaxSizeX1 { get; private set; }
        /// <summary>
        /// Minimum y size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This to be a quarter of the network tile's size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeY2"/>
        public float MinSizeY1 { get; private set; }
        /// <summary>
        /// Maximum y size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeY2"/>
        public float MaxSizeY1 { get; private set; }
        /// <summary>
        /// Minimum z size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeZ2"/>
        public float MinSizeZ1 { get; private set; }
        /// <summary>
        /// Maximum z size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeZ2"/>
        public float MaxSizeZ1 { get; private set; }

        /// <summary>
        /// X coordinate for the first set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos1X { get; private set; }
        /// <summary>
        /// Y coordinate for the first set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos1Y { get; private set; }
        /// <summary>
        /// Z coordinate for the first set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos1Z { get; private set; }

        /// <summary>
        /// X coordinate for the second set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos2X { get; private set; }
        /// <summary>
        /// Y coordinate for the second set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos2Y { get; private set; }
        /// <summary>
        /// Z coordinate for the second set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos2Z { get; private set; }

        /// <summary>
        /// X coordinate for the third set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos3X { get; private set; }
        /// <summary>
        /// Y coordinate for the third set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos3Y { get; private set; }
        /// <summary>
        /// Z coordinate for the third set of Network tile positions 
        /// </summary>
        /// <remark>
        /// These positions may have something to do with varying heights at different
        /// parts on the network tile (around the edges)
        /// </remark>
        public float Pos3Z { get; private set; }

        /// <summary>
        /// The network tile's type
        /// </summary>
        /// <see cref="SC4Parser.Constants.NETWORK_TYPE_STRINGS"/>
        public byte NetworkType { get; private set; }

        /// <summary>
        /// Specifies if the network tile is connected on it's west side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte WestConnection { get; private set; }
        /// <summary>
        /// Specifies if the network tile is connected on it's north side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte NorthConnection { get; private set; }
        /// <summary>
        /// Specifies if the network tile is connected on it's east side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte EastConnection { get; private set; }
        /// <summary>
        /// Specifies if the network tile is connected on it's south side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte SouthConnection { get; private set; }

        /// <summary>
        /// Minimum x size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeX1"/>
        public float MinSizeX2 { get; private set; }
        /// <summary>
        /// Maximum x size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeX1"/>
        public float MaxSizeX2 { get; private set; }
        /// <summary>
        /// Minimum y size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeY1"/>
        public float MinSizeY2 { get; private set; }
        /// <summary>
        /// Maximum y size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeY1"/>
        public float MaxSizeY2 { get; private set; }
        /// <summary>
        /// Minimum z size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MinSizeZ1"/>
        public float MinSizeZ2 { get; private set; }
        /// <summary>
        /// Maximum z size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile2.MaxSizeZ1"/>
        public float MaxSizeZ2 { get; private set; }

        /// <summary>
        /// Number of additional network blocks associated with the network tile
        /// </summary>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint ExtraBlocks { get; private set; }
        /// <summary>
        /// Number of blocks in the first set of network blocks
        /// </summary>
        /// <remarks>
        /// Min 0 blocks, max 4 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint NetworkBlockCount1 { get; private set; }
        /// <summary>
        /// Number of blocks in the second set of network blocks
        /// </summary>
        /// <remarks>
        /// Min 0 blocks, max 4 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint NetworkBlockCount2 { get; private set; }
        /// <summary>
        /// Number of blocks in the third set of network blocks
        /// </summary>
        /// <remarks>
        /// Min 0 blocks, max 4 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint NetworkBlockCount3 { get; private set; }
        /// <summary>
        /// Number of blocks in the fourth set of network blocks
        /// </summary>
        /// <remarks>
        /// Min 0 blocks, max 4 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint NetworkBlockCount4 { get; private set; }
        /// <summary>
        /// Number of blocks in the fifth set of network blocks
        /// </summary>
        /// <remarks>
        /// Min 0 blocks, max 4 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
        public uint NetworkBlockCount5 { get; private set; }

        /// <summary>
        /// First set of network blocks
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkBlock"/>
        public NetworkBlock[] NetworkBlocks1 { get; private set; }
        /// <summary>
        /// Second set of network blocks
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkBlock"/>
        public NetworkBlock[] NetworkBlocks2 { get; private set; }
        /// <summary>
        /// Third set of network blocks
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkBlock"/>
        public NetworkBlock[] NetworkBlocks3 { get; private set; }
        /// <summary>
        /// Fourth set of network blocks
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkBlock"/>
        public NetworkBlock[] NetworkBlocks4 { get; private set; }
        /// <summary>
        /// Fifth set of network blocks
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.NetworkBlock"/>
        public NetworkBlock[] NetworkBlocks5 { get; private set; }

        /// <summary>
        /// X coordinate for the fourth set of Network tile positions 
        /// </summary>
        public float Pos4X { get; private set; }
        /// <summary>
        /// Y coordinate for the fourth set of Network tile positions 
        /// </summary>
        public float Pos4Y { get; private set; }
        /// <summary>
        /// Z coordinate for the fourth set of Network tile positions 
        /// </summary>
        public float Pos4Z { get; private set; }

        /// <summary>
        /// Unknown flag 2
        /// </summary>
        public byte UnknownFlag2 { get; private set; }
        /// <summary>
        /// Unknown flag 3
        /// </summary>
        public byte UnknownFlag3 { get; private set; }
        /// <summary>
        /// Unknown flag 4
        /// </summary>
        public byte UnknownFlag4 { get; private set; }

        /// <summary>
        /// Network tile height value 1
        /// </summary>
        /// <remarks>
        /// Usage unknown
        /// </remarks>
        public float Height1 { get; private set; }
        /// <summary>
        /// Network tile height value 2
        /// </summary>
        /// <remarks>
        /// Usage unknown
        /// </remarks>
        public float Height2 { get; private set; }
        /// <summary>
        /// Network tile height value 3
        /// </summary>
        /// <remarks>
        /// Usage unknown
        /// </remarks>
        public float Height3 { get; private set; }
        /// <summary>
        /// Network tile height value 4
        /// </summary>
        /// <remarks>
        /// Usage unknown
        /// </remarks>
        public float Height4 { get; private set; }
        /// <summary>
        /// Network tile height value 5
        /// </summary>
        /// <remarks>
        /// Usage unknown
        /// </remarks>
        public float Height5 { get; private set; }

        /// <summary>
        /// File's type ID
        /// </summary>
        /// <remarks>
        /// Should always be 0xCA16374F
        /// </remarks>
        public uint FileTypeID { get; private set; }
        /// <summary>
        /// Unknown uint at the end of the network tile entry
        /// </summary>
        /// <remarks>
        /// Should always be 0x0000000
        /// </remarks>
        public uint UnknownUint { get; private set; }

        /// <summary>
        /// TypeGroupInstance (TGI) of network tile
        /// </summary>
        /// <remarks>
        /// Same as typeid, groupid and instanceid from this entry. Just included it for accessibility
        /// </remarks>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; private set; } = new TypeGroupInstance();

        /// <summary>
        /// Parses a network tile (from network subfile 2) from a byte array.
        /// </summary>
        /// <param name="buffer">buffer to parse from</param>
        /// <param name="offset">offset to start parsing at in the buffer</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        /// <seealso cref="SC4Parser.Subfiles.NetworkSubfile2"/>
        public void Parse(byte[] buffer, uint offset)
        {
            uint internalOffset = offset;

            // Ok why does this look different I hear the voice in my head ask?
            // Well I got sick of manually tracking the offset (especially for a file so big that can vary in size so much)
            // so I wrote a function Extensions.ReadBytes to copy bytes from the buffer into it's own array at the offset 
            // and automatically increment the offset, saves like 3 per entry... but look at that indentation tho

            Size                        = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CRC                         = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Memory                      = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MajorVersion                = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            MinorVersion                = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            UnknownVersion1             = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            UnknownVersion2             = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            internalOffset              += 3;
            AppearanceFlag              = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset              += 4;
            MinTractX                   = Extensions.ReadByte(buffer, ref internalOffset);
            MinTractZ                   = Extensions.ReadByte(buffer, ref internalOffset);
            MaxTractX                   = Extensions.ReadByte(buffer, ref internalOffset);
            MaxTractZ                   = Extensions.ReadByte(buffer, ref internalOffset);
            TractSizeX                  = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            TractSizeZ                  = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            SaveGamePropertyCount       = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            if (SaveGamePropertyCount > 0)
                SaveGamePropertyEntries = SaveGameProperty.ExtractFromBuffer(buffer, SaveGamePropertyCount, ref internalOffset);
            GroupID                     = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            TypeID                      = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            InstanceID                  = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            TGI                         = new TypeGroupInstance(TypeID, GroupID, InstanceID); // TODO: Should be only using this
            UnknownFlag1                = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset              += 36;
            MaxSizeX1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeY1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeZ1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeX1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeY1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeZ1                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            Pos1X                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos1Y                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos1Z                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            Pos2X                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos2Y                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos2Z                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            Pos3X                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos3Y                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            Pos3Z                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            TextureID                   = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 5;
            Orientation                 = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset              += 3;
            NetworkType                 = Extensions.ReadByte(buffer, ref internalOffset);
            WestConnection              = Extensions.ReadByte(buffer, ref internalOffset);
            NorthConnection             = Extensions.ReadByte(buffer, ref internalOffset);
            EastConnection              = Extensions.ReadByte(buffer, ref internalOffset);
            SouthConnection             = Extensions.ReadByte(buffer, ref internalOffset);
            internalOffset              += 4;
            MinSizeX2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeX2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeY2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeY2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MinSizeZ2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MaxSizeZ2                   = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 34;
            ExtraBlocks                 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlockCount1          = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlocks1 = new NetworkBlock[(int)NetworkBlockCount1];
            if (NetworkBlockCount1 > 0)
            {
                for (uint i = 0; i < NetworkBlockCount1; i++)
                {
                    NetworkBlock block = new NetworkBlock();
                    block.Parse(buffer, ref internalOffset);
                    NetworkBlocks1[i] = block;
                }
            }
            NetworkBlockCount2 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlocks2 = new NetworkBlock[(int)NetworkBlockCount2];
            if (NetworkBlockCount2 > 0)
            {
                for (uint i = 0; i < NetworkBlockCount2; i++)
                {
                    NetworkBlock block = new NetworkBlock();
                    block.Parse(buffer, ref internalOffset);
                    NetworkBlocks2[i] = block;
                }
            }
            NetworkBlockCount3 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlocks3 = new NetworkBlock[(int)NetworkBlockCount3];
            if (NetworkBlockCount3 > 0)
            {
                for (uint i = 0; i < NetworkBlockCount3; i++)
                {
                    NetworkBlock block = new NetworkBlock();
                    block.Parse(buffer, ref internalOffset);
                    NetworkBlocks3[i] = block;
                }
            }
            NetworkBlockCount4 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlocks4 = new NetworkBlock[(int)NetworkBlockCount4];
            if (NetworkBlockCount4 > 0)
            {
                for (uint i = 0; i < NetworkBlockCount4; i++)
                {
                    NetworkBlock block = new NetworkBlock();
                    block.Parse(buffer, ref internalOffset);
                    NetworkBlocks4[i] = block;
                }
            }
            NetworkBlockCount5 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            NetworkBlocks5 = new NetworkBlock[(int)NetworkBlockCount5];
            if (NetworkBlockCount5 > 0)
            {
                for (uint i = 0; i < NetworkBlockCount5; i++)
                {
                    NetworkBlock block = new NetworkBlock();
                    block.Parse(buffer, ref internalOffset);
                    NetworkBlocks5[i] = block;
                }
            }
            internalOffset              += 16;
            Pos4X                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            Pos4Y                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 12;
            Pos4Z                       = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset              += 24;
            UnknownFlag2                = Extensions.ReadByte(buffer, ref internalOffset);
            UnknownFlag3                = Extensions.ReadByte(buffer, ref internalOffset); 
            UnknownFlag4                = Extensions.ReadByte(buffer, ref internalOffset); 
            Height1                     = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); 
            Height2                     = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); 
            Height3                     = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); 
            Height4                     = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); 
            Height5                     = BitConverter.ToSingle(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); 
            FileTypeID                  = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            UnknownUint                 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

        }

        /// <summary>
        /// Prints out the contents of the network tile
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Size: {0} (0x{1})", Size, Size.ToString("x8"));
            Console.WriteLine("CRC: 0x{0}", CRC.ToString("x8"));
            Console.WriteLine("Memory address: 0x{0}", Memory.ToString("x8"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Appearance Flag: 0x{0}", AppearanceFlag.ToString("x8"));
            Console.WriteLine("Tract coordinates: {0} {1} {2} {3} (0x{4} 0x{5} 0x{6} 0x{7})",
                MinTractX,
                MaxTractX,
                MinTractZ,
                MaxTractZ,
                MinTractX.ToString("x8"),
                MaxTractX.ToString("x8"),
                MinTractZ.ToString("x8"),
                MaxTractZ.ToString("x8"));
            Console.WriteLine("TractSizeX: {0} (0x{1})", TractSizeX, TractSizeX.ToString("x8"));
            Console.WriteLine("TractSizeZ: {0} (0x{1})", TractSizeZ, TractSizeX.ToString("x8"));
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

            Console.WriteLine("Group ID: {0} (0x{1})", GroupID, GroupID.ToString("x8"));
            Console.WriteLine("Type ID: {0} (0x{1})", TypeID, TypeID.ToString("x8"));
            Console.WriteLine("Instance ID: {0} (0x{1})", InstanceID, InstanceID.ToString("x8"));

            Console.WriteLine("MaxSizeX1: {0}", MaxSizeX1);
            Console.WriteLine("MaxSizeY1: {0}", MaxSizeY1);
            Console.WriteLine("MaxSizeZ1: {0}", MaxSizeZ1);
            Console.WriteLine("MinSizeX1: {0}", MinSizeX1);
            Console.WriteLine("MinSizeY1: {0}", MinSizeY1);
            Console.WriteLine("MinSizeZ1: {0}", MinSizeZ1);

            Console.WriteLine("Pos1X: {0}", Pos1X);
            Console.WriteLine("Pos1Y: {0}", Pos1Y);
            Console.WriteLine("Pos1Z: {0}", Pos1Z);

            Console.WriteLine("Pos2X: {0}", Pos2X);
            Console.WriteLine("Pos2Y: {0}", Pos2Y);
            Console.WriteLine("Pos2Z: {0}", Pos2Z);

            Console.WriteLine("Pos3X: {0}", Pos3X);
            Console.WriteLine("Pos3Y: {0}", Pos3Y);
            Console.WriteLine("Pos3Z: {0}", Pos3Z);

            Console.WriteLine("TextureID: {0} (0x{1})", TextureID, TextureID.ToString("x8"));
            Console.WriteLine("Orientation: {0} (0x{1})", Orientation, Orientation.ToString("x8"));
            Console.WriteLine("NetworkType: {0} (0x{1})", NetworkType, NetworkType.ToString("x8"));
            Console.WriteLine("WestConnection: {0} (0x{1})", WestConnection, WestConnection.ToString("x8"));
            Console.WriteLine("NorthConnection: {0} (0x{1})", NorthConnection, NorthConnection.ToString("x8"));
            Console.WriteLine("EastConnection: {0} (0x{1})", EastConnection, EastConnection.ToString("x8"));
            Console.WriteLine("SouthConnection: {0} (0x{1})", SouthConnection, SouthConnection.ToString("x8"));

            Console.WriteLine("MaxSizeX2: {0}", MaxSizeX2);
            Console.WriteLine("MaxSizeY2: {0}", MaxSizeY2);
            Console.WriteLine("MaxSizeZ2: {0}", MaxSizeZ2);
            Console.WriteLine("MinSizeX2: {0}", MinSizeX2);
            Console.WriteLine("MinSizeY2: {0}", MinSizeY2);
            Console.WriteLine("MinSizeZ2: {0}", MinSizeZ2);

            Console.WriteLine("ExtraBlocks: {0}", ExtraBlocks);
            Console.WriteLine("NetworkBlock1Count: {0}", NetworkBlockCount1);
            if (NetworkBlockCount1 > 0)
            {
                for (int i = 0; i < NetworkBlockCount1; i++)
                {
                    Console.WriteLine("==================");
                    NetworkBlocks1[i].Dump();
                }
            }
            Console.WriteLine("NetworkBlock2Count: {0}", NetworkBlockCount1);
            if (NetworkBlockCount2 > 0)
            {
                for (int i = 0; i < NetworkBlockCount2; i++)
                {
                    Console.WriteLine("==================");
                    NetworkBlocks2[i].Dump();
                }
            }
            Console.WriteLine("NetworkBlock3Count: {0}", NetworkBlockCount3);
            if (NetworkBlockCount3 > 0)
            {
                for (int i = 0; i < NetworkBlockCount3; i++)
                {
                    Console.WriteLine("==================");
                    NetworkBlocks3[i].Dump();
                }
            }
            Console.WriteLine("NetworkBlock4Count: {0}", NetworkBlockCount4);
            if (NetworkBlockCount4 > 0)
            {
                for (int i = 0; i < NetworkBlockCount4; i++)
                {
                    Console.WriteLine("==================");
                    NetworkBlocks4[i].Dump();
                }
            }
            Console.WriteLine("NetworkBlock1Count: {0}", NetworkBlockCount5);
            if (NetworkBlockCount5 > 0)
            {
                for (int i = 0; i < NetworkBlockCount5; i++)
                {
                    Console.WriteLine("==================");
                    NetworkBlocks5[i].Dump();
                }
            }

            Console.WriteLine("Pos4X: {0}", Pos4X);
            Console.WriteLine("Pos4Y: {0}", Pos4Y);
            Console.WriteLine("Pos4Z: {0}", Pos4Z);

            Console.WriteLine("UnknownFlag2: {0} (0x{1})", UnknownFlag2, UnknownFlag2.ToString("x8"));
            Console.WriteLine("UnknownFlag3: {0} (0x{1})", UnknownFlag3, UnknownFlag3.ToString("x8"));
            Console.WriteLine("UnknownFlag4: {0} (0x{1})", UnknownFlag4, UnknownFlag4.ToString("x8"));

            Console.WriteLine("Height1: {0}", Height1);
            Console.WriteLine("Height2: {0}", Height2);
            Console.WriteLine("Height3: {0}", Height3);
            Console.WriteLine("Height4: {0}", Height4);
            Console.WriteLine("Height5: {0}", Height5);

            Console.WriteLine("FileTypeID: {0}", FileTypeID.ToString("x8"));
            Console.WriteLine("UnknownUint: {0} (0x{1})", UnknownUint, UnknownUint.ToString("x8"));

        }
    }
}
