using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class FormZoneCollisionData : Form
    {
        public FormZoneCollisionData(UserRect activeRect, Map.ZoneColorDetection zoneColorDetection)
        {
            InitializeComponent();

            Rectangle rect = activeRect.GetRectangle();
            numericUpDownX.Value = rect.X;
            numericUpDownY.Value = rect.Y;
            numericUpDownW.Value = rect.Width;
            numericUpDownH.Value = rect.Height;

            switch (zoneColorDetection)
            {
                case Map.ZoneColorDetection.Flag0:
                    radioButtonFlag0.Checked = true;
                    break;
                case Map.ZoneColorDetection.Flag1:
                    radioButtonFlag1.Checked = true;
                    break;
                default:
                    radioButtonAlways.Checked = true;
                    break;
            }
        }

        public Map.ZoneColorDetection ZoneColorDetectionResult
        {
            get
            {
                if (radioButtonFlag0.Checked)
                {
                    return Map.ZoneColorDetection.Flag0;
                }
                else if (radioButtonFlag1.Checked)
                {
                    return Map.ZoneColorDetection.Flag1;
                }
                else
                {
                    return Map.ZoneColorDetection.Always;
                }
            
            }
        }
        public Rectangle RectResult => new Rectangle((int)numericUpDownX.Value, (int)numericUpDownY.Value, (int)numericUpDownW.Value, (int)numericUpDownH.Value);
    }
}
