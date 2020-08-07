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

namespace BLEditor
{
    public partial class MapEditUserControl : UserControl
    {
        public MapEditUserControl()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            InitializeComponent();
        }
        public enum InterationType
        {
            PRESELECT,
            SELECT,
            PASTE,
            STAMP,
            DROP
        };

        private const int nbCol = 40;
        private const int nbRow = 11;
        private InterationType _interation = InterationType.PRESELECT;
        public InterationType Interation
        {
            get { return _interation; }
            set
            {
                SetInteration(value);
            }
        }
        static readonly object mouseDownEventKey = new object();

        protected EventHandlerList listEventDelegates = new EventHandlerList();

        public event EventHandler InteractionChanged
        {
            // Add the input delegate to the collection.
            add
            {
                listEventDelegates.AddHandler(mouseDownEventKey, value);
                value.Invoke(this, new InterationChangedEventArgs(_interation, currentStamp));
            }
            // Remove the input delegate from the collection.
            remove
            {
                listEventDelegates.RemoveHandler(mouseDownEventKey, value);
            }
        }
        // public event EventHandler InteractionChanged;

        public event EventHandler DirtyChanged;

        private bool _dirty = false;
        public bool Dirty
        {
            get { return _dirty; }
            private set { if (_dirty != value) { _dirty = value; /*DirtyChanged?.Invoke(this, new DirtyChangedEventArgs(_dirty));*/ } }
        }

        public Map inMap { get; private set; }
        public CharacterSet charset { get; private set; }

        static readonly int charW = 4;
        static readonly int charH = 8;
        static readonly int colSpacing = 1;
        static readonly int rowSpacing = 1;
        static readonly int zoom = 4;
        static readonly int cellSizeX = charW * zoom + colSpacing;
        static readonly int cellSizeY = charH * zoom + rowSpacing;


        private static AtariPFColors CreateDLIMapIndex(DLI[] DLIS, int row)
        {
            AtariPFColors result = DLIS[0].AtariPFColors;
            foreach (DLI dli in DLIS)
            {
                if (dli.IntLine > row)
                {
                    break;

                }
                result = dli.AtariPFColors;
            }

            return result;
        }

        private Bitmap tempBitmap;
        private Bitmap pasteBitmap;
        // private Size pasteSize;

        private static Bitmap CreateBitmapStamp(byte b, DLI[] DLIS, CharacterSet charset)
        {
            return CreateBitmapFromFnt(new Size(1, 1), new byte[] { b }, DLIS, charset);
        }
        private static Bitmap CreateBitmapFromFnt(Size size, byte[] MapData, DLI[] DLIS, CharacterSet charset, Pen borderPen = null)
        {
            int nbCol = size.Width;
            int nbRow = size.Height;

            Bitmap result = new Bitmap(nbCol * ((charW * zoom) + colSpacing) + colSpacing, nbRow * ((charH * zoom) + rowSpacing) + rowSpacing);

            Graphics bmGraphics = Graphics.FromImage(result);
            bmGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            bmGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            bmGraphics.CompositingQuality = CompositingQuality.AssumeLinear;
            bmGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            for (int y = 0; y < nbRow; y++)
            {
                RbgPFColors rbgPFColors = CreateDLIMapIndex(DLIS, y).ToBLColor();

                for (int x = 0; x < nbCol; x++)
                {
                    byte hexy = MapData == null ? (byte)0 : MapData[y * nbCol + x];
                    bool inverse = hexy > 127 ? true : false;

                    int offx = x * (charW * zoom + colSpacing) + colSpacing;
                    int offy = y * (charH * zoom + rowSpacing) + rowSpacing;

                    for (int yy = 0; yy < 8; yy++)//Eight rows
                    {
                        byte b = charset == null ? (byte)0 : charset.Data[(hexy & 127) * 8 + yy];

                        for (int xx = 0; xx < 4; xx++)//Four colors per row
                        {
                            int value = (b >> (6 - (xx * 2)) & 0X03);

                            for (int n = 0; n < zoom; n++) //scaled y value
                            {
                                for (int k = 0; k < zoom; k++) //scaled x value
                                {
                                    switch (value)
                                    {
                                        case 0:
                                            result.SetPixel(offx + xx * zoom + k, offy + yy * zoom + n, rbgPFColors.Colbk);
                                            break;
                                        case 1:
                                            result.SetPixel(offx + xx * zoom + k, offy + yy * zoom + n, rbgPFColors.Colpf0);
                                            break;
                                        case 2:
                                            result.SetPixel(offx + xx * zoom + k, offy + yy * zoom + n, rbgPFColors.Colpf1);
                                            break;
                                        case 3:
                                            if (inverse)
                                            {
                                                result.SetPixel(offx + xx * zoom + k, offy + yy * zoom + n, rbgPFColors.Colpf3);
                                            }
                                            else
                                            {
                                                result.SetPixel(offx + xx * zoom + k, offy + yy * zoom + n, rbgPFColors.Colpf2);
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                    }
                }
            }


            if (borderPen != null)
            {
                Rectangle r = new Rectangle();
                r.Size = result.Size;
                DrawBorderRectangle(bmGraphics, borderPen, r);
            }

            bmGraphics.Dispose();

            return result;
        }


        internal void LoadMap(Map inMap, CharacterSet charset)
        {
            this.inMap = inMap;
            this.charset = charset;
            pasteBitmap = CreateBitmapStamp(currentStamp, inMap?.DLIS, charset);
            RefreshMap();
        }

        void RefreshMap()
        {
            tempBitmap = CreateBitmapFromFnt(new Size(nbCol, nbRow), inMap?.MapData, inMap?.DLIS, charset);
            Invalidate();
        }
        byte currentStamp = 45;

        private void MapEditUserControl_Paint(object sender, PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.  
            //super.nt(e);

            if (tempBitmap != null)
            {
                e.Graphics.DrawImage(tempBitmap, 0, 0);
            }

            if (!tilesSelected.IsEmpty)
            {
                Graphics g = e.Graphics;

                Color customColor = Color.FromArgb(50, Color.White);

                using (SolidBrush shadowBrush = new SolidBrush(customColor))
                {
                    g.FillRectangle(shadowBrush, tilesSelected);
                }

                using (Pen pen2 = new Pen(Color.DeepSkyBlue, rowSpacing))
                {
                    pen2.DashPattern = new float[] { 2, 2 };

                    DrawBorderRectangle(g, pen2, tilesSelected);
                }
            }

            if (!tilesHightlighted.IsEmpty)
            {
                Graphics g = e.Graphics;
                Color customColor = Color.FromArgb(50, Color.Black);

                using (Pen pen2 = new Pen(Color.DeepSkyBlue, rowSpacing))
                {
                    DrawBorderRectangle(g, pen2, tilesHightlighted);
                }

                using (SolidBrush shadowBrush2 = new SolidBrush(customColor))
                {
                    Rectangle r = new Rectangle(tilesHightlighted.Location + new Size(2, 2), tilesHightlighted.Size - new Size(4, 4));

                    g.FillRectangle(shadowBrush2, r);
                }
            }

            if (pasteBitmap != null && !pasteRectangle.IsEmpty)
            {
                e.Graphics.DrawImage(pasteBitmap, pasteRectangle.Location);
            }

            if (pasteBitmap != null && !stampRectangle.IsEmpty)
            {
                e.Graphics.DrawImage(pasteBitmap, stampRectangle.Location);
            }
        }

        private static void DrawBorderRectangle(Graphics g, Pen pen2, Rectangle rec)
        {
            // CF https://stackoverflow.com/questions/925509/border-in-drawrectangle
            float shrinkAmount = pen2.Width / 2;
            g.DrawRectangle(
                pen2,
                rec.X + shrinkAmount,   // move half a pen-width to the right
                rec.Y + shrinkAmount,   // move half a pen-width to the down
                rec.Width - pen2.Width,   // shrink width with one pen-width
                rec.Height - pen2.Width); // shrink height with one pen-width
        }


        private Rectangle tilesSelected = Rectangle.Empty;
        private Rectangle tilesHightlighted = Rectangle.Empty;
        private Rectangle dataSelected = Rectangle.Empty;
        private Rectangle pasteRectangle = Rectangle.Empty;
        private Rectangle stampRectangle = Rectangle.Empty;

        Point topleft = Point.Empty;
        Point bottomright = Point.Empty;
        private InterationType dragInteraction;
        private Map.MapClipboardData clipboardData;

        private void MapEditUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateScreen(e.Location);
        }
        private void MapEditUserControl_MouseLeave(object sender, EventArgs e)
        {
            LeaveWindow();
        }

        private void LeaveWindow()
        {
            UpdateHightlightRectangle(Rectangle.Empty);
            UpdateStampRectangle(Rectangle.Empty);
        }

        private void UpdateScreen(Point mouselocation)
        {
            Point TilePosition = TileUnderMouseLocation(mouselocation);
            switch (_interation)
            {
                case InterationType.STAMP:
                    {
                        Point newStampPosition = new Point(TilePosition.X * cellSizeX, TilePosition.Y * cellSizeY);
                        if (!newStampPosition.Equals(stampRectangle.Location))
                        {
                            UpdateStampRectangle(new Rectangle(newStampPosition, new Size(cellSizeX + colSpacing, cellSizeY + rowSpacing)));
                        }
                    }; break;

                case InterationType.PASTE:
                    {
                        Point newPastePosition = OffsetPastePostion(TilePosition);
                        if (!newPastePosition.Equals(pasteRectangle.Location))
                        {
                            UpdatePasteRectangle(new Rectangle(newPastePosition, pasteRectangle.Size));
                        }
                    }; break;


                case InterationType.SELECT:
                    {
                        if (topleft.IsEmpty)
                        {
                            topleft = new Point(Math.Min(Math.Max(TilePosition.X, 0), nbCol), Math.Min(Math.Max(TilePosition.Y, 0), nbRow));
                            UpdateSelectedRectangle(new Rectangle(topleft.X * cellSizeX, topleft.Y * cellSizeY, cellSizeX + colSpacing, cellSizeY + rowSpacing));
                        }
                        else
                        {
                            bottomright = new Point(Math.Min(Math.Max(TilePosition.X, 0), nbCol), Math.Min(Math.Max(TilePosition.Y, 0), nbRow));
                            UpdateSelectedRectangle(new Rectangle(Math.Min(topleft.X, bottomright.X) * cellSizeX, Math.Min(topleft.Y, bottomright.Y) * cellSizeY, (Math.Abs(bottomright.X - topleft.X) + 1) * cellSizeX + colSpacing, (Math.Abs(bottomright.Y - topleft.Y) + 1) * cellSizeY + rowSpacing));
                        }
                    }; break;

                case InterationType.DROP:
                case InterationType.PRESELECT:
                    {
                        if (TilePosition.Y >= 0 && TilePosition.Y < nbRow && TilePosition.X >= 0 && TilePosition.X < nbCol)
                        {
                            UpdateHightlightRectangle(new Rectangle(TilePosition.X * cellSizeX, TilePosition.Y * cellSizeY, cellSizeX + colSpacing, cellSizeY + rowSpacing));
                        }
                        else
                        {
                            UpdateHightlightRectangle(Rectangle.Empty);
                        }
                    }
                    break;
            }
        }
        private static Point TileUnderMouseLocation(Point mouseLocation)
        {
            return new Point(mouseLocation.X / cellSizeX, mouseLocation.Y / cellSizeY);
        }

        private void UpdateStampRectangle(Rectangle rectangle)
        {
            if (!rectangle.Equals(this.stampRectangle))
            {
                Invalidate(stampRectangle);
                Invalidate(stampRectangle = rectangle);
            }
        }

        private void UpdateHightlightRectangle(Rectangle newTileUnderMouse)
        {
            if (!newTileUnderMouse.Equals(tilesHightlighted))
            {
                Invalidate(tilesHightlighted);
                Invalidate(tilesHightlighted = newTileUnderMouse);
            }
        }
        private void UpdateSelectedRectangle(Rectangle newSelectedRectangle)
        {
            if (!newSelectedRectangle.Equals(this.tilesSelected))
            {
                Invalidate(tilesSelected);
                Invalidate(tilesSelected = newSelectedRectangle);
            }
        }
        private void UpdatePasteRectangle(Rectangle newPasteRectangle)
        {
            if (!newPasteRectangle.Equals(pasteRectangle))
            {
                Invalidate(pasteRectangle);
                Invalidate(pasteRectangle = newPasteRectangle);
            }
        }



        private void MapEditUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_interation == InterationType.PRESELECT)
                {
                    SetInteration(InterationType.SELECT);
                }
                else if (_interation == InterationType.STAMP)
                {
                    inMap.SetMapDataByte(TileUnderMouseLocation(e.Location), currentStamp);
                    RefreshMap();
                }
                else if (_interation == InterationType.PASTE)
                {
                    Point tilePosition = TileUnderMouseLocation(e.Location);
                    tilePosition.Offset(-clipboardData.Size.Width / 2, -clipboardData.Size.Height / 2);
                    inMap.SetMapDataBytes(tilePosition, clipboardData);
                    RefreshMap();
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (_interation == InterationType.PASTE)
                {
                    SetInteration(InterationType.PRESELECT);
                } else
                {
                    Point tilePosition = TileUnderMouseLocation(e.Location);
                    if (inMap.Intersect(tilePosition))
                    {
                        currentStamp = inMap.GetMapDataByte(tilePosition);

                        pasteBitmap = CreateBitmapStamp(currentStamp, inMap?.DLIS, charset);
                        SetInteration(InterationType.STAMP);
                    }

                }
            }
        }

        private void MapEditUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && _interation == InterationType.SELECT)
            {
                dataSelected = new Rectangle(Math.Min(topleft.X, bottomright.X), Math.Min(topleft.Y, bottomright.Y), Math.Abs(bottomright.X - topleft.X) + 1, Math.Abs(bottomright.Y - topleft.Y) + 1);
                SetInteration(InterationType.PRESELECT);
            }
        }

        private void MapEditUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && _interation == InterationType.PASTE)
            {
                SetInteration(InterationType.PRESELECT);
            }
            else if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.A && _interation == InterationType.PRESELECT)
                {
                    dataSelected = new Rectangle(0, 0, nbCol, nbRow);
                    UpdateSelectedRectangle(new Rectangle(0, 0, nbCol * cellSizeX + colSpacing, nbRow * cellSizeY + rowSpacing));
                }
                else if (e.KeyCode == Keys.X && !dataSelected.IsEmpty)
                {
                    inMap.ClearMapData(dataSelected);
                    RefreshMap();
                }
                else if (e.KeyCode == Keys.C && !dataSelected.IsEmpty)
                {
                    inMap.CopyDataToClipboard(dataSelected);
                }
                else if (e.KeyCode == Keys.V)
                {
                    clipboardData = inMap.GetMapClipboardData();
                    if (clipboardData != null && clipboardData.Size != Size.Empty)
                    {
                        using (Pen pasteImageBorder = new Pen(Color.Red, rowSpacing))
                        {
                            pasteBitmap = CreateBitmapFromFnt(clipboardData.Size, clipboardData.Bytes, inMap?.DLIS, charset, pasteImageBorder);

                            Point TilePosition = TileUnderMouseLocation(PointToClient(MousePosition));
                            Point location = OffsetPastePostion(TilePosition);
                            UpdatePasteRectangle(new Rectangle(location, new Size(clipboardData.Size.Width * cellSizeX + colSpacing, clipboardData.Size.Height * cellSizeY + rowSpacing)));
                            SetInteration(InterationType.PASTE);

                        }
                    }
                }
            }
        }

        private Point OffsetPastePostion(Point TilePosition)
        {
            return new Point((TilePosition.X - clipboardData.Size.Width / 2) * cellSizeX, (TilePosition.Y - clipboardData.Size.Height / 2) * cellSizeY);
        }

        private InterationType SetInteration(InterationType interation)
        {
            InterationType precedenteInteration = _interation;

            if (_interation != interation)
            {
                _interation = interation;

                switch (_interation)
                {
                    case InterationType.STAMP:
                        {
                            UpdateSelectedRectangle(Rectangle.Empty);
                            UpdatePasteRectangle(Rectangle.Empty);
                            UpdateHightlightRectangle(Rectangle.Empty);

                        }; break;

                    case InterationType.SELECT:
                        {
                            topleft = Point.Empty;
                            bottomright = Point.Empty;
                            dataSelected = Rectangle.Empty;

                            UpdateSelectedRectangle(tilesHightlighted);
                            UpdatePasteRectangle(Rectangle.Empty);
                            UpdateHightlightRectangle(Rectangle.Empty);
                        }; break;

                    case InterationType.PRESELECT:
                        {

                            UpdatePasteRectangle(Rectangle.Empty);
                            //  UpdateSelectedRectangle(Rectangle.Empty);
                        }; break;
                    case InterationType.PASTE:
                        {
                            UpdateHightlightRectangle(Rectangle.Empty);
                            UpdateSelectedRectangle(Rectangle.Empty);
                        }; break;
                }
            }

            EventHandler mouseEventDelegate = (EventHandler)listEventDelegates[mouseDownEventKey];
            if (mouseEventDelegate != null) mouseEventDelegate(this, new InterationChangedEventArgs(_interation, currentStamp));

            return precedenteInteration;
        }

        public class InterationChangedEventArgs : EventArgs
        {
        
            public InterationChangedEventArgs(InterationType interation, byte stamp)
            {
                this.Interation = interation;
                this.Stamp = stamp;
            }

            public InterationType Interation { get; private set; }
            public byte Stamp { get; private set; }
        }


        private bool MouseIsOverControl()
        {
            return ClientRectangle.Contains(PointToClient(MousePosition));
        }

        private void MapEditUserControl_DragOver(object sender, DragEventArgs e)
        {
            UpdateScreen(PointToClient(new Point(e.X, e.Y)));
        }

        private void MapEditUserControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(CharacterSet.CharDataFormat.Name))
            {
                e.Effect = DragDropEffects.Copy;
                dragInteraction = SetInteration(InterationType.DROP);
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void MapEditUserControl_DragDrop(object sender, DragEventArgs e)
        {
            byte currentByte = (byte)e.Data.GetData(CharacterSet.CharDataFormat.Name);
            inMap.SetMapDataByte(TileUnderMouseLocation(PointToClient(new Point(e.X, e.Y))), currentByte);

            RefreshMap();

            SetInteration(dragInteraction);

        }

        private void MapEditUserControl_DragLeave(object sender, EventArgs e)
        {
            LeaveWindow();
        }
    }
}
