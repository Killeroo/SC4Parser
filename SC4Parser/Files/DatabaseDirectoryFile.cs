using System;
using System.Collections.Generic;

namespace SC4Parser.Structures
{
    class DatabaseDirectoryFile : IndexEntry
    {
        public uint ResourceCount;
        public List<DatabaseDirectoryResource> Resources;

        public DatabaseDirectoryFile() { }
        public DatabaseDirectoryFile(IndexEntry entry)
        {
            TGI = entry.TGI;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;

            ResourceCount = FileSize / 16;
            Resources = new List<DatabaseDirectoryResource>();
        }

        public void ParseResource(byte[] buffer)
        {
            DatabaseDirectoryResource resource = new DatabaseDirectoryResource();
            resource.Parse(buffer);
            Resources.Add(resource);
        }

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
