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