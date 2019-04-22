using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4.Helpers
{
    public static class SqlConnectionHelper
    {
        public static string SqlConnectionString =  @"Data Source=DESKTOP-E8GUR0D;Initial Catalog=Магазин2;Integrated Security=True";

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            return connection;

        }

        public static void ExecuteButtonHandle(string query, string tableName, DataSet ds, SqlConnection connect)
        {

            SqlCommand command = new SqlCommand(query, connect);
            command.CommandTimeout = 30;
            SqlDataAdapter sotrDA = new SqlDataAdapter();
            sotrDA.SelectCommand = command;
            ds.Clear();
            sotrDA.Fill(ds, tableName);
        }
    }
}
