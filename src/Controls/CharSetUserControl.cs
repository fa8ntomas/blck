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

namespace BLEditor.Controls
{
    public partial class CharSetUserControl : UserControl
    {
        private enum CellType {
            Low,
            High,
            HighLow
        }

        CellType cellType;

        public CharSetUserControl()
        {
            InitializeComponent();
            
            toolStrip1.Renderer = new MyToolStripSystemRenderer();

            colPerRow = 10;
            nbRow = (128 + colPerRow - 1) / colPerRow;

            ((Control)pictureBox1).AllowDrop = true;

            Change(CellType.Low);

            toolStripButtonHighLow.Click += (s,e)=> { Change(CellType.HighLow); };
            toolStripButtonHigh.Click += (s, e) => { Change( CellType.High); };
            toolStripButtonLow.Click += (s, e) => { Change( CellType.Low); };
        }

        private void Change(CellType cellType)
        {
   
            this.cellType = cellType;

            toolStripButtonHighLow.Checked = CellType.HighLow.Equals(cellType);
            toolStripButtonLow.Checked = CellType.Low.Equals(cellType);
            toolStripButtonHigh.Checked = CellType.High.Equals(cellType);

          //  doubleCell = !doubleCell;
            CreateBitmapFromFnt();

        }

        internal void UpdateCharTile(CharTile charTile)
        {
            int index = (charTile.Index&0x7F) * 8;
            for (int i= 0; i<  8; i++)
            {
                fnt[i + index] = charTile.Glyph[i];
            }
            CreateBitmapFromFnt();
            pictureBox1.Invalidate();//refreshes the picturebox
            Dirty = true;
        }

        private byte[] fnt;
        private Rectangle tileUnderMouse;
     
        public event EventHandler DirtyChanged;
        public event EventHandler TileSelected;

        bool _dirty = false;
        public bool Dirty 
        {
            get { return _dirty; }
            private set { if (_dirty != value) { _dirty = value; DirtyChanged?.Invoke(this, new DirtyChangedEventArgs(_dirty)); } }
        }

        DLI[] _dlis;
        public DLI[] DLIs
        {
            get { return _dlis; }
            set
            {
                _dlis = value;
                if (value != null)
                {
                    toolStripLabelColors.Visible = true;
                    toolStripComboBoxColors.Visible = true;

                    foreach (DLI dli in _dlis)
                    {
                        toolStripComboBoxColors.Items.Add(dli.ToDLIListEntry());
                    }

                    toolStripComboBoxColors.SelectedIndex = 0;
                }
            }
        }

        public bool Drag { get; set; } = false;

        public class DirtyChangedEventArgs : EventArgs
        {
            public bool Dirty { get; private set; }

            public DirtyChangedEventArgs(bool isDirty)
              : base()
            {
                this.Dirty = isDirty;
            }
        }

        public class TileSelectedEventArgs : EventArgs
        {
            public CharTile CharTile { get; private set; }
            
            public TileSelectedEventArgs(byte cchar, List<byte>glyph)
              : base()
            {
                this.CharTile = new CharTile(cchar, new List<byte>(glyph).ToArray());
            }
        }

        public byte[] FntByte
        {
            private get { return fnt; }
            set { fnt = value; CreateBitmapFromFnt(); Dirty = false; }
        }

        AtariPFColors _AtariPFColors;
        public AtariPFColors AtariPFColors
        {
            private get { return _AtariPFColors; }
            set { _AtariPFColors = value; colors = _AtariPFColors.ToBLColor();  CreateBitmapFromFnt(); }
        }
        private int GetValueFromByte(int b, byte byteToConvert)
        {
            return (byteToConvert >> (6 - (b * 2)) & 0X03);
        }

        private RbgPFColors colors = new RbgPFColors();

        private Color GetColorFromValue(int byteValue, bool bit7)
        {
            switch (byteValue&3)
            {
                case 0:
                    return colors.Colbk;
                case 1:
                    return colors.Colpf0;
                case 2:
                    return colors.Colpf1;
                case 3:
                    return bit7 ? colors.Colpf3 : colors.Colpf2;
                default:
                    throw new ArgumentException();

            }
        }

        int charW = 4;
        int charH = 8;
        int colPerRow = 32;
        int nbRow = 4;
        int zoom = 4;
        int textSizeInPixel = 7;

        int rowSpacing = 7 +2+2+2;

        int colSpacing = 4;

        private Size CellSize()
        {
            return new Size((charW * zoom + colSpacing) * (CellType.HighLow.Equals(cellType) ? 2 : 1), (charH * zoom + rowSpacing));
        }
        private Point GetPosition(int charIndex)
        {
            int x = (charIndex % colPerRow) * CellSize().Width;
            int y = (charIndex / colPerRow) * CellSize().Height;

            return new Point(x, y);
        }

        private int GetCharIndex(Point pos)
        {
            int col = pos.X / CellSize().Width;
            if (col >= 0 && col < colPerRow)
            {
                int row = pos.Y / CellSize().Height;
                int cchar = row * colPerRow + col;
                if (cchar > 127 || cchar < 0) {
                    return -1;
                } else
                {
                    return cchar + (CellType.High.Equals(cellType) ? 128 : 0);
                }
            }

            return -1;
        }


        private void CreateBitmapFromFnt()
        {
            if (FntByte != null)
            {
                Bitmap tempBitmap = new Bitmap(colPerRow * (charW * zoom + colSpacing) * 2, nbRow * (charH * zoom + rowSpacing));

                Graphics bmGraphics = Graphics.FromImage(tempBitmap);
                bmGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                bmGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                bmGraphics.CompositingQuality = CompositingQuality.AssumeLinear;
                bmGraphics.SmoothingMode = SmoothingMode.AntiAlias;

                Font drawFont = new Font("Console New", (textSizeInPixel - 2) * bmGraphics.DpiX / 72);
           
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                bmGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                StringFormat drawFormat = new StringFormat();
              

                int count = 0;
                for (int cchar = 0; cchar < 128; cchar++)
                {
                    Point topLeft = GetPosition(cchar);
                    int cc = cchar + (CellType.High.Equals(cellType) ? 128 : 0);
                    bmGraphics.DrawString(cc.ToString("X2"), drawFont, drawBrush, topLeft.X, topLeft.Y, drawFormat);
                    topLeft.Y += textSizeInPixel + 2;
                    for (int glyph = 0; glyph < (CellType.HighLow.Equals(cellType) ? 2:1); glyph++)
                    {
                        topLeft.X += glyph * (charW * zoom + colSpacing);
                        int byteIndex = count;
                        DrawChar(tempBitmap, topLeft, glyph, cchar,zoom);

                    }
                    count += 8;
                }

                bmGraphics.Dispose();

                pictureBox1.Image = tempBitmap;
            }
        }

        private void DrawChar(Bitmap tempBitmap, Point topLeft, int glyph, int cchar, int zoom)
        {
            for (int i = 0; i < 8; i++)//Eight rows
            {
                byte b = FntByte[(cchar&0x7F) * 8 + i];
                for (int j = 0; j < 4; j++)//Four colors per row
                {
                    bool b7 = false;
                    switch (this.cellType)
                    {
                        case CellType.HighLow:
                            b7 = glyph == 0 ? true : false;
                            break;
                        case CellType.Low:
                            b7 = false;
                            break;
                        case CellType.High:
                            b7 = true;
                            break;
                    }

                    Color pfColor = GetColorFromValue(GetValueFromByte(j, b), b7);
                    for (int x = 0; x < zoom; x++)
                    {
                        for (int y = 0; y < zoom; y++)
                        {
                            tempBitmap.SetPixel(topLeft.X + j * zoom + x, topLeft.Y + i * zoom + y, pfColor);
                        }
                    }

                }

            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!tileUnderMouse.IsEmpty)
            {
                tileUnderMouse = Rectangle.Empty;
                pictureBox1.Invalidate();//refreshes the picturebox
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int cchar = GetCharIndex(e.Location);
            if (cchar >= 0)
            {
                Point topLeft = GetPosition(cchar);
                Size size = CellSize();
                tileUnderMouse = new Rectangle(topLeft, size);
            } else
            {
                tileUnderMouse = Rectangle.Empty;
            }

            pictureBox1.Invalidate();//refreshes the picturebox
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
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
          
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            int cchar = GetCharIndex(pictureBox1.PointToClient(new Point(e.X, e.Y)));

            if (cchar >= 0)
            {
                Bitmap bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
               
                bool same = true;
                if (bmp.Width == 4 && bmp.Height == 8)
                {
                    List<byte> bytes = BitmapToByte(bmp, colors);
                    for (int i = 0; i < 8; i++)
                    {
                        same = same && (fnt[(cchar * 8) + i] == bytes[i]);
                        fnt[cchar * 8 + i] = bytes[i];
                    }

                    if (!same)
                    {
                        CreateBitmapFromFnt();
                        pictureBox1.Invalidate();//refreshes the picturebox
                        Dirty = true;
                    }
                }
            }
        }

        private void pictureBox1_DragOver(object sender, DragEventArgs e)
        {
            int cchar = GetCharIndex(pictureBox1.PointToClient(new Point(e.X,e.Y)));
            if (cchar >= 0)
            {
                Point topLeft = GetPosition(cchar);
                Size size = CellSize();
                tileUnderMouse = new Rectangle(topLeft, size);
            }
            else
            {
                tileUnderMouse = Rectangle.Empty;
            }

            pictureBox1.Invalidate();//refreshes the picturebox
        }

        public void SetColor(RbgPFColors.PlayFieldColor playFieldColor, Color color)
        {
            colors.SetColor(playFieldColor, color);
            CreateBitmapFromFnt();
        }


        private List<byte>  BitmapToByte(Bitmap bmp, RbgPFColors colors)
        {
            List<byte> bitc = new List<byte>();
           for (int y = 0; y < 8; y++)
            {
                int b = 0;
                for (int x = 0; x < 4; x++)
                {
                    int colindex = 0;
                    Color customColor = bmp.GetPixel(x,  y);
                  
                    if (customColor.Equals(colors.Colbk))
                    {
                        colindex = 0;
                    }
                    else if (customColor.Equals(colors.Colpf0))
                    {
                        colindex = 1;
                    }
                    else if (customColor.Equals(colors.Colpf1))
                    {
                        colindex = 2;
                    }
                    else if (customColor.Equals(colors.Colpf2) || customColor.Equals(colors.Colpf3))
                    {
                        colindex = 3;
                   
                    } else
                    {
                        colindex =0;
                    }

                    b = b * 4 + colindex;
                }

                bitc.Add((byte)b);

            }

            return bitc;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            zoom = Math.Min(zoom + 1, 10);
            CreateBitmapFromFnt();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            zoom = Math.Max(zoom - 1, 1);
            CreateBitmapFromFnt();
        }

 
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (TileSelected != null)
            {
                int cchar = GetCharIndex(e.Location);
                if (cchar >= 0)
                {
                    List<byte> glyph = new List<byte>();
                    for (int i = 0; i < 8; i++)
                    {
                        glyph.Add(fnt[(cchar&0x7f) * 8 + i]);
                    }

                    this.TileSelected(this, new TileSelectedEventArgs((byte)cchar, glyph));                  
                }
            }

            if (Drag)

            {
                int cchar = GetCharIndex(e.Location);
                if (cchar >= 0)
                {
                    DragDropChar(cchar);
                }
            }
        }

        private void DragDropChar(int cchar)
        {
            Bitmap tempBitmap = new Bitmap(4, 8);
            DrawChar(tempBitmap, new Point(0, 0), 0, cchar, 1);
            DataObject d = new DataObject();
            d.SetData(tempBitmap);
            d.SetData(CharacterSet.CharDataFormat.Name, (byte)cchar);

            this.DoDragDrop(d, DragDropEffects.Copy);
        }

        public event EventHandler OnDLISelected;

        private void toolStripComboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            DLIListEntry dLIListEntry = (DLIListEntry)toolStripComboBoxColors.SelectedItem;
            AtariPFColors = dLIListEntry.dli.AtariPFColors;

            OnDLISelected?.Invoke(this, new DLISelectedEventArgs(dLIListEntry.dli));
        }
    }
}
