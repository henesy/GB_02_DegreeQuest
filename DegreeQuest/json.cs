using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace DegreeQuest
{
    /* abstraction class for: byte[] -> object ;; object -> byte[] */
    public class json : System.Object
    {
        /* Converts an arbitrary object to a JSON string */
        public static string Encode(Object o)
        {
            string str;
            JavaScriptSerializer s = new JavaScriptSerializer();
            str = s.Serialize(o);
            return str;
        }

        /* Converts a JSON string to an Object, requires cast and knowledge of type to cast to */
        public static Object Decode<T>(string str)
        {
            Object o;
            JavaScriptSerializer s = new JavaScriptSerializer();
            o = (Object) s.Deserialize<string>(str);
            return o;
        }
    }
}
