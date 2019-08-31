using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BLEditor
{
    public class DLI
    {
     
        private int _intLine;
        public int IntLine { get { return _intLine; } set { SetField(ref _intLine, value); } }


        private AtariPFColors _atariPFColors;
        public AtariPFColors AtariPFColors { get { return _atariPFColors; } set { SetField(ref _atariPFColors, value); } }

        private Map Map;

        private Object _orderValue;
        public Object OrderValue { get { return _orderValue; } set { SetField(ref _orderValue, value); } }

        public DLI( Map map, AtariPFColors atariPFColors, int intLine, Object orderIndex=null, bool isNew=true)
        {
            AtariPFColors = new AtariPFColors(atariPFColors);
            Map = map;
            IntLine = intLine;
            OrderValue = orderIndex;
            IsNew = isNew;
         }

        
    
    public DLIListEntry ToDLIListEntry()
        {
            DLIListEntry result = new DLIListEntry();
            result.dli = this;
            return result;
        }

        public bool IsNew { get; private set; }
        public bool IsDirty { get; private set; }
        protected void SetField<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                IsDirty = true;
            }
        }

        public XElement Save()
        {
            XElement result = new XElement("dli");
            result.Add(new XAttribute("order", OrderEntry.GetNormalizedValue(OrderValue)));
            result.Add(new XAttribute("row", IntLine));
            result.Add(new XAttribute("colbk", AtariPFColors.Colbk));
            result.Add(new XAttribute("colpf0", AtariPFColors.Colpf0));
            result.Add(new XAttribute("colpf1", AtariPFColors.Colpf1));
            result.Add(new XAttribute("colpf2", AtariPFColors.Colpf2));
            result.Add(new XAttribute("colpf3", AtariPFColors.Colpf3));
            return result;
        }

        public void SetSaved()
        {
            IsDirty = false;
            IsNew = false;
        }

        public static DLI Load(Map map, XElement dliElement)
        {
            byte row = (byte)Int32.Parse(dliElement.Attribute("row").Value);
            byte colbak = (byte)Int32.Parse(dliElement.Attribute("colbk").Value);
            byte colpf0 = (byte)Int32.Parse(dliElement.Attribute("colpf0").Value);
            byte colpf1 = (byte)Int32.Parse(dliElement.Attribute("colpf1").Value);
            byte colpf2 = (byte)Int32.Parse(dliElement.Attribute("colpf2").Value);
            byte colpf3 = (byte)Int32.Parse(dliElement.Attribute("colpf3").Value);
            var OrderValue = OrderEntry.GetNormalizedValue(dliElement.Attribute("order")?.Value);
            var Order = OrderEntry.GetEntries().Find(val=>val.Value== OrderValue.ToString());
            return (new DLI(map,new AtariPFColors(colbak, colpf3, colpf2, colpf1, colpf0), row, Order,false));
        }
    }

    public class DLIListEntry
    {
        public DLI dli { get; set; }

        public override string ToString()
        {
            if (dli.IntLine == 0)
            {
                return "Init";
            }
            else
            {
                return "Line " + dli.IntLine;
            }
        }
    }

    public class DLISelectedEventArgs : EventArgs
    {
        public DLI DLI { get; private set; }

        public DLISelectedEventArgs(DLI DLI)
          : base()
        {
            this.DLI = DLI;
        }
    }

    public class OrderEntry
    {
        

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {

            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        private static IEnumerable<IEnumerable<RbgPFColors.PlayFieldColor>> GetOrfer()
        {

            List<RbgPFColors.PlayFieldColor> colors = new List<RbgPFColors.PlayFieldColor>();

            foreach (RbgPFColors.PlayFieldColor color in Enum.GetValues(typeof(RbgPFColors.PlayFieldColor)))
            {
                if (color == RbgPFColors.PlayFieldColor.COLPF3) continue;

                colors.Add(color);
            }


            return GetPermutations(colors, colors.Count);
        }

        static List<OrderEntry> _entries;
        static public List<OrderEntry> GetEntries()
        {
            if (_entries == null)
            {
                IEnumerable<IEnumerable<RbgPFColors.PlayFieldColor>> order = GetOrfer();

                List<OrderEntry> ord = new List<OrderEntry>();
                foreach (IEnumerable<RbgPFColors.PlayFieldColor> listColor in order)
                {
                    OrderEntry orderEntry = new OrderEntry();
                    orderEntry.Order = listColor;
                    ord.Add(orderEntry);
                }

                _entries= ord;
            }

            return _entries;
        }


        public IEnumerable<RbgPFColors.PlayFieldColor> Order { get; set; }

        public String Value { get { return ToString(); } }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            bool first = true;

            foreach (RbgPFColors.PlayFieldColor color in Order)
            {
                if (!first)
                {
                    result.Append(" / ");
                }
                else
                {
                    first = false;
                }

                result.Append(color.ToString());
            }

            return result.ToString();
        }



        internal static object GetNormalizedValue(object value)
        {
            if (value == null)
            {
                return GetEntries().ElementAt(0).Value;
            }

            var valueF = GetEntries().Find(val => val.Value == value.ToString());

            return valueF == null ? GetEntries().ElementAt(0).Value : valueF.Value;
        }
    }
  
}
