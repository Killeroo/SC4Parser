using System;

using SC4Parser.Logging;
using SC4Parser.DataStructures;
using SC4Parser.Subfiles;

namespace SC4Parser.Files
{
    /// <summary>
    /// SC4 save game implementation, SC4 save files use the Maxis DBPF 1.1 file format
    /// </summary>
    /// <remarks>
    /// This is a dud, inherited from DatabasePackedFile where the actual functionality resides
    /// Included for simplicity when referring to SC4saves, also contains methods for loading common subfiles in SC4 saves
    /// 
    /// Because the SC4SaveFile object is inherited from the DatabasePackedFile object
    /// it can be used and functions exactly the same and all the functions and examples in the DatabasePackedFile object apply
    /// to SC4SaveFile.
    /// </remarks>
    /// <see cref="SC4Parser.Files.DatabasePackedFile"/>
    /// <example>
    /// <c>
    /// // Load save game
    /// SC4SaveFile savegame;
    /// try
    /// {
    ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
    /// }
    /// catch (DBPFParsingException)
    /// {
    ///     Console.Writeline("Issue occured while parsing savegame");
    ///     return;
    /// }
    /// 
    /// // Get DBPF file version
    /// Console.WriteLine("DBPF Version {0}.{1}",
    ///     savegame.Header.MajorVersion,
    ///     savegame.Header.MinorVersion);
    /// </c>
    /// </example>
    public class SC4SaveFile : DatabasePackedFile
    {
        // Cached subfiles for easy access after they have been loaded the first time.
        // Saves loading and decompressing multiple times
        private LotSubfile m_CachedLotSubfile = null;
        private BuildingSubfile m_CachedBuildingSubfile = null;
        private RegionViewSubfile m_CachedRegionViewSubfile = null;
        private TerrainMapSubfile m_CachedTerrainMapSubfile = null;
        private NetworkSubfile1 m_CachedNetworkSubfile1 = null;

        /// <summary>
        /// Default constructor for SC4Save, that takes a save game's path to load from
        /// </summary>
        /// <param name="path">Path to Simcity 4 save game to load</param>
        /// <exception cref="SC4Parser.DBPFParsingException">Thrown when an exception occurs while loading the savegame file</exception>
        /// <example>
        /// <c>
        /// // Load save game
        /// SC4SaveFile savegame;
        /// try
        /// {
        ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// // You can now access and load data from the save game
        /// // using LoadIndexEntry or accessing the Index Entries directly:
        /// foreach (IndexEntry entry in savegame.IndexEntries)
        /// {
        ///     Console.WriteLine(entry.TGI);
        /// }
        /// </c>
        /// </example>
        public SC4SaveFile(string path) : base(path) { }

        /// <summary>
        /// Retrieves Lot Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Lot subfile from the SC4 save</returns>
        /// <remarks>
        /// Lot structure, used in Lots Subfile, is only partially implemented, so will not contain all values
        /// </remarks>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        /// <see cref="SC4Parser.Subfiles.LotSubfile"/>
        /// <seealso cref="SC4Parser.DataStructures.Lot"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// SC4SaveFile savegame;
        /// try
        /// {
        ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// LotSubfile lotFile = null
        /// try 
        /// {
        ///     lotFile = savegame.GetLotSubfile();
        /// }
        /// catch (SubfileNotFoundException)
        /// {
        ///     Console.Writeline("Could not find subfile");
        /// }
        /// </c>
        /// </example>
        public LotSubfile GetLotSubfile()
        {
            if (m_CachedLotSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached Lots subfile");
                return m_CachedLotSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching Lots subfile...");

                IndexEntry lotEntry = FindIndexEntryWithType(Constants.LOT_SUBFILE_TYPE);

                LotSubfile lotSubfile = new LotSubfile();
                byte[] lotSubfileData = LoadIndexEntry(lotEntry.TGI);
                lotSubfile.Parse(lotSubfileData, lotSubfileData.Length);

                Logger.Log(LogLevel.Info, "Lots subfile loaded, caching result");
                m_CachedLotSubfile = lotSubfile;

                return lotSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find Lots subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find Lot subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load Lots subfile");
                throw new SubfileNotFoundException($"Could not load Lots subfile", e);
            }
        }
        /// <summary>
        /// Retrieves Building Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Building subfile from the SC4 save</returns>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        /// <see cref="SC4Parser.Subfiles.BuildingSubfile"/>
        /// <seealso cref="SC4Parser.DataStructures.Building"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// SC4SaveFile savegame;
        /// try
        /// {
        ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// BuildingSubfile buildings = null
        /// try 
        /// {
        ///     buildings = savegame.GetBuildingSubfile();
        /// }
        /// catch (SubfileNotFoundException)
        /// {
        ///     Console.Writeline("Could not find subfile");
        /// }
        /// </c>
        /// </example>
        public BuildingSubfile GetBuildingSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached Buildings subfile");
                return m_CachedBuildingSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching Buildings subfile...");

                IndexEntry buildingEntry = FindIndexEntryWithType(Constants.BUILDING_SUBFILE_TYPE);
                if (buildingEntry == null)
                {
                    Logger.Log(LogLevel.Error, "Could not find Buildings subfile");
                    throw new SubfileNotFoundException($"Could not find Building subfile in {FilePath}");
                }

                BuildingSubfile buildingSubfile = new BuildingSubfile();
                byte[] buildingSubfileData = LoadIndexEntry(buildingEntry.TGI);
                buildingSubfile.Parse(buildingSubfileData, buildingSubfileData.Length);

                Logger.Log(LogLevel.Info, "Buildings subfile loaded, caching result");
                m_CachedBuildingSubfile = buildingSubfile;

                return buildingSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find Buildings subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find Building subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load Buildings subfile");
                throw new SubfileNotFoundException($"Could not load Building subfile", e);
            }

        }
        /// <summary>
        /// Retrieves Region View Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Region View Subfile from the SC4 save</returns>
        /// <remarks>
        /// Region View Subfile is only partially implemented, so will not contain all values
        /// </remarks>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        /// <see cref="SC4Parser.Subfiles.RegionViewSubfile"/>
        /// <example>
        /// <c>
        /// // Load save game
        /// SC4SaveFile savegame;
        /// try
        /// {
        ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// RegionViewSubfile regionView = null
        /// try 
        /// {
        ///     regionView = savegame.GetRegionViewSubfile();
        /// }
        /// catch (SubfileNotFoundException)
        /// {
        ///     Console.Writeline("Could not find subfile");
        /// }
        /// </c>
        /// </example>
        public RegionViewSubfile GetRegionViewSubfile()
        {
            if (m_CachedRegionViewSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached RegionView subfile");
                return m_CachedRegionViewSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching RegionView subfile...");

                RegionViewSubfile regionViewSubfile = new RegionViewSubfile();
                byte[] regionViewData = LoadIndexEntry(Constants.REGION_VIEW_SUBFILE_TGI);
                regionViewSubfile.Parse(regionViewData);

                Logger.Log(LogLevel.Info, "RegionView Subfile loaded, caching result");
                m_CachedRegionViewSubfile = regionViewSubfile;

                return regionViewSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find RegionView subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find RegionView Subfsubfileile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load RegionView subfile");
                throw new SubfileNotFoundException($"Could not load RegionView subfile", e);
            }
        }
        /// <summary>
        /// Returns Terrain Map Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Terrain Map Subfile from the SC4 save</returns>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        /// <example>
        /// <c>
        /// // Load save game
        /// SC4SaveFile savegame;
        /// try
        /// {
        ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
        /// }
        /// catch (DBPFParsingException)
        /// {
        ///     Console.Writeline("Issue occured while parsing DBPF");
        ///     return;
        /// }
        /// 
        /// TerrainMapSubfile terrainMap = null
        /// try 
        /// {
        ///     terrainMap = savegame.GetTerrainMapSubfile();
        /// }
        /// catch (SubfileNotFoundException)
        /// {
        ///     Console.Writeline("Could not find subfile");
        /// }
        /// </c>
        /// </example>
        public TerrainMapSubfile GetTerrainMapSubfile()
        {
            if (m_CachedTerrainMapSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached TerrainMap subfile");
                return m_CachedTerrainMapSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching TerrainMap subfile...");

                // We need the city size from the region view in order to read the correct amount of data from the terrain map file
                var regionViewData = GetRegionViewSubfile();

                TerrainMapSubfile terrainMapSubfile = new TerrainMapSubfile();
                byte[] terrainMapData = LoadIndexEntry(Constants.TERRAIN_MAP_SUBFILE_TGI);
                terrainMapSubfile.Parse(terrainMapData, regionViewData.CitySizeX, regionViewData.CitySizeY);

                Logger.Log(LogLevel.Info, "TerrainMap subfile loaded, caching result");
                m_CachedTerrainMapSubfile = terrainMapSubfile;

                return terrainMapSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find TerrainMap subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find TerrainMap subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load TerrainMap subfile");
                throw new SubfileNotFoundException($"Could not load TerrainMap subfile", e);
            }
        }

        public NetworkSubfile1 GetNetworkSubfile1()
        {
            if (m_CachedTerrainMapSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached Network subfile 1");
                return m_CachedNetworkSubfile1;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching Network subfile 1...");

                IndexEntry networkEntry = FindIndexEntryWithType(Constants.NETWORK_SUBFILE_1_TYPE);
                if (networkEntry == null)
                {
                    Logger.Log(LogLevel.Error, "Could not find Network subfile 1");
                    throw new SubfileNotFoundException($"Could not find Network subfile 1 in {FilePath}");
                }

                NetworkSubfile1 networkFile = new NetworkSubfile1();
                byte[] networkSubfileData = LoadIndexEntry(networkEntry.TGI);
                networkFile.Parse(networkSubfileData, networkSubfileData.Length);

                Logger.Log(LogLevel.Info, "Buildings subfile loaded, caching result");
                m_CachedNetworkSubfile1 = networkFile;

                return networkFile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find Network subfile 1 IndexEntry");
                throw new SubfileNotFoundException($"Could not find Network subfile 1 subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load Network subfile 1 ");
                throw new SubfileNotFoundException($"Could not load Network subfile 1 ", e);
            }
        }

        //GetList of terrain tiles
    }
}
