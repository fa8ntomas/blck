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
    public partial class FormBitmapToFnt : Form
    {
        private byte[] fnt;

        public FormBitmapToFnt(Image image, CharacterSet characterSet, DLI[] dLIS)
        {
            InitializeComponent();

            pictureGrid1.OnColorPicked += new EventHandler(pictureGrid1_OnColorPicked);
            fiveColorPickerUserControl1.OnPickColor += new EventHandler(fiveColorPickerUserControl1_onPickColor);
            charSetUserControl1.DirtyChanged += new EventHandler(charSetUserControl1_DirtyChanged);

            CharacterSet = characterSet;
            this.dLIS = dLIS;
            pictureGrid1.OriginalImage = image;

             
        }

        private CharacterSet characterSet;
        private DLI[] dLIS;

        private CharacterSet CharacterSet
        {
            get { return characterSet; }
            set { characterSet = value; fnt = new List<byte>(value.Data).ToArray(); this.charSetUserControl1.FntByte = fnt; this.Text = "Edit font '" + CharacterSet.Path + "'"; }
        }

        public byte[] ReturnFontData
        {
            get { return fnt; }
            private set { }
        }

        private void charSetUserControl1_DirtyChanged(object sender, EventArgs e)
        {
              buttonOK.Enabled = charSetUserControl1.Dirty;
        }

        private void fiveColorPickerUserControl1_onPickColor(object sender, EventArgs e)
        {
            ColorsPickerUserControl.MessageEventArgs messageEventArgs = (ColorsPickerUserControl.MessageEventArgs)e;

            pictureGrid1.SetPickerMod(messageEventArgs.playFieldColor, messageEventArgs.index);
        }

        private void pictureGrid1_OnColorPicked(object sender, EventArgs e)

        {
            PictureGrid.ColorPickedEventArgs colorPickedEventArgs = (PictureGrid.ColorPickedEventArgs)e;
            fiveColorPickerUserControl1.SetColor(colorPickedEventArgs.PlayFieldColor, colorPickedEventArgs.Color, colorPickedEventArgs.index);
            charSetUserControl1.SetColor(colorPickedEventArgs.PlayFieldColor, colorPickedEventArgs.Color);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureGrid1.OriginalImage = Image.FromFile(openFileDialog1.FileName);
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.charSetUserControl1.FntByte = File.ReadAllBytes(openFileDialog1.FileName);

            }
        }  
    }
}
