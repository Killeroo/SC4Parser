﻿using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;
using SC4Parser.Logging;

namespace SC4Parser.Subfiles
{
    /// <summary>
    /// Implmentation of the Building Subfile. Building subfile stores all building data in a SimCity 4 savegame.
    /// </summary>
    /// <remarks>
    /// Actual reading of individual builds is done in DataStructure\Buildings.cs
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile
    /// </remarks>
    /// <seealso cref="SC4Parser.DataStructures.Building"/> 
    /// <seealso cref="SC4Parser.Subfiles.LotSubfile"/>
    public class BuildingSubfile
    {
        /// <summary>
        /// Stores all building in the subfile
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.Building"/>
        public List<Building> Buildings { get; private set; } = new List<Building>();

        /// <summary>
        /// Reads the Building Subfile from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="size">Size of data that is being read</param>
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
                Logger.Log(LogLevel.Warning, "Not all building have been read from Building Subfile (" + bytesToRead + " bytes left)");
            }
        }

        /// <summary>
        /// Prints out the contents of the Building Subfile
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
