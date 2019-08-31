using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BLEditor
{

    public class UserRect
    {
        private PictureBox mPictureBox;
        public RectangleF rect;
        
        public bool allowDeformingDuringMovement = false;
        private bool mIsClick = false;
        private bool mMove = false;
        private int oldX;
        private int oldY;
        private int sizeNodeRect = 5;
        private float zoom;
        private PosSizableRect nodeSelected = PosSizableRect.None;

      
        private enum PosSizableRect
        {
            UpMiddle,
            LeftMiddle,
            LeftBottom,
            LeftUp,
            RightUp,
            RightMiddle,
            RightBottom,
            BottomMiddle,
            None
        };


        public UserRect(Rectangle r, float zoom=1.0F)
        {
            SetValue(r, zoom);
            mIsClick = false;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)(rect.X / zoom), (int)(rect.Y / zoom), (int)(rect.Width / zoom), (int)(rect.Height / zoom));
        }

        public void SetValue(Rectangle r, float zoom )
        {
            this.zoom = zoom;
            rect = new RectangleF(r.X * zoom,r.Y * zoom,r.Width * zoom,r.Height * zoom);
        }

        public void Draw(Graphics g, bool active=true)
        {
            if (active)
            {
                g.DrawRectangle(new Pen(Color.Red), Rectangle.Round(rect));

                foreach (PosSizableRect pos in Enum.GetValues(typeof(PosSizableRect)))
                {
                    g.DrawRectangle(new Pen(Color.Red), GetRect(pos));
                }
            } else
            {
                g.DrawRectangle(new Pen(Color.AliceBlue), Rectangle.Round(rect));
            }
        }


        public void SetPictureBox(PictureBox p)
        {
            this.mPictureBox = p;
         }

        private void mPictureBox_Paint(object sender, PaintEventArgs e)
        {

            try
            {
                Draw(e.Graphics);
            }
            catch (Exception exp)
            {
                System.Console.WriteLine(exp.Message);
            }

        }

        public bool isUnder(Point p)
        {
            if (GetNodeSelectable(p) != PosSizableRect.None)
            {
                return true;
            }

            if (rect.Contains(new Point(p.X, p.Y)))
            {
               return true;
            }

            return false;
        }

        public void mPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mIsClick = true;

 //           nodeSelected = PosSizableRect.None;
            nodeSelected = GetNodeSelectable(e.Location);

            if (rect.Contains(new Point(e.X, e.Y)))
            {
                mMove = true;
            }
            oldX = e.X;
            oldY = e.Y;
        }

        public void mPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mIsClick = false;
            mMove = false;
        }

        public void mPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeCursor(e.Location);
            if (mIsClick == false)
            {
                return;
            }

            Rectangle backupRect = Rectangle.Round(rect);

            switch (nodeSelected)
            {
                case PosSizableRect.LeftUp:
                    rect.X += e.X - oldX;
                    rect.Width -= e.X - oldX;
                    rect.Y += e.Y - oldY;
                    rect.Height -= e.Y - oldY;
                    break;
                case PosSizableRect.LeftMiddle:
                    rect.X += e.X - oldX;
                    rect.Width -= e.X - oldX;
                    break;
                case PosSizableRect.LeftBottom:
                    rect.Width -= e.X - oldX;
                    rect.X += e.X - oldX;
                    rect.Height += e.Y - oldY;
                    break;
                case PosSizableRect.BottomMiddle:
                    rect.Height += e.Y - oldY;
                    break;
                case PosSizableRect.RightUp:
                    rect.Width += e.X - oldX;
                    rect.Y += e.Y - oldY;
                    rect.Height -= e.Y - oldY;
                    break;
                case PosSizableRect.RightBottom:
                    rect.Width += e.X - oldX;
                    rect.Height += e.Y - oldY;
                    break;
                case PosSizableRect.RightMiddle:
                    rect.Width += e.X - oldX;
                    break;

                case PosSizableRect.UpMiddle:
                    rect.Y += e.Y - oldY;
                    rect.Height -= e.Y - oldY;
                    break;

                default:
                    if (mMove)
                    {
                        rect.X = rect.X + e.X - oldX;
                        rect.Y = rect.Y + e.Y - oldY;
                    }
                    break;
            }
            oldX = e.X;
            oldY = e.Y;

            if (rect.Width < 5 || rect.Height < 5)
            {
                rect = backupRect;
            }

            TestIfRectInsideArea();

            mPictureBox.Invalidate();
        }

        private void TestIfRectInsideArea()
        {
            // Test if rectangle still inside the area.
            if (rect.X < 0) rect.X = 0;
            if (rect.Y < 0) rect.Y = 0;
            if (rect.Width <= 0) rect.Width = 1;
            if (rect.Height <= 0) rect.Height = 1;

            if (rect.X + rect.Width > mPictureBox.Width)
            {
                rect.Width = mPictureBox.Width - rect.X - 1; // -1 to be still show 
                if (allowDeformingDuringMovement == false)
                {
                    mIsClick = false;
                }
            }
            if (rect.Y + rect.Height > mPictureBox.Height)
            {
                rect.Height = mPictureBox.Height - rect.Y - 1;// -1 to be still show 
                if (allowDeformingDuringMovement == false)
                {
                    mIsClick = false;
                }
            }
        }

        private Rectangle CreateRectSizableNode(int x, int y)
        {
            return new Rectangle(x - sizeNodeRect / 2, y - sizeNodeRect / 2, sizeNodeRect, sizeNodeRect);
        }

        private Rectangle GetRect(PosSizableRect p)
        {
            Rectangle rrect = Rectangle.Round(rect);
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return CreateRectSizableNode(rrect.X, rrect.Y);

                case PosSizableRect.LeftMiddle:
                    return CreateRectSizableNode(rrect.X, rrect.Y  +rrect.Height / 2);

                case PosSizableRect.LeftBottom:
                    return CreateRectSizableNode(rrect.X, rrect.Y + rrect.Height);

                case PosSizableRect.BottomMiddle:
                    return CreateRectSizableNode(rrect.X + rrect.Width / 2, rrect.Y + rrect.Height);

                case PosSizableRect.RightUp:
                    return CreateRectSizableNode(rrect.X + rrect.Width, rrect.Y);

                case PosSizableRect.RightBottom:
                    return CreateRectSizableNode(rrect.X + rrect.Width, rrect.Y + rrect.Height);

                case PosSizableRect.RightMiddle:
                    return CreateRectSizableNode(rrect.X + rrect.Width, rrect.Y + rrect.Height / 2);

                case PosSizableRect.UpMiddle:
                    return CreateRectSizableNode(rrect.X + rrect.Width / 2, rrect.Y);
                default:
                    return new Rectangle();
            }
        }

 
        public XElement ToXElement()
        {
           return new XElement("rect", new XElement("Top", rect.Top), new XElement("Left",rect.Left), new XElement("Bottom", rect.Bottom), new XElement("Right", rect.Right));
        }

        public static UserRect FromXElement(XElement xrect)
        {           
            int top = (int)Convert.ToSingle(xrect.Element("Top").Value, CultureInfo.InvariantCulture);
            int bottom = (int)Convert.ToSingle(xrect.Element("Bottom").Value, CultureInfo.InvariantCulture);
            int left = (int)Convert.ToSingle(xrect.Element("Left").Value, CultureInfo.InvariantCulture);
            int right = (int) Convert.ToSingle(xrect.Element("Right").Value, CultureInfo.InvariantCulture);

            return new UserRect( Rectangle.FromLTRB(left, top, right, bottom));  
        }

        private PosSizableRect GetNodeSelectable(Point p)
        {
            foreach (PosSizableRect r in Enum.GetValues(typeof(PosSizableRect)))
            {
                if (GetRect(r).Contains(p))
                {
                    return r;
                }
            }
            return PosSizableRect.None;
        }

        private void ChangeCursor(Point p)
        {
            mPictureBox.Cursor = GetCursor(GetNodeSelectable(p));
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Cursor GetCursor(PosSizableRect p)
        {
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return Cursors.SizeNWSE;

                case PosSizableRect.LeftMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.LeftBottom:
                    return Cursors.SizeNESW;

                case PosSizableRect.BottomMiddle:
                    return Cursors.SizeNS;

                case PosSizableRect.RightUp:
                    return Cursors.SizeNESW;

                case PosSizableRect.RightBottom:
                    return Cursors.SizeNWSE;

                case PosSizableRect.RightMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.UpMiddle:
                    return Cursors.SizeNS;
                default:
                    return Cursors.Default;
            }
        }

    }

}
