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
                Console.WriteLine("Reading location! ");
                pos = (new Location(System.Text.Encoding.ASCII.GetString(inStream))).toVector2();
                pc.Position = pos;
                Thread.Sleep(100);
            }
        }
    }


    class DQPostClient
    {
        TcpClient c = new TcpClient();
        Vector2 pos;
        PC pc;
        DegreeQuest dq;

        public DQPostClient(PC mainPC, DegreeQuest mainDQ)
        {
            pc = mainPC;
            dq = mainDQ;
        }

        public void ThreadRun()
        {
            c.Connect("127.0.0.1", 13338);
            Console.WriteLine(">>> POST Client Connected!");
            NetworkStream srvStream = c.GetStream();

            if (c == null)
            {
                Console.WriteLine("POST CLIENT IS NULL!");
            }


            //initial position
            Byte[] byt = Util.stb("OPEN " + pc.Name);
            srvStream.Write(byt, 0, byt.Length);
            srvStream.Flush();

            byte[] initB = new byte[100];
            srvStream.Read(initB, 0, 100);
            pos = new Location(Util.bts(initB)).toVector2();

            pc.Position = pos;

            Console.WriteLine(">>> POST Client Entering Primary Loop!");

            while (true)
            {
                byte[] inStream = new byte[100];
                Byte[] byt2;

                //the problem is last action
                string la = "nil";

                if(dq.actions.ToArray().Length > 0)
                {
                    la = (string)dq.actions.Dequeue();
                }

                Console.WriteLine(">>> Processing action: " + la);

                switch (la)
                {
                    case "MOVE":
                        byt2 = Util.stb("MOVE " + new Location(pc.Position).ToString());
                        srvStream.Write(byt2, 0, byt2.Length);
                        srvStream.Flush();
                        //srvStream.Read(inStream, 0, 100);
                        //pos = new Location(DegreeQuest.bts(inStream)).toVector2();
                        break;
                    default:
                        byt2 = Util.stb(la);
                        srvStream.Write(byt2, 0, byt2.Length);
                        srvStream.Flush();
                        break;
                }

                //wrap up
                
                //pc.Position = pos;
                Thread.Sleep(100);
            }
        }
    }
}
