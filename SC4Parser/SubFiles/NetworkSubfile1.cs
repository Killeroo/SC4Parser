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
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            // Loop through each byte in the subfile
            while (bytesToRead > 0)
            {
                // Work out the current tile size 
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                // Copy tile data out into it's own array
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);

                // Parse and add to list
                NetworkTile1 tile = new NetworkTile1();
                tile.Parse(tileBuffer, 0);
                NetworkTiles.Add(tile);

                // Record how much we have read and how far we have gone and move on
                // (deep)
                offset += recordSize;
                bytesToRead -= recordSize;
                
                Logger.Log(LogLevel.Debug, $"Network tile read ({size}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Network Subfile 1 ({bytesToRead} left)");
            }
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
