using System;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.xml;
using static OrdenesEspeciales.Form_Orden;
using static OrdenesEspeciales.Login;


namespace OrdenesEspeciales.Reportes
{
    public partial class FrmVisorReporte : Form
    {

        public FrmVisorReporte()
        {
            InitializeComponent();
            
        }

        public void MostrarReporte()
        {
            // Creas una instancia del formulario de login si aún no tienes una
            Login loginForm = new Login();
           // Form_Orden form_Orden = new Form_Orden();

            string despacho = DatosOrden.ValorOrden;
            string usuario = DatosUsuario.ValorUsuario;
            string contraseña = DatosContrasena.ValorContrasena;
            // Ejemplo: Mostrar los valores obtenidos en MessageBox (esto es solo para verificar)
            //MessageBox.Show(loginForm.getUser());
            //MessageBox.Show(usuario);
            //MessageBox.Show(contraseña);
            //MessageBox.Show(despacho);

            // Utilizas los valores en tu lógica para mostrar el reporte
            Reporte_Análisis reporte = new Reporte_Análisis();
            reporte.SetDatabaseLogon(usuario, contraseña);
            reporte.SetParameterValue("Despacho",despacho);
            crVisorReporte.ReportSource = reporte;
            crVisorReporte.Show();

            // Configuras las opciones de exportación
            /*ExportOptions exportOpts = new ExportOptions();
            DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pdfOpts = new PdfRtfWordFormatOptions();

            // Especificas la ruta y el nombre del archivo PDF @"C:\Nueva carpeta\Report"
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string pdfPath = @"C:\Nueva carpeta\Report" + timestamp + ".pdf"; // Cambia la ruta según tus necesidades
            diskOpts.DiskFileName = pdfPath;
            exportOpts = reporte.ExportOptions;
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
            exportOpts.DestinationOptions = diskOpts;
            exportOpts.FormatOptions = pdfOpts;

            // Exportas el reporte a PDF
            try
            {
                reporte.Export();
                MessageBox.Show("Reporte exportado exitosamente a PDF.");
                Process.Start(pdfPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el reporte: " + ex.Message);
            }*/



        }


        private void FrmVisorReporte_Load_1(object sender, EventArgs e)
        {
            this.MostrarReporte();
        }

        private void crVisorReporte_Load(object sender, EventArgs e)
        {

        }
    }
}