using BLEditor.Helpers;
using GuiLabs.Undo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BLEditor
{
    public class Map : IEquatable<Map>
    {
        public enum TypeColorDetection
        {
            None = 0,
            Always = 1,
            Inside = 2,
            Outside = 3
        };

        public enum ZoneColorDetection
        {
            Always = 0,
            Flag0 = 1,
            Flag1 = 2
        }

        public event EventHandler OnDLISChanged;

        protected EventHandlerList listEventDelegates = new EventHandlerList();
        static readonly object mapChangedEventKey = new object();

        public event EventHandler MapChanged
        {
            // Add the input delegate to the collection.
            add
            {
                listEventDelegates.AddHandler(mapChangedEventKey, value);
                //    value.Invoke(this, new InterationChangedEventArgs(_interation, CurrentStamp));
            }
            // Remove the input delegate from the collection.
            remove
            {
                listEventDelegates.RemoveHandler(mapChangedEventKey, value);
            }
        }

        public static Map EMPTY = CreateEmptyMap();

        static int counter = -1;

        public Guid UID { get; set; }

        public MapSet MapSet { get; private set; }

        private DLI[] dlis;
        public DLI[] DLIS
        {
            get { return dlis; }
            set { DLI[] sortedValue = value?.OrderBy(o => o.IntLine).ToArray(); SetField(ref dlis, sortedValue); CreateDLIMapIndex(); }
        }

        Guid fontID;
        public Guid FontID {
            get { return fontID; }
            set { SetField(ref fontID, value); }
        }

        byte[] mapData;
        public byte[] MapData {
            get { return mapData; }
            set { SetField(ref mapData, value); }
        }

        internal byte GetMapDataByte(Point point)
        {
            if (mapRectangle.Contains(point))
            {
                return mapData[point.X + 40 * point.Y];
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static readonly Rectangle mapRectangle = new Rectangle(0, 0, 40, 11);

        internal bool Contains(Point point)
        {
            return mapRectangle.Contains(point);
        }
        internal bool Contains(Rectangle rect)
        {
            return mapRectangle.Contains(rect);
        }


        class SetMapDataBytesAction : AbstractAction
        {
            private readonly Map map;
            private readonly Point point;
            private readonly MapDataBlock datab;
            private List<byte> oldBytes;

            public SetMapDataBytesAction(Map map, Point point, MapDataBlock datab)
            {
                this.map = map;
                this.point = point;
                this.datab = datab;

            }
            protected override void ExecuteCore()
            {
                oldBytes = CopyData(map, point, datab.Size, datab.Bytes);
                if (HadAction())
                {
                    map.SetChanged();
                }
            }
            private static List<byte> CopyData(Map map, Point point, Size size, byte[] data)
            {
                Debug.Assert(data != null && size != null && data.Length == size.Width * size.Height);

                List<byte> result = new List<byte>();
                Rectangle dataRec = new Rectangle(point, size);
                Rectangle updateRectangle = Rectangle.Intersect(mapRectangle, dataRec);
              
                bool updated = false;

                if (!updateRectangle.IsEmpty)
                {
                    int yoffset = dataRec.Y < 0 ? -dataRec.Y : 0;
     
                    for (int y = 0; y < updateRectangle.Height; y++)
                    {
                        int xoffset = dataRec.X < 0 ? -dataRec.X : 0;
                        for (int x = 0; x < updateRectangle.Width; x++)
                        {
                            byte newByte = data[(y + yoffset) * size.Width + xoffset + x];
                            
                            int byteIndex = x + updateRectangle.X + mapRectangle.Width * (y + updateRectangle.Y);
                            
                            byte oldByte = map.mapData[byteIndex];
                            
                            result.Add(oldByte);

                            if (oldByte!=newByte)
                            {
                                map.mapData[byteIndex] = newByte;
                                updated = true;
                            }
                        }
                    }
                }

                return updated ? result : null;
            }

            protected override void UnExecuteCore()
            {
                if (HadAction())
                {
                    CopyData(map, point, datab.Size, oldBytes.ToArray());
                    map.SetChanged();
                }
            }

            private bool HadAction()
            {
                return oldBytes!=null;
            }
        }

        private void SetChanged()
        {
            IsDirty = true;
            ((EventHandler)listEventDelegates[mapChangedEventKey])?.Invoke(this, null);
        }

        public void SetMapDataByte(Point point, byte b)
        {
            UndoManager.RecordAction(new SetMapDataBytesAction(this, point, new MapDataBlock(new Size(1, 1), new byte[] { b })));
        }

        public void SetMapDataBytes(Point point, MapDataBlock data)
        {
            UndoManager.RecordAction(new SetMapDataBytesAction(this, point, data));
        }

        public void ClearMapDataBytes(Rectangle toClear)
        {
            MapDataBlock datab = new MapDataBlock(toClear.Size, Enumerable.Repeat((byte)0, toClear.Size.Width * toClear.Size.Height).ToArray());
            UndoManager.RecordAction(new SetMapDataBytesAction(this, toClear.Location, datab));
        }

        [Serializable]
        public class MapDataBlock
        {
            public MapDataBlock(Size _size, byte[] _bytes) { Size = _size; Bytes = _bytes; }

            public Size Size { get ; }
            public byte[] Bytes { get; }
        }

        // Creates a new data format.
        readonly static DataFormats.Format myFormat = DataFormats.GetFormat("BLCK-MAP");
        public bool CopyDataToClipboard(Rectangle toCopy)
        {
            Rectangle b = new Rectangle(0, 0, 40, 11);
            toCopy = Rectangle.Intersect(toCopy, b);
            bool copied = false;

            if (!toCopy.IsEmpty)
            {         
                List<byte> bytesToCopy = new List<byte>();
                for (int row = toCopy.Y; row < toCopy.Bottom; row++)
                {
                    for (int col = toCopy.X; col < toCopy.Right; col++)
                    {
                        bytesToCopy.Add(mapData[col + 40 * row]);
                    }
                }

                MapDataBlock myObject = new MapDataBlock(toCopy.Size, bytesToCopy.ToArray());
                DataObject myDataObject = new DataObject(myFormat.Name, myObject);
                Clipboard.SetDataObject(myDataObject);
                
                copied = true;
            }

            return copied;
        }

        public MapDataBlock GetMapClipboardData()
        {
           return (MapDataBlock)Clipboard.GetDataObject().GetData(myFormat.Name);
        }

        string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value); }
        }

        public string Path { get; set; }
        public string InitRoutinePath { get; set; }
        public string InitRoutine { get; set; }
        public string TileCollisionRoutine { get; set; }
        public string TileCollisionRoutinePath { get; set; }

        public string ExecRoutinePath { get; set; }
        public string ExecRoutine { get; set; }
        
        //https://stackoverflow.com/questions/805505/c-sharp-marking-class-property-as-dirty
        bool isDirty;
        public bool IsDirty {
            get
            {
                if (isDirty) return true;
                if (DLIS != null)
                {
                    foreach (DLI dli in DLIS)
                    {
                        if (dli.IsDirty) return true;
                    }
                }
                return false;
            }
            private set { 
                isDirty = value; 
            }
        }

        public bool IsNew { get; private set; }

        protected void SetField<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                IsDirty = true;
            }
        }

        public static String Delta(String XMLFilename, String Filename)
        {
            if (String.IsNullOrWhiteSpace(Filename))
            {
                return String.Empty;
            }

            String result= System.IO.Path.Combine(PathHelper.RelativePath(System.IO.Path.GetDirectoryName(Filename), System.IO.Path.GetDirectoryName(XMLFilename)), System.IO.Path.GetFileName(Filename));

            Console.WriteLine(System.IO.Path.GetDirectoryName(Filename) + " -- " + XMLFilename + "--" + PathHelper.RelativePath(System.IO.Path.GetDirectoryName(Filename), System.IO.Path.GetDirectoryName(XMLFilename)));
            return result;
        }

        public XElement Save(String MapSetSaveFileName, bool moveToMapSetDirectory = false)
        {
            if (!moveToMapSetDirectory)
            {
                if (String.IsNullOrWhiteSpace(Path))
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"{Name}.rle");
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(Path))
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"{Name}.rle");
                }
                else
                {
                    Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(Path));
                }
            }

            File.WriteAllBytes(Path, EncodeRLE(mapData.ToList()).ToArray());


            XElement result = new XElement("map");
            result.Add(new XAttribute("name", Name));
            result.Add(new XAttribute("font", FontID));
            result.Add(new XAttribute("path", Delta(MapSetSaveFileName, Path)));

            if (UID != null)
            {
                result.Add(new XAttribute("uid", UID.ToString()));
            }

            // DLI

            XElement dlis = new XElement("dlis");
            if (DLIS != null)
            {
                foreach (DLI dli in DLIS)
                {
                    dlis.Add(dli.Save());
                }
            }

            result.Add(dlis);

            // Init Routine

            if (!String.IsNullOrWhiteSpace(InitRoutine))
            {
                if (!moveToMapSetDirectory)
                {
                    if (String.IsNullOrWhiteSpace(InitRoutinePath))
                    {
                        InitRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Init {Name}.asm");
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(InitRoutinePath))
                    {
                        InitRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Init {Name}.asm");
                    }
                    else
                    {
                        InitRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(InitRoutinePath));
                    }
                }

                File.WriteAllText(InitRoutinePath, InitRoutine);
            }
            result.Add(new XElement("InitRoutinePath", Delta(MapSetSaveFileName, InitRoutinePath)));

            // Exec Routine

            if (!String.IsNullOrWhiteSpace(ExecRoutine))
            {
                if (!moveToMapSetDirectory)
                {
                    if (String.IsNullOrWhiteSpace(ExecRoutinePath))
                    {
                        ExecRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Exec {Name}.asm");
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(ExecRoutinePath))
                    {
                        ExecRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Exec {Name}.asm");
                    }
                    else
                    {
                        ExecRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(ExecRoutinePath));

                    }

                }

                File.WriteAllText(ExecRoutinePath, ExecRoutine);
            }

            result.Add(new XElement("ExecRoutinePath", Delta(MapSetSaveFileName, ExecRoutinePath)));
            
            // TileCollision Routine

            if (!String.IsNullOrWhiteSpace(TileCollisionRoutine))
            {
                if (!moveToMapSetDirectory)
                {
                    if (String.IsNullOrWhiteSpace(TileCollisionRoutinePath))
                    {
                        TileCollisionRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"TileCollision {Name}.asm");
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(TileCollisionRoutinePath))
                    {
                        TileCollisionRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"TileCollision {Name}.asm");
                    }
                    else
                    {
                        TileCollisionRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(TileCollisionRoutinePath));

                    }

                }

                File.WriteAllText(TileCollisionRoutinePath, TileCollisionRoutine);
            }

            result.Add(new XElement("TileCollisionRoutinePath", Delta(MapSetSaveFileName, TileCollisionRoutinePath)));


            result.Add(ExportColorDetection("Colpf0Dectection", this.Colpf0Detection, this.Colpf0DetectionRects, this.Colpf0DetectionFlags));
            result.Add(ExportColorDetection("Colpf2Dectection", this.Colpf2Detection, this.Colpf2DetectionRects, this.Colpf2DetectionFlags));
            result.Add(ExportColorDetection("Colpf3Dectection", this.Colpf3Detection, this.Colpf3DetectionRects, this.Colpf3DetectionFlags));


            result.Add(new XElement("foe", Foe));
            result.Add(new XElement("brucestart", new XElement("x1", this.BruceStartX1), new XElement("x2", this.BruceStartX2), new XElement("y1", this.BruceStartY1), new XElement("y2", this.BruceStartY2)));
            result.Add(new XElement("exit1", new XElement("map", this.Exit1MapID), new XElement("x", this.Exit1X), new XElement("y", this.Exit1Y)));
            result.Add(new XElement("exit2", new XElement("map", this.Exit2MapID), new XElement("x", this.Exit2X), new XElement("y", this.Exit2Y)));
            result.Add(new XElement("exit3", new XElement("map", this.Exit3MapID), new XElement("x", this.Exit3X), new XElement("y", this.Exit3Y)));
            result.Add(new XElement("exit4", new XElement("map", this.Exit4MapID), new XElement("x", this.Exit4X), new XElement("y", this.Exit4Y)));
            result.Add(new XElement("YamoSpawnPosition", this.YamoSpawnPosition));
            result.Add(new XElement("NinjaSpawnPosition", this.NinjaSpawnPosition));
            result.Add(new XElement("NinjaEnterCount1", this.NinjaEnterCount1));
            result.Add(new XElement("NinjaEnterCount2", this.NinjaEnterCount2));
            result.Add(new XElement("YamoEnterCount1", this.YamoEnterCount1));
            result.Add(new XElement("YamoEnterCount2", this.YamoEnterCount2));

            return result;
        }

        static private XElement ExportColorDetection( string tagName, TypeColorDetection ColpfDectection,List<Rectangle> ColpfDectectionRect, List<ZoneColorDetection> ColpfDetectionFlags)
        {
            XElement XElementColpfDectection = new XElement(tagName, new XElement("Type", ColpfDectection));
            if (ColpfDectection == TypeColorDetection.Inside || ColpfDectection == TypeColorDetection.Outside)
            {
                XElement XElementZone = new XElement("Zones");
                foreach (RectangleF rect in ColpfDectectionRect)
                {
                    XElementZone.Add(new XElement("rect", new XElement("Top", rect.Top), new XElement("Left", rect.Left), new XElement("Bottom", rect.Bottom), new XElement("Right", rect.Right)));
                }

                XElementColpfDectection.Add(XElementZone);

                XElement XElementFlags = new XElement("Flags");
                foreach (ZoneColorDetection flag in ColpfDetectionFlags)
                {
                    XElementFlags.Add(new XElement("Flag", flag));
                }

                XElementColpfDectection.Add(XElementFlags);
            }
            return XElementColpfDectection;
        }

    
        public void SetSaved()
        {
            IsDirty = false;
            IsNew = false;
            if (DLIS != null)
            {
                foreach (DLI dli in DLIS)
                {
                    dli.SetSaved();
                }
            }
         }

        public static Map Load(MapSet mapSet, XElement mapElemept)
        {
            Map map = CreateNewMap(mapSet, mapSet.GetFont(GuidHelper.parse(mapElemept.Attribute("font")?.Value)), mapElemept.Attribute("uid")?.Value);
            map.Name = mapElemept.Attribute("name").Value;
            map.Path = mapElemept.Attribute("path").Value;
            if (!File.Exists(map.Path))
            {
                map.Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(mapSet.Path), map.Path);
            }

            map.MapData = DecodeRLE(File.ReadAllBytes(map.Path));

            // DLI
            XElement dlis = mapElemept.Element("dlis");
            List<DLI> DLIList = new List<DLI>();
            foreach (XElement xdli in dlis.Elements("dli"))
            {
                DLI dli = DLI.Load(map, xdli);
                DLIList.Add(dli);
            }
            map.DLIS = DLIList.ToArray();

            ImportTextFile(mapElemept, "InitRoutinePath", mapSet.Path, (Path, Content) => { map.InitRoutinePath = Path; map.InitRoutine = Content; });
            ImportTextFile(mapElemept, "ExecRoutinePath", mapSet.Path,(Path, Content) => { map.ExecRoutinePath = Path; map.ExecRoutine = Content; });
            ImportTextFile(mapElemept, "TileCollisionRoutinePath", mapSet.Path, (Path, Content) => { map.TileCollisionRoutinePath = Path; map.TileCollisionRoutine = Content; });
            ImportBoolean(mapElemept, "foe", value => map.Foe = value);
            ImportByte(mapElemept, "YamoSpawnPosition", value => map.YamoSpawnPosition= value);
            ImportByte(mapElemept, "NinjaSpawnPosition", value => map.NinjaSpawnPosition = value);
            ImportByte(mapElemept, "NinjaEnterCount1", value => map.NinjaEnterCount1 = value);
            ImportByte(mapElemept, "NinjaEnterCount2", value => map.NinjaEnterCount2 = value);
            ImportByte(mapElemept, "YamoEnterCount1", value => map.YamoEnterCount1 = value);
            ImportByte(mapElemept, "YamoEnterCount2", value => map.YamoEnterCount2 = value);
            ImportColorDetection(mapElemept, "Colpf0Dectection", ref map.Colpf0Detection, ref map.Colpf0DetectionRects, ref map.Colpf0DetectionFlags);
            ImportColorDetection(mapElemept, "Colpf2Dectection", ref map.Colpf2Detection, ref map.Colpf2DetectionRects, ref map.Colpf2DetectionFlags);
            ImportColorDetection(mapElemept, "Colpf3Dectection", ref  map.Colpf3Detection, ref map.Colpf3DetectionRects, ref map.Colpf3DetectionFlags);

            Console.WriteLine(map.Colpf0Detection.ToString());
            if (mapElemept.Elements("brucestart").Any())
            {
                XElement brucestart = mapElemept.Element("brucestart");
                map.BruceStartX1 = Convert.ToByte(brucestart.Element("x1").Value);
                map.BruceStartY1 = Convert.ToByte(brucestart.Element("y1").Value);
                map.BruceStartX2 = Convert.ToByte(brucestart.Element("x2").Value);
                map.BruceStartY2 = Convert.ToByte(brucestart.Element("y2").Value);
            }

            if (mapElemept.Elements("exit1").Any())
            {
                XElement exit = mapElemept.Element("exit1");
                map.Exit1MapID = GuidHelper.parse(exit.Element("map").Value);
                map.Exit1X = Convert.ToByte(exit.Element("x").Value);
                map.Exit1Y = Convert.ToByte(exit.Element("y").Value);
            }

            if (mapElemept.Elements("exit2").Any())
            {
                XElement exit = mapElemept.Element("exit2");
                map.Exit2MapID = GuidHelper.parse(exit.Element("map").Value);
                map.Exit2X = Convert.ToByte(exit.Element("x").Value);
                map.Exit2Y = Convert.ToByte(exit.Element("y").Value);
            }

            if (mapElemept.Elements("exit3").Any())
            {
                XElement exit = mapElemept.Element("exit3");
                map.Exit3MapID = GuidHelper.parse(exit.Element("map").Value);
                map.Exit3X = Convert.ToByte(exit.Element("x").Value);
                map.Exit3Y = Convert.ToByte(exit.Element("y").Value);
            }

            if (mapElemept.Elements("exit4").Any())
            {
                XElement exit = mapElemept.Element("exit4");
                map.Exit4MapID = GuidHelper.parse(exit.Element("map").Value);
                map.Exit4X = Convert.ToByte(exit.Element("x").Value);
                map.Exit4Y = Convert.ToByte(exit.Element("y").Value);
            }


            map.IsDirty = false;
            map.IsNew = false;
            return map;
        }

      
        private static void ImportTextFile(XElement mapElemept, string tagName, string mapSetPath, Action<string,string> setValue)
        {
            if (mapElemept.Elements(tagName).Any())
            {
                String Path = mapElemept.Element(tagName).Value.Trim();
                if (!String.IsNullOrWhiteSpace(Path))
                {
                    if (!File.Exists(Path))
                    {
                        Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(mapSetPath), Path);
                    }

                    String Content = null;
                    if (File.Exists(Path))
                    {
                        Content = File.ReadAllText(Path);
                    }

                    setValue(Path, Content);
                }
            }
        }

        private static void ImportByte(XElement mapElemept, string tagName, Action<byte> setValue, uint defaultValue =256)
        {
            if (mapElemept.Elements(tagName).Any())
            {
                setValue(Convert.ToByte(mapElemept.Element(tagName).Value.Trim()));
            } else if (defaultValue<256)
            {
                setValue(Convert.ToByte(defaultValue));
            }
        }
        private static void ImportBoolean(XElement mapElemept, string tagName, Action<bool> setValue, bool? defaultValue = null)
        {
            if (mapElemept.Elements(tagName).Any())
            {
                setValue(Convert.ToBoolean(mapElemept.Element(tagName).Value.Trim()));
            }
            else if (defaultValue.HasValue)
            {
                setValue(defaultValue.Value);
            }
        }
        private static void ImportColorDetection(XElement mapElemept, string tagName, ref TypeColorDetection ColpfDectection, ref List<Rectangle> ColpfDectectionRect, ref List<ZoneColorDetection> ColpfDetectionFlags)
        {
            if (mapElemept.Elements(tagName).Any())
            {
                XElement Colpf2Dectection = mapElemept.Element(tagName);
                if (Colpf2Dectection.Elements("Type").Any())
                {
                    ColpfDectection = (TypeColorDetection)Enum.Parse(typeof(TypeColorDetection), Colpf2Dectection.Element("Type").Value.Trim());
                    if ((ColpfDectection == TypeColorDetection.Inside || ColpfDectection == TypeColorDetection.Outside) && Colpf2Dectection.Elements("Zones").Any())
                    {
                        foreach (XElement item in Colpf2Dectection.Element("Zones").Descendants("rect"))
                        {
                            UserRect a = UserRect.FromXElement(item);
                            if (a != null)
                            {
                                ColpfDectectionRect.Add(a.GetRectangle());
                            }
                        }

                        if (Colpf2Dectection.Elements("Flags").Any())
                        {
                            foreach (XElement item in Colpf2Dectection.Element("Flags").Descendants("Flag"))
                            {
                                var zoneColorDetection = (ZoneColorDetection)Enum.Parse(typeof(ZoneColorDetection), item.Value.Trim());
                                 ColpfDetectionFlags.Add(zoneColorDetection);
                            }
                        }

                        if (ColpfDetectionFlags.Count != ColpfDectectionRect.Count)
                        {
                            ColpfDetectionFlags.Clear();
                            for (int i=0; i < ColpfDectectionRect.Count; i++)
                            {
                                ColpfDetectionFlags.Add(ZoneColorDetection.Always);
                            }
                        }
                    }
                }
            }
        }

        public void Load(String path, String rleFileName)
        {
            Path = rleFileName;
          
    
            if (!File.Exists(Path))
            {
                Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), Path);
            }

            MapData = DecodeRLE(File.ReadAllBytes(Path));

            IsDirty = false;
            IsNew = false;
           
        }


        internal static Map CreateNewMap(MapSet mapset, CharacterSet characterSet, String uid=null )
        {
            if (characterSet == null || mapset==null)
            {
                throw new ArgumentOutOfRangeException();
            }
         
            Map result = new Map
            {
                FontID  = characterSet.UID,
                UID      = GuidHelper.parse(uid) ?? Guid.NewGuid(),
                MapSet  = mapset
            };

            return result;
        }

        private Map()
        {
            Interlocked.Increment(ref counter);

            MapData = new byte[11 * 40];
            Name = "New Map " + counter;
            DLIS = new DLI[] { new DLI( this, new AtariPFColors(), 0) };

            IsDirty = true;
            IsNew = true;
        }

        private static Map CreateEmptyMap()
        {
            Map result = new Map
            {
                Name = ""
            };
            return result;
        }

        public void RemoveDLI(DLI dli)
        {
            List<DLI> dlis = new List<DLI>(DLIS);
            dlis.Remove(dli);
            DLIS = dlis.ToArray();
            OnDLISChanged?.Invoke(this, null);
        }

        public bool AddDLI(int intLine, AtariPFColors returnAtariPFColors, Object returnOrderValue)
        {
            List<DLI> dlis = new List<DLI>(DLIS);
            foreach (DLI dli in dlis) {
                if (dli.IntLine == intLine)
                {
                    MessageBox.Show("There already is a DLI at line " + intLine);
                    return false;
                }
            }
            dlis.Add(new DLI(this, returnAtariPFColors, intLine, returnOrderValue));

            DLIS = dlis.ToArray();

            OnDLISChanged?.Invoke(this, null);
            return true;
        }

        public bool ChangeDLI(DLI dliChanged, int intLine, AtariPFColors returnAtariPFColors, Object returnOrderValue)
        {
            foreach (DLI dli in DLIS)
            {
                if (dli.IntLine == intLine && !dli.Equals(dliChanged))
                {
                    MessageBox.Show("There already is a DLI at line " + intLine);
                    return false;
                }
            }

            dliChanged.AtariPFColors = returnAtariPFColors;
            dliChanged.IntLine = intLine;
            dliChanged.OrderValue = returnOrderValue;

            CreateDLIMapIndex();

            OnDLISChanged?.Invoke(this, null);
            return true;
    
        }


        private static byte[] DecodeRLE(byte[] inData)
        {
            List<byte> output = new List<byte>();
            int i = 0;
            while (i < inData.Length)
            {
                int value = inData[i];
                if (value == 128)
                {
                    return output.ToArray();
                }
                if (value < 128)
                {
                    i++;
                    for (int j = 0; j < value; j++)
                    {
                        output.Add(inData[i]);
                    }
                }
                else
                {
                    value &= 0x7F;
                    for (int j = 0; j < value; j++)
                    {
                        i++;
                        output.Add(inData[i]);
                    }
                }
                i++;
            }
            return output.ToArray();
        }

        private static List<byte> EncodeRLE(List<byte> input)
        {
            List<byte> outBytes = new List<byte>();
            List<Tuple<byte, int>> tuples = new List<Tuple<byte, int>>();

            // Build a list of tuples containing the byte and the length of its run
            int i;
            int count = 1;
            byte current = input[0];
            for (i = 1; i < input.Count; i++)
            {
                if (current == input[i])
                {
                    count++;
                }
                else
                {
                    tuples.Add(new Tuple<byte, int>(current, count));
                    count = 1;
                    current = input[i];
                }
            }
            tuples.Add(new Tuple<byte, int>(current, count));

            // Encode the list of tuples as expected by the Bruce Lee RLE decoder
            i = 0;
            while (i < tuples.Count)
            {
                count = tuples[i].Item2;

                if (count > 1)
                {
                    byte value = tuples[i++].Item1;
                    int toWrite = count;
                    while (toWrite > 0)
                    {
                        outBytes.Add((byte)Math.Min(127, toWrite));
                        outBytes.Add(value);
                        toWrite = Math.Max(0, toWrite - 127);
                    }
                }
                else
                {    // Count=1

                    Queue<byte> tempArray = new Queue<byte>();

                    while (i < tuples.Count && (tuples[i].Item2 == 1 || tuples[i].Item2 == 2))
                    {
                        int nb = tuples[i].Item2;
                        byte value = tuples[i++].Item1;

                        for (int j = 0; j < nb; j++)
                        {
                            tempArray.Enqueue(value);
                        }
                    }

                    int toWrite = tempArray.Count;
                    while (toWrite > 0)
                    {
                        outBytes.Add((byte)(Math.Min(127, toWrite) + 128));
                        for (int j = 0; j < Math.Min(127, toWrite); j++)
                        {
                            outBytes.Add(tempArray.Dequeue());
                        }
                        toWrite = Math.Max(0, toWrite - 127);
                    }
                }
            }

            outBytes.Add(128);

            return outBytes;
        }

        private readonly Dictionary<int, int> colorRange = new Dictionary<int, int>();

        private void CreateDLIMapIndex()
        {
            colorRange.Clear();
            int count = 0;

            foreach (DLI dli in DLIS)
            {
                if (dli.IntLine > 0)
                {
                    int changeLine = dli.IntLine * 40 -1;
                    colorRange.Add(count, changeLine);
                    count = changeLine + 1;
                }
            }
            colorRange.Add(count, 439);
        }

        public RbgPFColors GetColors(int xCol)
        {
            for (int i = 0; i < colorRange.Count; i++)
            {
                var bob = colorRange.ElementAt(i);
                if (xCol >= bob.Key && xCol <= bob.Value)
                {
                    return DLIS[i].AtariPFColors.ToBLColor();

                }
            }

            return null;
        }

        public bool Equals(Map other) =>
            String.Equals(UID, other.UID);

        public override bool Equals(object obj) =>
                (obj is Map map) && Equals(map);

        ///////////////////////////////////////////

        public bool Foe { get; set; } = false;
        public byte BruceStartX1 { get; set; } = 0;
        public byte BruceStartY1 { get; set; } = 0;
        public byte BruceStartX2 { get; set; } = 0;
        public byte BruceStartY2 { get; set; } = 0;
        public Guid? Exit1MapID { get; set; } = null;
        public byte Exit1X { get; set; } = 0;
        public byte Exit1Y { get; set; } = 0;
        public Guid? Exit2MapID { get; set; } = null;
        public byte Exit2X { get; set; } = 0;
        public byte Exit2Y { get; set; } = 0;
        public Guid? Exit3MapID { get; set; } = null;
        public byte Exit3X { get; set; } = 0;
        public byte Exit3Y { get; set; } = 0;
        public Guid? Exit4MapID { get; set; } = null;
        public byte Exit4X { get; set; } = 0;
        public byte Exit4Y { get; set; } = 0;
        public TypeColorDetection Colpf0Detection  = TypeColorDetection.Always;
        public List<Rectangle> Colpf0DetectionRects = new List<Rectangle>();
        public List<ZoneColorDetection> Colpf0DetectionFlags = new List<ZoneColorDetection>();
        public TypeColorDetection Colpf2Detection = TypeColorDetection.Always;
        public List<Rectangle> Colpf2DetectionRects = new List<Rectangle>();
        public List<ZoneColorDetection> Colpf2DetectionFlags = new List<ZoneColorDetection>();
        public TypeColorDetection Colpf3Detection  = TypeColorDetection.Always;
        public List<Rectangle> Colpf3DetectionRects = new List<Rectangle>();
        public List<ZoneColorDetection> Colpf3DetectionFlags = new List<ZoneColorDetection>();

        public byte YamoSpawnPosition { get;  set; } = 0;
        public byte NinjaSpawnPosition { get;  set; } = 0;

        public byte NinjaEnterCount1 { get; set; } = 0;
        public byte NinjaEnterCount2 { get; set; } = 0;

        public byte YamoEnterCount1 { get; set; } = 0;
        public byte YamoEnterCount2 { get; set; } = 0;

        public List<String> Includes { get; set; }
    }

    public class MapListEntry
    {
        public Map Map { get; set; }

        public override string ToString()
        {
            return this.Map.Name;
        }
    }

    public class MapTreeNode : TreeNode
    {
   
        public Map Map { get; set; }


        public MapTreeNode(Map map, TreeNode[] array) : base(map.Name, array)
        {
            Map = map;
            Tag = pbx1.TypeNode.Map;
        }
    }

    public class FontTreeNode : TreeNode
    {

        public CharacterSet CharacterSet { get; set; }


        public FontTreeNode(MapSet mapSet, CharacterSet CharacterSet ) : base(PathHelper.RelativizePath(mapSet.Path, CharacterSet.Path))
        {
            this.CharacterSet = CharacterSet;
            Tag = pbx1.TypeNode.FontFile;
        }
    }

    public class IncludeTreeNode : TreeNode
    { 
       public String Path { get; set; }


        public IncludeTreeNode(MapSet mapSet, String path) : base(PathHelper.RelativizePath(mapSet.Path, path))
        {
            this.Path = path;
            Tag = pbx1.TypeNode.IncludeFile;
        }
    }
    public class MapNameChangedEventArgs : EventArgs
    {
        public String OldName { get; private set; }
        public String NewName { get; private set; }
        public Map Map { get; set; }

        public MapNameChangedEventArgs(Map Map, String OldName, String NewName)
          : base()
        {
            this.Map = Map;
            this.OldName = OldName;
            this.NewName = NewName;
        }
    }
}
