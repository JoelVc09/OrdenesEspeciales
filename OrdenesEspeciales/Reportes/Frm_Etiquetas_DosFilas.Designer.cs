namespace OrdenesEspeciales.Reportes
{
    partial class Frm_Etiquetas_DosFilas
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
            this.crVisorEtiquetaDosFila = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crVisorEtiquetaDosFila
            // 
            this.crVisorEtiquetaDosFila.ActiveViewIndex = -1;
            this.crVisorEtiquetaDosFila.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crVisorEtiquetaDosFila.Cursor = System.Windows.Forms.Cursors.Default;
            this.crVisorEtiquetaDosFila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crVisorEtiquetaDosFila.Location = new System.Drawing.Point(0, 0);
            this.crVisorEtiquetaDosFila.Name = "crVisorEtiquetaDosFila";
            this.crVisorEtiquetaDosFila.Size = new System.Drawing.Size(800, 450);
            this.crVisorEtiquetaDosFila.TabIndex = 0;
            this.crVisorEtiquetaDosFila.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // Frm_Etiquetas_DosFilas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crVisorEtiquetaDosFila);
            this.Name = "Frm_Etiquetas_DosFilas";
            this.Text = "Frm_Etiquetas_DosFilas";
            this.Load += new System.EventHandler(this.Frm_Etiquetas_DosFilas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crVisorEtiquetaDosFila;
    }
}