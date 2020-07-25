using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures.SubFiles
{
    class BuildingsFile
    {
        List<Building> Buildings = new List<Building>();

        public void Parse(byte[] buffer, int size)
        {
            uint counted = Convert.ToUInt32(size);
            uint offset = 0;

            while (counted > 0)
            {
                uint s = BitConverter.ToUInt32(buffer, (int) offset);

                Building building = new Building();
                byte[] b = new byte[s];
                Array.Copy(buffer, offset, b, 0, (int)s);
                building.Parse(b, offset);
                Buildings.Add(building);

                offset += s;
                counted -= s;
            }

            if (counted != 0)
            {
                Logger.Warning("Not all building have been read from building subfile (" + counted + " bytes left)");
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
