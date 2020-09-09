using System;

using SC4Parser.Logging;

namespace SC4Parser.Compression
{
    /// <summary>
    /// Implementation of QFS/RefPack/LZ77 decompression
    /// </summary>
    class QFS
    {
        // QFS/quickref decompression implementation
        // ported from: https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/Modules/qfs.c#L25
        // specs:
        // - https://www.wiki.sc4devotion.com/index.php?title=DBPF_Compression
        // - http://wiki.niotso.org/RefPack#Naming_notes
        public static byte[] UncompressData(byte[] data)
        {
            byte[] sourceBytes = data;
            byte[] destinationBytes;
            int sourcePosition = 0;
            int destinationPosition = 0;

            // Check first 4 bytes (size of header + compressed data)
            uint compressedSize = BitConverter.ToUInt32(sourceBytes, 0);

            // Next read the 5 byte header
            byte[] header = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                header[i] = sourceBytes[i + 4];
            }

            // First 2 bytes should be the QFS identifier
            // Next 3 bytes should be the uncompressed size of file
            // (we do this by byte shifting (from most significant byte to least))
            // the last 3 bytes of the header to make a number)
            uint uncompressedSize = Convert.ToUInt32((long)(header[2] << 16) + (header[3] << 8) + header[4]); ;

            // Create our destination array
            destinationBytes = new byte[uncompressedSize];

            // Next set our position in the file
            // (The if checks if the first 4 bytes are the size of the file
            // if so our start position is 4 bytes + 5 byte header if not then our
            // offset is just the header (5 bytes))
            if ((sourceBytes[0] & 0x01) != 0)
            {
                sourcePosition = 9;//8;
            }
            else
            {
                sourcePosition = 5;
            }

            // In QFS the control character tells us what type of decompression operation we are going to perform (there are 4)
            // Most involve using the bytes proceeding the control byte to determine the amount of data that should be copied from what
            // offset. These bytes are labled a, b and c. Some operations only use 1 proceeding byte, others can use 3
            byte controlCharacter = 0;
            byte a = 0;
            byte b = 0;
            byte c = 0;
            int length = 0;
            int offset = 0;

            // Main decoding loop
            // Keep decoding while sourcePosition is in source array and position isn't 0xFC?
            while ((sourcePosition < sourceBytes.Length) && (sourceBytes[sourcePosition] < 0xFC))
            {
                // Read our packcode/control character
                controlCharacter = sourceBytes[sourcePosition];

                // Read bytes proceeding packcode
                a = sourceBytes[sourcePosition + 1];
                b = sourceBytes[sourcePosition + 2];
                c = sourceBytes[sourcePosition + 3];

                // Check which packcode type we are dealing with
                if ((controlCharacter & 0x80) == 0)
                {
                    // First we copy from the source array to the destination array
                    length = controlCharacter & 3;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 2, ref destinationBytes, destinationPosition, length);

                    // Then we copy characters already in the destination array to our current position in the destination array
                    sourcePosition += length + 2;
                    destinationPosition += length;
                    length = ((controlCharacter & 0x1C) >> 2) + 3;
                    offset = ((controlCharacter >> 5) << 8) + a + 1;
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);

                    destinationPosition += length;
                }
                else if ((controlCharacter & 0x40) == 0)
                {
                    length = (a >> 6) & 3;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 3, ref destinationBytes, destinationPosition, length);

                    sourcePosition += length + 3;
                    destinationPosition += length;
                    length = (controlCharacter & 0x3F) + 4;
                    offset = (a & 0x3F) * 256 + b + 1;
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);

                    destinationPosition += length;
                }
                else if ((controlCharacter & 0x20) == 0)
                {
                    length = controlCharacter & 3;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 4, ref destinationBytes, destinationPosition, length);

                    sourcePosition += length + 4;
                    destinationPosition += length;
                    length = ((controlCharacter >> 2) & 3) * 256 + c + 5;
                    offset = ((controlCharacter & 0x10) << 12) + 256 * a + b + 1;
                    LZCompliantCopy(ref destinationBytes, destinationPosition - offset, ref destinationBytes, destinationPosition, length);

                    destinationPosition += length;
                }
                else
                {
                    length = (controlCharacter & 0x1F) * 4 + 4;
                    LZCompliantCopy(ref sourceBytes, sourcePosition + 1, ref destinationBytes, destinationPosition, length);

                    sourcePosition += length + 1;
                    destinationPosition += length;
                }
            }

            // Add trailing bytes
            if ((sourcePosition < sourceBytes.Length) && (destinationPosition < destinationBytes.Length))
            {
                LZCompliantCopy(ref sourceBytes, sourcePosition + 1, ref destinationBytes, destinationPosition, sourceBytes[sourcePosition] & 3);
                destinationPosition += sourceBytes[sourcePosition] & 3;
            }

            if (destinationPosition != destinationBytes.Length)
            {
                Logger.Log(LogLevel.Warning, "QFS bad length, {0} instead of {1}", destinationPosition, destinationBytes.Length);
            }

            return destinationBytes;
        }

        // With QFS (LZ77) we require an LZ compatible copy method between arrays, what this means practically is that we need to copy
        // stuff one byte at a time from arrays. This is, because with LZ compatible algorithms, it is complete legal to copy over data that overruns
        // the currently filled position in the destination array. In other words it is more than likely the we will be asked to copy over data that hasn't
        // been copied yet. It's confusing, so we copy things one byte at a time using a recursive function.
        private static void LZCompliantCopy(ref byte[] source, int sourceOffset, ref byte[] destination, int destinationOffset, int length)
        {
            if (length != 0)
            {
                Buffer.BlockCopy(source, sourceOffset, destination, destinationOffset, 1);

                length = length - 1;
                sourceOffset++;
                destinationOffset++;

                LZCompliantCopy(ref source, sourceOffset, ref destination, destinationOffset, length);
            }
        }
    }
}
