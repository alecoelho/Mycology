using System;
using System.Collections.Generic;
using System.Configuration;

namespace Micologia.Negocio
{
    public class Util
    {
        public bool ExecutarSqlCrud(string query)
        {
            string _strConexao = ConfigurationManager.ConnectionStrings["MICOLOGIA"].ConnectionString; 

            System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand();

            //Objeto de conexão com o banco
            System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(_strConexao);

            //Preenche os parametros do command
            Cmd.CommandText = query;
            Cmd.Connection = Con;
            Cmd.CommandTimeout = 120 * 60; // 120 minutes

            //Abrir Conexão
            Con.Open();

            bool retorno = (Cmd.ExecuteNonQuery() > 0);

            //Fecha a conexão ativa
            Con.Close();

            return retorno;
        }
    }
}
