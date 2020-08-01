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
        public static readonly Color RESIDENTIAL_HIGH_COLOR = Color.FromArgb(46, 74, 35);
        public static readonly Color RESIDENTIAL_MEDIUM_COLOR = Color.FromArgb(82, 123, 54);
        public static readonly Color RESIDENTIAL_LOW_COLOR = Color.FromArgb(49, 102, 36);
        public static readonly Color COMMERCIAL_HIGH_COLOR = Color.FromArgb(62, 69, 92);
        public static readonly Color COMMERCIAL_MEDIUM_COLOR = Color.FromArgb(101, 117, 110);
        public static readonly Color COMMERCIAL_LOW_COLOR = Color.FromArgb(76, 97, 90);
        public static readonly Color INDUSTRIAL_HIGH_COLOR = Color.FromArgb(131, 121, 60);
        public static readonly Color INDUSTRIAL_MEDIUM_COLOR = Color.FromArgb(128, 123, 63);
        public static readonly Color INDUSTRIAL_LOW_COLOR = Color.FromArgb(101, 111, 47);

        static void Main(string[] args)
        {
            FileInfo file = new FileInfo("lot_decompressed");
            LotSubFile l = new LotSubFile();
            l.Parse(File.ReadAllBytes("lot_decompressed"), (int)file.Length);

            CreateGridImage(128, 128, 9, 9, 60, l);

    }

        public static void CreateGridImage(int maxXCells, int maxYCells, int cellXPosition, int cellYPosition, int boxSize, LotSubFile lots)
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

                    foreach (var lot in lots.Lots)
                    {
                        //Rectangle rect = new Rectangle((boxSize * lot.MaxTileX) + 5, (boxSize * lot.MaxTileZ) + 5, boxSize - 10, boxSize - 10);
                        //Rectangle rect2 = new Rectangle((boxSize * lot.MinTileX) + 5, (boxSize * lot.MinTileZ) + 5, boxSize - 10, boxSize - 10);


                        //Rectangle rect = new Rectangle((boxSize * lot.MaxTileX) + 5, (boxSize * lot.MaxTileZ) + 5, boxSize - 10, boxSize - 10);

                        Rectangle rect = new Rectangle();

                        //Rectangle r = Rectangle.FromLTRB(boxSize * lot.MinTileX, (boxSize * lot.MinTileX) + (boxSize * lot.SizeX),
                        //                                 boxSize * lot.MaxTileX, (boxSize * lot.MaxTileZ)
                        //int startX = 0;
                        //int startZ = 0;
                        //int sizeX = 1;
                        //int sizeZ = 1;

                        //if (lot.MinTileX == lot.MaxTileX && lot.MinTileZ == lot.MaxTileZ)
                        //{
                        //    rect = new Rectangle((boxSize * lot.MaxTileX) + 5, (boxSize * lot.MaxTileZ) + 5, boxSize - 10, boxSize - 10);
                        //}

                        //if (lot.MinTileX < lot.MaxTileX
                        //    && lot.MinTileZ > lot.MaxTileZ)
                        //{
                        //    sizeX = boxSize * lot.SizeX;
                        //    startX = lot.MinTileX;
                        //}

                        byte otherRectCorner1X = 0;
                        byte otherRectCorner1Z = 0;
                        
                        byte otherRectCorner2X = 0;
                        byte otherRectCorner2Z = 0;

                        if (lot.MinTileZ < lot.MaxTileZ)
                        {
                            otherRectCorner1Z = lot.MinTileZ;
                            otherRectCorner1X = lot.MaxTileX;
                        }
                        else
                        {
                            otherRectCorner1Z = lot.MaxTileZ;
                            otherRectCorner1X = lot.MinTileX;
                        }

                        if (lot.MinTileX < lot.MaxTileX)
                        {
                            otherRectCorner2X = lot.MinTileX;
                            otherRectCorner2Z = lot.MaxTileZ;
                        }
                        else
                        {
                            otherRectCorner2X = lot.MaxTileX;
                            otherRectCorner2Z = lot.MinTileZ;
                        }



                        //Console.WriteLine(lot.Offset.ToString("X"));
                        //Console.WriteLine("{0}x{1}", lot.MinTileX, lot.MaxTileX);
                        //Console.WriteLine("{0}x{1}", lot.MaxTileX, lot.MaxTileZ);
                        //Console.WriteLine("dep:{0} wid:{1}", lot.SizeZ, lot.SizeX);
                        //Console.WriteLine("-----------------");
                        //Console.ReadLine();




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

                        //Color c2 = new Color();
                        //switch (lot.Orientation)
                        //{
                        //    case Constants.ORIENTATION_NORTH:
                        //        c2 = Color.FromArgb(255, 100, 0);
                        //        break;

                        //    case Constants.ORIENTATION_EAST:

                        //        c2 = Color.FromArgb(0, 255, 100);
                        //        break;

                        //    case Constants.ORIENTATION_SOUTH:

                        //        c2 = Color.FromArgb(100, 0, 255);
                        //        break;

                        //    case Constants.ORIENTATION_WEST:

                        //        c2 = Color.FromArgb(0, 0, 0);
                        //        break;
                        //}


                        g.FillRectangle(new SolidBrush(c), rect);
                        //c = Color.FromArgb(c.R, c.G, 200);
                        //g.FillRectangle(new SolidBrush(c2), rect2);
                    }

                    // draw cross
                    
                    for (int i = 0; i <= maxXCells; i++)
                    {
                        g.DrawLine(pen, (i * boxSize), 0, i * boxSize, boxSize * maxYCells);
                    }

                    for (int i = 0; i <= maxYCells; i++)
                    {
                        g.DrawLine(pen, 0, (i * boxSize), boxSize * maxXCells, i * boxSize);
                    }
                }

                bmp.Save("test.png", ImageFormat.Png);

                //var memstream = new MemoryStream();
                //bmp.Save(memstream, Image)
            }
        }
    }
}
