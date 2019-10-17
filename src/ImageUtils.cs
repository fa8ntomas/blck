using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BLEditor
{
    class ImageUtils
    {
        public static bool HasMoreThanFiveColors(Image image)
        {
            HashSet<Color> colorsSet = new HashSet<Color>();

            if (image != null)
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            colorsSet.Add(bmp.GetPixel(x, y));

                            if (colorsSet.Count > 5)
                            {
                                return true;
                            }
                        }
                    }
                }
                 
            }

            return false;
        }

        public static Bitmap CropImage(Image source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.AssumeLinear;
            g.SmoothingMode = SmoothingMode.None;
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
    }
}
