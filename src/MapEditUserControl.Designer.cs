namespace BLEditor
{
    partial class MapEditUserControl
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
            this.SuspendLayout();
            // 
            // MapEditUserControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MapEditUserControl";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MapEditUserControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MapEditUserControl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MapEditUserControl_DragOver);
            this.DragLeave += new System.EventHandler(this.MapEditUserControl_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapEditUserControl_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapEditUserControl_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapEditUserControl_MouseDown);
            this.MouseLeave += new System.EventHandler(this.MapEditUserControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapEditUserControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapEditUserControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
