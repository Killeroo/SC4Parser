using System;
using System.Text;

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
        /// Mayor rating of city, in bars as seen on region view (12 max)
        /// </summary>
        public byte MayorRating { get; private set; }
        /// <summary>
        /// City star count (as seen from region view), (0=1, 1=2, 2=3)
        /// </summary>
        public byte StarCount { get; private set; }
        /// <summary>
        /// Indicates if the city is a tutorial city. 1 for tutorial map.
        /// </summary>
        public byte TutorialFlag { get; private set; }
        /// <summary>
        /// City GUID
        /// </summary>
        public uint CityGuid { get; private set; }
        /// <summary>
        /// Mode city is in (1 = Mayor mode, 0 = God mode)
        /// </summary>
        public byte ModeFlag { get; private set; }
        /// <summary>
        /// Length of city name string
        /// </summary>
        public uint CityNameLength { get; private set; }
        /// <summary>
        /// City's name
        /// </summary>
        public string CityName { get; private set; }
        /// <summary>
        /// Length of former city name
        /// </summary>
        public uint FormerCityNameLength { get; private set; }
        /// <summary>
        /// Cities former name 
        /// </summary>
        public string FormerCityName { get; private set; }
        /// <summary>
        /// Length of mayor name string
        /// </summary>
        public uint MayorNameLength { get; private set; }
        /// <summary>
        /// City's mayor
        /// </summary>
        public string MayorName { get; private set; }
        /// <summary>
        /// City description string length
        /// </summary>
        public uint InternalDescriptionLength { get; private set; }
        /// <summary>
        /// City description
        /// </summary>
        public string InternalDescription { get; private set; }

        /// <summary>
        /// Parses Region View Subfile from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer)
        {
            uint internalOffset = 0;

            Logger.Log(LogLevel.Info, "Parsing RegionView subfile...");

            MajorVersion = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);
            MinorVersion = BitConverter.ToUInt16(Extensions.ReadBytes(buffer, 2, ref internalOffset), 0);

            if (MinorVersion < 13)
                Logger.Log(LogLevel.Warning, "Parsing a pre Rush Hour save game, some of the values in the RegionView Subfile might be garbled");

            TileXLocation = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            TileYLocation = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CitySizeX = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0) * 64;
            CitySizeY = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0) * 64;
            ResidentialPopulation = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CommercialPopulation = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); ;
            IndustrialPopulation = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0); ;

            if (MinorVersion > 9)
                internalOffset += 4;

            if (MinorVersion > 10)
                MayorRating = Extensions.ReadByte(buffer, ref internalOffset);

            StarCount = Extensions.ReadByte(buffer, ref internalOffset);
            TutorialFlag = Extensions.ReadByte(buffer, ref internalOffset);
            CityGuid = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            internalOffset += 4 * 5; // Skip over unknown fields
            ModeFlag = Extensions.ReadByte(buffer, ref internalOffset);
            CityNameLength = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            CityName = Encoding.ASCII.GetString(Extensions.ReadBytes(buffer, CityNameLength, ref internalOffset));
            FormerCityNameLength = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            FormerCityName = Encoding.ASCII.GetString(Extensions.ReadBytes(buffer, FormerCityNameLength, ref internalOffset));
            MayorNameLength = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            MayorName = Encoding.ASCII.GetString(Extensions.ReadBytes(buffer, MayorNameLength, ref internalOffset));
            InternalDescriptionLength = BitConverter.ToUInt32(Extensions.ReadBytes(buffer, 4, ref internalOffset), 0);
            InternalDescription = Encoding.ASCII.GetString(Extensions.ReadBytes(buffer, InternalDescriptionLength, ref internalOffset));

            Logger.Log(LogLevel.Info, "RegionView subfile parsed");
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
            Console.WriteLine("Mayor Rating: {0}", MayorRating);
            Console.WriteLine("Star Count: {0}", StarCount);
            Console.WriteLine("Tutorial Flag: {0}", TutorialFlag);
            Console.WriteLine("City GUID: {0}", CityGuid);
            Console.WriteLine("Mode flag: {0} [{1}]", ModeFlag == Constants.GOD_MODE_FLAG ? "God Mode" : "Mayor Mode", ModeFlag);
            Console.WriteLine("City Name Length: {0}", CityNameLength);
            Console.WriteLine("City Name: {0}", CityName);
            Console.WriteLine("Former City Name Length: {0}", FormerCityNameLength);
            Console.WriteLine("Former City Name: {0}", FormerCityName);
            Console.WriteLine("Mayor Name Length: {0}", MayorNameLength);
            Console.WriteLine("Mayor Name: {0}", MayorName);
            Console.WriteLine("Internal Description Length: {0}", InternalDescriptionLength);
            Console.WriteLine("Internal Description: {0}", InternalDescription);
        }
    }
}
