using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SC4Parser
{
    class QFS
    {
        // Ok we are going to do our own QFS implementation instead of messing around with existing implementations (that just don't work)
        // or trying to get c++ code running from c# (passing a byte array sucks)



        // Ref:
        //https://github.com/OpenSAGE/OpenSAGE/blob/351b62df9e70041148beb1218386a4e838e5ae15/src/OpenSage.FileFormats.RefPack/RefPackStream.cs
        //https://github.com/gibbed/Gibbed.RefPack/blob/master/Decompression.cs

        // Note:
        // Standard refpack won't work because there are some slight variations in how refpack was implemented for different games
        // I think it is only slightly different behaviour in the last byte (might be the same as CC3) 

        // TODO:
        // Confirm differences for SC4 usage

        public static byte[] Uncompress(byte[] source, int uncompressedLength)
        {
            // TODO: convert to stream
            uint compressedSize;
            int uncompressedSize;
            ushort compressionId;
            int offset = 0;

            // First convert byte array to stream so it is a bit easier to handle
            Stream sourceStream = new MemoryStream(source);

            // Read compressed size
            compressedSize = BitConverter.ToUInt32(source, offset);
            offset += 4;

            // Next read compression id
            // NOTE: There are also some flags that we just assume are there for sc4 inside the first 
            // byte, probably fine..
            compressionId = BitConverter.ToUInt16(source, offset);
            if (compressionId != 0x10FB)
            {
                Logger.Error("Bad compression id found");
                return null;
            }
            offset += 2;

            // Read uncompressed size
            // Converting 3 bytes to 32 bit int (https://stackoverflow.com/a/8104367)
            uncompressedSize = source[offset] << 16 | source[offset + 1] << 8 | source[offset + 2];
            offset += 3;

            // TODO: Next write something basic to properly loop through the array
            // print out the control bytes and reach end of file
            // copy stuff later

            Console.WriteLine(compressedSize);
            Console.WriteLine(compressionId.ToString("X"));
            Console.WriteLine(uncompressedSize);

            return new byte[1];
        }

        public static void Dump()
        {

        }
    }
}
