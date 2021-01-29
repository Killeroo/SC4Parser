using System;
using System.IO;
using System.Collections.Generic;

namespace SC4Parser
{
    public static class Extensions
    {
        //public static byte[] ReadBytes(this MemoryStream stream, int count)
        //{
        //    byte[] data = new byte[count];
        //    stream.Read(data);

        //}

        public static byte[] ReadBytes(byte[] buffer, int count, ref int offset)
        {
            byte[] readData = new byte[count];
            Buffer.BlockCopy(buffer, offset, readData, 0, count);
            offset += count;
            return readData;
        }

        public static byte ReadByte(byte[] buffer, ref int offset)
        {
            byte data = buffer[offset];
            offset++;
            return data;
        }
    }
}
