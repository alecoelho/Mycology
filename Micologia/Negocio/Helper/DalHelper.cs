using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Micologia.Negocio
{
    public class DalHelper
    {
        // ----- Base Oficial -------
        SqlConnection Con = null;

        private string _strConexao;
        private static SqlTransaction _transacao;

        public void BeginTransaction()
        {
            Con = new SqlConnection(_strConexao);
            Con.Open();
            _transacao = Con.BeginTransaction();
        }
        public void Commit()
        {
            _transacao.Commit();
            Con.Close();
        }
        public void Rollback()
        {
            _transacao.Rollback();
            Con.Close();
        }

        public DalHelper()
        {
            //Desenvolvimento
            //_strConexao = string.Format(@"data source=RJO-TPO-DBDESENV01.corp.arcon.com.br;Initial Catalog=SiriusReport; Persist Security Info=True;User ID=sirius;Password=S15432##21;Pooling=true;Max Pool Size=1000");

            //Homologação
            _strConexao = string.Format(@"data source=mssql04.kinghost.net;initial catalog=alcaconsultoria;User ID=alcaconsultoria;Password=AlcaBD123456;;Pooling=true;Max Pool Size=1000");
 
        }

        public DataTable ObterDados(String sql)
        {
            SqlCommand Cmd = new SqlCommand();

            //Objeto de returno do método
            DataTable Retorno = new DataTable();

            //Objeto de conexão com o banco
            Con = new SqlConnection(_strConexao);

            //Preenche os parametros do command
            Cmd.CommandText = sql;
            Cmd.Connection = Con;
            Cmd.CommandTimeout = 300; // 120 minutes

            //Objeto adapter da consulta
            SqlDataAdapter Adp = new SqlDataAdapter(Cmd);

            //Objeto que vai receber os dados
            DataTable Dt = new DataTable();

            //Fill na tabela
            Adp.Fill(Dt);

            //Preenche o objeto de retorno
            Retorno = Dt;

            //Fecha a conexão ativa
            Con.Close();

            //Limpa o objeto da memória
            Dt.Dispose();

            return Retorno;
        }
 



        public bool ExecutarStoredProcedure(String Procedure)
        {
            SqlCommand Cmd = new SqlCommand();

            //Objeto de retorno
            Boolean Retorno = false;

            //Objeto de conexão com o banco
            Con = new SqlConnection(_strConexao);

            //Preenche os parametros do command
            Cmd.CommandText = Procedure;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Connection = Con;
            Cmd.CommandTimeout = 300; // 120 minutes

            //Abre a conexão com o banco
            if (Con.State != ConnectionState.Open)
                Con.Open();

            //Executa a SQL no banco
            Cmd.ExecuteNonQuery();

            //Fecha a conexão com o banco
            Con.Close();

            //Marca como true se não ocorrer
            Retorno = true;

            return Retorno;
        }
        public DataTable ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();

            // Open the connection 
            using (SqlConnection cnn = new SqlConnection(_strConexao))
            {
                cnn.Open();

                // Define the command 
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;

                    // Handle the parameters 
                    if (arrParam != null)
                    {
                        foreach (SqlParameter param in arrParam)
                            cmd.Parameters.Add(param);
                    }

                    // Define the data adapter and fill the dataset 
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

    }
}
