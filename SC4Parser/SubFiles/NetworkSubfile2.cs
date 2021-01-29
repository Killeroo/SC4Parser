using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    public class NetworkSubfile2
    {
        public List<NetworkTile2> NetworkTiles = new List<NetworkTile2>();

        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                NetworkTile2 tile = new NetworkTile2();
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);
                tile.Parse(tileBuffer, offset);
                NetworkTiles.Add(tile);

                offset += recordSize;
                bytesToRead -= recordSize;

                Logger.Log(LogLevel.Debug, $"Network tile read ({size}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Network Subfile 2 ({bytesToRead} left)");
            }
        }

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
