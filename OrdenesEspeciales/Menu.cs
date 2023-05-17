using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdenesEspeciales
{
    public partial class Menu : Form
    {
        public Menu(string pUsua)
        {

            InitializeComponent();
            Nom_Usua.Text = pUsua;
            //BtnReassay.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255); //Cambiar el color del borde
            //BtnReassay.BackColor = Color.FromArgb(255, 0, 128, 128); //Cambiar el color de fondo
            //BtnReassay.FlatAppearance.BorderSize = 2; //Cambiar el ancho del borde
            // Crear un nuevo botón

            // Personalizar el texto del botón
            //BtnReassay.Text = "Mi botón personalizado";

            // Personalizar el color de fondo del botón
            //BtnReassay.BackColor = Color.FromArgb(64, 64, 64);

            // Personalizar el color del texto del botón
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
        }

        private void BtnCompositos_Click(object sender, EventArgs e)
        {
            form_Compo frm3 = new form_Compo(Nom_Usua.Text);

            frm3.ShowDialog();
            
        }
    }
}
