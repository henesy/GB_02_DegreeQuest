using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DegreeQuest
{
    /* Database communication abstraction */
    class DBReader
    {
        MySqlConnection conn;
        string myConnectionString = "server=10.25.71.66;uid=dbu309gb2;pwd=ODcxNjc3ZGI4;database=db309gb2;";

        /*
         * current purpose is to test the connection by query the whole table and printing out everything in the table 
         */
        public void queryAll(String tableName)
        {
            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
                string sSQL = "SELECT * FROM " + tableName;
                MySqlCommand cmd = new MySqlCommand(sSQL, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                Console.WriteLine(rdr.ToString());
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }
        
           
        
    }
}
