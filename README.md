# SC4Parser
SC4Parser is a general purpose library for parsing, finding, and loading files from Simcity 4 save game files ([Maxis Database Packed Files (DBPF)](https://wiki.sc4devotion.com/index.php?title=Savegame)), implemented as a Net Standard 2.1 class library.

The library was mainly intended for extracting items, from save games. It contains some partial implementation of specific Simcity 4 subfiles but mainly provides solid structures for loading data from save game files. 

Because DBPF files were used for save games for other maxis games (Sims 3, Spore etc) the library may be useful for adapting to work with other Maxis games.

# What can it do

- Parse SimCity 4 savegames/Database Packed Files (DBPFs)
- Find and load IndexEntry/file data from saves based on Type ID or TypeGroupInstance (TGI)
- Decompress QFS compressed IndexEntry data
- Parse the following Subfiles:
  - [Buildings Subfile](https://wiki.sc4devotion.com/index.php?title=Building_Subfile) (*Fully Implemented*)
  - [TerrainMap Subfile](https://github.com/sebamarynissen/sc4/blob/227aecdd01fedd78059a4114e6b0a1e9b6bd50a0/lib/terrain-map.js#L19) (*Fully Implemented*)
  - [Network Subfile 1](https://wiki.sc4devotion.com/index.php?title=Network_Subfiles) (*Fully Implemented*)
  - [Network Subfile 2](https://wiki.sc4devotion.com/index.php?title=Network_Subfiles) (*Fully Implemented*)
  - Bridge Network Subfile (*Partially Implemented*)
  - [Lots Subfile](https://wiki.sc4devotion.com/index.php?title=Lot_Subfile) (*Partially Implemented*)
  - [RegionView Subfile](https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles) (*Partially Implemented*)

# Setup/Download

You can fetch the latest version of SC4Parser from [Nuget](https://www.nuget.org/packages/SC4Parser/), using the [NuGet Package Manager UI](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio) or via the commandline:

```
Install-Package SC4Parser -Version 1.1.2
```

You can also download a prebuilt windows version of the library from the [latest releases from github](https://github.com/Killeroo/SC4Parser/releases/latest) and reference the library in your project (when using Visual Studio have a look [at this guide](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2019#add-a-reference)). Or build the library from source (it's not that hard I swear..).

# Getting started
Once you have library setup and referenced (check above) it should be fairly straightforward to use. The first thing you need to do is load the SimCity 4 save game:
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

  - [Buildings Subfile](https://wiki.sc4devotion.com/index.php?title=Building_Subfile) (*Fully Implemented*)
  - [TerrainMap Subfile](https://github.com/sebamarynissen/sc4/blob/227aecdd01fedd78059a4114e6b0a1e9b6bd50a0/lib/terrain-map.js#L19) (*Fully Implemented*)
  - [Network Subfile 1](https://wiki.sc4devotion.com/index.php?title=Network_Subfiles) (*Fully Implemented*)
  - [Network Subfile 2](https://wiki.sc4devotion.com/index.php?title=Network_Subfiles) (*Fully Implemented*)
  - Bridge Network Subfile (*Partially Implemented*)
  - [Lots Subfile](https://wiki.sc4devotion.com/index.php?title=Lot_Subfile) (*Partially Implemented*)
  - [RegionView Subfile](https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles) (*Partially Implemented*)
  - [Network Index Subfile](https://wiki.sc4devotion.com/index.php?title=Network_Subfiles) (*Very Incomplete, do not use*)
  
Most information included in the library or source came from write ups found at https://wiki.sc4devotion.com

# Design
The classes in this library (specifically the subfiles and their objects) have been designed around what is actually found in the save game data. I have opted to, for the most part, implement the save game objects and properties as they appear in the raw data rather than representing them differently soley for the sake of accessiblity. 

This is why you may find what seem like duplicate properties like ```MaxSizeX1``` and ```MaxSizeX2```, littered throughout the save game subfiles. The save games and their data contain weird variables and odd patterns, the purposes of which are not always known and can only truly be understood by those who worked on the game. So I have left it as an exercise to the user; The library give you access to what is in the files and then left it up to them to understand and use them as they see fit. I do not want to let my implementation of the files dictate how their program should access them.

It is worth noting as well that some unknown fields have just been left out, I didn't want to litter classes with variables whos use was completely unknown (I'm not a complete monster). 

# Motivation
The library was created for [another project](https://github.com/Killeroo/SC4Cartographer) that would create maps from savegame. I couldn't find any good or usable code for DBPF parsing written in C# so I used the incredibly useful documentation over at [SC4Devotion](https://wiki.sc4devotion.com/index.php?title=Savegame) to decode and write a general parser for these saves.

Along the way it also became neccessary to implement the QFS/RefPak compression algorithm used to pack files in the savegames. This proved particularly troublesome as all source code for mods that I could find seemed to use the [same](https://github.com/wouanagaine/SC4Mapper-2013/blob/master/Modules/qfs.c) [algorithm](https://github.com/sebamarynissen/sc4/blob/master/src/decompress.cpp) for decompression. So instead of finding a way to compile and link the c implementation to the algorithm (I did try) I decided to [port it over](https://github.com/Killeroo/SC4Parser/blob/master/SC4Parser/Compression/QFS.cs) (thanks Denis Auroux!).

Hopefully some more accessible source code for parsing and decoding these files will help someone like me who just wants to write a small tool to open up SimCity 4 save games

# License
```
MIT License

Copyright (c) 2022 Matthew Carney

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
