using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    public class NetworkSubfile1
    {
        public List<NetworkTile1> NetworkTiles = new List<NetworkTile1>();

        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                NetworkTile1 tile = new NetworkTile1();
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);
                tile.Parse(tileBuffer, 0);
                NetworkTiles.Add(tile);

                offset += recordSize;
                bytesToRead -= recordSize;
                
                Logger.Log(LogLevel.Debug, $"Network tile read ({size}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Network Subfile 1 ({bytesToRead} left)");
            }
        }

        public void Dump()
        { 

        }
    }
}
