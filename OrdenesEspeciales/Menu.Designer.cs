﻿
namespace OrdenesEspeciales
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.BtnReassay = new System.Windows.Forms.Button();
            this.BtnCompositos = new System.Windows.Forms.Button();
            this.Btn_Envio_Ordenes = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Nom_Usua = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnReassay
            // 
            this.BtnReassay.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnReassay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReassay.Location = new System.Drawing.Point(12, 49);
            this.BtnReassay.Name = "BtnReassay";
            this.BtnReassay.Size = new System.Drawing.Size(156, 113);
            this.BtnReassay.TabIndex = 0;
            this.BtnReassay.Text = "Ordenes Especiales";
            this.BtnReassay.UseVisualStyleBackColor = false;
            this.BtnReassay.Click += new System.EventHandler(this.BtnReassay_Click);
            // 
            // BtnCompositos
            // 
            this.BtnCompositos.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnCompositos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCompositos.Location = new System.Drawing.Point(201, 49);
            this.BtnCompositos.Name = "BtnCompositos";
            this.BtnCompositos.Size = new System.Drawing.Size(143, 113);
            this.BtnCompositos.TabIndex = 1;
            this.BtnCompositos.Text = "Compositos";
            this.BtnCompositos.UseVisualStyleBackColor = false;
            this.BtnCompositos.Click += new System.EventHandler(this.BtnCompositos_Click);
            // 
            // Btn_Envio_Ordenes
            // 
            this.Btn_Envio_Ordenes.BackColor = System.Drawing.Color.SteelBlue;
            this.Btn_Envio_Ordenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Envio_Ordenes.Location = new System.Drawing.Point(379, 49);
            this.Btn_Envio_Ordenes.Name = "Btn_Envio_Ordenes";
            this.Btn_Envio_Ordenes.Size = new System.Drawing.Size(150, 113);
            this.Btn_Envio_Ordenes.TabIndex = 129;
            this.Btn_Envio_Ordenes.Text = "Ordenes de Analisis BH ";
            this.Btn_Envio_Ordenes.UseVisualStyleBackColor = false;
            this.Btn_Envio_Ordenes.Click += new System.EventHandler(this.Btn_Envio_Ordenes_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(169, 196);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(215, 46);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 128;
            this.pictureBox3.TabStop = false;
            // 
            // Nom_Usua
            // 
            this.Nom_Usua.AutoSize = true;
            this.Nom_Usua.Location = new System.Drawing.Point(505, 228);
            this.Nom_Usua.Name = "Nom_Usua";
            this.Nom_Usua.Size = new System.Drawing.Size(0, 20);
            this.Nom_Usua.TabIndex = 130;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(561, 253);
            this.Controls.Add(this.Nom_Usua);
            this.Controls.Add(this.Btn_Envio_Ordenes);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.BtnCompositos);
            this.Controls.Add(this.BtnReassay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Preparación";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnReassay;
        private System.Windows.Forms.Button BtnCompositos;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button Btn_Envio_Ordenes;
        private System.Windows.Forms.Label Nom_Usua;
    }
}