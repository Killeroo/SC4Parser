using SC4Parser.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implmentation of TerrainMap Subfile, contains height information for each tile of a city.
    /// Based off the implmentation here:
    /// https://github.com/sebamarynissen/sc4/blob/master/lib/terrain-map.js
    /// </summary>
    public class TerrainMapSubfile
    {
        public ushort MajorVersion;
        public uint SizeX;
        public uint SizeY;
        public float[][] Map; // Stored in x and y of tiles

        public void Parse(byte[] buffer, uint xSize, uint ySize)
        {
            SizeX = xSize + 1;
            SizeY = ySize + 1;

            MajorVersion = BitConverter.ToUInt16(buffer, 0);

            // Loop through rest of file and save to out array
            int offset = 2;
            Map = new float[SizeX][];
            for (int x = 0; x < SizeX; x++)
            {
                Map[x] = new float[SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    Map[x][y] = BitConverter.ToSingle(buffer, offset);
                    offset += 4;
                    Logger.Log(LogLevel.Debug, "Terrain segment [{0},{1}] read, offset {2} got {3}/{4} bytes left",
                        x,
                        y,
                        offset,
                        offset,
                        buffer.Length);
                }
            }

        }

        public void Dump()
        {
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Terrain Map Data:");
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Console.Write(Map[x][y] + " ");
                }
                Console.WriteLine();
            }

        }

    }
}
