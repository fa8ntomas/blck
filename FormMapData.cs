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
    public partial class Form1 : Form
    {
        public Form1(Map map)
        {
            InitializeComponent();

            this.checkBoxFoe.Checked = map.Foe;

            this.numericUpDownBruceStartX1.Value = map.BruceStartX1;
            this.numericUpDownBruceStartY1.Value = map.BruceStartY1;
            this.numericUpDownBruceStartX2.Value = map.BruceStartX2;
            this.numericUpDownBruceStartY2.Value = map.BruceStartY2;

            switch (map.YamoSpawnPosition)
            {
                case 1:
                    radioButtonYamoFavorA.Checked = true;
                    break;
                case 2:
                    radioButtonYamoFavorB.Checked = true;
                    break;
                default:
                    this.radioButtonYamoRandom.Checked = true;
                    break;
            }

            switch (map.NinjaSpawnPosition)
            {
                case 1:
                    this.radioButtonNinjaFavorA.Checked = true;
                    break;
                case 2:
                    this.radioButtonNinjaFavorB.Checked = true;
                    break;
                default:
                    this.radioButtonNinjaRandom.Checked = true;
                    break;
            }

            numericUpDownExit1X.Value = map.Exit1X;
            numericUpDownExit1Y.Value = map.Exit1Y;
            populateMapCombo(comboBoxExit1Map, map, map.Exit1MapID);

            numericUpDownExit2X.Value = map.Exit2X;
            numericUpDownExit2Y.Value = map.Exit2Y;
            populateMapCombo(comboBoxExit2Map, map, map.Exit2MapID);

            numericUpDownExit3X.Value = map.Exit3X;
            numericUpDownExit3Y.Value = map.Exit3Y;
            populateMapCombo(comboBoxExit3Map, map, map.Exit3MapID);

            numericUpDownExit4X.Value = map.Exit4X;
            numericUpDownExit4Y.Value = map.Exit4Y;
            populateMapCombo(comboBoxExit4Map, map, map.Exit4MapID);


        }

        private void populateMapCombo(ComboBox comboBoxExit, Map map, Guid? selectedMapID)
        {
            List<Map> mapList = new List<Map>();
            mapList.Add(Map.EMPTY);
            mapList.AddRange(map.MapSet.Maps);
            mapList.Remove(map);

            comboBoxExit.DisplayMember  = "Name";
            comboBoxExit.ValueMember    = "UID";
            comboBoxExit.DataSource     = mapList;

            Map seletedMap = map.MapSet.GetMap(selectedMapID);

            comboBoxExit.SelectedValue = (seletedMap == null) ? Map.EMPTY.UID : selectedMapID;
        }

  
    }
}
