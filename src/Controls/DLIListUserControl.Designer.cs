namespace BLEditor.Controls
{
    partial class DLIListUserControl
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
            this.dlis = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonNew2 = new System.Windows.Forms.ToolStripButton();
            this.buttonRemove2 = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlis
            // 
            this.dlis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlis.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.dlis.FormattingEnabled = true;
            this.dlis.IntegralHeight = false;
            this.dlis.Location = new System.Drawing.Point(5, 25);
            this.dlis.Margin = new System.Windows.Forms.Padding(0);
            this.dlis.Name = "dlis";
            this.dlis.ScrollAlwaysVisible = true;
            this.dlis.Size = new System.Drawing.Size(231, 251);
            this.dlis.TabIndex = 0;
            this.dlis.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.dlis_DrawItem);
            this.dlis.SelectedIndexChanged += new System.EventHandler(this.dlis_SelectedIndexChanged);
            this.dlis.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dlis_MouseDoubleClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 422);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(352, 1);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNew2,
            this.buttonRemove2,
            this.buttonEdit2});
            this.toolStrip1.Location = new System.Drawing.Point(5, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(231, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonNew2
            // 
            this.buttonNew2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNew2.Image = global::BLEditor.Properties.Resources.palette__plus;
            this.buttonNew2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNew2.Name = "buttonNew2";
            this.buttonNew2.Size = new System.Drawing.Size(23, 22);
            this.buttonNew2.Text = "toolStripButton1";
            // 
            // buttonRemove2
            // 
            this.buttonRemove2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRemove2.Image = global::BLEditor.Properties.Resources.palette__minus;
            this.buttonRemove2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemove2.Name = "buttonRemove2";
            this.buttonRemove2.Size = new System.Drawing.Size(23, 22);
            this.buttonRemove2.Text = "toolStripButton1";
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEdit2.Image = global::BLEditor.Properties.Resources.palette__arrow;
            this.buttonEdit2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Size = new System.Drawing.Size(23, 22);
            this.buttonEdit2.Text = "buttonEdit2";
            // 
            // DLIListUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.dlis);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "DLIListUserControl";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.Size = new System.Drawing.Size(241, 281);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

      //  private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox dlis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonNew2;
        private System.Windows.Forms.ToolStripButton buttonRemove2;
        private System.Windows.Forms.ToolStripButton buttonEdit2;
    }
}
