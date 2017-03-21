using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* class to handle the configuration found in config.txt */
    public class Config
    {
        protected Dictionary<string,string> values;
        protected string root;

        public Config()
        {
            values = new Dictionary<string, string>();

            /* Through trial and error, puts you at the "root" project directory (with the .sln, etc.) */
            root = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..";

            //should probably check for existence/authenticity of file
            string[] config = System.IO.File.ReadAllLines(root + @"/config.txt");

            int i;
            for(i = 0; i < config.Length; i++)
            {
                // we assume a properly formatted file
                string[] tuple = config[i].Split('=');
                values.Add(tuple[0], tuple[1]);

                Console.Write("File had: " + tuple);
            }

        }

        /* one would have to close the program to truly refresh Config and avoid overwriting by the program */
        public void write()
        {
            var arr = values.ToArray();

            var file = new System.IO.StreamWriter(root + @"/config.txt");

            int i;
            for(i = 0; i < arr.Length; i++)
            {
                string str = "";
                str += arr[i].Key;
                str += "=";
                str += arr[i].Value;
                file.Write(str);
            }
        }

        public string get(string key)
        {
            return values[key];
        }

        public void set(string key, string val)
        {
            values[key] = val;
        }

        // risky
        public bool bget(string key)
        {
            return Convert.ToBoolean(values[key]);
        }
    }

    //Working on, meant to have more specific methods relating to game configs
    public class GameConfig : Config
    {
        public GameConfig() : base()
        {
            
        }

        /* arbitrary state checks for ease of use */
        public bool isServer()
        {
            if (this.values["isServer"] == "true")
                return true;
            else
                return false;
        }
    }
}
