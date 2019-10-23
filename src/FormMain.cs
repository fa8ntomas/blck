using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.CheckedListBox;

namespace BLEditor
{
    public partial class pbx1 : Form
    {
        Bitmap[] tileArray;
        byte[] chars;
        bool mouseD = false;
        MapSet mapSet = new MapSet();

        List<byte> extraMapBytes = new List<byte>();

        public pbx1()
        {
            InitializeComponent();

            mapSet.Changed += mapSet_Changed;
            mapSet.OnDLISChanged +=  mapSet_OnDLISChanged;
            mapSet.MapNameChanged += (s, e) => { mapSet_MapNameChanged((MapNameChangedEventArgs)e); };

            for (int i = 0; i < 440; i++)//Populate Map Tiles
            {
                Tile tile = new Tile(i);
                tile.MouseDown += Lm_MouseDown;
                tile.MouseUp += Lm_MouseUp;
                tile.MouseEnter += Lm_MouseEnter;
                tile.MouseMove += Lm_MouseMove;
                tile.MouseClick += Lm_MouseClick;
                flpMap.Controls.Add(tile);
            }

            newMenu.Click += (s, e) => { newMapSet(); };
            loadMenu.Click += (s, e) => { load(); };
            saveMenu.Click += (s, e) => { MapSet.SSave(mapSet); };
            saveAsMenu.Click += (s, e) => { MapSet.SSaveAs(mapSet); };
            addANewMapFromAnImageMenu.Click += (s, e) => { addANewMapFromAnImage(); };
            addANewMapMenu.Click += (s, e) => { addANewMap(); };
            addAnExistingMapMenu.Click += (s, e) => { addAExistingMap(); };
            addIncludeMenu.Click += (s, e) => { AddInclude(); };
            settingsMenu.Click += (s, e) => { Setting(); };
            runMenu.Click += (s, e) => { Run(); };
            buildReleaseMenu.Click += (s, e) => { BuildRelease(); };

            runButton.Click += (s, e) => { Run(); };

            treeViewMaps.MouseDown += (sender, args) => treeViewMaps_MouseDown(args);

            renameMenu.Click += (s, e) => { RenameMap(s); };
            deleteMenu.Click += (s, e) => { DeleteMap(s); };
            importFromBitmapMenu.Click += (s, e) => { ImportMap(s); };

            importBitmapIntoFontMenu.Click += (s, e) => { importBitmapIntoCurrentFont(); };
            editFontMenu.Click += (s, e) => { editCurrentFont(); };
            copyFromFontMenu.Click += (s, e) => { CopyCurrentFont(); };
            CopyCharMenu.Click += (s, e) => { CopyChar(); };
        }

      
        private void AddInclude()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Assembly|*.asm";
            openFileDialog.Title = "Add include";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                mapSet.AddInclude(openFileDialog.FileName);
            }
        }
        private void treeViewMaps_MouseDown(MouseEventArgs e)
        {
            // Make sure this is the right button.
            if (e.Button != MouseButtons.Right) return;

            // Select this node.
            TreeNode node_here = treeViewMaps.GetNodeAt(e.X, e.Y);
            treeViewMaps.SelectedNode = node_here;

            // See if we got a node.
            if (node_here == null) return;

            contextMenuMap.Tag = GetMap(node_here);
            contextMenuMap.Show(treeViewMaps, new Point(e.X, e.Y));
        }

      
        private Map GetMap(TreeNode Node)
        {
            if (Node != null && Node.Parent == null)
            {
                return ((MapTreeNode)Node).Map;
            }
            else if (Node != null && Node.Parent != null)
            {
                return ((MapTreeNode)Node.Parent).Map;
            }

            return null;
        }

        private void CopyCurrentFont()
        {
            if (CurrentMap() != null)
            {
                CharacterSet characterSet = mapSet.CharSets.First(set => set.UID == CurrentMap().FontID);
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Font|*.fnt";
                openFileDialog.Title = "Load font to import";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    CharacterSet characterSetFrom = mapSet.CharSets.FirstOrDefault(set => set.Path == openFileDialog.FileName);
                    if (characterSetFrom == null)
                    {
                        characterSetFrom = CharacterSet.CreateFromFileName(openFileDialog.FileName);
                     }

                    FormFntToFnt formFntToFnt = new FormFntToFnt(characterSet, CurrentMap().DLIS, characterSetFrom);

                    if (formFntToFnt.ShowDialog() == DialogResult.OK)
                    {
                      characterSet.Data = formFntToFnt.ReturnFontData;
                          PopulateMap(CurrentMap());
                    }
                  
                }
               
            }
        }
        private void CopyChar()
        {
            if (CurrentMap() != null)
            {
                CharacterSet characterSet = mapSet.CharSets.First(set => set.UID == CurrentMap().FontID);
                OpenFileDialog openFileDialog = new OpenFileDialog();

             
                    FormFntToFnt formFntToFnt = new FormFntToFnt(characterSet, CurrentMap().DLIS, characterSet);

                    if (formFntToFnt.ShowDialog() == DialogResult.OK)
                    {
                        characterSet.Data = formFntToFnt.ReturnFontData;
                        PopulateMap(CurrentMap());
                    }

               

            }
        }
        private void mapSet_OnDLISChanged(object sender, EventArgs e)
        {
            Debug.Write("mapSet_OnDLISChanged");
            if (CurrentMap() != null)
            {
                PopulateMap(CurrentMap());
            }
        }

        private void mapSet_Changed(object sender, EventArgs e)
        {
            flpTiles.Controls.Clear();

            for (int i = 0; i < 440; i++)
            {
                (flpMap.Controls[i] as Tile).Text = null;
                (flpMap.Controls[i] as Tile).Image = null;
            }

            dliList.Map = null;

    
            treeViewMaps.Nodes.Clear();

            TreeNode gameDataNode = new TreeNode("Game Data");
            gameDataNode.Tag = TypeNode.GameData;
            treeViewMaps.Nodes.Add(gameDataNode);
     
            if (mapSet.Includes.Count > 0)
            {
                TreeNode gameIncludeNode = new TreeNode("Includes");
                gameIncludeNode.Tag = TypeNode.GameData;
                treeViewMaps.Nodes.Add(gameIncludeNode);

                foreach (String include in mapSet.Includes)
                {
                    TreeNode gameIncludeFileNode = new TreeNode(include);
                    gameIncludeFileNode.Tag = TypeNode.IncludeFile;
                    gameIncludeNode.Nodes.Add(gameIncludeFileNode);
                }
            }

            foreach (Map map in mapSet.Maps)
            {
                TreeNode dataNode = new TreeNode("Map Data");
                dataNode.Tag = TypeNode.MapData;

                TreeNode initNode = new TreeNode("Init routine");
                initNode.Tag = TypeNode.MapInit;

                TreeNode execNode = new TreeNode("Exec routine");
                execNode.Tag = TypeNode.MapExec;

                TreeNode tcollisionNode = new TreeNode("Tite Collision routine");
                tcollisionNode.Tag = TypeNode.MapTileCollision;
             
                TreeNode ccolpf0 = new TreeNode("Colpf0 Collision");
                ccolpf0.Tag = TypeNode.CColpf0;

                TreeNode ccolpf2 = new TreeNode("Colpf2 Collision");
                ccolpf2.Tag = TypeNode.CColpf2;

                TreeNode ccolpf3 = new TreeNode("Colpf3 Collision");
                ccolpf3.Tag = TypeNode.CColpf3;

                TreeNode[] array = new TreeNode[] { initNode, execNode, tcollisionNode, dataNode, ccolpf0, ccolpf2, ccolpf3 };
             
                MapTreeNode treeNode = new MapTreeNode(map, array);
                treeNode.Tag= TypeNode.Map;
                treeViewMaps.Nodes.Add(treeNode);
            }
        }

        public enum TypeNode { GameData, Includes, IncludeFile, Map, MapInit, MapExec, MapTileCollision, MapData, CColpf0, CColpf2, CColpf3 };

        private void mapSet_MapNameChanged(MapNameChangedEventArgs e)
        {
          
            var result = treeViewMaps.Nodes.OfType<MapTreeNode>()
                            .FirstOrDefault(node => node.Map == e.Map);

            result.Text = e.NewName;
      }
  
        private void Lm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSelected.Text))
            {
                (sender as Tile).Text = lblSelected.Text;
                Map currentMap = CurrentMap();
                int selectedNumber = int.Parse(lblSelected.Text, System.Globalization.NumberStyles.HexNumber);
                if (currentMap!=null)
                {
                    int tileNum = Convert.ToInt32((sender as Tile).Name);
                    RbgPFColors currentColors = currentMap.GetColors(tileNum);
                    currentMap.MapData[tileNum] = (byte)selectedNumber;
                    Tile newTile = new Tile(selectedNumber, (flpTiles.Controls[selectedNumber] as Tile).tbytes, currentColors);
                    (sender as Tile).Image = newTile.CreateBitmap(4, 16, 32);
                }
                else
                {
                    (sender as Tile).Image = (flpTiles.Controls[selectedNumber] as Tile).Image;
                }
            }
        }

        private void Lm_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSelected.Text) && mouseD)
            {
                (sender as Tile).Text = lblSelected.Text;
                int selectedNumber = int.Parse(lblSelected.Text, System.Globalization.NumberStyles.HexNumber);
                Map currentMap = CurrentMap();
                if (currentMap != null)
                {
                    int tileNum = Convert.ToInt32((sender as Tile).Name);
                   RbgPFColors currentColors = currentMap.GetColors(tileNum);
                    currentMap.MapData[tileNum] = (byte)selectedNumber;
                    Tile newTile = new Tile(selectedNumber, (flpTiles.Controls[selectedNumber] as Tile).tbytes, currentColors);
                    (sender as Tile).Image = newTile.CreateBitmap(4, 16, 32);
                }
                else
                {
                    (sender as Tile).Image = (flpTiles.Controls[selectedNumber] as Tile).Image;
                }
            }
        }

        private void Lm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseD = false;
        }

        private void Lm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseD = true;
        }

        private void Lm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
            {
                Control control = (Control)sender;

                if (control.Capture)
                    control.Capture = false;
            }
        }

        private void L_Click(object sender, System.EventArgs e)
        {
            Bitmap newSize = (sender as Tile).CreateBitmap(16, 64, 128);
            lblSelected.Image = newSize;
            lblSelected.Text = (sender as Tile).Text;
        }

        private void CreateTiles(byte[] byteSet)//Map Tiles
        {
            int count = 0;
            tileArray = new Bitmap[256];

            for (int i = 0; i < 256; i++)
            {
                chars = new byte[8];
                for (int j = 0; j < 8; j++)
                {
                    chars[j] = byteSet[count];
                    count++;
                }
                Tile tile;
                if (CurrentMap() != null && CurrentMap().DLIS.Length == 1)
                {
                    tile = new Tile(i, chars, CurrentMap().DLIS[0].AtariPFColors.ToBLColor());
                }
                else
                {
                    tile = new Tile(i, chars, new RbgPFColors());
                }
                tileArray[i] = tile.TMap;
                flpTiles.Controls.Add(tile);
                tile.Click += L_Click;
                if (count >= 1024)
                {
                    count = 0;
                }
            }
        }

        private void PopulateLabels()
        {
            int count = 0;
            foreach (Tile tile in flpTiles.Controls)
            {
                tile.Image = tileArray[count];
                count++;
            }
            count = 0;
        }

        private string GetProperName(string path)
        {
            string[] pathASplit = path.Split('\\');
            return pathASplit.Last();
        }

        private void PopulateMap(Map inMap)
        {
            dliList.Map = inMap;

            byte[] bytes = mapSet.CharSets.First(set => set.UID == inMap.FontID).Data;
            flpTiles.Controls.Clear();
            CreateTiles(bytes);
            PopulateLabels();
            buttonEditFnt.Enabled = true;

            for (int i = 0; i < Math.Min(inMap.MapData.Length, 440); i++)
            {
                Tile tile = flpMap.Controls[i] as Tile;

                if (tile.Tag == null)
                {
                    tile.Tag = new ToolTip();
                }

                (tile.Tag as ToolTip).SetToolTip(tile, inMap.MapData[i].ToString("X2")+ " ["+(i%40).ToString("X2")+"," + (i / 40+2).ToString("X2") + "]");

                tile.Text = inMap.MapData[i].ToString("X2");
                tile.Image = LookUpDLIColor(flpTiles.Controls[inMap.MapData[i]] as Tile, i, inMap);
            }
            extraMapBytes.Clear();
            for (int i = 440; i < inMap.MapData.Length; i++)
            {
                extraMapBytes.Add(inMap.MapData[i]);
            }

        }

        private Image LookUpDLIColor(Tile changeTile, int i, Map inMap)
        {
            changeTile.ColorArray = inMap.GetColors(i);
            return changeTile.CreateBitmap(4, 16, 32);
        }

        private void editCurrentFont()
        {
            if (CurrentMap() != null)
            {
                CharacterSet characterSet = mapSet.CharSets.First(set => set.UID == CurrentMap().FontID);

                FormFntEdit fontEditForm = new FormFntEdit(characterSet, CurrentMap().DLIS);

                if (fontEditForm.ShowDialog() == DialogResult.OK)
                {
                    characterSet.Data = fontEditForm.ReturnFontData;
                    PopulateMap(CurrentMap());
                }
            }
        }
       
        private Map CurrentMap()
        {
            return GetMap(treeViewMaps.SelectedNode);
        }

        private void addANewMapFromAnImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img;
                using (var bmpTemp = new Bitmap(openFileDialog1.FileName))
                {
                    img = new Bitmap(bmpTemp);
                }

                if (img.Width == 160 && img.Height == 88)
                {
                    if (ImageUtils.HasMoreThanFiveColors(img))
                    {
                        DialogResult result1 = MessageBox.Show("Image has more than five colors. Continue?", "Warning", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.No)
                        {
                            return;
                        }
                    }

                    FormBitmapToMap formBitmapToMap = new FormBitmapToMap(mapSet,img, openFileDialog1.FileName);
                    var result = formBitmapToMap.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        mapSet.AddCharSet(formBitmapToMap.ReturnCharactedSet);
                        mapSet.AddMap(formBitmapToMap.ReturnMap);
                    }
                }
                else
                {
                    MessageBox.Show("Only images of size 160x88 are allowed!");
                }
            }
        }

        private void ImportMap(object sender)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null && owner.Tag is Map)
                {
                    Map map = owner.Tag as Map;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Image img;
                        using (var bmpTemp = new Bitmap(openFileDialog1.FileName))
                        {
                            img = new Bitmap(bmpTemp);
                        }

                        if (img.Width == 160 && img.Height == 88)
                        {
                            if (ImageUtils.HasMoreThanFiveColors(img))
                            {
                                DialogResult result1 = MessageBox.Show("Image has more than five colors. Continue?", "Warning", MessageBoxButtons.YesNo);
                                if (result1 == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            CharacterSet characterSet = mapSet.CharSets.First(set => set.UID == map.FontID);

                            FormBitmapToMap formBitmapToMap = new FormBitmapToMap(mapSet, img, openFileDialog1.FileName,characterSet);
                            var result = formBitmapToMap.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                //mapSet.AddCharSet(formBitmapToMap.ReturnCharactedSet);
                                //map.FontID = formBitmapToMap.ReturnCharactedSet.UID;
                                characterSet.Data = (byte[])formBitmapToMap.ReturnCharactedSet.Data.Clone();
                                map.MapData = formBitmapToMap.ReturnMap.MapData;
                                if ( CurrentMap() == map) {
                                    PopulateMap(map);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Only images of size 160x88 are allowed!");
                        }
                    }
                }
            }
        }
        private void importBitmapIntoCurrentFont()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CharacterSet characterSet = mapSet.CharSets.First(set => set.UID == CurrentMap().FontID);

                Image image;
                using (var bmpTemp = new Bitmap(openFileDialog1.FileName))
                {
                    image = new Bitmap(bmpTemp);
                }

                if (ImageUtils.HasMoreThanFiveColors(image)){
                     DialogResult result = MessageBox.Show("Image has more than five colors. Continue?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        {
                        return;
                        }
                    }

                FormBitmapToFnt formBitmapToFnt = new FormBitmapToFnt(image, characterSet, CurrentMap().DLIS);
                if (formBitmapToFnt.ShowDialog() == DialogResult.OK)
                {
                    characterSet.Data = formBitmapToFnt.ReturnFontData;
                    PopulateMap(CurrentMap());
                }
            }
        }

        private void newMapSet()
        {
            mapSet.New();
        }

        private void addAExistingMap()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Map|*.rle";
            openFileDialog.Title = "Load map";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                OpenFileDialog openFileDialog2 = new OpenFileDialog();
                openFileDialog2.Filter = "Font|*.fnt";
                openFileDialog2.Title = "Load Font for "+ openFileDialog.FileName;
                openFileDialog2.ShowDialog();
                if (openFileDialog2.FileName != "")
                {

                    mapSet.AddMap(openFileDialog.FileName, openFileDialog2.FileName);
                }
            }
        }

        private void addANewMap()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Font|*.fnt";
            openFileDialog.Title = "Load font for the new map";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                mapSet.AddMap(openFileDialog.FileName);
            }
        }

        private void load()
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();
            saveFileDialog1.Filter = "Mapset|*.xml";
            saveFileDialog1.Title = "Load Mapset";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                mapSet.Load(saveFileDialog1.FileName);
            }
        } 

        private void RenameMap(object sender)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null && owner.Tag is Map)
                {
                    Map map = owner.Tag as Map;
                    InputBoxResult result = InputBox.Show("New name", "Change Name of the map", map.Name, null);
                    if (result.OK)
                    {
                        mapSet.ChangeMapName(map, result.Text);
                    }
                }
            }
        }

        private void DeleteMap(object sender)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null && owner.Tag is Map)
                {
                    Map map = owner.Tag as Map;
                  //  InputBoxResult result = InputBox.Show("New name", "Change Name of the map", map.Name, null);
                   // if (result.OK)
                   // {
                        mapSet.DeleteMap(map);
                  //  }
                }
            }

        }

        bool enableCollapseExpand = true;

        private void treeViewMaps_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            if (e.Node.Tag is TypeNode typeNode)
            {
                switch (typeNode)
                {
                    case TypeNode.GameData:
                        {
                            FormGameData d = new FormGameData(mapSet);
                            if (d.ShowDialog() == DialogResult.OK)
                            {
                                mapSet.SpriteSet = (MapSet.SpriteSetEnum)d.SpritesSetComboBox.SelectedIndex;
                            }
                        }; break;

                    case TypeNode.Map:
                        {
                            Map map = GetMap(e.Node);
                            PopulateMap(map);
                        }; break;

                    case TypeNode.MapData:
                        {
                            Map map = GetMap(e.Node);
                            Form1 form1 = new Form1(map);
                            if (form1.ShowDialog() == DialogResult.OK)
                            {
                                map.Foe = form1.checkBoxFoe.Checked;

                                map.BruceStartX1 = Convert.ToByte(form1.numericUpDownBruceStartX1.Value);
                                map.BruceStartY1 = Convert.ToByte(form1.numericUpDownBruceStartY1.Value);
                                map.BruceStartX2 = Convert.ToByte(form1.numericUpDownBruceStartX2.Value);
                                map.BruceStartY2 = Convert.ToByte(form1.numericUpDownBruceStartY2.Value);

                                Guid mapId = (Guid)form1.comboBoxExit1Map.SelectedValue;
                                if (mapId == Map.EMPTY.UID)
                                {
                                    map.Exit1MapID = null;
                                }
                                else
                                {
                                    map.Exit1MapID = mapId;
                                }

                                map.Exit1X = Convert.ToByte(form1.numericUpDownExit1X.Value);
                                map.Exit1Y = Convert.ToByte(form1.numericUpDownExit1Y.Value);

                                mapId = (Guid)form1.comboBoxExit2Map.SelectedValue;
                                if (mapId == Map.EMPTY.UID)
                                {
                                    map.Exit2MapID = null;
                                }
                                else
                                {
                                    map.Exit2MapID = mapId;
                                }
                                map.Exit2X = Convert.ToByte(form1.numericUpDownExit2X.Value);
                                map.Exit2Y = Convert.ToByte(form1.numericUpDownExit2Y.Value);

                                mapId = (Guid)form1.comboBoxExit3Map.SelectedValue;
                                if (mapId == Map.EMPTY.UID)
                                {
                                    map.Exit3MapID = null;
                                }
                                else
                                {
                                    map.Exit3MapID = mapId;
                                }
                                map.Exit3X = Convert.ToByte(form1.numericUpDownExit3X.Value);
                                map.Exit3Y = Convert.ToByte(form1.numericUpDownExit3Y.Value);

                                mapId = (Guid)form1.comboBoxExit4Map.SelectedValue;
                                if (mapId == Map.EMPTY.UID)
                                {
                                    map.Exit4MapID = null;
                                }
                                else
                                {
                                    map.Exit4MapID = mapId;
                                }

                                map.Exit4X = Convert.ToByte(form1.numericUpDownExit4X.Value);
                                map.Exit4Y = Convert.ToByte(form1.numericUpDownExit4Y.Value);

                                map.NinjaEnterCount1 = Convert.ToByte(form1.numericUpDownNinjaEnterCount1.Value);
                                map.NinjaEnterCount2 = Convert.ToByte(form1.numericUpDownNinjaEnterCount2.Value);

                                map.YamoEnterCount1 = Convert.ToByte(form1.numericUpDownYamoEnterCount1.Value);
                                map.YamoEnterCount2 = Convert.ToByte(form1.numericUpDownYamoEnterCount2.Value);

                                if (form1.radioButtonYamoFavorA.Checked)
                                {
                                    map.YamoSpawnPosition = 1;
                                }
                                else if (form1.radioButtonYamoFavorB.Checked)
                                {
                                    map.YamoSpawnPosition = 2;
                                }
                                else
                                {
                                    map.YamoSpawnPosition = 0;
                                }

                                if (form1.radioButtonNinjaFavorA.Checked)
                                {
                                    map.NinjaSpawnPosition = 1;
                                }
                                else if (form1.radioButtonNinjaFavorB.Checked)
                                {
                                    map.NinjaSpawnPosition = 2;
                                }
                                else
                                {
                                    map.NinjaSpawnPosition = 0;
                                }
                            }
                        }; break;

                    case TypeNode.CColpf0:
                        {
                            Map map = GetMap(e.Node);
                            FormColision fedit = new FormColision(map, map.Colpf0Detection, map.Colpf0DetectionRects, map.Colpf0DetectionFlags);
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.Colpf0Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                                map.Colpf0DetectionRects.Clear();
                                map.Colpf0DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                                map.Colpf0DetectionFlags.Clear();
                                map.Colpf0DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                            }
                        }; break;

                    case TypeNode.CColpf2:
                        {
                            Map map = GetMap(e.Node);
                            FormColision fedit = new FormColision(map, map.Colpf2Detection, map.Colpf2DetectionRects, map.Colpf2DetectionFlags);
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.Colpf2Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                                map.Colpf2DetectionRects.Clear();
                                map.Colpf2DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                                map.Colpf2DetectionFlags.Clear();
                                map.Colpf2DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                            }
                        }; break;

                    case TypeNode.CColpf3:
                        {
                            Map map = GetMap(e.Node);
                            FormColision fedit = new FormColision(map, map.Colpf3Detection, map.Colpf3DetectionRects, map.Colpf3DetectionFlags);
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.Colpf3Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                                map.Colpf3DetectionRects.Clear();
                                map.Colpf3DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                                map.Colpf3DetectionFlags.Clear();
                                map.Colpf3DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                            }
                        }; break;

                    case TypeNode.MapInit:
                        {
                            Map map = GetMap(e.Node);
                            String code = (!String.IsNullOrEmpty(map.InitRoutine)) ? map.InitRoutine : $"\t\t; ** Map '{map.Name}' Init **\n\n\n\trts";
                            FormASMEdit fedit = new FormASMEdit(map,typeNode, code, $"Init Routine for map {map.Name}");
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.InitRoutine = fedit.scintilla1.Text;
                            }
                        };  break;

                    case TypeNode.MapExec:
                        {
                            Map map = GetMap(e.Node);
                            String code = (!String.IsNullOrEmpty(map.ExecRoutine)) ? map.ExecRoutine : $"\t\t; ** Map '{map.Name}' Exec **\n\n\n\trts";
                            FormASMEdit fedit = new FormASMEdit(map, typeNode, code, $"Exec Routine for map {map.Name}");
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.ExecRoutine = fedit.scintilla1.Text;
                            }
                        }; break;

                    case TypeNode.MapTileCollision:
                        {
                            Map map = GetMap(e.Node);
                            String code = (!String.IsNullOrEmpty(map.TileCollisionRoutine)) ? map.TileCollisionRoutine : $"\t\t; ** Map '{map.Name}' Tile Collision **\n\t\t; A = Title\n\t\t; X = Actor\n\n\t\trts";
                            FormASMEdit fedit = new FormASMEdit(map, typeNode, code, $"Tile Collision Routine for map {map.Name}");
                            if (fedit.ShowDialog() == DialogResult.OK)
                            {
                                map.TileCollisionRoutine = fedit.scintilla1.Text;
                            }
                        }; break;
                }
            }

            enableCollapseExpand = true;
        }  
        
        private void treeViewMaps_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            Debug.WriteLine("treeViewMaps_BeforeExpand");

            e.Cancel = !enableCollapseExpand;

            enableCollapseExpand = true;
        }   
  
        private void treeViewMaps_MouseClick(object sender, MouseEventArgs e)
        {
           
            TreeViewHitTestInfo info = treeViewMaps.HitTest(e.Location);

            Debug.WriteLine("treeViewMaps_MouseClick");

            enableCollapseExpand = (info.Location == TreeViewHitTestLocations.PlusMinus);

        }

        private void treeViewMaps_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            Debug.WriteLine("treeViewMaps_BeforeCollapse");
            e.Cancel = !enableCollapseExpand;
            enableCollapseExpand = true;
        }

        private void Run()
        {
            if (MapSet.SSave(mapSet))
            {
                FormRunMADS.Compile(mapSet, true, firstMapNumericUpDown.Value) ;
            }
        }
        private void BuildRelease()
        {
            if (MapSet.SSave(mapSet))
            {
                FormRunMADS.BuidRelease(mapSet);
            }
        }
        private void Setting()
        {
            FormSettings formSettings = new FormSettings();
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                formSettings.AppSettings.Save();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Mapset|*.xml";
            saveFileDialog1.Title = "Save Mapset";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                 mapSet.Export(saveFileDialog1.FileName);
            }
        }



    }
}
