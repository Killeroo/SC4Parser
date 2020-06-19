using System;
using System.IO;
using System.Text;

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

            try
            {
                // Open file as a file stream
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[100];


                    // Read header

                    stream.Read(buffer, 0, 4);
                    Console.WriteLine(Encoding.ASCII.GetString(buffer));

                    buffer = new byte[100];

                    stream.Read(buffer, 4, 4);
                    Console.WriteLine(BitConverter.ToUInt32(buffer, 0));

                    buffer = new byte[100];

                    stream.Read(buffer, 8, 4);
                    Console.WriteLine(BitConverter.ToUInt32(buffer, 0));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hit exception: " + ex.GetType().ToString());
            }
        }
    }
}
