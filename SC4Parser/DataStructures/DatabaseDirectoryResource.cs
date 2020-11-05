﻿using System;

using SC4Parser.Types;
using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Implementation of a DatabaseDirectoryResource (DIR record).
    /// A DatabaseDirectoryResource represents a compressed file within a SimCity 4 savegame (DBPF)
    /// The uncompressed size of the record can be used to determine if a file has been decompressed properly.
    /// </summary>
    /// <remarks>
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=DBDF
    /// </remarks>
    /// <see cref="SC4Parser.Files.DatabaseDirectoryFile.Resources"/>
    public class DatabaseDirectoryResource
    {
        /// <summary>
        /// TypeGroupInstance (TGI) of resource
        /// </summary>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; private set; }
        /// <summary>
        /// Decompressed size of resource's file
        /// </summary>
        public uint DecompressedFileSize { get; private set; }

        /// <summary>
        /// Reads an individual resource from a byte array
        /// </summary>
        /// <param name="buffer">Data to load resource from</param>
        /// <remarks>
        /// Byte array given to method should only contain the data for one resource
        /// </remarks>
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
        /// Prints out the values of a resource
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
