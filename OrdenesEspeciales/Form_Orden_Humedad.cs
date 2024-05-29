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
    public partial class Form_Orden_Humedad : Form
    {
        OdbcConnection con = ConexionODBC.connection;
        public Form_Orden_Humedad()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            //INICIALIZAR 
            //Dgv_Orden.CellPainting += Dgv_Orden_CellPainting;
            //Dgv_Orden.MouseClick += Dgv_Orden_MouseClick;

            // Configurar el evento CheckedChanged del CheckBox
            //cbo_CuTot.CheckedChanged += HeaderCheckBox_CheckedChanged;
            // Inicialmente ocultamos el CheckBox para evitar que aparezca como control aparte



        }
        //


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

      

        

        // AGREGAR AUTOMATICAMENTE EL CODIGO DE MUESTRA 

        

        //CARGAR DATOS  AL COMBOBOX DE DUPLICADO 


        //CARGAR DATOS  COMBOBOX DE CONTROLES BLANCOS


       

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
                string query = "SELECT max(order_prep_guid) FROM [dbo].[UDEF_ORDER_PREP]";
                using (OdbcCommand command = new OdbcCommand(query, con))
                {
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        var source = new AutoCompleteStringCollection();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // Verificar que el valor no sea nulo
                            {
                                source.Add(reader[0].ToString());
                            }
                        }
                        reader.Close();
                        cajaTexto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cajaTexto.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        cajaTexto.AutoCompleteCustomSource = source;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo autocompletar el Textbox: " + ex.ToString());
            }
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
            Dgv_Consulta.ReadOnly = true;
            label7.Text = Dgv_Consulta.Rows.Count.ToString();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Dgv_Orden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_OrdenAnalisis_TextChanged(object sender, EventArgs e)
        {

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

           
            //paginahtml_texto = paginahtml_texto.Replace("@MUESTRERIA", txt_OrdenAnalisis.Text);
            //paginahtml_texto = paginahtml_texto.Replace("@FECHA", txt_OrdenAnalisis.Text);
            paginahtml_texto = paginahtml_texto.Replace("@LAB", cbo_Laborat.Text);
            paginahtml_texto = paginahtml_texto.Replace("@CODPROJECT", cbo_proyecto.Text);
            //paginahtml_texto = paginahtml_texto.Replace("@G.MINA", txt_OrdenAnalisis.Text);
            paginahtml_texto = paginahtml_texto.Replace("@FECHAPREP", DateTime.Now.ToString("dd/MM/yyyy"));
            

            string filas = string.Empty;

            foreach (DataGridViewRow row in Dgv_Consulta.Rows)
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
                    foreach (DataGridViewRow row in Dgv_Consulta.Rows)
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
        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
