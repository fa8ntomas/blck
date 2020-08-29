using BLEditor.Helpers;
using K4os.Compression.LZ4.Streams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BLEditor.Map;

namespace BLEditor
{
    static class MapDeserializer
    {
        public static Map Load(MapSet mapSet, String path, String mapFileName, CharacterSet characterSet)
        {
            Map map = Map.CreateNewMap(mapSet, characterSet);
            map.Path = mapFileName;

            if (!File.Exists(map.Path))
            {
                map.Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), map.Path);
            }

            map.MapData = (map.UseRleCompression()) ? DecodeRLE(File.ReadAllBytes(map.Path)) : DecodeLZ4(map.Path);

            map.SetLoaded();
            return map;
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


            map.MapData = (map.UseRleCompression()) ? DecodeRLE(File.ReadAllBytes(map.Path)) : DecodeLZ4(map.Path);


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
            ImportTextFile(mapElemept, "ExecRoutinePath", mapSet.Path, (Path, Content) => { map.ExecRoutinePath = Path; map.ExecRoutine = Content; });
            ImportTextFile(mapElemept, "TileCollisionRoutinePath", mapSet.Path, (Path, Content) => { map.TileCollisionRoutinePath = Path; map.TileCollisionRoutine = Content; });
            ImportBoolean(mapElemept, "foe", value => map.Foe = value);
            ImportByte(mapElemept, "YamoSpawnPosition", value => map.YamoSpawnPosition = value);
            ImportByte(mapElemept, "NinjaSpawnPosition", value => map.NinjaSpawnPosition = value);
            ImportByte(mapElemept, "NinjaEnterCount1", value => map.NinjaEnterCount1 = value);
            ImportByte(mapElemept, "NinjaEnterCount2", value => map.NinjaEnterCount2 = value);
            ImportByte(mapElemept, "YamoEnterCount1", value => map.YamoEnterCount1 = value);
            ImportByte(mapElemept, "YamoEnterCount2", value => map.YamoEnterCount2 = value);
            ImportColorDetection(mapElemept, "Colpf0Dectection", ref map.Colpf0Detection, ref map.Colpf0DetectionRects, ref map.Colpf0DetectionFlags);
            ImportColorDetection(mapElemept, "Colpf2Dectection", ref map.Colpf2Detection, ref map.Colpf2DetectionRects, ref map.Colpf2DetectionFlags);
            ImportColorDetection(mapElemept, "Colpf3Dectection", ref map.Colpf3Detection, ref map.Colpf3DetectionRects, ref map.Colpf3DetectionFlags);

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


            map.SetLoaded();

            return map;
        }


        private static void ImportTextFile(XElement mapElemept, string tagName, string mapSetPath, Action<string, string> setValue)
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

        private static void ImportByte(XElement mapElemept, string tagName, Action<byte> setValue, uint defaultValue = 256)
        {
            if (mapElemept.Elements(tagName).Any())
            {
                setValue(Convert.ToByte(mapElemept.Element(tagName).Value.Trim()));
            }
            else if (defaultValue < 256)
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
                            for (int i = 0; i < ColpfDectectionRect.Count; i++)
                            {
                                ColpfDetectionFlags.Add(ZoneColorDetection.Always);
                            }
                        }
                    }
                }
            }
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


        private static byte[] DecodeLZ4(string path)
        {
            using (LZ4DecoderStream source = LZ4Stream.Decode(File.OpenRead(path)))
            using (MemoryStream target = new MemoryStream(440))
            {
                source.CopyTo(target);

                return target.ToArray();
            }
        }


    }
}
