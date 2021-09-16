using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Barkacs
{
    public class Database
    {
        public static MySqlConnection kapcsolat;
        public static MySqlCommand parancs;
        public static MySqlDataReader eredmeny;
        public static MySqlDataAdapter adapter;

        public static void kapcsol()
        { 
            string connStr = "server=localhost; database=barkacsbolt;" + 
                             "uid=root; character set=utf8";
            kapcsolat = new MySqlConnection(connStr);
            kapcsolat.Open();
            parancs = kapcsolat.CreateCommand();
        }

        public static void kapcsolatBont()
        {
            kapcsolat.Close();
        }
    }
}
