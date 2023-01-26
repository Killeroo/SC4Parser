using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

using SC4Parser.Logging;

namespace SC4Parser.Region
{
    public class Region
    {
        public List<Building> Buildings { get; private set; } = new List<Building>();
        public List<Lot> Zones { get; private set; } = new List<Lot>();
        public float[][] Terrain { get; private set; }

        public Dictionary<int, string> CityNameDictionary { get; private set; }

        public string[][] RegionLayout;
        // TODO: Save an offset for building locations etc

        public void Load(string path)
        {
            string configImagePath = Path.Combine(path, "config.bmp");
            if (!File.Exists(configImagePath))
            {
                Logger.Log(LogLevel.Error, "Could not find config.bmp file for region @ {0}", path);
                return;
            }

            // Parse the region bitmap
            // TODO: Wrap in try
            Bitmap regionBitmap = new Bitmap(configImagePath);

            // Construct an empty region map
            RegionLayout = new string[regionBitmap.DiBHeader.Height][];
            for (int y = 0; y < regionBitmap.DiBHeader.Height; y++)
            {
                RegionLayout[y] = new string[regionBitmap.DiBHeader.Width];
                for (int x = 0; x < regionBitmap.DiBHeader.Width; x++)
                {
                    // Leave this blank for now
                    RegionLayout[y][x] = String.Empty;
                }
            }

            // Create blank terrain map for region
            Terrain = new float[regionBitmap.DiBHeader.Height * 64][];
            for (int y = 0; y < regionBitmap.DiBHeader.Height * 64; y++)
            {
                Terrain[y] = new float[regionBitmap.DiBHeader.Width * 64];
            }

            // Get all save games at path
            int tilesProcessed = 0;
            foreach (string file in Directory.GetFiles(path, "*.sc4"))
            {
                using (SC4SaveFile save = new SC4SaveFile(file))
                {
                    // TODO: Access manually
                    RegionViewSubfile regionView = save.GetRegionViewSubfile();

                    // Fill out the the tiles it occupies in the region map
                    int tileCount = (int)regionView.CitySizeY / 64;
                    for (int y = (int)regionView.TileYLocation; y < regionView.TileYLocation + tileCount; y++)
                    {
                        for (int x = (int)regionView.TileXLocation; x < regionView.TileXLocation + tileCount; x++)
                        {
                            RegionLayout[y][x] = Path.GetFileNameWithoutExtension(save.FilePath);
                            tilesProcessed++;
                        }
                    }

                    // Ok first add the terrain data to the correct part of the region terrain map
                    float[][] cityTerrainMap = save.GetTerrainMapSubfile().Map;
                    for (int x = 0; x < regionView.CitySizeX; x++)
                    {
                        for (int y = 0; y < regionView.CitySizeY; y++)
                        {
                            Terrain[(64 * (int)regionView.TileYLocation) + y][(64 * (int)regionView.TileXLocation) + x] = cityTerrainMap[y][x];
                        }
                    }

                    // Finally add the lots and buildings to region list (if they are present in city
                    if (save.ContainsLotSubfile()) Zones.AddRange(save.GetLotSubfile().Lots);
                    if (save.ContainsBuildingsSubfile()) Buildings.AddRange(save.GetBuildingSubfile().Buildings);
                }
            }

            // Check if we processed everything
            if (tilesProcessed != regionBitmap.DiBHeader.Width * regionBitmap.DiBHeader.Height)
            {
                Logger.Log(LogLevel.Warning, "Didn't process all tiles in config.bmp, region might not be properly constructed");
            }
        }
    }
}
