using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    class DQClient
    {
        TcpClient c = new TcpClient();

        public void ThreadRun()
        {
            c.Connect("127.0.0.1", 13337);
            Console.WriteLine(">>> Client Connected!");
            NetworkStream serverStream = c.GetStream();

            if(c == null)
            {
                Console.WriteLine("CLIENT IS NULL!");
            }

            //Int32 size = c.ReceiveBufferSize;

            byte[] inStream = new byte[100];
            serverStream.Read(inStream, 0, 100);
            string str = System.Text.Encoding.ASCII.GetString(inStream);
            Console.WriteLine(str);

        }
    }
}
