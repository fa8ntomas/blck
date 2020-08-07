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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStamp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.runButton = new System.Windows.Forms.Button();
            this.labelStamp = new System.Windows.Forms.Label();
            this.mapEditUserControl = new BLEditor.MapEditUserControl();
            this.charSetUserControl1 = new BLEditor.Controls.CharSetUserControl();
            this.dliList = new BLEditor.Controls.DLIListUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstMapNumericUpDown
            // 
            this.firstMapNumericUpDown.Location = new System.Drawing.Point(151, 376);
            this.firstMapNumericUpDown.Name = "firstMapNumericUpDown";
            this.firstMapNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.firstMapNumericUpDown.TabIndex = 41;
            // 
            // lblSelected
            // 
            this.lblSelected.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelected.Location = new System.Drawing.Point(737, 267);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(64, 128);
            this.lblSelected.TabIndex = 37;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mapEditUserControl);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(4, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 444);
            this.panel1.TabIndex = 44;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStamp,
            this.toolStripButtonSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(682, 33);
            this.toolStrip1.TabIndex = 43;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonStamp
            // 
            this.toolStripButtonStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStamp.Image = global::BLEditor.Properties.Resources.stamp;
            this.toolStripButtonStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStamp.Name = "toolStripButtonStamp";
            this.toolStripButtonStamp.Size = new System.Drawing.Size(34, 28);
            this.toolStripButtonStamp.Text = "toolStripButton1";
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelect.Image = global::BLEditor.Properties.Resources.layer_select;
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(34, 28);
            this.toolStripButtonSelect.Text = "toolStripButton2";
            // 
            // runButton
            // 
            this.runButton.Image = global::BLEditor.Properties.Resources.application_run;
            this.runButton.Location = new System.Drawing.Point(11, 388);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 10);
            this.runButton.TabIndex = 40;
            this.runButton.Text = "Run";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // labelStamp
            // 
            this.labelStamp.AutoSize = true;
            this.labelStamp.Location = new System.Drawing.Point(713, 221);
            this.labelStamp.Name = "labelStamp";
            this.labelStamp.Size = new System.Drawing.Size(35, 13);
            this.labelStamp.TabIndex = 45;
            this.labelStamp.Text = "label1";
            // 
            // mapEditUserControl
            // 
            this.mapEditUserControl.AllowDrop = true;
            this.mapEditUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditUserControl.Interation = BLEditor.MapEditUserControl.InterationType.PRESELECT;
            this.mapEditUserControl.Location = new System.Drawing.Point(0, 33);
            this.mapEditUserControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mapEditUserControl.Name = "mapEditUserControl";
            this.mapEditUserControl.Size = new System.Drawing.Size(682, 409);
            this.mapEditUserControl.TabIndex = 42;
            // 
            // charSetUserControl1
            // 
            this.charSetUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.charSetUserControl1.DLIs = null;
            this.charSetUserControl1.Drag = true;
            this.charSetUserControl1.Location = new System.Drawing.Point(879, 14);
            this.charSetUserControl1.Name = "charSetUserControl1";
            this.charSetUserControl1.Size = new System.Drawing.Size(273, 444);
            this.charSetUserControl1.TabIndex = 43;
            // 
            // dliList
            // 
            this.dliList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dliList.Location = new System.Drawing.Point(695, 14);
            this.dliList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dliList.Name = "dliList";
            this.dliList.Padding = new System.Windows.Forms.Padding(5);
            this.dliList.Size = new System.Drawing.Size(173, 176);
            this.dliList.TabIndex = 39;
            // 
            // MapEditPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelStamp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.charSetUserControl1);
            this.Controls.Add(this.firstMapNumericUpDown);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.dliList);
            this.Controls.Add(this.lblSelected);
            this.Name = "MapEditPanel";
            this.Size = new System.Drawing.Size(1165, 474);
            ((System.ComponentModel.ISupportInitialize)(this.firstMapNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private CharSetUserControl charSetUserControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStamp;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.Label labelStamp;
    }
}
