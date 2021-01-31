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

        public static byte[] ReadBytes(byte[] buffer, uint count, ref uint offset)
        {
            byte[] readData = new byte[count];
            Buffer.BlockCopy(buffer, (int) offset, readData, 0, (int) count);
            offset += count;
            return readData;
        }

        public static byte ReadByte(byte[] buffer, ref uint offset)
        {
            byte data = buffer[offset];
            offset++;
            return data;
        }
    }
}
