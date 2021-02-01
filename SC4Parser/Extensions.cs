using System;
using System.IO;
using System.Collections.Generic;

namespace SC4Parser
{
    /// <summary>
    /// Class for extension and helper methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Helper method for reading and copying bytes to a new array while updating the offset
        /// with the number of bytes read
        /// </summary>
        /// <remarks>
        /// This method of reading bytes is not used everywhere in the code, only in Network subfile parsing
        /// </remarks>
        /// <param name="buffer">data to read from</param>
        /// <param name="count">number of bytes to read</param>
        /// <param name="offset">offset to read from and update</param>
        /// <returns>New array with data copied from the source byte array</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to read from data outside of the bounds of the byte array
        /// </exception>
        /// <seealso cref="SC4Parser.Extensions.ReadByte(byte[], ref uint)"/>
        public static byte[] ReadBytes(byte[] buffer, uint count, ref uint offset)
        {
            byte[] readData = new byte[count];
            Buffer.BlockCopy(buffer, (int) offset, readData, 0, (int) count);
            offset += count;
            return readData;
        }

        /// <summary>
        /// Helper method for reading a single byte from an array while updating an offset
        /// </summary>
        /// <remarks>
        /// This method of reading bytes is not used everywhere in the code, only in Network subfile parsing
        /// </remarks>
        /// <param name="buffer">data to read from</param>
        /// <param name="offset">offset to read from and update</param>
        /// <returns>New array with data copied from the source byte array</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to read from data outside of the bounds of the byte array
        /// </exception>
        /// <seealso cref="SC4Parser.Extensions.ReadBytes(byte[], uint, ref uint)"/>
        public static byte ReadByte(byte[] buffer, ref uint offset)
        {
            byte data = buffer[offset];
            offset++;
            return data;
        }
    }
}
