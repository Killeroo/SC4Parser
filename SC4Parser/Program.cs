using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using SC4Parser.Structures;
using SC4Parser.Structures.Files.SubFiles;

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

namespace SC4Parser
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

            // Building parsing test stuff
            //FileInfo file = new FileInfo("buildings_decompressed");
            //BuildingSubFile f = new BuildingSubFile();
            //f.Parse(File.ReadAllBytes("buildings_decompressed"), (int)file.Length);
            //f.Dump();

            FileInfo file = new FileInfo("lot_decompressed");
            LotSubFile l = new LotSubFile();
            l.Parse(File.ReadAllBytes("lot_decompressed"), (int)file.Length);
            l.Dump();

            Console.ReadLine();
        }
    }
    
}
