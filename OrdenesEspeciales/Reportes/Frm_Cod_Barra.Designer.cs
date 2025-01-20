namespace OrdenesEspeciales.Reportes
{
    partial class Frm_Cod_Barra
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
            this.crVisorCodBarra = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.Cod_Barra1 = new OrdenesEspeciales.Reportes.Cod_Barra();
            this.Etiqueta_Preparación1 = new OrdenesEspeciales.Reportes.Etiqueta_Preparación();
            this.SuspendLayout();
            // 
            // crVisorCodBarra
            // 
            this.crVisorCodBarra.ActiveViewIndex = 0;
            this.crVisorCodBarra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crVisorCodBarra.Cursor = System.Windows.Forms.Cursors.Default;
            this.crVisorCodBarra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crVisorCodBarra.Location = new System.Drawing.Point(0, 0);
            this.crVisorCodBarra.Name = "crVisorCodBarra";
            this.crVisorCodBarra.ReportSource = this.Etiqueta_Preparación1;
            this.crVisorCodBarra.Size = new System.Drawing.Size(1362, 817);
            this.crVisorCodBarra.TabIndex = 0;
            this.crVisorCodBarra.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crVisorCodBarra.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // Frm_Cod_Barra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 817);
            this.Controls.Add(this.crVisorCodBarra);
            this.Name = "Frm_Cod_Barra";
            this.Text = "Frm_Cod_Barra";
            this.Load += new System.EventHandler(this.Frm_Cod_Barra_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crVisorCodBarra;
        private Cod_Barra Cod_Barra1;
        private Etiqueta_Preparación Etiqueta_Preparación1;
    }
}