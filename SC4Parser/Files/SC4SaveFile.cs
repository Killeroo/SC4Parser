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
    /// </remarks>
    /// <see cref="SC4Parser.Files.DatabasePackedFile"/>
    public class SC4SaveFile : DatabasePackedFile
    {
        // Cached subfiles for easy access after they have been loaded the first time.
        // Saves loading and decompressing multiple times
        private LotSubfile m_CachedLotSubfile = null;
        private BuildingSubfile m_CachedBuildingSubfile = null;
        private RegionViewSubfile m_CachedRegionViewSubfile = null;
        private TerrainMapSubfile m_CachedTerrainMapSubfile = null;

        /// <summary>
        /// Default constructor for SC4Save, that takes a save game's path to load from
        /// </summary>
        /// <param name="path">Path to Simcity 4 save game to load</param>
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
        public LotSubfile GetLotSubfile()
        {
            if (m_CachedLotSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached lot subfile");
                return m_CachedLotSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching Lot Subfile...");
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
                Logger.Log(LogLevel.Error, "Could not find Lot Subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find Lot Subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load lots subfile");
                throw new SubfileNotFoundException($"Could not load Lots Subfile", e);
            }
        }
        /// <summary>
        /// Retrieves Building Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Building subfile from the SC4 save</returns>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        /// <see cref="SC4Parser.Subfiles.BuildingSubfile"/>
        /// <seealso cref="SC4Parser.DataStructures.Building"/>
        public BuildingSubfile GetBuildingSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached building subfile");
                return m_CachedBuildingSubfile;
            }

            try
            {
                Logger.Log(LogLevel.Info, "Fetching Building Subfile...");
                IndexEntry buildingEntry = FindIndexEntryWithType(Constants.BUILDING_SUBFILE_TYPE);
                if (buildingEntry == null)
                {
                    Logger.Log(LogLevel.Error, "Could not find Building Subfile");
                    throw new SubfileNotFoundException($"Could not find Building Subfile in {FilePath}");
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
                Logger.Log(LogLevel.Error, "Could not find Building Subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find Building Subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load building subfile");
                throw new SubfileNotFoundException($"Could not load Building Subfile", e);
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
        public RegionViewSubfile GetRegionViewSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached region view subfile");
                return m_CachedRegionViewSubfile;
            }

            try
            {
                RegionViewSubfile regionViewSubfile = new RegionViewSubfile();
                byte[] regionViewData = LoadIndexEntry(Constants.REGION_VIEW_SUBFILE_TGI);
                regionViewSubfile.Parse(regionViewData);

                Logger.Log(LogLevel.Info, "RegionView subfile loaded, caching result");
                m_CachedRegionViewSubfile = regionViewSubfile;

                return regionViewSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find RegionView Subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find RegionView Subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load RegionView subfile");
                throw new SubfileNotFoundException($"Could not load RegionView Subfile", e);
            }
        }
        /// <summary>
        /// Returns Terrain Map Subfile from the SC4 save game 
        /// </summary>
        /// <returns>Terrain Map Subfile from the SC4 save</returns>
        /// <exception cref="SC4Parser.SubfileNotFoundException">Returns when there is an issue with loading or finding the subfile</exception>
        public TerrainMapSubfile GetTerrainMapSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached terrain map subfile");
                return m_CachedTerrainMapSubfile;
            }

            try
            {
                // We need the city size from the region view in order to read the correct amount of data from the terrain map file
                var regionViewData = GetRegionViewSubfile();

                TerrainMapSubfile terrainMapSubfile = new TerrainMapSubfile();
                byte[] terrainMapData = LoadIndexEntry(Constants.TERRAIN_MAP_SUBFILE_TGI);
                terrainMapSubfile.Parse(terrainMapData, regionViewData.CitySizeX, regionViewData.CitySizeY);

                Logger.Log(LogLevel.Info, "Region View subfile loaded, caching result");
                m_CachedTerrainMapSubfile = terrainMapSubfile;

                return terrainMapSubfile;
            }
            catch (IndexEntryNotFoundException e)
            {
                Logger.Log(LogLevel.Error, "Could not find TerrainMap Subfile IndexEntry");
                throw new SubfileNotFoundException($"Could not find TerrainMap Subfile in {FilePath}", e);
            }
            catch (IndexEntryLoadingException e)
            {
                Logger.Log(LogLevel.Error, "Could not load TerrainMap subfile");
                throw new SubfileNotFoundException($"Could not load TerrainMap Subfile", e);
            }
        }
    }
}
