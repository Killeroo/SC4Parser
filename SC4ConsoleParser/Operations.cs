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
    class Operations
    {
        public static void ListIndexEntries(string path)
        {
            SC4SaveFile save = new SC4SaveFile(path);

            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}",
                "Entry".PadRight(6),
                "Location".PadRight(15),
                "File Size".PadRight(15),
                "Compressed ",
                "Type".PadRight(14),
                "Group".PadRight(14),
                "Instance".PadRight(14));

            Console.WriteLine(new string('-', 85));

            int entryCount = 1;
            foreach (IndexEntry entry in save.IndexEntries)
            {
                bool compressed = save.IsIndexEntryCompressed(entry);
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}",
                    entryCount.ToString().PadRight(6),
                    ("0x" + entry.FileLocation.ToString("X").PadLeft(8, '0')).PadRight(15),
                    (entry.FileSize.ToString() + " bytes").PadRight(15),
                    (compressed ? "Yes" : "No").PadRight(11),
                    "0x" + entry.TGI.Type.ToString("X").PadLeft(8, '0').PadRight(12),
                    "0x" + entry.TGI.Group.ToString("X").PadLeft(8, '0').PadRight(12),
                    "0x" + entry.TGI.Instance.ToString("X").PadLeft(8, '0').PadRight(12));

                entryCount++;
            }
        }

        public static void SaveIndexEntry(string path, TypeGroupInstance tgi)
        {

        }
    }
}
