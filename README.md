# SC4Parser
SC4Parser is a general purpose library for parsing, finding, and loading files from Simcity 4 save game files ([Maxis Database Packed Files (DBPF)](https://wiki.sc4devotion.com/index.php?title=Savegame)) written in C#.

The library was mainly intended for extracting items, from save games. It contains some partial implementation of specific Simcity 4 subfiles but mainly provides solid structures for loading data from save game files.

Because DBPF files were used for save games for other maxis games (Sims 3, Spore etc) the library may be useful for adapting to work with other Maxis games.

# What can it do

- Parse SimCity 4 savegames/Database Packed Files (DBPFs)
- Find and load IndexEntry/file data from saves based on Type ID or TypeGroupInstance (TGI)
- Decompress QFS compressed IndexEntry data
- Parse the following Subfiles:
  - [Lots Subfile](https://wiki.sc4devotion.com/index.php?title=Lot_Subfile) (*Partially Implemented*)
  - [Buildings Subfile](https://wiki.sc4devotion.com/index.php?title=Building_Subfile) (*Fully Implemented*)
  - [RegionView Subfile](https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles) (*Partially Implemented*)
  - [TerrainMap Subfile](https://github.com/sebamarynissen/sc4/blob/227aecdd01fedd78059a4114e6b0a1e9b6bd50a0/lib/terrain-map.js#L19) (*Fully Implemented*)

# Getting started
Once the library is built (or [downloaded](https://github.com/Killeroo/SC4Parser/releases/latest)) and referenced by your project (when using Visual Studio have a look [here](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2019#add-a-reference)) it should be fairly straightforward to use. The first thing you need to do is load the SimCity 4 save game:
```
SC4SaveFile savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
```
Once the savegame is loaded you can search for specific files inside the save:
```
// Find the Terrain Map Subfile
IndexEntry entry = savegame.FindIndexEntry(new TypeGroupInstance("a9dd6ff4", "e98f9525", "00000001")); 
```
and/or load the file's data from the save (will be automatically decompressed if needed)
```
// Load the data for the Terrain Map Subfile
var data = savegame.LoadIndexEntry(new TypeGroupInstance("a9dd6ff4", "e98f9525", "00000001"));

// Pass the data to your own code or parser (the world is your oyster)
YourOwnInterestingTerrainParser parser = new YourOwnInterestingTerrainParser(data);
```
or you can load one of the implemented subfiles
```
// Get a list of all building in the city
List<Building> buildings = savegame.GetBuildingSubfile().Buildings;
```

# Documentation
You can find full documentation with examples in [DOCUMENTATION.md](DOCUMENTATION.md)

# Implemented Subfiles
As mentioned, this library was not intended to implement all subfiles contained in SimCity 4 saves but along the way a few subfiles were implemented and have been included in the library (some more may be added in the future):

  - [Lots Subfile](https://wiki.sc4devotion.com/index.php?title=Lot_Subfile) (*Partially Implemented*)
  - [Buildings Subfile](https://wiki.sc4devotion.com/index.php?title=Building_Subfile) (*Fully Implemented*)
  - [RegionView Subfile](https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles) (*Partially Implemented*)
  - [TerrainMap Subfile](https://github.com/sebamarynissen/sc4/blob/227aecdd01fedd78059a4114e6b0a1e9b6bd50a0/lib/terrain-map.js#L19) (*Fully Implemented*)

# Motivation
The library was created for [another project](https://github.com/Killeroo/SC4Cartographer) that would create maps from savegame. I couldn't find any good or usable code for DBPF parsing written in C# so I used the incredibly useful documentation over at [SC4Devotion](https://wiki.sc4devotion.com/index.php?title=Savegame) to decode and write a general parser for these saves.

Along the way it also became neccessary to implement the QFS/RefPak compression algorithm used to pack files in the savegames. This proved particularly troublesome as all source code for mods that I could find seemed to use the [same](https://github.com/wouanagaine/SC4Mapper-2013/blob/master/Modules/qfs.c) [algorithm](https://github.com/sebamarynissen/sc4/blob/master/src/decompress.cpp) for decompression. So instead of finding a way to compile and link the c implementation to the algorithm (I did try) I decided to [port it over](https://github.com/Killeroo/SC4Parser/blob/master/SC4Parser/Compression/QFS.cs) (thanks Denis Auroux!).

Hopefully some more accessible source code for parsing and decoding these files will help someone like me who just wants to write a small tool to open up SimCity 4 save games

# License
```
MIT License

Copyright (c) 2020 Matthew Carney

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
