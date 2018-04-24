using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal static class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            // create a database connection object

            var connString = @"Data Source=localhost;Initial Catalog=gameDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
