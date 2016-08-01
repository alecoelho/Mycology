using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;

namespace Micologia
{
    public partial class Paciente : Form
    {
        private int codigo = 0;
        PacienteBL blPaciente = new PacienteBL();
        Modelo.MICOLOGIA_Paciente entity;

        public Paciente(int pCodigo)
        {
            InitializeComponent();

            if (pCodigo != 0)
                LoadForm(pCodigo);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadForm(int pCodigo)
        {
            codigo = pCodigo;

            entity = blPaciente.ListarPorId(codigo);

            if (entity != null)
            {
                txtNome.Text = entity.sNome;
                txtEmail.Text = entity.sEmail;
                txtObs.Text = entity.sObservacao;

                txtTelefone.Text = entity.sTelefone;
                txtCelular.Text = entity.sCelular;

                if (!entity.bBranco)
                {
                    rdbBranco.Checked = false;
                    rdbNaoBranco.Checked = true;
                }

                if (entity.sSexo == "M")
                {
                    rdbFeminino.Checked = false;
                    rdbMasculino.Checked = true;
                }

                ckbAtivos.Checked = entity.bAtivo;
                if(entity.dDataNascimento != null) txtData.Text = entity.dDataNascimento.Value.ToString("dd/MM/yyyy");
            }
            else
                codigo = 0;
        }
        private void Salvar()
        {
            if (!Criticar())
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (entity == null)
                entity = new Modelo.MICOLOGIA_Paciente();

            entity.sNome = txtNome.Text.Trim();
            entity.sEmail = txtEmail.Text.Trim();
            entity.sObservacao = txtObs.Text.Trim();

            entity.sTelefone = txtTelefone.Text.Trim();
            entity.sCelular = txtCelular.Text.Trim();
            entity.bBranco = (rdbBranco.Checked) ? true : false;

            entity.sSexo = (rdbFeminino.Checked) ? "F" : "M";
            if (txtData.Text != "  /  /") entity.dDataNascimento = Convert.ToDateTime(txtData.Text);
            else entity.dDataNascimento = null;

            entity.bAtivo = ckbAtivos.Checked;

            if (codigo != 0)
            {
                blPaciente.Alterar(entity);
                MessageBox.Show("Alteração efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                blPaciente.Inserir(entity);
                MessageBox.Show("Inclusão efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }

            Cursor.Current = Cursors.Default;
        }
        private bool Criticar()
        {
            if (String.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                MessageBox.Show("Nome Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtData.Text != "  /  /")
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtData.Text);
                }
                catch
                {
                    MessageBox.Show("Data Inválida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Convert.ToDateTime(txtData.Text + " " + "00:00:00") > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy 00:00:00")))
                {
                    MessageBox.Show("Data de Nascimento superior a data de hoje", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private void LimparCampos()
        {
            txtNome.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtObs.Text = String.Empty;

            txtTelefone.Text = "(  )    -    ";
            txtCelular.Text = "(  )    -    ";
            rdbNaoBranco.Checked = false;
            rdbBranco.Checked = true;

            rdbMasculino.Checked = false;
            rdbFeminino.Checked = true;
            txtData.Text = String.Empty;
        }
    }
}
