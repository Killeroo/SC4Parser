# SC4Parser
SC4Parser is a general purpose library for parsing, finding, viewing and loading items from Simcity 4 save game files (Maxis Database Packed Files (DBPF)).
The library was mainly intended for extracting items, from save games. It contains some partial implementation of specific Simcity 4 subfiles but mainly provides solid structures for loading data from save game files.
Because DBPF files were used for save games for other maxis games (Sims 3, Spore etc) the library may be useful for adapting to work with other Maxis games.

# What can it do

- Find and load IndexEntry data from saves based on Type ID or TypeGroupInstance (TGI)
- Decompress QFS compressed IndexEntry data
- Implementations of following SubFiles:
  - [Lots Subfile](https://wiki.sc4devotion.com/index.php?title=Lot_Subfile) (*Partially Implemented*)
  - [Buildings Subfile](https://wiki.sc4devotion.com/index.php?title=Building_Subfile) (*Fully Implemented*)
  - [RegionView Subfile](https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles) (*Partially Implemented*)
  - [TerrainMap Subfile](https://github.com/sebamarynissen/sc4/blob/227aecdd01fedd78059a4114e6b0a1e9b6bd50a0/lib/terrain-map.js#L19) (*Fully Implemented*)

# How to use
The library should be fairly straight forward ot use. the below example extracts 
```
            SC4SaveFile save = new SC4SaveFile(path);

            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}",
                "Entry".PadRight(6),
                "Location".PadRight(15),
                "File Size".PadRight(15),
                "Compressed ",
                "Type".PadRight(14),
                "Group".PadRight(14),
                "Instance".PadRight(14));

            Console.WriteLine(new string('-', 85));

            int entryCount = 1;
            foreach (IndexEntry entry in save.IndexEntries)
            {
                bool compressed = save.IsIndexEntryCompressed(entry);
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}",
                    entryCount.ToString().PadRight(6),
                    ("0x" + entry.FileLocation.ToString("X").PadLeft(8, '0')).PadRight(15),
                    (entry.FileSize.ToString() + " bytes").PadRight(15),
                    (compressed ? "Yes" : "No").PadRight(11),
                    "0x" + entry.TGI.Type.ToString("X").PadLeft(8, '0').PadRight(12),
                    "0x" + entry.TGI.Group.ToString("X").PadLeft(8, '0').PadRight(12),
                    "0x" + entry.TGI.Instance.ToString("X").PadLeft(8, '0').PadRight(12));

                entryCount++;
            }
```

# Motivation
The library was created for another project (link) that would create maps from savegame. So the library was designed aorund extraction of specific subfiles.

# Documentation

# Extneding

