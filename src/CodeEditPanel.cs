using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace BLEditor
{
    public partial class CodeEditPanel : UserControl, MultiPagePanel.ISavePanel
    {
        public CodeEditPanel()
        {
            InitializeComponent();
            InitSyntaxColoring();

        }

        private readonly MADSLexer madsLexer = new MADSLexer();

        private Map preloadedMap;
        private  String preloadedCode;
        private string preloadedTitle;
        private  pbx1.TypeNode preloadedTypeNode;

        internal void PreLoad(Map map, pbx1.TypeNode typeNode, String code, String title)
        { 
            this.preloadedTitle = title;
            this.preloadedTypeNode = typeNode;
            this.preloadedMap = map;
            this.preloadedCode = code;
        }
        bool MultiPagePanel.ISavePanel.Save()
        {
            return true;
        }

        void MultiPagePanel.ISavePanel.Load()
        {
            scintilla1.Text = preloadedCode;
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
    }
}
