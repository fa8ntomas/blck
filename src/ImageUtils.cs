using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BLEditor
{
    class ImageUtils
    {
        // https://stackoverflow.com/questions/4779027/changing-the-opacity-of-a-bitmap-image
        public static Bitmap SetImageOpacity(Bitmap image, float opacity)
        {
            if (opacity >= 1f)
            {
                return image;
            }
            try
            {

                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (System.Exception ex)
            {
            //    MessageBox.Show(ex.Message);
                return null;
            }
        }
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
