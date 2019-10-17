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
using System.IO;
using System.Drawing.Imaging;

namespace BLEditor
{
    public partial class PictureGrid : UserControl
    {
        public PictureGrid()
        {
            InitializeComponent();

            toolStripButton1.CheckState = showGrid ? CheckState.Checked : CheckState.Unchecked;
        }

        private enum InterationType
        {
            NONE,
            COLORPICKERPF0,
            COLORPICKERPF1,
            COLORPICKERPF2,
            COLORPICKERPF3,
            COLORPICKERBK,
            TILE
        };

        public event EventHandler OnColorPicked;
        public class ColorPickedEventArgs : EventArgs
        {
            public RbgPFColors.PlayFieldColor PlayFieldColor { get; private set; }
            public Color Color { get; private set; }
            public int index { get; private set; }

            public ColorPickedEventArgs(RbgPFColors.PlayFieldColor playFieldColor, Color color, int index)
              : base()
            {
                this.PlayFieldColor = playFieldColor;
                this.Color = color;
                this.index = index;
            }
        }
        public void SetPickerMod(RbgPFColors.PlayFieldColor color, int index)
        {
            switch (color)
            {
                case RbgPFColors.PlayFieldColor.COLBK:
                    setInteration(InterationType.COLORPICKERBK, index);
                    break;

                case RbgPFColors.PlayFieldColor.COLPF0:
                    setInteration(InterationType.COLORPICKERPF0,  index);
                    break;

                case RbgPFColors.PlayFieldColor.COLPF1:
                    setInteration(InterationType.COLORPICKERPF1,  index);
                    break;

                case RbgPFColors.PlayFieldColor.COLPF2:
                    setInteration(InterationType.COLORPICKERPF2,  index);
                    break;

                case RbgPFColors.PlayFieldColor.COLPF3:
                    setInteration(InterationType.COLORPICKERPF3,  index);
                    break;

                default:
                    setInteration(InterationType.NONE);
                    break;
            }
        }
     
        private Image _Ori2ginalImage;
        private float zoomFactor = 3.0f;
        public bool showGrid = true;

        private InterationType interation = InterationType.TILE;
        private int index = -1;

        private RectangleF tileUnderMouse;
        private void setInteration(InterationType interationType, int index=-1)
        {

            switch (interation = interationType)
            {
                case InterationType.NONE:
                    pictureBox1.Cursor = null;
                    break;

                case InterationType.TILE:
                    pictureBox1.Cursor = null;
                    break;

                case InterationType.COLORPICKERPF0:
                case InterationType.COLORPICKERPF1:
                case InterationType.COLORPICKERPF2:
                case InterationType.COLORPICKERPF3:
                case InterationType.COLORPICKERBK:
                    {
                        pictureBox1.Cursor =
                                           new Cursor(new MemoryStream(Properties.Resources.ColorPickerToolCursor));

                    }; break;
            }

            this.index = index;
        }

        public Image OriginalImage {
            private get { return _Ori2ginalImage;}
            set { _Ori2ginalImage = value; AutoZoom(); }
        }

        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            Point position = new Point();
            position.X = Math.Max(0, (picBox.Parent.ClientSize.Width / 2) - (picImage.Width / 2));
            position.Y = Math.Max(0, (picBox.Parent.ClientSize.Height / 2) - (picImage.Height / 2));

            picBox.Image = picImage;
            picBox.Location = position;
            picBox.Refresh();
        }


        private void ResizeAndDisplayImage()
        {
            if (OriginalImage != null)
            {

                Size newSize = new Size((int)Math.Ceiling(OriginalImage.Width * zoomFactor), (int)Math.Ceiling(OriginalImage.Height * zoomFactor));

                Bitmap tempBitmap = new Bitmap(OriginalImage, newSize);


                Graphics bmGraphics = Graphics.FromImage(tempBitmap);
                bmGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                bmGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                bmGraphics.CompositingQuality = CompositingQuality.AssumeLinear;
                bmGraphics.SmoothingMode = SmoothingMode.None;

                bmGraphics.DrawImage(OriginalImage,
                                     new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height));


                bmGraphics.Dispose();


                CenterPictureBox(pictureBox1, tempBitmap);
             
            }
        }


        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (OriginalImage != null)
            {
                Graphics g = e.Graphics;
                if (showGrid)
                {
                    float cellSizeX = 4f * zoomFactor;
                    float cellSizeY = 8f * zoomFactor;
                    int numOfCellsY = (OriginalImage.Height + 7) / 8;
                    int numOfCellsX = (OriginalImage.Width + 3) / 4;

                    Pen p = new Pen(Color.Black);

                    for (float y = 0; y < numOfCellsY; ++y)
                    {
                        float yGrid = (float)Math.Round(y * cellSizeY,0);
                        g.DrawLine(p, 0, yGrid, numOfCellsX * cellSizeX, yGrid);
                    }

                    for (float x = 0; x < numOfCellsX; ++x)
                    {
                        float xGrid = (float)Math.Round(x * cellSizeX,0);
                        g.DrawLine(p, xGrid, 0, xGrid, numOfCellsY * cellSizeY);
                    }
                }
                if (!tileUnderMouse.IsEmpty)
                {
                    Color customColor = Color.FromArgb(50, Color.Black);

                    using (SolidBrush shadowBrush = new SolidBrush(customColor))
                    {
                        g.FillRectangle(shadowBrush, tileUnderMouse);
                    }

                }

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (OriginalImage != null)
            {
                switch (interation)
                {
                    case InterationType.TILE:
                        {
                            float cellSizeX = 4f * zoomFactor;
                            float cellSizeY = 8f * zoomFactor;
                            float TileCol = (float)Math.Floor((float)e.Location.X / cellSizeX);
                            float TitleRow = (float)Math.Floor((float)e.Location.Y / cellSizeY);
                            RectangleF newTileUnderMouse = new RectangleF(TileCol * cellSizeX, TitleRow * cellSizeY, cellSizeX, cellSizeY);
                            if (!newTileUnderMouse.Equals(tileUnderMouse))
                            {
                                tileUnderMouse = newTileUnderMouse;
                                pictureBox1.Invalidate();//refreshes the picturebox
                            }
                        }; break;


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



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (interation)
            {
                case InterationType.NONE:
                    break;

                case InterationType.TILE:
                    float cellSizeX = 4f * zoomFactor;
                    float cellSizeY = 8f * zoomFactor;
                    int TileCol = (int)Math.Floor((float)e.Location.X / cellSizeX);
                    int TitleRow = (int)Math.Floor((float)e.Location.Y / cellSizeY);
                    Rectangle cloneRect = new Rectangle(TileCol*4, TitleRow*8, 4, 8);

                    this.DoDragDrop(ImageUtils.CropImage(this._Ori2ginalImage, cloneRect), DragDropEffects.Copy);
                    break;

                case InterationType.COLORPICKERBK:
                    sendColorPickedMessage(RbgPFColors.PlayFieldColor.COLBK, e);
                    break;

                case InterationType.COLORPICKERPF0:
                    sendColorPickedMessage(RbgPFColors.PlayFieldColor.COLPF0, e);
                    break;
               
                case InterationType.COLORPICKERPF1:
                    sendColorPickedMessage(RbgPFColors.PlayFieldColor.COLPF1, e);
                    break;
                case InterationType.COLORPICKERPF2:
                    sendColorPickedMessage(RbgPFColors.PlayFieldColor.COLPF2, e);
                    break;
                case InterationType.COLORPICKERPF3:
                    sendColorPickedMessage(RbgPFColors.PlayFieldColor.COLPF3, e);
                    break;

            }

        }

        private void sendColorPickedMessage(RbgPFColors.PlayFieldColor playFieldColor, MouseEventArgs e )
        {
            if (this.OnColorPicked != null)
            {
                Color color = ((Bitmap)pictureBox1.Image).GetPixel(e.Location.X, e.Location.Y);

                this.OnColorPicked(this, new ColorPickedEventArgs(playFieldColor, color, index));
            }

            setInteration(InterationType.TILE);
        }

        private void toolStripButton1_CheckStateChanged(object sender, EventArgs e)
        {
            showGrid = !showGrid;
            pictureBox1.Invalidate();//refreshes the picturebox
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.0f;
            ResizeAndDisplayImage();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Min(10, zoomFactor+0.5f);
            ResizeAndDisplayImage();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Max(1, zoomFactor - 0.5f);
            ResizeAndDisplayImage();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AutoZoom();

        }

        private void AutoZoom()
        {
            Size availableSize = pictureBox1.Parent.ClientRectangle.Size;
            Size imageSize = _Ori2ginalImage.Size;

            zoomFactor = (float)availableSize.Width / (float)imageSize.Width;
            zoomFactor = Math.Min(zoomFactor, (float)availableSize.Height / (float)imageSize.Height);
            zoomFactor = Math.Max(zoomFactor, 1);
            ResizeAndDisplayImage();
        }
    }
}
