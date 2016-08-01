using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;

namespace Micologia
{
    public partial class frmUsuarioCadastro : Form
    {
        private int codigo = 0;
        MICOLOGIA_Usuario login;
        UsuarioBL blusuario = new UsuarioBL();

        public frmUsuarioCadastro(int pCodigo)
        {
            InitializeComponent();

            if (pCodigo != 0)
                LoadForm(pCodigo);
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private bool Criticar()
        {
            if (txtUsuario.Text.Equals(""))
            {
                MessageBox.Show("Nome do Usuário Obrigatório");
                return false;
            }

            if (txtLogin.Text.Equals(""))
            {
                MessageBox.Show("Login do Usuário Obrigatório");
                return false;
            }

            if (txtSenha.Text.Equals(""))
            {
                MessageBox.Show("Senha do Usuário Obrigatória");
                return false;
            }

            if (txtConfirma.Text.Equals(""))
            {
                MessageBox.Show("Confirmação de senha Obrigatória");
                return false;
            }

            if (!txtConfirma.Text.Equals(txtSenha.Text))
            {
                MessageBox.Show("Senhas não conferem");
                return false;
            }

            if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("Email do Usuário Obrigatório");
                return false;
            }

            return true;
        }
        private void Salvar()
        {
            if (!Criticar())
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (login == null)
                login = new MICOLOGIA_Usuario();

            login.nIDLogin = codigo;
            login.sNome = txtUsuario.Text;
            login.sLogin = txtLogin.Text;
            login.sSenha = txtSenha.Text;

            login.sEmail = txtEmail.Text;
            login.sTelefone = txtTelefone.Text;
            login.sCargo = txtCargo.Text;
            login.bAtivo = ckbAtivos.Checked;

            login.sSexo = (rdbFeminino.Checked) ? "F" : "M";

            if (codigo == 0)
            {
                blusuario.Inserir(login);
                limparCampos();
                MessageBox.Show("Inclusão efetuada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                blusuario.Alterar(login);
                MessageBox.Show("Alteração efetuada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }
        private void LoadForm(int pCodigo)
        {
            codigo = pCodigo;

            login = blusuario.ListarPorId(codigo);

            if (login != null)
            {
                txtEmail.Text = login.sEmail;
                txtLogin.Text = login.sLogin;
                txtUsuario.Text = login.sNome;
                txtSenha.Text = login.sSenha;
                txtConfirma.Text = login.sSenha;
                txtTelefone.Text = login.sTelefone;
                txtCargo.Text = login.sCargo;

                if (login.sSexo == "M")
                {
                    rdbFeminino.Checked = false;
                    rdbMasculino.Checked = true;
                }

                ckbAtivos.Checked = login.bAtivo;

                txtUsuario.Enabled = false;
            }
            else
                codigo = 0;
        }
        public void limparCampos()
        {
            txtEmail.Text = String.Empty;
            txtLogin.Text = String.Empty;
            txtUsuario.Text = String.Empty;
            txtSenha.Text = String.Empty;
            txtConfirma.Text = String.Empty;
            txtTelefone.Text = String.Empty;
            txtCargo.Text = String.Empty;
        }
    }
}
