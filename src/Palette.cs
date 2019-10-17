using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    class Palette
    {
        private byte[] palette;

        private Palette(byte[] palette) => this.palette = (byte[])palette.Clone();

        private static Palette defaultPalette;

        public static Palette GetDefaultPalette()
        {
            if (defaultPalette != null)
            {
                return defaultPalette;
            }
            else
            {
                String PaletteFullPath = Properties.Settings.Default["PaletteFullPath"].ToString();
                if (File.Exists(PaletteFullPath))
                {
                    byte[] palette = File.ReadAllBytes(PaletteFullPath);
                    if (palette.Length == 768)
                    {
                        return defaultPalette = new Palette(palette);
                    }
                }
              
                return defaultPalette = new Palette(Properties.Resources.DefaultPalette);
            }
        }

        public Color GetColorFromAtariColorValue(byte atariColorValue)
        {
           return Color.FromArgb(255, palette[atariColorValue*3], palette[atariColorValue*3 + 1], palette[atariColorValue*3 + 2]);
        }

        // https://stackoverflow.com/questions/3968179/compare-rgb-colors-in-c-sharp
        private static double ColourDistance(Color e1, Color e2)
        {
            long rmean = ((long)e1.R + (long)e2.R) / 2;
            long r = (long)e1.R - (long)e2.R;
            long g = (long)e1.G - (long)e2.G;
            long b = (long)e1.B - (long)e2.B;
            return Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
        }

        public byte GetNearestColor(Color c)
        {
            byte result = 0;
            double d = ColourDistance(c, GetColorFromAtariColorValue(result));
            byte index = result;
            do
            {
                index++;
                double d2 = ColourDistance(c, GetColorFromAtariColorValue(index));
                if (d2 < d)
                {
                    result = index;
                    d = d2;
                }
            } while (index < 255);

            return (byte)result;
        }
    }


   

}
