using System;
using System.IO;
using System.Collections.Generic;

using SC4Parser.Types;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// 
    /// </summary>
    public class NetworkBlock
    {
        public float X;
        public float Y;
        public float Z;
        public float Unknown1;
        public float Unknown2;
        public uint Unknown3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
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
        /// 
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
    /// 
    /// </summary>
    public class NetworkTile2
    {
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort UnknownVersion1;
        public ushort UnknownVersion2;
        public byte AppearanceFlag;
        public byte MinTractX;
        public byte MinTractZ;
        public byte MaxTractX;
        public byte MaxTractZ;
        public ushort TractSizeX;
        public ushort TractSizeZ;
        public uint SaveGamePropertyCount;
        public List<SaveGameProperty> SaveGamePropertyEntries = new List<SaveGameProperty>();
        public uint GroupID;
        public uint TypeID;
        public uint InstanceID;
        public byte UnknownFlag1;
        public uint TextureID;
        public byte Orientation;

        public float MinSizeX1;
        public float MaxSizeX1;
        public float MinSizeY1;
        public float MaxSizeY1;
        public float MinSizeZ1;
        public float MaxSizeZ1;

        public float Pos1X;
        public float Pos1Y;
        public float Pos1Z;

        public float Pos2X;
        public float Pos2Y;
        public float Pos2Z;

        public float Pos3X;
        public float Pos3Y;
        public float Pos3Z;

        public byte NetworkType;
        public byte WestConnection;
        public byte NorthConnection;
        public byte EastConnection;
        public byte SouthConnection;

        public float MinSizeX2;
        public float MaxSizeX2;
        public float MinSizeY2;
        public float MaxSizeY2;
        public float MinSizeZ2;
        public float MaxSizeZ2;

        public uint ExtraBlocks;
        public uint NetworkBlockCount1;
        public uint NetworkBlockCount2;
        public uint NetworkBlockCount3;
        public uint NetworkBlockCount4;
        public uint NetworkBlockCount5;
        public NetworkBlock[] NetworkBlocks1;
        public NetworkBlock[] NetworkBlocks2;
        public NetworkBlock[] NetworkBlocks3;
        public NetworkBlock[] NetworkBlocks4;
        public NetworkBlock[] NetworkBlocks5;

        public float Pos4X;
        public float Pos4Y;
        public float Pos4Z;

        public byte UnknownFlag2;
        public byte UnknownFlag3;
        public byte UnknownFlag4;

        public float Height1;
        public float Height2;
        public float Height3;
        public float Height4;
        public float Height5;

        public uint FileTypeID;
        public uint UnknownUint;

        public TypeGroupInstance TGI = new TypeGroupInstance();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public void Parse(byte[] buffer, uint offset)
        {
            uint internalOffset = offset;

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
        /// 
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
