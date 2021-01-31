using System;
using System.Collections.Generic;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Representation of a city's network tiles which are found in Network Subfile 1.
    /// </summary>
    /// <remarks>
    /// Network Subfile 1 contains all network tiles on the ground (so roads, rails etc).
    /// 
    /// Some unknown fields with no names have been skipped from tile implementation.
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Subfile_1_Structure
    /// </remarks>
    /// <see cref="SC4Parser.Subfiles.NetworkSubfile1"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkBlock"/>
    /// <seealso cref="SC4Parser.DataStructures.NetworkTile2"/>
    public class NetworkTile1
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
        /// Unknown flag 
        /// </summary>
        public byte UnknownFlag { get; private set; }
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
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeX2"/>
        public float MinSizeX1 { get; private set; }
        /// <summary>
        /// Maximum x size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeX2"/>
        public float MaxSizeX1 { get; private set; }
        /// <summary>
        /// Minimum y size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This to be a quarter of the network tile's size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeY2"/>
        public float MinSizeY1 { get; private set; }
        /// <summary>
        /// Maximum y size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeY2"/>
        public float MaxSizeY1 { get; private set; }
        /// <summary>
        /// Minimum z size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeZ2"/>
        public float MinSizeZ1 { get; private set; }
        /// <summary>
        /// Maximum z size of the Network tile (first set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeZ2"/>
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
        public byte NetworkType;

        /// <summary>
        /// Specifies if the network tile is connected on it's west side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte WestConnection;
        /// <summary>
        /// Specifies if the network tile is connected on it's north side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte NorthConnection;
        /// <summary>
        /// Specifies if the network tile is connected on it's east side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte EastConnection;
        /// <summary>
        /// Specifies if the network tile is connected on it's south side
        /// </summary>
        /// <remarks>
        /// 0x0 for false, 0x2 for true.
        /// </remarks>
        public byte SouthConnection;

        /// <summary>
        /// Minimum x size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeX1"/>
        public float MinSizeX2;
        /// <summary>
        /// Maximum x size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeX1"/>
        public float MaxSizeX2;
        /// <summary>
        /// Minimum y size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeY1"/>
        public float MinSizeY2;
        /// <summary>
        /// Maximum y size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeY1"/>
        public float MaxSizeY2;
        /// <summary>
        /// Minimum z size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MinSizeZ1"/>
        public float MinSizeZ2;
        /// <summary>
        /// Maximum z size of the Network tile (second set of sizes)
        /// </summary>
        /// <remarks>
        /// This seems to be a quarter of the network tile's actual size 
        /// </remarks>
        /// <seealso cref="SC4Parser.DataStructures.NetworkTile1.MaxSizeZ1"/>
        public float MaxSizeZ2;

        /// <summary>
        /// TypeGroupInstance (TGI) of network tile
        /// </summary>
        /// <remarks>
        /// Same as typeid, groupid and instanceid from this entry. Just included it for accessibility
        /// </remarks>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; private set; } = new TypeGroupInstance();

        /// <summary>
        /// Parses a network tile (from network subfile 1) from a byte array.
        /// </summary>
        /// <param name="buffer">buffer to parse from</param>
        /// <param name="offset">offset to start parsing at in the buffer</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        /// <seealso cref="SC4Parser.Subfiles.NetworkSubfile1"/>
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
            TGI                         = new TypeGroupInstance(TypeID, GroupID, InstanceID);
            UnknownFlag                 = Extensions.ReadByte(buffer, ref internalOffset);
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
            Console.WriteLine("UnknownFlag: {0}", UnknownFlag);

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
        }
    }
}
