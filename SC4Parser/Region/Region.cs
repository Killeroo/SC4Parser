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
        public Building[] Buildings { get; private set; }
        public Lot[] Zones { get; private set; }
        public float[][] Terrain { get; private set; }

        public Dictionary<int, string> CityNameDictionary { get; private set; }


        public void Load(string path)
        {
            string configImagePath = Path.Combine(path, "config.bmp");
            if (!File.Exists(configImagePath))
            {
                Logger.Log(LogLevel.Error, "Could not find config.bmp file for region @ {0}", path);
                return;
            }

            Bitmap b = new Bitmap(configImagePath);

          
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
