using BLEditor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLEditor
{
    public class CharacterSet
    {
       
        private CharacterSet(String uid=null)
        {
              UID = GuidHelper.parse(uid) ?? Guid.NewGuid();
        }
        
        public static CharacterSet CreateFromData(byte[] fnt)
        {
            CharacterSet result = new CharacterSet();
            result.Data = fnt;
            return result;
        }

        public static CharacterSet CreateFromFileName(String fontFileName)
        {
            CharacterSet result = new CharacterSet();
            result.LoadData( fontFileName);
            return result;
        }

        public byte[] Data { get; set; }
        public Guid UID { get; private set; }
        public String Path { get; set; }

        private void LoadData(String path)
        {
            Path = path;
            Data = File.ReadAllBytes(Path);
        }
 
        public static CharacterSet Load(MapSet mapSet, XElement mapElemept)
        {
            CharacterSet result = new CharacterSet(mapElemept.Attribute("uid")?.Value);

        
            String Path = mapElemept.Attribute("path")?.Value;
            if (!File.Exists(Path))
            {
                Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(mapSet.Path), Path);
            }

            result.LoadData(Path);

            return result;
        }

        public XElement Save(string MapSetSaveFileName, bool moveToMapSetDirectory=false)
        {
            if (!moveToMapSetDirectory)
            {
                if (String.IsNullOrWhiteSpace(Path))
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), "font.fnt");
                }
            } else
            {
                if (String.IsNullOrWhiteSpace(Path))
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), "font.fnt");
                }
                else
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(Path));

                }
            }

            File.WriteAllBytes(Path, Data);
         
            XElement result = new XElement("font");
            result.Add(new XAttribute("uid", UID.ToString()));
            result.Add(new XAttribute("path", System.IO.Path.Combine(PathHelper.RelativePath(System.IO.Path.GetDirectoryName(Path), System.IO.Path.GetDirectoryName(MapSetSaveFileName)), System.IO.Path.GetFileName(Path))));
            return result;
        }

  
    }
}
