using System;
using System.Text;

using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Header file for a Database Packed File (DBPF). 
    /// Implements version 1.0 of the DBPF header used for SimCity 4
    /// </summary>
    /// <remarks>
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=DBPF#Header
    /// </remarks>
    /// <seealso cref="SC4Parser.Files.DatabasePackedFile"/>
    public class DatabasePackedFileHeader
    {
        /// <summary>
        /// File identifier (always 'DBPF')
        /// </summary>
        public string Identifier { get; private set; }
        /// <summary>
        /// DBPF major version 
        /// </summary>
        /// <remarks>
        /// Common DBPF versions:
        ///     1.0 seen in Sim City 4, The Sims 2
        ///     1.1 seen in The Sims 2
        ///     2.0 seen in Spore, The Sims 3
        ///     3.0 seen in SimCity
        /// </remarks>
        public uint MajorVersion { get; private set; }
        /// <summary>
        /// DBPF minor version
        /// </summary>
        /// <remarks>
        /// Common DBPF versions:
        ///     1.0 seen in Sim City 4, The Sims 2
        ///     1.1 seen in The Sims 2
        ///     2.0 seen in Spore, The Sims 3
        ///     3.0 seen in SimCity
        /// </remarks>
        public uint MinorVersion { get; private set; }
        /// <summary>
        /// Date DBPF file was created
        /// </summary>
        /// <remarks>
        /// In Unix time stamp format
        /// </remarks>
        public DateTime DateCreated { get; private set; }
        /// <summary>
        /// Date DBPF file was modified
        /// </summary>
        /// <remarks>
        /// In Unix time stamp format
        /// </remarks>
        public DateTime DateModified { get; private set; }
        /// <summary>
        /// Index table major version
        /// </summary>
        /// <remarks>
        /// Always 7 in The Sims 2, Sim City 4. If this is used in 2.0, then it is 0 for SPORE. 
        /// </remarks>
        public uint IndexMajorVersion { get; private set; }
        /// <summary>
        /// Number of Index Entries in Index table
        /// </summary>
        public uint IndexCount { get; private set; }
        /// <summary>
        /// Position of first Index Entry in the DBPF file
        /// </summary>
        public uint FirstIndexOffset { get; private set; }
        /// <summary>
        /// Size of index table in bytes
        /// </summary>
        public uint IndexSize { get; private set; }
        /// <summary>
        /// Number of hole entries in Hole Record
        /// </summary>
        public uint HoleCount { get; private set; }
        /// <summary>
        /// Location of Hole Record in the DBPF file
        /// </summary>
        public uint HoleOffset { get; private set; }
        /// <summary>
        /// size of the Hold Record
        /// </summary>
        public uint HoleSize { get; private set; }
        /// <summary>
        /// Index table minor version
        /// </summary>
        public uint IndexMinorVersion { get; private set; }

        /// <summary>
        /// Reads a DBPF header from a byte array
        /// </summary>
        /// <param name="buffer">Data to read header from</param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        public void Parse(byte[] buffer)
        {
            if (buffer.Length < 96)
            {
                Logger.Log(LogLevel.Error, "DBPF header buffer is too small to parse ({0}/{1} bytes)", buffer.Length, 96);
                return;
            }

            // Read header contents
            Identifier = Encoding.ASCII.GetString(buffer, 0, 4);
            MajorVersion = BitConverter.ToUInt32(buffer, 4);
            MinorVersion = BitConverter.ToUInt32(buffer, 8);
            DateCreated = Utils.UnixTimestampToDateTime(BitConverter.ToUInt32(buffer, 24));
            DateModified = Utils.UnixTimestampToDateTime(BitConverter.ToUInt32(buffer, 28));
            IndexMajorVersion = BitConverter.ToUInt32(buffer, 32);
            IndexCount = BitConverter.ToUInt32(buffer, 36);
            FirstIndexOffset = BitConverter.ToUInt32(buffer, 40);
            IndexSize = BitConverter.ToUInt32(buffer, 44);
            HoleCount = BitConverter.ToUInt32(buffer, 48);
            HoleOffset = BitConverter.ToUInt32(buffer, 52);
            HoleSize = BitConverter.ToUInt32(buffer, 56);
            IndexMinorVersion = BitConverter.ToUInt32(buffer, 60);
        }

        /// <summary>
        /// Dumps contents of header
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Identifier: {0}", Identifier);
            Console.WriteLine("Major Version: {0}", MajorVersion);
            Console.WriteLine("Minor Version: {0}", MinorVersion);
            Console.WriteLine("Date Created: {0}", DateCreated);
            Console.WriteLine("Date Modified: {0}", DateModified);
            Console.WriteLine("Index Major Version: {0}", IndexMajorVersion);
            Console.WriteLine("Index Minor Version: {0}", IndexMinorVersion);
            Console.WriteLine("Index Entry Count: {0}", IndexCount);
            Console.WriteLine("First Index Offset: {0}", FirstIndexOffset);
            Console.WriteLine("Index Table Size: {0} bytes", IndexSize);
            Console.WriteLine("Hole Count: {0}", HoleCount);
            Console.WriteLine("Hole Offset: {0}", HoleOffset);
            Console.WriteLine("Hole Size: {0} bytes", HoleSize);
        }
    }
}
