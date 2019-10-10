using ScintillaNET;
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
    public partial class FormASMEdit : Form
    {
        private readonly Map Map;
        private readonly String InitialeCode;
        private readonly MADSLexer madsLexer = new MADSLexer();
        private readonly pbx1.TypeNode typeNode;

        public FormASMEdit(Map map, pbx1.TypeNode typeNode, String code, String title)
        {
            InitializeComponent();
            InitSyntaxColoring();

            scintilla1.Text = code;
            this.Text = title;
            this.typeNode = typeNode;
            this.Map = map;
            this.InitialeCode = scintilla1.Text;
        }

        private void InitSyntaxColoring()
        {
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();

            scintilla1.Styles[MADSLexer.StyleDefault].ForeColor = Color.Black;
            scintilla1.Styles[MADSLexer.StyleKeyword].ForeColor = Color.Blue;
            scintilla1.Styles[MADSLexer.StyleIdentifier].ForeColor = Color.Teal;
            scintilla1.Styles[MADSLexer.StyleNumber].ForeColor = Color.Purple;
            scintilla1.Styles[MADSLexer.StyleString].ForeColor = Color.Red;
            scintilla1.Styles[MADSLexer.StyleComment].ForeColor = Color.Gray;
       
            scintilla1.Lexer = Lexer.Container;
        }

    
        private void scintilla1_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            var startPos = scintilla1.GetEndStyled();
            var endPos = e.Position;

            madsLexer.Style(scintilla1, startPos, endPos);
        }

        private void FormASMEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //In case windows is trying to shut down, don't hold the process up
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                // Assume that X has been clicked and act accordingly.
                // Confirm user wants to close

                if (!string.Equals(scintilla1.Text, InitialeCode))
                {
                    switch (MessageBox.Show(this, "Save Routine?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        //Stay on this form
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                        case DialogResult.No:
                            this.DialogResult = DialogResult.Cancel;
                            break;
                        default:
                            this.DialogResult = DialogResult.OK;
                            break;
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            switch (typeNode)
            {
                case pbx1.TypeNode.Init:
                    Map.InitRoutine = scintilla1.Text;
                    break;
                case pbx1.TypeNode.Exec:
                    Map.ExecRoutine = scintilla1.Text;
                    break;
                case pbx1.TypeNode.TileCollision:
                    Map.TileCollisionRoutine = scintilla1.Text;
                    break;
            }


            if (MapSet.SSave(Map.MapSet)) {
                FormRunMADS.Compile(Map.MapSet);
            }
        }
    }
}
