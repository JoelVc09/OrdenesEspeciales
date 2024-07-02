using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Configuration;
using System.Runtime.InteropServices;
using static OrdenesEspeciales.Form_Orden;

namespace OrdenesEspeciales
{
    public partial class Login : Form

    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );




        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //this.AcceptButton = btnlogin_Click;

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text;
            string contraseña = txtpass.Text;

            DatosUsuario.ValorUsuario = usuario;
            DatosContrasena.ValorContrasena = contraseña;


            //MessageBox.Show($"Usuario: {usuario}\nContraseña: {contraseña}", "Valores Encapsulados");


            bool conexionExitosa = false;

            try
            {
                ConexionODBC.Conectar(usuario, contraseña);
                conexionExitosa = true;


            }
            catch (OdbcException ex)
            {
                // Ocurrió un error al establecer la conexión
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (conexionExitosa)
            {
                // La conexión se estableció correctamente
                //MessageBox.Show("Conexión exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el nuevo formulario
                Menu frm_menu = new Menu();
                frm_menu.Show();


                // Crear y mostrar el formulario Orden
                //Form_Orden form_orden = new Form_Orden();
                //form_orden.Usuario = Usuario;
                //form_orden.Contraseña = Contraseña;
                


                this.Hide(); // Opcional: Oculta el formulario de inicio de sesión

            }
            else
            {
                // Limpiar las cajas de texto de usuario y contraseña
                txtusuario.Text = string.Empty;
                txtpass.Text = string.Empty;

                // Establecer el enfoque en la caja de texto de usuario
                txtusuario.Focus();
            }
           
        }

        public String getUser()
        {
            return txtusuario.Text;
        }

        public String getPassword()
        {
            return txtpass.Text;
        }

        public static class DatosUsuario
        {
            public static string ValorUsuario { get; set; }
        }

        public static class DatosContrasena
        {
            public static string ValorContrasena { get; set; }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                string usuario = txtusuario.Text;
                string contraseña = txtpass.Text;

                DatosUsuario.ValorUsuario = usuario;
                DatosContrasena.ValorContrasena = contraseña;

                bool conexionExitosa = false;

                try
                {
                    ConexionODBC.Conectar(usuario, contraseña);
                    conexionExitosa = true;

                }
                catch (OdbcException)
                {
                    // Ocurrió un error al establecer la conexión
                    MessageBox.Show("Verificar Usuario o Contraseña: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (conexionExitosa)
                {
                    // La conexión se estableció correctamente
                    //MessageBox.Show("Conexión exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el nuevo formulario
                    Menu frm_menu = new Menu();
                    frm_menu.Show();
                    this.Hide(); // Opcional: Oculta el formulario de inicio de sesión
                }
                else
                {
                    // Limpiar las cajas de texto de usuario y contraseña
                    txtusuario.Text = string.Empty;
                    txtpass.Text = string.Empty;

                    // Establecer el enfoque en la caja de texto de usuario
                    txtusuario.Focus();
                }

            }
        }


        
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
