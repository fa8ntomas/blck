using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class pbx1 : Form
    {
        Bitmap[] tileArray;
        byte[] chars;
        bool mouseD = false;
        readonly MapSet mapSet = new MapSet();
        List<byte> extraMapBytes = new List<byte>();
        public enum TypeNode { GameData, Fonts, FontFile, Includes, IncludeFile, Map, MapInit, MapExec, MapTileCollision, MapData, MapCColpf0, MapCColpf2, MapCColpf3 };

        private Action openMenuAction;

        public pbx1()
        {
            InitializeComponent();
            mapSet.StrutureTreeChanged += (s, e) => MapSetStrutureTreeChanged();
            mapSet.OnDLISChanged += (s, e) => mapSet_OnDLISChanged();
            mapSet.MapNameChanged += (s, e) => MapNameChanged(e as MapNameChangedEventArgs); 

            for (int i = 0; i < 440; i++)
            {
                Tile tile = new Tile(i);
                tile.MouseDown += Lm_MouseDown;
                tile.MouseUp += Lm_MouseUp;
                tile.MouseEnter += Lm_MouseEnter;
                tile.MouseMove += Lm_MouseMove;
                tile.MouseClick += Lm_MouseClick;
                flpMap.Controls.Add(tile);
            }

            newMenu.Click += (s, e) => NewMapSet();
            loadMenu.Click += (s, e) =>  LoadMapset(); 
            saveMenu.Click += (s, e) => MapSet.SSave(mapSet); 
            saveAsMenu.Click += (s, e) => MapSet.SSaveAs(mapSet); 
            addANewMapFromAnImageMenu.Click += (s, e) =>  AddANewMapFromAnImage(); 
            addANewMapMenu.Click += (s, e) =>  AddNewMap(); 
            addAnExistingMapMenu.Click += (s, e) => AddExistingMap(); 
            addIncludeMenu.Click += (s, e) =>  AddInclude();
            addIncludeToolStripMenuItem.Click += (s, e) => AddInclude();
            removeToolStripMenuItem.Click += (s, e) => RemoveInclude(s);
            settingsMenu.Click += (s, e) => Setting(); 
            runMenu.Click += (s, e) => Run(); 
            buildReleaseMenu.Click += (s, e) => BuildRelease(); 

            runButton.Click += (s, e) =>  Run(); 

            treeViewMaps.MouseDown += (sender, args) => treeViewMaps_MouseDown(args);

            renameMenu.Click += (s, e) =>  RenameMap(s); 
            deleteMenu.Click += (s, e) =>  DeleteMap(s);
            importFromBitmapMenu.Click += (s, e) => ImportMap(s);
            importBitmapIntoFontMenu.Click += (s, e) => ImportBitmapIntoCurrentFont(s);
            editFontMenu.Click += (s, e) =>  EditCurrentFont(s); 
            copyFromFontMenu.Click += (s, e) => CopyCurrentFont(s); 
            CopyCharMenu.Click += (s, e) => CopyChar(s);

            openToolStripMenuItem.Click += (s, e) => { openMenuAction?.Invoke(); };
        }

        private void RemoveInclude(object sender)
        {
            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip owner && owner.Tag is String path)
            {
                mapSet.RemoveInclude(path);
            }
        }

        private void AddInclude()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Assembly|*.asm",
                Title = "Add include"
            })
            {
                openFileDialog.ShowDialog();

                if (!String.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    mapSet.AddInclude(openFileDialog.FileName);
                }
            }
        }

  

        private void treeViewMaps_MouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            TreeNode node_here = treeViewMaps.GetNodeAt(e.X, e.Y);
            treeViewMaps.SelectedNode = node_here;

            if (node_here == null) return;

            if (node_here.Tag is TypeNode typeNode)
            {
                switch (typeNode)
                {
                    case TypeNode.Map:
                        contextMenuMap.Tag = GetMap(node_here);
                        contextMenuMap.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.FontFile:
                        FontTreeNode fontTreeNode = (FontTreeNode)node_here;
                        contextMenuFont.Tag = fontTreeNode.CharacterSet;
                        contextMenuFont.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.Includes:
                        removeToolStripMenuItem.Visible = false;
                        contextMenuInclude.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.IncludeFile:
                        IncludeTreeNode includeTreeNode = (IncludeTreeNode)node_here;
                        removeToolStripMenuItem.Visible = true;
                        contextMenuInclude.Tag = includeTreeNode.Path;
                        contextMenuInclude.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.GameData:
                        openMenuAction = () => EditGameData();
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapInit:
                        openMenuAction = () => EditInitMapCode(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapExec:
                        openMenuAction = () => EditExecMapCode(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapTileCollision:
                        openMenuAction = () => EditTileCollisionMapCode(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapData:
                        openMenuAction = () => EditMapData(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapCColpf0:
                        openMenuAction = () => EditMapCColpf0(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapCColpf2:
                        openMenuAction = () => EditMapCColpf2(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                    case TypeNode.MapCColpf3:
                        openMenuAction = () => EditMapCColpf3(node_here);
                        contextMenuOpen.Show(treeViewMaps, new Point(e.X, e.Y));
                        break;
                }      
            }
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

        private void CopyCurrentFont(object sender)
        {
            CharacterSet characterSet = null;

            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip owner && owner.Tag is CharacterSet contextCharacterSet)
            {
                characterSet = contextCharacterSet;
            } else if (DisplayedMap != null)
            {
                characterSet = mapSet.CharSets.First(set => set.UID == DisplayedMap.FontID);
            }

            if (characterSet != null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Font|*.fnt",
                    Title = "Load font to import"
                })
                {
                    openFileDialog.ShowDialog();

                    if (!String.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        CharacterSet characterSetFrom = mapSet.CharSets.FirstOrDefault(set => set.Path == openFileDialog.FileName);
                        if (characterSetFrom == null)
                        {
                            characterSetFrom = CharacterSet.CreateFromFileName(openFileDialog.FileName);
                        }

                        using (FormFntToFnt formFntToFnt = new FormFntToFnt(characterSet, DisplayedMap?.DLIS, characterSetFrom))
                        {
                            if (formFntToFnt.ShowDialog() == DialogResult.OK)
                            {
                                characterSet.Data = formFntToFnt.ReturnFontData;
                                DisplayMap(DisplayedMap);
                            }
                        }
                    }
                }
            }

        }
        private void CopyChar(object sender)
        {
            CharacterSet characterSet = null;

            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip owner && owner.Tag is CharacterSet contextCharacterSet)
            {
                characterSet = contextCharacterSet;
            }
            else if (DisplayedMap != null)
            {
                characterSet = mapSet.CharSets.First(set => set.UID == DisplayedMap.FontID);
            }

            if (characterSet != null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    using (FormFntToFnt formFntToFnt = new FormFntToFnt(characterSet, DisplayedMap?.DLIS, characterSet))
                    {
                        if (formFntToFnt.ShowDialog() == DialogResult.OK)
                        {
                            characterSet.Data = formFntToFnt.ReturnFontData;
                            DisplayMap(DisplayedMap);
                        }
                    }
                }
            }
        }
        private void mapSet_OnDLISChanged()
        {
             DisplayMap(DisplayedMap);
            
        }

        private void MapSetStrutureTreeChanged()
        {
            if (DisplayedMap != null && !mapSet.Maps.Contains(DisplayedMap))
            {
                flpTiles.Controls.Clear();

                for (int i = 0; i < 440; i++)
                {
                    (flpMap.Controls[i] as Tile).Text = null;
                    (flpMap.Controls[i] as Tile).Image = null;
                }

                dliList.Map = null;
                DisplayedMap = null;
            }

            // GAMEDATA

            TreeNode gameDataNode = treeViewMaps.Nodes.Cast<TreeNode>().Where(a => a.Tag is TypeNode typeNode && typeNode == TypeNode.GameData).FirstOrDefault();
            if (gameDataNode == null)
            {
                gameDataNode = new TreeNode("Game Data")
                {
                    Tag = TypeNode.GameData
                };
                treeViewMaps.Nodes.Add(gameDataNode);
            }

            // INCLUDES

            TreeNode gameIncludesNode = treeViewMaps.Nodes.Cast<TreeNode>().Where(a => a.Tag is TypeNode typeNode && typeNode == TypeNode.Includes).FirstOrDefault();
            if (gameIncludesNode == null) {
                gameIncludesNode = new TreeNode("Includes")
                {
                    Tag = TypeNode.Includes
                };
                treeViewMaps.Nodes.Add(gameIncludesNode);
            }

            foreach (IncludeTreeNode node in gameIncludesNode.Nodes.Cast<IncludeTreeNode>().Where(a => !mapSet.Includes.Contains(a.Path)))
            {
                gameIncludesNode.Nodes.Remove(node);
            }

            foreach (String include in mapSet.Includes)
            {
                IncludeTreeNode gameIncludeNode = gameIncludesNode.Nodes.Cast<IncludeTreeNode>().Where(a => a.Path == include).FirstOrDefault();
                if (gameIncludeNode == null)
                {
                    gameIncludesNode.Nodes.Add(new IncludeTreeNode(mapSet, include));
                }
            }

            // FONTS

            TreeNode fontsNode = treeViewMaps.Nodes.Cast<TreeNode>().Where(a => a.Tag is TypeNode typeNode && typeNode == TypeNode.Fonts).FirstOrDefault();
            if (fontsNode == null) { 
                fontsNode = new TreeNode("Fonts")
                {
                    Tag = TypeNode.Fonts
                };

                treeViewMaps.Nodes.Add(fontsNode);
            }

            foreach (FontTreeNode node in fontsNode.Nodes.OfType<FontTreeNode>().Where(a => !mapSet.CharSets.Contains(a.CharacterSet)))
            {
                fontsNode.Nodes.Remove(node);
            }

            foreach (var charset in mapSet.CharSets)
            {
                FontTreeNode fontFileNode = fontsNode.Nodes.OfType<FontTreeNode>().Where(a => a.CharacterSet.Equals(charset)).FirstOrDefault();
                if (fontFileNode == null)
                {                 
                    fontsNode.Nodes.Add(new FontTreeNode(mapSet, charset));
                }
             }

            // MAPS 

            foreach (MapTreeNode node in treeViewMaps.Nodes.OfType<MapTreeNode>().Where(a => !mapSet.Maps.Contains(a.Map)))
            {
                treeViewMaps.Nodes.Remove(node);
            }

            foreach (Map map in mapSet.Maps)
            {
                MapTreeNode mapNode = treeViewMaps.Nodes.OfType<MapTreeNode>().Where(a => a.Map.Equals(map)).FirstOrDefault();
                if (mapNode == null)
                {
                    TreeNode dataNode = new TreeNode("Map Data")
                    {
                        Tag = TypeNode.MapData
                    };

                    TreeNode initNode = new TreeNode("Init routine")
                    {
                        Tag = TypeNode.MapInit
                    };

                    TreeNode execNode = new TreeNode("Exec routine")
                    {
                        Tag = TypeNode.MapExec
                    };

                    TreeNode tcollisionNode = new TreeNode("Tite Collision routine")
                    {
                        Tag = TypeNode.MapTileCollision
                    };

                    TreeNode ccolpf0 = new TreeNode("Colpf0 Collision")
                    {
                        Tag = TypeNode.MapCColpf0
                    };

                    TreeNode ccolpf2 = new TreeNode("Colpf2 Collision")
                    {
                        Tag = TypeNode.MapCColpf2
                    };

                    TreeNode ccolpf3 = new TreeNode("Colpf3 Collision")
                    {
                        Tag = TypeNode.MapCColpf3
                    };

                    TreeNode[] array = new TreeNode[] { initNode, execNode, tcollisionNode, dataNode, ccolpf0, ccolpf2, ccolpf3 };

                    treeViewMaps.Nodes.Add(new MapTreeNode(map, array));
                }
            }
        }

        private void MapNameChanged(MapNameChangedEventArgs e)
        {

            MapTreeNode result = treeViewMaps.Nodes.OfType<MapTreeNode>()
                            .FirstOrDefault(node => node.Map.Equals(e.Map));

            result.Text = e.NewName;
        }

        private void Lm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSelected.Text))
            {
                (sender as Tile).Text = lblSelected.Text;
                Map currentMap = DisplayedMap;
                int selectedNumber = int.Parse(lblSelected.Text, System.Globalization.NumberStyles.HexNumber);
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

        private void Lm_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSelected.Text) && mouseD)
            {
                (sender as Tile).Text = lblSelected.Text;
                int selectedNumber = int.Parse(lblSelected.Text, System.Globalization.NumberStyles.HexNumber);
                Map currentMap = DisplayedMap;
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

        private void CreateTiles(byte[] byteSet)
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
                if (DisplayedMap != null && DisplayedMap.DLIS.Length == 1)
                {
                    tile = new Tile(i, chars, DisplayedMap.DLIS[0].AtariPFColors.ToBLColor());
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

        private void DisplayMap(Map inMap)
        {
            DisplayedMap = inMap;

            dliList.Map = inMap;
            byte[] bytes = mapSet.CharSets.First(set => set.UID == inMap.FontID).Data;

            RbgPFColors colorMap = inMap.DLIS[0].AtariPFColors.ToBLColor();

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

                (tile.Tag as ToolTip).SetToolTip(tile, inMap.MapData[i].ToString("X2") + " [" + (i % 40).ToString("X2") + "," + (i / 40 + 2).ToString("X2") + "]");

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

        private void EditCurrentFont(object sender)
        {
            CharacterSet characterSet = null;

            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip owner && owner.Tag is CharacterSet contextCharacterSet)
            {
                characterSet = contextCharacterSet;
            }
            else if (DisplayedMap != null)
            {
                characterSet = mapSet.CharSets.First(set => set.UID == DisplayedMap.FontID);
            }

            if (characterSet != null)
            {
                using (FormFntEdit fontEditForm = new FormFntEdit(characterSet, DisplayedMap?.DLIS))
                {
                    if (fontEditForm.ShowDialog() == DialogResult.OK)
                    {
                        characterSet.Data = fontEditForm.ReturnFontData;
                        DisplayMap(DisplayedMap);
                    }
                }
            }
        }


        Map DisplayedMap { get; set; }

        private void AddANewMapFromAnImage()
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

                    using (FormBitmapToMap formBitmapToMap = new FormBitmapToMap(mapSet, img, openFileDialog1.FileName))
                    {
                        var result = formBitmapToMap.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            mapSet.AddCharSet(formBitmapToMap.ReturnCharactedSet);
                            mapSet.AddMap(formBitmapToMap.ReturnMap);

                        }
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

                            FormBitmapToMap formBitmapToMap = new FormBitmapToMap(mapSet, img, openFileDialog1.FileName, characterSet);
                            var result = formBitmapToMap.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                //mapSet.AddCharSet(formBitmapToMap.ReturnCharactedSet);
                                //map.FontID = formBitmapToMap.ReturnCharactedSet.UID;
                                characterSet.Data = (byte[])formBitmapToMap.ReturnCharactedSet.Data.Clone();
                                map.MapData = formBitmapToMap.ReturnMap.MapData;
                                if (Object.Equals(DisplayedMap, map)) {
                                    DisplayMap(map);
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
        private void ImportBitmapIntoCurrentFont(object sender)
        {
            CharacterSet characterSet = null;

            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip owner && owner.Tag is CharacterSet contextCharacterSet)
            {
                characterSet = contextCharacterSet;
            }
            else if (DisplayedMap != null)
            {
                characterSet = mapSet.CharSets.First(set => set.UID == DisplayedMap.FontID);
            }

            if (characterSet != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    Image image;
                    using (var bmpTemp = new Bitmap(openFileDialog1.FileName))
                    {
                        image = new Bitmap(bmpTemp);
                    }

                    if (ImageUtils.HasMoreThanFiveColors(image))
                    {
                        DialogResult result = MessageBox.Show("Image has more than five colors. Continue?", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    FormBitmapToFnt formBitmapToFnt = new FormBitmapToFnt(image, characterSet, DisplayedMap.DLIS);
                    if (formBitmapToFnt.ShowDialog() == DialogResult.OK)
                    {
                        characterSet.Data = formBitmapToFnt.ReturnFontData;
                        DisplayMap(DisplayedMap);
                    }
                }
            }
        }

        private void NewMapSet()
        {
            mapSet.New();
        }

        private void AddExistingMap()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Map|*.rle",
                Title = "Load map"
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                OpenFileDialog openFileDialog2 = new OpenFileDialog
                {
                    Filter = "Font|*.fnt",
                    Title = "Load Font for " + openFileDialog.FileName
                };
                openFileDialog2.ShowDialog();
                if (openFileDialog2.FileName != "")
                {
                    mapSet.AddMap(openFileDialog.FileName, openFileDialog2.FileName);
                }
            }
        }

        private void AddNewMap()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Font|*.fnt",
                Title = "Load font for the new map"
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                mapSet.AddMap(openFileDialog.FileName);
            }
        }

        private void LoadMapset()
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog
            {
                Filter = "Mapset|*.xml",
                Title = "Load Mapset"
            };
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
                    mapSet.DeleteMap(map);
                }
            }

        }

        bool enableCollapseExpand = true;

        private void NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            if (e.Node.Tag is TypeNode typeNode)
            {
                switch (typeNode)
                {
                    case TypeNode.GameData:
                        {
                            EditGameData();
                        }; break;

                    case TypeNode.Map:
                        {
                            OpenMap(e.Node);
                        }; break;

                    case TypeNode.MapData:
                        {
                            EditMapData(e.Node);
                        }; break;

                    case TypeNode.MapCColpf0:
                        {
                            EditMapCColpf0(e.Node);
                        }; break;

                    case TypeNode.MapCColpf2:
                        {
                            EditMapCColpf2(e.Node);
                        }; break;

                    case TypeNode.MapCColpf3:
                        {
                            EditMapCColpf3(e.Node);
                        }; break;

                    case TypeNode.MapInit:
                        {
                            EditInitMapCode(e.Node);
                        };  break;

                    case TypeNode.MapExec:
                        {
                            EditExecMapCode(e.Node);
                        }; break;

                    case TypeNode.MapTileCollision:
                        {
                            EditTileCollisionMapCode(e.Node);
                        }; break;
                }
            }

            enableCollapseExpand = true;
        }

        private void EditMapCColpf3(TreeNode node)
        {
            Map map = GetMap(node);
            using (FormColision fedit = new FormColision(map, map.Colpf3Detection, map.Colpf3DetectionRects, map.Colpf3DetectionFlags, "Colpf3 detection"))
            {
                if (fedit.ShowDialog() == DialogResult.OK)
                {
                    map.Colpf3Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                    map.Colpf3DetectionRects.Clear();
                    map.Colpf3DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                    map.Colpf3DetectionFlags.Clear();
                    map.Colpf3DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                }
            }
        }

        private void EditMapCColpf2(TreeNode node)
        {
            Map map = GetMap(node);
            using (FormColision fedit = new FormColision(map, map.Colpf2Detection, map.Colpf2DetectionRects, map.Colpf2DetectionFlags, "Colpf2 detection"))
            {
                if (fedit.ShowDialog() == DialogResult.OK)
                {
                    map.Colpf2Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                    map.Colpf2DetectionRects.Clear();
                    map.Colpf2DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                    map.Colpf2DetectionFlags.Clear();
                    map.Colpf2DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                }
            }
        }

        private void EditMapCColpf0(TreeNode node)
        {
            Map map = GetMap(node);
            using (FormColision fedit = new FormColision(map, map.Colpf0Detection, map.Colpf0DetectionRects, map.Colpf0DetectionFlags, "Colpf0 detection"))
            {
                if (fedit.ShowDialog() == DialogResult.OK)
                {
                    map.Colpf0Detection = (Map.TypeColorDetection)fedit.comboBoxColorDetection.SelectedIndex;

                    map.Colpf0DetectionRects.Clear();
                    map.Colpf0DetectionRects.AddRange(fedit.zoneCollisionUserControl1.Zones);

                    map.Colpf0DetectionFlags.Clear();
                    map.Colpf0DetectionFlags.AddRange(fedit.zoneCollisionUserControl1.Flags);
                }
            }
        }

        private void EditMapData(TreeNode node)
        {
            Map map = GetMap(node);
            using(Form1 form1 = new Form1(map)){
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
            }
        }

        private void OpenMap(TreeNode node)
        {
            if (node is MapTreeNode mapNode)
            {
                DisplayMap(mapNode.Map);
            }
        }

        private void EditTileCollisionMapCode(TreeNode node)
        {
            Map map = GetMap(node);
            String code = (!String.IsNullOrEmpty(map.TileCollisionRoutine)) ? map.TileCollisionRoutine : $"\t\t; ** Map '{map.Name}' Tile Collision **\n\t\t; A = Title\n\t\t; X = Actor\n\n\t\trts";
            FormASMEdit fedit = new FormASMEdit(map, TypeNode.MapTileCollision, code, $"Tile Collision Routine for map {map.Name}");
            if (fedit.ShowDialog() == DialogResult.OK)
            {
                map.TileCollisionRoutine = fedit.scintilla1.Text;
            }
        }

        private void EditExecMapCode(TreeNode node)
        {
            Map map = GetMap(node);
            String code = (!String.IsNullOrEmpty(map.ExecRoutine)) ? map.ExecRoutine : $"\t\t; ** Map '{map.Name}' Exec **\n\n\n\trts";
            FormASMEdit fedit = new FormASMEdit(map, TypeNode.MapExec, code, $"Exec Routine for map {map.Name}");
            if (fedit.ShowDialog() == DialogResult.OK)
            {
                map.ExecRoutine = fedit.scintilla1.Text;
            }
        }

        private void EditInitMapCode(TreeNode node)
        {
            Map map = GetMap(node);
            String code = (!String.IsNullOrEmpty(map.InitRoutine)) ? map.InitRoutine : $"\t\t; ** Map '{map.Name}' Init **\n\n\n\trts";
            FormASMEdit fedit = new FormASMEdit(map, TypeNode.MapInit, code, $"Init Routine for map {map.Name}");
            if (fedit.ShowDialog() == DialogResult.OK)
            {
                map.InitRoutine = fedit.scintilla1.Text;
            }
        }

        private void EditGameData()
        {
            using (FormGameData d = new FormGameData(mapSet))
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    mapSet.SpriteSet = (MapSet.SpriteSetEnum)d.SpritesSetComboBox.SelectedIndex;
                }
            }
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
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Mapset|*.xml",
                Title = "Save Mapset"
            };
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                 mapSet.Export(saveFileDialog1.FileName);
            }
        }  
    }
}
