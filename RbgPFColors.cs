using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    public class RbgPFColors
    {
        public enum PlayFieldColor{
            COLBK,
            COLPF0,
            COLPF1,
            COLPF2,
            COLPF3
        };

        public RbgPFColors(Color back, Color pf3, Color pf2, Color pf1, Color pf0 )
        {
            Colbk = back;
            Colpf3 = pf3;
            Colpf2 = pf2;
            Colpf1 = pf1;
            Colpf0 = pf0;
        }

        public RbgPFColors()
        {
            Colbk = Color.FromArgb(128, 128, 128);
            Colpf3 = Color.FromArgb(255, 0, 0);
            Colpf2 = Color.FromArgb(255, 255, 255);
            Colpf1 = Color.FromArgb(192, 192, 192);
            Colpf0 = Color.FromArgb(95, 95, 95) ;

        }

        public Color Colbk { get; set; }
        public Color Colpf3 { get; set; }
        public Color Colpf2 { get; set; }
        public Color Colpf1 { get; set; }
        public Color Colpf0 { get; set; }

        public void SetColor(RbgPFColors.PlayFieldColor playFieldColor, Color color)
        {
            switch (playFieldColor)
            {
                case PlayFieldColor.COLBK:
                    Colbk = color;
                    break;
                case PlayFieldColor.COLPF0:
                    Colpf0 = color;
                    break;
                case PlayFieldColor.COLPF1:
                    Colpf1 = color;
                    break;
                case PlayFieldColor.COLPF2:
                    Colpf2 = color;
                    break;
                case PlayFieldColor.COLPF3:
                    Colpf3 = color;
                    break;
            }
        }

        public AtariPFColors ToNearestAtariPFColors() {
            Palette p= Palette.GetDefaultPalette();
            byte aCOLBK = ((Colbk != null) ? p.GetNearestColor(Colbk) : (byte)0);
            byte aCOLPF0 = ((Colpf0 != null) ? p.GetNearestColor(Colpf0) : (byte)0);
            byte aCOLPF1 = ((Colpf1 != null) ? p.GetNearestColor(Colpf1) : (byte)0);
            byte aCOLPF2 = ((Colpf2 != null) ? p.GetNearestColor(Colpf2) : (byte)0);
            byte aCOLPF3 = ((Colpf3 != null) ? p.GetNearestColor(Colpf3) : (byte)0);
            return new AtariPFColors(aCOLBK, aCOLPF3, aCOLPF2, aCOLPF1, aCOLPF0);
        }
    }
}
