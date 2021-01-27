using System;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    public class NetworkSubfile1
    {
        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                NetworkTile tile = new NetworkTile();
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);
                tile.Parse(tileBuffer, offset);
                tile.Dump();
                Console.ReadLine();

                offset += recordSize;
                bytesToRead -= recordSize;
                
                Logger.Log(LogLevel.Debug, $"Network tile read ({size}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Network Subfile 1 ({bytesToRead} left)");
            }
        }
    }
}
