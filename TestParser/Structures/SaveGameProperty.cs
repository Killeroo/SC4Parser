using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures
{
    class SaveGameProperty
    {
        public uint PropertyNameValue;
        public uint PropertyNameValueCopy;
        public uint Unknown1;
        public byte DataType;
        public byte KeyType;
        public ushort Unknown2;
        public uint DataRepeatedCount;
        public List<object> Data;

        // Ok so this is going to get a little messy...
        // So as awesome and simple as it was to use just buffers, this is going to be a bit painful.
        // We are going to have to be given a buffer of indescriminate size, the first part is going to contain
        // our sig prop data BUT (because the actual data can be repeated X time) we are going to have to read as we go
        // a long, get everything we need and then determine that we have read the whole SGPROP then exit and return an
        // offset for whatever called us to use as an offset.. joy, prepare for some hacks
        // OH and the fucking data can be a variety of types (┛ಠ_ಠ)┛彡┻━┻
        public uint Parse(byte[] buffer)
        {
            PropertyNameValue = BitConverter.ToUInt32(buffer, 0);
            PropertyNameValueCopy = BitConverter.ToUInt32(buffer, 4);
            Unknown1 = BitConverter.ToUInt32(buffer, 8);
            DataType = buffer[12];
            KeyType = buffer[13];
            Unknown2 = BitConverter.ToUInt16(buffer, 14);
            DataRepeatedCount = BitConverter.ToUInt32(buffer, 16);
            return 0;
        }

        public void Dump()
        {

        }
    }
}
