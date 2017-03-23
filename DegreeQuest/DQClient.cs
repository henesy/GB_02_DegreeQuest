using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DegreeQuest
{
    class DQClient
    {
        TcpClient c = new TcpClient();
        DegreeQuest dq;
        public volatile Boolean _halt = false;
        int comSize;

        public DQClient(DegreeQuest mainDQ, int comSiz)
        {
            dq = mainDQ;
            comSize = comSiz;
        }

        public void ThreadRun()
        {

            try
            {
                c.Connect("127.0.0.1", 13337);
            } catch(SocketException e)
            {
                Console.WriteLine("> Cannot connect to server on port :13337...ending Client...");
                _halt = true;
                return;
            }

            Console.WriteLine(">>> Client Connected!");
            NetworkStream serverStream = c.GetStream();

            if (c == null)
            {
                Console.WriteLine("CLIENT IS NULL!");
            }

            while (!_halt)
            {
                Byte[] byt2 = new Byte[comSize];

                try
                {
                    serverStream.Read(byt2, 0, comSize);
                } catch(System.IO.IOException e)
                {
                    Console.WriteLine(">>> Client failed to write to the server on port :13337...Client ending...");
                    _halt = true;
                    break;
                }

                
                string str = Util.bts(byt2);
                string[] locations = str.Split('@');

                //this is bad and unsafe and can cause crashes
                lock (dq.room) {

                    dq.room.num = locations.Length - 1;

                  
                    //need to expand
                    int j;
                    for(j = 0; j < dq.room.num && j < dq.room.members.Length; j++)
                    {
                        PC tc = new PC();
                        dq.LoadPC(tc, tc.Texture);
                        dq.room.members[j] = tc;
                    }

                    int i;
                    for (i = 0; i < dq.room.num && j < dq.room.members.Length; i++)
                    {
                        string[] sub = locations[i].Split('#');
                        //Console.WriteLine(">>>SUB STRING: " + sub[0] + " then " + sub[1]);

                        dq.room.members[i].Position = new Location(sub[0]);
                        dq.room.members[i].Texture = sub[1];
                    }
                }

                Thread.Sleep(5);
            }
            _halt = true;
        }

        public void Halt()
        {
            _halt = true;
        }
    }


    class DQPostClient
    {
        TcpClient c = new TcpClient();
        Vector2 pos;
        PC pc;
        DegreeQuest dq;
        public volatile Boolean _halt = false;

        public DQPostClient(PC mainPC, DegreeQuest mainDQ)
        {
            pc = mainPC;
            dq = mainDQ;
        }

        public void ThreadRun()
        {
            try
            {
                c.Connect("127.0.0.1", 13338);
            } catch(SocketException e)
            {
                Console.WriteLine("> Cannot connect to server on port :13337...ending POST Client...");
                _halt = true;
                return;
            }
            
            Console.WriteLine(">>> POST Client Connected!");
            NetworkStream srvStream = c.GetStream();

            if (c == null)
            {
                Console.WriteLine("POST CLIENT IS NULL!");
            }

            //var js = new JavaScriptSerializer();
            BinaryFormatter bin = new BinaryFormatter();

            while (!_halt)
            {
                try
                {
                    bin.Serialize(srvStream, pc);
                } catch(Exception e)
                {
                    Console.WriteLine(">>> Failed to serialize to server...ending POST Client...");
                    return;
                }

                srvStream.Flush();

                /* submit to server and read back ;; update relevant fields */

                //disabled due to performance issues
                //PC tc = (PC)bin.Deserialize(srvStream);
                //pc.Position = tc.Position;
                //pc.Texture = tc.Texture;

                //wrap up

                Thread.Sleep(5);
            }
            _halt = true;
        }

        public void Halt()
        {
            _halt = true;
        }
    }
}
