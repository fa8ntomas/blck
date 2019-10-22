using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace BLEditor
{
    public class MapSet
    {
        public event EventHandler Changed;
        public event EventHandler MapNameChanged;
        public event EventHandler OnDLISChanged;

        public bool IsNew { get; private set; }

        bool isDirty;
        public bool IsDirty
        {
            get
            {
                if (isDirty) return true;

                foreach (Map map in maps)
                {
                    if (map.IsDirty) return true;
                }

                return false;
            }
            private set { isDirty = value;  }
        }

        protected void SetField<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                IsDirty = true;
            }
        }

        List<CharacterSet> charSets = new List<CharacterSet>();
        public ReadOnlyCollection<CharacterSet> CharSets { get { return charSets.AsReadOnly(); }  }

        private List<Map> maps = new List<Map>();
        public ReadOnlyCollection<Map> Maps { get { return maps.AsReadOnly(); } }

        public String Path { get; set; }
    
        private List<String> includes = new List<String>();
        public ReadOnlyCollection<String> Includes { get { return includes.AsReadOnly(); } }

        public void AddInclude(String include)
        {
            if (!includes.Contains(include))
            {
                includes.Add(include);
                IsDirty = true;
                Changed?.Invoke(this, null);
            }
        }

        public void AddCharSet(CharacterSet newCharSet)
        {
            charSets.Add(newCharSet);
        }


        private void RemoveUselessCharset()
        {

            List<CharacterSet> usedCharSets = new List<CharacterSet>();
            bool uselessCharset = false;
            for (int i = 0; i < CharSets.Count; i++)
            {
                bool used = false;
                for (int j = 0; j < Maps.Count && !used; j++)
                {
                    used = Maps[j].FontID == CharSets[i].UID;
                }

                if (!used && !CharSets[i].KeepEvenUseless)
                {
                    uselessCharset = true;
                } else
                {
                    usedCharSets.Add(CharSets[i]);

                }
            }

            if (uselessCharset)
            {
                IsDirty = true;
                this.charSets = usedCharSets;
            }
        }

        public void AddMap(Map newMap, bool sendChangedEvent=true)
        {
            maps.Add(newMap);
            newMap.OnDLISChanged += (s, e) => { OnDLISChanged?.Invoke(s, e); };
            if (sendChangedEvent)
            {
                Changed?.Invoke(this, null);
            }
        }

        public void AddMap(String fontFileName)
        {
            CharacterSet characterSet = CharSets.FirstOrDefault(set => set.Path == fontFileName);
            if (characterSet == null)
            {
                characterSet = CharacterSet.CreateFromFileName(fontFileName);
                AddCharSet(characterSet);
              
            }

            Map result = Map.CreateNewMap(this,characterSet);
            AddMap(result);

        }
        internal void AddMap(string rleFileName, string fontFileName)
        {
            CharacterSet characterSet = CharSets.FirstOrDefault(set => set.Path == fontFileName);
            if (characterSet == null)
            {
                characterSet = CharacterSet.CreateFromFileName(fontFileName);
                AddCharSet(characterSet);

            }

            Map map = Map.CreateNewMap(this, characterSet);

            map.Load(Path, rleFileName);
           
            AddMap(map);
        }

        public bool Save(String fileName = null)
        {
            try
            {
                Path = fileName ?? Path;

                XElement xmlTree1 = new XElement("bleditor");

                RemoveUselessCharset();

                foreach (CharacterSet characterSet in charSets)
                {
                    xmlTree1.Add(characterSet.Save(Path));
                }

                foreach (String include in Includes)
                { 
                    XElement includeElement = new XElement("include");
                    includeElement.SetValue(System.IO.Path.Combine(PathHelper.RelativePath(System.IO.Path.GetDirectoryName(include), System.IO.Path.GetDirectoryName(Path)), System.IO.Path.GetFileName(include)));
                    xmlTree1.Add(includeElement);
                }
            
                foreach (Map map in maps)
                {
                    xmlTree1.Add(map.Save(Path));
                }

                xmlTree1.Save(Path);

                SetSaved();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public bool Export(String fileName)
        {
            try
            {
                Path = fileName;

                XElement xmlTree1 = new XElement("bleditor");
                foreach (CharacterSet characterSet in charSets)
                {
                    xmlTree1.Add(characterSet.Save(Path, true));
                }

                foreach (Map map in maps)
                {
                    xmlTree1.Add(map.Save(Path,true));
                }

                xmlTree1.Save(Path);

                SetSaved();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }


        public static bool SSave(MapSet mapSet)
        {
            if (String.IsNullOrEmpty(mapSet.Path))
            {
                return SSaveAs(mapSet);
            }
            else
            {
                return mapSet.Save();
            }
        }

        public static bool SSaveAs(MapSet mapSet)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Mapset|*.xml";
            saveFileDialog1.Title = "Save Mapset";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                return mapSet.Save(saveFileDialog1.FileName);
            }

            return false;
        }
        public void SetSaved()
        {
            IsDirty = false;
            IsNew = false;

            foreach (Map map in maps)
            {
                map.SetSaved();
            }
        }

   

        public void Load(String fileName)
        {
            Clear(false);

            Path = fileName;

            XElement xml = XElement.Load(fileName);

            foreach (XElement node in xml.Elements("font"))
            {
                CharacterSet characterSet = CharacterSet.Load(this, node);
                AddCharSet(characterSet);
            }

            foreach (XElement node in xml.Elements("include"))
            {
                AddInclude(node.Value);
            }

            foreach (XElement node in xml.Elements("map"))
            {

                Map loadedMap = Map.Load(this, node);

                AddMap(loadedMap, false);

                CharacterSet characterSet = CharSets.FirstOrDefault(set => set.UID == loadedMap.FontID);
                if (characterSet == null)
                {
                    MessageBox.Show("Unknown font: " + loadedMap.FontID);
                }          
            }

            IsNew = false;
            IsDirty = false;

            Changed?.Invoke(this, null);
        }

        internal CharacterSet GetFont(Guid? FontID)
        {
            if (FontID == null)
            {
                throw new ArgumentNullException();
            }

            return CharSets.FirstOrDefault(set => set.UID == FontID);
        }

        internal Map GetMap(System.Guid? ID)
        {
            return maps.FirstOrDefault(set => set.UID == ID);
        }

        internal byte GetMapIndex(Guid? mapID)
        {
            int index = this.maps.FindIndex(m => m.UID == mapID);
            return (byte)((index>=0)?index: 0);
        }

        private void Clear( bool sendChangedEvent = true)
        {
            IsDirty = false;
            IsNew = true;
            charSets.Clear();
           
            maps.Clear();

            Path = null;
            if (sendChangedEvent)
            {
                Changed?.Invoke(this, null);
            }
        }

        internal void New()
        {
            Clear();
        }

        internal void ChangeMapName(Map Map, String NewName)
        {
            if (!String.Equals(Map.Name, NewName))
            {
                MapNameChangedEventArgs MapNameChangedEventArgs = new MapNameChangedEventArgs(Map, Map.Name, NewName);
                Map.Name = NewName;
                MapNameChanged?.Invoke(this, MapNameChangedEventArgs);
            }
        }

        internal void DeleteMap(Map map)
        {
            maps.Remove(map);
            Changed?.Invoke(this, null);
        }      
    }
}
