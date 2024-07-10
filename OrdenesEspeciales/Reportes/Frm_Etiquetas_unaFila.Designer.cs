namespace OrdenesEspeciales.Reportes
{
    partial class Frm_Etiquetas_unaFila
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
            this.crVisorEtiquetaUnaFila = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crVisorEtiquetaUnaFila
            // 
            this.crVisorEtiquetaUnaFila.ActiveViewIndex = -1;
            this.crVisorEtiquetaUnaFila.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crVisorEtiquetaUnaFila.Cursor = System.Windows.Forms.Cursors.Default;
            this.crVisorEtiquetaUnaFila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crVisorEtiquetaUnaFila.Location = new System.Drawing.Point(0, 0);
            this.crVisorEtiquetaUnaFila.Name = "crVisorEtiquetaUnaFila";
            this.crVisorEtiquetaUnaFila.Size = new System.Drawing.Size(1340, 809);
            this.crVisorEtiquetaUnaFila.TabIndex = 0;
            this.crVisorEtiquetaUnaFila.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // Frm_Etiquetas_unaFila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 809);
            this.Controls.Add(this.crVisorEtiquetaUnaFila);
            this.Name = "Frm_Etiquetas_unaFila";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Frm_Etiquetas_unaFila_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crVisorEtiquetaUnaFila;
    }
}