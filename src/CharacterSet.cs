using BLEditor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BLEditor
{
    public class CharacterSet : IEquatable<CharacterSet>
    {

        public static readonly DataFormats.Format CharDataFormat = DataFormats.GetFormat("BLCK-CHAR");

        private CharacterSet(String uid = null, bool KeepEvenUseless = false)
        {
            UID = GuidHelper.parse(uid) ?? Guid.NewGuid();
            this.KeepEvenUseless = KeepEvenUseless;
        }

        public byte[] Data { get; set; }

        public bool KeepEvenUseless { get; }

        public String Path { get; set; }

        public Guid UID { get; }

        public static CharacterSet CreateFromData(byte[] fnt)
        {
            CharacterSet result = new CharacterSet
            {
                Data = fnt
            };
            return result;
        }

        public static CharacterSet CreateFromFileName(String fontFileName)
        {
            CharacterSet result = new CharacterSet();
            result.LoadData(PathHelper.GetExactPath(fontFileName));
            return result;
        }
        public static CharacterSet Load(MapSet mapSet, XElement mapElemept)
        {
            CharacterSet result = new CharacterSet(mapElemept.Attribute("uid")?.Value, mapElemept.Attribute("keep") != null);
            result.LoadData(PathHelper.GetExactPath(mapSet.Path, mapElemept.Attribute("path")?.Value));
            return result;
        }

        public bool Equals(CharacterSet other) =>
            String.Equals(UID, other.UID);

        public override bool Equals(object obj) =>
                (obj is CharacterSet metrics) && Equals(metrics);

        public XElement Save(string MapSetSaveFileName, bool moveToMapSetDirectory = false)
        {

            if (String.IsNullOrWhiteSpace(Path))
            {
                Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), "font.fnt");
            }
            else if (moveToMapSetDirectory)
            {
                Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(Path));
            }

            File.WriteAllBytes(Path, Data);

            XElement result = new XElement("font");
            result.Add(new XAttribute("uid", UID.ToString()));
            result.Add(new XAttribute("path", PathHelper.Delta(MapSetSaveFileName,Path)));

            if (KeepEvenUseless)
            {
                result.Add(new XAttribute("keep", "keep"));
            }

            return result;
        }

        private void LoadData(String path)
        {
            Path = path;
            Data = File.ReadAllBytes(Path);
        }
    }
}
