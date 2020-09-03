using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Structures.SubFiles
{
    public class BuildingSubFile
    {
        List<Building> Buildings = new List<Building>();

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
            }

            if (bytesToRead != 0)
            {
                Logger.Warning("Not all building have been read from building subfile (" + bytesToRead + " bytes left)");
            }
        }

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
