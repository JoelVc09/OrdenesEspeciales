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
        }
        
        private void btnlogin_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text;
            string contraseña = txtpass.Text;
            ConexionODBC.Conectar(usuario, contraseña);

            try
            {
                //Form2cs frm2 = new Form2cs(txtusuario.Text);
                Menu frm_menu = new Menu(txtusuario.Text);
                //var conn = new OdbcConnection();
                //conn.ConnectionString =
                //              "Dsn=CENTRAL;" +
                //              "Uid=" + txtusuario.Text + ";" +
                //              "Pwd=" + txtpass.Text + ";";
                //conn.Open();
                //string cnn = ConfigurationManager.ConnectionStrings["prueba"].ConnectionString;
                using (OdbcConnection connection = ConexionODBC.connection)
                {   
                    
                    using (OdbcCommand cmd = new OdbcCommand("select a.userid from pfuser as a inner join pfuser_prof as b on a.userid = b.userid where a.userid='" + txtusuario.Text + "' and b.[profile] in ('QC MANAGER')", connection))
                    {
                        OdbcDataReader dr = cmd.ExecuteReader();


                        if (dr.Read())
                        {
                            DialogResult r = MessageBox.Show("Inicio de Sesión con el Usuario:  " + txtusuario.Text, "Confirmar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            //frm2.ShowDialog();
                            frm_menu.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                DialogResult r2 = MessageBox.Show("Error al eliminar el Dispatch: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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
                ConexionODBC.Conectar(usuario, contraseña);

                try
                {
                    //Form2cs frm2 = new Form2cs(txtusuario.Text);
                    Menu frm_menu = new Menu(txtusuario.Text);
                    //var conn = new OdbcConnection();
                    //conn.ConnectionString =
                    //              "Dsn=CENTRAL;" +
                    //              "Uid=" + txtusuario.Text + ";" +
                    //              "Pwd=" + txtpass.Text + ";";
                    //conn.Open();
                    //string cnn = ConfigurationManager.ConnectionStrings["prueba"].ConnectionString;
                    using (OdbcConnection connection = ConexionODBC.connection)
                    {   //select a.userid from pfuser as a inner join pfuser_prof as b on a.userid = b.userid where a.userid='"+ txtusuario.Text +"' and b.[profile] in ('QC MANAGER')
                        //"select userid from pfuser where userid='" + txtusuario.Text + "'"
                        //using (OdbcCommand cmd = new OdbcCommand("select userid from pfuser"))
                        using (OdbcCommand cmd = new OdbcCommand("select a.userid from pfuser as a inner join pfuser_prof as b on a.userid = b.userid where a.userid='" + txtusuario.Text + "' and b.[profile] in ('QC MANAGER')", connection))
                        {
                            OdbcDataReader dr = cmd.ExecuteReader();


                            if (dr.Read())
                            {
                              DialogResult r =  MessageBox.Show("Inicio de Sesión con el Usuario:  " + txtusuario.Text, "Confirmar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                //frm2.ShowDialog();
                                frm_menu.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("");
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    DialogResult r2 = MessageBox.Show("Error al eliminar el Dispatch: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

     
    }
}
