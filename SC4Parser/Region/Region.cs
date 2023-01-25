using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

using SC4Parser.Logging;

namespace SC4Parser.Region
{
    using RGB = Bitmap.RGB;

    public class Region
    {
        public Building[] Buildings { get; private set; }
        public Lot[] Zones { get; private set; }
        public float[][] Terrain { get; private set; }

        public Dictionary<int, string> CityNameDictionary { get; private set; }

        public string[][] RegionLayout;

        public void Load(string path)
        {
            string configImagePath = Path.Combine(path, "config.bmp");
            if (!File.Exists(configImagePath))
            {
                Logger.Log(LogLevel.Error, "Could not find config.bmp file for region @ {0}", path);
                return;
            }
            // TODO: Wrap in try
            Bitmap regionBitmap = new Bitmap(configImagePath);

            RegionLayout = new string[regionBitmap.DiBHeader.Height][];
            for (int y = 0; y < regionBitmap.DiBHeader.Height; y++)
            {
                RegionLayout[y] = new string[regionBitmap.DiBHeader.Width];
                for (int x = 0; x < regionBitmap.DiBHeader.Width; x++)
                {
                    RGB pixelColor = regionBitmap.PixelMap[y][x];

                    if (pixelColor.R == 255)
                    {
                        // Small city
                    }
                    else if (pixelColor.G == 255)
                    {
                        // Medium city
                    }
                    else if (pixelColor.B == 255)
                    {
                        // Large city
                    }
                }
            }


            //using (Bitmap image = new Bitmap(path))
            //{
            //    for (int i = 0; i < image.Width; i++)
            //    {
            //        for (int j = 0; j < image.Height; j++)
            //        {
            //            Color pixel = image.GetPixel(i, j);
            //            // TODO: It's laid out with coords in region view + 1 as the coord
            //        }
            //    }
            //}
        }
    }
}
