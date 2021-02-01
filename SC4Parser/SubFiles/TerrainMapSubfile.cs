using System;

using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implementation of TerrainMap Subfile, contains height information for each tile of a city.
    /// </summary>
    /// <remarks>
    /// Based off the implmentation here:
    /// https://github.com/sebamarynissen/sc4/blob/master/lib/terrain-map.js
    /// </remarks>
    /// <example>
    /// <c>
    /// // Simple usage
    /// // (Just assume the terrain map subfile has already been read, see SC4SaveGame.GetTerrainMapSubfile())
    ///
    /// // print out terrain map
    /// foreach (float x in terrainMapSubfile.Map)
    /// {
    ///     foreach (float y in terrainMapSubfile.Map[x])
    ///     {
    ///         Console.WriteLine(terrainMapSubfile.Map[x][y]);
    ///     }
    /// }
    /// </c>
    /// </example>
    public class TerrainMapSubfile
    {
        /// <summary>
        /// Major version of the subfile
        /// </summary>
        public ushort MajorVersion { get; private set; }
        /// <summary>
        /// X size of the city
        /// </summary>
        /// <remarks>
        /// Not included in actual file but borrowed from Region View Subfile for convience
        /// </remarks>
        public uint SizeX { get; private set; }
        /// <summary>
        /// Y size of the city
        /// </summary>
        /// <remarks>
        /// Not included in actual file but borrowed from Region View Subfile for convience
        /// </remarks>
        public uint SizeY { get; private set; }
        /// <summary>
        /// Actual terrain map, contains a height value for each tile in the sity
        /// </summary>
        /// <remarks>
        /// Stored in x and y of tiles
        /// </remarks>
        public float[][] Map { get; private set; }

        /// <summary>
        /// Reads the Terrain Map Subfile from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="xSize">Number of tiles on the X axis in the city</param>
        /// <param name="ySize">Number of tiles on the Y axis in the city</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, uint xSize, uint ySize)
        {
            SizeX = xSize + 1;
            SizeY = ySize + 1;

            Logger.Log(LogLevel.Info, "Parsing TerrainMap subfile...");

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

            Logger.Log(LogLevel.Info, "TerrainMap subfile parsed");

        }

        /// <summary>
        /// Prints out the contents of the Terrain Map Subfile
        /// </summary>
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
