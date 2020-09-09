using System;

using SC4Parser.Types;
using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// A DatabaseDirectoryResource represents a compressed file within a SimCity 4 savegame (DBPF)
    /// The uncompressed size of the record can be used to determine if a file has been decompressed properly.
    /// (Implemented from https://wiki.sc4devotion.com/index.php?title=DBDF)
    /// </summary>
    public class DatabaseDirectoryResource
    {
        public TypeGroupInstance TGI;
        public uint DecompressedFileSize;

        /// <summary>
        /// Reads an individual resource from a byte array
        /// </summary>
        /// <param name="buffer"></param>
        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 16)
            {
                Logger.Log(LogLevel.Warning, "DatabaseDirectoryResource is too small to parse");
                return;
            }

            TGI = new TypeGroupInstance(
                BitConverter.ToUInt32(buffer, 0),
                BitConverter.ToUInt32(buffer, 4),
                BitConverter.ToUInt32(buffer, 8)
            );
            DecompressedFileSize = BitConverter.ToUInt32(buffer, 12);
        }

        /// <summary>
        /// Dumps the contents of a resource
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("TypeID: {0}", TGI.Type.ToString("X"));
            Console.WriteLine("GroupID: {0}", TGI.Group.ToString("X"));
            Console.WriteLine("InstanceID: {0}", TGI.Instance.ToString("X"));
            Console.WriteLine("Decompressed File Size: {0} bytes", DecompressedFileSize);
        }
    }
}
