using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;


namespace OrdenesEspeciales
{
    public partial class Form_Orden : Form
    {
        OdbcConnection con = ConexionODBC.connection;
        public Form_Orden()
        {
            
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            completecodBh(txt_Orden);

        }

        //Listado de datos en DATAGRIDVIEW
        public void listar_datos()
        {
            try
            {
                string numeroDespacho = cbo_proyecto.Text;

                
                string query = "SELECT ID_BH, TIPO_CONO, RESISTENCIA, BH_LITOLOGIA, PROYECTO_GEOLOGIA, FECHA_LOGUEO FROM UDEF_LOG_BLASTHOLE WHERE PROYECTO_GEOLOGIA = ? ORDER BY ID_BH ";


                OdbcCommand command = new OdbcCommand(query, con);
                command.Parameters.AddWithValue("@dispatchNumber", numeroDespacho);

                OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataColumn itemColumn = new DataColumn("item", typeof(int));
                dt.Columns.Add(itemColumn);

                // Enumerar automáticamente los ítems
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["item"] = i + 1;
                }

                // Reordenar las columnas para que "item" aparezca primero
                dt.Columns["item"].SetOrdinal(0);

                Dgv_Consulta.DataSource = dt;

                // Ajustar automáticamente el tamaño de las columnas
                foreach (DataGridViewColumn column in Dgv_Consulta.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar el datagridview: " + ex.Message);
            }
        }

        // ENUMERAR ITEM 

        private void Dgv_Consulta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si es la primera columna y no es la fila de encabezado
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.Value = (e.RowIndex + 1).ToString(); // Enumerar automáticamente comenzando desde 1
                e.FormattingApplied = true;
            }
        }

        // LENAR EL COMBO BOX DEL DISPACHE

        private void proyecto_number()
        {
            // Obtener las fechas seleccionadas
            string fechaInicio = dtp_inicio.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fechaFin = dtp_fin.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Crear la consulta SQL con las fechas seleccionadas
            string query = $"select proyecto from UDEF_BLASTHOLE where CREATION_DATE > '{fechaInicio}' and CREATION_DATE < '{fechaFin}'";


            // Crear el comando ODBC con la consulta
            OdbcCommand cmd = new OdbcCommand(query, con);

            // Crear el adaptador ODBC
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);

            // Crear un nuevo DataTable para almacenar los resultados
            DataTable dt = new DataTable();

            // Llenar el DataTable con los resultados de la consulta
            da.Fill(dt);

            // Crear una nueva fila en el DataTable con un valor inicial especial
            DataRow fila = dt.NewRow();
            fila["proyecto"] = "Selecciona un Proyecto";
            dt.Rows.InsertAt(fila, 0);

            // Configurar el ComboBox cbo_proyecto
            cbo_proyecto.ValueMember = "proyecto ";
            cbo_proyecto.DisplayMember = "proyecto";
            cbo_proyecto.DataSource = dt;


        }



        // LLEVAR DATOS PARA CREAR EL DESPACHO, PREVIAMENTE SE TIENE QUE INGRESAR EL DESPACHO 

        private void recibir_datos()
        {
            // Verifica si hay filas en el Dgv_Consulta
            if (Dgv_Consulta.Rows.Count > 0)
            {

                if (string.IsNullOrEmpty(txt_Orden.Text))
                {
                    MessageBox.Show("Por favor, Crear un nuevo despacho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Copia las 2 primeras columnas del Dgv_Consulta al Dgv_Orden
                for (int i = 0; i < 2; i++)
                {
                    Dgv_Orden.Columns.Add(Dgv_Consulta.Columns[i].Clone() as DataGridViewColumn);
                }



                // Copia las filas del Dgv_Consulta al Dgv_Orden
                for (int filaIndex = 0; filaIndex < Dgv_Consulta.Rows.Count; filaIndex++) // Comienza desde la segunda fila para evitar el espacio en blanco
                {
                    DataGridViewRow filaDgvConsulta = Dgv_Consulta.Rows[filaIndex];
                    DataGridViewRow nuevaFila = new DataGridViewRow();

                    // Copia los valores de las tres primeras celdas
                    for (int colIndex = 0; colIndex < 2; colIndex++)
                    {
                        nuevaFila.Cells.Add(new DataGridViewTextBoxCell()
                        {
                            Value = filaDgvConsulta.Cells[colIndex].Value
                        });
                    }

                    // Agrega la fila al Dgv_Orden
                    Dgv_Orden.Rows.Add(nuevaFila);

                }

                // Agrega la nueva columna con el valor de txt_OrdenAnalisis en todas las filas

                DataGridViewTextBoxColumn nuevaColumna = new DataGridViewTextBoxColumn();
                nuevaColumna.Name = "NuevaColumna";
                nuevaColumna.HeaderText = "Nro Orden";
                Dgv_Orden.Columns.Add(nuevaColumna);

                foreach (DataGridViewRow fila in Dgv_Orden.Rows)
                {
                    fila.Cells["NuevaColumna"].Value = txt_Orden.Text;
                }
            }
        }

        private void recibir_datos1()
        {
            // Verifica si hay filas en el Dgv_Consulta
            if (Dgv_Consulta.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(txt_Orden.Text))
                {
                    MessageBox.Show("Por favor, Crear un nuevo despacho.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Itera sobre las filas del Dgv_Consulta
                foreach (DataGridViewRow filaDgvConsulta in Dgv_Consulta.Rows)
                {
                    // Crea una nueva fila en el Dgv_Orden
                    int rowIndex = Dgv_Orden.Rows.Add();

                    // Copia los valores de las dos primeras celdas del Dgv_Consulta
                    Dgv_Orden.Rows[rowIndex].Cells["Item"].Value = filaDgvConsulta.Cells[0].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["blasthole"].Value = filaDgvConsulta.Cells[1].Value;

                    // Establece el valor de la columna "NuevaColumna" en el Dgv_Orden
                    //Dgv_Orden.Rows[rowIndex].Cells["NuevaColumna"].Value = txt_OrdenAnalisis.Text;
                }
            }
        }

        // AGREGAR AUTOMATICAMENTE EL CODIGO DE MUESTRA 

        private void btnFillConsecutive_Click()
        {
            try
            {
                // Obtener el número ingresado en txt_orden
                string numeroOrden = txt_Orden.Text.Trim();

                // Verificar que el número de orden sea válido
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Ingrese un número de orden válido.");
                    return;
                }

                // Calcular el consecutivo
                int consecutivo = ObtenerConsecutivo(numeroOrden);

                // Verificar que el consecutivo sea válido
                if (consecutivo <= 0)
                {
                    MessageBox.Show("No se pudo obtener el consecutivo.");
                    return;
                }

                // Llenar la columna CodMuestra del Dgv_Orden con el consecutivo
                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    row.Cells["CodMuestra"].Value = numeroOrden + consecutivo.ToString("D2");
                    consecutivo++;
                }

                MessageBox.Show("Consecutivo llenado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar el consecutivo: " + ex.Message);
            }
        }

        private int ObtenerConsecutivo(string numeroOrden)
        {
            // Aquí puedes implementar la lógica para obtener el consecutivo, por ejemplo:
            // Consultar la base de datos, calcular en base a ciertas reglas, etc.
            // En este ejemplo simple, solo se toma el último número de la orden y se incrementa en 1.

            string ultimoNumero = numeroOrden.Substring(numeroOrden.Length - 2);
            if (int.TryParse(ultimoNumero, out int consecutivo))
            {
                return consecutivo;
            }
            else
            {
                return 0; // Error al obtener el consecutivo
            }
        }


        //AGREGAR LOS ANALITOS 
        private void AgregarColumnasCheckBoxAdicionales()

        {


            // Crear la columna MCtrl
            DataGridViewTextBoxColumn columnMCtrl = new DataGridViewTextBoxColumn();
            columnMCtrl.HeaderText = "MCtrl";
            columnMCtrl.Name = "MCtrl";
            columnMCtrl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnMCtrl.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Dgv_Orden.Columns.Add(columnMCtrl);

            // Crear la columna MCtrlB
            DataGridViewTextBoxColumn columnMCtrlB = new DataGridViewTextBoxColumn();
            columnMCtrlB.HeaderText = "Parent";
            columnMCtrlB.Name = "Parent";
            columnMCtrlB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnMCtrlB.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Dgv_Orden.Columns.Add(columnMCtrlB);



            DataGridViewCheckBoxColumn checkBoxColumn2 = new DataGridViewCheckBoxColumn();
            checkBoxColumn2.HeaderText = "CuTot";
            checkBoxColumn2.Name = "CuTot";
            Dgv_Orden.Columns.Add(checkBoxColumn2);
            Dgv_Orden.Columns["CuTot"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CuTot"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewCheckBoxColumn checkBoxColumn3 = new DataGridViewCheckBoxColumn();
            checkBoxColumn3.HeaderText = "CuOx";
            checkBoxColumn3.Name = "CuOx";
            Dgv_Orden.Columns.Add(checkBoxColumn3);
            Dgv_Orden.Columns["CuOx"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CuOx"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewCheckBoxColumn checkBoxColumn4 = new DataGridViewCheckBoxColumn();
            checkBoxColumn4.HeaderText = "CuSol";
            checkBoxColumn4.Name = "CuSol";
            Dgv_Orden.Columns.Add(checkBoxColumn4);
            Dgv_Orden.Columns["CuSol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CuSol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "U"
            DataGridViewCheckBoxColumn checkBoxColumnU = new DataGridViewCheckBoxColumn();
            checkBoxColumnU.HeaderText = "Au"; // Encabezado
            checkBoxColumnU.Name = "Au"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnU);
            Dgv_Orden.Columns["Au"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["Au"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "G"
            DataGridViewCheckBoxColumn checkBoxColumnG = new DataGridViewCheckBoxColumn();
            checkBoxColumnG.HeaderText = "Ag"; // Encabezado
            checkBoxColumnG.Name = "Ag"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnG);
            Dgv_Orden.Columns["Ag"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["Ag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "M"
            DataGridViewCheckBoxColumn checkBoxColumnM = new DataGridViewCheckBoxColumn();
            checkBoxColumnM.HeaderText = "Mo"; // Encabezado
            checkBoxColumnM.Name = "Mo"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnM);
            Dgv_Orden.Columns["Mo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["Mo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            // Agregar columna "C3"
            DataGridViewCheckBoxColumn checkBoxColumnC3 = new DataGridViewCheckBoxColumn();
            checkBoxColumnC3.HeaderText = "CO3"; // Encabezado
            checkBoxColumnC3.Name = "CO3"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnC3);
            Dgv_Orden.Columns["CO3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CO3"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "CS"
            DataGridViewCheckBoxColumn checkBoxColumnCS = new DataGridViewCheckBoxColumn();
            checkBoxColumnCS.HeaderText = "CSAc"; // Encabezado
            checkBoxColumnCS.Name = "CSAc"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnCS);
            Dgv_Orden.Columns["CSAc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CSAc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "CN" (segunda vez)
            DataGridViewCheckBoxColumn checkBoxColumnCN2 = new DataGridViewCheckBoxColumn();
            checkBoxColumnCN2.HeaderText = "CSCn"; // Encabezado
            checkBoxColumnCN2.Name = "CSCn"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnCN2);
            Dgv_Orden.Columns["CSCn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CSCn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "CR"
            DataGridViewCheckBoxColumn checkBoxColumnCR = new DataGridViewCheckBoxColumn();
            checkBoxColumnCR.HeaderText = "CuRes"; // Encabezado
            checkBoxColumnCR.Name = "CuRes"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnCR);
            Dgv_Orden.Columns["CuRes"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["CuRes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Agregar columna "CS" (segunda vez)
            DataGridViewCheckBoxColumn checkBoxColumnCS2 = new DataGridViewCheckBoxColumn();
            checkBoxColumnCS2.HeaderText = "FeTot"; // Encabezado
            checkBoxColumnCS2.Name = "FeTot"; // Nombre único
            Dgv_Orden.Columns.Add(checkBoxColumnCS2);
            Dgv_Orden.Columns["FeTot"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Orden.Columns["FeTot"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


        }

        //CARGAR DATOS  AL COMBOBOX DE DUPLICADO 

        public void cargar_duplicado()
        {
            string query = "select ASSAY_SAMPLE_TYPE_CODE, assay_sample_type_desc from ASSAY_SAMPLE_TYPE where assay_sample_type_category = 'QC'";
            OdbcCommand cmd = new OdbcCommand(query, con);
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["assay_sample_type_desc"] = "Seleccionar";
            dt.Rows.InsertAt(fila, 0);
            //Control del ComboBox

            MCtrl.ValueMember = "ASSAY_SAMPLE_TYPE_CODE";
            MCtrl.DisplayMember = "assay_sample_type_desc";
            MCtrl.DataSource = dt;
        }

        //CARGAR DATOS  COMBOBOX DE CONTROLES BLANCOS


        public void cargar_CBlanco()
        {
            string query = "select b.ASSAY_STANDARD_CODE,a.business_unit_name \r\nfrom reference_code_assignments as \r\na  inner join ASSAY_STANDARDS b on \r\na.reference_code_id = b.REFERENCE_CODE_ID \r\nwhere column_name = 'ASSAY_STANDARD_CODE' and a.business_unit_name='EXPLORACION' group by b.ASSAY_STANDARD_CODE,a.business_unit_name";
            OdbcCommand cmd = new OdbcCommand(query, con);
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["ASSAY_STANDARD_CODE"] = "Seleccionar";
            dt.Rows.InsertAt(fila, 0);
            //Control del ComboBox

            cbo_CtrlB.ValueMember = "business_unit_name";
            cbo_CtrlB.DisplayMember = "ASSAY_STANDARD_CODE";
            cbo_CtrlB.DataSource = dt;

        }

        // CARGAR DATOS  A COMBOX DE LABORATORY

        public void cargar_laboratory()
        {
            string query = "select laboratory_name from LABORATORY";
            OdbcCommand cmd = new OdbcCommand(query, con);
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow fila = dt.NewRow();
            fila["laboratory_name"] = "Seleccionar";
            dt.Rows.InsertAt(fila, 0);
            //Control del ComboBox

            cbo_Laborat.ValueMember = "laboratory_name";
            cbo_Laborat.DisplayMember = "laboratory_name";
            cbo_Laborat.DataSource = dt;

        }

        //AUTO COMPLETAR EL CODIGO MUESTRA PARA EVITAR DUPLICADO
       
        public void completecodBh(System.Windows.Forms.TextBox cajaTexto)
        {
          
            try
            {
                string query = "SELECT order_prep_guid FROM [dbo].[UDEF_ORDER_PREP] ORDER BY order_prep_guid ASC";
                OdbcCommand command = new OdbcCommand(query, con);

                OdbcDataReader reader = command.ExecuteReader();
                var source = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    source.Add(reader["order_prep_guid"].ToString());
                }
                reader.Close();
                cajaTexto.AutoCompleteCustomSource = source;

                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo autocompletar el Textbox: " + ex.ToString());
            }
        }

        private void txt_OrdenChanged(object sender, EventArgs e)
        {
            txt_Orden.MaxLength = 11;
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form_Orden_Load(object sender, EventArgs e)
        {

        }

        private void Tb_Consultar_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void btn_consultar_Click(object sender, EventArgs e)
        {
            listar_datos();
            cargar_laboratory();
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Dgv_Orden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_crear_Click(object sender, EventArgs e)
        {
            
            recibir_datos1();
            //AgregarColumnasCheckBoxAdicionales();
            cargar_duplicado();
            cargar_CBlanco();

            lblcount.Text = Dgv_Orden.Rows.Count.ToString();

            btnFillConsecutive_Click();

        }

        private void txt_OrdenAnalisis_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_CuTot_CheckedChanged(object sender, EventArgs e)
        {

            bool marcarTodo = cbo_CuTot.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CuTot"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }
        }

        private void cbo_CuOxi_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_CuOxi.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CuOx"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_CuSol_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_CuSol.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CuSol"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }
        }

        private void cbo_Au_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_Au.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Au"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_Ag_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_Ag.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Ag"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_Mo_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_Mo.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Mo"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }


        private void cbo_CO3_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_CO3.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CO3"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_CSAc_CheckedChanged(object sender, EventArgs e)
        {

            bool marcarTodo = cbo_CuSAc.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CSAc"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_CSCn_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_CuSCn.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CSCn"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_CuRes_CheckedChanged(object sender, EventArgs e)
        {

            bool marcarTodo = cbo_CuRes.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CuRes"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        private void cbo_FeTot_CheckedChanged(object sender, EventArgs e)
        {
            bool marcarTodo = cbo_FeTot.Checked;
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener la celda de la columna "T" en la fila actual
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["FeTot"] as DataGridViewCheckBoxCell;

                // Marcar o desmarcar la casilla dependiendo del estado de marcarTodo
                checkBoxCell.Value = marcarTodo;
            }

        }

        // BOTON PARA GUARDAR EL REPORTE EN PDF

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog Guardar = new SaveFileDialog();
            // Especificar el nombre del archivo
            Guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            Guardar.DefaultExt = ".pdf"; // Establecer la extensión predeterminada
            Guardar.Filter = "Archivos PDF (*.pdf)|*.pdf";


            string paginahtml_texto = Properties.Resources.plantilla.ToString();

            paginahtml_texto = paginahtml_texto.Replace("@NumOrden", txt_Orden.Text);
            //paginahtml_texto = paginahtml_texto.Replace("@MUESTRERIA", txt_OrdenAnalisis.Text);
            //paginahtml_texto = paginahtml_texto.Replace("@FECHA", txt_OrdenAnalisis.Text);
            paginahtml_texto = paginahtml_texto.Replace("@LAB", cbo_Laborat.Text);
            paginahtml_texto = paginahtml_texto.Replace("@CODPROJECT", cbo_proyecto.Text);
            //paginahtml_texto = paginahtml_texto.Replace("@G.MINA", txt_OrdenAnalisis.Text);
            paginahtml_texto = paginahtml_texto.Replace("@FECHAPREP", DateTime.Now.ToString("dd/MM/yyyy"));
            paginahtml_texto = paginahtml_texto.Replace("@CODBANCO", cb_banco.Text);
            paginahtml_texto = paginahtml_texto.Replace("@TOTALORDEN", lblcount.Text);

            string filas = string.Empty;

            foreach ( DataGridViewRow row in Dgv_Orden.Rows )
           {

                // Obtener el valor de la celda "CuTot"
                object CuTotValue = row.Cells["CuTot"].Value;

                // Verificar si el valor de la celda es un booleano y está marcado como verdadero
                bool isChecked = CuTotValue != null && CuTotValue is bool && (bool)CuTotValue;

                // Crear la fila de la tabla HTML con el checkbox marcado si isChecked es verdadero
                filas += "<tr>";
                filas += "<td>" + (row.Cells["Item"].Value ?? "").ToString() + "</td>";
                filas += "<td>" + (row.Cells["CodMuestra"].Value ?? "").ToString() + "</td>";
                filas += "<td>" + (row.Cells["observaciones"].Value ?? "").ToString() + "</td>";
                filas += "<td><input type='checkbox' " + (isChecked ? "checked" : "") + " /></td>";
                filas += "</tr>";

            }


            paginahtml_texto = paginahtml_texto.Replace("@FILAS", filas);

            string rutaArchivo = @"C:\Users\joel.vilcatoma\OneDrive - Vela Industries Group\Escritorio\miArchivo.html";
            File.WriteAllText(rutaArchivo, paginahtml_texto);

            if (Guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(Guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    //pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.logoAntapaccay, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(80, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.LeftMargin + 10, pdfDoc.Top - 23);
                    pdfDoc.Add(img);



                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {

                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

                    }

                    pdfDoc.Close();
                    stream.Close();

                }

            }


        }


        //Crear CONTROLES Y BLANCOS 


    //----------------------------

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            proyecto_number();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void BtnBlancos_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo_CtrlB.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un valor del campo 'MCtrl'.");
                    return;
                }

                int rowIndex = -1;
                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.Cells["sdk"].Value != null && (bool)row.Cells["sdk"].Value)
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }

                // Obtener el siguiente número correlativo para la columna "item"
                long siguiente = 0;
                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (!row.IsNewRow && !string.IsNullOrEmpty(row.Cells["item"].Value?.ToString()))
                    {
                        long cadena = Convert.ToInt64(row.Cells["item"].Value.ToString());
                        if (cadena > siguiente)
                        {
                            siguiente = cadena;
                        }
                    }
                }
                siguiente++;

                // Si no se ha seleccionado ninguna fila, se agrega al inicio
                if (rowIndex == -1)
                {
                    rowIndex = 0;
                    Dgv_Orden.Rows.Insert(rowIndex, 1);
                }
                else
                {
                    // Se agrega después de la fila seleccionada
                    rowIndex++;
                    Dgv_Orden.Rows.Insert(rowIndex, 1);
                }

                // Inicializamos el valor de las celdas
                DataGridViewRow newRow = Dgv_Orden.Rows[rowIndex];
                newRow.Cells["CodMuestra"].Value = siguiente.ToString("00000000");
                newRow.Cells["observaciones"].Value = cbo_CtrlB.SelectedItem.ToString();
                //newRow.Cells["Dispatch"].Value = txt_Orden.Text;
                //newRow.Cells["Hole"].Value = comboBox3.SelectedValue ?? ""; // Ajustar según la columna correspondiente

                // Establecemos el valor del check en false para la fila seleccionada
                if (rowIndex >= 0 && rowIndex < Dgv_Orden.Rows.Count)
                {
                    Dgv_Orden.Rows[rowIndex].Cells["sdk"].Value = false;
                }

                // Limpiamos el combo y actualizamos el label con la cantidad de filas
                cbo_CtrlB.SelectedIndex = -1;
                lblcant.Text = Dgv_Orden.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el Control: " + ex.Message);
            }
        }

    }
}
