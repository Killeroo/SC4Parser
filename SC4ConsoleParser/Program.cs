using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC4Parser.Files;
using SC4Parser.DataStructures;
using SC4Parser.Types;

namespace SC4ConsoleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: handle error reading file
            //       handle file not found

            Operations.ListIndexEntries("Fulham.sc4");
            Console.ReadLine();
        }


    }
}
