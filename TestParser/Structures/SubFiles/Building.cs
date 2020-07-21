using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures.SubFiles
{
    //https://wiki.sc4devotion.com/index.php?title=Building_Subfile
    class Building
    {
        public uint Size;
        public uint CRC;
        public uint Memory;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public ushort ZotWord;
        public byte AppearanceFlag;
        
        public void Parse(byte[] buffer)
        {
        }
    }
}
