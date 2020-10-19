using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Building subfile stores all building data in a SimCity 4 savegame (DBPF).
    /// Actual reading of individual builds is done in DataStructure\Buildings.cs
    /// (Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile)
    /// </summary>
    public class BuildingSubfile
    {
        List<Building> Buildings = new List<Building>();

        /// <summary>
        /// Reads the building subfile from a byte array
        /// </summary>
        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint currentSize = BitConverter.ToUInt32(buffer, (int) offset);

                // Read building at current position
                Building building = new Building();
                byte[] buildingBuffer = new byte[currentSize];
                Array.Copy(buffer, offset, buildingBuffer, 0, (int)currentSize);
                building.Parse(buildingBuffer, offset);
                Buildings.Add(building);

                // Update offset and bytes read and move on
                offset += currentSize;
                bytesToRead -= currentSize;

                Logger.Log(LogLevel.Debug, $"building read ({currentSize} bytes), offset {offset} got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Log(LogLevel.Warning, "Not all building have been read from building subfile (" + bytesToRead + " bytes left)");
            }
        }

        /// <summary>
        /// Dump the contents of the building subfile
        /// </summary>
        public void Dump()
        {
            foreach (Building building in Buildings)
            {
                Console.WriteLine("--------------------");
                building.Dump();
            }
        }
    }
}
