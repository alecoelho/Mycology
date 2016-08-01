namespace Micologia
{
    partial class RelatorioGerenciais
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
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpAte = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDe = new System.Windows.Forms.DateTimePicker();
            this.btnTotalExameMicologicoDireto = new System.Windows.Forms.Button();
            this.btnTotalCultura = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "DATA DO EXAME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = " ATÉ :";
            // 
            // dtpAte
            // 
            this.dtpAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAte.Location = new System.Drawing.Point(59, 72);
            this.dtpAte.Name = "dtpAte";
            this.dtpAte.Size = new System.Drawing.Size(110, 20);
            this.dtpAte.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "DE :";
            // 
            // dtpDe
            // 
            this.dtpDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDe.Location = new System.Drawing.Point(59, 45);
            this.dtpDe.Name = "dtpDe";
            this.dtpDe.Size = new System.Drawing.Size(110, 20);
            this.dtpDe.TabIndex = 22;
            // 
            // btnTotalExameMicologicoDireto
            // 
            this.btnTotalExameMicologicoDireto.Location = new System.Drawing.Point(204, 73);
            this.btnTotalExameMicologicoDireto.Name = "btnTotalExameMicologicoDireto";
            this.btnTotalExameMicologicoDireto.Size = new System.Drawing.Size(176, 23);
            this.btnTotalExameMicologicoDireto.TabIndex = 21;
            this.btnTotalExameMicologicoDireto.Text = "Total Exame Micologico Direto";
            this.btnTotalExameMicologicoDireto.UseVisualStyleBackColor = true;
            this.btnTotalExameMicologicoDireto.Visible = false;
            this.btnTotalExameMicologicoDireto.Click += new System.EventHandler(this.btnTotalExameMicologicoDireto_Click);
            // 
            // btnTotalCultura
            // 
            this.btnTotalCultura.Location = new System.Drawing.Point(204, 40);
            this.btnTotalCultura.Name = "btnTotalCultura";
            this.btnTotalCultura.Size = new System.Drawing.Size(93, 23);
            this.btnTotalCultura.TabIndex = 20;
            this.btnTotalCultura.Text = "Gerar Planilha";
            this.btnTotalCultura.UseVisualStyleBackColor = true;
            this.btnTotalCultura.Click += new System.EventHandler(this.btnTotalCultura_Click);
            // 
            // RelatorioGerenciais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 306);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpAte);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDe);
            this.Controls.Add(this.btnTotalExameMicologicoDireto);
            this.Controls.Add(this.btnTotalCultura);
            this.Name = "RelatorioGerenciais";
            this.Text = "RelatorioGerenciais";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpAte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDe;
        private System.Windows.Forms.Button btnTotalExameMicologicoDireto;
        private System.Windows.Forms.Button btnTotalCultura;
    }
}