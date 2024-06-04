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
using iTextSharp.text.pdf; // Comentar esta línea
using iTextSharp.text; // Comentar esta línea
using iTextSharp.tool.xml; // Comentar esta línea
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;
using BarcodeStandard;
//using iText.Kernel.Pdf; // Mantener esta línea
//using iText.Layout; // Mantener esta línea
//using iText.Layout.Element; // Mantener esta línea
//using BarcodeLib.Barcode;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using BarcodeLib;
using System.Drawing.Configuration;
using iText.Layout.Properties;
using ZXing;





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
            Dgv_Orden.CellValueChanged += Dgv_Orden_CellValueChanged;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            //INICIALIZAR 
            //Dgv_Orden.CellPainting += Dgv_Orden_CellPainting;
            //Dgv_Orden.MouseClick += Dgv_Orden_MouseClick;

            // Configurar el evento CheckedChanged del CheckBox
            //cbo_CuTot.CheckedChanged += HeaderCheckBox_CheckedChanged;
            // Inicialmente ocultamos el CheckBox para evitar que aparezca como control aparte

            cbo_proyecto.SelectedIndexChanged += cbo_proyecto_SelectedIndexChanged;

            // para no marcar mas de 2 veces el sdk
            Dgv_Orden.CellContentClick += Dgv_Orden_CellContentClick1;

            Dgv_Orden.CellContentClick += Dgv_Orden_CellContentClick;

            cbProyectoGeolo.SelectedIndexChanged += new EventHandler(cbProyectoGeolo_SelectedIndexChanged);

        }
        //


        //Listado de datos en DATAGRIDVIEW
        public void listar_datos()
        {
            try
            {
                string numeroDespacho = codPreparacion.Text;


                string query = "Select CODE_PREP, CAST(BH_ID AS INT), MUESTRA_CONTROL, BH_PARENT, PROYECTO_GEOLOGIA, SAMPLE_CODE TAJO, FASE, FECHA_ENTREGA, HORA_ENTREGA from UDEF_ORDER_PREP WHERE CODE_PREP = ? ";


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

        //DESMARCAR LOS CHECKBOX DE DGV

        private void DesmarcarColumnaSdk()
        {
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                if (row.Cells["sdk"] is DataGridViewCheckBoxCell)
                {
                    row.Cells["sdk"].Value = false;
                }
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

        // LENAR EL COMBO BOX DEL PROYECTO 

        private void proyecto_number()
        {
            // Obtener las fechas seleccionadas
            string fechaInicio = dtp_inicio.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string fechaFin = dtp_fin.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Crear la consulta SQL con las fechas seleccionadas
            string query = $"select proyecto,BLASTHOLE_guid from UDEF_BLASTHOLE where CREATION_DATE > '{fechaInicio}' and CREATION_DATE < '{fechaFin}'";

            // Crear el comando ODBC con la consulta
            using (OdbcCommand cmd = new OdbcCommand(query, con))
            {
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
                cbo_proyecto.ValueMember = "BLASTHOLE_guid";
                cbo_proyecto.DisplayMember = "proyecto";
                cbo_proyecto.DataSource = dt;
            }
        }

        // Llenar el ComboBox del proyecto geología
        private void cbo_proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            proyecto_number_geologi();
        }

        private void proyecto_number_geologi()
        {
            // Verificar que hay un proyecto seleccionado
            if (cbo_proyecto.SelectedValue != null && cbo_proyecto.SelectedValue.ToString() != "Selecciona un Proyecto geología ")
            {
                // Obtener el proyecto seleccionado usando SelectedValue
                string proyecto = cbo_proyecto.SelectedValue.ToString();

                // Crear la consulta SQL con el proyecto seleccionado
                string query = $"SELECT DISTINCT(PROYECTO_GEOLOGIA), BLASTHOLE_GUID FROM UDEF_LOG_BLASTHOLE WHERE BLASTHOLE_GUID = '{proyecto}'";

                // Crear el comando ODBC con la consulta
                using (OdbcCommand cmd = new OdbcCommand(query, con))
                {
                    // Abrir la conexión si no está abierta
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Crear un adaptador ODBC
                    OdbcDataAdapter da = new OdbcDataAdapter(cmd);

                    // Crear un nuevo DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Llenar el DataTable con los resultados de la consulta
                    da.Fill(dt);

                    // Crear una nueva fila en el DataTable con un valor inicial especial
                    DataRow fila = dt.NewRow();
                    fila["PROYECTO_GEOLOGIA"] = "Selecciona un Proyecto de geología";
                    //fila["BLASTHOLE_GUID"] = DBNull.Value; // Si no tienes un GUID para "Selecciona un Proyecto"
                    dt.Rows.InsertAt(fila, 0);


                    // Configurar el ComboBox cbProyectoGeolo
                    cbProyectoGeolo.ValueMember = "PROYECTO_GEOLOGIA";
                    cbProyectoGeolo.DisplayMember = "PROYECTO_GEOLOGIA";
                    cbProyectoGeolo.DataSource = dt;

                    // Seleccionar el primer ítem por defecto
                    if (cbProyectoGeolo.Items.Count > 0)
                    {
                        cbProyectoGeolo.SelectedIndex = 0;
                    }

                    // Cerrar la conexión
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un proyecto.");
            }
        }

        // Llenar el ComboBox de preparación

        private void cbProyectoGeolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarCodPreparacion();
        }


        private void LlenarCodPreparacion()
        {
            // Verificar que hay un proyecto seleccionado en cbProyectoGeolo
            if (cbProyectoGeolo.SelectedValue != null && cbProyectoGeolo.SelectedValue.ToString() != "Selecciona un Codigo de Preparación ")
            {
                // Obtener el valor seleccionado en cbProyectoGeolo
                string proyecto = cbProyectoGeolo.SelectedValue.ToString();

                // Crear la consulta SQL con el proyecto seleccionado
                string query = $"SELECT DISTINCT(CODE_PREP), BLASTHOLE_GUID FROM UDEF_ORDER_PREP WHERE PROYECTO_GEOLOGIA = '{proyecto}'";

                // Crear el comando ODBC con la consulta
                using (OdbcCommand cmd = new OdbcCommand(query, con))
                {
                    // Abrir la conexión si no está abierta
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Crear un adaptador ODBC
                    OdbcDataAdapter da = new OdbcDataAdapter(cmd);

                    // Crear un nuevo DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Llenar el DataTable con los resultados de la consulta
                    da.Fill(dt);

                    // Crear una nueva fila en el DataTable con un valor inicial especial
                    DataRow fila = dt.NewRow();
                    fila["CODE_PREP"] = "Selecciona una preparación";
                    dt.Rows.InsertAt(fila, 0);

                    // Configurar el ComboBox codPreparacion
                    codPreparacion.ValueMember = "CODE_PREP";
                    codPreparacion.DisplayMember = "CODE_PREP";
                    codPreparacion.DataSource = dt;

                    // Seleccionar el primer ítem por defecto
                    if (codPreparacion.Items.Count > 0)
                    {
                        codPreparacion.SelectedIndex = 0;
                    }

                    // Cerrar la conexión
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un proyecto.");
            }
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
                    Dgv_Orden.Rows[rowIndex].Cells["Cod_Prep"].Value = filaDgvConsulta.Cells[1].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["blasthole"].Value = filaDgvConsulta.Cells[2].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Control"].Value = filaDgvConsulta.Cells[3].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["parent"].Value = filaDgvConsulta.Cells[4].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Proyecto_geologia"].Value = filaDgvConsulta.Cells[5].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Tajo"].Value = filaDgvConsulta.Cells[6].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Fase"].Value = filaDgvConsulta.Cells[7].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Fecha_Entrega"].Value = filaDgvConsulta.Cells[8].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Hora_Entrega"].Value = filaDgvConsulta.Cells[9].Value;


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
                string numeroOrden = txSampleNumber.Text.Trim();

                // Verificar que el número de orden sea válido
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Ingrese un número de orden válido.");
                    return;
                }

                // Convertir el número a entero y sumarle 1
                Int64 numero = Int64.Parse(numeroOrden) ;

                // Verificar que el DataGridView tenga al menos una fila
                if (Dgv_Orden.Rows.Count == 0)
                {
                    MessageBox.Show("Agregue al menos una fila al DataGridView.");
                    return;
                }

                // Llenar la columna CodMuestra del Dgv_Orden con el número capturado y sumado 1
                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    row.Cells["CodMuestra"].Value = numero.ToString();
                    numero++;
                }

                MessageBox.Show("SUCCESSFULLY...");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar los números: " + ex.Message);
            }
        }

        //CARGAR DATOS  AL COMBOBOX DE DUPLICADO 

        public void cargar_duplicado()
        {
            string query = "select ASSAY_SAMPLE_TYPE_CODE, assay_sample_type_desc from ASSAY_SAMPLE_TYPE where ASSAY_SAMPLE_TYPE_CODE IN ('MDO','MDC','MDG','MDF') ORDER BY ASSAY_SAMPLE_TYPE_CODE DESC";
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

            cbo_CtrlB.ValueMember = "ASSAY_STANDARD_CODE";
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

        //AUTO COMPLETAR EL CODIGO DE DISPATCH_NUMBER ORDEN DE ENSAYO

        public void LlenarTextBoxConResultadoSQL()
        {
            try
            {
                // Asegúrate de abrir la conexión
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                // Define tu consulta SQL
                string query = "SELECT MAX(dispatch_number) FROM DHL_SAMPLE_DISPATCH_HEADER WHERE dispatch_number NOT LIKE '%E%' AND dispatch_number NOT LIKE '%A%'";

                // Crear el comando SQL
                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    // Ejecutar el comando y obtener el resultado
                    object result = command.ExecuteScalar();

                    // Asignar el resultado al TextBox, convirtiendo a string explícitamente
                    if (result != null)
                    {
                        string lastFourDigits = result.ToString().Substring(result.ToString().Length - 4); // Captura los últimos 4 dígitos
                        int nextNumber = int.Parse(lastFourDigits) + 1; // Suma 1 al número obtenido
                        string nextDispatchNumber = result.ToString().Substring(0, result.ToString().Length - 4) + nextNumber.ToString().PadLeft(4, '0'); // Concatena el número con el próximo número de despacho
                        txt_Orden.Text = nextDispatchNumber; // Mostrar el resultado en el TextBox
                    }
                    else
                    {
                        txt_Orden.Text = "No result found";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Asegúrate de cerrar la conexión
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        //AUTO COMPLETAR EL CODIGO DE ANALISIS

        public void LlenarTextBoxCodAnalisisSQL()
        {
            try
            {
                // Asegúrate de abrir la conexión
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                // Define tu consulta SQL
                string query = "select max(SAMPLE_NUMBER) from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER not like '%E%' and SAMPLE_NUMBER not like '%A%'";

                // Crear el comando SQL
                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    // Ejecutar el comando y obtener el resultado
                    object result = command.ExecuteScalar();

                    // Asignar el resultado al TextBox, convirtiendo a string explícitamente
                    if (result != null)
                    {
                        string lastFourDigits = result.ToString().Substring(result.ToString().Length - 4); // Captura los últimos 4 dígitos
                        int nextNumber = int.Parse(lastFourDigits) + 1; // Suma 1 al número obtenido
                        string nextDispatchNumber = result.ToString().Substring(0, result.ToString().Length - 4) + nextNumber.ToString().PadLeft(4, '0'); // Concatena el número con el próximo número de despacho
                        txSampleNumber.Text = nextDispatchNumber; // Mostrar el resultado en el TextBox
                    }
                    else
                    {
                        txSampleNumber.Text = "No result found";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Asegúrate de cerrar la conexión
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }




        private void txt_OrdenChanged(object sender, EventArgs e)
        {
            txt_Orden.MaxLength = 11;
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
            //Completar el ultimo orden de ensayo    
            LlenarTextBoxConResultadoSQL();
            LlenarTextBoxCodAnalisisSQL();
            //Completar el ultimo codigo de analisis 
            //completecodsamplenumber(txSampleNumber);
            Dgv_Consulta.ReadOnly = true;
            label7.Text = Dgv_Consulta.Rows.Count.ToString();

            //LLENAR DISPACH
            //completecodsamplenumber(txSampleNumber);
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
            //cargar_duplicado();
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

        //

        private void Dgv_Orden_CellContentClick1(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que el click fue en la columna de CheckBox
            if (e.ColumnIndex == Dgv_Orden.Columns["sdk"].Index && e.RowIndex >= 0)
            {
                // Desmarca todas las casillas excepto la actual
                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        DataGridViewCheckBoxCell cell = row.Cells["sdk"] as DataGridViewCheckBoxCell;
                        if (cell != null)
                        {
                            cell.Value = false;
                        }
                    }
                }

                // Marca la casilla seleccionada
                DataGridViewCheckBoxCell currentCell = Dgv_Orden.Rows[e.RowIndex].Cells["sdk"] as DataGridViewCheckBoxCell;
                if (currentCell != null)
                {
                    currentCell.Value = true;
                }
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
            paginahtml_texto = paginahtml_texto.Replace("@TOTALORDEN", lblcount.Text);

            string filas = string.Empty;

            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener el valor de la celda "CuTot"
                object CuTotValue = row.Cells["CuTot"].Value;
                // Verificar si el valor de la celda es un booleano y está marcado como verdadero
                bool isCheckedCuTot = CuTotValue != null && CuTotValue is bool && (bool)CuTotValue;

                // Obtener el valor de la celda "CuOx"
                object CuOxValue = row.Cells["CuOx"].Value;
                // Verificar si el valor de la celda es un booleano y está marcado como verdadero
                bool isCheckedCuOx = CuOxValue != null && CuOxValue is bool && (bool)CuOxValue;

                // Crear la fila de la tabla HTML
                filas += "<tr>";
                filas += "<td>" + (row.Cells["Item"].Value ?? "").ToString() + "</td>";
                filas += "<td>" + (row.Cells["CodMuestra"].Value ?? "").ToString() + "</td>";
                filas += "<td>" + (row.Cells["Control"].Value ?? "").ToString() + "</td>";
                filas += "<td>" + (isCheckedCuTot ? "SI" : "NO") + "</td>";
                filas += "<td>" + (isCheckedCuOx ? "SI" : "NO") + "</td>";
                filas += "</tr>";
            }


            paginahtml_texto = paginahtml_texto.Replace("@FILAS", filas);

            //string rutaArchivo = @"C:\Users\joel.vilcatoma\OneDrive - Vela Industries Group\Escritorio\miArchivo.html";
           // File.WriteAllText(rutaArchivo, paginahtml_texto);

            if (Guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(Guardar.FileName, FileMode.Create))
                {
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 25, 25, 25, 25);

                    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, stream);

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


        //Crear PARENT 

        private void GRV_DATOS_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Dgv_Orden.CurrentCell.ColumnIndex == Dgv_Orden.Columns["MCtrl"].Index && e.Control is System.Windows.Forms.ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // Evitar duplicados de eventos
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox comboBox && comboBox.SelectedItem != null)
            {
                DataGridViewRow currentRow = Dgv_Orden.CurrentRow;

                if (comboBox.SelectedItem.ToString() == "Duplicado Original" && currentRow != null)
                {
                    // Obtener el valor de CodMuestra en la fila actual
                    string codMuestraValue = currentRow.Cells["blasthole"].Value?.ToString();

                    // Asignar el valor de CodMuestra a la columna parent
                    //currentRow.Cells["parent"].Value = codMuestraValue;

                    // Generar 3 filas automáticamente con el mismo CodMuestra en la columna parent
                    for (int i = 0; i < 3; i++)
                    {
                        int newRowIndex = Dgv_Orden.Rows.Add();
                        DataGridViewRow newRow = Dgv_Orden.Rows[newRowIndex];
                        newRow.Cells["parent"].Value = codMuestraValue;
                        newRow.Cells["blasthole"].Value = codMuestraValue; // Si quieres también copiar CodMuestra
                    }

                    // Enumerar las filas en la columna "item"
                    for (int i = 0; i < Dgv_Orden.Rows.Count; i++)
                    {
                        Dgv_Orden.Rows[i].Cells["item"].Value = (i + 1).ToString();
                    }

                    // Llamar al método btnFillConsecutive_Click() si es necesario
                    btnFillConsecutive_Click();

                    // Actualizar el label lblcount con el conteo de filas
                    lblcount.Text = Dgv_Orden.Rows.Count.ToString();
                }
            }
        }

        // Método adicional para manejar cambios en el valor del ComboBox directamente en la celda
        private void Dgv_Orden_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Dgv_Orden.Columns["MCtrl"].Index && e.RowIndex >= 0)
            {
                string selectedValue = Dgv_Orden.Rows[e.RowIndex].Cells["MCtrl"].Value?.ToString();
                if (selectedValue == "Duplicado Original")
                {
                    string codMuestraValue = Dgv_Orden.Rows[e.RowIndex].Cells["CodMuestra"].Value?.ToString();
                    Dgv_Orden.Rows[e.RowIndex].Cells["parent"].Value = codMuestraValue;
                }
            }
        }

        //-------------IMPRIMIR CODIGO DE BARRA -----------------




        //--------------------------------------------------------

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

        //AGREGAR BLANCOS 

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

                DataGridViewRow newRow = Dgv_Orden.Rows[rowIndex];
                newRow.Cells["Control"].Value = cbo_CtrlB.SelectedValue.ToString();



                //NUMERAMOS NUEVAMENTE  LOS ITEMS : 

                for (int i = 0; i < Dgv_Orden.Rows.Count; i++)
                {
                    // Obtener la celda de la columna "item" en la fila actual y asignarle el número de fila más uno
                    Dgv_Orden.Rows[i].Cells["item"].Value = (i + 1).ToString();
                }

                // Establecemos el valor del check en false para la fila seleccionada
                if (rowIndex >= 0 && rowIndex < Dgv_Orden.Rows.Count)
                {
                    Dgv_Orden.Rows[rowIndex].Cells["sdk"].Value = false;
                }

                //Contar las filas

                lblcount.Text = Dgv_Orden.Rows.Count.ToString();

                // Limpiamos el combo y actualizamos el label con la cantidad de filas
                cbo_CtrlB.SelectedIndex = -1;
                lblcant.Text = Dgv_Orden.Rows.Count.ToString();

                btnFillConsecutive_Click();

                DesmarcarColumnaSdk();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el Control: " + ex.Message);
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_guardar2_Click(object sender, EventArgs e)
        {
            SaveFileDialog Guardar = new SaveFileDialog();
            // Especificar el nombre del archivo
            Guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            Guardar.DefaultExt = ".pdf"; // Establecer la extensión predeterminada
            Guardar.Filter = "Archivos PDF (*.pdf)|*.pdf";


            string paginahtml_texto = Properties.Resources.Plantilla2.ToString();

            string filas = string.Empty;


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
                    img.SetAbsolutePosition(pdfDoc.RightMargin + 470, pdfDoc.Top - 40);
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


        // IMPRIMIR CODIGO DE BARRA 

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            saveFileDialog.DefaultExt = ".pdf";
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    // Crear un documento PDF con el tamaño especificado
                    Document pdfDoc = new Document(new iTextSharp.text.Rectangle(226.77f, 567f)); // 8x13 cm en puntos (1 cm = 28.35 puntos)
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    // Inicializar el generador de códigos de barras
                    BarcodeWriter barcodeWriter = new BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Width = 300,
                            Height = 100
                        }
                    };

                    // Obtener los códigos de la columna "CodMuestra" del DataGridView
                    foreach (DataGridViewRow row in Dgv_Orden.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string codigo = row.Cells["CodMuestra"].Value.ToString();
                            string blasHole = row.Cells["blasthole"].Value.ToString();

                            // Generar el código de barras
                            Bitmap barcodeBitmap = barcodeWriter.Write(codigo);

                            // Convertir el Bitmap a un array de bytes
                            using (MemoryStream ms = new MemoryStream())
                            {
                                barcodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] barcodeBytes = ms.ToArray();

                                // Convertir el array de bytes a una imagen iTextSharp
                                iTextSharp.text.Image barcodeImage = iTextSharp.text.Image.GetInstance(barcodeBytes);
                                barcodeImage.ScaleToFit(200, 50);

                                // Crear una tabla para organizar el contenido
                                PdfPTable table = new PdfPTable(1);
                                table.WidthPercentage = 100;
                                table.DefaultCell.Border = PdfPCell.NO_BORDER;

                                // Añadir el nombre del blas_hole
                                PdfPCell cellBlasHole = new PdfPCell(new Phrase(blasHole));
                                cellBlasHole.HorizontalAlignment = Element.ALIGN_LEFT;
                                cellBlasHole.Border = PdfPCell.NO_BORDER;
                                table.AddCell(cellBlasHole);

                                // Añadir el código de barras
                                PdfPCell cellBarcode = new PdfPCell(barcodeImage);
                                cellBarcode.HorizontalAlignment = Element.ALIGN_CENTER;
                                cellBarcode.Border = PdfPCell.NO_BORDER;
                                table.AddCell(cellBarcode);

                                // Añadir "M8D" en la parte inferior izquierda
                                PdfPCell cellM8D = new PdfPCell(new Phrase("M8D"));
                                cellM8D.HorizontalAlignment = Element.ALIGN_LEFT;
                                cellM8D.Border = PdfPCell.NO_BORDER;
                                table.AddCell(cellM8D);

                                // Añadir la tabla al documento
                                pdfDoc.Add(table);

                                // Añadir un espacio entre códigos de barras
                                pdfDoc.Add(new Paragraph(" "));
                            }
                        }
                    }

                    pdfDoc.Close();
                    stream.Close();
                }
                MessageBox.Show("PDF generado con éxito.");
            }
        }

        //LIMPIAR TODO EL DGV_ORDE
        private void btn_limpiar_Click(object sender, EventArgs e)
        {

            Dgv_Orden.Rows.Clear();
            Dgv_Orden.Refresh();
            limpiarcheckssBox();
            lblcount.Text = Dgv_Orden.Rows.Count.ToString();

        }

        private void limpiarcheckssBox()
        {
            cbo_CuTot.Checked = false;
            cbo_CuOxi.Checked = false;
            cbo_CuSol.Checked = false;
            cbo_Au.Checked = false;
            cbo_Ag.Checked = false;
            cbo_Mo.Checked = false;
            cbo_CO3.Checked = false;
            cbo_CuSAc.Checked = false;
            cbo_CuSCn.Checked = false;
            cbo_CuRes.Checked = false;
            cbo_FeTot.Checked = false;
            this.Update();
            this.Refresh();
        }

        //------------------------------------
        private void lblcount_Click(object sender, EventArgs e)
        {

        }

        private void Dgv_Consulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        // checkBox al DGV_ORDEN

        private void HeaderCheckBox_Click(string columnName, System.Windows.Forms.CheckBox checkBox)
        {
            // Obtener la posición relativa al DataGridView
            Point relativePoint = Dgv_Orden.PointToClient(Cursor.Position);

            DataGridView.HitTestInfo hit = Dgv_Orden.HitTest(relativePoint.X, relativePoint.Y);

            if (hit.Type == DataGridViewHitTestType.ColumnHeader && hit.ColumnIndex == Dgv_Orden.Columns[columnName].Index)
            {
                checkBox.Checked = !checkBox.Checked;
                Dgv_Orden.InvalidateCell(hit.ColumnIndex, -1); // Forzar repintado de la celda de la cabecera
            }
        }

        private void Dgv_Orden_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBox_Click("CuTot", cbo_CuTot);
            HeaderCheckBox_Click("CuOx", cbo_CuOxi);
            HeaderCheckBox_Click("CuSol", cbo_CuSol);
            HeaderCheckBox_Click("Au", cbo_Au);
            HeaderCheckBox_Click("Ag", cbo_Ag);
            HeaderCheckBox_Click("Mo", cbo_Mo);
            HeaderCheckBox_Click("CO3", cbo_CO3);
            HeaderCheckBox_Click("CSAc", cbo_CuSAc);
            HeaderCheckBox_Click("CSCn", cbo_CuSCn);
            HeaderCheckBox_Click("CuRes", cbo_CuRes);
            HeaderCheckBox_Click("FeTot", cbo_FeTot);
        }

        //TODOS
        private void DrawCheckBoxHeader(string columnName, System.Windows.Forms.CheckBox checkBox, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == Dgv_Orden.Columns[columnName].Index)
            {
                // Limpiar la cabecera
                e.PaintBackground(e.ClipBounds, true);

                // Obtener el texto de la cabecera
                string headerText = Dgv_Orden.Columns[e.ColumnIndex].HeaderText;

                // Medir el tamaño del texto
                SizeF textSize = e.Graphics.MeasureString(headerText, e.CellStyle.Font);

                // Calcular la ubicación del texto y dibujarlo
                using (Brush brush = new SolidBrush(e.CellStyle.ForeColor))
                {
                    PointF textLocation = new PointF(e.CellBounds.X + 5, e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2);
                    e.Graphics.DrawString(headerText, e.CellStyle.Font, brush, textLocation);
                }

                // Calcular la ubicación del CheckBox al lado del texto
                int checkBoxX = e.CellBounds.X + (int)textSize.Width + 10; // 10px padding after text
                int checkBoxY = e.CellBounds.Y + (e.CellBounds.Height - checkBox.Height) / 2;

                // Dibujar el CheckBox en la cabecera
                ControlPaint.DrawCheckBox(
                    e.Graphics,
                    new System.Drawing.Rectangle(checkBoxX, checkBoxY, checkBox.Width, checkBox.Height),
                    checkBox.Checked ? ButtonState.Checked : ButtonState.Normal
                );

                e.Handled = true;
            }
        }


        private void Dgv_Orden_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DrawCheckBoxHeader("CuTot", cbo_CuTot, e);
            DrawCheckBoxHeader("CuOx", cbo_CuOxi, e);
            DrawCheckBoxHeader("CuSol", cbo_CuSol, e);
            DrawCheckBoxHeader("Au", cbo_Au, e);
            DrawCheckBoxHeader("Ag", cbo_Ag, e);
            DrawCheckBoxHeader("Mo", cbo_Mo, e);
            DrawCheckBoxHeader("CO3", cbo_CO3, e);
            DrawCheckBoxHeader("CSAc", cbo_CuSAc, e);
            DrawCheckBoxHeader("CSCn", cbo_CuSCn, e);
            DrawCheckBoxHeader("CuRes", cbo_CuRes, e);
            DrawCheckBoxHeader("FeTot", cbo_FeTot, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void cbo_CtrlB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
