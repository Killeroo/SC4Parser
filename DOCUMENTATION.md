<a name='assembly'></a>
# SC4Parser

## Contents

- [Building](#T-SC4Parser-DataStructures-Building 'SC4Parser.DataStructures.Building')
  - [AppearanceFlag](#P-SC4Parser-DataStructures-Building-AppearanceFlag 'SC4Parser.DataStructures.Building.AppearanceFlag')
  - [CRC](#P-SC4Parser-DataStructures-Building-CRC 'SC4Parser.DataStructures.Building.CRC')
  - [GroupID](#P-SC4Parser-DataStructures-Building-GroupID 'SC4Parser.DataStructures.Building.GroupID')
  - [InstanceID](#P-SC4Parser-DataStructures-Building-InstanceID 'SC4Parser.DataStructures.Building.InstanceID')
  - [InstanceIDOnAppearance](#P-SC4Parser-DataStructures-Building-InstanceIDOnAppearance 'SC4Parser.DataStructures.Building.InstanceIDOnAppearance')
  - [MajorVersion](#P-SC4Parser-DataStructures-Building-MajorVersion 'SC4Parser.DataStructures.Building.MajorVersion')
  - [MaxCoordinateX](#P-SC4Parser-DataStructures-Building-MaxCoordinateX 'SC4Parser.DataStructures.Building.MaxCoordinateX')
  - [MaxCoordinateY](#P-SC4Parser-DataStructures-Building-MaxCoordinateY 'SC4Parser.DataStructures.Building.MaxCoordinateY')
  - [MaxCoordinateZ](#P-SC4Parser-DataStructures-Building-MaxCoordinateZ 'SC4Parser.DataStructures.Building.MaxCoordinateZ')
  - [MaxTractX](#P-SC4Parser-DataStructures-Building-MaxTractX 'SC4Parser.DataStructures.Building.MaxTractX')
  - [MaxTractZ](#P-SC4Parser-DataStructures-Building-MaxTractZ 'SC4Parser.DataStructures.Building.MaxTractZ')
  - [Memory](#P-SC4Parser-DataStructures-Building-Memory 'SC4Parser.DataStructures.Building.Memory')
  - [MinCoordinateX](#P-SC4Parser-DataStructures-Building-MinCoordinateX 'SC4Parser.DataStructures.Building.MinCoordinateX')
  - [MinCoordinateY](#P-SC4Parser-DataStructures-Building-MinCoordinateY 'SC4Parser.DataStructures.Building.MinCoordinateY')
  - [MinCoordinateZ](#P-SC4Parser-DataStructures-Building-MinCoordinateZ 'SC4Parser.DataStructures.Building.MinCoordinateZ')
  - [MinTractX](#P-SC4Parser-DataStructures-Building-MinTractX 'SC4Parser.DataStructures.Building.MinTractX')
  - [MinTractZ](#P-SC4Parser-DataStructures-Building-MinTractZ 'SC4Parser.DataStructures.Building.MinTractZ')
  - [MinorVersion](#P-SC4Parser-DataStructures-Building-MinorVersion 'SC4Parser.DataStructures.Building.MinorVersion')
  - [Offset](#P-SC4Parser-DataStructures-Building-Offset 'SC4Parser.DataStructures.Building.Offset')
  - [Orientation](#P-SC4Parser-DataStructures-Building-Orientation 'SC4Parser.DataStructures.Building.Orientation')
  - [SaveGamePropertyCount](#P-SC4Parser-DataStructures-Building-SaveGamePropertyCount 'SC4Parser.DataStructures.Building.SaveGamePropertyCount')
  - [SaveGamePropertyEntries](#P-SC4Parser-DataStructures-Building-SaveGamePropertyEntries 'SC4Parser.DataStructures.Building.SaveGamePropertyEntries')
  - [ScaffoldingHeight](#P-SC4Parser-DataStructures-Building-ScaffoldingHeight 'SC4Parser.DataStructures.Building.ScaffoldingHeight')
  - [Size](#P-SC4Parser-DataStructures-Building-Size 'SC4Parser.DataStructures.Building.Size')
  - [TGI](#P-SC4Parser-DataStructures-Building-TGI 'SC4Parser.DataStructures.Building.TGI')
  - [TractSizeX](#P-SC4Parser-DataStructures-Building-TractSizeX 'SC4Parser.DataStructures.Building.TractSizeX')
  - [TractSizeZ](#P-SC4Parser-DataStructures-Building-TractSizeZ 'SC4Parser.DataStructures.Building.TractSizeZ')
  - [TypeID](#P-SC4Parser-DataStructures-Building-TypeID 'SC4Parser.DataStructures.Building.TypeID')
  - [Unknown1](#P-SC4Parser-DataStructures-Building-Unknown1 'SC4Parser.DataStructures.Building.Unknown1')
  - [Unknown2](#P-SC4Parser-DataStructures-Building-Unknown2 'SC4Parser.DataStructures.Building.Unknown2')
  - [ZotWord](#P-SC4Parser-DataStructures-Building-ZotWord 'SC4Parser.DataStructures.Building.ZotWord')
  - [x278128A0](#P-SC4Parser-DataStructures-Building-x278128A0 'SC4Parser.DataStructures.Building.x278128A0')
  - [Dump()](#M-SC4Parser-DataStructures-Building-Dump 'SC4Parser.DataStructures.Building.Dump')
  - [Parse(buffer,offset)](#M-SC4Parser-DataStructures-Building-Parse-System-Byte[],System-UInt32- 'SC4Parser.DataStructures.Building.Parse(System.Byte[],System.UInt32)')
- [BuildingSubfile](#T-SC4Parser-Subfiles-BuildingSubfile 'SC4Parser.Subfiles.BuildingSubfile')
  - [Buildings](#P-SC4Parser-Subfiles-BuildingSubfile-Buildings 'SC4Parser.Subfiles.BuildingSubfile.Buildings')
  - [Dump()](#M-SC4Parser-Subfiles-BuildingSubfile-Dump 'SC4Parser.Subfiles.BuildingSubfile.Dump')
  - [Parse(buffer,size)](#M-SC4Parser-Subfiles-BuildingSubfile-Parse-System-Byte[],System-Int32- 'SC4Parser.Subfiles.BuildingSubfile.Parse(System.Byte[],System.Int32)')
- [ConsoleLogger](#T-SC4Parser-Logging-ConsoleLogger 'SC4Parser.Logging.ConsoleLogger')
- [Constants](#T-SC4Parser-Constants 'SC4Parser.Constants')
  - [BUILDING_SUBFILE_TYPE](#F-SC4Parser-Constants-BUILDING_SUBFILE_TYPE 'SC4Parser.Constants.BUILDING_SUBFILE_TYPE')
  - [DATABASE_DIRECTORY_FILE_TGI](#F-SC4Parser-Constants-DATABASE_DIRECTORY_FILE_TGI 'SC4Parser.Constants.DATABASE_DIRECTORY_FILE_TGI')
  - [LARGE_CITY_TILE_COUNT](#F-SC4Parser-Constants-LARGE_CITY_TILE_COUNT 'SC4Parser.Constants.LARGE_CITY_TILE_COUNT')
  - [LOT_SUBFILE_TYPE](#F-SC4Parser-Constants-LOT_SUBFILE_TYPE 'SC4Parser.Constants.LOT_SUBFILE_TYPE')
  - [LOT_WEALTH_HIGH](#F-SC4Parser-Constants-LOT_WEALTH_HIGH 'SC4Parser.Constants.LOT_WEALTH_HIGH')
  - [LOT_WEALTH_LOW](#F-SC4Parser-Constants-LOT_WEALTH_LOW 'SC4Parser.Constants.LOT_WEALTH_LOW')
  - [LOT_WEALTH_MEDIUM](#F-SC4Parser-Constants-LOT_WEALTH_MEDIUM 'SC4Parser.Constants.LOT_WEALTH_MEDIUM')
  - [LOT_WEALTH_NONE](#F-SC4Parser-Constants-LOT_WEALTH_NONE 'SC4Parser.Constants.LOT_WEALTH_NONE')
  - [LOT_ZONE_TYPE_AIRPORT](#F-SC4Parser-Constants-LOT_ZONE_TYPE_AIRPORT 'SC4Parser.Constants.LOT_ZONE_TYPE_AIRPORT')
  - [LOT_ZONE_TYPE_COMMERCIAL_HIGH](#F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_HIGH 'SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_HIGH')
  - [LOT_ZONE_TYPE_COMMERCIAL_LOW](#F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_LOW 'SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_LOW')
  - [LOT_ZONE_TYPE_COMMERCIAL_MEDIUM](#F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_MEDIUM 'SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_MEDIUM')
  - [LOT_ZONE_TYPE_INDUSTRIAL_HIGH](#F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_HIGH 'SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_HIGH')
  - [LOT_ZONE_TYPE_INDUSTRIAL_LOW](#F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_LOW 'SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_LOW')
  - [LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM](#F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM 'SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM')
  - [LOT_ZONE_TYPE_MILITARY](#F-SC4Parser-Constants-LOT_ZONE_TYPE_MILITARY 'SC4Parser.Constants.LOT_ZONE_TYPE_MILITARY')
  - [LOT_ZONE_TYPE_PLOPPED_BUILDING](#F-SC4Parser-Constants-LOT_ZONE_TYPE_PLOPPED_BUILDING 'SC4Parser.Constants.LOT_ZONE_TYPE_PLOPPED_BUILDING')
  - [LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT](#F-SC4Parser-Constants-LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT 'SC4Parser.Constants.LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT')
  - [LOT_ZONE_TYPE_RESIDENTIAL_HIGH](#F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_HIGH 'SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_HIGH')
  - [LOT_ZONE_TYPE_RESIDENTIAL_LOW](#F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_LOW 'SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_LOW')
  - [LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM](#F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM 'SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM')
  - [LOT_ZONE_TYPE_SEAPORT](#F-SC4Parser-Constants-LOT_ZONE_TYPE_SEAPORT 'SC4Parser.Constants.LOT_ZONE_TYPE_SEAPORT')
  - [LOT_ZONE_TYPE_SPACEPORT](#F-SC4Parser-Constants-LOT_ZONE_TYPE_SPACEPORT 'SC4Parser.Constants.LOT_ZONE_TYPE_SPACEPORT')
  - [LOT_ZONE_TYPE_STRINGS](#F-SC4Parser-Constants-LOT_ZONE_TYPE_STRINGS 'SC4Parser.Constants.LOT_ZONE_TYPE_STRINGS')
  - [LOT_ZONE_WEALTH_STRINGS](#F-SC4Parser-Constants-LOT_ZONE_WEALTH_STRINGS 'SC4Parser.Constants.LOT_ZONE_WEALTH_STRINGS')
  - [MEDIUM_CITY_TILE_COUNT](#F-SC4Parser-Constants-MEDIUM_CITY_TILE_COUNT 'SC4Parser.Constants.MEDIUM_CITY_TILE_COUNT')
  - [ORIENTATION_EAST](#F-SC4Parser-Constants-ORIENTATION_EAST 'SC4Parser.Constants.ORIENTATION_EAST')
  - [ORIENTATION_NORTH](#F-SC4Parser-Constants-ORIENTATION_NORTH 'SC4Parser.Constants.ORIENTATION_NORTH')
  - [ORIENTATION_SOUTH](#F-SC4Parser-Constants-ORIENTATION_SOUTH 'SC4Parser.Constants.ORIENTATION_SOUTH')
  - [ORIENTATION_STRINGS](#F-SC4Parser-Constants-ORIENTATION_STRINGS 'SC4Parser.Constants.ORIENTATION_STRINGS')
  - [ORIENTATION_WEST](#F-SC4Parser-Constants-ORIENTATION_WEST 'SC4Parser.Constants.ORIENTATION_WEST')
  - [REGION_VIEW_SUBFILE_TGI](#F-SC4Parser-Constants-REGION_VIEW_SUBFILE_TGI 'SC4Parser.Constants.REGION_VIEW_SUBFILE_TGI')
  - [SIGPROP_DATATYPE_TYPES](#F-SC4Parser-Constants-SIGPROP_DATATYPE_TYPES 'SC4Parser.Constants.SIGPROP_DATATYPE_TYPES')
  - [SIGPROP_DATATYPE_TYPE_STRINGS](#F-SC4Parser-Constants-SIGPROP_DATATYPE_TYPE_STRINGS 'SC4Parser.Constants.SIGPROP_DATATYPE_TYPE_STRINGS')
  - [SMALL_CITY_TILE_COUNT](#F-SC4Parser-Constants-SMALL_CITY_TILE_COUNT 'SC4Parser.Constants.SMALL_CITY_TILE_COUNT')
  - [TERRAIN_MAP_SUBFILE_TGI](#F-SC4Parser-Constants-TERRAIN_MAP_SUBFILE_TGI 'SC4Parser.Constants.TERRAIN_MAP_SUBFILE_TGI')
- [DBPFParsingException](#T-SC4Parser-DBPFParsingException 'SC4Parser.DBPFParsingException')
  - [#ctor(message,e)](#M-SC4Parser-DBPFParsingException-#ctor-System-String,System-Exception- 'SC4Parser.DBPFParsingException.#ctor(System.String,System.Exception)')
- [DatabaseDirectoryFile](#T-SC4Parser-Files-DatabaseDirectoryFile 'SC4Parser.Files.DatabaseDirectoryFile')
  - [#ctor()](#M-SC4Parser-Files-DatabaseDirectoryFile-#ctor 'SC4Parser.Files.DatabaseDirectoryFile.#ctor')
  - [#ctor(entry)](#M-SC4Parser-Files-DatabaseDirectoryFile-#ctor-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabaseDirectoryFile.#ctor(SC4Parser.DataStructures.IndexEntry)')
  - [ResourceCount](#P-SC4Parser-Files-DatabaseDirectoryFile-ResourceCount 'SC4Parser.Files.DatabaseDirectoryFile.ResourceCount')
  - [Resources](#P-SC4Parser-Files-DatabaseDirectoryFile-Resources 'SC4Parser.Files.DatabaseDirectoryFile.Resources')
  - [AddResource(resource)](#M-SC4Parser-Files-DatabaseDirectoryFile-AddResource-SC4Parser-DataStructures-DatabaseDirectoryResource- 'SC4Parser.Files.DatabaseDirectoryFile.AddResource(SC4Parser.DataStructures.DatabaseDirectoryResource)')
  - [Dump()](#M-SC4Parser-Files-DatabaseDirectoryFile-Dump 'SC4Parser.Files.DatabaseDirectoryFile.Dump')
- [DatabaseDirectoryResource](#T-SC4Parser-DataStructures-DatabaseDirectoryResource 'SC4Parser.DataStructures.DatabaseDirectoryResource')
  - [DecompressedFileSize](#P-SC4Parser-DataStructures-DatabaseDirectoryResource-DecompressedFileSize 'SC4Parser.DataStructures.DatabaseDirectoryResource.DecompressedFileSize')
  - [TGI](#P-SC4Parser-DataStructures-DatabaseDirectoryResource-TGI 'SC4Parser.DataStructures.DatabaseDirectoryResource.TGI')
  - [Dump()](#M-SC4Parser-DataStructures-DatabaseDirectoryResource-Dump 'SC4Parser.DataStructures.DatabaseDirectoryResource.Dump')
  - [Parse(buffer)](#M-SC4Parser-DataStructures-DatabaseDirectoryResource-Parse-System-Byte[]- 'SC4Parser.DataStructures.DatabaseDirectoryResource.Parse(System.Byte[])')
- [DatabaseDirectoryResourceNotFoundException](#T-SC4Parser-DatabaseDirectoryResourceNotFoundException 'SC4Parser.DatabaseDirectoryResourceNotFoundException')
- [DatabasePackedFile](#T-SC4Parser-Files-DatabasePackedFile 'SC4Parser.Files.DatabasePackedFile')
  - [#ctor()](#M-SC4Parser-Files-DatabasePackedFile-#ctor 'SC4Parser.Files.DatabasePackedFile.#ctor')
  - [#ctor(databasePackedFile)](#M-SC4Parser-Files-DatabasePackedFile-#ctor-SC4Parser-Files-DatabasePackedFile- 'SC4Parser.Files.DatabasePackedFile.#ctor(SC4Parser.Files.DatabasePackedFile)')
  - [#ctor(path)](#M-SC4Parser-Files-DatabasePackedFile-#ctor-System-String- 'SC4Parser.Files.DatabasePackedFile.#ctor(System.String)')
  - [DBDFFile](#P-SC4Parser-Files-DatabasePackedFile-DBDFFile 'SC4Parser.Files.DatabasePackedFile.DBDFFile')
  - [FilePath](#P-SC4Parser-Files-DatabasePackedFile-FilePath 'SC4Parser.Files.DatabasePackedFile.FilePath')
  - [Header](#P-SC4Parser-Files-DatabasePackedFile-Header 'SC4Parser.Files.DatabasePackedFile.Header')
  - [IndexEntries](#P-SC4Parser-Files-DatabasePackedFile-IndexEntries 'SC4Parser.Files.DatabasePackedFile.IndexEntries')
  - [RawFile](#P-SC4Parser-Files-DatabasePackedFile-RawFile 'SC4Parser.Files.DatabasePackedFile.RawFile')
  - [Dump()](#M-SC4Parser-Files-DatabasePackedFile-Dump 'SC4Parser.Files.DatabasePackedFile.Dump')
  - [FindDatabaseDirectoryResource(entry)](#M-SC4Parser-Files-DatabasePackedFile-FindDatabaseDirectoryResource-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.FindDatabaseDirectoryResource(SC4Parser.DataStructures.IndexEntry)')
  - [FindIndexEntry(tgi)](#M-SC4Parser-Files-DatabasePackedFile-FindIndexEntry-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Files.DatabasePackedFile.FindIndexEntry(SC4Parser.Types.TypeGroupInstance)')
  - [FindIndexEntryWithType(type_id)](#M-SC4Parser-Files-DatabasePackedFile-FindIndexEntryWithType-System-String- 'SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(System.String)')
  - [IsIndexEntryCompressed(entry)](#M-SC4Parser-Files-DatabasePackedFile-IsIndexEntryCompressed-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.IsIndexEntryCompressed(SC4Parser.DataStructures.IndexEntry)')
  - [LoadIndexEntry(tgi)](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.Types.TypeGroupInstance)')
  - [LoadIndexEntry(entry)](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.DataStructures.IndexEntry)')
  - [LoadIndexEntryRaw(entry)](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntryRaw-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(SC4Parser.DataStructures.IndexEntry)')
  - [Parse(path)](#M-SC4Parser-Files-DatabasePackedFile-Parse-System-String- 'SC4Parser.Files.DatabasePackedFile.Parse(System.String)')
  - [ReadRawIndexEntryData(entry)](#M-SC4Parser-Files-DatabasePackedFile-ReadRawIndexEntryData-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.ReadRawIndexEntryData(SC4Parser.DataStructures.IndexEntry)')
- [DatabasePackedFileHeader](#T-SC4Parser-DataStructures-DatabasePackedFileHeader 'SC4Parser.DataStructures.DatabasePackedFileHeader')
  - [DateCreated](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-DateCreated 'SC4Parser.DataStructures.DatabasePackedFileHeader.DateCreated')
  - [DateModified](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-DateModified 'SC4Parser.DataStructures.DatabasePackedFileHeader.DateModified')
  - [FirstIndexOffset](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-FirstIndexOffset 'SC4Parser.DataStructures.DatabasePackedFileHeader.FirstIndexOffset')
  - [HoleCount](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleCount 'SC4Parser.DataStructures.DatabasePackedFileHeader.HoleCount')
  - [HoleOffset](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleOffset 'SC4Parser.DataStructures.DatabasePackedFileHeader.HoleOffset')
  - [HoleSize](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleSize 'SC4Parser.DataStructures.DatabasePackedFileHeader.HoleSize')
  - [Identifier](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-Identifier 'SC4Parser.DataStructures.DatabasePackedFileHeader.Identifier')
  - [IndexCount](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexCount 'SC4Parser.DataStructures.DatabasePackedFileHeader.IndexCount')
  - [IndexMajorVersion](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexMajorVersion 'SC4Parser.DataStructures.DatabasePackedFileHeader.IndexMajorVersion')
  - [IndexMinorVersion](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexMinorVersion 'SC4Parser.DataStructures.DatabasePackedFileHeader.IndexMinorVersion')
  - [IndexSize](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexSize 'SC4Parser.DataStructures.DatabasePackedFileHeader.IndexSize')
  - [MajorVersion](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-MajorVersion 'SC4Parser.DataStructures.DatabasePackedFileHeader.MajorVersion')
  - [MinorVersion](#P-SC4Parser-DataStructures-DatabasePackedFileHeader-MinorVersion 'SC4Parser.DataStructures.DatabasePackedFileHeader.MinorVersion')
  - [Dump()](#M-SC4Parser-DataStructures-DatabasePackedFileHeader-Dump 'SC4Parser.DataStructures.DatabasePackedFileHeader.Dump')
  - [Parse(buffer)](#M-SC4Parser-DataStructures-DatabasePackedFileHeader-Parse-System-Byte[]- 'SC4Parser.DataStructures.DatabasePackedFileHeader.Parse(System.Byte[])')
- [ILogger](#T-SC4Parser-Logging-ILogger 'SC4Parser.Logging.ILogger')
  - [EnableChannel(level)](#M-SC4Parser-Logging-ILogger-EnableChannel-SC4Parser-Logging-LogLevel- 'SC4Parser.Logging.ILogger.EnableChannel(SC4Parser.Logging.LogLevel)')
  - [Log(level,format,args)](#M-SC4Parser-Logging-ILogger-Log-SC4Parser-Logging-LogLevel,System-String,System-Object[]- 'SC4Parser.Logging.ILogger.Log(SC4Parser.Logging.LogLevel,System.String,System.Object[])')
- [IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry')
  - [#ctor()](#M-SC4Parser-DataStructures-IndexEntry-#ctor 'SC4Parser.DataStructures.IndexEntry.#ctor')
  - [#ctor(entry)](#M-SC4Parser-DataStructures-IndexEntry-#ctor-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.DataStructures.IndexEntry.#ctor(SC4Parser.DataStructures.IndexEntry)')
  - [#ctor(tgi,location,size)](#M-SC4Parser-DataStructures-IndexEntry-#ctor-SC4Parser-Types-TypeGroupInstance,System-UInt32,System-UInt32- 'SC4Parser.DataStructures.IndexEntry.#ctor(SC4Parser.Types.TypeGroupInstance,System.UInt32,System.UInt32)')
  - [FileLocation](#P-SC4Parser-DataStructures-IndexEntry-FileLocation 'SC4Parser.DataStructures.IndexEntry.FileLocation')
  - [FileSize](#P-SC4Parser-DataStructures-IndexEntry-FileSize 'SC4Parser.DataStructures.IndexEntry.FileSize')
  - [TGI](#P-SC4Parser-DataStructures-IndexEntry-TGI 'SC4Parser.DataStructures.IndexEntry.TGI')
  - [Dump()](#M-SC4Parser-DataStructures-IndexEntry-Dump 'SC4Parser.DataStructures.IndexEntry.Dump')
  - [Parse(buffer)](#M-SC4Parser-DataStructures-IndexEntry-Parse-System-Byte[]- 'SC4Parser.DataStructures.IndexEntry.Parse(System.Byte[])')
- [IndexEntryLoadingException](#T-SC4Parser-IndexEntryLoadingException 'SC4Parser.IndexEntryLoadingException')
  - [#ctor(message)](#M-SC4Parser-IndexEntryLoadingException-#ctor-System-String- 'SC4Parser.IndexEntryLoadingException.#ctor(System.String)')
  - [#ctor(message,e)](#M-SC4Parser-IndexEntryLoadingException-#ctor-System-String,System-Exception- 'SC4Parser.IndexEntryLoadingException.#ctor(System.String,System.Exception)')
- [IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException')
- [LogLevel](#T-SC4Parser-Logging-LogLevel 'SC4Parser.Logging.LogLevel')
  - [Debug](#F-SC4Parser-Logging-LogLevel-Debug 'SC4Parser.Logging.LogLevel.Debug')
  - [Error](#F-SC4Parser-Logging-LogLevel-Error 'SC4Parser.Logging.LogLevel.Error')
  - [Fatal](#F-SC4Parser-Logging-LogLevel-Fatal 'SC4Parser.Logging.LogLevel.Fatal')
  - [Info](#F-SC4Parser-Logging-LogLevel-Info 'SC4Parser.Logging.LogLevel.Info')
  - [Warning](#F-SC4Parser-Logging-LogLevel-Warning 'SC4Parser.Logging.LogLevel.Warning')
- [Logger](#T-SC4Parser-Logging-Logger 'SC4Parser.Logging.Logger')
  - [AddLogOutput(logOutput)](#M-SC4Parser-Logging-Logger-AddLogOutput-SC4Parser-Logging-ILogger- 'SC4Parser.Logging.Logger.AddLogOutput(SC4Parser.Logging.ILogger)')
  - [EnableLogChannel(level)](#M-SC4Parser-Logging-Logger-EnableLogChannel-SC4Parser-Logging-LogLevel- 'SC4Parser.Logging.Logger.EnableLogChannel(SC4Parser.Logging.LogLevel)')
  - [Log(level,format,args)](#M-SC4Parser-Logging-Logger-Log-SC4Parser-Logging-LogLevel,System-String,System-Object[]- 'SC4Parser.Logging.Logger.Log(SC4Parser.Logging.LogLevel,System.String,System.Object[])')
- [Lot](#T-SC4Parser-DataStructures-Lot 'SC4Parser.DataStructures.Lot')
  - [BuildingInstanceID](#P-SC4Parser-DataStructures-Lot-BuildingInstanceID 'SC4Parser.DataStructures.Lot.BuildingInstanceID')
  - [CRC](#P-SC4Parser-DataStructures-Lot-CRC 'SC4Parser.DataStructures.Lot.CRC')
  - [CommuteTileX](#P-SC4Parser-DataStructures-Lot-CommuteTileX 'SC4Parser.DataStructures.Lot.CommuteTileX')
  - [CommuteTileZ](#P-SC4Parser-DataStructures-Lot-CommuteTileZ 'SC4Parser.DataStructures.Lot.CommuteTileZ')
  - [DateLotAppeared](#P-SC4Parser-DataStructures-Lot-DateLotAppeared 'SC4Parser.DataStructures.Lot.DateLotAppeared')
  - [FlagByte1](#P-SC4Parser-DataStructures-Lot-FlagByte1 'SC4Parser.DataStructures.Lot.FlagByte1')
  - [FlagByte2](#P-SC4Parser-DataStructures-Lot-FlagByte2 'SC4Parser.DataStructures.Lot.FlagByte2')
  - [FlagByte3](#P-SC4Parser-DataStructures-Lot-FlagByte3 'SC4Parser.DataStructures.Lot.FlagByte3')
  - [LotInstanceID](#P-SC4Parser-DataStructures-Lot-LotInstanceID 'SC4Parser.DataStructures.Lot.LotInstanceID')
  - [MajorVersion](#P-SC4Parser-DataStructures-Lot-MajorVersion 'SC4Parser.DataStructures.Lot.MajorVersion')
  - [MaxTileX](#P-SC4Parser-DataStructures-Lot-MaxTileX 'SC4Parser.DataStructures.Lot.MaxTileX')
  - [MaxTileZ](#P-SC4Parser-DataStructures-Lot-MaxTileZ 'SC4Parser.DataStructures.Lot.MaxTileZ')
  - [Memory](#P-SC4Parser-DataStructures-Lot-Memory 'SC4Parser.DataStructures.Lot.Memory')
  - [MinTileX](#P-SC4Parser-DataStructures-Lot-MinTileX 'SC4Parser.DataStructures.Lot.MinTileX')
  - [MinTileZ](#P-SC4Parser-DataStructures-Lot-MinTileZ 'SC4Parser.DataStructures.Lot.MinTileZ')
  - [Offset](#P-SC4Parser-DataStructures-Lot-Offset 'SC4Parser.DataStructures.Lot.Offset')
  - [Orientation](#P-SC4Parser-DataStructures-Lot-Orientation 'SC4Parser.DataStructures.Lot.Orientation')
  - [PositionY](#P-SC4Parser-DataStructures-Lot-PositionY 'SC4Parser.DataStructures.Lot.PositionY')
  - [Size](#P-SC4Parser-DataStructures-Lot-Size 'SC4Parser.DataStructures.Lot.Size')
  - [SizeX](#P-SC4Parser-DataStructures-Lot-SizeX 'SC4Parser.DataStructures.Lot.SizeX')
  - [SizeZ](#P-SC4Parser-DataStructures-Lot-SizeZ 'SC4Parser.DataStructures.Lot.SizeZ')
  - [Slope1Y](#P-SC4Parser-DataStructures-Lot-Slope1Y 'SC4Parser.DataStructures.Lot.Slope1Y')
  - [Slope2Y](#P-SC4Parser-DataStructures-Lot-Slope2Y 'SC4Parser.DataStructures.Lot.Slope2Y')
  - [Unknown](#P-SC4Parser-DataStructures-Lot-Unknown 'SC4Parser.DataStructures.Lot.Unknown')
  - [ZoneType](#P-SC4Parser-DataStructures-Lot-ZoneType 'SC4Parser.DataStructures.Lot.ZoneType')
  - [ZoneWealth](#P-SC4Parser-DataStructures-Lot-ZoneWealth 'SC4Parser.DataStructures.Lot.ZoneWealth')
  - [Dump()](#M-SC4Parser-DataStructures-Lot-Dump 'SC4Parser.DataStructures.Lot.Dump')
  - [Parse(buffer,offset)](#M-SC4Parser-DataStructures-Lot-Parse-System-Byte[],System-UInt32- 'SC4Parser.DataStructures.Lot.Parse(System.Byte[],System.UInt32)')
- [LotSubfile](#T-SC4Parser-Subfiles-LotSubfile 'SC4Parser.Subfiles.LotSubfile')
  - [Lots](#F-SC4Parser-Subfiles-LotSubfile-Lots 'SC4Parser.Subfiles.LotSubfile.Lots')
  - [Dump()](#M-SC4Parser-Subfiles-LotSubfile-Dump 'SC4Parser.Subfiles.LotSubfile.Dump')
  - [Parse(buffer,size)](#M-SC4Parser-Subfiles-LotSubfile-Parse-System-Byte[],System-Int32- 'SC4Parser.Subfiles.LotSubfile.Parse(System.Byte[],System.Int32)')
- [QFS](#T-SC4Parser-Compression-QFS 'SC4Parser.Compression.QFS')
  - [LZCompliantCopy(source,sourceOffset,destination,destinationOffset,length)](#M-SC4Parser-Compression-QFS-LZCompliantCopy-System-Byte[]@,System-Int32,System-Byte[]@,System-Int32,System-Int32- 'SC4Parser.Compression.QFS.LZCompliantCopy(System.Byte[]@,System.Int32,System.Byte[]@,System.Int32,System.Int32)')
  - [UncompressData(data)](#M-SC4Parser-Compression-QFS-UncompressData-System-Byte[]- 'SC4Parser.Compression.QFS.UncompressData(System.Byte[])')
- [QFSDecompressionException](#T-SC4Parser-QFSDecompressionException 'SC4Parser.QFSDecompressionException')
  - [#ctor(message,e)](#M-SC4Parser-QFSDecompressionException-#ctor-System-String,System-Exception- 'SC4Parser.QFSDecompressionException.#ctor(System.String,System.Exception)')
- [RegionViewSubfile](#T-SC4Parser-Subfiles-RegionViewSubfile 'SC4Parser.Subfiles.RegionViewSubfile')
  - [CitySizeX](#P-SC4Parser-Subfiles-RegionViewSubfile-CitySizeX 'SC4Parser.Subfiles.RegionViewSubfile.CitySizeX')
  - [CitySizeY](#P-SC4Parser-Subfiles-RegionViewSubfile-CitySizeY 'SC4Parser.Subfiles.RegionViewSubfile.CitySizeY')
  - [CommercialPopulation](#P-SC4Parser-Subfiles-RegionViewSubfile-CommercialPopulation 'SC4Parser.Subfiles.RegionViewSubfile.CommercialPopulation')
  - [IndustrialPopulation](#P-SC4Parser-Subfiles-RegionViewSubfile-IndustrialPopulation 'SC4Parser.Subfiles.RegionViewSubfile.IndustrialPopulation')
  - [MajorVersion](#P-SC4Parser-Subfiles-RegionViewSubfile-MajorVersion 'SC4Parser.Subfiles.RegionViewSubfile.MajorVersion')
  - [MinorVersion](#P-SC4Parser-Subfiles-RegionViewSubfile-MinorVersion 'SC4Parser.Subfiles.RegionViewSubfile.MinorVersion')
  - [ResidentialPopulation](#P-SC4Parser-Subfiles-RegionViewSubfile-ResidentialPopulation 'SC4Parser.Subfiles.RegionViewSubfile.ResidentialPopulation')
  - [TileXLocation](#P-SC4Parser-Subfiles-RegionViewSubfile-TileXLocation 'SC4Parser.Subfiles.RegionViewSubfile.TileXLocation')
  - [TileYLocation](#P-SC4Parser-Subfiles-RegionViewSubfile-TileYLocation 'SC4Parser.Subfiles.RegionViewSubfile.TileYLocation')
  - [Dump()](#M-SC4Parser-Subfiles-RegionViewSubfile-Dump 'SC4Parser.Subfiles.RegionViewSubfile.Dump')
  - [Parse(buffer)](#M-SC4Parser-Subfiles-RegionViewSubfile-Parse-System-Byte[]- 'SC4Parser.Subfiles.RegionViewSubfile.Parse(System.Byte[])')
- [SC4SaveFile](#T-SC4Parser-Files-SC4SaveFile 'SC4Parser.Files.SC4SaveFile')
  - [#ctor(path)](#M-SC4Parser-Files-SC4SaveFile-#ctor-System-String- 'SC4Parser.Files.SC4SaveFile.#ctor(System.String)')
  - [GetBuildingSubfile()](#M-SC4Parser-Files-SC4SaveFile-GetBuildingSubfile 'SC4Parser.Files.SC4SaveFile.GetBuildingSubfile')
  - [GetLotSubfile()](#M-SC4Parser-Files-SC4SaveFile-GetLotSubfile 'SC4Parser.Files.SC4SaveFile.GetLotSubfile')
  - [GetRegionViewSubfile()](#M-SC4Parser-Files-SC4SaveFile-GetRegionViewSubfile 'SC4Parser.Files.SC4SaveFile.GetRegionViewSubfile')
  - [GetTerrainMapSubfile()](#M-SC4Parser-Files-SC4SaveFile-GetTerrainMapSubfile 'SC4Parser.Files.SC4SaveFile.GetTerrainMapSubfile')
- [SaveGameProperty](#T-SC4Parser-DataStructures-SaveGameProperty 'SC4Parser.DataStructures.SaveGameProperty')
  - [Data](#P-SC4Parser-DataStructures-SaveGameProperty-Data 'SC4Parser.DataStructures.SaveGameProperty.Data')
  - [DataRepeatedCount](#P-SC4Parser-DataStructures-SaveGameProperty-DataRepeatedCount 'SC4Parser.DataStructures.SaveGameProperty.DataRepeatedCount')
  - [DataType](#P-SC4Parser-DataStructures-SaveGameProperty-DataType 'SC4Parser.DataStructures.SaveGameProperty.DataType')
  - [KeyType](#P-SC4Parser-DataStructures-SaveGameProperty-KeyType 'SC4Parser.DataStructures.SaveGameProperty.KeyType')
  - [PropertyNameValue](#P-SC4Parser-DataStructures-SaveGameProperty-PropertyNameValue 'SC4Parser.DataStructures.SaveGameProperty.PropertyNameValue')
  - [PropertyNameValueCopy](#P-SC4Parser-DataStructures-SaveGameProperty-PropertyNameValueCopy 'SC4Parser.DataStructures.SaveGameProperty.PropertyNameValueCopy')
  - [Unknown1](#P-SC4Parser-DataStructures-SaveGameProperty-Unknown1 'SC4Parser.DataStructures.SaveGameProperty.Unknown1')
  - [Unknown2](#P-SC4Parser-DataStructures-SaveGameProperty-Unknown2 'SC4Parser.DataStructures.SaveGameProperty.Unknown2')
  - [Dump()](#M-SC4Parser-DataStructures-SaveGameProperty-Dump 'SC4Parser.DataStructures.SaveGameProperty.Dump')
  - [ExtractFromBuffer(buffer,count,offset)](#M-SC4Parser-DataStructures-SaveGameProperty-ExtractFromBuffer-System-Byte[],System-UInt32,System-UInt32@- 'SC4Parser.DataStructures.SaveGameProperty.ExtractFromBuffer(System.Byte[],System.UInt32,System.UInt32@)')
  - [Parse(buffer,offset)](#M-SC4Parser-DataStructures-SaveGameProperty-Parse-System-Byte[],System-Int32- 'SC4Parser.DataStructures.SaveGameProperty.Parse(System.Byte[],System.Int32)')
- [SubfileNotFoundException](#T-SC4Parser-SubfileNotFoundException 'SC4Parser.SubfileNotFoundException')
  - [#ctor(message)](#M-SC4Parser-SubfileNotFoundException-#ctor-System-String- 'SC4Parser.SubfileNotFoundException.#ctor(System.String)')
  - [#ctor(message,e)](#M-SC4Parser-SubfileNotFoundException-#ctor-System-String,System-Exception- 'SC4Parser.SubfileNotFoundException.#ctor(System.String,System.Exception)')
- [TerrainMapSubfile](#T-SC4Parser-Subfiles-TerrainMapSubfile 'SC4Parser.Subfiles.TerrainMapSubfile')
  - [MajorVersion](#P-SC4Parser-Subfiles-TerrainMapSubfile-MajorVersion 'SC4Parser.Subfiles.TerrainMapSubfile.MajorVersion')
  - [Map](#P-SC4Parser-Subfiles-TerrainMapSubfile-Map 'SC4Parser.Subfiles.TerrainMapSubfile.Map')
  - [SizeX](#P-SC4Parser-Subfiles-TerrainMapSubfile-SizeX 'SC4Parser.Subfiles.TerrainMapSubfile.SizeX')
  - [SizeY](#P-SC4Parser-Subfiles-TerrainMapSubfile-SizeY 'SC4Parser.Subfiles.TerrainMapSubfile.SizeY')
  - [Dump()](#M-SC4Parser-Subfiles-TerrainMapSubfile-Dump 'SC4Parser.Subfiles.TerrainMapSubfile.Dump')
  - [Parse(buffer,xSize,ySize)](#M-SC4Parser-Subfiles-TerrainMapSubfile-Parse-System-Byte[],System-UInt32,System-UInt32- 'SC4Parser.Subfiles.TerrainMapSubfile.Parse(System.Byte[],System.UInt32,System.UInt32)')
- [TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance')
  - [#ctor(type,group,instance)](#M-SC4Parser-Types-TypeGroupInstance-#ctor-System-UInt32,System-UInt32,System-UInt32- 'SC4Parser.Types.TypeGroupInstance.#ctor(System.UInt32,System.UInt32,System.UInt32)')
  - [#ctor(type_hex,group_hex,instance_hex)](#M-SC4Parser-Types-TypeGroupInstance-#ctor-System-String,System-String,System-String- 'SC4Parser.Types.TypeGroupInstance.#ctor(System.String,System.String,System.String)')
  - [Group](#P-SC4Parser-Types-TypeGroupInstance-Group 'SC4Parser.Types.TypeGroupInstance.Group')
  - [Instance](#P-SC4Parser-Types-TypeGroupInstance-Instance 'SC4Parser.Types.TypeGroupInstance.Instance')
  - [Type](#P-SC4Parser-Types-TypeGroupInstance-Type 'SC4Parser.Types.TypeGroupInstance.Type')
  - [Dump()](#M-SC4Parser-Types-TypeGroupInstance-Dump 'SC4Parser.Types.TypeGroupInstance.Dump')
  - [Equals(obj)](#M-SC4Parser-Types-TypeGroupInstance-Equals-System-Object- 'SC4Parser.Types.TypeGroupInstance.Equals(System.Object)')
  - [Equals(tgi)](#M-SC4Parser-Types-TypeGroupInstance-Equals-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Types.TypeGroupInstance.Equals(SC4Parser.Types.TypeGroupInstance)')
  - [FromHex(type_hex)](#M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String- 'SC4Parser.Types.TypeGroupInstance.FromHex(System.String)')
  - [FromHex(type_hex,group_hex)](#M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String,System-String- 'SC4Parser.Types.TypeGroupInstance.FromHex(System.String,System.String)')
  - [FromHex(type_hex,group_hex,instance_hex)](#M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String,System-String,System-String- 'SC4Parser.Types.TypeGroupInstance.FromHex(System.String,System.String,System.String)')
  - [GetHashCode()](#M-SC4Parser-Types-TypeGroupInstance-GetHashCode 'SC4Parser.Types.TypeGroupInstance.GetHashCode')
  - [ToString()](#M-SC4Parser-Types-TypeGroupInstance-ToString 'SC4Parser.Types.TypeGroupInstance.ToString')
  - [op_Equality(lhs,rhs)](#M-SC4Parser-Types-TypeGroupInstance-op_Equality-SC4Parser-Types-TypeGroupInstance,SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Types.TypeGroupInstance.op_Equality(SC4Parser.Types.TypeGroupInstance,SC4Parser.Types.TypeGroupInstance)')
  - [op_Inequality(lhs,rhs)](#M-SC4Parser-Types-TypeGroupInstance-op_Inequality-SC4Parser-Types-TypeGroupInstance,SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Types.TypeGroupInstance.op_Inequality(SC4Parser.Types.TypeGroupInstance,SC4Parser.Types.TypeGroupInstance)')
- [Utils](#T-SC4Parser-Utils 'SC4Parser.Utils')
  - [SaveByteArrayToFile(data,name,path)](#M-SC4Parser-Utils-SaveByteArrayToFile-System-Byte[],System-String,System-String- 'SC4Parser.Utils.SaveByteArrayToFile(System.Byte[],System.String,System.String)')
  - [UnixTimestampToDateTime(unixTimestamp)](#M-SC4Parser-Utils-UnixTimestampToDateTime-System-Int64- 'SC4Parser.Utils.UnixTimestampToDateTime(System.Int64)')

<a name='T-SC4Parser-DataStructures-Building'></a>
## Building `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Representation of a building in Simcity 4, as it is stored in a save game

##### Example

`
            // How to read and use building data using library
            // (this is effectively what is done in SC4Save.GetBuildingSubfile())
            
            // Load save game
            SC4SaveFile savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            
            // load Building Subfile from save
            BuildingSubfile buildingSubfile = new BuildingSubfile();
            IndexEntry buildingEntry = savegame.FindIndexEntryWithType("A9BD882D")
            byte[] buildingSubfileData = savegame.LoadIndexEntry(buildingEntry.TGI);
            buildingSubfile.Parse(buildingSubfileData, buildingSubfileData.Length);
            
            // loop through buildings and print out their TGIs
            foreach (Building building in buildingsSubfile.Buildings)
            {
                Console.Writeline(building.TGI.ToString();
            }
`

##### Remarks

Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile

##### See Also

- [SC4Parser.DataStructures.Lot](#T-SC4Parser-DataStructures-Lot 'SC4Parser.DataStructures.Lot')

<a name='P-SC4Parser-DataStructures-Building-AppearanceFlag'></a>
### AppearanceFlag `property`

##### Summary

Appearance flag for building. Can be one of the following values:
    0x01 (00000001b) - Building that appears in the game (if this is off, the building has been deleted).
    0x02 (00000010b) - ? (unused).
    0x04 (00000100b) - ? (always on).
    0x08 (00001000b) - Flora
    0x40 (01000000b) - Burnt

<a name='P-SC4Parser-DataStructures-Building-CRC'></a>
### CRC `property`

##### Summary

CRC of building data

<a name='P-SC4Parser-DataStructures-Building-GroupID'></a>
### GroupID `property`

##### Summary

The Building's Group ID

<a name='P-SC4Parser-DataStructures-Building-InstanceID'></a>
### InstanceID `property`

##### Summary

The Building's Instance ID

<a name='P-SC4Parser-DataStructures-Building-InstanceIDOnAppearance'></a>
### InstanceIDOnAppearance `property`

##### Summary

Building's Instance Id when the the building appears

<a name='P-SC4Parser-DataStructures-Building-MajorVersion'></a>
### MajorVersion `property`

##### Summary

Major version of building spec

<a name='P-SC4Parser-DataStructures-Building-MaxCoordinateX'></a>
### MaxCoordinateX `property`

##### Summary

Maximum Z coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MaxCoordinateY'></a>
### MaxCoordinateY `property`

##### Summary

Maximum Y coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MaxCoordinateZ'></a>
### MaxCoordinateZ `property`

##### Summary

Maximum Z coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MaxTractX'></a>
### MaxTractX `property`

##### Summary

Maximum tract X coordinate for building

<a name='P-SC4Parser-DataStructures-Building-MaxTractZ'></a>
### MaxTractZ `property`

##### Summary

Maximum tract Z coordinate for building

<a name='P-SC4Parser-DataStructures-Building-Memory'></a>
### Memory `property`

##### Summary

Memory reference of building data

<a name='P-SC4Parser-DataStructures-Building-MinCoordinateX'></a>
### MinCoordinateX `property`

##### Summary

Minimum X coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MinCoordinateY'></a>
### MinCoordinateY `property`

##### Summary

Minimum Y coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MinCoordinateZ'></a>
### MinCoordinateZ `property`

##### Summary

Minimum Z coordinate of building

<a name='P-SC4Parser-DataStructures-Building-MinTractX'></a>
### MinTractX `property`

##### Summary

Minimum tract X coordinate for building

<a name='P-SC4Parser-DataStructures-Building-MinTractZ'></a>
### MinTractZ `property`

##### Summary

Minimum tract Z coordinate for building

<a name='P-SC4Parser-DataStructures-Building-MinorVersion'></a>
### MinorVersion `property`

##### Summary

Minor version of building spec

<a name='P-SC4Parser-DataStructures-Building-Offset'></a>
### Offset `property`

##### Summary

Offset of building data within Building subfile

<a name='P-SC4Parser-DataStructures-Building-Orientation'></a>
### Orientation `property`

##### Summary

Building's orientation

<a name='P-SC4Parser-DataStructures-Building-SaveGamePropertyCount'></a>
### SaveGamePropertyCount `property`

##### Summary

Number of save game properties (SIGProps) associated with building

<a name='P-SC4Parser-DataStructures-Building-SaveGamePropertyEntries'></a>
### SaveGamePropertyEntries `property`

##### Summary

Save game properties (SIGProps) of building

##### Remarks

For a list of all possible building SIGPROPs visit the following:
https://wiki.sc4devotion.com/index.php?title=Building_Subfile#Savegame_Properties_.28SGProps.29

<a name='P-SC4Parser-DataStructures-Building-ScaffoldingHeight'></a>
### ScaffoldingHeight `property`

##### Summary

Building's scaffolding height

<a name='P-SC4Parser-DataStructures-Building-Size'></a>
### Size `property`

##### Summary

Size of building data

<a name='P-SC4Parser-DataStructures-Building-TGI'></a>
### TGI `property`

##### Summary

TypeGroupInstance (TGI) of building, reference to prop exemplar

##### Remarks

Same as typeid, groupid and instanceid from this file. Just included it for accessibility

<a name='P-SC4Parser-DataStructures-Building-TractSizeX'></a>
### TractSizeX `property`

##### Summary

Tract size on the X axis for building

<a name='P-SC4Parser-DataStructures-Building-TractSizeZ'></a>
### TractSizeZ `property`

##### Summary

Tract size on the Z axis for building

<a name='P-SC4Parser-DataStructures-Building-TypeID'></a>
### TypeID `property`

##### Summary

The Building's Type ID

<a name='P-SC4Parser-DataStructures-Building-Unknown1'></a>
### Unknown1 `property`

##### Summary

Unknown field

<a name='P-SC4Parser-DataStructures-Building-Unknown2'></a>
### Unknown2 `property`

##### Summary

Unknown field

<a name='P-SC4Parser-DataStructures-Building-ZotWord'></a>
### ZotWord `property`

##### Summary

Zot word for building

<a name='P-SC4Parser-DataStructures-Building-x278128A0'></a>
### x278128A0 `property`

##### Summary

Unknown value, is always the same for all buildings

<a name='M-SC4Parser-DataStructures-Building-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the building

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-Building-Parse-System-Byte[],System-UInt32-'></a>
### Parse(buffer,offset) `method`

##### Summary

Load a building from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to load building from |
| offset | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Position in data to read building from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Subfiles-BuildingSubfile'></a>
## BuildingSubfile `type`

##### Namespace

SC4Parser.Subfiles

##### Summary

Implmentation of the Building Subfile. Building subfile stores all building data in a SimCity 4 savegame.

##### Example

`
             // Simple usage
             // (Just assume the building subfile has already been read, see SC4SaveGame.GetBuildingSubfile())
            
             // Access a building
             Building firstBuilding = buildingSubfile.Buildings.First();
             
             // Do something with it
             firstBuilding.Dump();
 `

##### Remarks

Actual reading of individual builds is done in DataStructure\Buildings.cs
 
 Implemented from https://wiki.sc4devotion.com/index.php?title=Building_Subfile

##### See Also

- [SC4Parser.DataStructures.Building](#T-SC4Parser-DataStructures-Building 'SC4Parser.DataStructures.Building')
- [SC4Parser.Subfiles.LotSubfile](#T-SC4Parser-Subfiles-LotSubfile 'SC4Parser.Subfiles.LotSubfile')

<a name='P-SC4Parser-Subfiles-BuildingSubfile-Buildings'></a>
### Buildings `property`

##### Summary

Stores all building in the subfile

<a name='M-SC4Parser-Subfiles-BuildingSubfile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the Building Subfile

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Subfiles-BuildingSubfile-Parse-System-Byte[],System-Int32-'></a>
### Parse(buffer,size) `method`

##### Summary

Reads the Building Subfile from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read subfile from |
| size | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Size of data that is being read |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Logging-ConsoleLogger'></a>
## ConsoleLogger `type`

##### Namespace

SC4Parser.Logging

##### Summary

Console Logger implementation, logs output to standard output

##### Example

`
            // Setup logger
            // This will automatically add it to list of log outputs
            ConsoleLogger logger = new ConsoleLogger();
            
            // Run some operations and generate some logs
            
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            TerrainMapSubfile terrainMap = null
            try 
            {
                terrainMap = savegame.GetTerrainMapSubfile();
            }
            catch (SubfileNotFoundException)
            {
                Console.Writeline("Could not find subfile");
            }
`

<a name='T-SC4Parser-Constants'></a>
## Constants `type`

##### Namespace

SC4Parser

##### Summary

Stores common values and identifiers used in SimCity 4 save gamesa

<a name='F-SC4Parser-Constants-BUILDING_SUBFILE_TYPE'></a>
### BUILDING_SUBFILE_TYPE `constants`

##### Summary

Type ID of Building Subfile

##### See Also

- [SC4Parser.Subfiles.BuildingSubfile](#T-SC4Parser-Subfiles-BuildingSubfile 'SC4Parser.Subfiles.BuildingSubfile')

<a name='F-SC4Parser-Constants-DATABASE_DIRECTORY_FILE_TGI'></a>
### DATABASE_DIRECTORY_FILE_TGI `constants`

##### Summary

TypeGroupInstance (TGI) ID for the Database Directory File (DBDF)

##### See Also

- [SC4Parser.Files.DatabaseDirectoryFile](#T-SC4Parser-Files-DatabaseDirectoryFile 'SC4Parser.Files.DatabaseDirectoryFile')

<a name='F-SC4Parser-Constants-LARGE_CITY_TILE_COUNT'></a>
### LARGE_CITY_TILE_COUNT `constants`

##### Summary

Number of grid tiles in a large city

<a name='F-SC4Parser-Constants-LOT_SUBFILE_TYPE'></a>
### LOT_SUBFILE_TYPE `constants`

##### Summary

Type ID of Lot Subfile

##### See Also

- [SC4Parser.Subfiles.LotSubfile](#T-SC4Parser-Subfiles-LotSubfile 'SC4Parser.Subfiles.LotSubfile')

<a name='F-SC4Parser-Constants-LOT_WEALTH_HIGH'></a>
### LOT_WEALTH_HIGH `constants`

##### Summary

High zone wealth value

<a name='F-SC4Parser-Constants-LOT_WEALTH_LOW'></a>
### LOT_WEALTH_LOW `constants`

##### Summary

Low zone wealth value

<a name='F-SC4Parser-Constants-LOT_WEALTH_MEDIUM'></a>
### LOT_WEALTH_MEDIUM `constants`

##### Summary

Medium zone wealth value

<a name='F-SC4Parser-Constants-LOT_WEALTH_NONE'></a>
### LOT_WEALTH_NONE `constants`

##### Summary

No zone wealth value

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_AIRPORT'></a>
### LOT_ZONE_TYPE_AIRPORT `constants`

##### Summary

Airport zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_HIGH'></a>
### LOT_ZONE_TYPE_COMMERCIAL_HIGH `constants`

##### Summary

High density commercial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_LOW'></a>
### LOT_ZONE_TYPE_COMMERCIAL_LOW `constants`

##### Summary

Low density commercial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_COMMERCIAL_MEDIUM'></a>
### LOT_ZONE_TYPE_COMMERCIAL_MEDIUM `constants`

##### Summary

Medium density commercial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_HIGH'></a>
### LOT_ZONE_TYPE_INDUSTRIAL_HIGH `constants`

##### Summary

High density industrial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_LOW'></a>
### LOT_ZONE_TYPE_INDUSTRIAL_LOW `constants`

##### Summary

Low density industrial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM'></a>
### LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM `constants`

##### Summary

Medium density industrial zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_MILITARY'></a>
### LOT_ZONE_TYPE_MILITARY `constants`

##### Summary

Military zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_PLOPPED_BUILDING'></a>
### LOT_ZONE_TYPE_PLOPPED_BUILDING `constants`

##### Summary

Plopped building zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT'></a>
### LOT_ZONE_TYPE_PLOPPED_BUILDING_ALT `constants`

##### Summary

Plopped building zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_HIGH'></a>
### LOT_ZONE_TYPE_RESIDENTIAL_HIGH `constants`

##### Summary

High density residential zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_LOW'></a>
### LOT_ZONE_TYPE_RESIDENTIAL_LOW `constants`

##### Summary

Low density residential zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM'></a>
### LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM `constants`

##### Summary

Medium density residential zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_SEAPORT'></a>
### LOT_ZONE_TYPE_SEAPORT `constants`

##### Summary

Seaport zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_SPACEPORT'></a>
### LOT_ZONE_TYPE_SPACEPORT `constants`

##### Summary

Spaceport zone type

<a name='F-SC4Parser-Constants-LOT_ZONE_TYPE_STRINGS'></a>
### LOT_ZONE_TYPE_STRINGS `constants`

##### Summary

Lot zone types as strings

<a name='F-SC4Parser-Constants-LOT_ZONE_WEALTH_STRINGS'></a>
### LOT_ZONE_WEALTH_STRINGS `constants`

##### Summary

Lot wealth types as strings

<a name='F-SC4Parser-Constants-MEDIUM_CITY_TILE_COUNT'></a>
### MEDIUM_CITY_TILE_COUNT `constants`

##### Summary

Number of grid tiles in a medium sized city

<a name='F-SC4Parser-Constants-ORIENTATION_EAST'></a>
### ORIENTATION_EAST `constants`

##### Summary

East orientation

<a name='F-SC4Parser-Constants-ORIENTATION_NORTH'></a>
### ORIENTATION_NORTH `constants`

##### Summary

North orientation

<a name='F-SC4Parser-Constants-ORIENTATION_SOUTH'></a>
### ORIENTATION_SOUTH `constants`

##### Summary

South orientation

<a name='F-SC4Parser-Constants-ORIENTATION_STRINGS'></a>
### ORIENTATION_STRINGS `constants`

##### Summary

Orientations used by SimCity 4 save game items as strings

##### See Also

- [SC4Parser.DataStructures.Building.Orientation](#P-SC4Parser-DataStructures-Building-Orientation 'SC4Parser.DataStructures.Building.Orientation')
- [SC4Parser.DataStructures.Lot.Orientation](#P-SC4Parser-DataStructures-Lot-Orientation 'SC4Parser.DataStructures.Lot.Orientation')

<a name='F-SC4Parser-Constants-ORIENTATION_WEST'></a>
### ORIENTATION_WEST `constants`

##### Summary

West orientation

<a name='F-SC4Parser-Constants-REGION_VIEW_SUBFILE_TGI'></a>
### REGION_VIEW_SUBFILE_TGI `constants`

##### Summary

TypeGroupInstance (TGI) ID for the Region View Subfile

##### See Also

- [SC4Parser.Subfiles.RegionViewSubfile](#T-SC4Parser-Subfiles-RegionViewSubfile 'SC4Parser.Subfiles.RegionViewSubfile')

<a name='F-SC4Parser-Constants-SIGPROP_DATATYPE_TYPES'></a>
### SIGPROP_DATATYPE_TYPES `constants`

##### Summary

Different types used in Save Game Propertie's (SIGPROPs) data

##### See Also

- [SC4Parser.DataStructures.SaveGameProperty](#T-SC4Parser-DataStructures-SaveGameProperty 'SC4Parser.DataStructures.SaveGameProperty')
- [SC4Parser.DataStructures.SaveGameProperty.Data](#P-SC4Parser-DataStructures-SaveGameProperty-Data 'SC4Parser.DataStructures.SaveGameProperty.Data')

<a name='F-SC4Parser-Constants-SIGPROP_DATATYPE_TYPE_STRINGS'></a>
### SIGPROP_DATATYPE_TYPE_STRINGS `constants`

##### Summary

Different types used in Save Game Propertie's (SIGPROPs) as strings

<a name='F-SC4Parser-Constants-SMALL_CITY_TILE_COUNT'></a>
### SMALL_CITY_TILE_COUNT `constants`

##### Summary

Number of grid tiles in a small sized city

<a name='F-SC4Parser-Constants-TERRAIN_MAP_SUBFILE_TGI'></a>
### TERRAIN_MAP_SUBFILE_TGI `constants`

##### Summary

TypeGroupInstance (TGI) ID for the Terrain Map Subfile

##### See Also

- [SC4Parser.Subfiles.TerrainMapSubfile](#T-SC4Parser-Subfiles-TerrainMapSubfile 'SC4Parser.Subfiles.TerrainMapSubfile')

<a name='T-SC4Parser-DBPFParsingException'></a>
## DBPFParsingException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when there are issues parsing a Database Packed File (DBPF)

##### Remarks

Inner exception contains specific exception that occured

##### See Also

- [SC4Parser.Files.DatabasePackedFile](#T-SC4Parser-Files-DatabasePackedFile 'SC4Parser.Files.DatabasePackedFile')

<a name='M-SC4Parser-DBPFParsingException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,e) `constructor`

##### Summary

Construcotr that constructs with an exception message and an inner exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |
| e | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Inner exception |

<a name='T-SC4Parser-Files-DatabaseDirectoryFile'></a>
## DatabaseDirectoryFile `type`

##### Namespace

SC4Parser.Files

##### Summary

Represents a DatabaseDirectoryfile (DBDF or DIR file)

A DBDF (not to be confused with DBPF, thanks maxis...) is an IndexEntry within a SimCity 4 savegame (DBPF) that holds a list of
all compressed files (DatabaseDirectoryResources) within a save game. 

The TypegroupInstance of a DBDF is always E86B1EEF E86B1EEF 286B1F03 in a save

##### Remarks

Implemented from https://wiki.sc4devotion.com/index.php?title=DBDF

##### See Also

- [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry')

<a name='M-SC4Parser-Files-DatabaseDirectoryFile-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor for DatabaseDirectoryFile

##### Parameters

This constructor has no parameters.

<a name='M-SC4Parser-Files-DatabaseDirectoryFile-#ctor-SC4Parser-DataStructures-IndexEntry-'></a>
### #ctor(entry) `constructor`

##### Summary

Constructor for a DatabaseDirectoryFile that uses it's 
associated IndexEntry

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') |  |

<a name='P-SC4Parser-Files-DatabaseDirectoryFile-ResourceCount'></a>
### ResourceCount `property`

##### Summary

Number of resources in file

<a name='P-SC4Parser-Files-DatabaseDirectoryFile-Resources'></a>
### Resources `property`

##### Summary

List of all compressed resources in save

<a name='M-SC4Parser-Files-DatabaseDirectoryFile-AddResource-SC4Parser-DataStructures-DatabaseDirectoryResource-'></a>
### AddResource(resource) `method`

##### Summary

Adds a Database Directory Resource to Database Directory File's (DBDF) Resources

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resource | [SC4Parser.DataStructures.DatabaseDirectoryResource](#T-SC4Parser-DataStructures-DatabaseDirectoryResource 'SC4Parser.DataStructures.DatabaseDirectoryResource') | Resource to add |

<a name='M-SC4Parser-Files-DatabaseDirectoryFile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of DBDF

##### Parameters

This method has no parameters.

<a name='T-SC4Parser-DataStructures-DatabaseDirectoryResource'></a>
## DatabaseDirectoryResource `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Implementation of a Database Directory Resource (DIR record).
A Database Directory Resource represents a compressed file within a SimCity 4 savegame (DBPF)
The uncompressed size of the record can be used to determine if a file has been decompressed properly.

##### Remarks

Implemented from https://wiki.sc4devotion.com/index.php?title=DBDF

<a name='P-SC4Parser-DataStructures-DatabaseDirectoryResource-DecompressedFileSize'></a>
### DecompressedFileSize `property`

##### Summary

Decompressed size of resource's file

<a name='P-SC4Parser-DataStructures-DatabaseDirectoryResource-TGI'></a>
### TGI `property`

##### Summary

TypeGroupInstance (TGI) of resource

<a name='M-SC4Parser-DataStructures-DatabaseDirectoryResource-Dump'></a>
### Dump() `method`

##### Summary

Prints out the values of a resource

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-DatabaseDirectoryResource-Parse-System-Byte[]-'></a>
### Parse(buffer) `method`

##### Summary

Reads an individual resource from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to load resource from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

##### Remarks

Byte array given to method should only contain the data for one resource

<a name='T-SC4Parser-DatabaseDirectoryResourceNotFoundException'></a>
## DatabaseDirectoryResourceNotFoundException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when Database Directory (DBDF) Resource cannot be found

##### See Also

- [SC4Parser.DataStructures.DatabaseDirectoryResource](#T-SC4Parser-DataStructures-DatabaseDirectoryResource 'SC4Parser.DataStructures.DatabaseDirectoryResource')

<a name='T-SC4Parser-Files-DatabasePackedFile'></a>
## DatabasePackedFile `type`

##### Namespace

SC4Parser.Files

##### Summary

Implementation of Database Packed File (DBPF).

A Database Packed File (DBPF) is the file format used by maxis for savegames. They are compressed archive files and contain
multiple files related to a save, some of which are compressed using QFS/refpack.

##### Example

`
            // Basic usage
            
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
             
            // Get DBPF file version
            Console.WriteLine("DBPF Version {0}.{1}",
                savegame.Header.MajorVersion,
                savegame.Header.MinorVersion);
`

##### Remarks

This implementation is primarily focused on the DBPF version used in SimCity 4
DBPF version 1.1

A detailed spec and layout of the file format can be found here: 
- https://wiki.sc4devotion.com/index.php?title=DBPF
- http://wiki.niotso.org/DBPF

<a name='M-SC4Parser-Files-DatabasePackedFile-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor for Database Packed File (DBPF)
Sets up default values for all internal objects

##### Parameters

This constructor has no parameters.

<a name='M-SC4Parser-Files-DatabasePackedFile-#ctor-SC4Parser-Files-DatabasePackedFile-'></a>
### #ctor(databasePackedFile) `constructor`

##### Summary

Copy constructor, used to create a new Database Packed File (DBPF) object using the values
of another object without passing by reference

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databasePackedFile | [SC4Parser.Files.DatabasePackedFile](#T-SC4Parser-Files-DatabasePackedFile 'SC4Parser.Files.DatabasePackedFile') | Object you want to use to create the new object |

<a name='M-SC4Parser-Files-DatabasePackedFile-#ctor-System-String-'></a>
### #ctor(path) `constructor`

##### Summary

Constructor for Database Packed File (DBPF) that loads a DBPF
from a file at a given path

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path to DBPF file |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.DBPFParsingException](#T-SC4Parser-DBPFParsingException 'SC4Parser.DBPFParsingException') | Thrown when an exception occurs while loading the DBPF file |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // You can now access and load data from the save game
            // using LoadIndexEntry or accessing the Index Entries directly:
            foreach (IndexEntry entry in savegame.IndexEntries)
            {
                Console.WriteLine(entry.TGI);
            }
`

<a name='P-SC4Parser-Files-DatabasePackedFile-DBDFFile'></a>
### DBDFFile `property`

##### Summary

Database Packed File's (DBPF) Database Directory File (DBDI/DIR)
which contains all the DBPF's compressed index entries

##### See Also

- [SC4Parser.DataStructures.DatabaseDirectoryResource](#T-SC4Parser-DataStructures-DatabaseDirectoryResource 'SC4Parser.DataStructures.DatabaseDirectoryResource')

<a name='P-SC4Parser-Files-DatabasePackedFile-FilePath'></a>
### FilePath `property`

##### Summary

File path that the Database Packed File (DBPF) was loaded from

<a name='P-SC4Parser-Files-DatabasePackedFile-Header'></a>
### Header `property`

##### Summary

Database Packed File's (DBPF) header file

<a name='P-SC4Parser-Files-DatabasePackedFile-IndexEntries'></a>
### IndexEntries `property`

##### Summary

List of all index entries in Database Packed File (DBPF)

<a name='P-SC4Parser-Files-DatabasePackedFile-RawFile'></a>
### RawFile `property`

##### Summary

Stream which contains copy of the raw Database Packed File (DBPF) in memory,
used to load resources after file has been initially parsed

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Find flora file
            IndexEntry entry = save.FindIndexEntryWithType("A9C05C85"); 
            
            // Get a copy of the DBPF file
            var data = save.RawFile;
            
            // Read the file from the DBPF into our buffer
            buffer = new byte[entry.FileSize];
            data.Seek(entry.FileLocation, SeekOrigin.Begin);
            data.Read(buffer, 0, fileSize);
            
            //.. do what we want with it
`

<a name='M-SC4Parser-Files-DatabasePackedFile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the Database Packed File (DBPF)

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Files-DatabasePackedFile-FindDatabaseDirectoryResource-SC4Parser-DataStructures-IndexEntry-'></a>
### FindDatabaseDirectoryResource(entry) `method`

##### Summary

Finds a Database Directory Resource inside the Database Packed File's (DBPF) Database Directory File (DBDF/DIR)

##### Returns

Returns the found resource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | Entry to try and find in DIR file |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.DatabaseDirectoryResourceNotFoundException](#T-SC4Parser-DatabaseDirectoryResourceNotFoundException 'SC4Parser.DatabaseDirectoryResourceNotFoundException') | Thrown when the Index Entry's resource cannot be found |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Terrain map subfile TGI
            TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
            
            // Find terrain map subfile's Index Entry in DBPF
            IndexEntry entry = null;
            try 
            {
                save.FindIndexEntry(terrainTGI);
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
            
            // Try and find the Database Directory Resource of the Index Entry
            try
            {
                var resource = savegame.FindDatabaseDirectoryResource(entry);
            } 
            catch (DatabaseDirectoryResourceNotFoundException)
            {
                Console.Writeline("Resource for Index Entry cannot be found");
                return;
            }
`

##### See Also

- [SC4Parser.Files.DatabaseDirectoryFile](#T-SC4Parser-Files-DatabaseDirectoryFile 'SC4Parser.Files.DatabaseDirectoryFile')

<a name='M-SC4Parser-Files-DatabasePackedFile-FindIndexEntry-SC4Parser-Types-TypeGroupInstance-'></a>
### FindIndexEntry(tgi) `method`

##### Summary

Finds and returns an Index Entry with a given TypeGroupInstance (TGI) with in the Database Packed File (DBPF)

##### Returns

Returns the found Index Entry with the matching TypeGroupInstance (TGI)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tgi | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | The TypeGroupInstance (TGI) of the Index Entry |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException') | Thrown when Index Entry cannot be found |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Terrain map subfile TGI
            TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
            
            // Find terrain map subfile's Index Entry in DBPF
            IndexEntry entry = null;
            try 
            {
                save.FindIndexEntry(terrainTGI);
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
`

##### See Also

- [SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType](#M-SC4Parser-Files-DatabasePackedFile-FindIndexEntryWithType-System-String- 'SC4Parser.Files.DatabasePackedFile.FindIndexEntryWithType(System.String)')

<a name='M-SC4Parser-Files-DatabasePackedFile-FindIndexEntryWithType-System-String-'></a>
### FindIndexEntryWithType(type_id) `method`

##### Summary

Finds and returns an Index Entry with a given Type ID

##### Returns

The Index Entry with the given Type ID

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type_id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Type ID used to find Index Entry |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException') | Thrown when Index Entry cannot be found |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Find Lot Subfile's Index Entry from save
            IndexEntry entry = null
            try
            {
                entry = save.FindIndexEntryWithType("C9BD5D4A"); 
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
`

##### See Also

- [SC4Parser.Files.DatabasePackedFile.FindIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-FindIndexEntry-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Files.DatabasePackedFile.FindIndexEntry(SC4Parser.Types.TypeGroupInstance)')

<a name='M-SC4Parser-Files-DatabasePackedFile-IsIndexEntryCompressed-SC4Parser-DataStructures-IndexEntry-'></a>
### IsIndexEntryCompressed(entry) `method`

##### Summary

Checks if an IndexEntry is compressed

##### Returns

Returns `true` if the Index Entry is compressed, `false` if it is uncompressed

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | Index Entry to check |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Find Lot Subfile's Index Entry from save
            IndexEntry entry = null;
            try
            {
                entry = save.FindIndexEntryWithType("C9BD5D4A"); 
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
            
            // Check if entry is compressed
            if (savegame.IsEntryCompressed == true)
            {
                Console.Writeline("Lot data is compressed"); 
            }
            else 
            {
                Console.WriteLine("Lot data is not compressed");
            }
`

<a name='M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-Types-TypeGroupInstance-'></a>
### LoadIndexEntry(tgi) `method`

##### Summary

Loads the contents of an Index Entry from the Database Packed File (DBPF)

##### Returns

Returns the (possibly uncompressed) bytes of an IndexEntry

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tgi | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | The TypeGroupInstance (TGI) used to find the index entry |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException') | Thrown when IndexEntry doesn't exist in save game |
| [SC4Parser.IndexEntryLoadingException](#T-SC4Parser-IndexEntryLoadingException 'SC4Parser.IndexEntryLoadingException') | Thrown when exception occurs when loading IndexEntry |
| [SC4Parser.QFSDecompressionException](#T-SC4Parser-QFSDecompressionException 'SC4Parser.QFSDecompressionException') | Thrown when exception occurs while decompressing IndexEntry data |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Terrain map subfile TGI
            TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
            
            // load terrain map subfile from DBPF
            try
            {
                byte[] data = save.LoadIndexEntry(terrainTGI); 
            } 
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
            catch (IndexEntryLoadingException)
            {
                Console.Writeline("Issue loading Index Entry");
                return;
            }
            catch (QFSDecompressionException)
            {
                Console.Writeline("Issue decompressing data from DBPF");
                return;
            }
            
            // Do something with the terrain data...
`

##### Remarks

The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
in the Database Directory file (DBDF/DIR)

##### See Also

- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntryRaw-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Compression.QFS](#T-SC4Parser-Compression-QFS 'SC4Parser.Compression.QFS')

<a name='M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry-'></a>
### LoadIndexEntry(entry) `method`

##### Summary

Loads the contents of an Index Entry from the Database Packed File (DBPF)

##### Returns

Returns the (possibly uncompressed) bytes of an IndexEntry

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | The Index Entry used to load data |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException') | Thrown when IndexEntry doesn't exist in save game |
| [SC4Parser.IndexEntryLoadingException](#T-SC4Parser-IndexEntryLoadingException 'SC4Parser.IndexEntryLoadingException') | Thrown when exception occurs when loading IndexEntry |
| [SC4Parser.QFSDecompressionException](#T-SC4Parser-QFSDecompressionException 'SC4Parser.QFSDecompressionException') | Thrown when exception occurs while decompressing IndexEntry data |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Terrain map subfile TGI
            TypeGroupInstance terrainTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
            
            // Find terrain map subfile's Index Entry in DBPF
            IndexEntry entry = save.FindIndexEntry(terrainTGI); 
            
            // Load the Index Entry
            byte[] terrainData = null;
            try
            {
                terrainData = save.LoadIndexEntryRaw(entry);
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
            catch (IndexEntryLoadingException)
            {
                Console.Writeline("Issue loading Index Entry");
                return;
            }
            catch (QFSDecompressionException)
            {
                Console.Writeline("Issue decompressing data from DBPF");
                return;
            }
            
            // Do something with the terrain data...
`

##### Remarks

The data of the Index Entry will be decompressed using QFS/RefPack if it is compressed (has an entry
in the Database Directory file (DBDF/DIR)

##### See Also

- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntryRaw-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Compression.QFS](#T-SC4Parser-Compression-QFS 'SC4Parser.Compression.QFS')

<a name='M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntryRaw-SC4Parser-DataStructures-IndexEntry-'></a>
### LoadIndexEntryRaw(entry) `method`

##### Summary

Returns the raw bytes of an IndexEntry using the referring IndexEntry, does not attempt to decompress entry if it is compressed.

##### Returns

Return the raw data of the Index Entry from the DBPF file in a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | The entry to load |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryNotFoundException](#T-SC4Parser-IndexEntryNotFoundException 'SC4Parser.IndexEntryNotFoundException') | Thrown when IndexEntry doesn't exist in save game |
| [SC4Parser.IndexEntryLoadingException](#T-SC4Parser-IndexEntryLoadingException 'SC4Parser.IndexEntryLoadingException') | Thrown when exception occurs when loading IndexEntry |

##### Example

`
            // Load save game
            DatabasePackedFile savegame;
            try
            {
                savegame = new DatabasePackedFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // Find Lot Subfile's Index Entry from save
            IndexEntry entry = save.FindIndexEntryWithType("C9BD5D4A"); 
            
            // Load the compressed lot subfile 
            byte[] lotData = null;
            try
            {
                lotData = save.LoadIndexEntryRaw(entry);
            }
            catch (IndexEntryNotFoundException)
            {
                Console.Writeline("Could not find Index Entry");
                return;
            }
            catch (IndexEntryLoadingException)
            {
                Console.Writeline("Issue loading Index Entry");
                return;
            }
            
            // Do something with the compressed data;
            SuperAwesomeCustomQFSDecompressionMethod(lotData);
`

##### Remarks

Will load the raw data of an Index Entry, this is the data as it appears in the DBPF so maybe in a compressed format

##### See Also

- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.Types.TypeGroupInstance)')

<a name='M-SC4Parser-Files-DatabasePackedFile-Parse-System-String-'></a>
### Parse(path) `method`

##### Summary

Parses a DBPF/SimCity 4 save file at a path

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path to DBPF file |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.DBPFParsingException](#T-SC4Parser-DBPFParsingException 'SC4Parser.DBPFParsingException') | Thrown when an exception occurs while loading the DBPF file |

##### Example

`
            DatabasePackedFile savegame = new DatabasePackedFile();
            
            try
            {
                savegame.Parse(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
`

<a name='M-SC4Parser-Files-DatabasePackedFile-ReadRawIndexEntryData-SC4Parser-DataStructures-IndexEntry-'></a>
### ReadRawIndexEntryData(entry) `method`

##### Summary

Reads an IndexEntry's raw (possibly compressed) data from the Database Packed File (DBPF)

Internal function used to load data for other Index Entry loading functions

##### Returns

Return the raw data of the Index Entry from the DBPF file in a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | The entry to load |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.IndexEntryLoadingException](#T-SC4Parser-IndexEntryLoadingException 'SC4Parser.IndexEntryLoadingException') | Thrown when there is an error while loading the Index Entry |

##### Remarks

Will load the raw data of an Index Entry, this is the data as it appears in the DBPF so maybe in a compressed format

##### See Also

- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntryRaw-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntryRaw(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-DataStructures-IndexEntry- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.DataStructures.IndexEntry)')
- [SC4Parser.Files.DatabasePackedFile.LoadIndexEntry](#M-SC4Parser-Files-DatabasePackedFile-LoadIndexEntry-SC4Parser-Types-TypeGroupInstance- 'SC4Parser.Files.DatabasePackedFile.LoadIndexEntry(SC4Parser.Types.TypeGroupInstance)')

<a name='T-SC4Parser-DataStructures-DatabasePackedFileHeader'></a>
## DatabasePackedFileHeader `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Header file for a Database Packed File (DBPF). 
Implements version 1.0 of the DBPF header used for SimCity 4

##### Remarks

Implemented from https://wiki.sc4devotion.com/index.php?title=DBPF#Header

##### See Also

- [SC4Parser.Files.DatabasePackedFile](#T-SC4Parser-Files-DatabasePackedFile 'SC4Parser.Files.DatabasePackedFile')

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-DateCreated'></a>
### DateCreated `property`

##### Summary

Date DBPF file was created

##### Remarks

In Unix time stamp format

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-DateModified'></a>
### DateModified `property`

##### Summary

Date DBPF file was modified

##### Remarks

In Unix time stamp format

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-FirstIndexOffset'></a>
### FirstIndexOffset `property`

##### Summary

Position of first Index Entry in the DBPF file

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleCount'></a>
### HoleCount `property`

##### Summary

Number of hole entries in Hole Record

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleOffset'></a>
### HoleOffset `property`

##### Summary

Location of Hole Record in the DBPF file

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-HoleSize'></a>
### HoleSize `property`

##### Summary

size of the Hold Record

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-Identifier'></a>
### Identifier `property`

##### Summary

File identifier (always 'DBPF')

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexCount'></a>
### IndexCount `property`

##### Summary

Number of Index Entries in Index table

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexMajorVersion'></a>
### IndexMajorVersion `property`

##### Summary

Index table major version

##### Remarks

Always 7 in The Sims 2, Sim City 4. If this is used in 2.0, then it is 0 for SPORE.

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexMinorVersion'></a>
### IndexMinorVersion `property`

##### Summary

Index table minor version

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-IndexSize'></a>
### IndexSize `property`

##### Summary

Size of index table in bytes

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-MajorVersion'></a>
### MajorVersion `property`

##### Summary

DBPF major version

##### Remarks

Common DBPF versions:
    1.0 seen in Sim City 4, The Sims 2
    1.1 seen in The Sims 2
    2.0 seen in Spore, The Sims 3
    3.0 seen in SimCity

<a name='P-SC4Parser-DataStructures-DatabasePackedFileHeader-MinorVersion'></a>
### MinorVersion `property`

##### Summary

DBPF minor version

##### Remarks

Common DBPF versions:
    1.0 seen in Sim City 4, The Sims 2
    1.1 seen in The Sims 2
    2.0 seen in Spore, The Sims 3
    3.0 seen in SimCity

<a name='M-SC4Parser-DataStructures-DatabasePackedFileHeader-Dump'></a>
### Dump() `method`

##### Summary

Dumps contents of header

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-DatabasePackedFileHeader-Parse-System-Byte[]-'></a>
### Parse(buffer) `method`

##### Summary

Reads a DBPF header from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read header from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Logging-ILogger'></a>
## ILogger `type`

##### Namespace

SC4Parser.Logging

##### Summary

Logger interface, used to create new logging implementations that can be used to print out
internal logging from the library

##### Remarks

See ConsoleLogger to see how the logging interface can be implemented

##### See Also

- [SC4Parser.Logging.Logger](#T-SC4Parser-Logging-Logger 'SC4Parser.Logging.Logger')
- [SC4Parser.Logging.ConsoleLogger](#T-SC4Parser-Logging-ConsoleLogger 'SC4Parser.Logging.ConsoleLogger')

<a name='M-SC4Parser-Logging-ILogger-EnableChannel-SC4Parser-Logging-LogLevel-'></a>
### EnableChannel(level) `method`

##### Summary

Enable a log channel to be included in log output

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [SC4Parser.Logging.LogLevel](#T-SC4Parser-Logging-LogLevel 'SC4Parser.Logging.LogLevel') | Log level to be enabled |

##### Example

`
            // Enable any message using Debug log level to show up in log's output
            myLogger.EnableChannel(LogLevel.Debug);
`

<a name='M-SC4Parser-Logging-ILogger-Log-SC4Parser-Logging-LogLevel,System-String,System-Object[]-'></a>
### Log(level,format,args) `method`

##### Summary

Log a message

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [SC4Parser.Logging.LogLevel](#T-SC4Parser-Logging-LogLevel 'SC4Parser.Logging.LogLevel') | Message log level |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format of message |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Message arguments |

##### Example

`
            myLogger.Log(LogLevel.Error, "This is a test log message it can include {0} {1} {2}",
                "strings!",
                123,
                "Or any other type you want to pass!"
            );
`

<a name='T-SC4Parser-DataStructures-IndexEntry'></a>
## IndexEntry `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Implementation of an Index Entry in a save game 

An Index Entry represents a file stored within a SimCity 4 savegame (DBPF).
It stores the TGI (identifier), the location of the file with in the savegame and the size of the file.

##### Remarks

Implemented from https://wiki.sc4devotion.com/index.php?title=DBPF#DBPF_1.x.2C_Index_Table_7.0

<a name='M-SC4Parser-DataStructures-IndexEntry-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor

##### Parameters

This constructor has no parameters.

<a name='M-SC4Parser-DataStructures-IndexEntry-#ctor-SC4Parser-DataStructures-IndexEntry-'></a>
### #ctor(entry) `constructor`

##### Summary

Constructor meant for copying over an Index Entry to a new object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry') | Entry to copy over to new object |

<a name='M-SC4Parser-DataStructures-IndexEntry-#ctor-SC4Parser-Types-TypeGroupInstance,System-UInt32,System-UInt32-'></a>
### #ctor(tgi,location,size) `constructor`

##### Summary

Constructor for manually constructing an Index Entry without parsing it

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tgi | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | TypeGroupInstance (TGI) of Index Entry |
| location | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | File location of Index Entry |
| size | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | File size of Index Entry |

<a name='P-SC4Parser-DataStructures-IndexEntry-FileLocation'></a>
### FileLocation `property`

##### Summary

Location of the file in the DBPF that the index entry refers to

<a name='P-SC4Parser-DataStructures-IndexEntry-FileSize'></a>
### FileSize `property`

##### Summary

The size of the index entry's file

<a name='P-SC4Parser-DataStructures-IndexEntry-TGI'></a>
### TGI `property`

##### Summary

TypeGroupInstance (TGI) of Index entry

<a name='M-SC4Parser-DataStructures-IndexEntry-Dump'></a>
### Dump() `method`

##### Summary

Dumps the contents of an entry

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-IndexEntry-Parse-System-Byte[]-'></a>
### Parse(buffer) `method`

##### Summary

Loads an individual entry from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to load the index entry from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

##### Remarks

Buffer should only contain data for a single entry

<a name='T-SC4Parser-IndexEntryLoadingException'></a>
## IndexEntryLoadingException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when there is an issue with loading an Index Entry

##### Remarks

Inner exception contains specific exception that occured

##### See Also

- [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry')

<a name='M-SC4Parser-IndexEntryLoadingException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Constructor that constructs with an exception message

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |

<a name='M-SC4Parser-IndexEntryLoadingException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,e) `constructor`

##### Summary

Construcotr that constructs with an exception message and an inner exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |
| e | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Inner exception |

<a name='T-SC4Parser-IndexEntryNotFoundException'></a>
## IndexEntryNotFoundException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when an Index Entry cannot be found

##### See Also

- [SC4Parser.DataStructures.IndexEntry](#T-SC4Parser-DataStructures-IndexEntry 'SC4Parser.DataStructures.IndexEntry')

<a name='T-SC4Parser-Logging-LogLevel'></a>
## LogLevel `type`

##### Namespace

SC4Parser.Logging

##### Summary

Log levels used in log output

<a name='F-SC4Parser-Logging-LogLevel-Debug'></a>
### Debug `constants`

##### Summary

Debug messages

<a name='F-SC4Parser-Logging-LogLevel-Error'></a>
### Error `constants`

##### Summary

Error messages

<a name='F-SC4Parser-Logging-LogLevel-Fatal'></a>
### Fatal `constants`

##### Summary

Fatal error messages

<a name='F-SC4Parser-Logging-LogLevel-Info'></a>
### Info `constants`

##### Summary

General messages

<a name='F-SC4Parser-Logging-LogLevel-Warning'></a>
### Warning `constants`

##### Summary

Warning messages

<a name='T-SC4Parser-Logging-Logger'></a>
## Logger `type`

##### Namespace

SC4Parser.Logging

##### Summary

Static logger class, used to pass log messages from library components to any attached/implemented log interfaces

<a name='M-SC4Parser-Logging-Logger-AddLogOutput-SC4Parser-Logging-ILogger-'></a>
### AddLogOutput(logOutput) `method`

##### Summary

Add a logger interface to send log output to

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logOutput | [SC4Parser.Logging.ILogger](#T-SC4Parser-Logging-ILogger 'SC4Parser.Logging.ILogger') | Logger interface to add |

##### Example

`
            MyOwnLogger myLogger = new MyOwnLogger();
            Logger.AddLogOutput(myLogger);
            
            // Your logger will now be used as an output for any log message..
`

<a name='M-SC4Parser-Logging-Logger-EnableLogChannel-SC4Parser-Logging-LogLevel-'></a>
### EnableLogChannel(level) `method`

##### Summary

Enable a log level on all log outputs

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [SC4Parser.Logging.LogLevel](#T-SC4Parser-Logging-LogLevel 'SC4Parser.Logging.LogLevel') |  |

##### Example

`
            // Enable any message using Debug log level to show up in all logging outputs
            Logger.EnableChannel(LogLevel.Debug);
`

<a name='M-SC4Parser-Logging-Logger-Log-SC4Parser-Logging-LogLevel,System-String,System-Object[]-'></a>
### Log(level,format,args) `method`

##### Summary

Log a message

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [SC4Parser.Logging.LogLevel](#T-SC4Parser-Logging-LogLevel 'SC4Parser.Logging.LogLevel') | Message log level |
| format | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Format of message |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Message arguments |

##### Example

`
            Logger.Log(LogLevel.Error, "This is a test log message it can include {0} {1} {2}",
                "strings!",
                123,
                "Or any other type you want to pass!"
            );
`

<a name='T-SC4Parser-DataStructures-Lot'></a>
## Lot `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Representation of a Simcity 4 lot as it is stored in a save game

##### Example

`
            How to read and use lot data using library
            // (this is effectively what is done in SC4Save.GetLotSubfile())
            
            // Load save game
            SC4SaveFile savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            
            // load Lot Subfile from save
            LotSubfile lotSubfile = new LotSubfile();
            IndexEntry lotEntry = savegame.FindIndexEntryWithType("C9BD5D4A")
            byte[] lotSubfileData = savegame.LoadIndexEntry(lotEntry.TGI);
            lotSubfile.Parse(lotSubfileData, lotSubfileData.Length);
            
            // loop through lots and print out their sizes
            foreach (Lot lot in lotSubfile.Lots)
            {
                Console.Writeline(lot.SizeX + "x" + lot.SizeZ);
            }
`

##### Remarks

This implementation is not complete.

Implemented from https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile

##### See Also

- [SC4Parser.DataStructures.Building](#T-SC4Parser-DataStructures-Building 'SC4Parser.DataStructures.Building')

<a name='P-SC4Parser-DataStructures-Lot-BuildingInstanceID'></a>
### BuildingInstanceID `property`

##### Summary

Lot's associated building Instance ID

<a name='P-SC4Parser-DataStructures-Lot-CRC'></a>
### CRC `property`

##### Summary

Lot data's crc

<a name='P-SC4Parser-DataStructures-Lot-CommuteTileX'></a>
### CommuteTileX `property`

##### Summary

Lot's commute tile X

<a name='P-SC4Parser-DataStructures-Lot-CommuteTileZ'></a>
### CommuteTileZ `property`

##### Summary

Lot's commute tile Z

<a name='P-SC4Parser-DataStructures-Lot-DateLotAppeared'></a>
### DateLotAppeared `property`

##### Summary

Date (in game?) that lot grew or was plopped

<a name='P-SC4Parser-DataStructures-Lot-FlagByte1'></a>
### FlagByte1 `property`

##### Summary

Lot Flag byte 1 , can have one of the following values:
    0x01 - Might have to do with road access
    0x02 - Might have to do with road(job?) access
    0x04 - Might have to do with road access
    0x08 - Means the lot is watered
    0x10 - Means the lot is powered
    0x20 - Means the lot is marked historical
    0x40 - Might mean the lot is built

##### Remarks

Data from https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_1_-_Flag_Byte_1

<a name='P-SC4Parser-DataStructures-Lot-FlagByte2'></a>
### FlagByte2 `property`

##### Summary

Lot flag byte 2, can be one of the following files:
    0x01 - Flag Byte 1 = 0x10 - Powered (empty growable zones)
    0x02 - Flag Byte 1 = 0x50 - Powered and built
    0x03 - Flag Byte 1 = 0x58 - Powered, watered and built
    0x04 - Seen it once on a tall office under construction
    0x06 - Seen it once on a water tower without power

##### Remarks

Information from: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_2_-_Flag_Byte_2

<a name='P-SC4Parser-DataStructures-Lot-FlagByte3'></a>
### FlagByte3 `property`

##### Summary

Lot flag byte 3, unknown use

##### Remarks

More information here: https://www.wiki.sc4devotion.com/index.php?title=Lot_Subfile#Appendix_3_-_Flag_Byte_3

<a name='P-SC4Parser-DataStructures-Lot-LotInstanceID'></a>
### LotInstanceID `property`

##### Summary

Instance ID of the lot

<a name='P-SC4Parser-DataStructures-Lot-MajorVersion'></a>
### MajorVersion `property`

##### Summary

Lot's spec major version

<a name='P-SC4Parser-DataStructures-Lot-MaxTileX'></a>
### MaxTileX `property`

##### Summary

Maximum tile X coordinate for lot

<a name='P-SC4Parser-DataStructures-Lot-MaxTileZ'></a>
### MaxTileZ `property`

##### Summary

Maximum tile Z coordinate for lot

<a name='P-SC4Parser-DataStructures-Lot-Memory'></a>
### Memory `property`

##### Summary

Lot's memory

<a name='P-SC4Parser-DataStructures-Lot-MinTileX'></a>
### MinTileX `property`

##### Summary

Minimum tile X coordinate for lot

<a name='P-SC4Parser-DataStructures-Lot-MinTileZ'></a>
### MinTileZ `property`

##### Summary

Minimum tile Z coordinate for lot

<a name='P-SC4Parser-DataStructures-Lot-Offset'></a>
### Offset `property`

##### Summary

Position of lot within DBPf file

<a name='P-SC4Parser-DataStructures-Lot-Orientation'></a>
### Orientation `property`

##### Summary

Lot's orientation

<a name='P-SC4Parser-DataStructures-Lot-PositionY'></a>
### PositionY `property`

##### Summary

Lot's Y position

<a name='P-SC4Parser-DataStructures-Lot-Size'></a>
### Size `property`

##### Summary

Size of lot

<a name='P-SC4Parser-DataStructures-Lot-SizeX'></a>
### SizeX `property`

##### Summary

Lot width

<a name='P-SC4Parser-DataStructures-Lot-SizeZ'></a>
### SizeZ `property`

##### Summary

Lot depth

<a name='P-SC4Parser-DataStructures-Lot-Slope1Y'></a>
### Slope1Y `property`

##### Summary

Lot's Y coordinate if slope is conforming

<a name='P-SC4Parser-DataStructures-Lot-Slope2Y'></a>
### Slope2Y `property`

##### Summary

Lot's Y coordinate if slope is conforming

<a name='P-SC4Parser-DataStructures-Lot-Unknown'></a>
### Unknown `property`

##### Summary

Unknown lot value

<a name='P-SC4Parser-DataStructures-Lot-ZoneType'></a>
### ZoneType `property`

##### Summary

Lot's zone type

<a name='P-SC4Parser-DataStructures-Lot-ZoneWealth'></a>
### ZoneWealth `property`

##### Summary

Lot's zone wealth

<a name='M-SC4Parser-DataStructures-Lot-Dump'></a>
### Dump() `method`

##### Summary

Prints out the values of the lot

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-Lot-Parse-System-Byte[],System-UInt32-'></a>
### Parse(buffer,offset) `method`

##### Summary

Read an individual lot object from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read lot from |
| offset | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Position in data to read lot from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

##### Remarks

This implementation is not complete

<a name='T-SC4Parser-Subfiles-LotSubfile'></a>
## LotSubfile `type`

##### Namespace

SC4Parser.Subfiles

##### Summary

Implementation of the Lots Subfile. LotSubfile contains all logs in a SimCity 4 savegame (Partial implementation).

##### Example

`
             // Simple usage
             // (Just assume the lot subfile has already been read, see SC4SaveGame.GetLotSubfile())
            
             // Access a lot
             Lot firstLot = lotSubfile.Lots.First();
             
             // Do something with it
             firstLot.Dump();
 `

##### Remarks

The implementation of the lots is only partially complete and will not contain all data associated with the lots
 
 Actual reading of individual builds is done in DataStructure\Lot.cs
 
 Implemented from https://wiki.sc4devotion.com/index.php?title=Lot_Subfile

##### See Also

- [SC4Parser.DataStructures.Lot](#T-SC4Parser-DataStructures-Lot 'SC4Parser.DataStructures.Lot')
- [SC4Parser.Subfiles.BuildingSubfile](#T-SC4Parser-Subfiles-BuildingSubfile 'SC4Parser.Subfiles.BuildingSubfile')

<a name='F-SC4Parser-Subfiles-LotSubfile-Lots'></a>
### Lots `constants`

##### Summary

All lots stored in the subfile

<a name='M-SC4Parser-Subfiles-LotSubfile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the Lot Subfile

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Subfiles-LotSubfile-Parse-System-Byte[],System-Int32-'></a>
### Parse(buffer,size) `method`

##### Summary

Reads the Lots Subfile from byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read subfile from |
| size | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Size of data to be read |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Compression-QFS'></a>
## QFS `type`

##### Namespace

SC4Parser.Compression

##### Summary

Implementation of QFS/RefPack/LZ77 decompression. This compression is used on larger entries inside saves

##### Remarks

Note that this implementaiton contains control characters and other changes specific to SimCity 4.
You can read about other game specifics at thsi specification for QFS spec http://wiki.niotso.org/RefPack.

Ported from https://github.com/wouanagaine/SC4Mapper-2013/blob/db29c9bf88678a144dd1f9438e63b7a4b5e7f635/Modules/qfs.c#L25

More information on file specification:
- https://www.wiki.sc4devotion.com/index.php?title=DBPF_Compression
- http://wiki.niotso.org/RefPack#Naming_notes

<a name='M-SC4Parser-Compression-QFS-LZCompliantCopy-System-Byte[]@,System-Int32,System-Byte[]@,System-Int32,System-Int32-'></a>
### LZCompliantCopy(source,sourceOffset,destination,destinationOffset,length) `method`

##### Summary

Recurvise method that implements LZ compliant copying of data between arrays

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.Byte[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[]@ 'System.Byte[]@') | Array to copy from |
| sourceOffset | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Position in array to copy from |
| destination | [System.Byte[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[]@ 'System.Byte[]@') | Array to copy to |
| destinationOffset | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Position in array to copy to |
| length | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Amount of data to copy |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when the copy method tries to access an element that is out of bounds in the array |

##### Remarks

With QFS (LZ77) we require an LZ compatible copy method between arrays, what this means practically is that we need to copy
stuff one byte at a time from arrays. This is, because with LZ compatible algorithms, it is complete legal to copy over data that overruns
the currently filled position in the destination array. In other words it is more than likely the we will be asked to copy over data that hasn't
been copied yet. It's confusing, so we copy things one byte at a time using a recursive function.

<a name='M-SC4Parser-Compression-QFS-UncompressData-System-Byte[]-'></a>
### UncompressData(data) `method`

##### Summary

Uncompress data using QFS/RefPak and return uncompressed array of uncompressed data

##### Returns

Uncompressed data array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Compressed array of data |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when the compression algorithm tries to access an element that is out of bounds in the array |

##### Example

`
            // Load save game
            SC4SaveFile savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            
            // Read raw data for Region View Subfile from save
            byte[] data = sc4Save.LoadIndexEntryRaw(REGION_VIEW_SUBFILE_TGI);
            
            // Decompress data (This file will normally be compressed, should idealy check before decompressing)
            byte[] decompressedData = QFS.UncompressData(data);
`

<a name='T-SC4Parser-QFSDecompressionException'></a>
## QFSDecompressionException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when an error occurs while preforming a QFS/Refpack decompression

##### Remarks

Inner exception contains specific exception that occured

##### See Also

- [SC4Parser.Compression.QFS](#T-SC4Parser-Compression-QFS 'SC4Parser.Compression.QFS')

<a name='M-SC4Parser-QFSDecompressionException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,e) `constructor`

##### Summary

Construcotr that constructs with an exception message and an inner exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |
| e | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Inner exception |

<a name='T-SC4Parser-Subfiles-RegionViewSubfile'></a>
## RegionViewSubfile `type`

##### Namespace

SC4Parser.Subfiles

##### Summary

Region View Subfile (partial implementation). Contains basic city information from a region point of view.

##### Example

`
             // Simple usage
             // (Just assume the region view subfile has already been read, see SC4SaveGame.GetRegionViewSubfile())
            
             // Access some data
             Console.WriteLine("city location x={0} y={1}",
                 regionViewSubfile.TileXLocation,
                 regionViewSubfile.TileYLocation);
 `

##### Remarks

Only a partial implementation and will not contain all values from the save game
 
 Based off spec from here: https://wiki.sc4devotion.com/index.php?title=Region_View_Subfiles

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-CitySizeX'></a>
### CitySizeX `property`

##### Summary

X size of the city

##### Remarks

Multiplied by 64 to get the number of the tiles in the city

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-CitySizeY'></a>
### CitySizeY `property`

##### Summary

Y size of the city

##### Remarks

Multiplied by 64 to get the number of the tiles in the city

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-CommercialPopulation'></a>
### CommercialPopulation `property`

##### Summary

Commercial population of city

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-IndustrialPopulation'></a>
### IndustrialPopulation `property`

##### Summary

Industrial population of city

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-MajorVersion'></a>
### MajorVersion `property`

##### Summary

Major version of the subfile

##### Remarks

You can see the different versions here: https://www.wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
This implementation is based around the SimCity 4 Rush Hour/Deluxe version of the game (1.13)

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-MinorVersion'></a>
### MinorVersion `property`

##### Summary

Minor version of the subfile

##### Remarks

W
You can see the different versions here: https://www.wiki.sc4devotion.com/index.php?title=Region_View_Subfiles
This implementation is based around the SimCity 4 Rush Hour/Deluxe version of the game (1.13)

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-ResidentialPopulation'></a>
### ResidentialPopulation `property`

##### Summary

Residential population of city

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-TileXLocation'></a>
### TileXLocation `property`

##### Summary

X location of the city in the region view

<a name='P-SC4Parser-Subfiles-RegionViewSubfile-TileYLocation'></a>
### TileYLocation `property`

##### Summary

Z location of the city in the region view

<a name='M-SC4Parser-Subfiles-RegionViewSubfile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the Region View Subfile

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Subfiles-RegionViewSubfile-Parse-System-Byte[]-'></a>
### Parse(buffer) `method`

##### Summary

Parses Region View Subfile from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read subfile from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Files-SC4SaveFile'></a>
## SC4SaveFile `type`

##### Namespace

SC4Parser.Files

##### Summary

SC4 save game implementation, SC4 save files use the Maxis DBPF 1.1 file format

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing savegame");
                return;
            }
            
            // Get DBPF file version
            Console.WriteLine("DBPF Version {0}.{1}",
                savegame.Header.MajorVersion,
                savegame.Header.MinorVersion);
`

##### Remarks

This is a dud, inherited from DatabasePackedFile where the actual functionality resides
Included for simplicity when referring to SC4saves, also contains methods for loading common subfiles in SC4 saves

Because the SC4SaveFile object is inherited from the DatabasePackedFile object
it can be used and functions exactly the same and all the functions and examples in the DatabasePackedFile object apply
to SC4SaveFile.

<a name='M-SC4Parser-Files-SC4SaveFile-#ctor-System-String-'></a>
### #ctor(path) `constructor`

##### Summary

Default constructor for SC4Save, that takes a save game's path to load from

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path to Simcity 4 save game to load |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.DBPFParsingException](#T-SC4Parser-DBPFParsingException 'SC4Parser.DBPFParsingException') | Thrown when an exception occurs while loading the savegame file |

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            // You can now access and load data from the save game
            // using LoadIndexEntry or accessing the Index Entries directly:
            foreach (IndexEntry entry in savegame.IndexEntries)
            {
                Console.WriteLine(entry.TGI);
            }
`

<a name='M-SC4Parser-Files-SC4SaveFile-GetBuildingSubfile'></a>
### GetBuildingSubfile() `method`

##### Summary

Retrieves Building Subfile from the SC4 save game

##### Returns

Building subfile from the SC4 save

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.SubfileNotFoundException](#T-SC4Parser-SubfileNotFoundException 'SC4Parser.SubfileNotFoundException') | Returns when there is an issue with loading or finding the subfile |

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            BuildingSubfile buildings = null
            try 
            {
                buildings = savegame.GetBuildingSubfile();
            }
            catch (SubfileNotFoundException)
            {
                Console.Writeline("Could not find subfile");
            }
`

##### See Also

- [SC4Parser.DataStructures.Building](#T-SC4Parser-DataStructures-Building 'SC4Parser.DataStructures.Building')

<a name='M-SC4Parser-Files-SC4SaveFile-GetLotSubfile'></a>
### GetLotSubfile() `method`

##### Summary

Retrieves Lot Subfile from the SC4 save game

##### Returns

Lot subfile from the SC4 save

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.SubfileNotFoundException](#T-SC4Parser-SubfileNotFoundException 'SC4Parser.SubfileNotFoundException') | Returns when there is an issue with loading or finding the subfile |

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            LotSubfile lotFile = null
            try 
            {
                lotFile = savegame.GetLotSubfile();
            }
            catch (SubfileNotFoundException)
            {
                Console.Writeline("Could not find subfile");
            }
`

##### Remarks

Lot structure, used in Lots Subfile, is only partially implemented, so will not contain all values

##### See Also

- [SC4Parser.DataStructures.Lot](#T-SC4Parser-DataStructures-Lot 'SC4Parser.DataStructures.Lot')

<a name='M-SC4Parser-Files-SC4SaveFile-GetRegionViewSubfile'></a>
### GetRegionViewSubfile() `method`

##### Summary

Retrieves Region View Subfile from the SC4 save game

##### Returns

Region View Subfile from the SC4 save

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.SubfileNotFoundException](#T-SC4Parser-SubfileNotFoundException 'SC4Parser.SubfileNotFoundException') | Returns when there is an issue with loading or finding the subfile |

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            RegionViewSubfile regionView = null
            try 
            {
                regionView = savegame.GetRegionViewSubfile();
            }
            catch (SubfileNotFoundException)
            {
                Console.Writeline("Could not find subfile");
            }
`

##### Remarks

Region View Subfile is only partially implemented, so will not contain all values

<a name='M-SC4Parser-Files-SC4SaveFile-GetTerrainMapSubfile'></a>
### GetTerrainMapSubfile() `method`

##### Summary

Returns Terrain Map Subfile from the SC4 save game

##### Returns

Terrain Map Subfile from the SC4 save

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [SC4Parser.SubfileNotFoundException](#T-SC4Parser-SubfileNotFoundException 'SC4Parser.SubfileNotFoundException') | Returns when there is an issue with loading or finding the subfile |

##### Example

`
            // Load save game
            SC4SaveFile savegame;
            try
            {
                savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
            }
            catch (DBPFParsingException)
            {
                Console.Writeline("Issue occured while parsing DBPF");
                return;
            }
            
            TerrainMapSubfile terrainMap = null
            try 
            {
                terrainMap = savegame.GetTerrainMapSubfile();
            }
            catch (SubfileNotFoundException)
            {
                Console.Writeline("Could not find subfile");
            }
`

<a name='T-SC4Parser-DataStructures-SaveGameProperty'></a>
## SaveGameProperty `type`

##### Namespace

SC4Parser.DataStructures

##### Summary

Represents a Savegame Property (SIGPROP). SIGPROPs are small structures used to store individual entries of information for a given object.

##### Remarks

SIGPROPs are highly situational and their value and use depends on where they are used:
Take, for example, SIGPROPs being used for storing specific information about buildings (patient capacity for a hospital, dispatches available for a firestation, 
custom name given to a building). A SIGPROPs data can be several different types, they can also contain several values.
(Writing this parsing was a pain)

Implemented using the following: https://wiki.sc4devotion.com/index.php?title=Building_Subfile#Appendix_1:_Structure_of_SGPROP_.28SaveGame_Properties.29

<a name='P-SC4Parser-DataStructures-SaveGameProperty-Data'></a>
### Data `property`

##### Summary

Data that is stored in the SaveGame Property (SIGPROP)

<a name='P-SC4Parser-DataStructures-SaveGameProperty-DataRepeatedCount'></a>
### DataRepeatedCount `property`

##### Summary

Amount of data that is stored in the SaveGame Property (SIGPROP)

<a name='P-SC4Parser-DataStructures-SaveGameProperty-DataType'></a>
### DataType `property`

##### Summary

Data type stored in the SaveGame Property (SIGPROP)

##### Remarks

01=UInt8, 02=UInt16, 03=UInt32, 07=SInt32, 08=SInt64, 09=Float32, 0B=Boolean, 0C=String

<a name='P-SC4Parser-DataStructures-SaveGameProperty-KeyType'></a>
### KeyType `property`

##### Summary

Determines if there is repeated/multiple data in the SaveGame Property (SIGPROP)

<a name='P-SC4Parser-DataStructures-SaveGameProperty-PropertyNameValue'></a>
### PropertyNameValue `property`

##### Summary

SaveGame Property (SIGPROP) value, used to identify the use of the SIGPROP

##### Example

A SIGPROP with a Property Name Value of 0x899AFBAD is used to store a buildings custom name

<a name='P-SC4Parser-DataStructures-SaveGameProperty-PropertyNameValueCopy'></a>
### PropertyNameValueCopy `property`

##### Summary

SaveGame Property (SIGPROP) value copy, duplicated for unknown reason.

<a name='P-SC4Parser-DataStructures-SaveGameProperty-Unknown1'></a>
### Unknown1 `property`

##### Summary

Unknown SaveGame Property value

<a name='P-SC4Parser-DataStructures-SaveGameProperty-Unknown2'></a>
### Unknown2 `property`

##### Summary

Unknown SaveGame Property value

<a name='M-SC4Parser-DataStructures-SaveGameProperty-Dump'></a>
### Dump() `method`

##### Summary

Prints the values of SaveGame Property (SIGPROP)

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-DataStructures-SaveGameProperty-ExtractFromBuffer-System-Byte[],System-UInt32,System-UInt32@-'></a>
### ExtractFromBuffer(buffer,count,offset) `method`

##### Summary

Extracts a bunch of SaveGame Properties (SIGPROP) and then returns the new offset after everything has been read

##### Returns

A list of all parsed SIGPROPs

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read SIGPROPs from |
| count | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Number of SIGPROPs to try and read |
| offset | [System.UInt32@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32@ 'System.UInt32@') | Offset/position to start reading the SIGPROPs from in the data array |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='M-SC4Parser-DataStructures-SaveGameProperty-Parse-System-Byte[],System-Int32-'></a>
### Parse(buffer,offset) `method`

##### Summary

Loads an individual SaveGame Property (SIGPROP) from a byte array

##### Returns

The new offset/position after the SIGPROP has been read

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read the SIGPROP from |
| offset | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Position in the data array to start reading the SIGPROP from |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

##### Remarks

This parse functions works pretty similarly to other parse functions, but because they can return in the middle
of data entries, the calling function might need the current index after the data has been read so we return that.

The data buffer provided may contain multiple SIGPROPs but the method is only designed to read one

<a name='T-SC4Parser-SubfileNotFoundException'></a>
## SubfileNotFoundException `type`

##### Namespace

SC4Parser

##### Summary

Exception thrown when Subfile cannot be found

##### Remarks

Inner exception contains specific exception that occured

<a name='M-SC4Parser-SubfileNotFoundException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Constructor that constructs with an exception message

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |

<a name='M-SC4Parser-SubfileNotFoundException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,e) `constructor`

##### Summary

Construcotr that constructs with an exception message and an inner exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Exception message |
| e | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Inner exception |

<a name='T-SC4Parser-Subfiles-TerrainMapSubfile'></a>
## TerrainMapSubfile `type`

##### Namespace

SC4Parser.Subfiles

##### Summary

Implementation of TerrainMap Subfile, contains height information for each tile of a city.

##### Example

`
             // Simple usage
             // (Just assume the terrain map subfile has already been read, see SC4SaveGame.GetTerrainMapSubfile())
            
             // print out terrain map
             foreach (float x in terrainMapSubfile.Map)
             {
                 foreach (float y in terrainMapSubfile.Map[x])
                 {
                     Console.WriteLine(terrainMapSubfile.Map[x][y]);
                 }
             }
 `

##### Remarks

Based off the implmentation here:
 https://github.com/sebamarynissen/sc4/blob/master/lib/terrain-map.js

<a name='P-SC4Parser-Subfiles-TerrainMapSubfile-MajorVersion'></a>
### MajorVersion `property`

##### Summary

Major version of the subfile

<a name='P-SC4Parser-Subfiles-TerrainMapSubfile-Map'></a>
### Map `property`

##### Summary

Actual terrain map, contains a height value for each tile in the sity

##### Remarks

Stored in x and y of tiles

<a name='P-SC4Parser-Subfiles-TerrainMapSubfile-SizeX'></a>
### SizeX `property`

##### Summary

X size of the city

##### Remarks

Not included in actual file but borrowed from Region View Subfile for convience

<a name='P-SC4Parser-Subfiles-TerrainMapSubfile-SizeY'></a>
### SizeY `property`

##### Summary

Y size of the city

##### Remarks

Not included in actual file but borrowed from Region View Subfile for convience

<a name='M-SC4Parser-Subfiles-TerrainMapSubfile-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the Terrain Map Subfile

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Subfiles-TerrainMapSubfile-Parse-System-Byte[],System-UInt32,System-UInt32-'></a>
### Parse(buffer,xSize,ySize) `method`

##### Summary

Reads the Terrain Map Subfile from a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buffer | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to read subfile from |
| xSize | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Number of tiles on the X axis in the city |
| ySize | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Number of tiles on the Y axis in the city |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') | Thrown when trying to parse an element that is out of bounds in the data array |

<a name='T-SC4Parser-Types-TypeGroupInstance'></a>
## TypeGroupInstance `type`

##### Namespace

SC4Parser.Types

##### Summary

Implements TypeGroupInstance (TGI) identifier used to identify elements and files in a SimCity 4 savegame (DBPF)

##### Remarks

TypeGroupInstance (TGI) is used as an identifier for files within a SimCity 4 savegame (DBPF)
It consists of the items TypeID, GroupID and InstanceID. The combination of these fields creates
a unique reference for the file. 
It is used in comparisons a lot and the values for the fields are quite often referenced as hex 
so it is represented here with all the neccessary methods for usage, conversion and comparison
mainly for convience.
More info here: https://www.wiki.sc4devotion.com/index.php?title=Type_Group_Instance

<a name='M-SC4Parser-Types-TypeGroupInstance-#ctor-System-UInt32,System-UInt32,System-UInt32-'></a>
### #ctor(type,group,instance) `constructor`

##### Summary

TypeGroupInstance (TGI) constructor using uint values of IDs

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Items Type ID |
| group | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Items Group ID |
| instance | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Items Instance ID |

##### Example

`
            // Create Terrain Map Subfile's TGI
            TypeGroupInstance terrainMapTGI = new TypeGroupInstance(2849861620, 3918501157, 1);
            
            // Use tgi to load subfile from save
            sc4Save.LoadIndexEntry(terrainMapTGI);
`

##### See Also

- [SC4Parser.Types.TypeGroupInstance.#ctor](#M-SC4Parser-Types-TypeGroupInstance-#ctor-System-String,System-String,System-String- 'SC4Parser.Types.TypeGroupInstance.#ctor(System.String,System.String,System.String)')

<a name='M-SC4Parser-Types-TypeGroupInstance-#ctor-System-String,System-String,System-String-'></a>
### #ctor(type_hex,group_hex,instance_hex) `constructor`

##### Summary

TypeGroupInstance (TGI) constructor using string hex values of IDs

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Items Type ID |
| group_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Items Group ID |
| instance_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Items Instance ID |

##### Example

`
            // Create Terrain Map Subfile's TGI
            TypeGroupInstance terrainMapTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
            
            // Use tgi to load subfile from save
            sc4Save.LoadIndexEntry(terrainMapTGI);
`

##### Remarks

Don't include the 0x at the start of any hex

##### See Also

- [SC4Parser.Types.TypeGroupInstance.#ctor](#M-SC4Parser-Types-TypeGroupInstance-#ctor-System-UInt32,System-UInt32,System-UInt32- 'SC4Parser.Types.TypeGroupInstance.#ctor(System.UInt32,System.UInt32,System.UInt32)')

<a name='P-SC4Parser-Types-TypeGroupInstance-Group'></a>
### Group `property`

##### Summary

Group ID

<a name='P-SC4Parser-Types-TypeGroupInstance-Instance'></a>
### Instance `property`

##### Summary

Instance ID

<a name='P-SC4Parser-Types-TypeGroupInstance-Type'></a>
### Type `property`

##### Summary

Type ID

<a name='M-SC4Parser-Types-TypeGroupInstance-Dump'></a>
### Dump() `method`

##### Summary

Prints out the contents of the TypeGroupInstance (TGI)

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Types-TypeGroupInstance-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary

General equals comparitor for TypeGroupInstance (TGI)

##### Returns

returns `true` if objects are equal, `false` if they are not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object to compare against |

<a name='M-SC4Parser-Types-TypeGroupInstance-Equals-SC4Parser-Types-TypeGroupInstance-'></a>
### Equals(tgi) `method`

##### Summary

Specific equals compaitor for TypeGroupInstance (TGI)

##### Returns

returns `true` if objects are equal, `false` if they are not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tgi | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | TGI to compare against |

<a name='M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String-'></a>
### FromHex(type_hex) `method`

##### Summary

Creates a TypeGroupInstance (TGI) from a Type ID string

##### Returns

The created TGI

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item'ss Type ID |

<a name='M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String,System-String-'></a>
### FromHex(type_hex,group_hex) `method`

##### Summary

Creates a TypeGroupInstance (TGI) from a Type ID and Group ID strings

##### Returns

The created TGI

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item's Type ID |
| group_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item's Group ID |

<a name='M-SC4Parser-Types-TypeGroupInstance-FromHex-System-String,System-String,System-String-'></a>
### FromHex(type_hex,group_hex,instance_hex) `method`

##### Summary

Creates a TypeGroupInstance (TGI) from a Type ID, Group ID and Instance ID strings

##### Returns

The created TGI

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item's Type ID |
| group_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item's Group ID |
| instance_hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The item's Instance ID |

<a name='M-SC4Parser-Types-TypeGroupInstance-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

Get the hash for the TypeGroupInstance (TGI)

##### Returns

The hash value of the TGI

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Types-TypeGroupInstance-ToString'></a>
### ToString() `method`

##### Summary

Converts the TypeGroupInstance (TGI) to a string

##### Returns

String representation of the TGI

##### Parameters

This method has no parameters.

<a name='M-SC4Parser-Types-TypeGroupInstance-op_Equality-SC4Parser-Types-TypeGroupInstance,SC4Parser-Types-TypeGroupInstance-'></a>
### op_Equality(lhs,rhs) `method`

##### Summary

Equals comparitor, checks if the 2 provided objects are equal

##### Returns

returns `true` if objects are equal, `false` if they are not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lhs | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | Left hand side of comparison |
| rhs | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | Right habd sude if comparison |

<a name='M-SC4Parser-Types-TypeGroupInstance-op_Inequality-SC4Parser-Types-TypeGroupInstance,SC4Parser-Types-TypeGroupInstance-'></a>
### op_Inequality(lhs,rhs) `method`

##### Summary

Not equals comparitor, checks if the 2 provided objects are not equal

##### Returns

returns `true` if objects are not equal, `false` if they are not

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lhs | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | Left hand side of comparison |
| rhs | [SC4Parser.Types.TypeGroupInstance](#T-SC4Parser-Types-TypeGroupInstance 'SC4Parser.Types.TypeGroupInstance') | Right habd sude if comparison |

<a name='T-SC4Parser-Utils'></a>
## Utils `type`

##### Namespace

SC4Parser

<a name='M-SC4Parser-Utils-SaveByteArrayToFile-System-Byte[],System-String,System-String-'></a>
### SaveByteArrayToFile(data,name,path) `method`

##### Summary

Save a byte array to a file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to save |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of file to save |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path to save file to |

<a name='M-SC4Parser-Utils-UnixTimestampToDateTime-System-Int64-'></a>
### UnixTimestampToDateTime(unixTimestamp) `method`

##### Summary

Converts datetime to unixtime used as timestamps in saves

##### Returns

Converted Datatime

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| unixTimestamp | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | Unix timestamp to convert |

##### Remarks

Based on https://stackoverflow.com/a/250400

 Could use DateTimeOffset.FromUnixTimeSeconds from .NET 4.6 > but thought it was new enough
 That I would ensure a bit of backwards compatability
