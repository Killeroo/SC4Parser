using System;
using System.Collections.Generic;

namespace SC4Parser 
{
    /// <summary>
    /// Stores common values and identifiers used in SimCity 4 save gamesa
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// TypeGroupInstance (TGI) ID for the Database Directory File (DBDF)
        /// </summary>
        /// <see cref="SC4Parser.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.DatabaseDirectoryFile"/>
        public static readonly TypeGroupInstance DATABASE_DIRECTORY_FILE_TGI = new TypeGroupInstance("E86B1EEF", "E86B1EEF", "286B1F03");
        /// <summary>
        /// TypeGroupInstance (TGI) ID for the Region View Subfile
        /// </summary>
        /// <see cref="SC4Parser.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.RegionViewSubfile"/>
        public static readonly TypeGroupInstance REGION_VIEW_SUBFILE_TGI = new TypeGroupInstance("CA027EDB", "CA027EE1", "00000000");
        /// <summary>
        /// TypeGroupInstance (TGI) ID for the Terrain Map Subfile
        /// </summary>
        /// <see cref="SC4Parser.TypeGroupInstance"/>
        /// <seealso cref="SC4Parser.TerrainMapSubfile"/>
        public static readonly TypeGroupInstance TERRAIN_MAP_SUBFILE_TGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");

        /// <summary>
        /// Type ID of Lot Subfile
        /// </summary>
        /// <seealso cref="SC4Parser.LotSubfile"/>
        public static readonly string LOT_SUBFILE_TYPE = "C9BD5D4A";
        /// <summary>
        /// Type ID of Building Subfile
        /// </summary>
        /// <seealso cref="SC4Parser.BuildingSubfile"/>
        public static readonly string BUILDING_SUBFILE_TYPE = "A9BD882D";
        /// <summary>
        /// Type ID of Network Index Subfile
        /// </summary>
        /// <seealso cref="SC4Parser.NetworkIndex"/>
        public static readonly string NETWORK_INDEX_SUBFILE_TYPE = "6A0F82B2";
        /// <summary>
        /// Type ID of Network Subfile 1
        /// </summary>
        /// <seealso cref="SC4Parser.NetworkSubfile1"/>
        public static readonly string NETWORK_SUBFILE_1_TYPE = "C9C05C6E";
        /// <summary>
        /// Type ID of Network Subfile 2
        /// </summary>
        /// <seealso cref="SC4Parser.NetworkSubfile2"/>
        public static readonly string NETWORK_SUBFILE_2_TYPE = "CA16374F";
        /// <summary>
        /// Type ID of the bridge network subfile
        /// </summary>
        /// <seealso cref="SC4Parser.BridgeNetworkSubfile"/>
        public static readonly string BRIDGE_NETWORK_SUBFILE_TYPE = "49CC1BCD";
        /// <summary>
        /// Type ID of the tunnel network subfile
        /// </summary>
        public static readonly string TUNNEL_NETWORK_SUBFILE_TYPE = "8A4BD52B";
        public static readonly string ITEM_INDEX_SUBFILE_TYPE = "098F964D";

        /// <summary>
        /// Orientations used by SimCity 4 save game items as strings
        /// </summary>
        /// <remarks>
        /// Following is a full list of all different orientations:
        ///     0x00 = North
        ///     0x01 = East
        ///     0x02 = South
        ///     0x03 = West
        ///     0x80 = North, mirrored
        ///     0x81 = East, mirrored
        ///     0x82 = south, mirrored
        ///     0x83 = West, mirrored
        /// </remarks>
        /// <seealso cref="SC4Parser.Building.Orientation"/>
        /// <seealso cref="SC4Parser.Lot.Orientation"/>
        public static string[] ORIENTATION_STRINGS = new string[]
        {
            "North",
            "East",
            "South",
            "West"
        };

        /// <summary>
        /// Different types used in Save Game Propertie's (SIGPROPs) data
        /// </summary>
        /// <seealso cref="SC4Parser.SaveGameProperty"/>
        /// <seealso cref="SC4Parser.SaveGameProperty.Data"/>
        public static Dictionary<byte, object> SIGPROP_DATATYPE_TYPES = new Dictionary<byte, object>
        {
            {0x01, new byte()},
            {0x02, new UInt16()},
            {0x03, new UInt32()},
            {0x07, new Int32()},
            {0x08, new Int64()},
            {0x09, new float()},
            {0x0B, new Boolean()},
            {0x0C, new char()}
        };
        /// <summary>
        /// Different types used in Save Game Propertie's (SIGPROPs) as strings
        /// </summary>
        public static Dictionary<byte, string> SIGPROP_DATATYPE_TYPE_STRINGS = new Dictionary<byte, string>
        {
            {0x01, "UInt8"},
            {0x02, "UInt16"},
            {0x03, "UInt32"},
            {0x07, "Int32"},
            {0x08, "Int64"},
            {0x09, "Float"},
            {0x0B, "Boolean"},
            {0x0C, "string//char"}
        };
        
        /// <summary>
        /// Lot zone types as strings
        /// </summary>
        public static Dictionary<byte, string> LOT_ZONE_TYPE_STRINGS = new Dictionary<byte, string>
        {
            {0x00, "Unknown"},
            {0x01, "Residential - Low"},
            {0x02, "Residential - Medium"},
            {0x03, "Residential - High"},
            {0x04, "Commercial - Low"},
            {0x05, "Commercial - Medium"},
            {0x06, "Commercial - High"},
            {0x07, "Industrial - Low"},
            {0x08, "Industrial - Medium"},
            {0x09, "Industrial - High"},
            {0x0A, "Military"},
            {0x0B, "Airport"},
            {0x0C, "Seaport"},
            {0x0D, "Spaceport"},
            {0x0E, "Plopped building"},
            {0x0F, "Plopped building"},
        };
        /// <summary>
        /// Lot wealth types as strings
        /// </summary>
        public static Dictionary<byte, string> LOT_ZONE_WEALTH_STRINGS = new Dictionary<byte, string>
        {
            {0x00, "None"},
            {0x01, @"$"},
            {0x02, @"$$"},
            {0x03, @"$$$"}
        };

        /// <summary>
        /// Different network types as strings
        /// </summary>
        public static Dictionary<byte, string> NETWORK_TYPE_STRINGS = new Dictionary<byte, string>
        {
            {0x00, "Road"},
            {0x01, "Rail"},
            {0x03, "Street"},
            {0x06, "Avenue"},
            {0x07, "Subway"},
            {0x0A, "One Way Road"},
        };

        /// <summary>
        /// Different occupancy groups that appear in the RegionView Subfile. Ordered by the index that they appear at.
        /// </summary>
        public static Dictionary<int, string> OCCUPANCY_GROUPS = new Dictionary<int, string>()
        {
            {0, "Max Residential Population"},
            {1, "R$ Max Population"},
            {2, "R$ Current Population"},
            {3, "R$$ Max Population"},
            {4, "R$$ Current Population"},
            {5, "R$$$ Max Population"},
            {6, "R$$$ Current Population"},
            {7, "R$ Jobs"},
            {8, "R$$ Jobs"},
            {9, "R$$$ Jobs"},
            {10, "R$ Workforce EQ1"},
            {11, "R$ Workforce EQ2"},
            {12, "R$ Workforce EQ3"},
            {13, "R$ Workforce EQ4"},
            {14, "R$$ Workforce EQ1"},
            {15, "R$$ Workforce EQ2"},
            {16, "R$$ Workforce EQ3"},
            {17, "R$$ Workforce EQ4"},
            {18, "R$$$ Workforce EQ1"},
            {19, "R$$$ Workforce EQ2"},
            {20, "R$$$ Workforce EQ3"},
            {21, "R$$$ Workforce EQ4"},
            {22, "CS$ Max Jobs"},
            {23, "CS$ Cur Jobs"},
            {24, "CS$$ Max Jobs"},
            {25, "CS$$ Cur Jobs"},
            {26, "CS$$$ Max Jobs"},
            {27, "CS$$$ Cur Jobs"},
            {28, "CO$$ Max Jobs"},
            {29, "CO$$ Cur Jobs"},
            {30, "CO$$$ Max Jobs"},
            {31, "CO$$$ Cur Jobs"},
            {32, "IA Max Jobs"},
            {33, "IA Cur Jobs"},
            {34, "ID Max Jobs"},
            {35, "ID Cur Jobs"},
            {36, "IM Max Jobs"},
            {37, "IM Cur Jobs"},
            {38, "IH Max Jobs"},
            {39, "IH Cur Jobs"}
        };

        /// <summary>
        /// Low density residential zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_LOW = 0x01;
        /// <summary>
        /// Medium density residential zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM = 0x02;
        /// <summary>
        /// High density residential zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_HIGH = 0x03;
        /// <summary>
        /// Low density commercial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_COMMERCIAL_LOW = 0x04;
        /// <summary>
        /// Medium density commercial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_COMMERCIAL_MEDIUM = 0x05;
        /// <summary>
        /// High density commercial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_COMMERCIAL_HIGH = 0x06;
        /// <summary>
        /// Low density industrial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_LOW = 0x07;
        /// <summary>
        /// Medium density industrial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM = 0x08;
        /// <summary>
        /// High density industrial zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_HIGH = 0x09;
        /// <summary>
        /// Military zone type 
        /// </summary>
        public const byte LOT_ZONE_TYPE_MILITARY = 0x0A;
        /// <summary>
        /// Airport zone type
        /// </summary>
        public const byte LOT_ZONE_TYPE_AIRPORT = 0x0B;
        /// <summary>
        /// Seaport zone type
        /// </summary>
        public const byte LOT_ZONE_TYPE_SEAPORT = 0x0C;
        /// <summary>
        /// Spaceport zone type
        /// </summary>
        public const byte LOT_ZONE_TYPE_SPACEPORT = 0x0D;
        /// <summary>
        /// Plopped building zone type
        /// </summary>
        public const byte LOT_ZONE_TYPE_PLOPPED_BUILDING = 0x0F;
        /// <summary>
        /// Plopped building zone type
        /// </summary>
        public const byte LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT = 0x0E;

        /// <summary>
        /// No zone wealth value
        /// </summary>
        public const byte LOT_WEALTH_NONE = 0x00;
        /// <summary>
        /// Low zone wealth value
        /// </summary>
        public const byte LOT_WEALTH_LOW = 0x01;
        /// <summary>
        /// Medium zone wealth value
        /// </summary>
        public const byte LOT_WEALTH_MEDIUM = 0x02;
        /// <summary>
        /// High zone wealth value
        /// </summary>
        public const byte LOT_WEALTH_HIGH = 0x03;
        
        /// <summary>
        /// North orientation
        /// </summary>
        public const byte ORIENTATION_NORTH = 0x00;
        /// <summary>
        /// East orientation
        /// </summary>
        public const byte ORIENTATION_EAST = 0x01;
        /// <summary>
        /// South orientation
        /// </summary>
        public const byte ORIENTATION_SOUTH = 0x02;
        /// <summary>
        /// West orientation
        /// </summary>
        public const byte ORIENTATION_WEST = 0x03;

        /// <summary>
        /// Number of grid tiles in a small sized city
        /// </summary>
        public const int SMALL_CITY_TILE_COUNT = 64;
        /// <summary>
        /// Number of grid tiles in a medium sized city
        /// </summary>
        public const int MEDIUM_CITY_TILE_COUNT = 128;
        /// <summary>
        /// Number of grid tiles in a large city
        /// </summary>
        public const int LARGE_CITY_TILE_COUNT = 128;

        /// <summary>
        /// City mode that represents if a city is in God Mode
        /// </summary>
        /// <seealso cref="SC4Parser.RegionViewSubfile.ModeFlag"/>
        public const int GOD_MODE_FLAG = 0;
        /// <summary>
        /// City mode that represents if a city is in Mayor Mode
        /// </summary>
        /// <seealso cref="SC4Parser.RegionViewSubfile.ModeFlag"/>
        public const int MAYOR_MODE_FLAG = 1;
    }
}
