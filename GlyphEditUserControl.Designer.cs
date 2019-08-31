namespace BLEditor
{
    partial class GlyphEditUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxColors = new System.Windows.Forms.ToolStripComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelDraw = new System.Windows.Forms.Panel();
            this.radioButtonColbk = new System.Windows.Forms.RadioButton();
            this.radioButtonColpf0 = new System.Windows.Forms.RadioButton();
            this.radioButtonColpf1 = new System.Windows.Forms.RadioButton();
            this.radioButtonColpf2 = new System.Windows.Forms.RadioButton();
            this.radioButtonColpf3 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripButtonHigh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLow = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelDraw.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 242);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonHigh,
            this.toolStripButtonLow,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripComboBoxColors});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(260, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBoxColors
            // 
            this.toolStripComboBoxColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxColors.Name = "toolStripComboBoxColors";
            this.toolStripComboBoxColors.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxColors.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxColors_SelectedIndexChanged);
            // 
            // panelDraw
            // 
            this.panelDraw.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelDraw.Controls.Add(this.radioButtonColpf3);
            this.panelDraw.Controls.Add(this.radioButtonColpf2);
            this.panelDraw.Controls.Add(this.radioButtonColpf1);
            this.panelDraw.Controls.Add(this.radioButtonColpf0);
            this.panelDraw.Controls.Add(this.radioButtonColbk);
            this.panelDraw.Controls.Add(this.pictureBox1);
            this.panelDraw.Location = new System.Drawing.Point(47, 20);
            this.panelDraw.Name = "panelDraw";
            this.panelDraw.Size = new System.Drawing.Size(161, 166);
            this.panelDraw.TabIndex = 0;
            // 
            // radioButtonColbk
            // 
            this.radioButtonColbk.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonColbk.Location = new System.Drawing.Point(1, 146);
            this.radioButtonColbk.Name = "radioButtonColbk";
            this.radioButtonColbk.Size = new System.Drawing.Size(20, 20);
            this.radioButtonColbk.TabIndex = 1;
            this.radioButtonColbk.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonColbk, "COLBK");
            this.radioButtonColbk.UseVisualStyleBackColor = true;
            // 
            // radioButtonColpf0
            // 
            this.radioButtonColpf0.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonColpf0.Location = new System.Drawing.Point(36, 146);
            this.radioButtonColpf0.Name = "radioButtonColpf0";
            this.radioButtonColpf0.Size = new System.Drawing.Size(20, 20);
            this.radioButtonColpf0.TabIndex = 2;
            this.radioButtonColpf0.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonColpf0, "COLPF0 [floor]");
            this.radioButtonColpf0.UseVisualStyleBackColor = true;
            // 
            // radioButtonColpf1
            // 
            this.radioButtonColpf1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonColpf1.Location = new System.Drawing.Point(71, 146);
            this.radioButtonColpf1.Name = "radioButtonColpf1";
            this.radioButtonColpf1.Size = new System.Drawing.Size(20, 20);
            this.radioButtonColpf1.TabIndex = 3;
            this.radioButtonColpf1.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonColpf1, "COLPF1");
            this.radioButtonColpf1.UseVisualStyleBackColor = true;
            // 
            // radioButtonColpf2
            // 
            this.radioButtonColpf2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonColpf2.Location = new System.Drawing.Point(106, 146);
            this.radioButtonColpf2.Name = "radioButtonColpf2";
            this.radioButtonColpf2.Size = new System.Drawing.Size(20, 20);
            this.radioButtonColpf2.TabIndex = 4;
            this.radioButtonColpf2.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonColpf2, "COLPF2 [ladder]");
            this.radioButtonColpf2.UseVisualStyleBackColor = true;
            // 
            // radioButtonColpf3
            // 
            this.radioButtonColpf3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonColpf3.Location = new System.Drawing.Point(141, 146);
            this.radioButtonColpf3.Name = "radioButtonColpf3";
            this.radioButtonColpf3.Size = new System.Drawing.Size(20, 20);
            this.radioButtonColpf3.TabIndex = 5;
            this.radioButtonColpf3.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonColpf3, "COLPF3 [obstacle,black]");
            this.radioButtonColpf3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panelDraw);
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 211);
            this.panel1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Colors:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Location = new System.Drawing.Point(48, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 137);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint_1);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // toolStripButtonHigh
            // 
            this.toolStripButtonHigh.CheckOnClick = true;
            this.toolStripButtonHigh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHigh.Image = global::BLEditor.Properties.Resources.application_tile_horizontal_left;
            this.toolStripButtonHigh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHigh.Name = "toolStripButtonHigh";
            this.toolStripButtonHigh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHigh.Text = "toolStripButton4";
            this.toolStripButtonHigh.ToolTipText = "High Char Display [COLPF3 instead of COLPF2]";
            // 
            // toolStripButtonLow
            // 
            this.toolStripButtonLow.CheckOnClick = true;
            this.toolStripButtonLow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLow.Image = global::BLEditor.Properties.Resources.application_tile_horizontal_right;
            this.toolStripButtonLow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLow.Name = "toolStripButtonLow";
            this.toolStripButtonLow.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLow.Text = "toolStripButton5";
            this.toolStripButtonLow.ToolTipText = "Low Char Display [COLPF2 instead of COLPF3]";
            // 
            // GlyphEditUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GlyphEditUserControl";
            this.Size = new System.Drawing.Size(260, 242);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelDraw.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxColors;
        private System.Windows.Forms.ToolStripButton toolStripButtonLow;
        private System.Windows.Forms.ToolStripButton toolStripButtonHigh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelDraw;
        private System.Windows.Forms.RadioButton radioButtonColpf3;
        private System.Windows.Forms.RadioButton radioButtonColpf2;
        private System.Windows.Forms.RadioButton radioButtonColpf1;
        private System.Windows.Forms.RadioButton radioButtonColpf0;
        private System.Windows.Forms.RadioButton radioButtonColbk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
