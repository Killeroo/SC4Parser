using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implementation of the Lots Subfile. Lot Subfile contains all logs in a SimCity 4 savegame (Partial implementation).
    /// </summary>
    /// <remarks>
    /// The implementation of the lots is only partially complete and will not contain all data associated with the lots
    /// 
    /// Actual reading of individual builds is done in DataStructure\Lot.cs
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Lot_Subfile
    /// </remarks>
    /// <seealso cref="SC4Parser.DataStructures.Lot"/>
    /// <seealso cref="SC4Parser.Subfiles.BuildingSubfile"/>
    /// <example>
    /// <c>
    /// // Simple usage
    /// // (Just assume the lot subfile has already been read, see SC4SaveGame.GetLotSubfile())
    ///
    /// // Access a lot
    /// Lot firstLot = lotSubfile.Lots.First();
    /// 
    /// // Do something with it
    /// firstLot.Dump();
    /// </c>
    /// </example>
    public class LotSubfile
    {
        /// <summary>
        /// All lots stored in the subfile
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.Lot"/>
        public List<Lot> Lots = new List<Lot>();

        /// <summary>
        /// Reads the Lots Subfile from byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="size">Size of data to be read</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, int size)
        {
            Logger.Log(LogLevel.Info, "Parsing Lot subfile...");

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

            Logger.Log(LogLevel.Info, "Lot subfile parsed");
        }

        /// <summary>
        /// Prints out the contents of the Lot Subfile
        /// </summary>
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
