using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BuildWebWithDotNetCore.Models
{
    public class Database
    {
        public static MySqlConnection GetConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "website";
            string username = "root";
            string password = "";


            string connString = "Server=" + host + ";Database=" + database
                    + ";port=" + port + ";User Id=" + username + ";password=" + password;
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}

