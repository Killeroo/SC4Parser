﻿using System;

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

        public SC4SaveFile(string path) : base(path) { }

        /// <summary>
        /// Some methods for getting more common subfiles from a DBPF
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
                return null;
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
                return null;
            }

            BuildingSubfile buildingSubfile = new BuildingSubfile();
            byte[] lotSubfileData = LoadIndexEntry(buildingEntry.TGI);
            buildingSubfile.Parse(lotSubfileData, lotSubfileData.Length);

            Logger.Log(LogLevel.Info, "Buildings subfile loaded, caching result");
            m_CachedBuildingSubfile = buildingSubfile;

            return buildingSubfile;
        }
    }
}
