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
        static String myConnectionString = "server=10.25.71.66;uid=dbu309gb2;pwd=ODcxNjc3ZGI4;database=db309gb2;";
        static MySqlConnection conn = new MySqlConnection(myConnectionString);
        /*
         * current purpose is to test the connection by query the whole table and printing out everything in the table 
         */
        public static void queryAll(String tableName)
        {
            try
            {
                conn.Open();
                String sSQL = "SELECT * FROM " + tableName;
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
        
        /*
         * @param TableName is the name of the table in MySQL
         * @param colomnName is the name of the colomn that contains the primary key
         * @param key is the string value of the primary key that wants to be deleted
         * this function takes in the table name the colmn name and a unique key and finds the record 
         * associated with that key and deletes it.
         */
        public static void deleteRecordUsingKey(String tableName, String colomnName, String key)
        {
            try
            {
                conn.Open();
                String sSQL = "DELETE FROM " + tableName + "WHERE " + colomnName + "='" + key + "'";
                MySqlCommand cmd = new MySqlCommand(sSQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        /*
         * takes in the table name the name of the colomns that the values go into
         * and the values as parameters and creates a new record and inserts it into the table
         */
        public static void insertRecord(String tableName, String[] colmnNames, String[] values)
        {
            try
            {
                conn.Open();
                String sSQL = "insert into " + tableName + '(';
                for(int i = 0; i < colmnNames.Length; i++)
                {
                    if (i == 0)
                        sSQL = sSQL + colmnNames[i];
                    else
                        sSQL = sSQL + "," + colmnNames[i];
                }
                sSQL = sSQL + ") VALUES (";
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 0)
                        sSQL = sSQL + "'" + values[i] + "'";
                    else
                        sSQL = sSQL + ",'" + values[i] + "'";
                }
                sSQL = sSQL + ");";
                MySqlCommand cmd = new MySqlCommand(sSQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }
    }
}
