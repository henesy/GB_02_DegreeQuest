using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DegreeQuest
{
    class DQClient
    {
        TcpClient c = new TcpClient();
        Vector2 pos;
        PC pc;

        public DQClient(PC mainPC)
        {
            pc = mainPC;
        }

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

            while (true)
            {
                byte[] inStream = new byte[100];
                serverStream.Read(inStream, 0, 100);
                //need to get PC Position
                //Vector2 pos = pc.Position;
                pos = (new Location(System.Text.Encoding.ASCII.GetString(inStream))).toVector2();
                pc.Position = pos;
                Thread.Sleep(100);
            }
        }
    }
}
