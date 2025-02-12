﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Runtime.InteropServices;
using OrdenesEspeciales.Reportes;
using static OrdenesEspeciales.Login;
using System.IO;    
using System.Text.Json;



namespace OrdenesEspeciales
{
    public partial class Menu : Form
    {


        public Menu()
        {


            InitializeComponent();




            //BtnReassay.ForeColor = Color.Black;

            // Personalizar la fuente del texto del botón
            //BtnReassay.Font = new Font("Arial", 10, FontStyle.Bold);

            // Personalizar el estilo del botón
            //BtnReassay.FlatStyle = FlatStyle.Flat;
            //BtnReassay.FlatAppearance.BorderSize = 1;
            //// BtnReassay.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 96, 96);
            //BtnReassay.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 128, 128);
            // BtnReassay.FlatAppearance.BorderColor = Color.White;


            //BtnCompositos.BackColor = Color.FromArgb(64, 64, 64);

            // Personalizar el color del texto del botón
            //BtnCompositos1.ForeColor = Color.Black;

            // Personalizar la fuente del texto del botón
            //BtnCompositos1.Font = new Font("Arial", 10, FontStyle.Bold);

            // Personalizar el estilo del botón
            //BtnCompositos.FlatStyle = FlatStyle.Flat;
            //BtnCompositos.FlatAppearance.BorderSize = 0;
            //BtnCompositos1.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 96, 96);
            //BtnCompositos1.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 128, 128);
            //BtnCompositos1.FlatStyle = FlatStyle.Flat;
            //BtnCompositos1.FlatAppearance.BorderSize = 2;
            //BtnCompositos1.FlatAppearance.BorderColor = Color.White;

            string usuario = DatosUsuario.ValorUsuario;
            //Form_Orden frm4 = new Form_Orden();
            //frm4.ShowDialog();
            //this.Hide();
            if (usuario == "admin")
            {
                btn_admin.Visible = true;
            }
            else
            {
                btn_admin.Visible = false;
            }
        }

        private bool confirmacionCierre = false;
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!confirmacionCierre)
            {
                // Mostrar el cuadro de diálogo de confirmación solo una vez
                DialogResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar el formulario?", "Confirmar cierre", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancelar el cierre del formulario
                }
                else
                {
                    ConexionODBC.CerrarConexion();
                    confirmacionCierre = true; // Indicar que ya se mostró la confirmación de cierre
                    Application.Exit(); // Finalizar la aplicaciónad
                }
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        //----

        private List<string> CargarUsuariosDesdeJSON(string path)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(fullPath))
            {
                try
                {
                    return JsonSerializer.Deserialize<List<string>>(File.ReadAllText(fullPath));
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"Error al leer el archivo JSON: {ex.Message}");
                }
            }
            return new List<string>();
        }

        private void BtnCompositos_Click_1(object sender, EventArgs e)
        {
            var usuariosPermitidos = CargarUsuariosDesdeJSON("usuariosCompositos.json");
            string usuario = DatosUsuario.ValorUsuario;

            if (usuariosPermitidos.Contains(usuario))
            {
                AbrirpanelHija(new form_Compo(Nom_Usua.Text));
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a esta opción.");
            }
        }


        private void Btn_Envio_Ordenes_Click_1(object sender, EventArgs e)
        {
            var usuariosPermitidos = CargarUsuariosDesdeJSON("usuariosOrdenAnalisis.json");
            string usuario = DatosUsuario.ValorUsuario;

            if (usuariosPermitidos.Contains(usuario))
            {
                submenu.Visible = true;
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a esta opción.");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var usuariosPermitidos = CargarUsuariosDesdeJSON("usuariosOrdenesespeciales.json");
            string usuario = DatosUsuario.ValorUsuario;

            if (usuariosPermitidos.Contains(usuario))
            {
                AbrirpanelHija(new Form2cs(Nom_Usua.Text));
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a esta opción.");
            }
        }




        // MOVER FORMULARIO 

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWind, int wMsg, int wParam, int lParam);

        private void barratitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }



        private void AbrirpanelHija(object formhija)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill; // Activar AutoSize para que el formulario hijo ajuste su tamaño automáticamente
            //fh.AutoSizeMode = AutoSizeMode.GrowAndShrink; // Ajustar AutoSizeMode según tus necesidades
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }


        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void btnBlastHole_Click(object sender, EventArgs e)
        {
            AbrirpanelHija(new Form_Orden());

        }

        private void btnEspeciales_Click(object sender, EventArgs e)
        {
            //AbrirpanelHija(new Form_Orden_Especial());
        }

        private void btnHumedad_Click(object sender, EventArgs e)
        {
            //AbrirpanelHija(new Form_Orden_Humedad());

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            //panelContenedor.Visible = true;
            Menu frm_menu = new Menu();
            frm_menu.Show();
            this.Hide(); // Opcional: Oculta el formulario de inicio de sesión
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {

            this.Close();
            // Crear una nueva instancia del formulario Login
            //Login loginForm = new Login();

            // Mostrar el formulario Login
            //loginForm.Show();
            Application.Restart();
            // Cerrar el formulario Menu actual


        }

        public void ClickBlastHoleButton()
        {
            btnBlastHole.PerformClick();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AbrirpanelHija(new PanelAdmin());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
