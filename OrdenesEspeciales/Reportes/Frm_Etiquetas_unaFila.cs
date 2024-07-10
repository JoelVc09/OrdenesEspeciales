﻿using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OrdenesEspeciales.Form_Orden;
using static OrdenesEspeciales.Login;

namespace OrdenesEspeciales.Reportes
{
    public partial class Frm_Etiquetas_unaFila : Form
    {
        public Frm_Etiquetas_unaFila()
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
            Etiqueta_Laboratorio_1Fila reporte = new Etiqueta_Laboratorio_1Fila();
            reporte.SetDatabaseLogon(usuario, contraseña);
            reporte.SetParameterValue("Despacho", despacho);
            crVisorEtiquetaUnaFila.ReportSource = reporte;
            crVisorEtiquetaUnaFila.Show();

        }

        private void Frm_Etiquetas_unaFila_Load(object sender, EventArgs e)
        {
            MostrarReporte();
        }
    }
}
