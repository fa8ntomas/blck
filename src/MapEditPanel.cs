using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLEditor.Controls;
using GuiLabs.Undo;

namespace BLEditor
{
    public partial class MapEditPanel : UserControl
    {
        private Map _inMap;

        [Browsable(false)]
        public Map InMap
        {
            get => _inMap;

            private set
            {
                if (value != _inMap)
                {
                    _inMap = value;
                    dliList.Map = value;
                }
            }
        }

        public MapEditPanel()
        {
          
            InitializeComponent();
            
            this.toolStripButtonStamp.Click += (s, e) => { mapEditUserControl.Interation=MapEditUserControl.InterationType.STAMP; };
            this.toolStripButtonSelect.Click += (s, e) => { mapEditUserControl.Interation=MapEditUserControl.InterationType.PRESELECT; };

            mapEditUserControl.InteractionChanged += new EventHandler(mapEditUserControl_InteractionChanged);
            charSetUserControl.CharClickedChanged += (s, e) => { mapEditUserControl.CurrentStamp=(((CharSetUserControl.CharClickedEventArgs)e).Char); };
            toolStrip1.Renderer   = new MyToolStripSystemRenderer();
        }
       
        internal void LoadMap(Map inMap, CharacterSet charset)
        {
            InMap = inMap;
            mapEditUserControl.LoadMap(inMap, charset);
            charSetUserControl.FntByte = charset.Data;
            mapEditUserControl.Focus();
        }
        private void mapEditUserControl_InteractionChanged(object sender, EventArgs e)
        {
            MapEditUserControl.InterationChangedEventArgs messageEventArgs = (MapEditUserControl.InterationChangedEventArgs)e;

            switch (messageEventArgs.Interation)
            {
                case MapEditUserControl.InterationType.PRESELECT:
                case MapEditUserControl.InterationType.SELECT:
                    { 
                    toolStripButtonStamp.Checked = false;
                    toolStripButtonSelect.Checked = true;
                    };break;
                case MapEditUserControl.InterationType.PASTE:
                case MapEditUserControl.InterationType.STAMP:
                    {
                        toolStripButtonStamp.Checked = true;
                        toolStripButtonSelect.Checked = false;

                        if (messageEventArgs.Interation==MapEditUserControl.InterationType.STAMP)
                        {
                            SetCurrentStamp(messageEventArgs.Stamp);
                        }
                    }; break;
            }
        }

        private void SetCurrentStamp(byte stamp)
        {
            this.labelStamp.Text = stamp.ToString("X4"); 
        }

        private void runButton_Click(object sender, EventArgs e)
        {
           if (MapSet.SSave(InMap.MapSet))
           {
               FormRunMADS.Compile(InMap.MapSet, true, Decimal.ToInt32(firstMapNumericUpDown.Value)) ;
           }
        }
    }
}
