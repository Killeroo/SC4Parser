using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    // Partially implemented
    //https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
    public class RegionViewSubfile
    {
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint TileXLocation;
        public uint TileYLocation;
        public uint CitySizeX;
        public uint CitySizeY;
        public uint ResidentialPopulation;
        public uint CommercialPopulation;
        public uint IndustrialPopulation;
        public byte MayorRating;

        public void Parse(byte[] buffer)
        {
            MajorVersion = BitConverter.ToUInt16(buffer, 0);
            MinorVersion = BitConverter.ToUInt16(buffer, 2);

            if (MinorVersion < 13)
                Logger.Log(LogLevel.Warning, "Parsing a pre Rush Hour save game, some of the values in the RegionView Subfile might be garbled");

            TileXLocation = BitConverter.ToUInt32(buffer, 4);
            TileYLocation = BitConverter.ToUInt32(buffer, 8);
            CitySizeX = BitConverter.ToUInt32(buffer, 12);
            CitySizeY = BitConverter.ToUInt32(buffer, 16);
            ResidentialPopulation = BitConverter.ToUInt32(buffer, 20);
            CommercialPopulation = BitConverter.ToUInt32(buffer, 24);
            IndustrialPopulation = BitConverter.ToUInt32(buffer, 28);
        }

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
