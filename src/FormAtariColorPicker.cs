using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class FormAtariColorPicker : Form
    {
        public FormAtariColorPicker(byte? currentColor =null)
        {
            InitializeComponent();
            CreateBitmap();
            CurrentColor = currentColor;
        }

        private byte? currentColor;
        public byte? CurrentColor
        {
             get { return currentColor; }
            private set { currentColor = value; buttonOK.Enabled = currentColor.HasValue; if (currentColor.HasValue) { numericUpDown1.Value = currentColor.Value; } else { numericUpDown1.Text = ""; }; pictureColors.Invalidate(); }
        }

        int charW = 16;
        int charH = 16;
        int rowSpacing = 1;
        int colSpacing = 1;
        Bitmap tempBitmap;
        private Rectangle tileUnderMouse;
        Palette palette = Palette.GetDefaultPalette();

        private void CreateBitmap()
        {
            tempBitmap = new Bitmap((charW + colSpacing) * 16 + 1, (charH  + rowSpacing) * 8 + 1);
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                g.Clear(Color.Black);

                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        byte paletteIndex = (byte)(16* i + j * 2);
                        using (Brush brush = new SolidBrush(palette.GetColorFromAtariColorValue(paletteIndex)))
                        {
                            g.FillRectangle(brush, (charW + colSpacing) * i + 1, (charH + rowSpacing) * j + 1, charW, charH);
                        }
                    }
                }
            }

            pictureColors.Image = tempBitmap; 
        }

        private void pictureColors_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left || !currentColor.HasValue)
            {
                int TileCol = e.Location.X / (charW + colSpacing);
                int TileRow = e.Location.Y / (charH + rowSpacing);
                if (TileCol < 16 && TileRow < 8 && TileCol >= 0 && TileRow >= 0)
                {
                    Rectangle newTileUnderMouse = new Rectangle(TileCol * (charW + colSpacing), TileRow * (charH + rowSpacing), (charW + colSpacing), (charH + rowSpacing));
                    if (!newTileUnderMouse.Equals(tileUnderMouse))
                    {
                        tileUnderMouse = newTileUnderMouse;
                      
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            CurrentColor = (byte)(TileRow * 2 + TileCol * 16);
                        } else
                        {
                            pictureColors.Invalidate();
                        }
                    }
                } else
                {
                    if (!tileUnderMouse.IsEmpty)
                    {
                        tileUnderMouse = Rectangle.Empty;
                        pictureColors.Invalidate();
                    }
                }
            }
            else
            {
                if (!tileUnderMouse.IsEmpty)
                {
                    tileUnderMouse = Rectangle.Empty;
                    pictureColors.Invalidate();
                }
            }
        }

        private void pictureColors_Paint(object sender, PaintEventArgs e)
        {
            if (!tileUnderMouse.IsEmpty)
            {
                using (Pen whitePen = new Pen(Color.White))
                {
                    e.Graphics.DrawRectangle(whitePen, tileUnderMouse);
                }
            } else if (CurrentColor.HasValue)
            {
                int TileCol = CurrentColor.Value / 16;
                int TitleRow = (CurrentColor.Value & 0x0e)/2;
              
                Rectangle newTileUnderMouse = new Rectangle(TileCol * (charW + colSpacing), TitleRow * (charH + rowSpacing), (charW + colSpacing), (charH + rowSpacing));
                using (Pen whitePen = new Pen(Color.White))
                {
                    e.Graphics.DrawRectangle(whitePen, newTileUnderMouse);
                }
            }
        }

        private void pictureColors_MouseLeave(object sender, EventArgs e)
        {
            if (!tileUnderMouse.IsEmpty)
            {
                tileUnderMouse = Rectangle.Empty;
                pictureColors.Invalidate();
            }
        }

        private void pictureColors_MouseDown(object sender, MouseEventArgs e)
        {
            int TileCol = e.Location.X / (charW + colSpacing);
            int TileRow = e.Location.Y / (charH + rowSpacing);
            if (TileCol < 16 && TileRow < 8 && TileCol >= 0 && TileRow >= 0)
            {
                CurrentColor = (byte)(TileRow * 2 + TileCol * 16);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {   
                CurrentColor = (byte)numericUpDown1.Value;        
        }

        private void numericUpDown1_Validated(object sender, EventArgs e)
        {
            if (numericUpDown1.Text == "")
            {
                CurrentColor = null;
            }
        }

        private void pictureColors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int TileCol = e.Location.X / (charW + colSpacing);
            int TileRow = e.Location.Y / (charH + rowSpacing);
            if (TileCol < 16 && TileRow < 8 && TileCol >= 0 && TileRow >= 0)
            {
                CurrentColor = (byte)(TileRow * 2 + TileCol * 16);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
