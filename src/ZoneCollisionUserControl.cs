using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static BLEditor.Map;

namespace BLEditor
{
    public partial class ZoneCollisionUserControl : UserControl
    {
    

        public ZoneCollisionUserControl()
        {
            InitializeComponent();

            pictureBox1.MouseDown += new MouseEventHandler(mPictureBox_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(mPictureBox_MouseUp);
            pictureBox1.MouseMove += new MouseEventHandler(mPictureBox_MouseMove);
            pictureBox1.Paint += pictureBox1_Paint;

            ActiveRect = null;

            toolStripButtonRemove.Click += (s, e) => { RemoveSelected(); };
            toolStripButtonExtended.Click += (s, e) => { InfoSelected(); };
            toolStripButtonAdd.Click += (s, e) => { this.AddNewRect(); };
        }

        private Map map;
        public Map Map
        {
            get { return map; }
           set { map = value; CreateImage(); }
        }


        private UserRect _ActiveRect;
        private UserRect ActiveRect
        {
            get { return _ActiveRect; }
            set { if (_ActiveRect != value) { _ActiveRect = value; pictureBox1.Invalidate(); } toolStripButtonRemove.Enabled= _ActiveRect!=null; toolStripButtonExtended.Enabled = _ActiveRect != null; }
        }

        private float zoom= 4;
        private List<UserRect> userRects = new List<UserRect>();

        public List<Rectangle> Zones
        {
            get { var result = new List<Rectangle>(); foreach (UserRect rect in userRects) { result.Add(rect.GetRectangle()); } return result; }
            set { userRects.Clear();  if(value!=null) foreach(Rectangle rect in value) AddInZones(rect, zoom); }
        }


        private List<ZoneColorDetection> flags = new List<ZoneColorDetection>();
        public List<ZoneColorDetection> Flags
        {
            get { return new List<ZoneColorDetection>(flags); }
            set { flags.Clear(); if (value != null) foreach (ZoneColorDetection flag in value) flags.Add(flag); }
        }



        private void AddNewRect()
        {
            ActiveRect = AddInZones(new Rectangle(10, 10, 100, 100));
        }

        private void RemoveSelected()
        {
            if (this.ActiveRect != null)
            {
                var index = userRects.IndexOf(ActiveRect);
                userRects.RemoveAt(index);
                flags.RemoveAt(index);
                ActiveRect = null;
            }
        }

        private void InfoSelected()
        {
            if (this.ActiveRect != null)
            {
                var index = userRects.IndexOf(ActiveRect);

                FormZoneCollisionData form = new FormZoneCollisionData(ActiveRect, flags.ElementAt(index));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    flags[index] = form.ZoneColorDetectionResult;
                    userRects[index].SetValue(form.RectResult,zoom);
                    pictureBox1.Invalidate();
                }
            }
        }
        private UserRect AddInZones(Rectangle rect, float zoom=1.0F)
        {
            UserRect userRect = new UserRect(rect, zoom);

            userRect.SetPictureBox(this.pictureBox1);

            userRects.Add(userRect);

            flags.Add(ZoneColorDetection.Always);

            pictureBox1.Invalidate();

            return userRect;
        }

 
        private void mPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            ActiveRect?.mPictureBox_MouseMove(sender, e);
        }

        private void mPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            ActiveRect?.mPictureBox_MouseUp(sender, e);
        }

        private void mPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            UserRect newActiveRect = null;
            foreach (UserRect rect in userRects)
            {
                if (rect.isUnder(e.Location))
                {
                    newActiveRect = rect;
                }
            }

            if (newActiveRect != ActiveRect)
            {
                ActiveRect = newActiveRect;
             }

            ActiveRect?.mPictureBox_MouseDown(sender, e);
        }

        private void CreateImage()
        {
        
            Bitmap tempBitmap = new Bitmap((int)Math.Ceiling(160.0F * zoom), (int)Math.Ceiling(88.0F * zoom));
         
            if (Map!=null)
            {
                byte[] bytes = Map.MapSet.CharSets.First(set => set.UID == Map.FontID).Data;

                for (int row = 0; row < 11; row++)
                {
                    RbgPFColors BLcolor = GetColors(row).ToBLColor();
                    for (int col = 0; col < 40; col++)
                    {
                        byte tile = Map.MapData[row * 40 + col];

                        DrawTile(tempBitmap, new Point(col * 4, row * 8), tile, zoom, BLcolor, bytes);
                    }

                }
            }

            CenterPictureBox(pictureBox1, tempBitmap);
        }

        private AtariPFColors GetColors(int row)
        {
            DLI[] dlis = Map.DLIS;

            for (int i = 0; i < dlis.Length; i++)
            {
                if (dlis[i].IntLine == row)
                {
                    return dlis[i].AtariPFColors;
                }
                else if (dlis[i].IntLine > row)
                {
                    return dlis[i - 1].AtariPFColors;

                }
            }

            return dlis[dlis.Length - 1].AtariPFColors;
        }

        private static void DrawTile(Bitmap tempBitmap, Point topLeft, byte tile, float zoom, RbgPFColors colors, byte[] FntByte)
        {
            bool b7 = (tile & 0x80) != 0;

            for (int i = 0; i < 8; i++)
            {
                byte b = FntByte[(tile & 0x7f) * 8 + i];

                int minY = (int)Math.Floor((float)(topLeft.Y + i) * zoom);
                int maxY = (int)Math.Floor((float)(topLeft.Y + i + 1) * zoom);

                for (int j = 0; j < 4; j++)
                {
                    Color pfColor = GetColorFromValue(colors, GetValueFromByte(j, b), b7);
                    int minX = (int)Math.Floor((float)(topLeft.X + j) * zoom);
                    int maxX = (int)Math.Floor((float)(topLeft.X + j + 1) * zoom);

                    for (int x = minX; x < maxX; x++)
                    {
                        for (int y = minY; y < maxY; y++)
                        {
                            tempBitmap.SetPixel(x, y, pfColor);
                        }
                    }
                }
            }
        }

        private static int GetValueFromByte(int b, byte byteToConvert)
        {
            return (byteToConvert >> (6 - (b * 2)) & 0X03);
        }

        private static Color GetColorFromValue(RbgPFColors colors, int byteValue, bool bit7)
        {
            switch (byteValue & 3)
            {
                case 0:
                    return colors.Colbk;
                case 1:
                    return colors.Colpf0;
                case 2:
                    return colors.Colpf1;
                default: // 3 
                    return bit7 ? colors.Colpf3 : colors.Colpf2;
            }
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (UserRect rect in userRects)
            {
                if (rect != ActiveRect)
                {
                    rect.Draw(g,  false);
                }
            }

            ActiveRect?.Draw(g, true);
        }
    }
}
