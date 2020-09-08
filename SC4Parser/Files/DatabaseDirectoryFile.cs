using System;
using System.Collections.Generic;

using SC4Parser.DataStructures;

namespace SC4Parser.Files
{
    /// <summary>
    /// DatabaseDirectoryFile (DBDF) (not to be confused with DBPF, thanks maxis...) is an IndexEntry within a SimCity 4 savegame (DBPF) that holds a list of
    /// all compressed files (DatabaseDirectoryResources) within a SC4 savegame. The TGI of a DBDF is always E86B1EEF E86B1EEF 286B1F03 in a SC4 save
    /// Implemented using following spec: https://wiki.sc4devotion.com/index.php?title=DBDF
    /// </summary>
    public class DatabaseDirectoryFile : IndexEntry
    {
        public List<DatabaseDirectoryResource> Resources;
        public uint ResourceCount;

        public DatabaseDirectoryFile() { }
        public DatabaseDirectoryFile(IndexEntry entry)
        {
            TGI = entry.TGI;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;

            ResourceCount = FileSize / 16;
            Resources = new List<DatabaseDirectoryResource>();
        }

        /// <summary>
        /// Load an individual DirectoryResource (compressed file entry) 
        /// </summary>
        public void ParseResource(byte[] buffer)
        {
            DatabaseDirectoryResource resource = new DatabaseDirectoryResource();
            resource.Parse(buffer);
            Resources.Add(resource);
        }

        /// <summary>
        /// Dump contents of DBDF
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
