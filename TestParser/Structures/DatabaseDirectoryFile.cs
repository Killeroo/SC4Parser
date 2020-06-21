using System;
using System.Collections.Generic;

namespace TestParser.Structures
{
    class DatabaseDirectoryFile : IndexEntry
    {
        public uint ResourceCount;
        public List<DBDFResource> Resources;

        public DatabaseDirectoryFile() { }
        public DatabaseDirectoryFile(IndexEntry entry)
        {
            TGI = entry.TGI;
            FileLocation = entry.FileLocation;
            FileSize = entry.FileSize;

            ResourceCount = FileSize / 16;
            Resources = new List<DBDFResource>();
        }

        public void ParseResource(byte[] buffer)
        {
            DBDFResource resource = new DBDFResource();
            resource.Parse(buffer);
            Resources.Add(resource);
        }

        public override void Dump()
        {
            base.Dump();

            foreach (DBDFResource resource in Resources)
            {
                Console.WriteLine("--------------");
                resource.Dump();
            }
        }
    }


}
