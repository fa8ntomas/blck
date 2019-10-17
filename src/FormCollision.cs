using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BLEditor.Map;

namespace BLEditor
{
    public partial class FormColision : Form
    {
      
        Map Map { get;  set; }

        public FormColision(Map map, TypeColorDetection DetectionType, List<Rectangle> DetectionRects, List<ZoneColorDetection> DetectionFlags)
        {
            InitializeComponent();

            Console.WriteLine("**"+DetectionType.ToString() + (int)DetectionType);

            Map = map;
            this.zoneCollisionUserControl1.Map = map;

            this.zoneCollisionUserControl1.Zones = DetectionRects;
            this.zoneCollisionUserControl1.Flags = DetectionFlags;

            this.comboBoxColorDetection.SelectedIndex = (int)DetectionType;
    }

    private void comboBoxExit1Map_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxColorDetection.SelectedIndex)
            {
                case 0:
                case 1:
                    this.zoneCollisionUserControl1.Visible = false;
                    break;
                default:
                    this.zoneCollisionUserControl1.Visible = true;
                    break;
            }
        }


    }

       
}
