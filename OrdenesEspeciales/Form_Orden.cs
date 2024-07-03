using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf; // Comentar esta línea
using iTextSharp.text; // Comentar esta línea
using iTextSharp.tool.xml; // Comentar esta línea
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;
//using BarcodeStandard;
//using iText.Kernel.Pdf; // Mantener esta línea
//using iText.Layout; // Mantener esta línea
//using iText.Layout.Element; // Mantener esta línea
//using BarcodeLib.Barcode;
//using System.Drawing.Imaging;
//using System.Runtime.InteropServices;
//using BarcodeLib;
//using System.Drawing.Configuration;
//using iText.Layout.Properties;
using ZXing;
//using Org.BouncyCastle.Math;
//using System.Configuration;
using OrdenesEspeciales.Reportes;





namespace OrdenesEspeciales
{


    public partial class Form_Orden : Form
    {

        public string Usuario { get; set; }
        public string Contraseña { get; set; }

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

            llenarComboBoxOrdenes();

        }


        //LLENAR PROYECTO_GEOLOGIA




        //Listado de datos en DATAGRIDVIEW
        public void listar_datos()
        {
            try
            {
                string numeroDespacho = codPreparacion.Text;


                string query = "Select CODE_PREP,  SAMPLE_CODE, CAST(BH_ID AS INT) AS BH_ID, MUESTRA_CONTROL, BH_PARENT, PROYECTO_GEOLOGIA, TAJO, BANCO, FASE, FECHA_ENTREGA, HORA_ENTREGA from UDEF_ORDER_PREP WHERE CODE_PREP = ? ORDER BY CASE WHEN SAMPLE_CODE IS NULL THEN 0 ELSE 1 END, CASE WHEN MUESTRA_CONTROL IS NULL THEN 0 ELSE 1 END, SAMPLE_CODE;";


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

        //LLENAR LOS ULTIMOS 10 DESPACHOS :

        private void llenarComboBoxOrdenes()
        {
            // Crear la consulta SQL
            string query = "SELECT distinct DISPATCH_CODE FROM UDEF_ORDER_ANALYSIS WHERE DISPATCH_CODE NOT LIKE '%E%' AND DISPATCH_CODE NOT LIKE '%A%' ORDER BY DISPATCH_CODE DESC";

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
                fila["DISPATCH_CODE"] = "Seleccionar";
                dt.Rows.InsertAt(fila, 0);

                // Configurar el ComboBox Cb_ordenes
                Cb_ordenes.ValueMember = "DISPATCH_CODE";
                Cb_ordenes.DisplayMember = "DISPATCH_CODE";
                Cb_ordenes.DataSource = dt;
            }
        }

        //--------------------------------------------------


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
                //string proyecto = cbProyectoGeolo.Text ;
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
                MessageBox.Show("Por favor, selecciona un proyecto de geologia ");
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
                    Dgv_Orden.Rows[rowIndex].Cells["sample_code"].Value = filaDgvConsulta.Cells[2].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["blasthole"].Value = filaDgvConsulta.Cells[3].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Control"].Value = filaDgvConsulta.Cells[4].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["parent"].Value = filaDgvConsulta.Cells[5].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Proyecto_geologia"].Value = filaDgvConsulta.Cells[6].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Tajo"].Value = filaDgvConsulta.Cells[7].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["BANCO"].Value = filaDgvConsulta.Cells[8].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Fase"].Value = filaDgvConsulta.Cells[9].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Fecha_Entrega"].Value = filaDgvConsulta.Cells[10].Value;
                    Dgv_Orden.Rows[rowIndex].Cells["Hora_Entrega"].Value = filaDgvConsulta.Cells[11].Value;



                    // Establece el valor de la columna "NuevaColumna" en el Dgv_Orden
                    //Dgv_Orden.Rows[rowIndex].Cells["NuevaColumna"].Value = txt_OrdenAnalisis.Text;
                }
            }
        }

        // LLENAR EL VALOR DE BASTHOLE PARA LA IMPRESION 

        private void FillHoleColumn()
        {
            // Copiar todos los valores de la columna "blasthole"
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                var blastholeValue = row.Cells["blasthole"].Value?.ToString();
                row.Cells["Hole"].Value = blastholeValue;
            }

            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Verificar si la columna "Hole" está vacía
                if (string.IsNullOrEmpty(row.Cells["Hole"].Value?.ToString()))
                {
                    // Obtener el valor de la columna "parent"
                    var parentValue = row.Cells["parent"].Value?.ToString();

                    // Recorrer todas las filas nuevamente para comparar los valores de "sample code"
                    foreach (DataGridViewRow compareRow in Dgv_Orden.Rows)
                    {
                        // Obtener el valor de la columna "sample code" de la fila de comparación
                        var sampleCodeValue = compareRow.Cells["sample_code"].Value?.ToString();

                        // Comparar el valor de "parent" con "sample code"
                        if (parentValue == sampleCodeValue)
                        {
                            // Obtener el valor de la columna "blasthole" de la fila de comparación
                            var blastholeValue = compareRow.Cells["blasthole"].Value?.ToString();

                            // Llenar la columna "Hole" con el valor de "blasthole"
                            row.Cells["Hole"].Value = blastholeValue;
                            break; // Salir del bucle interno una vez encontrado el valor
                        }
                    }
                }
            }
        }


        // LLEVAR LOS DATOS DE MUESTRADUPLICADO A LA COLUMNA CORRESPONDIENTE 4

        private void FillControlInColumn()
        {
            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow row in Dgv_Orden.Rows)
            {
                // Obtener el valor de la columna "parent"
                var parentValue = row.Cells["parent"].Value?.ToString();

                // Si el valor de "parent" es nulo o vacío, continuar con la siguiente fila
                if (string.IsNullOrEmpty(parentValue))
                {
                    continue;
                }

                // Recorrer todas las filas nuevamente para comparar los valores de "sample_code"
                foreach (DataGridViewRow compareRow in Dgv_Orden.Rows)
                {
                    // Obtener el valor de la columna "sample_code" de la fila de comparación
                    var sampleCodeValue = compareRow.Cells["sample_code"].Value?.ToString();

                    // Comparar el valor de "parent" con "sample_code"
                    if (parentValue == sampleCodeValue)
                    {
                        // Llenar la columna "Control_in" con el valor "MD"
                        compareRow.Cells["Control_in"].Value = "MD";
                        break; // Salir del bucle interno una vez encontrado el valor
                    }
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
                    row.Cells["CodAnalisis"].Value = numero.ToString();
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
            string query = "select LABORATORY_ID, laboratory_name from LABORATORY where LABORATORY_ID in (2,8)";
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

        // CARGAR LAB_PACKAGE 

        private void cbo_Laborat_SelectedIndexChanged(object sender, EventArgs e)
        {
            lab_package();
        }

        private void lab_package()
        {
            // Verificar que hay un proyecto seleccionado
            if (cbo_Laborat.SelectedValue != null && cbo_Laborat.SelectedValue.ToString() != "Seleccionar")
            {
                // Obtener el laboratorio seleccionado usando SelectedValue
                string laboratorio = cbo_Laborat.SelectedValue.ToString();

                // Crear la consulta SQL con el proyecto seleccionado
                string query = $"select p.LAB_PACKAGE_NAME from LABORATORY as l inner join LAB_PACKAGE as p ON l.LABORATORY_ID = p.LABORATORY_ID where l.laboratory_name =  '{laboratorio}'";

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
                    fila["LAB_PACKAGE_NAME"] = "Seleccionar ";

                    dt.Rows.InsertAt(fila, 0);

                    // Configurar el ComboBox cbo_Lab_package
                    cbo_Lab_package.ValueMember = "LAB_PACKAGE_NAME";
                    cbo_Lab_package.DisplayMember = "LAB_PACKAGE_NAME";
                    cbo_Lab_package.DataSource = dt;

                    // Seleccionar el primer ítem por defecto
                    if (cbo_Lab_package.Items.Count > 0)
                    {
                        cbo_Lab_package.SelectedIndex = 0;
                    }

                    // Cerrar la conexión
                    con.Close();
                }
            }
            else
            {
                //MessageBox.Show("Por favor, selecciona un laboratorio.");
            }
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
                string query = "select max(SAMPLE_NUMBER) from DHL_SAMPLE_DISPATCH_SAMPLES where SAMPLE_NUMBER not like '%E%' and SAMPLE_NUMBER not like '%A%' and SAMPLE_NUMBER like '24%'";

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

            limpiarcheckssBox();

            


            if (cbo_Laborat.SelectedIndex > 0)
            {
                // Ejecutar los métodos solo si se ha seleccionado un valor en cbo_Laborat
                
                recibir_datos1();
                //AgregarColumnasCheckBoxAdicionales();
                //cargar_duplicado();
                //cargar_CBlanco();
                lblcount.Text = Dgv_Orden.Rows.Count.ToString();
                btnFillConsecutive_Click();
                UpdateLabels();
                
            }
            else
            {
                MessageBox.Show("Seleccione laboratorio");
            }

            FillHoleColumn();
            FillControlInColumn();
            CopiarValorTxtOrdenATxtDespacho();

            DatosOrden.ValorOrden = txtdespacho.Text;

        }

        // DESPACHO PARA PARAMETRO 

        public void CopiarValorTxtOrdenATxtDespacho()
        {
            txtdespacho.Text = txt_Orden.Text;
        }

        public void CopiarValorTxtOrdenATxtDespacho2()
        {
            txtdespacho.Text = lbDispatch.Text;
        }
        //------------

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
            FrmVisorReporte frmVisorReporte = new FrmVisorReporte();

            //frmVisorReporte.ShowDialog();
            frmVisorReporte.MostrarReporte();


        }

        //Parametrodespaho

        public string GetDespacho
        {
            get {  return txtdespacho.Text; }
        }

        public static class DatosOrden
        {
            public static string ValorOrden { get; set; }
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
            // Verificar que el sender sea un ComboBox y que tenga un elemento seleccionado
            if (sender is System.Windows.Forms.ComboBox comboBox && comboBox.SelectedItem != null)
            {
                // Obtener la fila actual del DataGridView
                DataGridViewRow currentRow = Dgv_Orden.CurrentRow;

                // Verificar que la fila actual no sea nula
                if (currentRow != null)
                {
                    // Obtener el valor seleccionado en el ComboBox
                    string selectedValue = comboBox.SelectedItem.ToString();

                    // Condiciones para cada valor seleccionado en el ComboBox
                    switch (selectedValue)
                    {
                        case "Duplicado Original":
                            // Obtener el valor de la celda "sample_code" en la fila actual
                            string codMuestraValue = currentRow.Cells["sample_code"].Value?.ToString();
                            string codvalue = currentRow.Cells["Hole"].Value?.ToString();

                            // Generar 3 nuevas filas con el mismo CodMuestra en la columna "parent"
                            for (int i = 0; i < 3; i++)
                            {
                                int newRowIndex = Dgv_Orden.Rows.Add();
                                DataGridViewRow newRow = Dgv_Orden.Rows[newRowIndex];
                                newRow.Cells["parent"].Value = codMuestraValue;
                                newRow.Cells["Hole"].Value = codvalue;

                                // Descomentar la siguiente línea si quieres también copiar CodMuestra
                                // newRow.Cells["sample_code"].Value = codMuestraValue;
                            }

                            // Enumerar las filas en la columna "item"
                            for (int i = 0; i < Dgv_Orden.Rows.Count; i++)
                            {
                                Dgv_Orden.Rows[i].Cells["item"].Value = (i + 1).ToString();
                            }

                            // Llamar al método btnFillConsecutive_Click() si es necesario
                            btnFillConsecutive_Click();
                            FillControlInColumn();

                            // Actualizar el label lblcount con el conteo de filas
                            lblcount.Text = Dgv_Orden.Rows.Count.ToString();
                            break;

                        case "Duplicado Campo":
                            // Asignar el valor "MDC" a la columna "Control" en la fila actual
                            currentRow.Cells["Control"].Value = "MDC";
                            break;

                        case "Duplicado Grueso":
                            // Asignar el valor "MDG" a la columna "Control" en la fila actual
                            currentRow.Cells["Control"].Value = "MDG";
                            break;

                        case "Duplicado Fino":
                            // Asignar el valor "MDF" a la columna "Control" en la fila actual
                            currentRow.Cells["Control"].Value = "MDF";
                            break;
                    }
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
                    //string codMuestraValue = Dgv_Orden.Rows[e.RowIndex].Cells["CodAnalisis"].Value?.ToString();
                    //Dgv_Orden.Rows[e.RowIndex].Cells["parent"].Value = codMuestraValue;
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
                if (cbo_CtrlB.SelectedItem.ToString() == "Seleccionar")
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
                newRow.Cells["Control"].Value = cbo_CtrlB.SelectedItem?.ToString();



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
           

        }

        // IMPRIMIR CODIGO DE BARRA 

        private void button3_Click_1(object sender, EventArgs e)
        {
            Frm_Cod_Barra frmVisorReporte = new Frm_Cod_Barra();
            frmVisorReporte.ShowDialog();

        }

        //------------------------------------------

        private PdfPTable CreateNewTable()
        {
            PdfPTable table = new PdfPTable(2)
            {
                WidthPercentage = 100
            };
            table.DefaultCell.Border = PdfPCell.NO_BORDER;
            return table;
        }

        private iTextSharp.text.Image GenerateBarcodeImage(BarcodeWriter barcodeWriter, string codigo)
        {
            Bitmap barcodeBitmap = barcodeWriter.Write(codigo);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] barcodeBytes = ms.ToArray();

                iTextSharp.text.Image barcodeImage = iTextSharp.text.Image.GetInstance(barcodeBytes);

                barcodeImage.ScaleToFit(250, 70);
                barcodeImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                return barcodeImage;
            }
        }

        private PdfPTable CreateCellTable(string proyect_geolo, string blasHole, string txt_Orden_Value, string currentDate, iTextSharp.text.Image barcodeImage)
        {
            PdfPTable cellTable = new PdfPTable(2)
            {
                WidthPercentage = 100,
                DefaultCell = { Border = PdfPCell.NO_BORDER }
            };

            cellTable.AddCell(CreateCell("4Project: " + proyect_geolo, Element.ALIGN_LEFT));
            cellTable.AddCell(CreateCell(txt_Orden_Value, Element.ALIGN_RIGHT));

            cellTable.AddCell(CreateCell("Hole : " + blasHole, Element.ALIGN_LEFT));
            cellTable.AddCell(CreateCell("Date: " + currentDate, Element.ALIGN_RIGHT));

            PdfPCell barcodeCell = new PdfPCell(barcodeImage)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = PdfPCell.NO_BORDER,
                Colspan = 2
            };
            cellTable.AddCell(barcodeCell);

            PdfPCell numberCell = new PdfPCell(new Phrase("BH GEO", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = PdfPCell.NO_BORDER,
                Colspan = 2
            };
            cellTable.AddCell(numberCell);

            return cellTable;
        }

        private PdfPCell CreateCell(string text, int alignment)
        {
            return new PdfPCell(new Phrase(text))
            {
                HorizontalAlignment = alignment,
                Border = PdfPCell.NO_BORDER
            };
        }







        //LIMPIAR TODO EL DGV_ORDE
        private void btn_limpiar_Click(object sender, EventArgs e)
        {


            limpiarcheckssBox();
            

        }

        private void limpiarcheckssBox()
        {

            lblcount.Text = Dgv_Orden.Rows.Count.ToString();
            Dgv_Orden.Rows.Clear();
            Dgv_Orden.Refresh();
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
        // INSERTAR A DHL_SAMPLE_DISPATCH_HEADER

        public void InsertarDatosDispatch_Header()
        {
            try
            {
                // Abrir la conexión lo más pronto posible
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string valorOrden = txt_Orden.Text; // Obtener el valor del TextBox
                string sampleList = string.Join(" ", Dgv_Orden.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["CodAnalisis"].Value.ToString()));
                string companyName = "Glencore Antapaccay";
                string companyCountry = "Lima";
                string companyPostal = "051";
                string dispatchedBy = string.Empty;
                string projectNumber = "ANT-2020DH";
                string projectArea = "Drill Hole - Antapaccay";
                string dispatchDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string dateEntered = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string dispatchStatus = "NEW";
                string labPackage = cbo_Lab_package.SelectedValue.ToString();
                string laboratoryId = cbo_Laborat.SelectedValue.ToString();
                int id_labo = 0;  // Cambiado a int
                int samplePriority = 20;

                // Obtener el usuario conectado a la DB
                string lastModifiedByQuery = "SELECT SUSER_SNAME() AS CurrentUser";
                using (OdbcCommand cmd = new OdbcCommand(lastModifiedByQuery, con))
                {
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dispatchedBy = reader["CurrentUser"].ToString();
                        }
                    }
                }

                // Obtener el id del laboratorio 
                string id_laboratorio_query = $"SELECT LABORATORY_ID FROM LABORATORY WHERE laboratory_name = '{laboratoryId}'";
                using (OdbcCommand cmd = new OdbcCommand(id_laboratorio_query, con))
                {
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_labo = Convert.ToInt32(reader["LABORATORY_ID"]);  // Convertir a int
                        }
                    }
                }

                // Mostrar los datos capturados en un MessageBox (solo para propósitos de depuración)
                //MessageBox.Show($"Valor de Orden: {valorOrden}\nSample List: {sampleList}\nCompany Name: {companyName}\nCompany Country: {companyCountry}\nCompany Postal: {companyPostal}\nDispatched By: {dispatchedBy}\nProject Number: {projectNumber}\nProject Area: {projectArea}\nDispatch Date: {dispatchDate}\nDate Entered: {dateEntered}\nDispatch Status: {dispatchStatus}\nLab Package: {labPackage}\nLaboratory ID: {id_labo}\nSample Priority: {samplePriority}");

                // Query de inserción
                string query = "INSERT INTO DHL_SAMPLE_DISPATCH_HEADER(dispatch_number, sample_list, company_name, company_country, company_postal, dispatched_by, project_number, project_area, dispatch_date, date_entered, dispatch_status, lab_package, laboratory_id, carrier_company, sample_priority) " +
                               "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                // Crear y configurar el comando SQL
                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    // Parámetros de la consulta
                    command.Parameters.AddWithValue("@dispatch_number", valorOrden);
                    command.Parameters.AddWithValue("@sample_list", sampleList);
                    command.Parameters.AddWithValue("@company_name", companyName);
                    command.Parameters.AddWithValue("@company_country", companyCountry);
                    command.Parameters.AddWithValue("@company_postal", companyPostal);
                    command.Parameters.AddWithValue("@dispatched_by", dispatchedBy);
                    command.Parameters.AddWithValue("@project_number", projectNumber);
                    command.Parameters.AddWithValue("@project_area", projectArea);
                    command.Parameters.AddWithValue("@dispatch_date", dispatchDate);
                    command.Parameters.AddWithValue("@date_entered", dateEntered);
                    command.Parameters.AddWithValue("@dispatch_status", dispatchStatus);
                    command.Parameters.AddWithValue("@lab_package", labPackage);
                    command.Parameters.AddWithValue("@laboratory_id", id_labo);
                    command.Parameters.AddWithValue("@carrier_company", companyName); // Asumiendo que la compañía es la misma
                    command.Parameters.AddWithValue("@sample_priority", samplePriority);

                    // Ejecutar la consulta de inserción
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si se insertaron filas y mostrar un mensaje
                    if (rowsAffected > 0)
                    {
                        //MessageBox.Show("Datos insertados exitosamente.");
                    }
                    else
                    {
                        //MessageBox.Show("No se pudo insertar los datos.");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // INSERTAR A DHL_SAMPLE_DISPATCH_SAMPLE

        public void InsertarDatosDispatch_Samples()
        {
            try
            {
                // Abrir la conexión lo más pronto posible
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string dispatchNumber = txt_Orden.Text; // Obtener el valor del TextBox
                string labReferenceNumber = dispatchNumber + "OCT09";
                string analysisDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string numberOfBags = "1";
                string isSizeFraction = "N";
                string isDensityFraction = "N";
                string hasDensityFractions = "N";

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.IsNewRow) continue;

                    string sampleNumber = row.Cells["CodAnalisis"].Value?.ToString() ?? DBNull.Value.ToString();
                    string sampleType = row.Cells["Control"].Value?.ToString();
                    string holeNumber = row.Cells["Blasthole"].Value?.ToString() ?? DBNull.Value.ToString();
                    string holeTypeCode = string.IsNullOrEmpty(sampleType) || sampleType == "MDC" || sampleType == "MDG" || sampleType == "MDF" ? "DDH" : null;
                    string moduleName = string.IsNullOrEmpty(sampleType) || sampleType == "MDC" || sampleType == "MDG" || sampleType == "MDF" ? "BLASTHOLE" : "STD";
                    string sortOrder = row.Cells["Item"].Value?.ToString() ?? DBNull.Value.ToString();

                    if (string.IsNullOrEmpty(sampleType))
                    {
                        sampleType = "ASSAY";
                    }

                    // Mostrar los datos capturados en un MessageBox (solo para propósitos de depuración)
                    /*MessageBox.Show($"Valor de Orden: {dispatchNumber}\n" +
                                    $"Sample Number: {sampleNumber}\n" +
                                    $"Sample Type: {sampleType}\n" +
                                    $"Hole Number: {holeNumber}\n" +
                                    $"Dispatch Number: {dispatchNumber}\n" +
                                    $"Lab Reference Number: {labReferenceNumber}\n" +
                                    $"Analysis Date: {analysisDate}\n" +
                                    $"Number of Bags: {numberOfBags}\n" +
                                    $"Hole Type Code: {(holeTypeCode ?? "NULL")}\n" +
                                    $"Module Name: {moduleName}\n" +
                                    $"Is Size Fraction: {isSizeFraction}\n" +
                                    $"Is Density Fraction: {isDensityFraction}\n" +
                                    $"Has Density Fractions: {hasDensityFractions}\n" +
                                    $"Sort Order: {sortOrder}");*/

                    // Query de inserción
                    string query = "INSERT INTO DHL_SAMPLE_DISPATCH_SAMPLES (SAMPLE_NUMBER, sample_type, hole_number, dispatch_number, lab_reference_number, analysis_date, number_of_bags, hole_type_code, module_name, is_size_fraction, is_density_fraction, has_density_fractions, SORT_ORDER) " +
                                   "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    // Crear y configurar el comando SQL
                    using (OdbcCommand command = new OdbcCommand(query, con))
                    {
                        // Parámetros de la consulta
                        command.Parameters.AddWithValue("@SAMPLE_NUMBER", string.IsNullOrEmpty(sampleNumber) ? (object)DBNull.Value : sampleNumber);
                        command.Parameters.AddWithValue("@sample_type", string.IsNullOrEmpty(sampleType) ? (object)DBNull.Value : sampleType);
                        command.Parameters.AddWithValue("@hole_number", string.IsNullOrEmpty(holeNumber) ? (object)DBNull.Value : holeNumber);
                        command.Parameters.AddWithValue("@dispatch_number", string.IsNullOrEmpty(dispatchNumber) ? (object)DBNull.Value : dispatchNumber);
                        command.Parameters.AddWithValue("@lab_reference_number", string.IsNullOrEmpty(labReferenceNumber) ? (object)DBNull.Value : labReferenceNumber);
                        command.Parameters.AddWithValue("@analysis_date", string.IsNullOrEmpty(analysisDate) ? (object)DBNull.Value : analysisDate);
                        command.Parameters.AddWithValue("@number_of_bags", string.IsNullOrEmpty(numberOfBags) ? (object)DBNull.Value : numberOfBags);
                        command.Parameters.AddWithValue("@hole_type_code", string.IsNullOrEmpty(holeTypeCode) ? (object)DBNull.Value : holeTypeCode);
                        command.Parameters.AddWithValue("@module_name", string.IsNullOrEmpty(moduleName) ? (object)DBNull.Value : moduleName);
                        command.Parameters.AddWithValue("@is_size_fraction", string.IsNullOrEmpty(isSizeFraction) ? (object)DBNull.Value : isSizeFraction);
                        command.Parameters.AddWithValue("@is_density_fraction", string.IsNullOrEmpty(isDensityFraction) ? (object)DBNull.Value : isDensityFraction);
                        command.Parameters.AddWithValue("@has_density_fractions", string.IsNullOrEmpty(hasDensityFractions) ? (object)DBNull.Value : hasDensityFractions);
                        command.Parameters.AddWithValue("@SORT_ORDER", string.IsNullOrEmpty(sortOrder) ? (object)DBNull.Value : sortOrder);

                        // Ejecutar la consulta de inserción
                        int rowsAffected = command.ExecuteNonQuery();

                        // Verificar si se insertaron filas y mostrar un mensaje
                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show($"Datos insertados exitosamente para SAMPLE_NUMBER: {sampleNumber}");
                        }
                        else
                        {
                            //MessageBox.Show($"No se pudo insertar los datos para SAMPLE_NUMBER: {sampleNumber}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }








        // INSERTAR A MODULAR SAMPLE

        public void InsertIntoModularSamples()
        {
            // Obtener el valor del TextBox y del ComboBox
            string geolo = cbProyectoGeolo.SelectedValue.ToString();

            string codGui = string.Empty;

            try
            {
                // Abrir la conexión si está cerrada
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Ejecutar la consulta para obtener el valor de codGui
                string codGuiQuery = $"SELECT DISTINCT a1.BLASTHOLE_guid " +
                                     $"FROM UDEF_BLASTHOLE AS a1 " +
                                     $"INNER JOIN UDEF_LOG_BLASTHOLE AS a2 " +
                                     $"ON a1.BLASTHOLE_guid = a2.BLASTHOLE_GUID " +
                                     $"WHERE a2.PROYECTO_GEOLOGIA = ?";

                using (OdbcCommand cmd = new OdbcCommand(codGuiQuery, con))
                {
                    cmd.Parameters.AddWithValue("@PROYECTO_GEOLOGIA", geolo);
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            codGui = reader["BLASTHOLE_guid"].ToString();
                        }
                    }
                }

                // Ejecutar para obtener el usuario 

                string lastModifiedByQuery = "SELECT SUSER_SNAME() AS CurrentUser";
                string lastModifiedBy = string.Empty;

                using (OdbcCommand cmd = new OdbcCommand(lastModifiedByQuery, con))
                {
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastModifiedBy = reader["CurrentUser"].ToString();
                        }
                    }
                }


                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.IsNewRow) continue; // Saltar la fila de nueva inserción del DataGridView

                    // Obtener valores de cada fila del DataGridView
                    string sampleNumber = row.Cells["CodAnalisis"].Value?.ToString();
                    string moduleName = "BLASTHOLE"; // Valor fijo
                    string assaySampleTypeCode = row.Cells["Control"].Value?.ToString();
                    if (string.IsNullOrEmpty(assaySampleTypeCode) || new[] { "MB 105", "MC 401", "MC 402", "MC 403", "MC 976" }.Contains(assaySampleTypeCode))
                    {
                        assaySampleTypeCode = "ASSAY";
                    }
                    string parentSampleNumber = row.Cells["Parent"].Value?.ToString();
                    string dateShipped = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string statusCode = "Logged"; // Valor fijo
                    string sampleDispatched = "N"; // Valor fijo
                    int numberOfBags = 1; // Valor fijo
                    string lastModifiedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string isMaster = "Y"; // Valor fijo
                    string bhId = row.Cells["Blasthole"].Value?.ToString();
                    string sampleCode = row.Cells["Sample_Code"].Value?.ToString();

                    // Construir la consulta de inserción para modular_samples
                    string insertQuery = "INSERT INTO modular_samples(sample_number, standalone_guid, module_name, assay_sample_type_code, parent_sample_number, date_shipped, status_code, sample_dispatched, number_of_bags, last_modified_by, last_modified_date_time, is_master, PROYECTO_GEOLOGIA, BH_ID, SAMPLE_CODE) " +
                                         "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    try
                    {
                        // Configurar el comando de inserción
                        using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, con))
                        {
                            insertCmd.Parameters.AddWithValue("@sample_number", sampleNumber ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@standalone_guid", codGui ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@module_name", moduleName);
                            insertCmd.Parameters.AddWithValue("@assay_sample_type_code", assaySampleTypeCode ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@parent_sample_number", parentSampleNumber ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@date_shipped", dateShipped);
                            insertCmd.Parameters.AddWithValue("@status_code", statusCode);
                            insertCmd.Parameters.AddWithValue("@sample_dispatched", sampleDispatched);
                            insertCmd.Parameters.AddWithValue("@number_of_bags", numberOfBags);
                            insertCmd.Parameters.AddWithValue("@last_modified_by", lastModifiedBy ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@last_modified_date_time", lastModifiedDateTime);
                            insertCmd.Parameters.AddWithValue("@is_master", isMaster);
                            insertCmd.Parameters.AddWithValue("@PROYECTO_GEOLOGIA", geolo ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@BH_ID", bhId ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@SAMPLE_CODE", sampleCode ?? (object)DBNull.Value);

                            // Ejecutar el comando de inserción
                            insertCmd.ExecuteNonQuery();
                        }

                        // Insertar en HOLE_ASSAY_STANDARDS si el valor de 'Control' es uno de los especificados
                        if (new[] { "MB 105", "MC 401", "MC 402", "MC 403", "MC 976" }.Contains(row.Cells["Control"].Value?.ToString()))
                        {
                            string insertHoleAssayQuery = "INSERT INTO HOLE_ASSAY_STANDARDS(sample_number, assay_standard_code, hole_number, date_shipped, last_modified_by, last_modified_date_time, is_master, sample_dispatched, standalone_guid, module_name) " +
                                                          "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                            using (OdbcCommand insertHoleAssayCmd = new OdbcCommand(insertHoleAssayQuery, con))
                            {
                                insertHoleAssayCmd.Parameters.AddWithValue("@sample_number", sampleNumber ?? (object)DBNull.Value);
                                insertHoleAssayCmd.Parameters.AddWithValue("@assay_standard_code", row.Cells["Control"].Value?.ToString() ?? (object)DBNull.Value);
                                insertHoleAssayCmd.Parameters.AddWithValue("@hole_number", bhId ?? (object)DBNull.Value);
                                insertHoleAssayCmd.Parameters.AddWithValue("@date_shipped", dateShipped);
                                insertHoleAssayCmd.Parameters.AddWithValue("@last_modified_by", lastModifiedBy ?? (object)DBNull.Value);
                                insertHoleAssayCmd.Parameters.AddWithValue("@last_modified_date_time", lastModifiedDateTime);
                                insertHoleAssayCmd.Parameters.AddWithValue("@is_master", isMaster);
                                insertHoleAssayCmd.Parameters.AddWithValue("@sample_dispatched", sampleDispatched);
                                insertHoleAssayCmd.Parameters.AddWithValue("@standalone_guid", codGui ?? (object)DBNull.Value);
                                insertHoleAssayCmd.Parameters.AddWithValue("@module_name", moduleName);

                                // Ejecutar el comando de inserción
                                insertHoleAssayCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (OdbcException ex)
                    {
                        //MessageBox.Show("Ocurrió un error al insertar: " + ex.Message);
                    }
                }

                //MessageBox.Show("Datos Insertados");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        // INSERTAR A STANDAR RESULT 

        public void InsertIntoStandarResult()
        {
            try
            {
                // Crear una tabla temporal para almacenar los resultados de la consulta
                DataTable tempTable = new DataTable();

                // Definir las columnas de la tabla temporal
                tempTable.Columns.Add("CodAnalisis", typeof(string));
                tempTable.Columns.Add("ASSAY_UOFM_CODE", typeof(string));
                tempTable.Columns.Add("ELEMENT_TYPE_CODE", typeof(string));
                tempTable.Columns.Add("method_code", typeof(string));

                // Abrir la conexión si está cerrada
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.IsNewRow) continue; // Saltar la fila de nueva inserción del DataGridView

                    // Obtener valores de cada fila del DataGridView
                    string SampleNumber = row.Cells["CodAnalisis"].Value?.ToString();
                    string controlValue = row.Cells["Control"].Value?.ToString();

                    try
                    {
                        if (new[] { "MB 105", "MC 401", "MC 402", "MC 403", "MC 976" }.Contains(controlValue))
                        {
                            string assayStandardDefaultsQuery = "SELECT ASSAY_UOFM_CODE, ELEMENT_TYPE_CODE, method_code FROM ASSAY_STANDARD_DEFAULTS WHERE ASSAY_STANDARD_CODE = ?";

                            using (OdbcCommand cmd = new OdbcCommand(assayStandardDefaultsQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@ControlValue", controlValue);

                                using (OdbcDataReader reader = cmd.ExecuteReader())
                                {
                                    // Agregar los resultados de la consulta a la tabla temporal
                                    while (reader.Read())
                                    {
                                        DataRow newRow = tempTable.NewRow();
                                        newRow["CodAnalisis"] = SampleNumber;
                                        newRow["ASSAY_UOFM_CODE"] = reader["ASSAY_UOFM_CODE"].ToString();
                                        newRow["ELEMENT_TYPE_CODE"] = reader["ELEMENT_TYPE_CODE"].ToString();
                                        newRow["method_code"] = reader["method_code"].ToString();
                                        tempTable.Rows.Add(newRow);
                                    }
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurrió un error al insertar: " + ex.Message);
                    }
                }

                // Procesar los datos de la tabla temporal para realizar los inserts
                foreach (DataRow tempRow in tempTable.Rows)
                {
                    string assayUofmCode = tempRow["ASSAY_UOFM_CODE"].ToString();
                    string elementTypeCode = tempRow["ELEMENT_TYPE_CODE"].ToString();
                    string methodCode = tempRow["method_code"].ToString();

                    // Realizar el insert en ASSAY_STANDARD_RESULTS
                    string insertQuery = "INSERT INTO ASSAY_STANDARD_RESULTS(SAMPLE_NUMBER, ASSAY_UOFM_CODE, ELEMENT_TYPE_CODE, method_code) " +
                                         "VALUES (?, ?, ?, ?)";

                    using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@SampleNumber", tempRow["CodAnalisis"]);
                        insertCmd.Parameters.AddWithValue("@AssayUofmCode", assayUofmCode);
                        insertCmd.Parameters.AddWithValue("@ElementTypeCode", elementTypeCode);
                        insertCmd.Parameters.AddWithValue("@MethodCode", methodCode);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                //MessageBox.Show("Datos Insertados");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }






        //INSERTAR A UDEF_ORDER_ANALYSIS

        public void InsertarDatosOrder_Analysis()
        {
            try
            {
                // Abrir la conexión lo más pronto posible
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Validar que los controles no sean nulos
                if (cbProyectoGeolo.SelectedValue == null || cbo_Laborat.SelectedValue == null ||
                    codPreparacion.SelectedValue == null || cbo_proyecto.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos necesarios.");
                    return;
                }

                // Obtener el valor del TextBox y del ComboBox
                string geolo = cbProyectoGeolo.SelectedValue.ToString();
                string laboratorio = cbo_Laborat.SelectedValue.ToString();
                string cod_prep = codPreparacion.SelectedValue.ToString();
                string valorOrden = txt_Orden.Text; // Obtener el valor del TextBox

                // Obtener el valor del usuario actual conectado
                string lastModifiedByQuery = "SELECT SUSER_SNAME() AS CurrentUser";
                string lastModifiedBy = string.Empty;
                using (OdbcCommand cmd = new OdbcCommand(lastModifiedByQuery, con))
                {
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastModifiedBy = reader["CurrentUser"].ToString();
                        }
                    }
                }

                // Obtener el valor de codGui
                string codGui = string.Empty;
                string codGuiQuery = $"SELECT DISTINCT a1.BLASTHOLE_guid " +
                                     $"FROM UDEF_BLASTHOLE AS a1 " +
                                     $"INNER JOIN UDEF_LOG_BLASTHOLE AS a2 " +
                                     $"ON a1.BLASTHOLE_guid = a2.BLASTHOLE_GUID " +
                                     $"WHERE a2.PROYECTO_GEOLOGIA = ?";
                using (OdbcCommand cmd = new OdbcCommand(codGuiQuery, con))
                {
                    cmd.Parameters.Add("@PROYECTO_GEOLOGIA", OdbcType.VarChar).Value = geolo;
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            codGui = reader["BLASTHOLE_guid"].ToString();
                        }
                    }
                }

                // Obtener el valor de cod_planamiento
                string proy_planeamiento = string.Empty;
                string proy_planeamientoByQuery = $"SELECT DISTINCT a1.PROYECTO " +
                                                  $"FROM UDEF_BLASTHOLE AS a1 " +
                                                  $"INNER JOIN UDEF_LOG_BLASTHOLE AS a2 " +
                                                  $"ON a1.BLASTHOLE_guid = a2.BLASTHOLE_GUID  " +
                                                  $"WHERE a2.PROYECTO_GEOLOGIA = ?";
                using (OdbcCommand cmd = new OdbcCommand(proy_planeamientoByQuery, con))
                {
                    cmd.Parameters.Add("@PROYECTO_GEOLOGIA", OdbcType.VarChar).Value = geolo;
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proy_planeamiento = reader["PROYECTO"].ToString();
                        }
                    }
                }

                //-------------------

                string lastModifiedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.IsNewRow) continue;

                    string codeAnalysis = row.Cells["CodAnalisis"].Value?.ToString() ?? DBNull.Value.ToString();
                    string sampleNumber = row.Cells["sample_code"].Value?.ToString() ?? DBNull.Value.ToString();
                    string fechaPreparacion = row.Cells["Fecha_Entrega"].Value?.ToString() ?? DBNull.Value.ToString();

                    bool CuTotVal = row.Cells["CuTot"].Value != null && bool.TryParse(row.Cells["CuTot"].Value.ToString(), out bool CuTot_p) ? CuTot_p : false;
                    bool CuOxVal = row.Cells["CuOx"].Value != null && bool.TryParse(row.Cells["CuOx"].Value.ToString(), out bool CuOx_p) ? CuOx_p : false;
                    bool CuSolVal = row.Cells["CuSol"].Value != null && bool.TryParse(row.Cells["CuSol"].Value.ToString(), out bool CuSol_p) ? CuSol_p : false;
                    bool AuVal = row.Cells["Au"].Value != null && bool.TryParse(row.Cells["Au"].Value.ToString(), out bool Au_p) ? Au_p : false;
                    bool AgVal = row.Cells["Ag"].Value != null && bool.TryParse(row.Cells["Ag"].Value.ToString(), out bool Ag_p) ? Ag_p : false;
                    bool MoVal = row.Cells["Mo"].Value != null && bool.TryParse(row.Cells["Mo"].Value.ToString(), out bool Mo_p) ? Mo_p : false;
                    bool CO3Val = row.Cells["CO3"].Value != null && bool.TryParse(row.Cells["CO3"].Value.ToString(), out bool CO3_p) ? CO3_p : false;
                    bool CSAcVal = row.Cells["CSAc"].Value != null && bool.TryParse(row.Cells["CSAc"].Value.ToString(), out bool CSAc) ? CSAc : false;
                    bool CsCnVal = row.Cells["CsCn"].Value != null && bool.TryParse(row.Cells["CsCn"].Value.ToString(), out bool CsCn_p) ? CsCn_p : false;
                    bool CuResVal = row.Cells["CuRes"].Value != null && bool.TryParse(row.Cells["CuRes"].Value.ToString(), out bool CuRes_p) ? CuRes_p : false;
                    bool FeTotVal = row.Cells["FeTot"].Value != null && bool.TryParse(row.Cells["FeTot"].Value.ToString(), out bool FeTot_p) ? FeTot_p : false;

                    int CuTot_pInt = CuTotVal ? 1 : 0;
                    int CuOx_pInt = CuOxVal ? 1 : 0;
                    int CuSol_pInt = CuSolVal ? 1 : 0;
                    int Au_pInt = AuVal ? 1 : 0;
                    int Ag_pInt = AgVal ? 1 : 0;
                    int Mo_pInt = MoVal ? 1 : 0;
                    int CO3_pInt = CO3Val ? 1 : 0;
                    int CSAcInt = CSAcVal ? 1 : 0;
                    int CsCn_pInt = CsCnVal ? 1 : 0;
                    int CuRes_pInt = CuResVal ? 1 : 0;
                    int FeTot_pInt = FeTotVal ? 1 : 0;

                    string query = "INSERT INTO UDEF_ORDER_ANALYSIS (STATUS, IS_MASTER, CHECKEDOUT_COMPUTER, CODE_ANALYSIS, CuTot_p, CuOx_p, CuSol_p, Au_p, Ag_p, Mo_p, CO3_p, CSAc, CsCn_p, CuRes_p, FeTot_p, LAST_MODIFIED_BY, LAST_MODIFIED_DATE_TIME, INCLUDE_IN_TRANSFER, SELECTED_BY, BLASTHOLE_GUID, current_owner, last_successful_transfer, ready_for_auto_checkin, samplenumber, PROYECTO_GEOLOGIA, ORIGINADO_POR, REPORTAR_A, FECHA_PREPARACION, FECHA_MUESTREO, LABORATORIO, CODE_PREP, PROYECTO_PLANEAMIENTO, DISPATCH_CODE ) " +
                                      "VALUES ('NEW', 'Y', 'Y', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 'Y', ?, ?, ?, ?, 'Y', ?, ?, 'MUESTRERIA', 'G.MINA/BASE DE DATOS', ?, ?, ?, ?, ?, ? )";

                    using (OdbcCommand command = new OdbcCommand(query, con))
                    {
                        command.Parameters.Add("@CODE_ANALYSIS", OdbcType.VarChar).Value = codeAnalysis;
                        command.Parameters.Add("@CuTot_p", OdbcType.Int).Value = CuTot_pInt;
                        command.Parameters.Add("@CuOx_p", OdbcType.Int).Value = CuOx_pInt;
                        command.Parameters.Add("@CuSol_p", OdbcType.Int).Value = CuSol_pInt;
                        command.Parameters.Add("@Au_p", OdbcType.Int).Value = Au_pInt;
                        command.Parameters.Add("@Ag_p", OdbcType.Int).Value = Ag_pInt;
                        command.Parameters.Add("@Mo_p", OdbcType.Int).Value = Mo_pInt;
                        command.Parameters.Add("@CO3_p", OdbcType.Int).Value = CO3_pInt;
                        command.Parameters.Add("@CSAc", OdbcType.Int).Value = CSAcInt;
                        command.Parameters.Add("@CsCn_p", OdbcType.Int).Value = CsCn_pInt;
                        command.Parameters.Add("@CuRes_p", OdbcType.Int).Value = CuRes_pInt;
                        command.Parameters.Add("@FeTot_p", OdbcType.Int).Value = FeTot_pInt;
                        command.Parameters.Add("@LAST_MODIFIED_BY", OdbcType.VarChar).Value = lastModifiedBy;
                        command.Parameters.Add("@LAST_MODIFIED_DATE_TIME", OdbcType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@SELECTED_BY", OdbcType.VarChar).Value = lastModifiedBy;
                        command.Parameters.Add("@BLASTHOLE_GUID", OdbcType.VarChar).Value = codGui;
                        command.Parameters.Add("@current_owner", OdbcType.VarChar).Value = lastModifiedBy;
                        command.Parameters.Add("@last_successful_transfer", OdbcType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@samplenumber", OdbcType.VarChar).Value = sampleNumber;
                        command.Parameters.Add("@PROYECTO_GEOLOGIA", OdbcType.VarChar).Value = geolo;
                        command.Parameters.Add("@FECHA_PREPARACION", OdbcType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@FECHA_MUESTREO", OdbcType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@LABORATORIO", OdbcType.VarChar).Value = laboratorio;
                        command.Parameters.Add("@CODE_PREP", OdbcType.VarChar).Value = cod_prep;
                        command.Parameters.Add("@PROYECTO_PLANEAMIENTO", OdbcType.VarChar).Value = proy_planeamiento;
                        command.Parameters.Add("@DISPATCH_CODE", OdbcType.VarChar).Value = valorOrden;

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // MessageBox.Show($"Datos insertados exitosamente para CODE_ANALYSIS: {codeAnalysis}");
                        }
                        else
                        {
                            //MessageBox.Show($"No se pudo insertar los datos para CODE_ANALYSIS: {codeAnalysis}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
















        //-----------------------------------------------------
        private void label11_Click(object sender, EventArgs e)
        {

        }

        // DAR EL VALOR A GEOLOGIA Y PREPARACIÓN
        private void UpdateLabels()
        {
            // Capturar el valor seleccionado en el ComboBox cbProyectoGeolo
            string selectedProyectoGeolo = cbProyectoGeolo.SelectedValue?.ToString();

            // Asignar el valor al Label lbProyGeolo
            lbProyGeolo.Text = selectedProyectoGeolo;

            // Capturar el valor seleccionado en el ComboBox cbCodPreparacion
            string selectedCodPreparacion = codPreparacion.SelectedValue?.ToString();

            // Asignar el valor al Label lbCodPrep
            lbCodPrep.Text = selectedCodPreparacion;
        }

        private void guardar_bd_Click(object sender, EventArgs e)
        {
            try
            {
                InsertIntoModularSamples();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar en ModularSamples: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si ocurre un error
            }

            try
            {
                InsertIntoStandarResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar en StandarResult: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si ocurre un error
            }

            try
            {
                InsertarDatosDispatch_Header();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar en Dispatch_Header: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si ocurre un error
            }

            try
            {
                InsertarDatosDispatch_Samples();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar en Dispatch_Samples: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si ocurre un error
            }

            try
            {
                InsertarDatosOrder_Analysis();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar en Order_Analysis: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si ocurre un error
            }

            MessageBox.Show("Todos los datos se guardaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llenarComboBoxOrdenes();
        }

        // CONSULTA DE DESPACHO 

        private void LlenarDataGridView()
        {
            // Verificar si la conexión está cerrada y abrirla
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            try
            {
                // Obtener el valor seleccionado del ComboBox
                string dispatchCode = Cb_ordenes.SelectedValue.ToString();

                // Definir la consulta SQL
                string query = "select a1.sample_number, a1.SAMPLE_CODE, a1.BH_ID, a2.code_prep, a1.PROYECTO_GEOLOGIA, case when a3.sample_type = 'ASSAY' THEN NULL ELSE a3.sample_type END AS assay_sample_type_code  , a1.parent_sample_number, a2.CuTot_p, a2.CuOx_p , a2.CuSol_p, a2.Au_p, a2.Ag_p, a2.Mo_p, a2.CO3_p, a2.CSAc, a2.CsCn_p, a2.CuRes_p, a2.FeTot_p  from modular_samples as a1 inner join UDEF_ORDER_ANALYSIS as a2 ON  a1.sample_number = a2.CODE_ANALYSIS inner join DHL_SAMPLE_DISPATCH_SAMPLES as a3 ON a3.SAMPLE_NUMBER = a2.CODE_ANALYSIS where DISPATCH_CODE = ? order by sample_number";


                // Crear y configurar el comando
                using (OdbcCommand cmd = new OdbcCommand(query, con))
                {
                    cmd.Parameters.Add("@DISPATCH_CODE", OdbcType.VarChar).Value = dispatchCode;

                    // Ejecutar la consulta y obtener los resultados
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        // Limpiar el DataGridView antes de llenarlo
                        Dgv_Orden.Rows.Clear();

                        // Leer los datos de la consulta
                        while (reader.Read())
                        {
                            // Crear una nueva fila en el DataGridView
                            int rowIndex = Dgv_Orden.Rows.Add();

                            // Asignar valores a las celdas correspondientes
                            Dgv_Orden.Rows[rowIndex].Cells["CodAnalisis"].Value = reader["sample_number"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["sample_code"].Value = reader["SAMPLE_CODE"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["blasthole"].Value = reader["BH_ID"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["Cod_Prep"].Value = reader["code_prep"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["Proyecto_geologia"].Value = reader["PROYECTO_GEOLOGIA"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["Control"].Value = reader["assay_sample_type_code"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["parent"].Value = reader["parent_sample_number"].ToString();
                            Dgv_Orden.Rows[rowIndex].Cells["CuTot"].Value = Convert.ToBoolean(reader["CuTot_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CuOx"].Value = Convert.ToBoolean(reader["CuOx_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CuSol"].Value = Convert.ToBoolean(reader["CuSol_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["Au"].Value = Convert.ToBoolean(reader["Au_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["Ag"].Value = Convert.ToBoolean(reader["Ag_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["Mo"].Value = Convert.ToBoolean(reader["Mo_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CO3"].Value = Convert.ToBoolean(reader["CO3_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CSAc"].Value = Convert.ToBoolean(reader["CSAc"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CsCn"].Value = Convert.ToBoolean(reader["CsCn_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["CuRes"].Value = Convert.ToBoolean(reader["CuRes_p"]);
                            Dgv_Orden.Rows[rowIndex].Cells["FeTot"].Value = Convert.ToBoolean(reader["FeTot_p"]);
                        }
                    }
                }

                // Definir la segunda consulta SQL
                string query2 = "select distinct a2.code_prep, a1.PROYECTO_GEOLOGIA " +
                                "from modular_samples as a1 " +
                                "inner join UDEF_ORDER_ANALYSIS as a2 ON a1.sample_number = a2.CODE_ANALYSIS " +
                                "where A2.DISPATCH_CODE = ?";

                // Crear y configurar el comando para la segunda consulta
                using (OdbcCommand cmd2 = new OdbcCommand(query2, con))
                {
                    cmd2.Parameters.Add("@DISPATCH_CODE", OdbcType.VarChar).Value = dispatchCode;

                    // Ejecutar la consulta y obtener los resultados
                    using (OdbcDataReader reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            // Asignar los valores a los labels
                            lbCodPrep.Text = reader2["code_prep"].ToString();
                            lbProyGeolo.Text = reader2["PROYECTO_GEOLOGIA"].ToString();
                        }
                    }
                }


                string query3 = "select  distinct DISPATCH_CODE from UDEF_ORDER_ANALYSIS WHERE DISPATCH_CODE = ? ";

                using (OdbcCommand cmd3 = new OdbcCommand(query3, con))
                {
                    cmd3.Parameters.Add("@DISPATCH_CODE", OdbcType.VarChar).Value = dispatchCode;

                    // Ejecutar la consulta y obtener los resultados
                    using (OdbcDataReader reader3 = cmd3.ExecuteReader())
                    {
                        if (reader3.Read())
                        {
                            // Asignar los valores a los labels
                            lbDispatch.Text = reader3["DISPATCH_CODE"].ToString();
                        }
                    }
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la consulta: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        // DELETE DE LAS TABLAS ---------------------

        //HOLE_ASSAY_STANDARDS 
        private void EliminarRegistrosASSAY_STANDARDS()
        {
            try
            {
                // Lista de valores que buscamos en la columna "CONTROL"
                var valoresControl = new[] { "MB 105", "MC 401", "MC 402", "MC 403", "MC 976" };

                // Construir una lista de valores para el DELETE
                var codigosAnalisis = new List<string>();

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.Cells["CONTROL"] != null && valoresControl.Contains(row.Cells["CONTROL"].Value.ToString()))
                    {
                        codigosAnalisis.Add(row.Cells["CodAnalisis"].Value.ToString());
                    }
                }

                if (codigosAnalisis.Count > 0)
                {
                    string codigos = string.Join("','", codigosAnalisis);
                    string query = $"DELETE FROM HOLE_ASSAY_STANDARDS WHERE SAMPLE_NUMBER IN ('{codigos}')";

                    using (OdbcCommand command = new OdbcCommand(query, con))
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        //MessageBox.Show($"{rowsAffected} registros eliminados.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros: " + ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //------------------modular_samples 
        private void EliminarRegistrosModularSamples()
        {
            try
            {
                // Construir una lista de valores para el DELETE
                var codigosAnalisis = new List<string>();

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.Cells["CodAnalisis"] != null && row.Cells["CodAnalisis"].Value != null)
                    {
                        codigosAnalisis.Add(row.Cells["CodAnalisis"].Value.ToString());
                    }
                }

                if (codigosAnalisis.Count > 0)
                {
                    string codigos = string.Join("','", codigosAnalisis);
                    string query = $"DELETE FROM modular_samples WHERE sample_number IN ('{codigos}')";

                    using (OdbcCommand command = new OdbcCommand(query, con))
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        //MessageBox.Show($"{rowsAffected} registros eliminados.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros: " + ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //----------------- DHL_SAMPLE_DISPATCH_SAMPLES

        private void EliminarRegistrosDHL()
        {
            try
            {
                // Construir una lista de valores para el DELETE
                var codigosAnalisis = new List<string>();

                foreach (DataGridViewRow row in Dgv_Orden.Rows)
                {
                    if (row.Cells["CodAnalisis"] != null && row.Cells["CodAnalisis"].Value != null)
                    {
                        codigosAnalisis.Add(row.Cells["CodAnalisis"].Value.ToString());
                    }
                }

                if (codigosAnalisis.Count > 0)
                {
                    string codigos = string.Join("','", codigosAnalisis);
                    string query = $"DELETE FROM DHL_SAMPLE_DISPATCH_SAMPLES WHERE SAMPLE_NUMBER IN ('{codigos}')";

                    using (OdbcCommand command = new OdbcCommand(query, con))
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        //MessageBox.Show($"{rowsAffected} registros eliminados.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros: " + ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // ELIMINAR DHL_SAMPLE_DISPATCH_HEADER 

        private void EliminarRegistrosDHLHeaderFromLabel()
        {
            try
            {
                string dispatchNumber = lbDispatch.Text;// Obtener el valor del Label

                string query = "DELETE FROM DHL_SAMPLE_DISPATCH_HEADER WHERE dispatch_number = ?";

                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    command.Parameters.Add("@dispatchNumber", OdbcType.VarChar).Value = dispatchNumber;

                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    //MessageBox.Show($"{rowsAffected} registros eliminados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros: " + ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //--------------ELIMINAR UDEF_ORDER_ANALYSIS  

        private void EliminarRegistrosUDEFOrderAnalysis()
        {
            try
            {

                string dispatchCode = lbDispatch.Text;

                string query = "DELETE FROM UDEF_ORDER_ANALYSIS WHERE DISPATCH_CODE = ?";

                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    command.Parameters.Add("@dispatchCode", OdbcType.VarChar).Value = dispatchCode;

                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    //MessageBox.Show($"{rowsAffected} registros eliminados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros: " + ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //----------------------------------------------

        private void cbProyectoGeolo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //------------------------------------------reporte
        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            LlenarDataGridView();
            FillHoleColumn();

            for (int i = 0; i < Dgv_Orden.Rows.Count; i++)
            {
                Dgv_Orden.Rows[i].Cells["item"].Value = (i + 1).ToString();
            }

            

            // Hacer visible el botón Actualizar

            CopiarValorTxtOrdenATxtDespacho2();

            DatosOrden.ValorOrden = txtdespacho.Text;

            guardar_bd.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                EliminarRegistrosASSAY_STANDARDS();
                EliminarRegistrosModularSamples();
                //EliminarRegistrosDHL();
                EliminarRegistrosDHLHeaderFromLabel();
                EliminarRegistrosUDEFOrderAnalysis();
                MessageBox.Show("Se eliminó el despacho.");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al eliminar el despacho: " + ex.Message);
            }

            llenarComboBoxOrdenes();

            

        }

    }
}
