using System;

using SC4Parser.Types;
using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Implementation of an IndexEntry in a save game 
    /// 
    /// An IndexEntry represents a file stored within a SimCity 4 savegame (DBPF).
    /// It stores the TGI (identifier), the location of the file with in the savegame and the size of the file.
    /// </summary>
    /// <remarks>
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=DBPF#DBPF_1.x.2C_Index_Table_7.0
    /// </remarks>
    public class IndexEntry
    {
        /// <summary>
        /// TypeGroupInstance (TGI) of Index entry
        /// </summary>
        /// <see cref="SC4Parser.Types.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; set; }
        /// <summary>
        /// Location of the file in the DBPF that the index entry refers to
        /// </summary>
        public uint FileLocation { get; set; }
        /// <summary>
        /// The size of the index entry's file 
        /// </summary>
        public uint FileSize { get; set; }

        /// <summary>
        /// Loads an individual entry from a byte array
        /// </summary>
        /// <param name="buffer">Data to load the index entry from</param>
        /// <remarks>
        /// Buffer should only contain data for a single entry
        /// </remarks>
        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 20)
            {
                Logger.Log(LogLevel.Warning, "IndexEntry buffer is too small to parse");
                return;
            }

            TGI = new TypeGroupInstance(
                BitConverter.ToUInt32(buffer, 0),
                BitConverter.ToUInt32(buffer, 4),
                BitConverter.ToUInt32(buffer, 8)
            );
            FileLocation = BitConverter.ToUInt32(buffer, 12);
            FileSize = BitConverter.ToUInt32(buffer, 16);
        }

        /// <summary>
        /// Dumps the contents of an entry
        /// </summary>
        public virtual void Dump()
        {
            Console.WriteLine("TypeID: {0}", TGI.Type.ToString("X"));
            Console.WriteLine("GroupID: {0}", TGI.Group.ToString("X"));
            Console.WriteLine("InstanceID: {0}", TGI.Instance.ToString("X"));
            Console.WriteLine("File Location: {0}", FileLocation);
            Console.WriteLine("File Size: {0}", FileSize);
        }
    }
}
