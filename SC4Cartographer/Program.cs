using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using SC4Parser;
using SC4Parser.Structures;
using SC4Parser.Structures.Files.SubFiles;

namespace SC4Cartographer
{
    class Program
    {
        public static readonly Color BACKGROUND_COLOR = Color.FromArgb(64, 64, 64);
        public static readonly Color INTERNAL_GRID_COLOR = Color.FromArgb(32, 32, 95);
        //public static Color EXTERNAL_GRID_COLOR;
        public static readonly Color BUILDING_COLOR = Color.FromArgb(121, 121, 121);

        // TODO: Colours aren't perfect (picking the color in paint uses the colour of the background
        // find pure colours
        // TODO: Use highlights of zones
        public static readonly Color RESIDENTIAL_HIGH_COLOR = Color.FromArgb(0, 126, 47);
        public static readonly Color RESIDENTIAL_MEDIUM_COLOR = Color.FromArgb(2, 207, 79);
        public static readonly Color RESIDENTIAL_LOW_COLOR = Color.FromArgb(4, 255, 98);
        public static readonly Color COMMERCIAL_HIGH_COLOR = Color.FromArgb(4, 1, 128);
        public static readonly Color COMMERCIAL_MEDIUM_COLOR = Color.FromArgb(1, 93, 188);
        public static readonly Color COMMERCIAL_LOW_COLOR = Color.FromArgb(0, 126, 255);
        public static readonly Color INDUSTRIAL_HIGH_COLOR = Color.FromArgb(103, 103, 22);
        public static readonly Color INDUSTRIAL_MEDIUM_COLOR = Color.FromArgb(129, 129, 43);
        public static readonly Color INDUSTRIAL_LOW_COLOR = Color.FromArgb(180, 180, 46);

        static void Main(string[] args)
        {
            FileInfo file = new FileInfo("lot_decompressed");
            LotSubFile l = new LotSubFile();
            l.Parse(File.ReadAllBytes("lot_decompressed"), (int)file.Length);
            

            //128 for mid
            // 256 for large
            CreateGridImage(256, 256, 60, l);

    }

        public static void CreateGridImage(int maxXCells, int maxYCells, int boxSize, LotSubFile lots)
        {
            using (Bitmap bmp = new Bitmap(maxXCells * boxSize + 1, maxYCells * boxSize + 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(BACKGROUND_COLOR);
                    Pen pen = new Pen(INTERNAL_GRID_COLOR);
                    pen.Width = 1;

                    // Draw rectangle red rectangle
                    //Rectangle rect = new Rectangle(boxSize * (cellXPosition - 1), boxSize * (cellYPosition), boxSize, boxSize);
                    //g.FillRectangle(new SolidBrush(BUILDING_COLOR), rect);

                    // TODO: make the zones a bit smaller than the grid size
                    // TODO: work out width and heigh
                    int count = 0;
                    foreach (var lot in lots.Lots)
                    {
                        Rectangle rect = new Rectangle();

                        Color c = new Color();
                        switch (lot.ZoneType)
                        {
                            case SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_LOW:
                                c = RESIDENTIAL_LOW_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_MEDIUM:
                                c = RESIDENTIAL_MEDIUM_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_RESIDENTIAL_HIGH:
                                c = RESIDENTIAL_HIGH_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_LOW:
                                c = COMMERCIAL_HIGH_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_MEDIUM:
                                c = COMMERCIAL_MEDIUM_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_COMMERCIAL_HIGH:
                                c = COMMERCIAL_LOW_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_LOW:
                                c = INDUSTRIAL_LOW_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_MEDIUM:
                                c = INDUSTRIAL_MEDIUM_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_INDUSTRIAL_HIGH:
                                c = INDUSTRIAL_HIGH_COLOR;
                                break;

                            case SC4Parser.Constants.LOT_ZONE_TYPE_PLOPPED_BUILDING:
                                c = BUILDING_COLOR;
                                break;
                        }

                        switch (lot.Orientation)
                        {
                            case Constants.ORIENTATION_NORTH:
                            case Constants.ORIENTATION_SOUTH:
                                rect = new Rectangle((boxSize * lot.MinTileX) + 5, (boxSize * lot.MinTileZ) + 5, (boxSize * lot.SizeX) - 10, (boxSize * lot.SizeZ) - 10);
                                break;

                            case Constants.ORIENTATION_WEST:
                            case Constants.ORIENTATION_EAST:
                                rect = new Rectangle((boxSize * lot.MinTileX) + 5, (boxSize * lot.MinTileZ) + 5, (boxSize * lot.SizeZ) - 10, (boxSize * lot.SizeX) - 10);


                                break;

                        }

                        g.FillRectangle(new SolidBrush(c), rect);
                    }
                    
                }

                bmp.Save("test.png", ImageFormat.Png);

                //var memstream = new MemoryStream();
                //bmp.Save(memstream, Image)
            }
        }
    }
}
