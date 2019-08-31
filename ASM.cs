using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    class ASM
    {
        const int DataWidth = 4;

        public static void export(String fileName, MapSet mapset, decimal firstmap=0)
        {
            ASM asm = new ASM();

            using (StreamWriter file = new StreamWriter(fileName, false))
            {
                file.WriteLine($"MAPCOUNT equ {mapset.Maps.Length - 1}");
                file.WriteLine($"MAPSTART equ {firstmap}");
                file.WriteLine();

                asm.ExportFonts(file, mapset);
                
                asm.ExportDLIs(file, mapset);
                asm.ExportLamps(file, mapset);
                asm.ExportRLE(file, mapset);
                asm.ExportFOE(file, mapset);
                asm.ExportBruceStart(file, mapset);
                asm.ExportSpawnPositions(file, mapset);
                asm.ExportRoutines(file, mapset);
                asm.ExportExit1(file, mapset);
                asm.ExportExit2(file, mapset);
                asm.ExportExit3(file, mapset);
                asm.ExportExit4(file, mapset);
                asm.ExportCollision(file, mapset);
            }
        }

        private void ExportExit1(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit1X", (int i) => mapset.Maps[i].Exit1X);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit1Y", (int i) => mapset.Maps[i].Exit1Y);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExits1", (int i) => mapset.GetMapIndex(mapset.Maps[i].Exit1MapID));
        }

        private void ExportExit2(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit2X", (int i) => mapset.Maps[i].Exit2X);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit2Y", (int i) => mapset.Maps[i].Exit2Y);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExits2", (int i) => mapset.GetMapIndex(mapset.Maps[i].Exit2MapID));
        }

        private void ExportExit3(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit3X", (int i) => mapset.Maps[i].Exit3X);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit3Y", (int i) => mapset.Maps[i].Exit3Y);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExits3", (int i) => mapset.GetMapIndex(mapset.Maps[i].Exit3MapID));
        }

        private void ExportExit4(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit4X", (int i) => mapset.Maps[i].Exit4X);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExit4Y", (int i) => mapset.Maps[i].Exit4Y);
            ExportMapsBytes(file, mapset.Maps.Length, "MapExits4", (int i) => mapset.GetMapIndex(mapset.Maps[i].Exit4MapID));
        }

        private void ExportFonts(StreamWriter file, MapSet mapset)
        {
            file.WriteLine("\t.align $400,0   ; Useless I hope");

            for (int i = 0; i < mapset.CharSets.Length; i++)
            {
                AddLabel(file, $"Font{i}");
                AddIns(file, mapset.CharSets[i].Path);
            }

            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapFonts", (int i) => { for (int j = 0; j < mapset.CharSets.Length; j++) if (mapset.Maps[i].FontID == mapset.CharSets[j].UID) return $"Font{j}"; return null; });
        }

        private void ExportRoutines(StreamWriter file, MapSet mapset)
        {
            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapInitsLo", (int i) => $"(Map{i}Init-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapInitsHi", (int i) => $"(Map{i}Init-1)");

            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapExecsLo", (int i) => $"(Map{i}Exec-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapExecsHi", (int i) => $"(Map{i}Exec-1)");

            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapTileCollisionLo", (int i) => $"(Map{i}TileCollision-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapTileCollisionHi", (int i) => $"(Map{i}TileCollision-1)");

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                AddLabel(file, $"Map{i}Init");

                file.WriteLine($"\t.local LocalMap{i}Init");
                file.WriteLine($"\t.use LocalMap{i}TileCollision");
                file.WriteLine($"\t.use LocalMap{i}Exec");

                ExportLabels(file, mapset, i);

                if (String.IsNullOrWhiteSpace(mapset.Maps[i].InitRoutinePath))
                {
                    file.WriteLine("\trts");
                } else
                {
                    AddIcl(file, mapset.Maps[i].InitRoutinePath);
                }

                file.WriteLine("\t.endl");

                AddLabel(file, $"Map{i}Exec");

                file.WriteLine($"\t.local LocalMap{i}Exec");
                file.WriteLine($"\t.use LocalMap{i}TileCollision");
                file.WriteLine($"\t.use LocalMap{i}Init");

                ExportLabels(file, mapset, i);

                if (String.IsNullOrWhiteSpace(mapset.Maps[i].ExecRoutinePath))
                {
                    file.WriteLine("\trts");
                }
                else
                {
                    AddIcl(file, mapset.Maps[i].ExecRoutinePath);
                }

                file.WriteLine("\t.endl");

                AddLabel(file, $"Map{i}TileCollision");

                file.WriteLine($"\t.local LocalMap{i}TileCollision");
                file.WriteLine($"\t.use LocalMap{i}Exec");
                file.WriteLine($"\t.use LocalMap{i}Init");

                ExportLabels(file, mapset, i);

                if (String.IsNullOrWhiteSpace(mapset.Maps[i].TileCollisionRoutinePath))
                {
                    file.WriteLine("\trts");
                }
                else
                {
                    AddIcl(file, mapset.Maps[i].TileCollisionRoutinePath);
                }

                file.WriteLine("\t.endl");
            }
        }

        private void ExportLabels(StreamWriter file, MapSet mapset, int i)
        {
            file.WriteLine($"\tCurrentFontAdr = Font{i}");
            DLI[] dlis = mapset.Maps[i].DLIS;
            for (int j = 0; j < dlis.Length; j++)
            {
                file.WriteLine($"\tCurrentDli{j}Adr = Map{i}Dli{j}");
            }
            file.WriteLine($"\tCurrentPlayerLampsCounts=PlayerMap{i}LampsCounts");
            file.WriteLine($"\tCurrentPlayerLamps=Map{i}Lamps");
        }

        private void ExportFOE(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapFoeFlags", (int i) => (byte)(mapset.Maps[i].Foe ? 0x00 : 0xFF));
        }

        private void ExportBruceStart(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapBruceStartX1", (int i) => mapset.Maps[i].BruceStartX1);
            ExportMapsBytes(file, mapset.Maps.Length, "MapBruceStartY1", (int i) => mapset.Maps[i].BruceStartY1);
            ExportMapsBytes(file, mapset.Maps.Length, "MapBruceStartX2", (int i) => mapset.Maps[i].BruceStartX2);
            ExportMapsBytes(file, mapset.Maps.Length, "MapBruceStartY2", (int i) => mapset.Maps[i].BruceStartY2);
        }

        private void ExportRLE(StreamWriter file, MapSet mapset)
        {
            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                AddLabel(file, $"Map{i}Rle");
                AddIns(file, mapset.Maps[i].Path);
            }

            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapDataLo", (int i) => $"Map{i}Rle");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapDataHi", (int i) => $"Map{i}Rle");
        }

        private void ExportDLIs(StreamWriter file, MapSet mapset)
        {
            byte[] dlilow = new byte[mapset.Maps.Length];
            byte[] dlihigh = new byte[mapset.Maps.Length];

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                foreach (var dli in mapset.Maps[i].DLIS)
                {
                    if (dli.IntLine > 0 && dli.IntLine < 9)
                    {
                        dlilow[i] = (byte)(dlilow[i] + (1 << (dli.IntLine - 1)));
                    }
                    else if (dli.IntLine >= 9)
                    {
                        dlihigh[i] = (byte)(dlihigh[i] + (1 << (dli.IntLine - 9)));
                    }
                }
            }

            ExportMapsBytes(file, mapset.Maps.Length, "MapDLILinesLo", (int i) => dlilow[i]);
            ExportMapsBytes(file, mapset.Maps.Length, "MapDLILinesHi", (int i) => dlihigh[i]);
            /*
            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                AddLabel(file, $"MapColor{i}");
                

                foreach (var dli in mapset.Maps[i].DLIS)
                {
                    AtariPFColors atariPFColors = dli.AtariPFColors;
                    // COLBK COLPF3 COLPF2 COLPF1 COLPF0
                    AddBytes(file, atariPFColors.Colbk, atariPFColors.Colpf3, atariPFColors.Colpf2, atariPFColors.Colpf1, atariPFColors.Colpf0);
                }
            }

            ExportMapsTable(file, mapset.Maps.Length, "MapColors", (int i) => $"MapColor{i}");
            */
            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapDliLo", (int i) => $"Map{i}Dli");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapDliHi", (int i) => $"Map{i}Dli");

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                DLI[] dlis = mapset.Maps[i].DLIS;

                AddLabel(file, $"Map{i}Dli");

                file.WriteLine($"\tpha");
                file.WriteLine($"\tlda MapFont");
                file.WriteLine($"\tsta WSYNC");
                file.WriteLine($"\tsta CHBASE");

                String firstDliLabel = $"Map{i}Dli0";

                file.WriteLine($"\tlda #<{firstDliLabel}");
                file.WriteLine($"\tsta VDSLST");
                file.WriteLine($"\tlda #>{firstDliLabel}");
                file.WriteLine($"\tsta VDSLST+1");
                file.WriteLine($"\tpla");
                file.WriteLine($"\trti");

                for (int j=0; j<dlis.Length; j++)
                {
                    ExportColorsDeltaDLI(file, i, j, dlis[j], (j > 0)?dlis[j - 1]: null, j== dlis.Length-1);
                }
            }
        }

        private void ExportColorsDeltaDLI(StreamWriter file, int mapIndex, int dliIndex, DLI dli, DLI previousDli, bool lastDLI)
        {
            Dictionary<RbgPFColors.PlayFieldColor, byte> colorsDelta = dli.AtariPFColors.Delta(previousDli?.AtariPFColors);
            colorsDelta.Remove(RbgPFColors.PlayFieldColor.COLPF3);  // Always Black;

            colorsDelta = sortColors(colorsDelta, dli.OrderValue);

            String currentLabel = $"Map{mapIndex}Dli{dliIndex}";

            AddLabel(file, currentLabel);

            file.WriteLine($"\tpha");

            switch (colorsDelta.Count)
            {
                case 1:
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(0)}");
                    file.WriteLine($"\tsta WSYNC");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(0)}");
                    break;

                case 2:
                    file.WriteLine($"\tstx {currentLabel}X");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(0)}");
                    file.WriteLine($"\tldx #{colorsDelta.Values.ElementAt(1)}");
                    file.WriteLine($"\tsta WSYNC");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(0)}");
                    file.WriteLine($"\tstx {colorsDelta.Keys.ElementAt(1)}");
                    file.WriteLine($"\t{currentLabel}X equ *+1");
                    file.WriteLine($"\tldx #$FF");
                    break;

                case 3:
                    file.WriteLine($"\tstx {currentLabel}X");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(0)}");
                    file.WriteLine($"\tldx #{colorsDelta.Values.ElementAt(1)}");
                    file.WriteLine($"\tsta WSYNC");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(0)}");
                    file.WriteLine($"\tstx {colorsDelta.Keys.ElementAt(1)}");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(2)}");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(2)}");
                    file.WriteLine($"\t{currentLabel}X equ *+1");
                    file.WriteLine($"\tldx #$FF");
                    break;

                case 4:
                    file.WriteLine($"\tstx {currentLabel}X");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(0)}");
                    file.WriteLine($"\tldx #{colorsDelta.Values.ElementAt(1)}");
                    file.WriteLine($"\tsta WSYNC");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(0)}");
                    file.WriteLine($"\tstx {colorsDelta.Keys.ElementAt(1)}");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(2)}");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(2)}");
                    file.WriteLine($"\tlda #{colorsDelta.Values.ElementAt(3)}");
                    file.WriteLine($"\tsta {colorsDelta.Keys.ElementAt(3)}");
                    file.WriteLine($"\t{currentLabel}X equ *+1");
                    file.WriteLine($"\tldx #$FF");
                    break;
            }

            String nextLabel = lastDLI?"LastDLI":$"Map{mapIndex}Dli{dliIndex+1}";
                
            
            file.WriteLine($"\tlda #<{nextLabel}");
            file.WriteLine($"\tsta VDSLST");
            file.WriteLine($"\tlda #>{nextLabel}");
            file.WriteLine($"\tsta VDSLST+1");
            file.WriteLine($"\tpla");
            file.WriteLine($"\trti");
        }

        private Dictionary<RbgPFColors.PlayFieldColor, byte> sortColors(Dictionary<RbgPFColors.PlayFieldColor, byte> colorsDelta, object orderValue)
        {
            Dictionary<RbgPFColors.PlayFieldColor, byte> result = new Dictionary<RbgPFColors.PlayFieldColor, byte>();

            var OrderValue = OrderEntry.GetNormalizedValue(orderValue);

            OrderEntry Order = OrderEntry.GetEntries().Find(val => val.Value == OrderValue.ToString());

            foreach (var color in Order.Order)
            {
                if (colorsDelta.ContainsKey(color))
                {
                    result.Add(color, colorsDelta[color]);
                }
            }

            return result;
        }

        private void ExportSpawnPositions(StreamWriter file, MapSet mapset)
        {
            ExportMapsBytes(file, mapset.Maps.Length, "MapL0D1C", (int i) => { switch (mapset.Maps[i].NinjaSpawnPosition) { case 1: return 0x80; case 2: return 2; default: return 0; } });
            ExportMapsBytes(file, mapset.Maps.Length, "MapL0D58", (int i) => { switch (mapset.Maps[i].YamoSpawnPosition) { case 1: return 0x80; case 2: return 2; default: return 0; } });   
        }

        private void ExportCollision(StreamWriter file, MapSet mapset)
        {
            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapColpf0CollisionsLo", (int i) => $"(Map{i}Colpf0Collision-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapColpf0CollisionsHi", (int i) => $"(Map{i}Colpf0Collision-1)");

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                Map map = mapset.Maps[i];
                AddLabel(file, $"Map{i}Colpf0Collision");
                ExportColorCollision(file, i,map.Colpf0Detection, map.Colpf0DetectionRects,map.Colpf0DetectionFlags);
            }

            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapColpf2CollisionsLo", (int i) => $"(Map{i}Colpf2Collision-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapColpf2CollisionsHi", (int i) => $"(Map{i}Colpf2Collision-1)");

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                Map map = mapset.Maps[i];
                AddLabel(file, $"Map{i}Colpf2Collision");
                ExportColorCollision(file, i, map.Colpf2Detection, map.Colpf2DetectionRects, map.Colpf2DetectionFlags);
            }

            ExportMapsHighLowComponents(file, mapset.Maps.Length, false, "MapColpf3CollisionsLo", (int i) => $"(Map{i}Colpf3Collision-1)");
            ExportMapsHighLowComponents(file, mapset.Maps.Length, true, "MapColpf3CollisionsHi", (int i) => $"(Map{i}Colpf3Collision-1)");

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                Map map = mapset.Maps[i];
                AddLabel(file, $"Map{i}Colpf3Collision");
                ExportColorCollision(file, i,map.Colpf3Detection, map.Colpf3DetectionRects, map.Colpf3DetectionFlags);
            }
        }

        private static void ExportColorCollision(StreamWriter file, int mapIndex, Map.TypeColorDetection ColorDetection, List<Rectangle> DectectionRect, List<Map.ZoneColorDetection> colpf2DetectionFlags)
        {
            switch (ColorDetection)
            {
                case Map.TypeColorDetection.Always:
                    file.WriteLine($"\tclc");
                    file.WriteLine($"\trts");
                    break;
                case Map.TypeColorDetection.Outside:
                    file.WriteLine($"\tjsr @+1");
                    file.WriteLine($"\tbcs @+");
                    file.WriteLine($"\tsec");
                    file.WriteLine($"\trts");
                    file.WriteLine($"@\tclc");
                    file.WriteLine($"\trts");
                    file.WriteLine($"@; test inside");

                    for (int i = 0; i < DectectionRect.Count; i++)
                    {
                        RectangleF rect = DectectionRect[i];
                        Map.ZoneColorDetection flag = colpf2DetectionFlags[i];
                        ExportRect(file, mapIndex, rect, flag);
                    }

                    file.WriteLine($"\trts");
                    break;

                case Map.TypeColorDetection.Inside:
                    for (int i = 0; i < DectectionRect.Count; i++){
                        RectangleF rect = DectectionRect[i];
                        Map.ZoneColorDetection flag = colpf2DetectionFlags[i];
                        ExportRect(file, mapIndex, rect, flag);
                    }
                    file.WriteLine($"\trts");
                    break;

                default:
                    file.WriteLine($"\tsec");
                    file.WriteLine($"\trts");
                    break;
            }
        }

        private static void ExportRect(StreamWriter file, int mapIndex, RectangleF rect, Map.ZoneColorDetection flag)
        {
            switch (flag)
            {

                case Map.ZoneColorDetection.Flag0:
                    file.WriteLine($"\tlda PlayerMap{mapIndex}LampsCounts");
                    file.WriteLine($"\tand #CONTEXTFLAG0MASK");
                    file.WriteLine($"\tbeq @+");
                    break;
                case Map.ZoneColorDetection.Flag1:
                    file.WriteLine($"\tlda PlayerMap{mapIndex}LampsCounts");
                    file.WriteLine($"\tand #CONTEXTFLAG1MASK");
                    file.WriteLine($"\tbeq @+");

                    break;
            }
            // http://retro.hansotten.nl/lee-davison-web-site/some-veryshort-code-bits/
            byte xmax = (byte)(Math.Floor(rect.Right) + 48);
            byte xmin = (byte)(Math.Ceiling(rect.Left) + 48);
            byte ymax = (byte)((Math.Floor(rect.Bottom) + 13 ) * 2);
            byte ymin = (byte)((Math.Ceiling(rect.Top) + 13 ) * 2);
            file.WriteLine($"\ttxa");
            file.WriteLine($"\tclc");
            file.WriteLine($"\tadc #{255 - xmax}");
            file.WriteLine($"\tadc #{xmax - xmin + 1}");
            file.WriteLine($"\tbcc @+");
            file.WriteLine($"\t;{xmin} < X < {xmax}");
            file.WriteLine($"\ttya");
            file.WriteLine($"\tclc");
            file.WriteLine($"\tadc #{255 - ymax}");
            file.WriteLine($"\tadc #{ymax - ymin + 1}");
            file.WriteLine($"\tbcc @+");
            file.WriteLine($"\t;{ymin} < Y < {ymax}");
            file.WriteLine($"\trts;  Hit : Carry Set");
            file.WriteLine($"@; Carry Clear");
        }

        private void ExportLamps(StreamWriter file, MapSet mapset)
        {
            int[] LampCounts = new int[mapset.Maps.Length];

            for (int i = 0; i < mapset.Maps.Length; i++)
            {
                Map map = mapset.Maps[i];

                AddLabel(file, $"Map{i}Lamps");

                byte[] mapData = map.MapData;
                LampCounts[i] = 0;
                for (int j = 0; j < mapData.Length; j++)
                {
                    if (IsLamp(mapData[j]))
                    {
                        AddBytes(file, 1, LampOff(mapData[j]), j % 40, j / 40 + 2);

                        LampCounts[i]++;
                    }
                }

                /*if (LampCounts[i] == 0)
                {
                    AddBytes(file, 1, 0, 0, 0);
                }*/

                AddBytes(file, 0xff);
            }

            ExportMapsBytes(file, mapset.Maps.Length, "MapLampsCount", (int i) => (byte)LampCounts[i]);
            ExportMapsTable(file, mapset.Maps.Length, "MapLamps", (int i) => $"Map{i}Lamps");
        }

        private bool IsLamp(byte lampChar)
        {
            return LampOff(lampChar)!=0;
        }

        private byte LampOff(byte lampChar)
        {
            switch (lampChar)
            {
                case 0x93:
                    return 0x9E;
                case 0X94:
                    return 0X9F;
                case 0xB1:
                    return 0X92;
                default:
                    return 0;
            }
        }

        private void AddHighLowComponents(StreamWriter file, bool high, string[] Labels)
        {
            file.Write("\t.byte ");
            bool firstByte = true;
            for (int i = 0; i < Labels.Length; i++)
            {
                if (!firstByte)
                {
                    file.Write(",");
                }
                firstByte = false;
                file.Write($"{(high ? '>' : '<')}{Labels[i]}");
            }
            file.WriteLine();
        }

        private void AddIns(StreamWriter file, string path)
        {
            file.WriteLine($"\tins '{path}'");
        }

        private void AddIcl(StreamWriter file, string path)
        {
            file.WriteLine($"\ticl '{path}'");
        }

        private void AddLabel(StreamWriter file, String label)
        {
            file.WriteLine($"{label}:");
        }

        private void AddWordTable(StreamWriter file, params string[] Labels)
        {
            file.Write("\t.word ");
            bool firstByte = true;
            for (int i = 0; i < Labels.Length; i++)
            {
                if (!firstByte)
                {
                    file.Write(",");
                }
                firstByte = false;
                file.Write($"a({Labels[i]})");

            }
            file.WriteLine();
        }

        private void AddBytes(StreamWriter file, params int[] Bytes)
        {
            file.Write("\t.byte ");
            bool firstByte = true;
            for (int i = 0; i < Bytes.Length; i++)
            {
                if (!firstByte)
                {
                    file.Write(",");
                }
                firstByte = false;
                file.Write($"${Bytes[i]:X}");

            }
            file.WriteLine();
        }

        private void ExportMapsHighLowComponents(StreamWriter file, int length, bool high, String label, Func<int, String> myFunc)
        {
            AddLabel(file, label);
            for (int i = 0; i < length; i += DataWidth)
            {
                String[] Labels = new String[Math.Min(i + DataWidth, length) - i];

                for (int j = 0; j < Labels.Length; j++)
                {
                    Labels[j] = myFunc(i + j);
                }

                AddHighLowComponents(file, high, Labels);
            }
        }

        private void ExportMapsTable(StreamWriter file, int length, String label, Func<int, String> myFunc)
        {
            AddLabel(file, label);
            for (int i = 0; i < length; i += DataWidth)
            {
                String[] Labels = new String[Math.Min(i + DataWidth, length) - i];

                for (int j = 0; j < Labels.Length; j++)
                {
                    Labels[j] = myFunc(i + j); 
                }

                AddWordTable(file, Labels);
            }
        }

        private void ExportMapsBytes(StreamWriter file, int length, String label, Func<int, byte> myFunc)
        {
            AddLabel(file, label);
            for (int i = 0; i < length; i += DataWidth)
            {
                int[] Bytes = new int[Math.Min(i + DataWidth, length) - i];

                for (int j = 0; j < Bytes.Length; j++)
                {
                    Bytes[j] = myFunc(i + j);
                }

                AddBytes(file, Bytes);
            }
        }
    }
}
