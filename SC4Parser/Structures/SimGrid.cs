using System;
using System.Collections.Generic;
using System.Text;

namespace SC4Parser.Structures
{
    //https://wiki.sc4devotion.com/index.php?title=SimGrid
    public class SimGrid
    {
        public uint Size { get; private set; }
        public uint CRC;
        public uint MemoryAddress;
        public uint MajorVersion;
        public uint TypeId;
        public uint DataId;
        public uint Resolution;
        public uint ResolutionPower;
        public uint SizeX;
        public uint SizeY;
        public object[][] Values;
    }
}
