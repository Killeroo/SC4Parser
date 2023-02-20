using System;
using System.Collections.Generic;

using SC4Parser.Logging;

namespace SC4Parser
{
    /// <summary>
    /// Implmentation of the Building Subfile. Building subfile stores all building data in a SimCity 4 savegame.
    /// </summary>
    /// <remarks>
    /// Actual reading of individual builds is done in DataStructure\Buildings.cs
    /// 
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile
    /// </remarks>
    /// <seealso cref="SC4Parser.Building"/> 
    /// <seealso cref="SC4Parser.LotSubfile"/>
    /// <example>
    /// <c>
    /// // Simple usage
    /// // (Just assume the building subfile has already been read, see SC4SaveGame.GetBuildingSubfile())
    ///
    /// // Access a building
    /// Building firstBuilding = buildingSubfile.Buildings.First();
    /// 
    /// // Do something with it
    /// firstBuilding.Dump();
    /// </c>
    /// </example>
    public class BuildingSubfile
    {
        /// <summary>
        /// Stores all building in the subfile
        /// </summary>
        /// <see cref="SC4Parser.Building"/>
        public List<Building> Buildings { get; private set; } = new List<Building>();

        /// <summary>
        /// Reads the Building Subfile from a byte array
        /// </summary>
        /// <param name="buffer">Data to read subfile from</param>
        /// <param name="size">Size of data that is being read</param>        
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer, int size)
        {
            Logger.Log(LogLevel.Info, "Parsing Building subfile...");

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

            Logger.Log(LogLevel.Info, "Parsed Building subfile");
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
