using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using static BLEditor.RbgPFColors;

namespace BLEditor
{
    public partial class GlyphEditUserControl : UserControl
    {
        public event EventHandler GlyphEdited;
        public class GlyphEditedEventArgs : EventArgs
        {
            public CharTile CharTile { get; private set; }

            public GlyphEditedEventArgs(byte cchar, byte[] glyph)
              : base()
            {
                this.CharTile = new CharTile(cchar, new List<byte>(glyph).ToArray());
            }
        }

        private enum CellType
        {
            Low,
            High
         }

        CellType cellType;

        
        public GlyphEditUserControl()
        {
            InitializeComponent();

            CreateBitmap();

            radioButtonColbk.Click += (s, e) => { SelectColor(PlayFieldColor.COLBK); };
            radioButtonColpf0.Click += (s, e) => { SelectColor(PlayFieldColor.COLPF0); };
            radioButtonColpf1.Click += (s, e) => { SelectColor(PlayFieldColor.COLPF1); };
            radioButtonColpf2.Click += (s, e) => { SelectColor(PlayFieldColor.COLPF2); };
            radioButtonColpf3.Click += (s, e) => { SelectColor(PlayFieldColor.COLPF3); };

            Change(CellType.Low);

            toolStripButtonHigh.Click += (s, e) => { Change(CellType.High); };
            toolStripButtonLow.Click += (s, e) => { Change(CellType.Low); };
        }
       
        private void Change(CellType cellType, bool resetColorButton=true)
        {

            this.cellType = cellType;

            toolStripButtonLow.Checked = CellType.Low.Equals(cellType);
            toolStripButtonHigh.Checked = CellType.High.Equals(cellType);

            setGlyph(CharTile?.Glyph);

            if (resetColorButton)
            {
                radioButtonColbk.Checked = true;
                radioButtonColbk.Focus();
            }
        }

        PlayFieldColor currentColor;

        private void SelectColor(PlayFieldColor pfColor)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            String title = "Change Display Mode";

            if (PlayFieldColor.COLPF2.Equals(pfColor) && CellType.High.Equals(cellType))
            {
                DialogResult result = MessageBox.Show("COLPF2 is only available in 'Low Char Display' mode. Switch to this mode?", title, buttons);
                if (result == DialogResult.No)
                {
                    radioButtonColpf3.Checked = true;
                    radioButtonColpf3.Focus();
                }
                else
                {
                    Change(CellType.Low, false);
                    currentColor = pfColor;
                }
               
            }
            else if (PlayFieldColor.COLPF3.Equals(pfColor) && CellType.Low.Equals(cellType))
            {
                DialogResult result = MessageBox.Show("COLPF3 is only available in 'High Char Display' mode. Switch to this mode?", title, buttons);
                if (result == DialogResult.No)
                {
                    radioButtonColpf2.Checked = true;
                    radioButtonColpf2.Focus();
                }
                else
                {
                    Change(CellType.High, false);
                    currentColor = pfColor;
                }
            
            }
            else
            {
                currentColor = pfColor;
            }
        }

        private void SetButtonsColor()
        {
            radioButtonColbk.BackColor = RbgPFColors.Colbk;
            radioButtonColpf0.BackColor = RbgPFColors.Colpf0;
            radioButtonColpf1.BackColor = RbgPFColors.Colpf1;
            radioButtonColpf2.BackColor = RbgPFColors.Colpf2;
            radioButtonColpf3.BackColor = RbgPFColors.Colpf3;

            radioButtonColbk.Checked = true;
        }

        private Rectangle tileUnderMouse;
        int charW = 4;
        int charH = 4;
        int zoom = 4;
        int rowSpacing = 1;
        int colSpacing = 1;
        private bool showGrid = true;
        Bitmap tempBitmap;

        private RbgPFColors _RbgPFColors=new RbgPFColors();
        private RbgPFColors RbgPFColors
        {
            get { return _RbgPFColors; }
            set { _RbgPFColors = value; SetButtonsColor(); }
        }

        DLI[] _dlis;
        public DLI[] DLIs
        {
            get { return _dlis; }
            set
            {
                _dlis = value;
                //  RbgPFColors = value[0].AtariPFColors.ToBLColor();
                if (value != null)
                {
                    foreach (DLI dli in _dlis)
                    {
                        toolStripComboBoxColors.Items.Add(dli.ToDLIListEntry());
                    }

                    toolStripComboBoxColors.SelectedIndex = 0;
                }
            }
        }

        public event EventHandler OnDLISelected;

        private Color GetColorFromValue(int byteValue, bool bit7)
        {
            switch (byteValue & 3)
            {
                case 0:
                    return RbgPFColors.Colbk;
                case 1:
                    return RbgPFColors.Colpf0;
                case 2:
                    return RbgPFColors.Colpf1;
                case 3:
                    return bit7 ? RbgPFColors.Colpf3 : RbgPFColors.Colpf2;
                default:
                    throw new ArgumentException();
            }
        }

        CharTile _CharTile;
        public CharTile CharTile
        {
            get { return _CharTile; }
            set { _CharTile = value; setGlyph(_CharTile?.Glyph); }
        }

        private void CreateBitmap()
        {

            tempBitmap = new Bitmap((charW * zoom + colSpacing) * 4 + 2, (charH * zoom + rowSpacing) * 8 + 2);
            using (Graphics g = Graphics.FromImage(tempBitmap)) { g.Clear(Color.White); }
            pictureBox1.Image= tempBitmap;
        }


        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!tileUnderMouse.IsEmpty)
            {
                tileUnderMouse = Rectangle.Empty;
                pictureBox1.Invalidate();//refreshes the picturebox
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int cellSizeX = charW * zoom + colSpacing;
            int cellSizeY = charH * zoom + rowSpacing;
            int TileCol = e.Location.X / cellSizeX;
            int TitleRow = e.Location.Y / cellSizeY;
            if (TitleRow >= 0 && TitleRow < 8 && TileCol >= 0 && TileCol < 4)
            {
                Rectangle newTileUnderMouse = new Rectangle(TileCol * cellSizeX, TitleRow * cellSizeY, cellSizeX, cellSizeY);
                if (!newTileUnderMouse.Equals(tileUnderMouse))
                {
                    tileUnderMouse = newTileUnderMouse;
                    pictureBox1.Invalidate();
                }
            }
            else
            {
                if (!tileUnderMouse.IsEmpty)
                {
                    tileUnderMouse = Rectangle.Empty;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            if (!tileUnderMouse.IsEmpty)
            {
                Graphics g = e.Graphics;
                Color customColor = Color.FromArgb(50, Color.Black);

                using (SolidBrush shadowBrush = new SolidBrush(customColor))
                {
                    g.FillRectangle(shadowBrush, tileUnderMouse);
                }

            }
            if (showGrid)
            {
                int cellSizeX = charW * zoom + colSpacing;
                int cellSizeY = charH * zoom + rowSpacing;
                int numOfCellsY = 8;
                int numOfCellsX = 4;
                Graphics g = e.Graphics;

                Pen p = new Pen(Color.Black);

                for (int y = 0; y <= numOfCellsY; ++y)
                {
                    g.DrawLine(p, 0, y * cellSizeY, numOfCellsX * cellSizeX, y * cellSizeY);
                }

                for (int x = 0; x <= numOfCellsX; ++x)
                {
                    g.DrawLine(p, x * cellSizeX, 0, x * cellSizeX, numOfCellsY * cellSizeY);
                }
            }
        }

        private void setGlyph(byte[] glyph)
        {
            Graphics g = Graphics.FromImage(tempBitmap);

            g.Clear(Color.White);

            for (int i = 0; i < 8; i++)
            {
                byte b = (glyph == null) ? (byte)0 : glyph[i];
                for (int j = 0; j < 4; j++)
                {
                    Color pfColor = GetColorFromValue(GetValueFromByte(j, b), CellType.High.Equals(cellType));
                    g.FillRectangle(new SolidBrush(pfColor), new Rectangle(j * (charW * zoom + colSpacing), i * (charH * zoom + rowSpacing), charW * zoom + colSpacing, charH * zoom + rowSpacing));
                }
            }

            g.Dispose();
            pictureBox1.Invalidate();
        }

        private int GetValueFromByte(int b, byte byteToConvert)
        {
            return (byteToConvert >> (6 - (b * 2)) & 0X03);
        }

        private void toolStripComboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            DLIListEntry dLIListEntry = (DLIListEntry)toolStripComboBoxColors.SelectedItem;
            RbgPFColors = dLIListEntry.dli.AtariPFColors.ToBLColor();

            setGlyph(CharTile?.Glyph);

            OnDLISelected?.Invoke(this, new DLISelectedEventArgs(dLIListEntry.dli));
        }

   
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (CharTile?.Glyph != null)
            {
                int cellSizeX = charW * zoom + colSpacing;
                int cellSizeY = charH * zoom + rowSpacing;
                int TileCol = e.Location.X / cellSizeX;
                int TitleRow = e.Location.Y / cellSizeY;
                if (TitleRow >= 0 && TitleRow < 8 && TileCol >= 0 && TileCol < 4)
                {
                    CharTile.Glyph[TitleRow] = ChangeByte(CharTile.Glyph[TitleRow], TileCol, currentColor);
                    setGlyph(CharTile.Glyph);
                    if (GlyphEdited != null)
                    {
                        this.GlyphEdited(this, new GlyphEditedEventArgs(CharTile.Index, CharTile.Glyph));
                      
                    }
                }
            } else
            {
                MessageBox.Show("Please, select the tile to edit before drawing!");
            }
        }

        private byte ChangeByte(byte v, int tileCol, PlayFieldColor currentColor)
        {
            int value;

            switch (currentColor)
            {
                case PlayFieldColor.COLBK:
                    value = 0;
                    break;
                case PlayFieldColor.COLPF0:
                    value = 1;
                    break;
                case PlayFieldColor.COLPF1:
                    value = 2;
                    break;
                case PlayFieldColor.COLPF2:
                case PlayFieldColor.COLPF3:
                    value = 3;
                    break;
                default:
                    throw new ArgumentException();
            }

          
            value = value << (2*(3 - tileCol));
            int mask = 0xFF ^ (0x3 << (2*(3 - tileCol)));

            return (byte)((v & mask) | value);
        }


    }
}
