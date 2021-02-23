using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implementation of the Bridge Network Subfile. This file contains all bridge network tiles in a city.
    /// </summary>
    /// <seealso cref="SC4Parser.DataStructures.BridgeNetworkTile"/>
    /// <seealso cref="SC4Parser.Subfiles.NetworkSubfile1"/>
    public class BridgeNetworkSubfile
    {
        /// <summary>
        /// Contains all network tiles in the bridge network subfile
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.BridgeNetworkTile"/>
        public List<BridgeNetworkTile> NetworkTiles { get; private set; } = new List<BridgeNetworkTile>();

        /// <summary>
        /// Reads bridge network subfile from a byte array
        /// </summary>        
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="size">Size of the subfile</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, int size)
        {
            Logger.Log(LogLevel.Info, "Parsing Bridge Network Subfile...");

            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            // Loop through each byte in the file
            while (bytesToRead > 0)
            {
                // Get the current tile size
                // (each tile is stored one after another in the file with the size of tile at the beginning)
                uint recordSize = BitConverter.ToUInt32(buffer, (int)offset);

                // Copy tile data to it's own array
                byte[] tileBuffer = new byte[recordSize];
                Array.Copy(buffer, offset, tileBuffer, 0, (int)recordSize);

                // Parse the tile and add it to the list of tiles
                BridgeNetworkTile tile = new BridgeNetworkTile();
                tile.Parse(tileBuffer, 0);
                NetworkTiles.Add(tile);

                // Record how much we have read and how far we have gone and move on
                // (deep.. again...)
                offset += recordSize;
                bytesToRead -= recordSize;

                Logger.Log(LogLevel.Debug, $"Network tile read ({recordSize}) got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, $"Not all network tiles read from Bridge Network Subfile ({bytesToRead} left)");
            }

            Logger.Log(LogLevel.Info, "Bridge Network Subfile parsed");
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
