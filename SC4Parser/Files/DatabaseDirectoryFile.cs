using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;

namespace SC4Parser.Files
{
    /// <summary>
    /// Represents a DatabaseDirectoryfile (DBDF or DIR file)
    /// 
    /// A DBDF (not to be confused with DBPF, thanks maxis...) is an IndexEntry within a SimCity 4 savegame (DBPF) that holds a list of
    /// all compressed files (DatabaseDirectoryResources) within a save game. 
    /// 
    /// The TypegroupInstance of a DBDF is always E86B1EEF E86B1EEF 286B1F03 in a save
    /// </summary>
    /// <remarks>
    /// Implemented from https://wiki.sc4devotion.com/index.php?title=DBDF
    /// </remarks>
    /// <seealso cref="SC4Parser.DataStructures.IndexEntry"/>
    public class DatabaseDirectoryFile : IndexEntry
    {
        /// <summary>
        /// List of all compressed resources in save
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.DatabaseDirectoryResource"/>
        public List<DatabaseDirectoryResource> Resources { get; private set; }
        /// <summary>
        /// Number of resources in file
        /// </summary>
        public uint ResourceCount { get; private set; }

        /// <summary>
        /// Default constructor for DatabaseDirectoryFile
        /// </summary>
        public DatabaseDirectoryFile() { }
        /// <summary>
        /// Constructor for a DatabaseDirectoryFile that uses it's 
        /// associated IndexEntry
        /// </summary>
        /// <param name="entry"></param>
        public DatabaseDirectoryFile(IndexEntry entry)
        {
            TGI = entry.TGI;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;

            ResourceCount = FileSize / 16;
            Resources = new List<DatabaseDirectoryResource>();
        }

        /// <summary>
        /// Load an individual DirectoryResource (compressed file entry) from a byte array
        /// </summary>
        /// <param name="buffer">Data containing DatabaseeDirectoryResource</param>
        public void ParseResource(byte[] buffer)
        {
            DatabaseDirectoryResource resource = new DatabaseDirectoryResource();
            resource.Parse(buffer);
            Resources.Add(resource);
        }

        /// <summary>
        /// Prints out the contents of DBDF
        /// </summary>
        public override void Dump()
        {
            base.Dump();

            foreach (DatabaseDirectoryResource resource in Resources)
            {
                Console.WriteLine("--------------");
                resource.Dump();
            }
        }
    }


}
