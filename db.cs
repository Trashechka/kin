using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Кинотеатр
{
    public class db
    {
        static string serverName = @"DESKTOP-QAK3A45\SQLEXPRESS";
        static string dbName = "Кинотеатр";

        public SqlConnection con = new SqlConnection($@"Data Source={serverName}; Initial Catalog={dbName};Integrated Security = True");
    }
}
