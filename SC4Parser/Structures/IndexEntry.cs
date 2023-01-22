using System;

using SC4Parser.Logging;

namespace SC4Parser
{
    /// <summary>
    /// Implementation of an Index Entry in a save game 
    /// 
    /// An Index Entry represents a file stored within a SimCity 4 savegame (DBPF).
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
        /// <see cref="SC4Parser.TypeGroupInstance"/>
        public TypeGroupInstance TGI { get; protected set; }
        /// <summary>
        /// Location of the file in the DBPF that the index entry refers to
        /// </summary>
        public uint FileLocation { get; protected set; }
        /// <summary>
        /// The size of the index entry's file 
        /// </summary>
        public uint FileSize { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public IndexEntry() { }
        /// <summary>
        /// Constructor meant for copying over an Index Entry to a new object
        /// </summary>
        /// <param name="entry">Entry to copy over to new object</param>
        public IndexEntry(IndexEntry entry)
        {
            TGI = entry.TGI;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;
        }
        /// <summary>
        /// Constructor for manually constructing an Index Entry without parsing it
        /// </summary>
        /// <param name="tgi">TypeGroupInstance (TGI) of Index Entry</param>
        /// <param name="location">File location of Index Entry</param>
        /// <param name="size">File size of Index Entry</param>
        public IndexEntry(TypeGroupInstance tgi, uint location, uint size)
        {
            TGI = tgi;
            FileLocation = location;
            FileSize = size;
        }

        /// <summary>
        /// Loads an individual entry from a byte array
        /// </summary>
        /// <param name="buffer">Data to load the index entry from</param>
        /// <remarks>
        /// Buffer should only contain data for a single entry
        /// </remarks>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
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
            Console.WriteLine("TypeID: {0}", TGI.Type.ToString("x8"));
            Console.WriteLine("GroupID: {0}", TGI.Group.ToString("x8"));
            Console.WriteLine("InstanceID: {0}", TGI.Instance.ToString("x8"));
            Console.WriteLine("File Location: {0}", FileLocation);
            Console.WriteLine("File Size: {0}", FileSize);
        }
    }
}
