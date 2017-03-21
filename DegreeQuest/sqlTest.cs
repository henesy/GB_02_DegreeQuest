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
        public MySql()
        {
            
            string connStr = "server=mysql.cs.iastate.edu;user=dbu309gb2;database=db309gb2;port=3306;password=ODcxNjc3ZGI4";
            this.conn = new MySqlConnection(connStr);

        }

        public void readT(string city)
        {
            try
            {

                string read = "SELECT CustomerName FROM TestTable WHERE City=" + city;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = read;
                cmd.Connection = conn;
                conn.Open();

                MySqlDataReader mR = cmd.ExecuteReader();
                while (mR.Read())
                {
                    Console.WriteLine(mR.GetString(0));
                }
               

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
        public void write(string CustomerName, string ConatactName, string Adress, string City, string PostalCode,string Country)
        {
            try
            {
                string write = "INSERT INTO TestTable( CustomerName, ConatactName, Adress, City, PostalCode, Country) VALUES (@CustomerName, @ConatactName, @Adress, @City, @PostalCode, @Country);";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(write, conn);
             

                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@ConatactName", ConatactName);
                cmd.Parameters.AddWithValue("@Adress", Adress);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                cmd.Parameters.AddWithValue("@Country", Country);
               
                cmd.ExecuteNonQuery();
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
