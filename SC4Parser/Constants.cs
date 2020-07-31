using System;
using System.Collections.Generic;
using SC4Parser.Structures;

namespace SC4Parser // SC4.constants
{
    public class Constants
    {
        public static readonly TypeGroupInstance DATABASE_DIRECTORY_FILE_TGI = new TypeGroupInstance("E86B1EEF", "E86B1EEF", "286B1F03");

        public static readonly byte ORIENTATION_NORTH = 0x01;
        public static readonly byte ORIENTATION_EAST = 0x02;
        public static readonly byte ORIENTATION_SOUTH = 0x03;
        public static readonly byte ORIENTATION_WEST = 0x04;

        public static string[] ORIENTATIONS = new string[]
        {
            "North",
            "East",
            "South",
            "West"
        };

        // SIGPROP DataType types
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

        // Lot zone types
        public static Dictionary<byte, string> LOT_ZONE_TYPES = new Dictionary<byte, string>
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
            {0x0F, "Plopped building"},
        };
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_LOW = 0x01;
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM = 0x02;
        public const byte LOT_ZONE_TYPE_RESIDENTIAL_HIGH = 0x03;
        public const byte LOT_ZONE_TYPE_COMMERCIAL_LOW = 0x04;
        public const byte LOT_ZONE_TYPE_COMMERCIAL_MEDIUM = 0x05;
        public const byte LOT_ZONE_TYPE_COMMERCIAL_HIGH = 0x06;
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_LOW = 0x07;
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM = 0x08;
        public const byte LOT_ZONE_TYPE_INDUSTRIAL_HIGH = 0x09;
        public const byte LOT_ZONE_TYPE_PLOPPED_BUILDING = 0x0F;

        // Lot zone wealths
        public static Dictionary<byte, string> LOT_ZONE_WEALTHS = new Dictionary<byte, string>
        {
            {0x00, "None"},
            {0x01, @"$"},
            {0x02, @"$$"},
            {0x03, @"$$$"}
        };
        public const byte LOT_WEALTH_NONE = 0x00;
        public const byte LOT_WEALTH_LOW = 0x01;
        public const byte LOT_WEALTH_MEDIUM = 0x02;
        public const byte LOT_WEALTH_HIGH = 0x03;

        // Lot Orientation
        public static Dictionary<byte, string> LOT_ORIENTATIONS = new Dictionary<byte, string>
        {
            {0x00, "North"},

            { 0x02, "South"},

        };
    }
}
