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

namespace BLEditor
{
    public partial class MapEditPanel : UserControl
    {
        public MapEditPanel()
        {
          
            InitializeComponent();
            
            this.toolStripButtonStamp.Click += (s, e) => { mapEditUserControl.Interation=MapEditUserControl.InterationType.STAMP; };
            this.toolStripButtonSelect.Click += (s, e) => { mapEditUserControl.Interation=MapEditUserControl.InterationType.PRESELECT; };

            mapEditUserControl.InteractionChanged += new EventHandler(mapEditUserControl_InteractionChanged);

            toolStrip1.Renderer   = new MyToolStripSystemRenderer();
        }
       
        internal void LoadMap(Map inMap, CharacterSet charset)
        {
            dliList.Map = inMap;
            mapEditUserControl.LoadMap(inMap, charset);
            charSetUserControl1.FntByte = charset.Data;
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
    }
}
