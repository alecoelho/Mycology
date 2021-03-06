﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Micologia.Negocio;
using Micologia.Modelo;
using System.Linq;

namespace Micologia
{
    public partial class ResultadoConsulta : Form
    {
        IList<MICOLOGIA_Resultado> lista;

        public ResultadoConsulta()
        {
            InitializeComponent();

            txtPesquisa.Select();

            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Resultado" && item.sIncluir.Trim() == "N" select item).Count() > 0)
                brnNovo.Visible = false;
        }
        private void brnNovo_Click(object sender, EventArgs e)
        {
            LoadDetalhe(0);
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }
        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }
        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsulta.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (dgvConsulta.Columns[e.ColumnIndex].Name == "Alterar")
                {
                    LoadDetalhe(Convert.ToInt32(dgvConsulta.Rows[e.RowIndex].Cells["codigo"].Value));
                }
            }
        }
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Pesquisar();
        }

        private void LoadDetalhe(int id)
        {
            Cursor.Current = Cursors.WaitCursor;

            Resultado form = new Resultado(id);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            Cursor.Current = Cursors.Default;

            Pesquisar();
        }
        private void Pesquisar()
        {
            Cursor.Current = Cursors.WaitCursor;

            ResultadoBL blPaciente = new ResultadoBL();

            if (!String.IsNullOrEmpty(txtPesquisa.Text.Trim()))
                lista = blPaciente.ListarTodos().Where(x => x.sDescricao.ToUpper().Contains(txtPesquisa.Text.ToUpper().Trim())).OrderBy(x => x.sDescricao).ToList();
            else
                lista = blPaciente.ListarTodos().OrderBy(x => x.sDescricao).ToList();

            CarregarGrid();
            lblTotal.Text = lista.Count.ToString();

            Cursor.Current = Cursors.Default;
        }
        private void CarregarGrid()
        {
            dgvConsulta.ReadOnly = true;
            dgvConsulta.AllowUserToAddRows = false;

            gerarColunaDataGridView();

            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    dgvConsulta.Rows.Add("Alterar", lista[i].nIDResultado.ToString(), lista[i].sDescricao);
                }
            }
            else
                MessageBox.Show("Nenhum registro encontrado");
        }
        private void gerarColunaDataGridView()
        {
            dgvConsulta.Columns.Clear();

            DataGridViewButtonColumn columnAlterar = new DataGridViewButtonColumn();
            {
                columnAlterar.HeaderText = String.Empty;
                columnAlterar.Name = "Alterar";
                columnAlterar.Text = "Alterar";
                columnAlterar.Width = 65;
            }

            dgvConsulta.Columns.Insert(0, columnAlterar);
            dgvConsulta.Columns.Add("codigo", "codigo");
            dgvConsulta.Columns.Add("resultado", "Exame Micológico Direto");

            dgvConsulta.Columns[1].Visible = false; //codigo
            dgvConsulta.Columns[2].Width = 350; //resultado


            if ((from item in PermissaoUsuarioBL.ListaPermissao where item.Tela.sDescricao.Trim() == "Resultado" && item.sAlterar.Trim() == "N" select item).Count() > 0)
                dgvConsulta.Columns[0].Visible = false;
        }

        #region Singleton
        private static ResultadoConsulta form = null;
        public static ResultadoConsulta getInstance()
        {
            if (form == null)
                form = new ResultadoConsulta();

            return form;
        }
        #endregion
    }
}
