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
                offset += s;
                counted -= s;
                Console.WriteLine("Building found, size: {0}, {1} left", s, counted);
            }
        }

        public void Dump()
        {

        }
    }
}
