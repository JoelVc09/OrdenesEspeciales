namespace OrdenesEspeciales
{
    partial class Form_Orden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Orden));
            this.label1 = new System.Windows.Forms.Label();
            this.Dgv_Consulta = new System.Windows.Forms.DataGridView();
            this.gb_Analisis = new System.Windows.Forms.GroupBox();
            this.cbo_FeTot = new System.Windows.Forms.CheckBox();
            this.cbo_CuOxi = new System.Windows.Forms.CheckBox();
            this.cbo_CuRes = new System.Windows.Forms.CheckBox();
            this.cbo_CuSCn = new System.Windows.Forms.CheckBox();
            this.cbo_CuSAc = new System.Windows.Forms.CheckBox();
            this.cbo_CO3 = new System.Windows.Forms.CheckBox();
            this.cbo_Mo = new System.Windows.Forms.CheckBox();
            this.cbo_Ag = new System.Windows.Forms.CheckBox();
            this.cbo_Au = new System.Windows.Forms.CheckBox();
            this.cbo_CuSol = new System.Windows.Forms.CheckBox();
            this.cbo_CuTot = new System.Windows.Forms.CheckBox();
            this.Consulta = new System.Windows.Forms.GroupBox();
            this.txt_Orden = new System.Windows.Forms.TextBox();
            this.cb_banco = new System.Windows.Forms.ComboBox();
            this.lb_blanco = new System.Windows.Forms.Label();
            this.cbo_laboratory = new System.Windows.Forms.Label();
            this.cbo_Laborat = new System.Windows.Forms.ComboBox();
            this.btn_Buscar = new System.Windows.Forms.Button();
            this.dtp_fin = new System.Windows.Forms.DateTimePicker();
            this.dtp_inicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_desde = new System.Windows.Forms.Label();
            this.cbo_proyecto = new System.Windows.Forms.ComboBox();
            this.btn_crear = new System.Windows.Forms.Button();
            this.btn_consultar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Dgv_Orden = new System.Windows.Forms.DataGridView();
            this.sdk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blasthole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodMuestra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCtrl = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuTot = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CuOx = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CuSol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Au = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Ag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Mo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CO3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CSAc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CSCn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CuRes = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FeTot = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.guardar_bd = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblcount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnBlancos = new System.Windows.Forms.Button();
            this.cbo_CtrlB = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblcant = new System.Windows.Forms.Label();
            this.gDMS_ANTAPACCAYDataSet = new OrdenesEspeciales.GDMS_ANTAPACCAYDataSet();
            this.gDMSANTAPACCAYDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.count_es = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_guardar2 = new System.Windows.Forms.Button();
            this.dgv_esp = new System.Windows.Forms.DataGridView();
            this.muestrases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoensayo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ismr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.litologia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porarcilla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cordeste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cordnorte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txttipomuestra_Es = new System.Windows.Forms.TextBox();
            this.dtpfecha_es = new System.Windows.Forms.DateTimePicker();
            this.txtLaboratorio_Es = new System.Windows.Forms.TextBox();
            this.txtorden_es = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Consulta)).BeginInit();
            this.gb_Analisis.SuspendLayout();
            this.Consulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Orden)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gDMS_ANTAPACCAYDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gDMSANTAPACCAYDataSetBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_esp)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Orden *";
            // 
            // Dgv_Consulta
            // 
            this.Dgv_Consulta.AllowUserToAddRows = false;
            this.Dgv_Consulta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_Consulta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_Consulta.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.Dgv_Consulta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.Dgv_Consulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Consulta.Location = new System.Drawing.Point(36, 78);
            this.Dgv_Consulta.Name = "Dgv_Consulta";
            this.Dgv_Consulta.RowHeadersWidth = 51;
            this.Dgv_Consulta.RowTemplate.Height = 24;
            this.Dgv_Consulta.Size = new System.Drawing.Size(1193, 186);
            this.Dgv_Consulta.TabIndex = 1;
            // 
            // gb_Analisis
            // 
            this.gb_Analisis.Controls.Add(this.cbo_FeTot);
            this.gb_Analisis.Controls.Add(this.cbo_CuOxi);
            this.gb_Analisis.Controls.Add(this.cbo_CuRes);
            this.gb_Analisis.Controls.Add(this.cbo_CuSCn);
            this.gb_Analisis.Controls.Add(this.cbo_CuSAc);
            this.gb_Analisis.Controls.Add(this.cbo_CO3);
            this.gb_Analisis.Controls.Add(this.cbo_Mo);
            this.gb_Analisis.Controls.Add(this.cbo_Ag);
            this.gb_Analisis.Controls.Add(this.cbo_Au);
            this.gb_Analisis.Controls.Add(this.cbo_CuSol);
            this.gb_Analisis.Controls.Add(this.cbo_CuTot);
            this.gb_Analisis.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Analisis.Location = new System.Drawing.Point(15, 383);
            this.gb_Analisis.Name = "gb_Analisis";
            this.gb_Analisis.Size = new System.Drawing.Size(384, 123);
            this.gb_Analisis.TabIndex = 2;
            this.gb_Analisis.TabStop = false;
            this.gb_Analisis.Text = "Enviar analisis por :";
            // 
            // cbo_FeTot
            // 
            this.cbo_FeTot.AutoSize = true;
            this.cbo_FeTot.Location = new System.Drawing.Point(208, 68);
            this.cbo_FeTot.Name = "cbo_FeTot";
            this.cbo_FeTot.Size = new System.Drawing.Size(70, 20);
            this.cbo_FeTot.TabIndex = 32;
            this.cbo_FeTot.Text = "FeTot";
            this.cbo_FeTot.UseVisualStyleBackColor = false;
            this.cbo_FeTot.CheckedChanged += new System.EventHandler(this.cbo_FeTot_CheckedChanged);
            // 
            // cbo_CuOxi
            // 
            this.cbo_CuOxi.AutoSize = true;
            this.cbo_CuOxi.Location = new System.Drawing.Point(18, 44);
            this.cbo_CuOxi.Name = "cbo_CuOxi";
            this.cbo_CuOxi.Size = new System.Drawing.Size(69, 20);
            this.cbo_CuOxi.TabIndex = 21;
            this.cbo_CuOxi.Text = "CuOxi";
            this.cbo_CuOxi.UseVisualStyleBackColor = false;
            this.cbo_CuOxi.CheckedChanged += new System.EventHandler(this.cbo_CuOxi_CheckedChanged);
            // 
            // cbo_CuRes
            // 
            this.cbo_CuRes.AutoSize = true;
            this.cbo_CuRes.Location = new System.Drawing.Point(208, 46);
            this.cbo_CuRes.Name = "cbo_CuRes";
            this.cbo_CuRes.Size = new System.Drawing.Size(75, 20);
            this.cbo_CuRes.TabIndex = 30;
            this.cbo_CuRes.Text = "CuRes";
            this.cbo_CuRes.UseVisualStyleBackColor = false;
            this.cbo_CuRes.CheckedChanged += new System.EventHandler(this.cbo_CuRes_CheckedChanged);
            // 
            // cbo_CuSCn
            // 
            this.cbo_CuSCn.AutoSize = true;
            this.cbo_CuSCn.Location = new System.Drawing.Point(208, 22);
            this.cbo_CuSCn.Name = "cbo_CuSCn";
            this.cbo_CuSCn.Size = new System.Drawing.Size(75, 20);
            this.cbo_CuSCn.TabIndex = 29;
            this.cbo_CuSCn.Text = "CuSCn";
            this.cbo_CuSCn.UseVisualStyleBackColor = false;
            this.cbo_CuSCn.CheckedChanged += new System.EventHandler(this.cbo_CSCn_CheckedChanged);
            // 
            // cbo_CuSAc
            // 
            this.cbo_CuSAc.AutoSize = true;
            this.cbo_CuSAc.Location = new System.Drawing.Point(115, 91);
            this.cbo_CuSAc.Name = "cbo_CuSAc";
            this.cbo_CuSAc.Size = new System.Drawing.Size(75, 20);
            this.cbo_CuSAc.TabIndex = 28;
            this.cbo_CuSAc.Text = "CuSAc";
            this.cbo_CuSAc.UseVisualStyleBackColor = false;
            this.cbo_CuSAc.CheckedChanged += new System.EventHandler(this.cbo_CSAc_CheckedChanged);
            // 
            // cbo_CO3
            // 
            this.cbo_CO3.AutoSize = true;
            this.cbo_CO3.Location = new System.Drawing.Point(115, 67);
            this.cbo_CO3.Name = "cbo_CO3";
            this.cbo_CO3.Size = new System.Drawing.Size(58, 20);
            this.cbo_CO3.TabIndex = 27;
            this.cbo_CO3.Text = "CO3";
            this.cbo_CO3.UseVisualStyleBackColor = false;
            this.cbo_CO3.CheckedChanged += new System.EventHandler(this.cbo_CO3_CheckedChanged);
            // 
            // cbo_Mo
            // 
            this.cbo_Mo.AutoSize = true;
            this.cbo_Mo.Location = new System.Drawing.Point(115, 44);
            this.cbo_Mo.Name = "cbo_Mo";
            this.cbo_Mo.Size = new System.Drawing.Size(50, 20);
            this.cbo_Mo.TabIndex = 25;
            this.cbo_Mo.Text = "Mo";
            this.cbo_Mo.UseVisualStyleBackColor = false;
            this.cbo_Mo.CheckedChanged += new System.EventHandler(this.cbo_Mo_CheckedChanged);
            // 
            // cbo_Ag
            // 
            this.cbo_Ag.AutoSize = true;
            this.cbo_Ag.Location = new System.Drawing.Point(115, 20);
            this.cbo_Ag.Name = "cbo_Ag";
            this.cbo_Ag.Size = new System.Drawing.Size(48, 20);
            this.cbo_Ag.TabIndex = 24;
            this.cbo_Ag.Text = "Ag";
            this.cbo_Ag.UseVisualStyleBackColor = false;
            this.cbo_Ag.CheckedChanged += new System.EventHandler(this.cbo_Ag_CheckedChanged);
            // 
            // cbo_Au
            // 
            this.cbo_Au.AutoSize = true;
            this.cbo_Au.Location = new System.Drawing.Point(18, 93);
            this.cbo_Au.Name = "cbo_Au";
            this.cbo_Au.Size = new System.Drawing.Size(47, 20);
            this.cbo_Au.TabIndex = 23;
            this.cbo_Au.Text = "Au";
            this.cbo_Au.UseVisualStyleBackColor = false;
            this.cbo_Au.CheckedChanged += new System.EventHandler(this.cbo_Au_CheckedChanged);
            // 
            // cbo_CuSol
            // 
            this.cbo_CuSol.AutoSize = true;
            this.cbo_CuSol.Location = new System.Drawing.Point(18, 68);
            this.cbo_CuSol.Name = "cbo_CuSol";
            this.cbo_CuSol.Size = new System.Drawing.Size(70, 20);
            this.cbo_CuSol.TabIndex = 22;
            this.cbo_CuSol.Text = "CuSol";
            this.cbo_CuSol.UseVisualStyleBackColor = false;
            this.cbo_CuSol.CheckedChanged += new System.EventHandler(this.cbo_CuSol_CheckedChanged);
            // 
            // cbo_CuTot
            // 
            this.cbo_CuTot.AutoSize = true;
            this.cbo_CuTot.Location = new System.Drawing.Point(18, 20);
            this.cbo_CuTot.Name = "cbo_CuTot";
            this.cbo_CuTot.Size = new System.Drawing.Size(70, 20);
            this.cbo_CuTot.TabIndex = 20;
            this.cbo_CuTot.Text = "CuTot";
            this.cbo_CuTot.UseVisualStyleBackColor = false;
            this.cbo_CuTot.CheckedChanged += new System.EventHandler(this.cbo_CuTot_CheckedChanged);
            // 
            // Consulta
            // 
            this.Consulta.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Consulta.Controls.Add(this.txt_Orden);
            this.Consulta.Controls.Add(this.cb_banco);
            this.Consulta.Controls.Add(this.lb_blanco);
            this.Consulta.Controls.Add(this.cbo_laboratory);
            this.Consulta.Controls.Add(this.cbo_Laborat);
            this.Consulta.Controls.Add(this.btn_Buscar);
            this.Consulta.Controls.Add(this.dtp_fin);
            this.Consulta.Controls.Add(this.dtp_inicio);
            this.Consulta.Controls.Add(this.label2);
            this.Consulta.Controls.Add(this.lb_desde);
            this.Consulta.Controls.Add(this.cbo_proyecto);
            this.Consulta.Controls.Add(this.btn_crear);
            this.Consulta.Controls.Add(this.btn_consultar);
            this.Consulta.Controls.Add(this.label6);
            this.Consulta.Controls.Add(this.label1);
            this.Consulta.Controls.Add(this.Dgv_Consulta);
            this.Consulta.Cursor = System.Windows.Forms.Cursors.Default;
            this.Consulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Consulta.Location = new System.Drawing.Point(6, 17);
            this.Consulta.Name = "Consulta";
            this.Consulta.Size = new System.Drawing.Size(1267, 340);
            this.Consulta.TabIndex = 5;
            this.Consulta.TabStop = false;
            this.Consulta.Text = "Header";
            // 
            // txt_Orden
            // 
            this.txt_Orden.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_Orden.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_Orden.Location = new System.Drawing.Point(36, 302);
            this.txt_Orden.Name = "txt_Orden";
            this.txt_Orden.Size = new System.Drawing.Size(157, 22);
            this.txt_Orden.TabIndex = 100;
            // 
            // cb_banco
            // 
            this.cb_banco.BackColor = System.Drawing.Color.White;
            this.cb_banco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_banco.FormattingEnabled = true;
            this.cb_banco.Location = new System.Drawing.Point(486, 302);
            this.cb_banco.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_banco.Name = "cb_banco";
            this.cb_banco.Size = new System.Drawing.Size(154, 24);
            this.cb_banco.TabIndex = 99;
            // 
            // lb_blanco
            // 
            this.lb_blanco.AutoSize = true;
            this.lb_blanco.Location = new System.Drawing.Point(487, 283);
            this.lb_blanco.Name = "lb_blanco";
            this.lb_blanco.Size = new System.Drawing.Size(57, 16);
            this.lb_blanco.TabIndex = 98;
            this.lb_blanco.Text = "Banco*";
            // 
            // cbo_laboratory
            // 
            this.cbo_laboratory.AutoSize = true;
            this.cbo_laboratory.Location = new System.Drawing.Point(230, 283);
            this.cbo_laboratory.Name = "cbo_laboratory";
            this.cbo_laboratory.Size = new System.Drawing.Size(92, 16);
            this.cbo_laboratory.TabIndex = 97;
            this.cbo_laboratory.Text = "Laboratory *";
            this.cbo_laboratory.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbo_Laborat
            // 
            this.cbo_Laborat.BackColor = System.Drawing.Color.White;
            this.cbo_Laborat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Laborat.FormattingEnabled = true;
            this.cbo_Laborat.Location = new System.Drawing.Point(228, 302);
            this.cbo_Laborat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_Laborat.Name = "cbo_Laborat";
            this.cbo_Laborat.Size = new System.Drawing.Size(237, 24);
            this.cbo_Laborat.TabIndex = 96;
            // 
            // btn_Buscar
            // 
            this.btn_Buscar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_Buscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Buscar.ForeColor = System.Drawing.Color.Black;
            this.btn_Buscar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Buscar.Location = new System.Drawing.Point(312, 20);
            this.btn_Buscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Buscar.Name = "btn_Buscar";
            this.btn_Buscar.Size = new System.Drawing.Size(130, 53);
            this.btn_Buscar.TabIndex = 81;
            this.btn_Buscar.Text = "BUSCAR";
            this.btn_Buscar.UseVisualStyleBackColor = false;
            this.btn_Buscar.Click += new System.EventHandler(this.btn_Buscar_Click);
            // 
            // dtp_fin
            // 
            this.dtp_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fin.Location = new System.Drawing.Point(172, 49);
            this.dtp_fin.Name = "dtp_fin";
            this.dtp_fin.Size = new System.Drawing.Size(121, 22);
            this.dtp_fin.TabIndex = 92;
            // 
            // dtp_inicio
            // 
            this.dtp_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_inicio.Location = new System.Drawing.Point(36, 49);
            this.dtp_inicio.Name = "dtp_inicio";
            this.dtp_inicio.Size = new System.Drawing.Size(130, 22);
            this.dtp_inicio.TabIndex = 91;
            this.dtp_inicio.Value = new System.DateTime(2024, 5, 7, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 90;
            this.label2.Text = "Fecha Final :";
            // 
            // lb_desde
            // 
            this.lb_desde.AutoSize = true;
            this.lb_desde.Location = new System.Drawing.Point(37, 24);
            this.lb_desde.Name = "lb_desde";
            this.lb_desde.Size = new System.Drawing.Size(103, 16);
            this.lb_desde.TabIndex = 81;
            this.lb_desde.Text = "Fecha Inicial :";
            // 
            // cbo_proyecto
            // 
            this.cbo_proyecto.FormattingEnabled = true;
            this.cbo_proyecto.Location = new System.Drawing.Point(569, 38);
            this.cbo_proyecto.Name = "cbo_proyecto";
            this.cbo_proyecto.Size = new System.Drawing.Size(191, 24);
            this.cbo_proyecto.TabIndex = 78;
            this.cbo_proyecto.Text = "36334";
            // 
            // btn_crear
            // 
            this.btn_crear.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_crear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_crear.ForeColor = System.Drawing.Color.Black;
            this.btn_crear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_crear.Location = new System.Drawing.Point(663, 269);
            this.btn_crear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_crear.Name = "btn_crear";
            this.btn_crear.Size = new System.Drawing.Size(130, 57);
            this.btn_crear.TabIndex = 77;
            this.btn_crear.Text = "CREAR";
            this.btn_crear.UseVisualStyleBackColor = false;
            this.btn_crear.Click += new System.EventHandler(this.btn_crear_Click);
            // 
            // btn_consultar
            // 
            this.btn_consultar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_consultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_consultar.ForeColor = System.Drawing.Color.Black;
            this.btn_consultar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_consultar.Location = new System.Drawing.Point(1099, 20);
            this.btn_consultar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_consultar.Name = "btn_consultar";
            this.btn_consultar.Size = new System.Drawing.Size(130, 49);
            this.btn_consultar.TabIndex = 76;
            this.btn_consultar.Text = "CONSULTAR";
            this.btn_consultar.UseVisualStyleBackColor = false;
            this.btn_consultar.Click += new System.EventHandler(this.btn_consultar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(468, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 75;
            this.label6.Text = "Proyecto :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Dgv_Orden
            // 
            this.Dgv_Orden.AllowUserToAddRows = false;
            this.Dgv_Orden.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_Orden.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_Orden.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.Dgv_Orden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Orden.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sdk,
            this.item,
            this.blasthole,
            this.CodMuestra,
            this.MCtrl,
            this.observaciones,
            this.parent,
            this.CuTot,
            this.CuOx,
            this.CuSol,
            this.Au,
            this.Ag,
            this.Mo,
            this.CO3,
            this.CSAc,
            this.CSCn,
            this.CuRes,
            this.FeTot});
            this.Dgv_Orden.Location = new System.Drawing.Point(21, 89);
            this.Dgv_Orden.Name = "Dgv_Orden";
            this.Dgv_Orden.RowHeadersWidth = 51;
            this.Dgv_Orden.RowTemplate.Height = 24;
            this.Dgv_Orden.Size = new System.Drawing.Size(1221, 268);
            this.Dgv_Orden.TabIndex = 6;
            this.Dgv_Orden.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Orden_CellContentClick);
            this.Dgv_Orden.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GRV_DATOS_EditingControlShowing);
            // 
            // sdk
            // 
            this.sdk.HeaderText = "";
            this.sdk.MinimumWidth = 6;
            this.sdk.Name = "sdk";
            this.sdk.Width = 6;
            // 
            // item
            // 
            this.item.HeaderText = "Item";
            this.item.MinimumWidth = 6;
            this.item.Name = "item";
            this.item.Width = 69;
            // 
            // blasthole
            // 
            this.blasthole.HeaderText = "Blast_Hole";
            this.blasthole.MinimumWidth = 6;
            this.blasthole.Name = "blasthole";
            this.blasthole.Visible = false;
            this.blasthole.Width = 112;
            // 
            // CodMuestra
            // 
            this.CodMuestra.HeaderText = "CodMuestra";
            this.CodMuestra.MinimumWidth = 6;
            this.CodMuestra.Name = "CodMuestra";
            this.CodMuestra.Width = 125;
            // 
            // MCtrl
            // 
            this.MCtrl.HeaderText = "MCtrl";
            this.MCtrl.Items.AddRange(new object[] {
            "Duplicado de Campo",
            "Duplicado de Finos",
            "Duplicado de Gruesos",
            "Duplicado de Testigos"});
            this.MCtrl.MinimumWidth = 6;
            this.MCtrl.Name = "MCtrl";
            this.MCtrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MCtrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MCtrl.Width = 76;
            // 
            // observaciones
            // 
            this.observaciones.HeaderText = "Observaciones";
            this.observaciones.MinimumWidth = 6;
            this.observaciones.Name = "observaciones";
            this.observaciones.Width = 147;
            // 
            // parent
            // 
            this.parent.HeaderText = "Parent";
            this.parent.MinimumWidth = 6;
            this.parent.Name = "parent";
            this.parent.Width = 86;
            // 
            // CuTot
            // 
            this.CuTot.FalseValue = "false";
            this.CuTot.HeaderText = "CuTot";
            this.CuTot.IndeterminateValue = "false";
            this.CuTot.MinimumWidth = 6;
            this.CuTot.Name = "CuTot";
            this.CuTot.TrueValue = "true";
            this.CuTot.Width = 57;
            // 
            // CuOx
            // 
            this.CuOx.FalseValue = "false";
            this.CuOx.HeaderText = "CuOx";
            this.CuOx.IndeterminateValue = "false";
            this.CuOx.MinimumWidth = 6;
            this.CuOx.Name = "CuOx";
            this.CuOx.TrueValue = "true";
            this.CuOx.Width = 54;
            // 
            // CuSol
            // 
            this.CuSol.FalseValue = "false";
            this.CuSol.HeaderText = "CuSol";
            this.CuSol.IndeterminateValue = "false";
            this.CuSol.MinimumWidth = 6;
            this.CuSol.Name = "CuSol";
            this.CuSol.TrueValue = "true";
            this.CuSol.Width = 57;
            // 
            // Au
            // 
            this.Au.FalseValue = "false";
            this.Au.HeaderText = "Au";
            this.Au.IndeterminateValue = "false";
            this.Au.MinimumWidth = 6;
            this.Au.Name = "Au";
            this.Au.TrueValue = "true";
            this.Au.Width = 34;
            // 
            // Ag
            // 
            this.Ag.FalseValue = "false";
            this.Ag.HeaderText = "Ag";
            this.Ag.IndeterminateValue = "false";
            this.Ag.MinimumWidth = 6;
            this.Ag.Name = "Ag";
            this.Ag.TrueValue = "true";
            this.Ag.Width = 34;
            // 
            // Mo
            // 
            this.Mo.FalseValue = "false";
            this.Mo.HeaderText = "Mo";
            this.Mo.IndeterminateValue = "false";
            this.Mo.MinimumWidth = 6;
            this.Mo.Name = "Mo";
            this.Mo.TrueValue = "true";
            this.Mo.Width = 35;
            // 
            // CO3
            // 
            this.CO3.FalseValue = "false";
            this.CO3.HeaderText = "CO3";
            this.CO3.IndeterminateValue = "false";
            this.CO3.MinimumWidth = 6;
            this.CO3.Name = "CO3";
            this.CO3.TrueValue = "true";
            this.CO3.Width = 46;
            // 
            // CSAc
            // 
            this.CSAc.FalseValue = "false";
            this.CSAc.HeaderText = "CSAc";
            this.CSAc.IndeterminateValue = "false";
            this.CSAc.MinimumWidth = 6;
            this.CSAc.Name = "CSAc";
            this.CSAc.TrueValue = "true";
            this.CSAc.Width = 55;
            // 
            // CSCn
            // 
            this.CSCn.FalseValue = "false";
            this.CSCn.HeaderText = "CSCn";
            this.CSCn.IndeterminateValue = "false";
            this.CSCn.MinimumWidth = 6;
            this.CSCn.Name = "CSCn";
            this.CSCn.TrueValue = "true";
            this.CSCn.Width = 55;
            // 
            // CuRes
            // 
            this.CuRes.FalseValue = "false";
            this.CuRes.HeaderText = "CuRes";
            this.CuRes.IndeterminateValue = "false";
            this.CuRes.MinimumWidth = 6;
            this.CuRes.Name = "CuRes";
            this.CuRes.TrueValue = "true";
            this.CuRes.Width = 62;
            // 
            // FeTot
            // 
            this.FeTot.FalseValue = "false";
            this.FeTot.HeaderText = "FeTot";
            this.FeTot.IndeterminateValue = "false";
            this.FeTot.MinimumWidth = 6;
            this.FeTot.Name = "FeTot";
            this.FeTot.TrueValue = "true";
            this.FeTot.Width = 55;
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_Guardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Guardar.ForeColor = System.Drawing.Color.Black;
            this.btn_Guardar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Guardar.Location = new System.Drawing.Point(1118, 362);
            this.btn_Guardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(124, 43);
            this.btn_Guardar.TabIndex = 78;
            this.btn_Guardar.Text = "IMPRIMIR";
            this.btn_Guardar.UseVisualStyleBackColor = false;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.guardar_bd);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.lblcount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.gb_Analisis);
            this.groupBox1.Controls.Add(this.btn_Guardar);
            this.groupBox1.Controls.Add(this.BtnBlancos);
            this.groupBox1.Controls.Add(this.cbo_CtrlB);
            this.groupBox1.Controls.Add(this.Dgv_Orden);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblcant);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1267, 526);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blast Hole";
            // 
            // guardar_bd
            // 
            this.guardar_bd.BackColor = System.Drawing.Color.LightSeaGreen;
            this.guardar_bd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardar_bd.ForeColor = System.Drawing.Color.Black;
            this.guardar_bd.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.guardar_bd.Location = new System.Drawing.Point(1118, 409);
            this.guardar_bd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guardar_bd.Name = "guardar_bd";
            this.guardar_bd.Size = new System.Drawing.Size(124, 43);
            this.guardar_bd.TabIndex = 99;
            this.guardar_bd.Text = "GUARDAR / DB";
            this.guardar_bd.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.Location = new System.Drawing.Point(1118, 462);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 43);
            this.button3.TabIndex = 98;
            this.button3.Text = "CÓDIGO BARRA";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcount.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblcount.Location = new System.Drawing.Point(124, 360);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(19, 20);
            this.lblcount.TabIndex = 97;
            this.lblcount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label3.Location = new System.Drawing.Point(12, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 96;
            this.label3.Text = "Recuento : ";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // BtnBlancos
            // 
            this.BtnBlancos.BackColor = System.Drawing.Color.Transparent;
            this.BtnBlancos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBlancos.BackgroundImage")));
            this.BtnBlancos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnBlancos.FlatAppearance.BorderSize = 0;
            this.BtnBlancos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBlancos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBlancos.Location = new System.Drawing.Point(147, 47);
            this.BtnBlancos.Margin = new System.Windows.Forms.Padding(4);
            this.BtnBlancos.Name = "BtnBlancos";
            this.BtnBlancos.Size = new System.Drawing.Size(43, 35);
            this.BtnBlancos.TabIndex = 92;
            this.BtnBlancos.UseVisualStyleBackColor = false;
            this.BtnBlancos.Click += new System.EventHandler(this.BtnBlancos_Click);
            // 
            // cbo_CtrlB
            // 
            this.cbo_CtrlB.BackColor = System.Drawing.Color.White;
            this.cbo_CtrlB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CtrlB.FormattingEnabled = true;
            this.cbo_CtrlB.Items.AddRange(new object[] {
            "MB 105",
            "MC 401",
            "MC 402",
            "MC 403\t",
            "MC 976"});
            this.cbo_CtrlB.Location = new System.Drawing.Point(21, 57);
            this.cbo_CtrlB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_CtrlB.Name = "cbo_CtrlB";
            this.cbo_CtrlB.Size = new System.Drawing.Size(119, 25);
            this.cbo_CtrlB.TabIndex = 80;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label8.Location = new System.Drawing.Point(17, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 20);
            this.label8.TabIndex = 81;
            this.label8.Text = "Controles y Blancos:*";
            // 
            // lblcant
            // 
            this.lblcant.AutoSize = true;
            this.lblcant.Location = new System.Drawing.Point(349, 184);
            this.lblcant.Name = "lblcant";
            this.lblcant.Size = new System.Drawing.Size(52, 17);
            this.lblcant.TabIndex = 7;
            this.lblcant.Text = "label2";
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1619, 1165);
            this.tabControl1.TabIndex = 81;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Consulta);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1611, 1137);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ENVIO DE ORDENES";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1611, 1137);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ORDENES DE HUMEDAD";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.count_es);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.btn_guardar2);
            this.groupBox3.Controls.Add(this.dgv_esp);
            this.groupBox3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.groupBox3.Location = new System.Drawing.Point(17, 209);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1240, 674);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Blast Hole";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button1.Location = new System.Drawing.Point(1084, 617);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "CÓDIGO BARRA";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // count_es
            // 
            this.count_es.AutoSize = true;
            this.count_es.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.count_es.Location = new System.Drawing.Point(61, 569);
            this.count_es.Name = "count_es";
            this.count_es.Size = new System.Drawing.Size(41, 29);
            this.count_es.TabIndex = 3;
            this.count_es.Text = "20";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 580);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "Count : ";
            // 
            // btn_guardar2
            // 
            this.btn_guardar2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_guardar2.Location = new System.Drawing.Point(1084, 566);
            this.btn_guardar2.Name = "btn_guardar2";
            this.btn_guardar2.Size = new System.Drawing.Size(133, 45);
            this.btn_guardar2.TabIndex = 1;
            this.btn_guardar2.Text = "GUARDAR ";
            this.btn_guardar2.UseVisualStyleBackColor = false;
            this.btn_guardar2.Click += new System.EventHandler(this.btn_guardar2_Click);
            // 
            // dgv_esp
            // 
            this.dgv_esp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_esp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.muestrases,
            this.tipoensayo,
            this.tajo,
            this.banco,
            this.equipo,
            this.ismr,
            this.litologia,
            this.porarcilla,
            this.cordeste,
            this.cordnorte,
            this.cota,
            this.descripcion});
            this.dgv_esp.Location = new System.Drawing.Point(21, 38);
            this.dgv_esp.Name = "dgv_esp";
            this.dgv_esp.RowHeadersWidth = 51;
            this.dgv_esp.RowTemplate.Height = 24;
            this.dgv_esp.Size = new System.Drawing.Size(1196, 518);
            this.dgv_esp.TabIndex = 0;
            // 
            // muestrases
            // 
            this.muestrases.HeaderText = "MUESTRAS";
            this.muestrases.MinimumWidth = 6;
            this.muestrases.Name = "muestrases";
            this.muestrases.Width = 125;
            // 
            // tipoensayo
            // 
            this.tipoensayo.HeaderText = "TIPO DE ENSAYO";
            this.tipoensayo.MinimumWidth = 6;
            this.tipoensayo.Name = "tipoensayo";
            this.tipoensayo.Width = 125;
            // 
            // tajo
            // 
            this.tajo.HeaderText = "TAJO";
            this.tajo.MinimumWidth = 6;
            this.tajo.Name = "tajo";
            this.tajo.Width = 125;
            // 
            // banco
            // 
            this.banco.HeaderText = "BANCO";
            this.banco.MinimumWidth = 6;
            this.banco.Name = "banco";
            this.banco.Width = 125;
            // 
            // equipo
            // 
            this.equipo.HeaderText = "EQUIPO";
            this.equipo.MinimumWidth = 6;
            this.equipo.Name = "equipo";
            this.equipo.Width = 125;
            // 
            // ismr
            // 
            this.ismr.HeaderText = "ISMR";
            this.ismr.MinimumWidth = 6;
            this.ismr.Name = "ismr";
            this.ismr.Width = 125;
            // 
            // litologia
            // 
            this.litologia.HeaderText = "LITOLOGIA";
            this.litologia.MinimumWidth = 6;
            this.litologia.Name = "litologia";
            this.litologia.Width = 125;
            // 
            // porarcilla
            // 
            this.porarcilla.HeaderText = "% ARCILLAS";
            this.porarcilla.MinimumWidth = 6;
            this.porarcilla.Name = "porarcilla";
            this.porarcilla.Width = 125;
            // 
            // cordeste
            // 
            this.cordeste.HeaderText = "COORD ESTE";
            this.cordeste.MinimumWidth = 6;
            this.cordeste.Name = "cordeste";
            this.cordeste.Width = 125;
            // 
            // cordnorte
            // 
            this.cordnorte.HeaderText = "COORD NORTE";
            this.cordnorte.MinimumWidth = 6;
            this.cordnorte.Name = "cordnorte";
            this.cordnorte.Width = 125;
            // 
            // cota
            // 
            this.cota.HeaderText = "COTA";
            this.cota.MinimumWidth = 6;
            this.cota.Name = "cota";
            this.cota.Width = 125;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "DESCRIPCIÓN";
            this.descripcion.MinimumWidth = 6;
            this.descripcion.Name = "descripcion";
            this.descripcion.Width = 125;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.txttipomuestra_Es);
            this.groupBox2.Controls.Add(this.dtpfecha_es);
            this.groupBox2.Controls.Add(this.txtLaboratorio_Es);
            this.groupBox2.Controls.Add(this.txtorden_es);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1244, 185);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Head";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button2.Location = new System.Drawing.Point(432, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 52);
            this.button2.TabIndex = 8;
            this.button2.TabStop = false;
            this.button2.Text = "CONSULTAR";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // txttipomuestra_Es
            // 
            this.txttipomuestra_Es.Location = new System.Drawing.Point(180, 124);
            this.txttipomuestra_Es.Name = "txttipomuestra_Es";
            this.txttipomuestra_Es.Size = new System.Drawing.Size(121, 23);
            this.txttipomuestra_Es.TabIndex = 7;
            // 
            // dtpfecha_es
            // 
            this.dtpfecha_es.Location = new System.Drawing.Point(180, 58);
            this.dtpfecha_es.Name = "dtpfecha_es";
            this.dtpfecha_es.Size = new System.Drawing.Size(218, 23);
            this.dtpfecha_es.TabIndex = 6;
            // 
            // txtLaboratorio_Es
            // 
            this.txtLaboratorio_Es.Location = new System.Drawing.Point(180, 89);
            this.txtLaboratorio_Es.Name = "txtLaboratorio_Es";
            this.txtLaboratorio_Es.Size = new System.Drawing.Size(218, 23);
            this.txtLaboratorio_Es.TabIndex = 5;
            // 
            // txtorden_es
            // 
            this.txtorden_es.Location = new System.Drawing.Point(180, 26);
            this.txtorden_es.Name = "txtorden_es";
            this.txtorden_es.Size = new System.Drawing.Size(218, 23);
            this.txtorden_es.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "TIPO DE MUESTRA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "LABORATORIO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "FECHA DE ENVIO";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "ORDEN DE ENSAYO";
            // 
            // Form_Orden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1309, 932);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form_Orden";
            this.Text = "ORDEN DE ANÁLISIS";
            this.Load += new System.EventHandler(this.Form_Orden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Consulta)).EndInit();
            this.gb_Analisis.ResumeLayout(false);
            this.gb_Analisis.PerformLayout();
            this.Consulta.ResumeLayout(false);
            this.Consulta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Orden)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gDMS_ANTAPACCAYDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gDMSANTAPACCAYDataSetBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_esp)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Dgv_Consulta;
        private System.Windows.Forms.GroupBox gb_Analisis;
        private System.Windows.Forms.GroupBox Consulta;
        private System.Windows.Forms.DataGridView Dgv_Orden;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_crear;
        private System.Windows.Forms.Button btn_consultar;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblcant;
        private System.Windows.Forms.CheckBox cbo_CuTot;
        private System.Windows.Forms.CheckBox cbo_FeTot;
        private System.Windows.Forms.CheckBox cbo_CuRes;
        private System.Windows.Forms.CheckBox cbo_CuSCn;
        private System.Windows.Forms.CheckBox cbo_CuSAc;
        private System.Windows.Forms.CheckBox cbo_CO3;
        private System.Windows.Forms.CheckBox cbo_Mo;
        private System.Windows.Forms.CheckBox cbo_Ag;
        private System.Windows.Forms.CheckBox cbo_Au;
        private System.Windows.Forms.CheckBox cbo_CuSol;
        private System.Windows.Forms.CheckBox cbo_CuOxi;
        private System.Windows.Forms.ComboBox cbo_CtrlB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnBlancos;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.ComboBox cb_banco;
        private System.Windows.Forms.Label lb_blanco;
        private System.Windows.Forms.TextBox txt_Orden;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLaboratorio_Es;
        private System.Windows.Forms.TextBox txtorden_es;
        private System.Windows.Forms.TextBox txttipomuestra_Es;
        private System.Windows.Forms.DateTimePicker dtpfecha_es;
        private System.Windows.Forms.DataGridView dgv_esp;
        private System.Windows.Forms.Button btn_guardar2;
        private System.Windows.Forms.Label count_es;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn muestrases;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoensayo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tajo;
        private System.Windows.Forms.DataGridViewTextBoxColumn banco;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ismr;
        private System.Windows.Forms.DataGridViewTextBoxColumn litologia;
        private System.Windows.Forms.DataGridViewTextBoxColumn porarcilla;
        private System.Windows.Forms.DataGridViewTextBoxColumn cordeste;
        private System.Windows.Forms.DataGridViewTextBoxColumn cordnorte;
        private System.Windows.Forms.DataGridViewTextBoxColumn cota;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sdk;
        private System.Windows.Forms.DataGridViewTextBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn blasthole;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodMuestra;
        private System.Windows.Forms.DataGridViewComboBoxColumn MCtrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CuTot;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CuOx;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CuSol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Au;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Ag;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Mo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CO3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CSAc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CSCn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CuRes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FeTot;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button guardar_bd;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}