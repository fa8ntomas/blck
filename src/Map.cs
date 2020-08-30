using BLEditor.Helpers;
using GuiLabs.Undo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BLEditor
{
    public class FontTreeNode : TreeNode
    {
        public FontTreeNode(MapSet mapSet, CharacterSet CharacterSet) : base(PathHelper.RelativizePath(mapSet.Path, CharacterSet.Path))
        {
            this.CharacterSet = CharacterSet;
            Tag = pbx1.TypeNode.FontFile;
        }

        public CharacterSet CharacterSet { get; set; }
    }

    public class IncludeTreeNode : TreeNode
    {
        public IncludeTreeNode(MapSet mapSet, String path) : base(PathHelper.RelativizePath(mapSet.Path, path))
        {
            this.Path = path;
            Tag = pbx1.TypeNode.IncludeFile;
        }

        public String Path { get; set; }
    }

    public class Map : IEquatable<Map>
    {
        public static Map EMPTY = CreateEmptyMap();

        public TypeColorDetection Colpf0Detection = TypeColorDetection.Always;

        public List<ZoneColorDetection> Colpf0DetectionFlags = new List<ZoneColorDetection>();

        public List<Rectangle> Colpf0DetectionRects = new List<Rectangle>();

        public TypeColorDetection Colpf2Detection = TypeColorDetection.Always;

        public List<ZoneColorDetection> Colpf2DetectionFlags = new List<ZoneColorDetection>();

        public List<Rectangle> Colpf2DetectionRects = new List<Rectangle>();

        public TypeColorDetection Colpf3Detection = TypeColorDetection.Always;

        public List<ZoneColorDetection> Colpf3DetectionFlags = new List<ZoneColorDetection>();

        public List<Rectangle> Colpf3DetectionRects = new List<Rectangle>();

        protected static readonly object dliChangedEventKey = new object();

        protected static readonly object mapChangedEventKey = new object();

        protected EventHandlerList listEventDelegates = new EventHandlerList();

        private static readonly Rectangle mapRectangle = new Rectangle(0, 0, 40, 11);

        // Creates a new data format.
        private static readonly DataFormats.Format myFormat = DataFormats.GetFormat("BLCK-MAP");

        private static int counter = -1;

        private readonly Dictionary<int, int> colorRange = new Dictionary<int, int>();

        private DLI[] dlis;

        private Guid fontID;

        //https://stackoverflow.com/questions/805505/c-sharp-marking-class-property-as-dirty
        private bool isDirty;

        private byte[] mapData;

        private string name;

        private Map()
        {
            Interlocked.Increment(ref counter);

            MapData = new byte[11 * 40];
            Name = "New Map " + counter;
            DLIS = new DLI[] { new DLI(this, new AtariPFColors(), 0) };

            IsDirty = true;
            IsNew = true;
        }

        public event EventHandler DLISChanged
        {
            add
            {
                listEventDelegates.AddHandler(dliChangedEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(dliChangedEventKey, value);
            }
        }

        public event EventHandler MapChanged
        {
            add
            {
                listEventDelegates.AddHandler(mapChangedEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(mapChangedEventKey, value);
            }
        }

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

        public byte BruceStartX1 { get; set; } = 0;
        public byte BruceStartX2 { get; set; } = 0;
        public byte BruceStartY1 { get; set; } = 0;
        public byte BruceStartY2 { get; set; } = 0;

        public DLI[] DLIS
        {
            get { return dlis; }
            set { DLI[] sortedValue = value?.OrderBy(o => o.IntLine).ToArray(); SetField(ref dlis, sortedValue); CreateDLIMapIndex(); }
        }

        public string ExecRoutine { get; set; }
        public string ExecRoutinePath { get; set; }
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
        public bool Foe { get; set; } = false;

        public Guid FontID
        {
            get { return fontID; }
            set { SetField(ref fontID, value); }
        }

        public List<String> Includes { get; set; }
        public string InitRoutine { get; set; }
        public string InitRoutinePath { get; set; }

        public bool IsDirty
        {
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
            private set
            {
                isDirty = value;
            }
        }

        public bool IsNew { get; private set; }

        public byte[] MapData
        {
            get { return mapData; }
            set { SetField(ref mapData, value); }
        }

        public MapSet MapSet { get; private set; }

        public string Name
        {
            get { return name; }
            set { SetField(ref name, value); }
        }

        public byte NinjaEnterCount1 { get; set; } = 0;
        public byte NinjaEnterCount2 { get; set; } = 0;
        public byte NinjaSpawnPosition { get; set; } = 0;
        public string Path { get; set; }
        public string TileCollisionRoutine { get; set; }
        public string TileCollisionRoutinePath { get; set; }
        public Guid UID { get; set; }
        public byte YamoEnterCount1 { get; set; } = 0;

        public byte YamoEnterCount2 { get; set; } = 0;

        public byte YamoSpawnPosition { get; set; } = 0;

        public bool AddDLI(int intLine, AtariPFColors returnAtariPFColors, Object returnOrderValue)
        {
            List<DLI> dlis = new List<DLI>(DLIS);
            foreach (DLI dli in dlis)
            {
                if (dli.IntLine == intLine)
                {
                    MessageBox.Show("There already is a DLI at line " + intLine);
                    return false;
                }
            }
            dlis.Add(new DLI(this, returnAtariPFColors, intLine, returnOrderValue));

            DLIS = dlis.ToArray();

            ((EventHandler)listEventDelegates[dliChangedEventKey])?.Invoke(this, null);
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

            ((EventHandler)listEventDelegates[dliChangedEventKey])?.Invoke(this, null);
            return true;
        }

        public void ClearMapDataBytes(Rectangle toClear)
        {
            MapDataBlock datab = new MapDataBlock(toClear.Size, Enumerable.Repeat((byte)0, toClear.Size.Width * toClear.Size.Height).ToArray());
            UndoManager.RecordAction(new SetMapDataBytesAction(this, toClear.Location, datab));
        }

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

        public bool Equals(Map other) =>
            String.Equals(UID, other.UID);

        public override bool Equals(object obj) =>
                (obj is Map map) && Equals(map);

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

        public MapDataBlock GetMapClipboardData()
        {
            return (MapDataBlock)Clipboard.GetDataObject().GetData(myFormat.Name);
        }

        public void RemoveDLI(DLI dli)
        {
            List<DLI> dlis = new List<DLI>(DLIS);
            dlis.Remove(dli);
            DLIS = dlis.ToArray();
            ((EventHandler)listEventDelegates[dliChangedEventKey])?.Invoke(this, null);
        }

        public void SetLoaded()
        {
            IsDirty = false;
            IsNew = false;
        }

        public void SetMapDataByte(Point point, byte b)
        {
            UndoManager.RecordAction(new SetMapDataBytesAction(this, point, new MapDataBlock(new Size(1, 1), new byte[] { b })));
        }

        public void SetMapDataBytes(Point point, MapDataBlock data)
        {
            UndoManager.RecordAction(new SetMapDataBytesAction(this, point, data));
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

        public bool UseRleCompression()
        {
            return System.IO.Path.GetExtension(Path).Equals(".rle", StringComparison.OrdinalIgnoreCase);
        }

        internal static Map CreateNewMap(MapSet mapset, CharacterSet characterSet, String uid = null)
        {
            if (characterSet == null || mapset == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            Map result = new Map
            {
                FontID = characterSet.UID,
                UID = GuidHelper.parse(uid) ?? Guid.NewGuid(),
                MapSet = mapset
            };

            return result;
        }

        internal bool Contains(Point? point)
        {
            return point.HasValue && mapRectangle.Contains(point.Value);
        }

        internal bool Contains(Rectangle rect)
        {
            return mapRectangle.Contains(rect);
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

        protected void SetField<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                IsDirty = true;
            }
        }

        private static Map CreateEmptyMap()
        {
            Map result = new Map
            {
                Name = ""
            };
            return result;
        }

        private void CreateDLIMapIndex()
        {
            colorRange.Clear();
            int count = 0;

            foreach (DLI dli in DLIS)
            {
                if (dli.IntLine > 0)
                {
                    int changeLine = dli.IntLine * 40 - 1;
                    colorRange.Add(count, changeLine);
                    count = changeLine + 1;
                }
            }
            colorRange.Add(count, 439);
        }

        private void SetChanged()
        {
            IsDirty = true;
            ((EventHandler)listEventDelegates[mapChangedEventKey])?.Invoke(this, null);
        }

        [Serializable]
        public class MapDataBlock
        {
            public MapDataBlock(Size _size, byte[] _bytes)
            {
                Size = _size; Bytes = _bytes;
            }

            public byte[] Bytes { get; }
            public Size Size { get; }
        }

        private class SetMapDataBytesAction : AbstractAction
        {
            private readonly MapDataBlock datab;
            private readonly Map map;
            private readonly Point point;
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

            protected override void UnExecuteCore()
            {
                if (HadAction())
                {
                    CopyData(map, point, datab.Size, oldBytes.ToArray());
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

                            if (oldByte != newByte)
                            {
                                map.mapData[byteIndex] = newByte;
                                updated = true;
                            }
                        }
                    }
                }

                return updated ? result : null;
            }

            private bool HadAction()
            {
                return oldBytes != null;
            }
        }
    }

    public class MapListEntry
    {
        public Map Map { get; set; }

        public override string ToString()
        {
            return this.Map.Name;
        }
    }

    public class MapNameChangedEventArgs : EventArgs
    {
        public MapNameChangedEventArgs(Map Map, String OldName, String NewName)
          : base()
        {
            this.Map = Map;
            this.OldName = OldName;
            this.NewName = NewName;
        }

        public Map Map { get; set; }
        public String NewName { get; private set; }
        public String OldName { get; private set; }
    }

    public class MapTreeNode : TreeNode
    {
        public MapTreeNode(Map map, TreeNode[] array) : base(map.Name, array)
        {
            Map = map;
            Tag = pbx1.TypeNode.Map;
        }

        public Map Map { get; set; }
    }
}