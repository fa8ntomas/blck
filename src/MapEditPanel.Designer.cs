using BLEditor.Controls;

namespace BLEditor
{
    partial class MapEditPanel
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
            this.firstMapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblSelected = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mapEditUserControl = new BLEditor.MapEditUserControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStamp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.runButton = new System.Windows.Forms.Button();
            this.labelStamp = new System.Windows.Forms.Label();
            this.charSetUserControl = new BLEditor.Controls.CharSetUserControl();
            this.dliList = new BLEditor.Controls.DLIListUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstMapNumericUpDown
            // 
            this.firstMapNumericUpDown.Location = new System.Drawing.Point(154, 726);
            this.firstMapNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.firstMapNumericUpDown.Name = "firstMapNumericUpDown";
            this.firstMapNumericUpDown.Size = new System.Drawing.Size(180, 26);
            this.firstMapNumericUpDown.TabIndex = 41;
            // 
            // lblSelected
            // 
            this.lblSelected.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelected.Location = new System.Drawing.Point(1106, 411);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(95, 196);
            this.lblSelected.TabIndex = 37;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mapEditUserControl);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(6, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1025, 682);
            this.panel1.TabIndex = 44;
            // 
            // mapEditUserControl
            // 
            this.mapEditUserControl.AllowDrop = true;
            this.mapEditUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditUserControl.Interation = BLEditor.MapEditUserControl.InterationType.PRESELECT;
            this.mapEditUserControl.Location = new System.Drawing.Point(0, 38);
            this.mapEditUserControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.mapEditUserControl.Name = "mapEditUserControl";
            this.mapEditUserControl.Size = new System.Drawing.Size(1023, 642);
            this.mapEditUserControl.TabIndex = 42;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStamp,
            this.toolStripButtonSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1023, 38);
            this.toolStrip1.TabIndex = 43;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonStamp
            // 
            this.toolStripButtonStamp.AutoSize = false;
            this.toolStripButtonStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStamp.Image = global::BLEditor.Properties.Resources.stamp;
            this.toolStripButtonStamp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStamp.Name = "toolStripButtonStamp";
            this.toolStripButtonStamp.Size = new System.Drawing.Size(34, 28);
            this.toolStripButtonStamp.Text = "toolStripButton1";
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.AutoSize = false;
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelect.Image = global::BLEditor.Properties.Resources.layer_select;
            this.toolStripButtonSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(34, 33);
            this.toolStripButtonSelect.Text = "toolStripButton2";
            // 
            // runButton
            // 
            this.runButton.Image = global::BLEditor.Properties.Resources.application_run;
            this.runButton.Location = new System.Drawing.Point(7, 726);
            this.runButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(139, 26);
            this.runButton.TabIndex = 40;
            this.runButton.Text = "Run";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // labelStamp
            // 
            this.labelStamp.AutoSize = true;
            this.labelStamp.Location = new System.Drawing.Point(1070, 340);
            this.labelStamp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStamp.Name = "labelStamp";
            this.labelStamp.Size = new System.Drawing.Size(51, 20);
            this.labelStamp.TabIndex = 45;
            this.labelStamp.Text = "label1";
            // 
            // charSetUserControl
            // 
            this.charSetUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charSetUserControl.DLIs = null;
            this.charSetUserControl.Drag = true;
            this.charSetUserControl.Location = new System.Drawing.Point(1318, 22);
            this.charSetUserControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.charSetUserControl.Name = "charSetUserControl";
            this.charSetUserControl.Size = new System.Drawing.Size(408, 682);
            this.charSetUserControl.TabIndex = 43;
            // 
            // dliList
            // 
            this.dliList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dliList.Location = new System.Drawing.Point(1042, 22);
            this.dliList.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dliList.Name = "dliList";
            this.dliList.Padding = new System.Windows.Forms.Padding(8);
            this.dliList.Size = new System.Drawing.Size(258, 270);
            this.dliList.TabIndex = 39;
            // 
            // MapEditPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelStamp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.charSetUserControl);
            this.Controls.Add(this.firstMapNumericUpDown);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.dliList);
            this.Controls.Add(this.lblSelected);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MapEditPanel";
            this.Size = new System.Drawing.Size(1748, 895);
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MapEditUserControl mapEditUserControl;
        private System.Windows.Forms.NumericUpDown firstMapNumericUpDown;
        private System.Windows.Forms.Button runButton;
        private DLIListUserControl dliList;
        private System.Windows.Forms.Label lblSelected;
        private CharSetUserControl charSetUserControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStamp;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.Label labelStamp;
    }
}
