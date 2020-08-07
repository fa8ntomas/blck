using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static BLEditor.Controls.CharSetUserControl;
using static BLEditor.GlyphEditUserControl;

namespace BLEditor
{
    public partial class FntEditPanel : UserControl, MultiPagePanel.ISavePanel
    {
        private byte[] fnt;

        public FntEditPanel()
        {
            InitializeComponent();

            glyphEditUserControl1.OnDLISelected += new EventHandler(glyphEditUserControl1_OnDLISelected);
             glyphEditUserControl1.GlyphEdited += glyphEditUserControl1_GlyphEdited;

            charSetUserControl1.TileSelected += new EventHandler(charSetUserControl1_TileSelected);
        }

        private CharacterSet characterSet;
        private CharacterSet preloadCharacterSet;

        private DLI[] dLIS;

        internal void PreLoad(CharacterSet characterSet, DLI[] dLIS = null)
        {
            preloadCharacterSet = characterSet;
            this.dLIS = dLIS;
        }

        bool MultiPagePanel.ISavePanel.Save()
        {
            CharacterSet.Data = fnt;
            return true;
        }

        void MultiPagePanel.ISavePanel.Load()
        {
            CharacterSet = preloadCharacterSet;
            glyphEditUserControl1.DLIs = dLIS;
            glyphEditUserControl1.CharTile = null;
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
            TileSelectedEventArgs tileSelectedEventArgs = (TileSelectedEventArgs)e;
            glyphEditUserControl1.CharTile = tileSelectedEventArgs.CharTile;
        }



        private CharacterSet CharacterSet
        {
            get { return characterSet; }
            set { characterSet = value; fnt = new List<byte>(value.Data).ToArray(); this.charSetUserControl1.FntByte = fnt; this.Text = "Edit font '" + CharacterSet.Path + "'"; }
        }
    }
}


