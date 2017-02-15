using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;


namespace DegreeQuest
{
    class MySql
    {
        MySqlConnection conn;

        string tbName { get; set; }

        public MySql(MySqlConnection conn)
        {
            this.conn = conn;
           
        }
        public MySql(String dbName)
        {
            string connStr = "server=localhost;user=root;database=" + dbName + ";port=3306;password=testdb";
            this.conn = new MySqlConnection(connStr);

        }
    
        public void createDB(String tName)
        {

        }

        public void readT(String name)
        {
            try
            {
             
                string read = "SELECT name FROM t1 WHERE boss="+name;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = read;
                cmd.Connection = conn;
                conn.Open();

                MySqlDataReader mR = cmd.ExecuteReader();
                while(mR.Read())
                {
                    Console.WriteLine(mR.GetString(0));
                }
                Console.Read();

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                Console.Read();
            }
            finally
            {
                conn.Close();
            }
        }

        public void write(String tName)
        {

        }

        public void testConn()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                Console.Read();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
