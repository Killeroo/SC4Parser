using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using TestParser.Structures;
using TestParser.Structures.SubFiles;

// Key:
// ----
// File format: https://www.wiki.sc4devotion.com/index.php?title=DBPF
// Header format: https://www.wiki.sc4devotion.com/images/e/e8/DBPF_File_Format_v1.1.png
// Save game format: https://www.wiki.sc4devotion.com/index.php?title=Savegame
// Lot subsection: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile

// code examples
// --------
// https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/rgnReader.py#L156
//https://github.com/wouanagaine/SC4Mapper-2013/tree/master/Modules
//https://community.simtropolis.com/forums/topic/758258-modifying-sc4-savegames-it-is-possible/?page=2

// Tools:
// ------
// https://sc4devotion.com/forums/index.php?topic=7400
// https://sc4devotion.com/forums/index.php?topic=15455.0
// https://sc4devotion.com/csxlex/lex_filedesc.php?lotGET=731
//https://www.sc4devotion.com/csxlex/lex_filedesc.php?lotGET=2021
// https://sourceforge.net/p/ilive-reader/code/HEAD/tree/trunk/
//https://sc4devotion.com/forums/index.php?topic=10417.0

// Future
// (we want roads, builds and terrain at some point?)
//https://www.wiki.sc4devotion.com/index.php?title=Network_Subfiles#Network_Index_Subfile_.286A0F82B2.29

namespace TestParser
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string savePath = "Fulham.sc4";

            SC4SaveFile save = new SC4SaveFile(savePath);

            // Loading and decompression stuff:
            // save.FindIndexEntry("C9BD5D4A"); // Lot file
            //IndexEntry en = save.FindIndexEntry("A9BD882D"); // building file
            //save.LoadEntry(en.TGI);// new TypeGroupInstance("8A2482B9", "4A2482BB", "0"));

            // Building subfile work:
            Building buildingSubfile = new Building();
            buildingSubfile.Parse(File.ReadAllBytes("buildings_decompressed"));

            Console.ReadLine();
        }
    }


    class Utils
    {
        // Based on https://stackoverflow.com/a/250400
        // Could use DateTimeOffset.FromUnixTimeSeconds from .NET 4.6 > but thought it was new enough
        // That I would ensure a bit of backwards compatability
        public static DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            DateTime convertedDateTime = unixDateTime.AddSeconds(unixTimestamp);
            return convertedDateTime;
        }
    }

    class Logger
    {
        public static void Info(string message, ConsoleColor color = ConsoleColor.White)
        {
            // Preserve original foreground color
            ConsoleColor startingColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);

            Console.ForegroundColor = startingColor;
        }

        public static void Error(string message, bool newline = true)
        {
            ConsoleColor startingColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: {0}", message);

            Console.ForegroundColor = startingColor;
        }
    }

    
}
