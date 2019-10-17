using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    public class CharTile
    {
        public byte[] Glyph { get; private set; }
        public byte Index { get; private set; }

        public CharTile(byte index, byte[] glyph)
          : base()
        {
            this.Index = index;
            this.Glyph = glyph;
        }
    }
}
