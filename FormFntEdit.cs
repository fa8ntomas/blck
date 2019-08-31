using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BLEditor.CharSetUserControl;
using static BLEditor.GlyphEditUserControl;

namespace BLEditor
{
    public partial class FormFntEdit : Form
    {
        private byte[] fnt;
        
        public FormFntEdit(CharacterSet characterSet, DLI[] dLIS)
        {
        
            InitializeComponent();

            
            CharacterSet = characterSet;
            this.dLIS = dLIS;

            glyphEditUserControl1.OnDLISelected += new EventHandler(glyphEditUserControl1_OnDLISelected);
            glyphEditUserControl1.DLIs = dLIS;
            glyphEditUserControl1.GlyphEdited += glyphEditUserControl1_GlyphEdited;

            charSetUserControl1.TileSelected += new EventHandler(charSetUserControl1_TileSelected);
        }

        private void glyphEditUserControl1_GlyphEdited(object sender, EventArgs e)
        {
            GlyphEditedEventArgs GlyphEditedEventArgs = (GlyphEditedEventArgs)e;
            charSetUserControl1.UpdateCharTile(GlyphEditedEventArgs.CharTile);
        }

        private void glyphEditUserControl1_OnDLISelected(object sender, EventArgs e)
        {
            DLISelectedEventArgs DLISelectedEventArgs = (DLISelectedEventArgs)e;
            charSetUserControl1.AtariPFColors = DLISelectedEventArgs.DLI.AtariPFColors;
        }

        private void charSetUserControl1_TileSelected(object sender, EventArgs e)
        {
            TileSelectedEventArgs tileSelectedEventArgs= (TileSelectedEventArgs)e;
            glyphEditUserControl1.CharTile= tileSelectedEventArgs.CharTile;
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
    }


}
