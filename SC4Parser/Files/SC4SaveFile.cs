using System;

using SC4Parser.Logging;
using SC4Parser.DataStructures;
using SC4Parser.Subfiles;

namespace SC4Parser.Files
{
    /// <summary>
    /// SC4 save implementation, SC4 save files use the Maxis DBPF 1.1 file format
    /// This is a dud, inherited from DatabasePackedFile where the actual functionality resides
    /// Included for simplicity when referring to SC4saves
    /// also contains methods for loading common subfiles in SC4 saves
    /// </summary>
    public class SC4SaveFile : DatabasePackedFile
    {
        private LotSubfile m_CachedLotSubfile = null;
        private BuildingSubfile m_CachedBuildingSubfile = null;
        private RegionViewSubfile m_CachedRegionViewSubfile = null;
        private TerrainMapSubfile m_CachedTerrainMapSubfile = null;

        public SC4SaveFile(string path) : base(path) { }

        /// <summary>
        /// Some methods for getting more common subfiles from a Simcity 4 save
        /// </summary>
        public LotSubfile GetLotSubfile()
        {
            if (m_CachedLotSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached lot subfile");
                return m_CachedLotSubfile;
            }

            Logger.Log(LogLevel.Info, "Fetching Lot Subfile...");
            IndexEntry lotEntry = FindIndexEntryWithType(Constants.LOT_SUBFILE_TYPE);
            if (lotEntry == null)
            {
                Logger.Log(LogLevel.Error, "Could not find Lot Subfile");
                throw new Exception($"Could not find Lot Subfile in {FilePath}");
            }

            LotSubfile lotSubfile = new LotSubfile();
            byte[] lotSubfileData = LoadIndexEntry(lotEntry.TGI);
            lotSubfile.Parse(lotSubfileData, lotSubfileData.Length);


            Logger.Log(LogLevel.Info, "Lots subfile loaded, caching result");
            m_CachedLotSubfile = lotSubfile;

            return lotSubfile;
        }
        public BuildingSubfile GetBuildingSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached building subfile");
                return m_CachedBuildingSubfile;
            }

            Logger.Log(LogLevel.Info, "Fetching Building Subfile...");
            IndexEntry buildingEntry = FindIndexEntryWithType(Constants.BUILDING_SUBFILE_TYPE);
            if (buildingEntry == null)
            {
                Logger.Log(LogLevel.Error, "Could not find Building Subfile");
                throw new Exception($"Could not find Building Subfile in {FilePath}");
            }

            BuildingSubfile buildingSubfile = new BuildingSubfile();
            byte[] buildingSubfileData = LoadIndexEntry(buildingEntry.TGI);
            buildingSubfile.Parse(buildingSubfileData, buildingSubfileData.Length);

            Logger.Log(LogLevel.Info, "Buildings subfile loaded, caching result");
            m_CachedBuildingSubfile = buildingSubfile;

            return buildingSubfile;
        }
        public RegionViewSubfile GetRegionViewSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached building subfile");
                return m_CachedRegionViewSubfile;
            }

            RegionViewSubfile regionViewSubfile = new RegionViewSubfile();
            byte[] regionViewData = LoadIndexEntry(Constants.REGION_VIEW_SUBFILE_TGI);
            regionViewSubfile.Parse(regionViewData);

            Logger.Log(LogLevel.Info, "Region View subfile loaded, caching result");
            m_CachedRegionViewSubfile = regionViewSubfile;

            return regionViewSubfile;
        }
        public TerrainMapSubfile GetTerrainMapSubfile()
        {
            if (m_CachedBuildingSubfile != null)
            {
                Logger.Log(LogLevel.Info, "Returning cached terrain map subfile");
                return m_CachedTerrainMapSubfile;
            }

            var regionViewData = GetRegionViewSubfile();

            TerrainMapSubfile terrainMapSubfile = new TerrainMapSubfile();
            byte[] terrainMapData = LoadIndexEntry(Constants.TERRAIN_MAP_SUBFILE_TGI);
            terrainMapSubfile.Parse(terrainMapData, regionViewData.CitySizeX, regionViewData.CitySizeY);

            Logger.Log(LogLevel.Info, "Region View subfile loaded, caching result");
            m_CachedTerrainMapSubfile = terrainMapSubfile;

            return terrainMapSubfile;
        }
    }
}
