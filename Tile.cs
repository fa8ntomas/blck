using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BLEditor
{
    public class Tile : Label
    {
        public byte[] tbytes;
        Color pfColor;
        Font labelFont = new Font("Arial", 5F);

        Color colbak = Color.FromArgb(192, 192, 192);
        Color colpf0 = Color.FromArgb(128, 128, 128);
        Color colpf1 = Color.FromArgb(255, 0, 0);
        Color colpf2 = Color.FromArgb(255, 255, 255);
        Color colpf3 = Color.FromArgb(0, 0, 0);


        public Tile(int i, byte[] bytes, RbgPFColors colorArray)//Create Char Set Tiles
        {
            tbytes = bytes;
            ColorArray = colorArray;
            Text = i.ToString("X2");
            TMap = CreateBitmap(4, 16, 32);
            Margin = new Padding(1, 1, 1, 1);
            Name = i.ToString();
            Font = labelFont;
            Size = new Size(16, 32);
        }

        public Tile(int i)//Create Map tiles
        {      
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(0, 0, 0, 0);
            Name = i.ToString();
            Font = labelFont;
            Size = new Size(16, 32);
        }

        public Bitmap CreateBitmap(int scale, int xSize, int ySize)
        {
            int hexy = Convert.ToInt32(Text, 16);
            Bitmap initImage = new Bitmap(xSize, ySize);
            int count = 0;

            for (int y = 0; y < 8; y++)//Eight rows
            {
                for (int x = 0; x < 4; x++)//Four colors per row
                {
                    int value = GetValueFromByte(x, tbytes[count]);
                    bool inverse = hexy > 127 ? true : false;

                    pfColor = GetColorFromValue(value, inverse);

                    for (int n = 0; n < scale; n++) //scaled y value
                    {
                        for (int k = 0; k < scale; k++) //scaled x value
                        {
                            initImage.SetPixel(x * scale + k, y * scale + n, pfColor);
                        }
                    }
                }
                count++;
            }

            return initImage;
        }

        private int GetValueFromByte(int b, byte byteToConvert)
        {
            return (byteToConvert >> (6 - (b * 2)) & 0X03);
        }

        private Color GetColorFromValue(int byteValue, bool inverse)
        {
            if (ColorArray == null)
            {
                switch (byteValue)
                {
                    case 0:
                        return colbak;
                    case 1:
                        return colpf0;
                    case 2:
                        return colpf1;
                    case 3:
                        if(inverse)
                        {
                            return colpf3;
                        }
                        else
                        {
                            return colpf2;
                        }
                    default:
                        return colbak;
                }
            }
            else
            {
                switch (byteValue)
                {
                    case 0:
                        return ColorArray.Colbk;
                    case 1:
                        return ColorArray.Colpf0;
                    case 2:
                        return ColorArray.Colpf1;
                    case 3:
                        if(inverse)
                        {
                            return ColorArray.Colpf3;
                        }
                        else
                        {
                            return ColorArray.Colpf2;
                        }
                        
                    default:
                        return ColorArray.Colbk;
                }
            }

        }

        public Bitmap TMap { get; set; }

        public RbgPFColors ColorArray { get; set; }
    }
}
