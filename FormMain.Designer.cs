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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addANewMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addANewMapFromAnImageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addAnExistingMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.button2 = new System.Windows.Forms.Button();
            this.buttonEditFnt = new BLEditor.MenuButton();
            this.dliList = new BLEditor.DLIListUserControl();
            this.firstMapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.contextMenuMap.SuspendLayout();
            this.contextMenuFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flpTiles
            // 
            this.flpTiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTiles.Location = new System.Drawing.Point(949, 27);
            this.flpTiles.Name = "flpTiles";
            this.flpTiles.Size = new System.Drawing.Size(578, 275);
            this.flpTiles.TabIndex = 4;
            // 
            // lblSelected
            // 
            this.lblSelected.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelected.Location = new System.Drawing.Point(835, 238);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(64, 128);
            this.lblSelected.TabIndex = 5;
            // 
            // lblSelectName
            // 
            this.lblSelectName.AutoSize = true;
            this.lblSelectName.Location = new System.Drawing.Point(658, 239);
            this.lblSelectName.Name = "lblSelectName";
            this.lblSelectName.Size = new System.Drawing.Size(69, 13);
            this.lblSelectName.TabIndex = 6;
            this.lblSelectName.Text = "Selected Tile";
            // 
            // flpMap
            // 
            this.flpMap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flpMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMap.Location = new System.Drawing.Point(126, 28);
            this.flpMap.Name = "flpMap";
            this.flpMap.Size = new System.Drawing.Size(643, 353);
            this.flpMap.TabIndex = 7;
            // 
            // cbxDliDisplay
            // 
            this.cbxDliDisplay.AutoSize = true;
            this.cbxDliDisplay.Enabled = false;
            this.cbxDliDisplay.Location = new System.Drawing.Point(777, 209);
            this.cbxDliDisplay.Name = "cbxDliDisplay";
            this.cbxDliDisplay.Size = new System.Drawing.Size(87, 17);
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
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1540, 24);
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
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.addANewMapMenu,
            this.addANewMapFromAnImageMenu,
            this.addAnExistingMapMenu,
            this.toolStripSeparator2,
            this.settingsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newMenu
            // 
            this.newMenu.Name = "newMenu";
            this.newMenu.Size = new System.Drawing.Size(238, 22);
            this.newMenu.Text = "New";
            // 
            // loadMenu
            // 
            this.loadMenu.Name = "loadMenu";
            this.loadMenu.Size = new System.Drawing.Size(238, 22);
            this.loadMenu.Text = "Load";
            // 
            // saveMenu
            // 
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.Size = new System.Drawing.Size(238, 22);
            this.saveMenu.Text = "Save";
            // 
            // saveAsMenu
            // 
            this.saveAsMenu.Name = "saveAsMenu";
            this.saveAsMenu.Size = new System.Drawing.Size(238, 22);
            this.saveAsMenu.Text = "Save As";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(235, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.treeViewMaps.Location = new System.Drawing.Point(0, 28);
            this.treeViewMaps.Name = "treeViewMaps";
            this.treeViewMaps.Size = new System.Drawing.Size(121, 473);
            this.treeViewMaps.TabIndex = 27;
            this.treeViewMaps.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeCollapse);
            this.treeViewMaps.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMaps_BeforeExpand);
            this.treeViewMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewMaps_NodeMouseDoubleClick);
            this.treeViewMaps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewMaps_MouseClick);
            // 
            // button2
            // 
            this.button2.Image = global::BLEditor.Properties.Resources.application_run;
            this.button2.Location = new System.Drawing.Point(128, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Run";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.dliList.Location = new System.Drawing.Point(777, 27);
            this.dliList.Name = "dliList";
            this.dliList.Padding = new System.Windows.Forms.Padding(5);
            this.dliList.Size = new System.Drawing.Size(166, 176);
            this.dliList.TabIndex = 24;
            // 
            // firstMapNumericUpDown
            // 
            this.firstMapNumericUpDown.Location = new System.Drawing.Point(285, 391);
            this.firstMapNumericUpDown.Name = "firstMapNumericUpDown";
            this.firstMapNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.firstMapNumericUpDown.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "First Map:";
            // 
            // pbx1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 501);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstMapNumericUpDown);
            this.Controls.Add(this.button2);
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
            this.Name = "pbx1";
            this.Text = "Bruce Lee Level Editor";
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addANewMapMenu;
        private System.Windows.Forms.ToolStripMenuItem addANewMapFromAnImageMenu;
        private System.Windows.Forms.ToolStripMenuItem addAnExistingMapMenu;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyCharMenu;
        private System.Windows.Forms.ToolStripMenuItem importFromBitmapMenu;
        private System.Windows.Forms.NumericUpDown firstMapNumericUpDown;
        private System.Windows.Forms.Label label1;
    }
}

