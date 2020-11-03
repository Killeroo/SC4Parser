using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// LotSubfile contains all logs in a SimCity 4 savegame (Partial implementation).
    /// Actual reading of individual builds is done in DataStructure\Lot.cs
    /// (Implmeneted from https://wiki.sc4devotion.com/index.php?title=Lot_Subfile)
    /// </summary>
    public class LotSubfile
    {
        public List<Lot> Lots = new List<Lot>();

        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint currentSize = BitConverter.ToUInt32(buffer, (int)offset);

                Lot lot = new Lot();
                byte[] b = new byte[currentSize];
                Array.Copy(buffer, offset, b, 0, (int)currentSize);
                lot.Parse(b, offset);
                Lots.Add(lot);

                offset += currentSize;
                bytesToRead -= currentSize;

                Logger.Log(LogLevel.Debug, $"lot read ({currentSize} bytes), offset {offset} got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, "Not all lots have been read from lot subfile (" + bytesToRead + " bytes left)");
            }
        }

        public void Dump()
        {
            foreach (Lot lot in Lots)
            {
                Console.WriteLine("--------------------");
                lot.Dump();
            }
        }
    }
}
