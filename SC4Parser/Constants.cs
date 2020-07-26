using System;
using System.Collections.Generic;
using TestParser.Structures;

namespace TestParser
{
    class Constants
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

    }
}
