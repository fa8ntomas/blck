using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class FormBitmapToMap : Form
    {

        public Map ReturnMap { get; set; }
        public CharacterSet ReturnCharactedSet { get; set; }

        private Image _Ori2ginalImage;
        private string fileName;
        private byte[] fnt;

        public MapSet MapSet { get; set; }

        public FormBitmapToMap(MapSet mapset, Image originalImage, string fileName, CharacterSet characterSet=null)
        {
            InitializeComponent();

            pictureGrid1.OnColorPicked += new EventHandler(pictureGrid1_OnColorPicked);
            fiveColorPickerUserControl1.OnPickColor += new EventHandler(fiveColorPickerUserControl1_onPickColor);

            this.OriginalImage = originalImage;
            this.fileName = fileName;
            MapSet = mapset;

            fnt = characterSet == null ? Properties.Resources.font0 : (byte[])characterSet.Data.Clone();
        }

       
        private Image OriginalImage
        {
            get { return _Ori2ginalImage; }
            set { _Ori2ginalImage = value; this.pictureGrid1.OriginalImage = value; }
        }

     
        private void fiveColorPickerUserControl1_onPickColor(object sender, EventArgs e)
        {
            ColorsPickerUserControl.MessageEventArgs messageEventArgs = (ColorsPickerUserControl.MessageEventArgs)e;

            pictureGrid1.SetPickerMod(messageEventArgs.playFieldColor, messageEventArgs.index);
        }

        private void pictureGrid1_OnColorPicked(object sender, EventArgs e)
        {
            PictureGrid.ColorPickedEventArgs colorPickedEventArgs = (PictureGrid.ColorPickedEventArgs)e;
            fiveColorPickerUserControl1.SetColor(colorPickedEventArgs.PlayFieldColor, colorPickedEventArgs.Color, colorPickedEventArgs.index);
        }

        private HashSet<Color> ExtractColors(Bitmap bmp)
        {
            HashSet<Color> colorsSet = new HashSet<Color>();

            if (_Ori2ginalImage != null)
            {
                for (int col = 0; col < _Ori2ginalImage.Width / 4; col++)
                {
                    for (int row = 0; row < _Ori2ginalImage.Height / 8; row++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 3; x++)
                            {
                               colorsSet.Add(bmp.GetPixel(col * 4 + x, row * 8 + y));
                            }
                        }
                    }
                }
            }

            return colorsSet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_Ori2ginalImage != null)
            {
                using (Bitmap bmp = new Bitmap(_Ori2ginalImage))
                {

                    HashSet<Color> colorsSet = ExtractColors(bmp);

                    List<Color> colorsList = new List<Color>(colorsSet);
                    Dictionary<List<byte>, int> charset = new Dictionary<List<byte>, int>(new SequenceComparer<byte>());
                    List<bool?> bits7 = new List<bool?>();
                    List<int> map = new List<int>();

                    PrefillCharset(charset);

                    int PrefillCharsetSize = charset.Keys.Count;

                    int curchar = -1;

                    for (int row = 0; row < _Ori2ginalImage.Height / 8; row++)
                    {
                        for (int col = 0; col < _Ori2ginalImage.Width / 4; col++)
                        {
                            List<byte> bitc = new List<byte>();
                            bool? bit7 = null;
                            for (int y = 0; y < 8; y++)
                            {
                                int b = 0;
                                for (int x = 0; x < 4; x++)
                                {
                                    int colindex = 0;

                                    Color customColor = bmp.GetPixel(col * 4 + x, row * 8 + y);

                                    if (fiveColorPickerUserControl1.Colbks.Contains(customColor))
                                    {
                                        colindex = 0;
                                    }
                                    else if (fiveColorPickerUserControl1.Colpf0s.Contains(customColor))
                                    {
                                        colindex = 1;
                                    }
                                    else if (fiveColorPickerUserControl1.Colpf1s.Contains(customColor))
                                    {
                                        colindex = 2;
                                    }
                                    else if (fiveColorPickerUserControl1.Colpf2s.Contains(customColor))
                                    {
                                        colindex = 3;
                                        if (bit7 == true)
                                        {
                                            MessageBox.Show($"Problem at {col * 4 + x},{row * 8 + y}");
                                            return;
                                        }
                                        bit7 = false;
                                    }
                                    else if (fiveColorPickerUserControl1.Colpf3s.Contains(customColor))
                                    {
                                        colindex = 3;
                                        if (bit7 == false)
                                        {
                                            MessageBox.Show($"Problem at {col * 4 + x},{row * 8 + y}");
                                            return;
                                        }
                                        bit7 = true;
                                    }

                                    b = b * 4 + colindex;
                                }

                                bitc.Add((byte)b);
                            }

                            if (!charset.ContainsKey(bitc))
                            {
                                while (charset.ContainsValue(curchar))
                                {
                                    curchar--;
                                }

                                charset.Add(bitc, curchar);
                            }

                            map.Add(charset[bitc]);
                            bits7.Add(bit7);
                        }
                    }

                    int[] usedByEngine = {
                        0x00, // * *Full colbk tile
                        0x02, // + $80 Exit1
                        0x03, // + $80 Exit2
                        0x04, // + $80 Exit3
                        0x05, // + $80 Exit4
                        0x06, // + $80 * *Full colpf3 tile = black border tile **
                     //   0x0F, // + $80 Fatal
                        0x12, // + $80 Lamp3 Off
                        0x13, // + $80 Lamp1
                        0x14, // + $80 Lamp2
                        0x1E, // + $80 Lamp1 Off
                        0x1F, // + $80 Lamp2 Off
                        0x2D, // Only for map $10 - Yin - Yang extra life bonus in the castle
                        0x2E, // Only for map $10 - Yin - Yang extra life bonus in the castle
                        0x31, // + $80 Lamp3                        
                      /*  0x40, // + $80 Fatal
                        0x54, // + $80 Fatal
                        0x55, // + $80 Fatal
                        0x56, // + $80 Fatal
                        0x58, // + $80 Fatal
                        0x59, // + $80 Fatal
                        0x5A, // + $80 Fatal*/
                        0x60, // + $80 Tree
                        0x62, // Vine ???
                       /* 0x64, // + $80 Fatal
                        0x66, // + $80 Fatal if < Map 10
                        0x72, // + $80 Fatal
                        0x73, // + $80 Fatal
                        0x74, // + $80 Fatal
                        0x75, // + $80 Fatal
                        0x76, // + $80 Fatal
                        0x77, // + $80 Fatal
                        0x78, // + $80 Fatal
                        0x79, // + $80 Fatal
                        0x7a, // + $80 Fatal
                        0x7b, // + $80 Fatal
                        0x7C  // + $80 Fatal */
                    };
                     
                    if (charset.Count- PrefillCharsetSize <= 128 - usedByEngine.Length)
                    {

                        Dictionary<int, int> mapping = new Dictionary<int, int>();
                        List<int> indexesKO = new List<int>(usedByEngine);
                        List<int> indexes = new List<int>();

                        
                      

                        int reservedIndex = 128;
                        if (this.radioButtonReserve5.Checked)
                        {
                            reservedIndex -= 5;
                        }
                        else if (this.radioButtonReserve10.Checked)
                        {
                            reservedIndex -= 10;
                        }
                        else if (this.radioButtonReserve15.Checked)
                        {
                            reservedIndex -= 15;
                        }

                        for (int i = 0; i < reservedIndex; i++)
                        {
                            if (!indexesKO.Contains(i))
                            {
                                indexes.Add(i);
                            }
                        }

                        foreach (KeyValuePair<List<byte>, int> entry in charset)
                        {
                            int c = entry.Value;
                            if (c < 0 && !mapping.ContainsKey(c) && indexes.Count>0)
                            {
                                int indexe = indexes[0];
                                indexes.RemoveAt(0);
                                mapping.Add(c, indexe);
                            }
                        }

                        foreach (KeyValuePair<List<byte>, int> entry in charset)
                        {
                            int c = entry.Value;
                            if (c < 0 && mapping.ContainsKey(c))
                            {
                                int i = mapping[c] * 8;
                                foreach (byte b in entry.Key)
                                {
                                    fnt[i++] = b;
                                }
                            }
                        }

                        List<byte> mapByte = new List<byte>();

                        for (int i=0; i<map.Count; i++)
                        {
                            int b = map[i];
                            if (b < 0)
                            {
                                mapByte.Add((byte)(mapping[b] + ( (bits7[i] ?? false) ? 0x80 : 0)));
                            } else
                            {
                                mapByte.Add((byte)(b + ((bits7[i] ?? false) ? 0x80 : 0)));
                            }
                        }

                        ReturnCharactedSet = CharacterSet.CreateFromData(fnt);

                        ReturnMap = Map.CreateNewMap(MapSet,ReturnCharactedSet);

                        List<DLI> dlis = new List<DLI>();
                        RbgPFColors rbgPFColors = new RbgPFColors(fiveColorPickerUserControl1.Colbks[0], fiveColorPickerUserControl1.Colpf3s[0], fiveColorPickerUserControl1.Colpf2s[0], fiveColorPickerUserControl1.Colpf1s[0], fiveColorPickerUserControl1.Colpf0s[0]);
                        AtariPFColors dliColors = rbgPFColors.ToNearestAtariPFColors();
                        dlis.Add(new DLI(ReturnMap, dliColors, 0,-1));
                  
                        ReturnMap.DLIS = dlis.ToArray();
                        ReturnMap.MapData = mapByte.ToArray();
                  
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Too much tiles needed: {0} for {1} maximum", charset.Count, 128-usedByEngine.Length));

                        this.DialogResult = DialogResult.Abort;
                    }

                    this.Close();
                }
            }
        }

        private void PrefillCharset(Dictionary<List<byte>, int> charset)
        {
            charset.Add(new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 }, 0x00);
            charset.Add(new List<byte> { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0x06);
        }

        private class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            public bool Equals(IEnumerable<T> seq1, IEnumerable<T> seq2)
            {
                return seq1.SequenceEqual(seq2);
            }

            public int GetHashCode(IEnumerable<T> seq)
            {
                int hash = 1234567;
                foreach (T elem in seq)
                    hash = hash * 37 + elem.GetHashCode();
                return hash;
            }
        }  
    }
}
