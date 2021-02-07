using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Subfiles
{
    public class NetworkIndex
    {
        //List<NetworkBlock> NetworkBlocks = new List<NetworkBlock>();

        public void Parse(byte[] buffer, NetworkSubfile1 subfile1, NetworkSubfile2 subfile2)
        {
            uint internalOffset = 0;

            uint SubfileSize = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            uint CRC = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            uint MemoryAddress = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            ushort MajorVersion = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            uint CityTileCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            uint NetworkTileCount = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);

            Console.WriteLine(SubfileSize);
            Console.WriteLine(CRC.ToString("X"));
            Console.WriteLine(MemoryAddress.ToString("X"));
            Console.WriteLine(MajorVersion);
            Console.WriteLine(CityTileCount);
            Console.WriteLine(NetworkTileCount);
            Console.WriteLine();

            

            for (int index = 0; index < NetworkTileCount; index++)
            {
                uint TileNumber = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                uint MemoryAddressRef = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                uint SubfileTypeIDRef = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
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
                //Console.WriteLine("=========");

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

                ushort UnknownShort17 = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
                uint UnknownUint5 = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
                if (UnknownUint5 == 0)
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

                Console.WriteLine("#######################################");
                //Console.WriteLine(TileNumber.ToString("x8"));
                //Console.WriteLine(MemoryAddressRef.ToString("x8"));
                //Console.WriteLine(SubfileTypeIDRef.ToString("x8"));
                //Console.WriteLine(BlockCount);

                if (SubfileTypeIDRef.ToString("x") == Constants.NETWORK_SUBFILE_1_TYPE.ToLower())
                {
                    var tile = subfile1.FindTile(MemoryAddressRef);
                    Console.WriteLine("-> " + tile.NetworkType);
                }
                else if (SubfileTypeIDRef.ToString("x") == Constants.NETWORK_SUBFILE_2_TYPE.ToLower())
                {
                    var tile = subfile2.FindTile(MemoryAddressRef);
                    Console.WriteLine("-> " + tile.NetworkType);
                }
                else
                {
                    Console.WriteLine(SubfileTypeIDRef.ToString("x"));
                }
                    

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

    }
}
