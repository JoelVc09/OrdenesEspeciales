using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace OrdenesEspeciales
{
    public partial class PanelAdmin : Form
    {
        OdbcConnection con = ConexionODBC.connection;

        private const string PathOrdenesEspeciales = "usuariosOrdenesespeciales.json";
        private const string PathCompositos = "usuariosCompositos.json";
        private const string PathOrdenAnalisis = "usuariosOrdenAnalisis.json";

        public PanelAdmin()
        {
            InitializeComponent();
            listar_datos();
            LlenarDataGridViews();
        }

        public void listar_datos()
        {
            try
            {
                string query = "SELECT userid, last_name, department, user_role, phone_number, email_address FROM pfuser";

                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataColumn itemColumn = new DataColumn("item", typeof(int));
                        dt.Columns.Add(itemColumn);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["item"] = i + 1;
                        }

                        dt.Columns["item"].SetOrdinal(0);

                        dgvUsuarios.DataSource = dt;

                        foreach (DataGridViewColumn column in dgvUsuarios.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar el DataGridView: " + ex.Message);
            }
        }

        private void LlenarDataGridViews()
        {
            // Cargar datos desde JSON y llenar los DataGridViews
            CargarDatosDesdeJSON(dgvordenesespeciales, PathOrdenesEspeciales);
            CargarDatosDesdeJSON(dgvcompositos, PathCompositos);
            CargarDatosDesdeJSON(dgvordenanalisis, PathOrdenAnalisis);
        }

        private void LlenarDataGridView(DataGridView dgv, List<string> usuarios)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Usuario", typeof(string));

            foreach (var usuario in usuarios)
            {
                dt.Rows.Add(usuario);
            }

            dgv.DataSource = dt;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            GuardarDatosEnJSON(dgvordenesespeciales, PathOrdenesEspeciales);
            GuardarDatosEnJSON(dgvcompositos, PathCompositos);
            GuardarDatosEnJSON(dgvordenanalisis, PathOrdenAnalisis);

            MessageBox.Show("Las listas han sido actualizadas correctamente.");
        }

        private void GuardarDatosEnJSON(DataGridView dgv, string path)
        {
            List<string> usuarios = ObtenerUsuariosDeDataGridView(dgv);
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            File.WriteAllText(fullPath, JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void CargarDatosDesdeJSON(DataGridView dgv, string path)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(fullPath))
            {
                List<string> usuarios = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(fullPath));
                LlenarDataGridView(dgv, usuarios);
            }
        }

        private List<string> ObtenerUsuariosDeDataGridView(DataGridView dgv)
        {
            List<string> usuarios = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    object usuarioValue = row.Cells["Usuario"].Value;
                    if (usuarioValue != null)
                    {
                        string usuario = usuarioValue.ToString();
                        if (!string.IsNullOrWhiteSpace(usuario))
                        {
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}
