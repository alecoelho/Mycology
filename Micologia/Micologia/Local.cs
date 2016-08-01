using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;

namespace Micologia
{
    public partial class Local : Form
    {
        private int codigo = 0;
        LocalBL blResultado = new LocalBL();
        Modelo.MICOLOGIA_Local entity;

        public Local(int pCodigo)
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

            entity = blResultado.ListarPorId(codigo);

            if (entity != null)
            {
                txtNome.Text = entity.sDescricao;
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
                entity = new Modelo.MICOLOGIA_Local();

            entity.sDescricao = txtNome.Text.Trim();

            if (codigo != 0)
            {
                blResultado.Alterar(entity);
                MessageBox.Show("Alteração efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                blResultado.Inserir(entity);
                MessageBox.Show("Inclusão efetuada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }

            Cursor.Current = Cursors.Default;
        }
        private bool Criticar()
        {
            if (String.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                MessageBox.Show("Local Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void LimparCampos()
        {
            txtNome.Text = String.Empty;
        }
    }
}
