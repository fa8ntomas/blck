using K4os.Compression.LZ4;
using K4os.Compression.LZ4.Streams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static BLEditor.Map;

namespace BLEditor
{
    internal static class MapSerializer
    {
        public static XElement Save(Map map, String MapSetSaveFileName, bool moveToMapSetDirectory = false)
        {
            if (String.IsNullOrWhiteSpace(map.Path))
            {
                map.Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"{map.Name}.lz4");
            }
            else if (moveToMapSetDirectory)
            {
                map.Path = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(map.Path));
            }

      
            XElement result = new XElement("map");
            result.Add(new XAttribute("name", map.Name));
            result.Add(new XAttribute("font", map.FontID));
            result.Add(new XAttribute("path", PathHelper.Delta(MapSetSaveFileName, map.Path)));
            result.Add(new XAttribute("uid", map.UID.ToString()));
          
            // DLI

            XElement dlis = new XElement("dlis");
            if (map.DLIS != null)
            {
                foreach (DLI dli in map.DLIS)
                {
                    dlis.Add(dli.Save());
                }
            }

            result.Add(dlis);

            // Init Routine

            if (!String.IsNullOrWhiteSpace(map.InitRoutine))
            {
                if (String.IsNullOrWhiteSpace(map.InitRoutinePath))
                {
                    map.InitRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Init {map.Name}.asm");
                }
                else if (moveToMapSetDirectory)
                {
                    map.InitRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(map.InitRoutinePath));
                }

                File.WriteAllText(map.InitRoutinePath, map.InitRoutine);
            }

            result.Add(new XElement("InitRoutinePath", PathHelper.Delta(MapSetSaveFileName, map.InitRoutinePath)));

            // Exec Routine

            if (!String.IsNullOrWhiteSpace(map.ExecRoutine))
            {
                if (String.IsNullOrWhiteSpace(map.ExecRoutinePath))
                {
                    map.ExecRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"Exec {map.Name}.asm");
                }
                else if (moveToMapSetDirectory)

                {
                    map.ExecRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(map.ExecRoutinePath));
                }

                File.WriteAllText(map.ExecRoutinePath, map.ExecRoutine);
            }

            result.Add(new XElement("ExecRoutinePath", PathHelper.Delta(MapSetSaveFileName, map.ExecRoutinePath)));

            // TileCollision Routine

            if (!String.IsNullOrWhiteSpace(map.TileCollisionRoutine))
            {
                if (String.IsNullOrWhiteSpace(map.TileCollisionRoutinePath))
                {
                    map.TileCollisionRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), $"TileCollision {map.Name}.asm");
                }
                else if (moveToMapSetDirectory)
                {
                    map.TileCollisionRoutinePath = PathHelper.CreateFileWithUniqueName(System.IO.Path.GetDirectoryName(MapSetSaveFileName), System.IO.Path.GetFileName(map.TileCollisionRoutinePath));
                }

                File.WriteAllText(map.TileCollisionRoutinePath, map.TileCollisionRoutine);
            }

            result.Add(new XElement("TileCollisionRoutinePath", PathHelper.Delta(MapSetSaveFileName, map.TileCollisionRoutinePath)));

            result.Add(ExportColorDetection("Colpf0Dectection", map.Colpf0Detection, map.Colpf0DetectionRects, map.Colpf0DetectionFlags));
            result.Add(ExportColorDetection("Colpf2Dectection", map.Colpf2Detection, map.Colpf2DetectionRects, map.Colpf2DetectionFlags));
            result.Add(ExportColorDetection("Colpf3Dectection", map.Colpf3Detection, map.Colpf3DetectionRects, map.Colpf3DetectionFlags));

            result.Add(new XElement("foe", map.Foe));
            result.Add(new XElement("brucestart", new XElement("x1", map.BruceStartX1), new XElement("x2", map.BruceStartX2), new XElement("y1", map.BruceStartY1), new XElement("y2", map.BruceStartY2)));
            result.Add(new XElement("exit1", new XElement("map", map.Exit1MapID), new XElement("x", map.Exit1X), new XElement("y", map.Exit1Y)));
            result.Add(new XElement("exit2", new XElement("map", map.Exit2MapID), new XElement("x", map.Exit2X), new XElement("y", map.Exit2Y)));
            result.Add(new XElement("exit3", new XElement("map", map.Exit3MapID), new XElement("x", map.Exit3X), new XElement("y", map.Exit3Y)));
            result.Add(new XElement("exit4", new XElement("map", map.Exit4MapID), new XElement("x", map.Exit4X), new XElement("y", map.Exit4Y)));
            result.Add(new XElement("YamoSpawnPosition", map.YamoSpawnPosition));
            result.Add(new XElement("NinjaSpawnPosition", map.NinjaSpawnPosition));
            result.Add(new XElement("NinjaEnterCount1", map.NinjaEnterCount1));
            result.Add(new XElement("NinjaEnterCount2", map.NinjaEnterCount2));
            result.Add(new XElement("YamoEnterCount1", map.YamoEnterCount1));
            result.Add(new XElement("YamoEnterCount2", map.YamoEnterCount2));

            EncodeAndSaveTiles(map);

            return result;
        }

        private static void EncodeAndSaveTiles(Map map)
        {
            if (map.UseRleCompression())
            {
                File.WriteAllBytes(map.Path, EncodeRLE(map.MapData.ToList()).ToArray());
            }
            else
            {
                using (var source = new MemoryStream(map.MapData))
                using (var target = LZ4Stream.Encode(File.Create(map.Path), new LZ4EncoderSettings
                {
                    CompressionLevel = LZ4Level.L12_MAX,
                    ChainBlocks = false
                    // ContentChecksum = true;
                }))
                {
                    source.CopyTo(target);
                }
            }
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

        static private XElement ExportColorDetection(string tagName, TypeColorDetection ColpfDectection, List<Rectangle> ColpfDectectionRect, List<ZoneColorDetection> ColpfDetectionFlags)
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
    }
}