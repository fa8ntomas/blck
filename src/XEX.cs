using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    public class XEXSegment{
        public ushort startAddress;
        public ushort endAddress;
        public byte[] data;

        public XEXSegment(ushort startAddress, ushort endAddress, byte[] data = null)
        {
            this.startAddress = startAddress;
            this.endAddress = endAddress;
            this.data = data;
        }

        public static XEXSegment Load(FileStream fileStream, ushort startAddress, ushort endAddress)
        {
            Debug.WriteLine("Read Segment " + startAddress.ToString("X4") + " "+ endAddress.ToString("X4"));
            XEXSegment result = new XEXSegment(startAddress, endAddress);
            var len = endAddress - startAddress + 1;
            result.data = new byte[len];
            for (int i=0; i<len; i++)
            {
                result.data[i] = (byte)fileStream.ReadByte();
            }
          
            return result;
        }

        internal void Print()
        {
            Debug.WriteLine("Read Segment " + startAddress.ToString("X4") + " " + endAddress.ToString("X4"));
        }
    }

    public class XEX
    {
        List<XEXSegment> Segments = new List<XEXSegment>();

        private static ushort ReadUShort(FileStream fileStream)
        {
            return (ushort)(fileStream.ReadByte() + 256 * fileStream.ReadByte());
        }

        public static XEX Load(String path)
        {
            var fileStream = new FileStream(path, FileMode.Open);
            
            if (ReadUShort(fileStream)!=0xFFFF)
            {
                throw new ArgumentException();
            }

            XEX result = new XEX(); ;

            do {
                ushort startAddress = ReadUShort(fileStream);
                if (startAddress == 0xFFFF)
                {
                    // $FFFF - Indicates a binary load file. Mandatory for first segment, optional for any other segment
                    startAddress = ReadUShort(fileStream);
                }
                ushort endAddress = ReadUShort(fileStream);
                result.Segments.Add(XEXSegment.Load(fileStream, startAddress, endAddress));
            } while (fileStream.Position< fileStream.Length);

            return result;   
        }

        public void AddSegment(ushort startAddress, byte[] data)
        {
            // data.Length = endAddress - startAddress + 1;
            // => endAddress = data.Length + startAddress - 1;
            ushort endAddress = (ushort)(data.Length + startAddress - 1);

            Segments.Add(new XEXSegment(startAddress, endAddress, data));
        }

        public void RemoveSegment(ushort startAddress, ushort endAddress )
        {
            Segments.RemoveAll(x =>  x.startAddress == startAddress && x.endAddress== endAddress);
        }

        public ushort GetMaxAdr()
        {
            return Segments.Max(segment => segment.endAddress);
        }

        public void Print()
        {
            foreach (var Segment in Segments)
            {
                Segment.Print();
            }
        }
    
    }
}
