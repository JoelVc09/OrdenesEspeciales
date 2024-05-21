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

namespace OrdenesEspeciales
{
    public partial class Menu : Form
    {
        public Menu()
        {
            

            InitializeComponent();
            
            BtnReassay.ForeColor = Color.Black;

            // Personalizar la fuente del texto del botón
            BtnReassay.Font = new Font("Arial", 10, FontStyle.Bold);

            // Personalizar el estilo del botón
            BtnReassay.FlatStyle = FlatStyle.Flat;
            BtnReassay.FlatAppearance.BorderSize = 1;
            BtnReassay.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 96, 96);
            BtnReassay.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 128, 128);
            BtnReassay.FlatAppearance.BorderColor = Color.White;


            //BtnCompositos.BackColor = Color.FromArgb(64, 64, 64);

            // Personalizar el color del texto del botón
            BtnCompositos.ForeColor = Color.Black;
        
            // Personalizar la fuente del texto del botón
            BtnCompositos.Font = new Font("Arial", 10, FontStyle.Bold);

            // Personalizar el estilo del botón
            //BtnCompositos.FlatStyle = FlatStyle.Flat;
            //BtnCompositos.FlatAppearance.BorderSize = 0;
            BtnCompositos.FlatAppearance.MouseOverBackColor = Color.FromArgb(96, 96, 96);
            BtnCompositos.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 128, 128);
            BtnCompositos.FlatStyle = FlatStyle.Flat;
            BtnCompositos.FlatAppearance.BorderSize = 2;
            BtnCompositos.FlatAppearance.BorderColor = Color.White;
        }

        private void BtnReassay_Click(object sender, EventArgs e)
        {
            Form2cs frm_reassay = new Form2cs(Nom_Usua.Text);
            frm_reassay.ShowDialog();
            //this.Hide();
        }

        private void BtnCompositos_Click(object sender, EventArgs e)
        {
            form_Compo frm3 = new form_Compo(Nom_Usua.Text);

            frm3.ShowDialog();
            //this.Hide();

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

        private void Btn_Envio_Ordenes_Click(object sender, EventArgs e)
        {
            
            Form_Orden frm4 = new Form_Orden();
            frm4.ShowDialog();
            //this.Hide();
        }
    }
}
