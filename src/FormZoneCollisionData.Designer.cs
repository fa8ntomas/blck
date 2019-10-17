namespace BLEditor
{
    partial class FormZoneCollisionData
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
            this.radioButtonAlways = new System.Windows.Forms.RadioButton();
            this.radioButtonFlag0 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButtonFlag1 = new System.Windows.Forms.RadioButton();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xxx = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownW = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonAlways
            // 
            this.radioButtonAlways.AutoSize = true;
            this.radioButtonAlways.Checked = true;
            this.radioButtonAlways.Location = new System.Drawing.Point(26, 116);
            this.radioButtonAlways.Name = "radioButtonAlways";
            this.radioButtonAlways.Size = new System.Drawing.Size(58, 17);
            this.radioButtonAlways.TabIndex = 0;
            this.radioButtonAlways.TabStop = true;
            this.radioButtonAlways.Text = "Always";
            this.radioButtonAlways.UseVisualStyleBackColor = true;
            // 
            // radioButtonFlag0
            // 
            this.radioButtonFlag0.AutoSize = true;
            this.radioButtonFlag0.Location = new System.Drawing.Point(26, 135);
            this.radioButtonFlag0.Name = "radioButtonFlag0";
            this.radioButtonFlag0.Size = new System.Drawing.Size(118, 17);
            this.radioButtonFlag0.TabIndex = 1;
            this.radioButtonFlag0.Text = "If Map Flag 0 is true";
            this.radioButtonFlag0.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(11, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // radioButtonFlag1
            // 
            this.radioButtonFlag1.AutoSize = true;
            this.radioButtonFlag1.Location = new System.Drawing.Point(26, 154);
            this.radioButtonFlag1.Name = "radioButtonFlag1";
            this.radioButtonFlag1.Size = new System.Drawing.Size(118, 17);
            this.radioButtonFlag1.TabIndex = 13;
            this.radioButtonFlag1.Text = "If Map Flag 1 is true";
            this.radioButtonFlag1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(28, 22);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownX.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xxx);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownH);
            this.groupBox1.Controls.Add(this.numericUpDownW);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownY);
            this.groupBox1.Controls.Add(this.numericUpDownX);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(223, 82);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coordinates";
            // 
            // xxx
            // 
            this.xxx.AutoSize = true;
            this.xxx.Location = new System.Drawing.Point(121, 51);
            this.xxx.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.xxx.Name = "xxx";
            this.xxx.Size = new System.Drawing.Size(18, 13);
            this.xxx.TabIndex = 21;
            this.xxx.Text = "H:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "W:";
            // 
            // numericUpDownH
            // 
            this.numericUpDownH.Location = new System.Drawing.Point(143, 49);
            this.numericUpDownH.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownH.TabIndex = 19;
            // 
            // numericUpDownW
            // 
            this.numericUpDownW.Location = new System.Drawing.Point(28, 49);
            this.numericUpDownW.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownW.Name = "numericUpDownW";
            this.numericUpDownW.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownW.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "X:";
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(143, 22);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownY.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(9, 94);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(223, 92);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active";
            // 
            // FormZoneCollisionData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 221);
            this.Controls.Add(this.radioButtonFlag1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButtonFlag0);
            this.Controls.Add(this.radioButtonAlways);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormZoneCollisionData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Data";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonAlways;
        private System.Windows.Forms.RadioButton radioButtonFlag0;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButtonFlag1;
        public System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label xxx;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDownH;
        public System.Windows.Forms.NumericUpDown numericUpDownW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownY;
    }
}