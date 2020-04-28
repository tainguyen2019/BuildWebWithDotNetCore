using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BuildWebWithDotNetCore.Models.Home
{
    public class HomeModel
    {
        public List<Account> Test()
        {
            MySqlConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "select email, password from account";
            MySqlCommand query = new MySqlCommand();
            query.CommandText = sql;
            query.Connection = conn;
            MySqlDataReader reader = query.ExecuteReader();
            List<Account> data = new List<Account>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Add(new Account(reader.GetString(0), reader.GetString(1)));
                }
            }
            
            return data;
        }
    }
}
