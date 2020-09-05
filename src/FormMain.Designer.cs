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
            this.multiPagePanel = new BLEditor.MultiPagePanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.contextMenuMap.SuspendLayout();
            this.contextMenuFont.SuspendLayout();
            this.contextMenuInclude.SuspendLayout();
            this.contextMenuOpen.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.gameToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1283, 24);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 22);
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
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 22);
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
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
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
            this.contextMenuMap.ImageScalingSize = new System.Drawing.Size(24, 24);
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
            this.contextMenuFont.ImageScalingSize = new System.Drawing.Size(24, 24);
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
            this.treeViewMaps.Size = new System.Drawing.Size(167, 636);
            this.treeViewMaps.TabIndex = 27;
            this.treeViewMaps.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeCollapse);
            this.treeViewMaps.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeExpand);
            this.treeViewMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeMouseDoubleClick);
            this.treeViewMaps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewMaps_MouseClick);
            // 
            // contextMenuInclude
            // 
            this.contextMenuInclude.ImageScalingSize = new System.Drawing.Size(24, 24);
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
            this.contextMenuOpen.ImageScalingSize = new System.Drawing.Size(24, 24);
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
            this.splitter1.Location = new System.Drawing.Point(167, 24);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 636);
            this.splitter1.TabIndex = 34;
            this.splitter1.TabStop = false;
            // 
            // multiPagePanel
            // 
            this.multiPagePanel.AutoSize = true;
            this.multiPagePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.multiPagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.multiPagePanel.CurrentPage = null;
            this.multiPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiPagePanel.Location = new System.Drawing.Point(168, 24);
            this.multiPagePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.multiPagePanel.Name = "multiPagePanel";
            this.multiPagePanel.Size = new System.Drawing.Size(1115, 636);
            this.multiPagePanel.TabIndex = 36;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1283, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // pbx1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 682);
            this.Controls.Add(this.multiPagePanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeViewMaps);
            this.Controls.Add(this.statusStrip1);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.ToolStripMenuItem loadMenu;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuMap;
        private System.Windows.Forms.ToolStripMenuItem renameMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteMenu;
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
     
        private MultiPagePanel multiPagePanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

