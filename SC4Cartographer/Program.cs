using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SC4Cartographer
{
    class Program
    {

        //struct CustomColour
        //{
        //    int r;
        //    int g;
        //    int b;
        //    int a;

        //    //void CustomColour(int r_, int g_, int b_)
        //    //{

        //    //}
        //}
        //public static Color BACKGROUND_COLOR = new Color;
        //public static Color INTERNAL_GRID_COLOR;
        //public static Color EXTERNAL_GRID_COLOR;
        //public static Color BUILDING_COLOR;

        static void Main(string[] args)
        {
            CreateGridImage(10, 10, 9, 9, 30);
        }

        public static void CreateGridImage(int maxXCells, int maxYCells, int cellXPosition, int cellYPosition, int boxSize)
        {
            Color backgroundColor;
            Color buildingColor;
            Color internalGridColor;

            using (Bitmap bmp = new Bitmap(maxXCells * boxSize + 1, maxYCells * boxSize + 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.FromArgb(64, 64, 64));
                    Pen pen = new Pen(Color.FromArgb(32, 32, 95));
                    pen.Width = 1;

                    // Draw rectangle red rectangle
                    Rectangle rect = new Rectangle(boxSize * (cellXPosition - 1), boxSize * (cellYPosition), boxSize, boxSize);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(121, 121, 121)), rect);

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
