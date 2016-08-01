namespace Micologia
{
    partial class ExameConsulta
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
            form = null;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.brnInserir = new System.Windows.Forms.Button();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.dgvConsulta = new System.Windows.Forms.DataGridView();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.lblNumeroExame = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtExame = new System.Windows.Forms.TextBox();
            this.txtProntuario = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.MaskedTextBox();
            this.cmbCultura = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbDireto = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.Label200 = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsulta)).BeginInit();
            this.SuspendLayout();
            // 
            // brnInserir
            // 
            this.brnInserir.Location = new System.Drawing.Point(999, 94);
            this.brnInserir.Name = "brnInserir";
            this.brnInserir.Size = new System.Drawing.Size(75, 23);
            this.brnInserir.TabIndex = 6;
            this.brnInserir.Text = "Novo Exame";
            this.brnInserir.UseVisualStyleBackColor = true;
            this.brnInserir.Click += new System.EventHandler(this.brnInserir_Click);
            // 
            // txtPaciente
            // 
            this.txtPaciente.Location = new System.Drawing.Point(670, 18);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.Size = new System.Drawing.Size(228, 20);
            this.txtPaciente.TabIndex = 3;
            this.txtPaciente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaciente_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(612, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 119;
            this.label2.Text = "Paciente:";
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(918, 93);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisar.TabIndex = 5;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // dgvConsulta
            // 
            this.dgvConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsulta.Location = new System.Drawing.Point(12, 139);
            this.dgvConsulta.Name = "dgvConsulta";
            this.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsulta.Size = new System.Drawing.Size(886, 573);
            this.dgvConsulta.TabIndex = 117;
            this.dgvConsulta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsulta_CellContentClick);
            // 
            // txtMedico
            // 
            this.txtMedico.Location = new System.Drawing.Point(105, 55);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.Size = new System.Drawing.Size(223, 20);
            this.txtMedico.TabIndex = 4;
            this.txtMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMedico_KeyDown);
            // 
            // lblNumeroExame
            // 
            this.lblNumeroExame.AutoSize = true;
            this.lblNumeroExame.Location = new System.Drawing.Point(17, 21);
            this.lblNumeroExame.Name = "lblNumeroExame";
            this.lblNumeroExame.Size = new System.Drawing.Size(82, 13);
            this.lblNumeroExame.TabIndex = 123;
            this.lblNumeroExame.Text = "Número Exame:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 124;
            this.label1.Text = "Data Exame:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 125;
            this.label3.Text = "Número Prontuário:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Médico Solicitante:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(943, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 24);
            this.label5.TabIndex = 131;
            this.label5.Text = "Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(997, 22);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(21, 24);
            this.lblTotal.TabIndex = 132;
            this.lblTotal.Text = "0";
            // 
            // txtExame
            // 
            this.txtExame.Location = new System.Drawing.Point(105, 18);
            this.txtExame.Name = "txtExame";
            this.txtExame.Size = new System.Drawing.Size(103, 20);
            this.txtExame.TabIndex = 0;
            this.txtExame.TextChanged += new System.EventHandler(this.txtExame_TextChanged);
            this.txtExame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExame_KeyDown);
            this.txtExame.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExame_KeyPress);
            // 
            // txtProntuario
            // 
            this.txtProntuario.Location = new System.Drawing.Point(314, 18);
            this.txtProntuario.Name = "txtProntuario";
            this.txtProntuario.Size = new System.Drawing.Size(104, 20);
            this.txtProntuario.TabIndex = 1;
            this.txtProntuario.TextChanged += new System.EventHandler(this.txtProntuario_TextChanged);
            this.txtProntuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProntuario_KeyDown);
            this.txtProntuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProntuario_KeyPress);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(498, 18);
            this.txtData.Mask = "00/00/0000";
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(104, 20);
            this.txtData.TabIndex = 2;
            this.txtData.ValidatingType = typeof(System.DateTime);
            this.txtData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtData_KeyDown);
            // 
            // cmbCultura
            // 
            this.cmbCultura.FormattingEnabled = true;
            this.cmbCultura.Location = new System.Drawing.Point(545, 96);
            this.cmbCultura.Name = "cmbCultura";
            this.cmbCultura.Size = new System.Drawing.Size(353, 21);
            this.cmbCultura.TabIndex = 134;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(497, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 136;
            this.label11.Text = "Cultura:";
            // 
            // cmbDireto
            // 
            this.cmbDireto.FormattingEnabled = true;
            this.cmbDireto.Location = new System.Drawing.Point(105, 96);
            this.cmbDireto.Name = "cmbDireto";
            this.cmbDireto.Size = new System.Drawing.Size(353, 21);
            this.cmbDireto.TabIndex = 133;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 135;
            this.label10.Text = "Ex Micol Direto:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 138;
            this.label6.Text = "Local:";
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Location = new System.Drawing.Point(670, 54);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(228, 21);
            this.cmbMaterial.TabIndex = 139;
            // 
            // Label200
            // 
            this.Label200.AutoSize = true;
            this.Label200.Location = new System.Drawing.Point(616, 57);
            this.Label200.Name = "Label200";
            this.Label200.Size = new System.Drawing.Size(47, 13);
            this.Label200.TabIndex = 140;
            this.Label200.Text = "Material:";
            // 
            // txtLocal
            // 
            this.txtLocal.Location = new System.Drawing.Point(383, 54);
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.Size = new System.Drawing.Size(228, 20);
            this.txtLocal.TabIndex = 141;
            this.txtLocal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocal_KeyDown);
            // 
            // ExameConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 728);
            this.Controls.Add(this.txtLocal);
            this.Controls.Add(this.cmbMaterial);
            this.Controls.Add(this.Label200);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbCultura);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbDireto);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.txtProntuario);
            this.Controls.Add(this.txtExame);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMedico);
            this.Controls.Add(this.lblNumeroExame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.brnInserir);
            this.Controls.Add(this.txtPaciente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.dgvConsulta);
            this.Name = "ExameConsulta";
            this.Text = "ExameConsulta";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsulta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button brnInserir;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView dgvConsulta;
        private System.Windows.Forms.TextBox txtMedico;
        private System.Windows.Forms.Label lblNumeroExame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtExame;
        private System.Windows.Forms.TextBox txtProntuario;
        private System.Windows.Forms.MaskedTextBox txtData;
        private System.Windows.Forms.ComboBox cmbCultura;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbDireto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.Label Label200;
        private System.Windows.Forms.TextBox txtLocal;
    }
}