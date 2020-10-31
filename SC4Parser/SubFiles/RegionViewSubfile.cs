using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Subfiles
{
    //https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
    class RegionViewSubfile
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
        }

        public void Dump()
        {

        }
    }
}
