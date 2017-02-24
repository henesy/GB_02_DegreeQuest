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
        public MySql(string dbName)
        {
            string connStr = "server=localhost;user=root;database=" + dbName + ";port=3306;password=testdb";
            this.conn = new MySqlConnection(connStr);

        }
    
        public void createT(string tName, string columns)
        {
            try
            {
                string mkT = "CREATE TABLE " + tName + " (" + columns + ")";
                MySqlCommand cmd = new MySqlCommand(mkT, conn);
                conn.Open();
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void readT(string name)
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

        public void write(string bName, string name, DateTime birthdate)
        {
            try
            {
                string write = "INSERT INOT inf(name, boss, birthdate) VALUES (@name, @boss, @birthdate)";
                MySqlCommand cmd = new MySqlCommand(write, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@boss", bName);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
