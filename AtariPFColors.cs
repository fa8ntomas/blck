using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLEditor.RbgPFColors;

namespace BLEditor
{
    public class AtariPFColors
    {

        public AtariPFColors(byte back, byte pf3, byte pf2, byte pf1, byte pf0)
        {
            Colbk = back;
            Colpf3 = pf3;
            Colpf2 = pf2;
            Colpf1 = pf1;
            Colpf0 = pf0;
        }

        public AtariPFColors()
        {
            Colbk = 0;
            Colpf3 = 10;
            Colpf2 = 20;
            Colpf1 = 30;
            Colpf0 = 40;
        }

        public AtariPFColors(AtariPFColors atariPFColors)
        {
            Colbk = atariPFColors.Colbk;
            Colpf3 = atariPFColors.Colpf3;
            Colpf2 = atariPFColors.Colpf2;
            Colpf1 = atariPFColors.Colpf1;
            Colpf0 = atariPFColors.Colpf0;
        }

        public byte Colbk { get; set; }
        public byte Colpf3 { get; set; }
        public byte Colpf2 { get; set; }
        public byte Colpf1 { get; set; }
        public byte Colpf0 { get; set; }

        public void SetColor(RbgPFColors.PlayFieldColor playFieldColor, byte colorValue)
        {
            switch (playFieldColor)
            {
                case PlayFieldColor.COLBK:
                    Colbk = colorValue;
                    break;
                case PlayFieldColor.COLPF0:
                    Colpf0 = colorValue;
                    break;
                case PlayFieldColor.COLPF1:
                    Colpf1 = colorValue;
                    break;
                case PlayFieldColor.COLPF2:
                    Colpf2 = colorValue;
                    break;
                case PlayFieldColor.COLPF3:
                    Colpf3 = colorValue;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public byte GetColor(RbgPFColors.PlayFieldColor playFieldColor)
        {
            switch (playFieldColor)
            {
                case PlayFieldColor.COLBK:
                    return Colbk;
                case PlayFieldColor.COLPF0:
                    return Colpf0;
                case PlayFieldColor.COLPF1:
                    return Colpf1;
                case PlayFieldColor.COLPF2:
                    return Colpf2;
                case PlayFieldColor.COLPF3:
                    return Colpf3;
                default:
                    throw new ArgumentException();

            }
        }
        public RbgPFColors ToBLColor()
        {
            Palette p = Palette.GetDefaultPalette();
            return new RbgPFColors(p.GetColorFromAtariColorValue(Colbk), p.GetColorFromAtariColorValue(Colpf3), p.GetColorFromAtariColorValue(Colpf2), p.GetColorFromAtariColorValue(Colpf1), p.GetColorFromAtariColorValue(Colpf0));

        }


        public Dictionary<RbgPFColors.PlayFieldColor, byte> Delta(AtariPFColors other)
        {
            Dictionary<RbgPFColors.PlayFieldColor, byte> result = new Dictionary<RbgPFColors.PlayFieldColor, byte>();

            foreach (RbgPFColors.PlayFieldColor color in Enum.GetValues(typeof(RbgPFColors.PlayFieldColor)))
            {

                if (other != null && GetColor(color) != other.GetColor(color))
                {
                    result.Add(color, GetColor(color));
                }
                else if (other == null)
                {
                    result.Add(color, GetColor(color));
                }

            }
            return result;
        }
    }
}
