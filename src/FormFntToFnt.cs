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
    public partial class FormFntToFnt : Form
    {
        private byte[] fnt;

        DLI[] dlis;
        public DLI[] DLIS
        {
            get { return dlis; }
            set
            {
                dlis = value;
                this.charSetUserControl1.DLIs = value;
            }
        }
       
        private CharacterSet characterSet;
        private CharacterSet CharacterSet
        {
            get { return characterSet; }
            set { characterSet = value; fnt = (byte[])value.Data.Clone(); this.charSetUserControl1.FntByte = fnt; this.Text = "Edit font '" + CharacterSet.Path + "'"; }
        }

        private CharacterSet characterSetFrom;
        private CharacterSet CharacterSetFrom
        {
            get { return characterSetFrom; }
            set { characterSetFrom = value; this.charSetUserControl2.FntByte = value.Data;  }
        }

        public byte[] ReturnFontData
        {
            get { return fnt; }
            private set { }
        }

        public FormFntToFnt(CharacterSet characterSet, DLI[] dLIS, CharacterSet characterSetFrom)
        {
            InitializeComponent();

            charSetUserControl1.OnDLISelected += new EventHandler(charSetUserControl1_OnDLISelected);

            CharacterSet = characterSet;

            CharacterSetFrom = characterSetFrom;

            DLIS = dLIS;

            charSetUserControl2.Drag = true;
        }

        private void charSetUserControl1_OnDLISelected(object sender, EventArgs e)
        {
            DLISelectedEventArgs DLISelectedEventArgs = (DLISelectedEventArgs)e;
            charSetUserControl2.AtariPFColors = DLISelectedEventArgs.DLI.AtariPFColors;
        }


    }
}
