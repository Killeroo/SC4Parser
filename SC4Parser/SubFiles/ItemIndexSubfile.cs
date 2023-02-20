using System;
using System.Collections.Generic;
using System.IO;

namespace SC4Parser
{
    public class ItemIndexSubfile
    {
        public struct Item
        {
            public uint MemoryAddress { get; internal set; }
            public uint SubfileTypeId { get; internal set; }
        }

        public uint Size { get; private set; }
        public uint CRC { get; private set; }
        public uint MemoryAddress { get; private set; }
        public ushort MajorVersion { get; private set; }
        public float CityWidthMeters { get; private set; }
        public float CityDepthMeters { get; private set; }
        public uint CityWidthTracts { get; private set; }
        public uint CityDepthTracts { get; private set; }
        public uint CityWidthTiles { get; private set; }
        public uint CityDepthTiles { get; private set; }
        public uint CellColumnCount { get; private set; }
        public uint CellRowCount { get; private set; }
        public List<Item>[][] Items { get; private set; }

        public void Parse(byte[] buffer, int size)
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                Size = reader.ReadUInt32();
                CRC = reader.ReadUInt32();
                MemoryAddress = reader.ReadUInt32();
                MajorVersion = reader.ReadUInt16();
                CityWidthMeters = reader.ReadSingle();
                CityDepthMeters = reader.ReadSingle();
                CityWidthTracts = reader.ReadUInt32();
                CityDepthTracts = reader.ReadUInt32();
                CityWidthTiles = reader.ReadUInt32();
                CityDepthTiles = reader.ReadUInt32();
                CellColumnCount = reader.ReadUInt32();

                Items = new List<Item>[CellColumnCount][];
                for (int x = 0; x < CellColumnCount; x++)
                {
                    CellRowCount = reader.ReadUInt32();
                    Items[x] = new List<Item>[CellRowCount];
                    for (int y = 0; y < CellRowCount; y++)
                    {
                        uint cellItemCount = reader.ReadUInt32();

                        Items[x][y] = new List<Item>();
                        if (cellItemCount > 0)
                        {
                            for (int i = 0; i < cellItemCount; i++)
                            {
                                Items[x][y].Add(new Item()
                                {
                                    MemoryAddress = reader.ReadUInt32(),
                                    SubfileTypeId = reader.ReadUInt32()
                                });
                            }
                        }

                    }
                }
            }
        }

        public void Dump()
        {
            Console.WriteLine("Size: {0} bytes", Size);
            Console.WriteLine("CRC: 0x{0}", CRC);
            Console.WriteLine("Memory Address: 0x{0}", MemoryAddress.ToString("X8"));
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("City Width (Meters): {0}", CityWidthMeters);
            Console.WriteLine("City Depth (Meters): {0}", CityDepthMeters);
            Console.WriteLine("City Width (Tracts): {0}", CityWidthTracts);
            Console.WriteLine("City Depth (Tracts): {0}", CityDepthTracts);
            Console.WriteLine("City Width (Tiles): {0}", CityWidthTiles);
            Console.WriteLine("City Depth (Tiles): {0}", CityDepthTiles);
            Console.WriteLine("Cell Column Count: {0}", CellColumnCount);
            Console.WriteLine("Cell Row Count: {0}", CellRowCount);
            for (int x = 0; x < CellColumnCount; x++)
            {
                for (int y = 0; y < CellRowCount; y++)
                {
                    foreach (Item i in Items[x][y])
                    {
                        Console.WriteLine("[{2},{3}] MemoryAddress=0x{0} Subfile=0x{1}", 
                            i.MemoryAddress.ToString("X8"), 
                            i.SubfileTypeId.ToString("X8"),
                            x,
                            y);
                    }
                }
            }
        }

    }
}
