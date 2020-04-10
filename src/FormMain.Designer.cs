namespace BLEditor
{
    partial class pbx1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flpTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSelected = new System.Windows.Forms.Label();
            this.flpMap = new System.Windows.Forms.FlowLayoutPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addANewMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addANewMapFromAnImageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addAnExistingMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.addIncludeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buildReleaseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromBitmapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFont = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editFontMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyCharMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importBitmapIntoFontMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFromFontMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewMaps = new System.Windows.Forms.TreeView();
            this.contextMenuInclude = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addIncludeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.buttonEditFnt = new BLEditor.MenuButton();
            this.dliList = new BLEditor.DLIListUserControl();
            this.picBxMap = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.firstMapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.runButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuMap.SuspendLayout();
            this.contextMenuFont.SuspendLayout();
            this.contextMenuInclude.SuspendLayout();
            this.contextMenuOpen.SuspendLayout();
            this.picBxMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flpTiles
            // 
            this.flpTiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTiles.Location = new System.Drawing.Point(825, 3);
            this.flpTiles.Name = "flpTiles";
            this.flpTiles.Size = new System.Drawing.Size(578, 275);
            this.flpTiles.TabIndex = 4;
            // 
            // lblSelected
            // 
            this.lblSelected.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelected.Location = new System.Drawing.Point(652, 186);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(64, 128);
            this.lblSelected.TabIndex = 5;
            // 
            // flpMap
            // 
            this.flpMap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMap.Location = new System.Drawing.Point(5, 6);
            this.flpMap.Name = "flpMap";
            this.flpMap.Size = new System.Drawing.Size(643, 353);
            this.flpMap.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.gameToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1480, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenu,
            this.loadMenu,
            this.saveMenu,
            this.saveAsMenu,
            this.exportToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newMenu
            // 
            this.newMenu.Name = "newMenu";
            this.newMenu.Size = new System.Drawing.Size(114, 22);
            this.newMenu.Text = "New";
            // 
            // loadMenu
            // 
            this.loadMenu.Name = "loadMenu";
            this.loadMenu.Size = new System.Drawing.Size(114, 22);
            this.loadMenu.Text = "Load";
            // 
            // saveMenu
            // 
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.Size = new System.Drawing.Size(114, 22);
            this.saveMenu.Text = "Save";
            // 
            // saveAsMenu
            // 
            this.saveAsMenu.Name = "saveAsMenu";
            this.saveAsMenu.Size = new System.Drawing.Size(114, 22);
            this.saveAsMenu.Text = "Save As";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.addANewMapMenu,
            this.addANewMapFromAnImageMenu,
            this.addAnExistingMapMenu,
            this.toolStripSeparator4,
            this.addIncludeMenu,
            this.toolStripSeparator1,
            this.runMenu,
            this.buildReleaseMenu});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(235, 6);
            // 
            // addANewMapMenu
            // 
            this.addANewMapMenu.Name = "addANewMapMenu";
            this.addANewMapMenu.Size = new System.Drawing.Size(238, 22);
            this.addANewMapMenu.Text = "Add a new map";
            // 
            // addANewMapFromAnImageMenu
            // 
            this.addANewMapFromAnImageMenu.Name = "addANewMapFromAnImageMenu";
            this.addANewMapFromAnImageMenu.Size = new System.Drawing.Size(238, 22);
            this.addANewMapFromAnImageMenu.Text = "Add a new map from an image";
            // 
            // addAnExistingMapMenu
            // 
            this.addAnExistingMapMenu.Name = "addAnExistingMapMenu";
            this.addAnExistingMapMenu.Size = new System.Drawing.Size(238, 22);
            this.addAnExistingMapMenu.Text = "Add an existing map";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(235, 6);
            // 
            // addIncludeMenu
            // 
            this.addIncludeMenu.Name = "addIncludeMenu";
            this.addIncludeMenu.Size = new System.Drawing.Size(238, 22);
            this.addIncludeMenu.Text = "Add Include";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
            // 
            // runMenu
            // 
            this.runMenu.Name = "runMenu";
            this.runMenu.Size = new System.Drawing.Size(238, 22);
            this.runMenu.Text = "Run";
            // 
            // buildReleaseMenu
            // 
            this.buildReleaseMenu.Name = "buildReleaseMenu";
            this.buildReleaseMenu.Size = new System.Drawing.Size(238, 22);
            this.buildReleaseMenu.Text = "Build Release";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsMenu});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // settingsMenu
            // 
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(116, 22);
            this.settingsMenu.Text = "Settings";
            // 
            // contextMenuMap
            // 
            this.contextMenuMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameMenu,
            this.deleteMenu,
            this.importFromBitmapMenu});
            this.contextMenuMap.Name = "contextMenuMap";
            this.contextMenuMap.Size = new System.Drawing.Size(181, 70);
            // 
            // renameMenu
            // 
            this.renameMenu.Name = "renameMenu";
            this.renameMenu.Size = new System.Drawing.Size(180, 22);
            this.renameMenu.Text = "Rename";
            // 
            // deleteMenu
            // 
            this.deleteMenu.Name = "deleteMenu";
            this.deleteMenu.Size = new System.Drawing.Size(180, 22);
            this.deleteMenu.Text = "Delete";
            // 
            // importFromBitmapMenu
            // 
            this.importFromBitmapMenu.Name = "importFromBitmapMenu";
            this.importFromBitmapMenu.Size = new System.Drawing.Size(180, 22);
            this.importFromBitmapMenu.Text = "Import from bitmap";
            // 
            // contextMenuFont
            // 
            this.contextMenuFont.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editFontMenu,
            this.CopyCharMenu,
            this.importBitmapIntoFontMenu,
            this.copyFromFontMenu});
            this.contextMenuFont.Name = "contextMenuFont";
            this.contextMenuFont.Size = new System.Drawing.Size(233, 92);
            // 
            // editFontMenu
            // 
            this.editFontMenu.Name = "editFontMenu";
            this.editFontMenu.Size = new System.Drawing.Size(232, 22);
            this.editFontMenu.Text = "Edit font";
            // 
            // CopyCharMenu
            // 
            this.CopyCharMenu.Name = "CopyCharMenu";
            this.CopyCharMenu.Size = new System.Drawing.Size(232, 22);
            this.CopyCharMenu.Text = "Copy Char";
            // 
            // importBitmapIntoFontMenu
            // 
            this.importBitmapIntoFontMenu.Name = "importBitmapIntoFontMenu";
            this.importBitmapIntoFontMenu.Size = new System.Drawing.Size(232, 22);
            this.importBitmapIntoFontMenu.Text = "Import bitmap into font";
            // 
            // copyFromFontMenu
            // 
            this.copyFromFontMenu.Name = "copyFromFontMenu";
            this.copyFromFontMenu.Size = new System.Drawing.Size(232, 22);
            this.copyFromFontMenu.Text = "Import tiles from another font";
            // 
            // treeViewMaps
            // 
            this.treeViewMaps.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewMaps.Location = new System.Drawing.Point(0, 24);
            this.treeViewMaps.Name = "treeViewMaps";
            this.treeViewMaps.Size = new System.Drawing.Size(121, 573);
            this.treeViewMaps.TabIndex = 27;
            this.treeViewMaps.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeCollapse);
            this.treeViewMaps.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeExpand);
            this.treeViewMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeMouseDoubleClick);
            this.treeViewMaps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewMaps_MouseClick);
            // 
            // contextMenuInclude
            // 
            this.contextMenuInclude.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.addIncludeToolStripMenuItem});
            this.contextMenuInclude.Name = "contextMenuInclude";
            this.contextMenuInclude.Size = new System.Drawing.Size(139, 48);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // addIncludeToolStripMenuItem
            // 
            this.addIncludeToolStripMenuItem.Name = "addIncludeToolStripMenuItem";
            this.addIncludeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addIncludeToolStripMenuItem.Text = "Add Include";
            // 
            // contextMenuOpen
            // 
            this.contextMenuOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.contextMenuOpen.Name = "contextMenuOpen";
            this.contextMenuOpen.Size = new System.Drawing.Size(104, 26);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(121, 24);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 573);
            this.splitter1.TabIndex = 34;
            this.splitter1.TabStop = false;
            // 
            // buttonEditFnt
            // 
            this.buttonEditFnt.Enabled = false;
            this.buttonEditFnt.Location = new System.Drawing.Point(1423, 308);
            this.buttonEditFnt.Menu = this.contextMenuFont;
            this.buttonEditFnt.Name = "buttonEditFnt";
            this.buttonEditFnt.ShowMenuUnderCursor = false;
            this.buttonEditFnt.Size = new System.Drawing.Size(105, 23);
            this.buttonEditFnt.TabIndex = 26;
            this.buttonEditFnt.Text = "Font";
            this.buttonEditFnt.UseVisualStyleBackColor = true;
            // 
            // dliList
            // 
            this.dliList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dliList.Location = new System.Drawing.Point(652, 3);
            this.dliList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dliList.Name = "dliList";
            this.dliList.Padding = new System.Windows.Forms.Padding(5);
            this.dliList.Size = new System.Drawing.Size(166, 176);
            this.dliList.TabIndex = 24;
            // 
            // picBxMap
            // 
            this.picBxMap.AutoScroll = true;
            this.picBxMap.Controls.Add(this.label1);
            this.picBxMap.Controls.Add(this.firstMapNumericUpDown);
            this.picBxMap.Controls.Add(this.runButton);
            this.picBxMap.Controls.Add(this.flpMap);
            this.picBxMap.Controls.Add(this.dliList);
            this.picBxMap.Controls.Add(this.flpTiles);
            this.picBxMap.Controls.Add(this.lblSelected);
            this.picBxMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBxMap.Location = new System.Drawing.Point(123, 24);
            this.picBxMap.Margin = new System.Windows.Forms.Padding(2);
            this.picBxMap.Name = "picBxMap";
            this.picBxMap.Size = new System.Drawing.Size(1357, 573);
            this.picBxMap.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "First Map:";
            // 
            // firstMapNumericUpDown
            // 
            this.firstMapNumericUpDown.Location = new System.Drawing.Point(145, 365);
            this.firstMapNumericUpDown.Name = "firstMapNumericUpDown";
            this.firstMapNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.firstMapNumericUpDown.TabIndex = 33;
            // 
            // runButton
            // 
            this.runButton.Image = global::BLEditor.Properties.Resources.application_run;
            this.runButton.Location = new System.Drawing.Point(5, 364);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 32;
            this.runButton.Text = "Run";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // pbx1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 597);
            this.Controls.Add(this.picBxMap);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeViewMaps);
            this.Controls.Add(this.buttonEditFnt);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pbx1";
            this.Text = "Bruce Lee Construction Kit (BLCK)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuMap.ResumeLayout(false);
            this.contextMenuFont.ResumeLayout(false);
            this.contextMenuInclude.ResumeLayout(false);
            this.contextMenuOpen.ResumeLayout(false);
            this.picBxMap.ResumeLayout(false);
            this.picBxMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpTiles;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.FlowLayoutPanel flpMap;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DLIListUserControl dliList;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.ToolStripMenuItem loadMenu;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuMap;
        private System.Windows.Forms.ToolStripMenuItem renameMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteMenu;
        private MenuButton buttonEditFnt;
        private System.Windows.Forms.ContextMenuStrip contextMenuFont;
        private System.Windows.Forms.ToolStripMenuItem editFontMenu;
        private System.Windows.Forms.ToolStripMenuItem importBitmapIntoFontMenu;
        private System.Windows.Forms.ToolStripMenuItem copyFromFontMenu;
        private System.Windows.Forms.TreeView treeViewMaps;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyCharMenu;
        private System.Windows.Forms.ToolStripMenuItem importFromBitmapMenu;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem addANewMapMenu;
        private System.Windows.Forms.ToolStripMenuItem addANewMapFromAnImageMenu;
        private System.Windows.Forms.ToolStripMenuItem addAnExistingMapMenu;
        private System.Windows.Forms.ToolStripMenuItem addIncludeMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem runMenu;
        private System.Windows.Forms.ToolStripMenuItem buildReleaseMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuInclude;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addIncludeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel picBxMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown firstMapNumericUpDown;
        private System.Windows.Forms.Button runButton;
    }
}

