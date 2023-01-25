using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using SC4Parser.Logging;

namespace SC4Parser.Region
{
    /// <summary>
    /// Very simple Bitmap implementation, parses specifically the type of bitmap used by SimCity 4 
    /// (non compressed, BITMAPINFOHEADER)
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    internal class Bitmap
    {
        internal static class EndinessHelper
        {
            public static bool IsLittleEndian => BitConverter.IsLittleEndian;

            public static UInt16 ReverseBytes(UInt16 value)
            {
                return (ushort)((value >> 8) + (value << 8));
            }

            public static UInt32 ReverseBytes(UInt32 value)
            {
                return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                    (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
            }
        }

        public class FileHeader
        {
            public ushort Signature { get; private set; }
            public uint FileSize { get; private set; }
            public uint PixelArrayOffset { get; private set; }

            public FileHeader(BinaryReader reader)
            {
                Parse(reader);
            }

            public void Parse(BinaryReader reader)
            {
                Signature = reader.ReadUInt16();
                FileSize = reader.ReadUInt32();
                reader.BaseStream.Position += 4; // Skip reserved bytes
                PixelArrayOffset = reader.ReadUInt32();

                // Header is little endian, reserve it we need to
                // TODO: Does it make sense to check the endianness of machine?
                if (EndinessHelper.IsLittleEndian)
                {
                    Signature = EndinessHelper.ReverseBytes(Signature);
                    FileSize = EndinessHelper.ReverseBytes(FileSize);
                    PixelArrayOffset = EndinessHelper.ReverseBytes(PixelArrayOffset);
                }
            }

            public void Dump()
            {
                Console.WriteLine("Signature: 0x{0}", Signature.ToString("X2"));
                Console.WriteLine("File Size: 0x{0}", FileSize.ToString("X8"));
                Console.WriteLine("Pixel Array Offset: 0x{0}", PixelArrayOffset.ToString("X8"));
            }
        }

        public class InformationHeader
        {
            public uint HeaderSize { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public ushort ColorPlanes { get; private set; }
            public ushort BitPerPixel { get; private set; }
            public uint Compression { get; private set; }

            public InformationHeader(BinaryReader reader)
            {
                Parse(reader);
            }

            public void Parse(BinaryReader reader)
            {
                HeaderSize = reader.ReadUInt32();
                if (HeaderSize != 40)
                {
                    Logger.Log(LogLevel.Error, "Can't parse non BITMAPINFOHEADER bitmap, this bitmap was probably not produced by SimCity");
                    return;
                }
                Width = reader.ReadInt32();
                Height = reader.ReadInt32();
                ColorPlanes = reader.ReadUInt16();
                BitPerPixel = reader.ReadUInt16();
                Compression = reader.ReadUInt32();
                if (Compression != 0)
                {
                    Logger.Log(LogLevel.Error, "Can't parse bitmap with compression, this bitmap was probably not produced by SimCity");
                    return;
                }
                reader.BaseStream.Position += 20; // Skip over the rest of the header, we don't care
            }

            public void Dump()
            {
                Console.WriteLine("HeaderSize: {0}", HeaderSize);
                Console.WriteLine("Width: {0}", Width);
                Console.WriteLine("Height: {0}", Height);
                Console.WriteLine("BitPerPixel: {0}", BitPerPixel);
                Console.WriteLine("Compression: {0}", Compression);
            }
        }

        public struct RGB
        {
            byte R;
            byte G;
            byte B;


            public static RGB Parse(BinaryReader reader)
            {
                return new RGB()
                {
                    B = reader.ReadByte(),
                    G = reader.ReadByte(),
                    R = reader.ReadByte(),
                };
            }

            public string ToString()
            {
                return string.Format("{0},{1},{2}", R, G, B);
            }
        }

        public FileHeader Header { get; private set; }
        public InformationHeader DiBHeader { get; private set; }
        public RGB[][] PixelMap { get; private set; }

        public Bitmap(string path)
        {
            Parse(path);
        }

        public void Parse(string path)
        {
            Parse(File.ReadAllBytes(path));
        }
        public void Parse(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Parse headers
                Header = new FileHeader(reader);
                Header.Dump();
                DiBHeader = new InformationHeader(reader);
                DiBHeader.Dump();

                // Parse pixels
                PixelMap = new RGB[DiBHeader.Height][];
                for (int y = DiBHeader.Height - 1; y >= 0; y--)
                {
                    PixelMap[y] = new RGB[DiBHeader.Width];
                    for (int x = 0; x < DiBHeader.Width; x++)
                    {
                        PixelMap[y][x] = RGB.Parse(reader);
                    }
                }
            }
        }
    }
}
