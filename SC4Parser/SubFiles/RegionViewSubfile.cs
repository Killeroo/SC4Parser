using System;

using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Region View Subfile (partial implementation). Contains basic city information from a region point of view.
    /// </summary>
    /// <remarks>
    /// Only a partial implementation and will not contain all values from the save game
    /// 
    /// Based off spec from here: https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
    /// </remarks>
    /// <example>
    /// <c>
    /// // Simple usage
    /// // (Just assume the region view subfile has already been read, see SC4SaveGame.GetRegionViewSubfile())
    ///
    /// // Access some data
    /// Console.WriteLine("city location x={0} y={1}",
    ///     regionViewSubfile.TileXLocation,
    ///     regionViewSubfile.TileYLocation);
    /// </c>
    /// </example>
    public class RegionViewSubfile
    {
        /// <summary>
        /// Major version of the subfile
        /// </summary>
        /// <remarks>
        /// You can see the different versions here: https://www.wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
        /// This implementation is based around the SimCity 4 Rush Hour/Deluxe version of the game (1.13)
        /// </remarks>
        public ushort MajorVersion { get; protected set; }
        /// <summary>
        /// Minor version of the subfile
        /// </summary>
        /// <remarks>W
        /// You can see the different versions here: https://www.wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
        /// This implementation is based around the SimCity 4 Rush Hour/Deluxe version of the game (1.13)
        /// </remarks>
        public ushort MinorVersion { get; private set; }
        /// <summary>
        /// X location of the city in the region view
        /// </summary>
        public uint TileXLocation { get; private set; }
        /// <summary>
        /// Z location of the city in the region view
        /// </summary>
        public uint TileYLocation { get; private set; }
        /// <summary>
        /// X size of the city
        /// </summary>
        /// <remarks>
        /// Multiplied by 64 to get the number of the tiles in the city
        /// </remarks>
        public uint CitySizeX { get; private set; }
        /// <summary>
        /// Y size of the city
        /// </summary>
        /// <remarks>
        /// Multiplied by 64 to get the number of the tiles in the city
        /// </remarks>
        public uint CitySizeY { get; private set; }
        /// <summary>
        /// Residential population of city
        /// </summary>
        public uint ResidentialPopulation { get; private set; }
        /// <summary>
        /// Commercial population of city
        /// </summary>
        public uint CommercialPopulation { get; private set; }
        /// <summary>
        /// Industrial population of city
        /// </summary>
        public uint IndustrialPopulation { get; private set; }

        /// <summary>
        /// Parses Region View Subfile from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer)
        {
            MajorVersion = BitConverter.ToUInt16(buffer, 0);
            MinorVersion = BitConverter.ToUInt16(buffer, 2);

            if (MinorVersion < 13)
                Logger.Log(LogLevel.Warning, "Parsing a pre Rush Hour save game, some of the values in the RegionView Subfile might be garbled");

            TileXLocation = BitConverter.ToUInt32(buffer, 4);
            TileYLocation = BitConverter.ToUInt32(buffer, 8);
            CitySizeX = BitConverter.ToUInt32(buffer, 12) * 64;
            CitySizeY = BitConverter.ToUInt32(buffer, 16) * 64;
            ResidentialPopulation = BitConverter.ToUInt32(buffer, 20);
            CommercialPopulation = BitConverter.ToUInt32(buffer, 24);
            IndustrialPopulation = BitConverter.ToUInt32(buffer, 28);
        }

        /// <summary>
        /// Prints out the contents of the Region View Subfile
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Tile X Location: {0}", TileXLocation);
            Console.WriteLine("Tile Y Location: {0}", TileYLocation);
            Console.WriteLine("City Size X: {0}", CitySizeX);
            Console.WriteLine("City Size Y: {0}", CitySizeY);
            Console.WriteLine("Residential Population: {0}", ResidentialPopulation);
            Console.WriteLine("Commercial Population: {0}", CommercialPopulation);
            Console.WriteLine("Industrial Population: {0}", IndustrialPopulation);
        }
    }
}
