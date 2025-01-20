namespace OrdenesEspeciales.Reportes
{
    partial class FrmVisorReporte
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
            this.crVisorReporte = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.Reporte_Análisis1 = new OrdenesEspeciales.Reportes.Reporte_Análisis();
            this.SuspendLayout();
            // 
            // crVisorReporte
            // 
            this.crVisorReporte.ActiveViewIndex = 0;
            this.crVisorReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crVisorReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.crVisorReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crVisorReporte.Location = new System.Drawing.Point(0, 0);
            this.crVisorReporte.Name = "crVisorReporte";
            this.crVisorReporte.ReportSource = this.Reporte_Análisis1;
            this.crVisorReporte.Size = new System.Drawing.Size(1342, 851);
            this.crVisorReporte.TabIndex = 0;
            this.crVisorReporte.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crVisorReporte.Load += new System.EventHandler(this.crVisorReporte_Load);
            // 
            // FrmVisorReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 851);
            this.Controls.Add(this.crVisorReporte);
            this.Name = "FrmVisorReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor Reporte de Análisis";
            this.Load += new System.EventHandler(this.FrmVisorReporte_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crVisorReporte;
        private Reporte_Análisis Reporte_Análisis1;
    }
}