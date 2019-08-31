using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class DLIListUserControl : UserControl
    {
        public DLIListUserControl()
        {
            InitializeComponent();

            buttonNew.Click += (s, e) => { AddDLI(); };
            buttonRemove.Click += (s, e) => { RemoveDLI(dlis.SelectedIndex); };
        }

     
        private Palette palette = Palette.GetDefaultPalette();

        public Map _Map;
        public Map Map { private get { return _Map; } set { _Map = value; SetDLIS(_Map); } }

        private void SetDLIS(Map map)
        {
            dlis.SelectedIndex = -1;

            dlis.Items.Clear();

            if (map != null)
            {
                foreach (DLI item in map.DLIS)
                {
                    dlis.Items.Add(item.ToDLIListEntry());
                }

            }

            buttonNew.Enabled = dlis.Items.Count>0;
        }

        private void dlis_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0)
            {  
                DLIListEntry DLIListEntry = ((DLIListEntry)dlis.Items[e.Index]);

                e.Graphics.DrawString(DLIListEntry.ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

                Rectangle rect = e.Bounds;
                rect.Height -= 3;
                rect.X += 45;
                rect.Y += 1;
                rect.Width = 18;

                AtariPFColors DliColors = DLIListEntry.dli.AtariPFColors;

                byte[] colorValues = { DliColors.Colbk, DliColors.Colpf3, DliColors.Colpf2, DliColors.Colpf1, DliColors.Colpf0 };
       
                foreach (byte colorValue in colorValues)
                {
                    using (Brush brush = new SolidBrush(palette.GetColorFromAtariColorValue(colorValue)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                        e.Graphics.DrawRectangle(Pens.Black, rect);
                        rect.X += rect.Width + 2;
                    }
                }
            } 
        }

        private void dlis_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = dlis.SelectedIndex >= 0;
            buttonEdit.Enabled = dlis.SelectedIndex >= 0;
        }

        private void dlis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.dlis.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                EditDli(index);
            }
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlis.SelectedIndex >= 0)
            {
                EditDli(dlis.SelectedIndex);
            }
        }

        private void EditDli(int index)
        {
            DLIListEntry mapListEntry = (DLIListEntry)this.dlis.Items[index];
            FormDLI FormDLI = new FormDLI(mapListEntry.dli);
            FormDLI.Text = "Edit DLI";
            if (FormDLI.ShowDialog() == DialogResult.OK)
            {
                if (Map.ChangeDLI(mapListEntry.dli, FormDLI.ReturnIntLine, FormDLI.ReturnAtariPFColors, FormDLI.ReturnOrderValue))
                {
                    dlis.Refresh();
                }
            }
        }

        private void AddDLI()
        {
            DLI dli  = null;

            if (this.dlis.SelectedIndex != -1)
            {
                dli = ((DLIListEntry)this.dlis.Items[this.dlis.SelectedIndex]).dli;
            }
            FormDLI FormDLI = new FormDLI(dli, dli == null ? -1 : dli.IntLine + 1)
            {
                Text = "Add DLI"
            };
            if (FormDLI.ShowDialog() == DialogResult.OK)
            {
                Map.AddDLI(FormDLI.ReturnIntLine, FormDLI.ReturnAtariPFColors, FormDLI.ReturnOrderValue);
            }
        }

        private void RemoveDLI(int dliIndex)
        {
            if (dliIndex > 0)
            {
                Map.RemoveDLI(((DLIListEntry)this.dlis.Items[dliIndex]).dli);
            }
            else
            {
                MessageBox.Show("You can not delete the first DLI (Init DLI).");
            }
        }

    }
}
