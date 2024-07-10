namespace OrdenesEspeciales
{
    partial class Form_Orden_Humedad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dgv_Consulta = new System.Windows.Forms.DataGridView();
            this.Consulta = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbo_laboratory = new System.Windows.Forms.Label();
            this.cbo_Laborat = new System.Windows.Forms.ComboBox();
            this.btn_Buscar = new System.Windows.Forms.Button();
            this.dtp_fin = new System.Windows.Forms.DateTimePicker();
            this.dtp_inicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_desde = new System.Windows.Forms.Label();
            this.cbo_proyecto = new System.Windows.Forms.ComboBox();
            this.btn_consultar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.guardar_bd = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblcant = new System.Windows.Forms.Label();
            this.gDMS_ANTAPACCAYDataSet = new OrdenesEspeciales.GDMS_ANTAPACCAYDataSet();
            this.gDMSANTAPACCAYDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Consulta)).BeginInit();
            this.Consulta.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gDMS_ANTAPACCAYDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gDMSANTAPACCAYDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_Consulta
            // 
            this.Dgv_Consulta.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Dgv_Consulta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Consulta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_Consulta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_Consulta.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.Dgv_Consulta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgv_Consulta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Consulta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Consulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Consulta.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Consulta.EnableHeadersVisualStyles = false;
            this.Dgv_Consulta.GridColor = System.Drawing.Color.SteelBlue;
            this.Dgv_Consulta.Location = new System.Drawing.Point(12, 25);
            this.Dgv_Consulta.Name = "Dgv_Consulta";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Consulta.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_Consulta.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.Dgv_Consulta.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Dgv_Consulta.RowTemplate.Height = 24;
            this.Dgv_Consulta.Size = new System.Drawing.Size(1264, 537);
            this.Dgv_Consulta.TabIndex = 1;
            this.Dgv_Consulta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Consulta_CellContentClick);
            // 
            // Consulta
            // 
            this.Consulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.Consulta.Controls.Add(this.label5);
            this.Consulta.Controls.Add(this.comboBox1);
            this.Consulta.Controls.Add(this.cbo_laboratory);
            this.Consulta.Controls.Add(this.cbo_Laborat);
            this.Consulta.Controls.Add(this.btn_Buscar);
            this.Consulta.Controls.Add(this.dtp_fin);
            this.Consulta.Controls.Add(this.dtp_inicio);
            this.Consulta.Controls.Add(this.label2);
            this.Consulta.Controls.Add(this.lb_desde);
            this.Consulta.Controls.Add(this.cbo_proyecto);
            this.Consulta.Controls.Add(this.btn_consultar);
            this.Consulta.Controls.Add(this.label6);
            this.Consulta.Cursor = System.Windows.Forms.Cursors.Default;
            this.Consulta.ForeColor = System.Drawing.Color.White;
            this.Consulta.Location = new System.Drawing.Point(12, 12);
            this.Consulta.Name = "Consulta";
            this.Consulta.Size = new System.Drawing.Size(1286, 134);
            this.Consulta.TabIndex = 5;
            this.Consulta.TabStop = false;
            this.Consulta.Text = "Header";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(104, 591);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 23);
            this.label7.TabIndex = 102;
            this.label7.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 593);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 101;
            this.label9.Text = "Recuento : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(782, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 17);
            this.label5.TabIndex = 103;
            this.label5.Text = "Tipo de Muestra";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(785, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 24);
            this.comboBox1.TabIndex = 102;
            this.comboBox1.Text = "36334";
            // 
            // cbo_laboratory
            // 
            this.cbo_laboratory.AutoSize = true;
            this.cbo_laboratory.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_laboratory.ForeColor = System.Drawing.Color.White;
            this.cbo_laboratory.Location = new System.Drawing.Point(601, 43);
            this.cbo_laboratory.Name = "cbo_laboratory";
            this.cbo_laboratory.Size = new System.Drawing.Size(99, 20);
            this.cbo_laboratory.TabIndex = 97;
            this.cbo_laboratory.Text = "Laboratory *";
            this.cbo_laboratory.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbo_Laborat
            // 
            this.cbo_Laborat.BackColor = System.Drawing.Color.White;
            this.cbo_Laborat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Laborat.FormattingEnabled = true;
            this.cbo_Laborat.Location = new System.Drawing.Point(604, 65);
            this.cbo_Laborat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_Laborat.Name = "cbo_Laborat";
            this.cbo_Laborat.Size = new System.Drawing.Size(164, 24);
            this.cbo_Laborat.TabIndex = 96;
            // 
            // btn_Buscar
            // 
            this.btn_Buscar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_Buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Buscar.FlatAppearance.BorderSize = 0;
            this.btn_Buscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Buscar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Buscar.ForeColor = System.Drawing.Color.White;
            this.btn_Buscar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Buscar.Location = new System.Drawing.Point(325, 52);
            this.btn_Buscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Buscar.Name = "btn_Buscar";
            this.btn_Buscar.Size = new System.Drawing.Size(93, 37);
            this.btn_Buscar.TabIndex = 81;
            this.btn_Buscar.Text = "Buscar";
            this.btn_Buscar.UseVisualStyleBackColor = false;
            this.btn_Buscar.Click += new System.EventHandler(this.btn_Buscar_Click);
            // 
            // dtp_fin
            // 
            this.dtp_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fin.Location = new System.Drawing.Point(162, 66);
            this.dtp_fin.Name = "dtp_fin";
            this.dtp_fin.Size = new System.Drawing.Size(139, 22);
            this.dtp_fin.TabIndex = 92;
            // 
            // dtp_inicio
            // 
            this.dtp_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_inicio.Location = new System.Drawing.Point(9, 66);
            this.dtp_inicio.Name = "dtp_inicio";
            this.dtp_inicio.Size = new System.Drawing.Size(142, 22);
            this.dtp_inicio.TabIndex = 91;
            this.dtp_inicio.Value = new System.DateTime(2024, 5, 7, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(168, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 90;
            this.label2.Text = "Fecha Final ";
            // 
            // lb_desde
            // 
            this.lb_desde.AutoSize = true;
            this.lb_desde.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_desde.ForeColor = System.Drawing.Color.White;
            this.lb_desde.Location = new System.Drawing.Point(5, 47);
            this.lb_desde.Name = "lb_desde";
            this.lb_desde.Size = new System.Drawing.Size(92, 17);
            this.lb_desde.TabIndex = 81;
            this.lb_desde.Text = "Fecha Inicial ";
            // 
            // cbo_proyecto
            // 
            this.cbo_proyecto.FormattingEnabled = true;
            this.cbo_proyecto.Location = new System.Drawing.Point(445, 64);
            this.cbo_proyecto.Name = "cbo_proyecto";
            this.cbo_proyecto.Size = new System.Drawing.Size(139, 24);
            this.cbo_proyecto.TabIndex = 78;
            this.cbo_proyecto.Text = "36334";
            // 
            // btn_consultar
            // 
            this.btn_consultar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_consultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_consultar.FlatAppearance.BorderSize = 0;
            this.btn_consultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_consultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_consultar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_consultar.ForeColor = System.Drawing.Color.White;
            this.btn_consultar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_consultar.Location = new System.Drawing.Point(961, 53);
            this.btn_consultar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_consultar.Name = "btn_consultar";
            this.btn_consultar.Size = new System.Drawing.Size(93, 34);
            this.btn_consultar.TabIndex = 76;
            this.btn_consultar.Text = "Consultar";
            this.btn_consultar.UseVisualStyleBackColor = false;
            this.btn_consultar.Click += new System.EventHandler(this.btn_consultar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(445, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 17);
            this.label6.TabIndex = 75;
            this.label6.Text = "Orden de Ensayo";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_Guardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Guardar.FlatAppearance.BorderSize = 0;
            this.btn_Guardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Guardar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Guardar.ForeColor = System.Drawing.Color.White;
            this.btn_Guardar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Guardar.Location = new System.Drawing.Point(917, 577);
            this.btn_Guardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(96, 37);
            this.btn_Guardar.TabIndex = 78;
            this.btn_Guardar.Text = "Imprimir";
            this.btn_Guardar.UseVisualStyleBackColor = false;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btn_limpiar);
            this.groupBox1.Controls.Add(this.guardar_bd);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btn_Guardar);
            this.groupBox1.Controls.Add(this.lblcant);
            this.groupBox1.Controls.Add(this.Dgv_Consulta);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1286, 648);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blast Hole";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(654, 577);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 35);
            this.button1.TabIndex = 101;
            this.button1.Text = "Código de Barra";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.BackColor = System.Drawing.Color.IndianRed;
            this.btn_limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_limpiar.FlatAppearance.BorderSize = 0;
            this.btn_limpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_limpiar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_limpiar.ForeColor = System.Drawing.Color.White;
            this.btn_limpiar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_limpiar.Location = new System.Drawing.Point(1183, 579);
            this.btn_limpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(94, 35);
            this.btn_limpiar.TabIndex = 100;
            this.btn_limpiar.Text = "Limpiar";
            this.btn_limpiar.UseVisualStyleBackColor = false;
            // 
            // guardar_bd
            // 
            this.guardar_bd.BackColor = System.Drawing.SystemColors.HotTrack;
            this.guardar_bd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guardar_bd.FlatAppearance.BorderSize = 0;
            this.guardar_bd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.guardar_bd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar_bd.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardar_bd.ForeColor = System.Drawing.Color.White;
            this.guardar_bd.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.guardar_bd.Location = new System.Drawing.Point(810, 578);
            this.guardar_bd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guardar_bd.Name = "guardar_bd";
            this.guardar_bd.Size = new System.Drawing.Size(92, 36);
            this.guardar_bd.TabIndex = 99;
            this.guardar_bd.Text = "Guardar";
            this.guardar_bd.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.Location = new System.Drawing.Point(1029, 579);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 35);
            this.button3.TabIndex = 98;
            this.button3.Text = "Código de Barra";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // lblcant
            // 
            this.lblcant.AutoSize = true;
            this.lblcant.Location = new System.Drawing.Point(1223, 625);
            this.lblcant.Name = "lblcant";
            this.lblcant.Size = new System.Drawing.Size(53, 20);
            this.lblcant.TabIndex = 7;
            this.lblcant.Text = "label2";
            this.lblcant.Visible = false;
            // 
            // gDMS_ANTAPACCAYDataSet
            // 
            this.gDMS_ANTAPACCAYDataSet.DataSetName = "GDMS_ANTAPACCAYDataSet";
            this.gDMS_ANTAPACCAYDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gDMSANTAPACCAYDataSetBindingSource
            // 
            this.gDMSANTAPACCAYDataSetBindingSource.DataSource = this.gDMS_ANTAPACCAYDataSet;
            this.gDMSANTAPACCAYDataSetBindingSource.Position = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form_Orden_Humedad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1310, 943);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Consulta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Orden_Humedad";
            this.Text = "ORDEN DE ANÁLISIS";
            this.Load += new System.EventHandler(this.Form_Orden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Consulta)).EndInit();
            this.Consulta.ResumeLayout(false);
            this.Consulta.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gDMS_ANTAPACCAYDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gDMSANTAPACCAYDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView Dgv_Consulta;
        private System.Windows.Forms.GroupBox Consulta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_consultar;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblcant;
        private System.Windows.Forms.ComboBox cbo_proyecto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_desde;
        private System.Windows.Forms.DateTimePicker dtp_inicio;
        private System.Windows.Forms.DateTimePicker dtp_fin;
        private System.Windows.Forms.Button btn_Buscar;
        private System.Windows.Forms.BindingSource gDMSANTAPACCAYDataSetBindingSource;
        private GDMS_ANTAPACCAYDataSet gDMS_ANTAPACCAYDataSet;
        private System.Windows.Forms.Label cbo_laboratory;
        private System.Windows.Forms.ComboBox cbo_Laborat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button guardar_bd;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_limpiar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
    }
}