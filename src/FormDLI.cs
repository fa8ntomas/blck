using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BLEditor.RbgPFColors;

namespace BLEditor
{
    public partial class FormDLI : Form
    {
        public FormDLI(DLI dli=null, int forcedDliLineIndex=-1)
        {
            InitializeComponent();

            populateOrderCombo();

            if (dli != null)
            {
                ReturnAtariPFColors = new AtariPFColors(dli.AtariPFColors);
                ReturnIntLine = forcedDliLineIndex!=-1? forcedDliLineIndex:dli.IntLine;
                ReturnOrderValue = dli.OrderValue;
            } else
            {
                ReturnAtariPFColors = new AtariPFColors();
                ReturnIntLine = 1;
                ReturnOrderValue = null;
            }

            UpdateColors();

            buttonColbk.Click += (s, e) => { HandleButtonClick(PlayFieldColor.COLBK); };
            buttonColpf0.Click += (s, e) => { HandleButtonClick(PlayFieldColor.COLPF0); };
            buttonColpf1.Click += (s, e) => { HandleButtonClick(PlayFieldColor.COLPF1); };
            buttonColpf2.Click += (s, e) => { HandleButtonClick(PlayFieldColor.COLPF2); };
            buttonColpf3.Click += (s, e) => { HandleButtonClick(PlayFieldColor.COLPF3); };

        }

        private void populateOrderCombo()
        {    
            comboBoxOrder.DisplayMember = "Value";
            comboBoxOrder.ValueMember = "Value";
            comboBoxOrder.DataSource = OrderEntry.GetEntries();
        }

       
        private Palette palette = Palette.GetDefaultPalette();

        public AtariPFColors ReturnAtariPFColors { get; set; }

        public int ReturnIntLine
        {
            get {
                return (int)lineNumberEdit.Value;
            }

            private set {
                lineNumberEdit.Value = value; lineNumberEdit.Enabled=(value!=0);
            }
        }

        public Object ReturnOrderValue
        {
            get
            {
              
                return comboBoxOrder.SelectedValue;
            }

            private set
            {
                comboBoxOrder.SelectedValue = OrderEntry.GetNormalizedValue(value);
              
            }
        }
        private void UpdateColors()
        {
            COLBKI.BackColor = palette.GetColorFromAtariColorValue(ReturnAtariPFColors.Colbk);
            COLPF3I.BackColor = palette.GetColorFromAtariColorValue(ReturnAtariPFColors.Colpf3);
            COLPF2I.BackColor = palette.GetColorFromAtariColorValue(ReturnAtariPFColors.Colpf2);
            COLPF1I.BackColor = palette.GetColorFromAtariColorValue(ReturnAtariPFColors.Colpf1);
            COLPF0I.BackColor = palette.GetColorFromAtariColorValue(ReturnAtariPFColors.Colpf0);
        }

        private void HandleButtonClick(PlayFieldColor color) 
        {
            FormAtariColorPicker FormAtariColorPicker = new FormAtariColorPicker(ReturnAtariPFColors.GetColor(color));
            if (FormAtariColorPicker.ShowDialog() == DialogResult.OK && FormAtariColorPicker.CurrentColor.HasValue)
            {
                ReturnAtariPFColors.SetColor(color, FormAtariColorPicker.CurrentColor.Value);
                UpdateColors();
            }
        }
    }
}
