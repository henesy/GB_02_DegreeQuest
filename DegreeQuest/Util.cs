using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public static class Util
    {
        public static byte[] stb(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string bts(byte[] b)
        {
            return Encoding.ASCII.GetString(b);
        }

        public static void SerializePC(PC pc, Stream stream)
        {
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();

            // Serialize multiple objects into the stream
            formatter.Serialize(stream, pc);
        }

        /*
        private void DeSerializeMultipleObject(Stream stream)
        {
            // Construct a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize the stream into object
            Orders obOrders = (Orders)formatter.Deserialize(stream);
            Products obProducts = (Products)formatter.Deserialize(stream);
            Customers obCustomers = (Customer)formatter.Deserialize(stream);
        }
        */
    }
}
