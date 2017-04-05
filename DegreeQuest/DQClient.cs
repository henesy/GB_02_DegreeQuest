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
        int spectatorPort;

        public DQClient(DegreeQuest mainDQ, Config conf)
        {
            dq = mainDQ;
            comSize = conf.getComSize();
            spectatorPort = Convert.ToInt32(conf.get("spectatorPort"));
        }

        public void ThreadRun()
        {

            try
            {
                c.Connect("127.0.0.1", spectatorPort);
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

                    string[] sub = locations[0].Split('#');
                    dq.room.num = int.Parse(sub[0]);
                    dq.room.num_item = int.Parse(sub[1]);
                    if (sub[2] != dq.room.id)
                        dq.switchRooms(sub[2]);

                    //need to expand
                    int j;
                    for(j = 0; j < dq.room.num && j < dq.room.members.Length; j++)
                    {
                        PC tc = new PC();
                        dq.LoadPC(tc, tc.Texture);
                        dq.room.members[j] = tc;
                    }

                    int i;
                    for (i = 0; i < dq.room.num && i < dq.room.members.Length; i++)
                    {
                        sub = locations[i+1].Split('#');
                        //Console.WriteLine(">>>SUB STRING: " + sub[0] + " then " + sub[1]);

                        dq.room.members[i].Position = new Location(sub[0]);
                        dq.room.members[i].Texture = sub[1];
                    }
                    for (i = 0; i< dq.room.num_item && i < dq.room.items.Length; i++)
                    {
                        dq.room.items[i] = new Item();
                        sub = locations[i+dq.room.num+1].Split('#');
                        //Console.WriteLine(">>>SUB STRING: " + sub[0] + " then " + sub[1]);

                        dq.room.items[i].Position = new Location(sub[0]);
                        dq.room.items[i].Texture = sub[1];
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
        int postPort;

        public DQPostClient(PC mainPC, DegreeQuest mainDQ, Config conf)
        {
            pc = mainPC;
            dq = mainDQ;
            postPort = Convert.ToInt32(conf.get("postPort"));
        }

        public void ThreadRun()
        {
            try
            {
                c.Connect("127.0.0.1", postPort);
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
