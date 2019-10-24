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
            this.lblSelectName = new System.Windows.Forms.Label();
            this.flpMap = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxDliDisplay = new System.Windows.Forms.CheckBox();
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
            this.runButton = new System.Windows.Forms.Button();
            this.firstMapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEditFnt = new BLEditor.MenuButton();
            this.dliList = new BLEditor.DLIListUserControl();
            this.menuStrip1.SuspendLayout();
            this.contextMenuMap.SuspendLayout();
            this.contextMenuFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flpTiles
            // 
            this.flpTiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTiles.Location = new System.Drawing.Point(1424, 42);
            this.flpTiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpTiles.Name = "flpTiles";
            this.flpTiles.Size = new System.Drawing.Size(866, 422);
            this.flpTiles.TabIndex = 4;
            // 
            // lblSelected
            // 
            this.lblSelected.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelected.Location = new System.Drawing.Point(1252, 366);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(95, 196);
            this.lblSelected.TabIndex = 5;
            // 
            // lblSelectName
            // 
            this.lblSelectName.AutoSize = true;
            this.lblSelectName.Location = new System.Drawing.Point(987, 368);
            this.lblSelectName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectName.Name = "lblSelectName";
            this.lblSelectName.Size = new System.Drawing.Size(100, 20);
            this.lblSelectName.TabIndex = 6;
            this.lblSelectName.Text = "Selected Tile";
            // 
            // flpMap
            // 
            this.flpMap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMap.Location = new System.Drawing.Point(189, 43);
            this.flpMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpMap.Name = "flpMap";
            this.flpMap.Size = new System.Drawing.Size(964, 542);
            this.flpMap.TabIndex = 7;
            // 
            // cbxDliDisplay
            // 
            this.cbxDliDisplay.AutoSize = true;
            this.cbxDliDisplay.Enabled = false;
            this.cbxDliDisplay.Location = new System.Drawing.Point(1166, 322);
            this.cbxDliDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxDliDisplay.Name = "cbxDliDisplay";
            this.cbxDliDisplay.Size = new System.Drawing.Size(120, 24);
            this.cbxDliDisplay.TabIndex = 8;
            this.cbxDliDisplay.Text = "Display DLI\'s";
            this.cbxDliDisplay.UseVisualStyleBackColor = true;
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1924, 25);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 19);
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
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 19);
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
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
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
            this.contextMenuMap.Size = new System.Drawing.Size(181, 92);
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
            this.treeViewMaps.Location = new System.Drawing.Point(0, 43);
            this.treeViewMaps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewMaps.Name = "treeViewMaps";
            this.treeViewMaps.Size = new System.Drawing.Size(180, 726);
            this.treeViewMaps.TabIndex = 27;
            this.treeViewMaps.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeCollapse);
            this.treeViewMaps.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeExpand);
            this.treeViewMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeMouseDoubleClick);
            this.treeViewMaps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewMaps_MouseClick);
            // 
            // runButton
            // 
            this.runButton.Image = global::BLEditor.Properties.Resources.application_run;
            this.runButton.Location = new System.Drawing.Point(192, 597);
            this.runButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(112, 35);
            this.runButton.TabIndex = 29;
            this.runButton.Text = "Run";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // firstMapNumericUpDown
            // 
            this.firstMapNumericUpDown.Location = new System.Drawing.Point(428, 602);
            this.firstMapNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstMapNumericUpDown.Name = "firstMapNumericUpDown";
            this.firstMapNumericUpDown.Size = new System.Drawing.Size(180, 26);
            this.firstMapNumericUpDown.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 605);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "First Map:";
            // 
            // buttonEditFnt
            // 
            this.buttonEditFnt.Enabled = false;
            this.buttonEditFnt.Location = new System.Drawing.Point(2134, 474);
            this.buttonEditFnt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonEditFnt.Menu = this.contextMenuFont;
            this.buttonEditFnt.Name = "buttonEditFnt";
            this.buttonEditFnt.ShowMenuUnderCursor = false;
            this.buttonEditFnt.Size = new System.Drawing.Size(158, 35);
            this.buttonEditFnt.TabIndex = 26;
            this.buttonEditFnt.Text = "Font";
            this.buttonEditFnt.UseVisualStyleBackColor = true;
            // 
            // dliList
            // 
            this.dliList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dliList.Location = new System.Drawing.Point(1166, 42);
            this.dliList.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dliList.Name = "dliList";
            this.dliList.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.dliList.Size = new System.Drawing.Size(248, 270);
            this.dliList.TabIndex = 24;
            // 
            // pbx1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 771);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstMapNumericUpDown);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.treeViewMaps);
            this.Controls.Add(this.buttonEditFnt);
            this.Controls.Add(this.dliList);
            this.Controls.Add(this.cbxDliDisplay);
            this.Controls.Add(this.flpMap);
            this.Controls.Add(this.lblSelectName);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.flpTiles);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "pbx1";
            this.Text = "Bruce Lee Construction Kit (BLCK)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuMap.ResumeLayout(false);
            this.contextMenuFont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpTiles;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblSelectName;
        private System.Windows.Forms.FlowLayoutPanel flpMap;
        private System.Windows.Forms.CheckBox cbxDliDisplay;
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
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyCharMenu;
        private System.Windows.Forms.ToolStripMenuItem importFromBitmapMenu;
        private System.Windows.Forms.NumericUpDown firstMapNumericUpDown;
        private System.Windows.Forms.Label label1;
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
    }
}

