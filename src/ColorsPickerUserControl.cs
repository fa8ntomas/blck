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
    public partial class ColorsPickerUserControl : UserControl
    {
        public event EventHandler OnPickColor;

        public Color[] Colbks = new Color[1];
        public Color[] Colpf0s = new Color[2];
        public Color[] Colpf1s = new Color[2];
        public Color[] Colpf2s = new Color[2];
        public Color[] Colpf3s = new Color[1];

        public class MessageEventArgs : EventArgs
        {
            public RbgPFColors.PlayFieldColor playFieldColor { get; private set; }
            public int index { get; private set; }

            public MessageEventArgs(RbgPFColors.PlayFieldColor playFieldColor, int index)
              : base()
            {
                this.playFieldColor = playFieldColor;
                this.index = index;
            }
        }

        public ColorsPickerUserControl()
        {
            InitializeComponent();
            
            this.buttonColbk.Click += (s,e) => { PickColor(RbgPFColors.PlayFieldColor.COLBK,0); };
            this.buttonColpf0A.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF0,0); };
            this.buttonColpf0B.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF0,1); };
            this.buttonColpf1A.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF1,0); };
            this.buttonColpf1B.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF1, 1); };
            this.buttonColpf2A.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF2,0); };
            this.buttonColpf2B.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF2, 1); };
            this.buttonColpf3.Click += (s, e) => { PickColor(RbgPFColors.PlayFieldColor.COLPF3,0); };
        }

        private void PickColor(RbgPFColors.PlayFieldColor pfColor, int index)
        {
              this.OnPickColor?.Invoke(this, new MessageEventArgs(pfColor, index)); 
        }

        public void SetColor(RbgPFColors.PlayFieldColor playFieldColor, Color color, int index)
        {
            switch (playFieldColor)
            {
                case RbgPFColors.PlayFieldColor.COLBK:
                    {
                        Colbks[index] = color;
                        changeLabel(this.COLBKI, Colbks[0]);
   
                    }; break;
                case RbgPFColors.PlayFieldColor.COLPF0:
                    {
                        Colpf0s[index] = color;
                        changeLabel(this.COLPF0IA, Colpf0s[0]);
                        changeLabel(this.COLPF0IB, Colpf0s[1]);

                    }; break;
                case RbgPFColors.PlayFieldColor.COLPF1:
                    {
                        Colpf1s[index] = color;
                        changeLabel(this.COLPF1IA, Colpf1s[0]);
                        changeLabel(this.COLPF1IB, Colpf1s[1]);

                    }; break;
                case RbgPFColors.PlayFieldColor.COLPF2:
                    {
                        Colpf2s[index] = color;
                        changeLabel(this.COLPF2IA, Colpf2s[0]);
                        changeLabel(this.COLPF2IB, Colpf2s[1]);

                    }; break;

                case RbgPFColors.PlayFieldColor.COLPF3:
                    {
                        Colpf3s[index] = color;
                        changeLabel(this.COLPF3I, Colpf3s[0]);

                    }; break;

            }
        }

        private void changeLabel(Label labelInverse, Color customColor)
        {
            labelInverse.BackColor = customColor;
        }
    }
}
