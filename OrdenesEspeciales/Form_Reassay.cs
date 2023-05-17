using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace OrdenesEspeciales
{
    public partial class Form2cs : Form
    {
        string connectionString = @"Data Source=10.120.1.190\sql_2019; Initial Catalog=GDMS_ANTAPACCAY; Integrated Security=True;";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        public Form2cs(
            string pUsua2
            )
        {

            InitializeComponent();
            lblUsu.Text = pUsua2;
            cargar_datos();
            cargar();
            CargarModule();
            //CargarDup();
            //CargarTiposMuestras();
            CargarStatus();
            autoCompletar(txt_newsample);
            autoCompletar1(txtDispatch);
            //cargar_tipocontroles();
            //cargar_bussinnescontroles();
            //cargar_datos1();
            //fillComboBox1();
            lblcant.Text = dataGridView1.Rows.Count.ToString();
            txtStatusDisp.Text = "NEW";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //CARGA DE COMBOBOX//
        public void cargar_datos()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select original_business_unit from DRILL_HOLE where HOLE_NUMBER not like '@%' and original_business_unit not like '%RELOG%' group by original_business_unit", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DataRow fila = dt.NewRow();
            fila["original_business_unit"] = "Selecciona un Business Unit";
            dt.Rows.InsertAt(fila, 0);

            cboUnidad.ValueMember = "original_business_unit";
            cboUnidad.DisplayMember = "original_business_unit";
            cboUnidad.DataSource = dt;
        }

        public void cargar_proyectos(string original)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("select b.REFERENCE_CODE_ID,a.PROJECT_NUMBER,a.original_business_unit from DRILL_HOLE a inner join PROJECT b on a.PROJECT_NUMBER = b.PROJECT_NUMBER where a.original_business_unit = @original_business_unit group by  a.PROJECT_NUMBER,original_business_unit,b.REFERENCE_CODE_ID", con);
            cmd.Parameters.AddWithValue("original_business_unit", original);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DataRow dr = dt.NewRow();
            dr["PROJECT_NUMBER"] = "Selecciona un proyecto";
            dt.Rows.InsertAt(dr, 0);

            comboBox2.ValueMember = "PROJECT_NUMBER";
            comboBox2.DisplayMember = "PROJECT_NUMBER";
            comboBox2.DataSource = dt;
        }

        public void cargar_sondajes(string PROJECT_NUMBER)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select trim(a.HOLE_NUMBER)as HOLE_NUMBER,b.original_business_unit,b.PROJECT_NUMBER from HOLE_ASSAY_SAMPLE a inner join DRILL_HOLE b on a.HOLE_NUMBER = b.HOLE_NUMBER where a.HOLE_NUMBER not like '@%' and b.is_master='Y' and PROJECT_NUMBER = @PROJECT_NUMBER Group by a.HOLE_NUMBER,b.PROJECT_NUMBER,b.original_business_unit order by HOLE_NUMBER asc", con);
            cmd.Parameters.AddWithValue("PROJECT_NUMBER", PROJECT_NUMBER);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DataRow dr = dt.NewRow();
            dr["HOLE_NUMBER"] = "Selecciona un sondaje";
            dt.Rows.InsertAt(dr, 0);

            comboBox3.ValueMember = "HOLE_NUMBER";
            comboBox3.DisplayMember = "HOLE_NUMBER";
            comboBox3.DataSource = dt;
        }
        public void cargar_tipodatos(string HOLE_NUMBER)
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("Select a.ASSAY_SAMPLE_TYPE_CODE from HOLE_ASSAY_SAMPLE as a inner join ASSAY_SAMPLE_TYPE as b on a.ASSAY_SAMPLE_TYPE_CODE = b.ASSAY_SAMPLE_TYPE_CODE where b.assay_sample_type_category = 'original' and a.HOLE_NUMBER=@HOLE_NUMBER Group by a.ASSAY_SAMPLE_TYPE_CODE", con);
            cmd.Parameters.AddWithValue("HOLE_NUMBER", HOLE_NUMBER);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DataRow fila = dt.NewRow();
            fila["ASSAY_SAMPLE_TYPE_CODE"] = "Select type";
            dt.Rows.InsertAt(fila, 0);

            cboSType.ValueMember = "ASSAY_SAMPLE_TYPE_CODE";
            cboSType.DisplayMember = "ASSAY_SAMPLE_TYPE_CODE";
            cboSType.DataSource = dt;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnidad.SelectedValue.ToString() != null)
            {
                string original = cboUnidad.SelectedValue.ToString();
                cargar_proyectos(original);
            }
            if (cboUnidad.SelectedValue.ToString() != null)
            {
                string original1 = cboUnidad.SelectedValue.ToString();
                cargar_proyectos1(original1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue.ToString() != null)
            {
                string PROJECT_NUMBER = comboBox2.SelectedValue.ToString();
                cargar_sondajes(PROJECT_NUMBER);
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedValue.ToString() != null)
            {
                string HOLE_NUMBER = comboBox3.SelectedValue.ToString();
                cargar_tipodatos(HOLE_NUMBER);
            }

        }
        
        private void cboSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSType.SelectedIndex == 1 && cboNewType.SelectedIndex ==3)
            {
                MessageBox.Show("No se puede seleccionar el valor 'ASSAY' en el segundo combo cuando el primer combo tiene seleccionado ese valor");
                cboNewType.SelectedIndex = 0; // deseleccionar el valor
            }

        }

        public void cargar_proyectos1(string original1)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("select b.ASSAY_STANDARD_CODE,a.business_unit_name from reference_code_assignments as a  inner join ASSAY_STANDARDS b on a.reference_code_id = b.REFERENCE_CODE_ID where column_name = 'ASSAY_STANDARD_CODE' and a.business_unit_name=@business_unit_name group by b.ASSAY_STANDARD_CODE,a.business_unit_name", con);
            cmd.Parameters.AddWithValue("business_unit_name", original1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DataRow dr = dt.NewRow();
            dr["ASSAY_STANDARD_CODE"] = "Seleccionar";
            dt.Rows.InsertAt(dr, 0);

            cbo2.ValueMember = "ASSAY_STANDARD_CODE";
            cbo2.DisplayMember = "ASSAY_STANDARD_CODE";
            cbo2.DataSource = dt;
        }

        public void cargar()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ASSAY_SAMPLE_TYPE_CODE from ASSAY_SAMPLE_TYPE where assay_sample_type_category = 'original'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                DataRow fila = dt.NewRow();
                fila["ASSAY_SAMPLE_TYPE_CODE"] = "Seleccionar";
                dt.Rows.InsertAt(fila, 0);
                //Control del ComboBox
                cboNewType.DataSource = dt;
                cboNewType.ValueMember = "ASSAY_SAMPLE_TYPE_CODE";
                cboNewType.DisplayMember = "ASSAY_SAMPLE_TYPE_CODE";

            }
            //-----------------------------------------------------------------------------
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ASSAY_SAMPLE_TYPE_CODE, assay_sample_type_desc from ASSAY_SAMPLE_TYPE where assay_sample_type_category = 'QC'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                DataRow fila = dt.NewRow();
                fila["assay_sample_type_desc"] = "Seleccionar";
                dt.Rows.InsertAt(fila, 0);
                //Control del ComboBox

                CboDup2.ValueMember = "ASSAY_SAMPLE_TYPE_CODE";
                CboDup2.DisplayMember = "assay_sample_type_desc";
                CboDup2.DataSource = dt;

            }
            
        }

        public void CargarModule()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct module_name from DHL_SAMPLE_DISPATCH_SAMPLES", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                DataRow fila = dt.NewRow();
                fila["module_name"] = "Seleccionar";
                dt.Rows.InsertAt(fila, 0);
                //Control del ComboBox
                cboMod.DataSource = dt;
                cboMod.ValueMember = "module_name";
                cboMod.DisplayMember = "module_name";
            }
        }
        //public void CargarDup()
        //{
        //    //using (SqlConnection sqlCon = new SqlConnection(connectionString))
        //    //{
        //    //    con.Open();
        //    //    SqlCommand cmd = new SqlCommand("select ASSAY_SAMPLE_TYPE_CODE, assay_sample_type_desc from ASSAY_SAMPLE_TYPE where assay_sample_type_category = 'QC'", con);
        //    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //    DataTable dt = new DataTable();
        //    //    da.Fill(dt);
        //    //    con.Close();

        //    //    DataRow fila = dt.NewRow();
        //    //    fila["assay_sample_type_desc"] = "Seleccionar";
        //    //    dt.Rows.InsertAt(fila, 0);
        //    //    //Control del ComboBox

        //    //    CboDup2.ValueMember = "ASSAY_SAMPLE_TYPE_CODE";
        //    //    CboDup2.DisplayMember = "assay_sample_type_desc";
        //    //    CboDup2.DataSource = dt;

        //    //}
        //}

        //public void CargarTiposMuestras()
        //{
        //    using (SqlConnection sqlCon = new SqlConnection(connectionString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT idcode,descripcion FROM REF_ASSAY_TIPO_MUESTRA", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();

        //        DataRow fila = dt.NewRow();
        //        fila["descripcion"] = "Seleccionar";
        //        dt.Rows.InsertAt(fila, 0);
        //        //Control del ComboBox
        //        cboMuestra.DataSource = dt;
        //        cboMuestra.ValueMember = "IDCODE";
        //        cboMuestra.DisplayMember = "descripcion";
        //    }
        //}
        public void CargarStatus()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select STATUS_CODE,status_description from HOLE_ASSAY_SAMPLE_STATUS", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                DataRow fila = dt.NewRow();
                fila["STATUS_CODE"] = "Seleccionar";
                dt.Rows.InsertAt(fila, 0);
                //Control del ComboBox
                cbosta.DataSource = dt;
                cbosta.ValueMember = "status_description";
                cbosta.DisplayMember = "STATUS_CODE";
            }
        }

        //Listado de datos en DATAGRIDVIEW
        public void listar_datos()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select trim(a.HOLE_NUMBER)as HOLE_NUMBER,a.ASSAY_SAMPLE_TYPE_CODE as TypeCode , a.SAMPLE_NUMBER,a.depth_from,a.depth_to from HOLE_ASSAY_SAMPLE a inner join ASSAY_SAMPLE_TYPE b on a.ASSAY_SAMPLE_TYPE_CODE = b.ASSAY_SAMPLE_TYPE_CODE where a.HOLE_NUMBER not like '@%' and b.ASSAY_SAMPLE_TYPE_CODE='" + cboSType.Text + "' and a.HOLE_NUMBER='" + comboBox3.Text + "'  group by a.HOLE_NUMBER,a.ASSAY_SAMPLE_TYPE_CODE , a.SAMPLE_NUMBER,a.depth_from,a.depth_to", con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                DgvSondajes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar el datagridview" + ex.ToString());
            }
        }

        //Obtener datos de el otro datagridview
        private void recibir_datos()
        {
            try
            {
                if (cboNewType.SelectedIndex == 0)
                {
                    MessageBox.Show("Por favor, seleccione un nuevo tipo válido.");
                    return;
                }
                //bool confirm = true;
                else if (string.IsNullOrEmpty(txtDispatch.Text))
                {
                    MessageBox.Show("Por favor, Crear un nuevo despacho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //bool confirm = false;
                foreach (DataGridViewRow row in DgvSondajes.Rows)
                {

                    bool isselect = Convert.ToBoolean(row.Cells["chk"].Value);

                    if (isselect && !string.IsNullOrEmpty(txtDispatch.Text))
                    {
                        bool exists = false;
                        foreach (DataGridViewRow existingRow in dataGridView1.Rows)
                        {
                            if ((!string.IsNullOrEmpty(row.Cells[5].Value?.ToString()) && !string.IsNullOrEmpty(row.Cells[4].Value?.ToString())) &&
                                (!string.IsNullOrEmpty(existingRow.Cells[3].Value?.ToString()) && !string.IsNullOrEmpty(existingRow.Cells[2].Value?.ToString())) &&
                                (existingRow.Cells[3].Value.ToString() == row.Cells[5].Value.ToString() &&
                                    existingRow.Cells[2].Value.ToString() == row.Cells[4].Value.ToString()))
                            {
                                exists = true;
                                break;
                            }

                        }

                        if (exists)
                        {
                            MessageBox.Show("Ya existe una fila con los mismos valores de 'from' y 'to'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        DataGridViewRow fila = new DataGridViewRow();
                        fila.CreateCells(dataGridView1);
                        fila.Cells[1].Value = row.Cells[1].Value;
                        fila.Cells[4].Value = row.Cells[3].Value;
                        fila.Cells[2].Value = row.Cells[4].Value;
                        fila.Cells[3].Value = row.Cells[5].Value;
                        fila.Cells[6].Value = cboNewType.SelectedValue;
                        //.SelectedValue = "REASSAY";
                        fila.Cells[7].Value = cbosta.SelectedValue = "Logged";
                        fila.Cells[8].Value = cboMod.SelectedValue = "DHL";
                        fila.Cells[9].Value = txtDispatch.Text;

                        dataGridView1.Rows.Add(fila);

                        row.Cells[0].Value = false;

                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;
                        dataGridView1.Columns[8].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;
                        dataGridView1.Columns[10].ReadOnly = true;
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.Cells["chk"].ReadOnly = true;
                        lblcant.Text = dataGridView1.Rows.Count.ToString();
                    }


                    else if (isselect && string.IsNullOrEmpty(txtDispatch.Text))
                    {
                        MessageBox.Show("Por favor, Crear un nuevo despacho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al enviar al Despacho: " + ex.Message);
            }

        }


        private void select_dup()
        {
            try
            {
                if (CboDup2.SelectedIndex == 0)
                {
                    MessageBox.Show("Debe seleccionar un valor del campo 'Duplicado'.");
                    return;
                }

                // Validar que exista un new_sample en la tabla
                bool existeNewSample = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
                    {
                        existeNewSample = true;
                        break;
                    }
                }

                if (!existeNewSample)
                {
                    MessageBox.Show("Debe crear un new_sample antes de crear un duplicado.");
                    return;
                }

                // Mostrar un MessageBox de opciones para la ubicación del duplicado
                var dialogResult = MessageBox.Show("¿Dónde desea generar el duplicado?\n\n" +
                    "Presione 'Yes' para ubicarlo debajo de la fila seleccionada.\n\n" +
                    "Presione 'No' para ubicarlo al final de la Fila",
                    "Seleccionar ubicación del duplicado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Establecer el valor de insertAtSelectedRow según la respuesta del usuario
                bool insertAtSelectedRow = (dialogResult == DialogResult.Yes);

                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = null;
                int selectedRowIndex = -1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                    {
                        selectedRow = row;
                        selectedRowIndex = row.Index;
                        break;
                    }
                }

                long siguiente = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && !string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
                    {
                        long cadena = Convert.ToInt64(row.Cells[5].Value.ToString());
                        if (cadena > siguiente)
                        {
                            siguiente = cadena;
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isselect = Convert.ToBoolean(row.Cells["chk1"].Value);
                    if (isselect)
                    {
                        siguiente++;
                        DataGridViewRow fila = new DataGridViewRow();
                        fila.CreateCells(dataGridView1);
                        fila.Cells[1].Value = row.Cells[1].Value;
                        fila.Cells[2].Value = row.Cells[2].Value;
                        fila.Cells[3].Value = row.Cells[3].Value;
                        fila.Cells[10].Value = row.Cells[5].Value;
                        fila.Cells[6].Value = CboDup2.SelectedValue;
                        fila.Cells[7].Value = cbosta.SelectedValue = "Logged";
                        fila.Cells[8].Value = cboMod.SelectedValue = "DHL";
                        fila.Cells[9].Value = txtDispatch.Text;
                        fila.Cells[5].Value = siguiente.ToString("00000000");

                        if (insertAtSelectedRow)
                        {
                            // Se agrega después de la fila seleccionada
                            dataGridView1.Rows.Insert(selectedRowIndex + 1, fila);
                        }
                        else
                        {
                            // Se agrega al final del DataGridView
                            dataGridView1.Rows.Add(fila);
                        }

                        row.Cells[0].Value = false;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;
                        dataGridView1.Columns[8].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el Consecutivo: " + ex.Message);
            }
            //if (CboDup2.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Debe seleccionar un valor del campo 'Duplicado'.");
            //    return;
            //}
            //// Validar que exista un new_sample en la tabla
            //bool existeNewSample = false;
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (!string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
            //    {
            //        existeNewSample = true;
            //        break;
            //    }
            //}

            //if (!existeNewSample)
            //{
            //    MessageBox.Show("Debe crear un new_sample antes de crear un duplicado.");
            //    return;
            //}
            ////-----creacion de los duplicados
            ////-----creacion de los consecutivos
            //try
            //{
            //    long siguiente = 0;
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (!row.IsNewRow && !string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
            //        {
            //            long cadena = Convert.ToInt64(row.Cells[5].Value.ToString());
            //            if (cadena > siguiente)
            //            {
            //                siguiente = cadena;
            //            }
            //        }
            //    }

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        bool isselect = Convert.ToBoolean(row.Cells["chk1"].Value);
            //        if (isselect)
            //        {
            //            siguiente++;
            //            DataGridViewRow fila = new DataGridViewRow();
            //            fila.CreateCells(dataGridView1);
            //            fila.Cells[1].Value = row.Cells[1].Value;
            //            fila.Cells[2].Value = row.Cells[2].Value;
            //            fila.Cells[3].Value = row.Cells[3].Value;
            //            fila.Cells[10].Value = row.Cells[5].Value;
            //            fila.Cells[6].Value = CboDup2.SelectedValue;
            //            fila.Cells[7].Value = cbosta.SelectedValue = "Logged";
            //            fila.Cells[8].Value = cboMod.SelectedValue = "DHL";
            //            fila.Cells[9].Value = txtDispatch.Text;
            //            fila.Cells[5].Value = siguiente.ToString("00000000"); 
            //            dataGridView1.Rows.Add(fila);
            //            row.Cells[0].Value = false;
            //            dataGridView1.Columns[1].ReadOnly = true;
            //            dataGridView1.Columns[2].ReadOnly = true;
            //            dataGridView1.Columns[3].ReadOnly = true;
            //            dataGridView1.Columns[4].ReadOnly = true;
            //            dataGridView1.Columns[5].ReadOnly = true;
            //            dataGridView1.Columns[6].ReadOnly = true;
            //            dataGridView1.Columns[7].ReadOnly = true;
            //            dataGridView1.Columns[8].ReadOnly = true;
            //            dataGridView1.Columns[9].ReadOnly = true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ocurrió un error al generar el Consecutivo: " + ex.Message);
            //}


            //// Validar que exista un new_sample en la tabla
            //bool existeNewSample = false;
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (!string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
            //    {
            //        existeNewSample = true;
            //        break;
            //    }
            //}

            //if (!existeNewSample)
            //{
            //    MessageBox.Show("Debe crear un new_sample antes de crear un duplicado.");
            //    return;
            //}
            //try
            //{
            //    long siguiente = 0;
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (!row.IsNewRow)
            //        {
            //            if (!string.IsNullOrEmpty(row.Cells[5].Value?.ToString()))
            //            {
            //                long cadena = Convert.ToInt64(row.Cells[5].Value.ToString());
            //                if (cadena > siguiente)
            //                {
            //                    siguiente = cadena;
            //                }
            //            }
            //        }
            //    }
            //    siguiente++;

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        bool isselect = Convert.ToBoolean(row.Cells["chk1"].Value);
            //        if (isselect)
            //        {
            //            //long siguiente = 0;
            //            foreach (DataGridViewRow row2 in dataGridView1.Rows)
            //            {
            //                if (!row2.IsNewRow && !object.ReferenceEquals(row, row2))
            //                {
            //                    if (!string.IsNullOrEmpty(row2.Cells[5].Value?.ToString()))
            //                    {
            //                        long cadena = Convert.ToInt64(row2.Cells[5].Value.ToString());
            //                        if (cadena > siguiente)
            //                        {
            //                            siguiente = cadena;
            //                        }
            //                    }
            //                }
            //            }
            //            siguiente++;

            //            DataGridViewRow fila = new DataGridViewRow();
            //            fila.CreateCells(dataGridView1);
            //            fila.Cells[1].Value = row.Cells[1].Value;
            //            fila.Cells[2].Value = row.Cells[2].Value;
            //            fila.Cells[3].Value = row.Cells[3].Value;
            //            fila.Cells[10].Value = row.Cells[5].Value;
            //            fila.Cells[6].Value = CboDup2.SelectedValue;
            //            fila.Cells[7].Value = cbosta.SelectedValue = "Logged";
            //            fila.Cells[8].Value = cboMod.SelectedValue = "DHL";
            //            fila.Cells[9].Value = txtDispatch.Text;
            //            fila.Cells[5].Value = siguiente.ToString("00000000");
            //            dataGridView1.Rows.Add(fila);
            //            row.Cells[0].Value = false;
            //            dataGridView1.Columns[1].ReadOnly = true;
            //            dataGridView1.Columns[2].ReadOnly = true;
            //            dataGridView1.Columns[3].ReadOnly = true;
            //            dataGridView1.Columns[4].ReadOnly = true;
            //            dataGridView1.Columns[5].ReadOnly = true;
            //            dataGridView1.Columns[6].ReadOnly = true;
            //            dataGridView1.Columns[7].ReadOnly = true;
            //            dataGridView1.Columns[8].ReadOnly = true;
            //            dataGridView1.Columns[9].ReadOnly = true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ocurrió un error al generar el Duplicado: " + ex.Message);
            //}

        }

        public void autoCompletar(TextBox cajaTexto)
        {

            try
            {
                //select distinct top 1 SAMPLE_NUMBER from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER like'"+txt_newsample.Text+"%' order by SAMPLE_NUMBER desc SAMPLE_NUMBER like '" + txt_newsample.Text + "%' where SAMPLE_NUMBER like '" + txt_newsample.Text + "%' 
                con.Open();

                //string consultar = "select SAMPLE_NUMBER from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER like ('" + txt_newsample.Text + "%')order by SAMPLE_NUMBER desc";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM(SELECT SAMPLE_NUMBER FROM HOLE_ASSAY_SAMPLE WHERE HOLE_NUMBER NOT LIKE '@%'UNION SELECT SAMPLE_NUMBER FROM HOLE_ASSAY_STANDARDS WHERE HOLE_NUMBER NOT LIKE '@%'UNION SELECT sample_number FROM sstn_surface_samples ) AS A WHERE ISNUMERIC(SAMPLE_NUMBER) <> 0 AND SAMPLE_NUMBER like '"+txt_newsample.Text+ "%'  GROUP BY SAMPLE_NUMBER";
                cmd1.ExecuteNonQuery();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    //namesCollection.Add(dr1["SAMPLE_NUMBER"].ToString());
                    cajaTexto.AutoCompleteCustomSource.Add(dr1["SAMPLE_NUMBER"].ToString());
                }
                dr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo autocompletar el Textbox :" + ex.ToString());
            }
            finally
            {
                con.Close();
            }

        }
        public void autoCompletar1(TextBox cajaTexto1)
        {
            try
            {
                //select distinct top 1 SAMPLE_NUMBER from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER like'"+txt_newsample.Text+"%' order by SAMPLE_NUMBER desc SAMPLE_NUMBER like '" + txt_newsample.Text + "%' where SAMPLE_NUMBER like '" + txt_newsample.Text + "%' 
                con.Open();

                //string consultar = "select SAMPLE_NUMBER from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER like ('" + txt_newsample.Text + "%')order by SAMPLE_NUMBER desc";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select dispatch_number from DHL_SAMPLE_DISPATCH_HEADER where dispatch_number like '" + txtDispatch.Text + "%' group by dispatch_number order by dispatch_number desc";
                cmd1.ExecuteNonQuery();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {

                    cajaTexto1.AutoCompleteCustomSource.Add(dr1["dispatch_number"].ToString());
                }
                dr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo autocompletar el Textbox :" + ex.ToString());
            }
            finally
            {
                con.Close();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //insert_newsample();

            //ENVIO A LA BD Y A DIFERENTES TABLAS
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string valor = (string)row.Cells["Estado"].Value;
                string valor2 = (string)row.Cells["ParentSample"].Value;
                string valor3 = (string)row.Cells["Sample_Original"].Value;
                string valor4 = (string)row.Cells["Dispatch"].Value;
                string valor5 = (string)row.Cells["Sample_new"].Value;

                string cone = "select SAMPLE_NUMBER,dispatch_number from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER ='" + valor5 + "'and dispatch_number ='" + valor4 + "'";
                try
                {
                    if (valor != "")
                    {


                        //INSERT A DISPATCH
                        string sql = "INSERT INTO DHL_SAMPLE_DISPATCH_SAMPLES(SAMPLE_NUMBER,sample_type,hole_number,dispatch_number,module_name) values(@SampleNumber,@AssayCode,@HoleNumber,@dispatch,@mode)";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(cone, con);



                        if (cone == valor5 && cone == valor4)
                        {
                            cmd.Parameters.AddWithValue("sample_new", valor5);
                            cmd.Parameters.AddWithValue("dispatch", valor4);
                            MessageBox.Show("Registro Ya Existe");
                        }
                        else
                        {
                            try
                            {
                                SqlCommand cmd1 = new SqlCommand(sql, con);
                                cmd1.Parameters.AddWithValue("@HoleNumber", row.Cells["Hole"].Value);
                                cmd1.Parameters.AddWithValue("@SampleNumber", row.Cells["Sample_new"].Value);
                                cmd1.Parameters.AddWithValue("@AssayCode", row.Cells["TypeNew"].Value);
                                cmd1.Parameters.AddWithValue("@dispatch", row.Cells["Dispatch"].Value);
                                cmd1.Parameters.AddWithValue("@mode", row.Cells["Module_Name"].Value);
                                cmd1.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show("No Pudo Guardar Los Datos Debido A: " + ex.Message);
                            }
                            finally
                            {
                                con.Close();
                            }

                        }

                    }

                    if (valor == null)
                    {
                        //INSERT STANDAR
                        SqlCommand cmd1 = new SqlCommand("insert into HOLE_ASSAY_STANDARDS(SAMPLE_NUMBER,ASSAY_STANDARD_CODE,HOLE_NUMBER) values (@SAMPLE_NUMBER,@ASSAY_STANDARD_CODE,@HOLE_NUMBER)", con);
                        cmd1.Parameters.AddWithValue("@SAMPLE_NUMBER", row.Cells["Sample_new"].Value);
                        cmd1.Parameters.AddWithValue("@ASSAY_STANDARD_CODE", row.Cells["TypeNew"].Value);
                        cmd1.Parameters.AddWithValue("@HOLE_NUMBER", row.Cells["Hole"].Value);

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (valor2 == null)
                    {
                        //INSERT NUEVAS MUESTRA TYPE REASSAY
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO HOLE_ASSAY_SAMPLE(SAMPLE_NUMBER, ASSAY_SAMPLE_TYPE_CODE, HOLE_NUMBER, depth_from, depth_to,STATUS_CODE,parent_sample_number) values(@SampleNumber, @AssayCode, @HoleNumber, @depth_from, @depth_to,@Estado,@parent)", con);
                        cmd1.Parameters.AddWithValue("@SampleNumber", row.Cells["Sample_new"].Value);
                        cmd1.Parameters.AddWithValue("@AssayCode", row.Cells["TypeNew"].Value);
                        cmd1.Parameters.AddWithValue("@HoleNumber", row.Cells["Hole"].Value);
                        cmd1.Parameters.AddWithValue("@depth_from", row.Cells["Depth_From"].Value);
                        cmd1.Parameters.AddWithValue("@depth_to", row.Cells["Depth_to"].Value);
                        cmd1.Parameters.AddWithValue("@Estado", row.Cells["Estado"].Value);
                        cmd1.Parameters.AddWithValue("@parent", row.Cells["Sample_Original"].Value);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();

                    }
                    else if (valor3 == null)
                    {
                        //INSERT DE DUPLICADOS
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO HOLE_ASSAY_SAMPLE(SAMPLE_NUMBER, ASSAY_SAMPLE_TYPE_CODE, HOLE_NUMBER, depth_from, depth_to,STATUS_CODE,parent_sample_number) values(@SampleNumber, @AssayCode, @HoleNumber, @depth_from, @depth_to,@Estado,@parent)", con);
                        cmd1.Parameters.AddWithValue("@SampleNumber", row.Cells["Sample_new"].Value);
                        cmd1.Parameters.AddWithValue("@AssayCode", row.Cells["TypeNew"].Value);
                        cmd1.Parameters.AddWithValue("@HoleNumber", row.Cells["Hole"].Value);
                        cmd1.Parameters.AddWithValue("@depth_from", row.Cells["Depth_From"].Value);
                        cmd1.Parameters.AddWithValue("@depth_to", row.Cells["Depth_to"].Value);
                        cmd1.Parameters.AddWithValue("@Estado", row.Cells["Estado"].Value);
                        cmd1.Parameters.AddWithValue("@parent", row.Cells["ParentSample"].Value);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    /* else*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al insertar: " + ex.Message);
                }

                
            }
            MessageBox.Show("Datos Insertados");
            BtnBlancos.Enabled = true;
        }
       
        //Insertar Encabezado de Dispatch
        public void insert()
        {
            //Instancia de formilario Login

            if (string.IsNullOrWhiteSpace(txtDispatch.Text))
            {
                MessageBox.Show("Digite el Nuevo N° Despacho", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtDispatch.Text.Length < 8) // Verifica que el valor tenga más de 8 caracteres
            {
                MessageBox.Show("El número de dispatch debe tener al menos 12 caracteres", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    con.Open();

                    // Consulta SQL para buscar el dispatch_number ingresado por el usuario
                    string consultar = "SELECT dispatch_status FROM DHL_SAMPLE_DISPATCH_HEADER WHERE dispatch_number='" + txtDispatch.Text + "'";

                    SqlCommand cmdConsultar = new SqlCommand(consultar, con);
                    object result = cmdConsultar.ExecuteScalar();

                    // Si la consulta devuelve un resultado, el dispatch ya existe
                    if (result != null)
                    {
                        string dispatchStatus = result.ToString();

                        MessageBox.Show($"Dispatch ya existe con el estado: {dispatchStatus}");
                    }
                    else // Si la consulta no devuelve un resultado, el dispatch no existe
                    {
                        SqlCommand cmdInsertar = new SqlCommand("insert into DHL_SAMPLE_DISPATCH_HEADER(dispatch_number,dispatch_status) " +
                        $"values('{txtDispatch.Text}','{txtStatusDisp.Text}')", con);

                        cmdInsertar.ExecuteNonQuery();

                        MessageBox.Show($"El Dispatch: {txtDispatch.Text} se ha agregado correctamente", "Confirmar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception /*ex*/)
                {
                    MessageBox.Show($"No se pudo agregar el Dispatch: {txtDispatch.Text}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    con.Close();
                }
            }
            ////Instancia de formilario Login

            //string consultar = "SELECT * FROM DHL_SAMPLE_DISPATCH_SAMPLES where dispatch_number='" + txtDispatch.Text + "'";
            //if (string.IsNullOrWhiteSpace(txtDispatch.Text))
            //{
            //    MessageBox.Show("Llenar los campos");
            //}

            //else
            //{
            //    try
            //    {
            //        con.Open();
            //        if (consultar == txtDispatch.Text)
            //        {

            //            MessageBox.Show("Este Dispatch ya existe");
            //        }
            //        else
            //        {

            //            SqlCommand cmd1 = new SqlCommand("insert into DHL_SAMPLE_DISPATCH_HEADER(dispatch_number,dispatch_status) " +
            //            "values('" + txtDispatch.Text + "','"+ txtStatusDisp.Text+"')", con);
            //            cmd1.ExecuteNonQuery();
            //            MessageBox.Show("El Dispatch :" + txtDispatch.Text + " Se ha agregado correctamente.", "Confirmar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }

            //    }
            //    catch (Exception /*ex*/)
            //    {
            //        MessageBox.Show("El Dispatch: " + txtDispatch.Text + " Ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
            //}


        }
        //select SAMPLE_NUMBER from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER like'"++"%'
        private void button9_Click_1(object sender, EventArgs e)
        {
            insert();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recibir_datos();
            //MessageBox.Show("Se agregó satisfactoriamente al despacho.");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (cbo2.SelectedIndex == 0)
                {
                    MessageBox.Show("Debe seleccionar un valor del campo 'Controles'.");
                    return;
                }
                if (string.IsNullOrEmpty(txtDispatch.Text))
                {
                    MessageBox.Show("Por favor, ingrese un valor en el campo de envío al despacho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }

                // Si no se ha seleccionado ninguna fila, se agrega al final
                if (rowIndex == -1)
                {
                    rowIndex = dataGridView1.Rows.Add();
                }
                else
                {
                    // Se agrega después de la fila seleccionada
                    rowIndex++;
                    dataGridView1.Rows.Insert(rowIndex, 1);
                }

                // Inicializamos el valor de las celdas
                dataGridView1.Rows[rowIndex].Cells["TypeNew"].Value = cbo2.SelectedValue ?? "";
                dataGridView1.Rows[rowIndex].Cells["Module_Name"].Value = "STD";
                dataGridView1.Rows[rowIndex].Cells["Dispatch"].Value = txtDispatch.Text ?? "";
                dataGridView1.Rows[rowIndex].Cells["Hole"].Value = comboBox3.SelectedValue ?? "";
                //dataGridView1.Rows[rowIndex].Cells["Sample_New"].Value = $"{textBox2.Text}{textBox3.Text}{textBox4.Text}{textBox1.Text}";

                // Establecemos el valor del check en false para la fila seleccionada
                if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[rowIndex].Cells[0].Value = false;
                }

                // Limpiamos el combo y actualizamos el label con la cantidad de filas
                cbo2.SelectedIndex = 0;
                lblcant.Text = dataGridView1.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el Control: " + ex.Message);
            }


        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            select_dup();
            CboDup2.SelectedIndex = 0;
            lblcant.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            txtDispatch.Clear();
            //cboMuestra.SelectedItem = null;
            txtStatusDisp.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            lblcant.Text = dataGridView1.Rows.Count.ToString();
            BtnBlancos.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isselect = Convert.ToBoolean(row.Cells["chk1"].Value);

                if (isselect)
                {
                    rowsToDelete.Add(row);
                }
            }

            foreach (DataGridViewRow row in rowsToDelete)
            {
                dataGridView1.Rows.Remove(row);
            }

            lblcant.Text = dataGridView1.Rows.Count.ToString();
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{

            //    bool isselect = Convert.ToBoolean(row.Cells["chk1"].Value);

            //    if (isselect)
            //    {
            //        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            //    }
            //    lblcant.Text = dataGridView1.Rows.Count.ToString();
            //}
        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {

            listar_datos();
        }



        
        private void txtDispatch_TextChanged(object sender, EventArgs e)
        {
            txtDispatch.MaxLength = 12;
        }

        private void txt_newsample_TextChanged(object sender, EventArgs e)
        {
            txt_newsample.MaxLength = 11;
        }

        private void txt_newsample_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void cbo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CboDup2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboMuestra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                // Seleccionar el checkbox
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (checkbox.Value == null)
                {
                    checkbox.Value = false;
                }

                checkbox.Value = !(bool)checkbox.Value;

            }
        }

        
        public void delete()
        {
            try
            {
                // Mostrar advertencia al usuario
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el Dispatch " + txtDispatch.Text + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    con.Open();

                    // Consulta SQL para verificar el estado del dispatch_number
                    SqlCommand cmd2 = new SqlCommand("SELECT dispatch_status FROM DHL_SAMPLE_DISPATCH_HEADER WHERE dispatch_number = @dispatch_number", con);
                    cmd2.Parameters.AddWithValue("@dispatch_number", txtDispatch.Text);
                    string dispatchStatus = cmd2.ExecuteScalar()?.ToString();

                    // Si el dispatch_number tiene un estado de "DISPATCHED", mostrar un mensaje al usuario
                    if (dispatchStatus == "DISPATCHED")
                    {
                        MessageBox.Show("No se puede eliminar un Dispatch que esta en estado 'DISPATCHED'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Si el dispatch_number no ha sido despachado, eliminarlo
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM DHL_SAMPLE_DISPATCH_HEADER WHERE dispatch_number = @dispatch_number", con);
                        cmd1.Parameters.AddWithValue("@dispatch_number", txtDispatch.Text);
                        int rowsAffected = cmd1.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El Dispatch " + txtDispatch.Text + " ha sido eliminado correctamente.", "Confirmar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDispatch.Clear(); // Limpiar el contenido del cuadro de texto
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el Dispatch " + txtDispatch.Text + " en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Dispatch: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

        private void cboNewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSType.SelectedIndex == 1 && cboNewType.SelectedIndex == 3)
            {
                MessageBox.Show("No se puede seleccionar el valor 'ASSAY'");
                cboNewType.SelectedIndex = -1; // deseleccionar el valor
            }
        }

        private void BtnNewSam2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos. Verifique antes de agregar un nuevo sample.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string valorNuevo = txt_newsample.Text.Trim();

                if (!string.IsNullOrEmpty(valorNuevo) && valorNuevo.Length == 11)
                {
                    // Consultar si el nuevo sample ya existe en la BD
                    con.Open();
                    string consultar = "SELECT COUNT(*) FROM(SELECT SAMPLE_NUMBER FROM HOLE_ASSAY_SAMPLE WHERE HOLE_NUMBER NOT LIKE '@%'UNION SELECT SAMPLE_NUMBER FROM HOLE_ASSAY_STANDARDS WHERE HOLE_NUMBER NOT LIKE '@%'UNION SELECT sample_number FROM sstn_surface_samples ) AS A WHERE ISNUMERIC(SAMPLE_NUMBER) <> 0 AND SAMPLE_NUMBER = '" + valorNuevo + "'";
                    SqlCommand cmdConsultar = new SqlCommand(consultar, con);
                    int result = Convert.ToInt32(cmdConsultar.ExecuteScalar());
                    con.Close();

                    // Si el sample ya existe, mostrar mensaje y salir del método sin agregarlo al DataGridView
                    if (result > 0)
                    {
                        MessageBox.Show("El sample ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Si el sample no existe, agregarlo al DataGridView
                    int filaActual = 0;
                    int valorCorrelativo = int.Parse(valorNuevo.Substring(3));
                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        if (fila.Cells["Sample_new"].Value != null && fila.Cells["Sample_new"].Value.ToString() != "")
                        {
                            filaActual = fila.Index;
                            continue;
                        }

                        if (fila.Cells[6].Value != null && fila.Cells[6].Value.ToString() == "COMP_GEO")
                        {
                            fila.Cells["Sample_new"].Value = valorNuevo.Substring(0, 3) + string.Format("{0:00000000}", valorCorrelativo);
                            fila.Cells[4].Value = fila.Cells[5].Value.ToString();
                        }
                        else
                        {
                            fila.Cells["Sample_new"].Value = valorNuevo.Substring(0, 3) + string.Format("{0:00000000}", valorCorrelativo);
                        }

                        valorCorrelativo += 1;
                    }

                    dataGridView1.CurrentCell = dataGridView1.Rows[filaActual].Cells["Sample_new"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el Nuevo Sample: " + ex.Message);
            }

            //BtnTipDup.Enabled = true;
            BtnBlancos.Enabled = false;
        }

        private void BtnEliDis2_Click(object sender, EventArgs e)
        {
            delete();
        }

    }
}

