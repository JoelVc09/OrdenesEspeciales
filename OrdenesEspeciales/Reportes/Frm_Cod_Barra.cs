using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OrdenesEspeciales.Form_Orden;
using static OrdenesEspeciales.Login;

namespace OrdenesEspeciales.Reportes
{
    public partial class Frm_Cod_Barra : Form
    {
        public Frm_Cod_Barra()
        {
            InitializeComponent();
        }

        private void MostrarReporte()
        {
            // Creas una instancia del formulario de login si aún no tienes una
            Login loginForm = new Login();

            string despacho = DatosOrden.ValorOrden;
            string usuario = DatosUsuario.ValorUsuario;
            string contraseña = DatosContrasena.ValorContrasena;

            // Ejemplo: Mostrar los valores obtenidos en MessageBox (esto es solo para verificar)
            //MessageBox.Show(loginForm.getUser());
            //MessageBox.Show(loginForm.getPassword());   

            // Utilizas los valores en tu lógica para mostrar el reporte
            Cod_Barra reporte = new Cod_Barra();
            reporte.SetDatabaseLogon(usuario, contraseña);
            reporte.SetParameterValue("Dispatch_Cod", despacho);
            crVisorCodBarra.ReportSource = reporte;
            crVisorCodBarra.Show();

        }

        private void Frm_Cod_Barra_Load(object sender, EventArgs e)
        {
            this.MostrarReporte();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
