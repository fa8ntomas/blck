namespace BLEditor
{
    partial class FormBitmapToMap
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonReserve0 = new System.Windows.Forms.RadioButton();
            this.radioButtonReserve5 = new System.Windows.Forms.RadioButton();
            this.radioButtonReserve10 = new System.Windows.Forms.RadioButton();
            this.pictureGrid1 = new BLEditor.PictureGrid();
            this.fiveColorPickerUserControl1 = new BLEditor.ColorsPickerUserControl();
            this.radioButtonReserve15 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pictureGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1006, 506);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 469);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(596, 34);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 26);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.fiveColorPickerUserControl1);
            this.flowLayoutPanel2.Controls.Add(this.panel1);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(605, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(398, 460);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonReserve15);
            this.panel1.Controls.Add(this.radioButtonReserve10);
            this.panel1.Controls.Add(this.radioButtonReserve5);
            this.panel1.Controls.Add(this.radioButtonReserve0);
            this.panel1.Location = new System.Drawing.Point(3, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 100);
            this.panel1.TabIndex = 2;
            // 
            // radioButtonReserve0
            // 
            this.radioButtonReserve0.AutoSize = true;
            this.radioButtonReserve0.Checked = true;
            this.radioButtonReserve0.Location = new System.Drawing.Point(3, 3);
            this.radioButtonReserve0.Name = "radioButtonReserve0";
            this.radioButtonReserve0.Size = new System.Drawing.Size(102, 17);
            this.radioButtonReserve0.TabIndex = 0;
            this.radioButtonReserve0.TabStop = true;
            this.radioButtonReserve0.Text = "Reserve No Cell";
            this.radioButtonReserve0.UseVisualStyleBackColor = true;
            // 
            // radioButtonReserve5
            // 
            this.radioButtonReserve5.AutoSize = true;
            this.radioButtonReserve5.Location = new System.Drawing.Point(3, 26);
            this.radioButtonReserve5.Name = "radioButtonReserve5";
            this.radioButtonReserve5.Size = new System.Drawing.Size(99, 17);
            this.radioButtonReserve5.TabIndex = 1;
            this.radioButtonReserve5.Text = "Reserve 5 Cells";
            this.radioButtonReserve5.UseVisualStyleBackColor = true;
            // 
            // radioButtonReserve10
            // 
            this.radioButtonReserve10.AutoSize = true;
            this.radioButtonReserve10.Location = new System.Drawing.Point(3, 49);
            this.radioButtonReserve10.Name = "radioButtonReserve10";
            this.radioButtonReserve10.Size = new System.Drawing.Size(105, 17);
            this.radioButtonReserve10.TabIndex = 2;
            this.radioButtonReserve10.Text = "Reserve 10 Cells";
            this.radioButtonReserve10.UseVisualStyleBackColor = true;
            // 
            // pictureGrid1
            // 
            this.pictureGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureGrid1.Location = new System.Drawing.Point(3, 3);
            this.pictureGrid1.Name = "pictureGrid1";
            this.pictureGrid1.Size = new System.Drawing.Size(596, 460);
            this.pictureGrid1.TabIndex = 0;
            // 
            // fiveColorPickerUserControl1
            // 
            this.fiveColorPickerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fiveColorPickerUserControl1.Location = new System.Drawing.Point(3, 30);
            this.fiveColorPickerUserControl1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.fiveColorPickerUserControl1.MinimumSize = new System.Drawing.Size(100, 100);
            this.fiveColorPickerUserControl1.Name = "fiveColorPickerUserControl1";
            this.fiveColorPickerUserControl1.Size = new System.Drawing.Size(395, 197);
            this.fiveColorPickerUserControl1.TabIndex = 1;
            // 
            // radioButtonReserve15
            // 
            this.radioButtonReserve15.AutoSize = true;
            this.radioButtonReserve15.Location = new System.Drawing.Point(3, 72);
            this.radioButtonReserve15.Name = "radioButtonReserve15";
            this.radioButtonReserve15.Size = new System.Drawing.Size(105, 17);
            this.radioButtonReserve15.TabIndex = 3;
            this.radioButtonReserve15.Text = "Reserve 15 Cells";
            this.radioButtonReserve15.UseVisualStyleBackColor = true;
            // 
            // FormBitmapToMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormBitmapToMap";
            this.Text = "Bitmap To Map";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PictureGrid pictureGrid1;
        private ColorsPickerUserControl fiveColorPickerUserControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonReserve0;
        private System.Windows.Forms.RadioButton radioButtonReserve10;
        private System.Windows.Forms.RadioButton radioButtonReserve5;
        private System.Windows.Forms.RadioButton radioButtonReserve15;
        // private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}