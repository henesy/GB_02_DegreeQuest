﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
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

        public DQClient(DegreeQuest mainDQ)
        {
            dq = mainDQ;
        }

        public void ThreadRun()
        {
            c.Connect("127.0.0.1", 13337);
            Console.WriteLine(">>> Client Connected!");
            NetworkStream serverStream = c.GetStream();

            if (c == null)
            {
                Console.WriteLine("CLIENT IS NULL!");
            }

            //Int32 size = c.ReceiveBufferSize;
            //Type[] knownTypes = new Type[] {typeof(Vector2), typeof(Actor), typeof(AType), typeof(List<PC>) };
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<PC>), knownTypes);
            //var ser = new JavaScriptSerializer();

            while (true)
            {
                Console.WriteLine(">>> Reading Room!");

                //right now only runs this once
                //dq.room.members = (List<PC>)ser.ReadObject(serverStream);
                Byte[] byt2 = new Byte[10000];
                serverStream.Read(byt2, 0, 10000);
                string json = DegreeQuest.bts(byt2);
                //List<string> vl = ser.Deserialize<List<string>>(json);
                string[] locations = json.Split('@');

                //populate rooms
                //List<Vector2> vl = new List<Vector2>();

                //this is bad and unsafe and can cause crashes
                lock (dq.room.members) {

                    /** this entire block should be replaced with a server-side ID that re-writes the members array once at start and then just uses server id's to write to the members array **/
                    dq.room.members = new List<Actor>();

                    int i;
                    for (i = 0; i < locations.Length - 1; i++)
                    {
                        PC lc = new PC();
                        dq.LoadPC(lc);
                        lc.Position = new Location(locations[i]).toVector2();
                        dq.room.members.Add(lc);
                    }
                }

                Thread.Sleep(5);
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
            Byte[] byt = DegreeQuest.stb("OPEN " + pc.Name);
            srvStream.Write(byt, 0, byt.Length);
            srvStream.Flush();

            byte[] initB = new byte[100];
            srvStream.Read(initB, 0, 100);
            pos = new Location(DegreeQuest.bts(initB)).toVector2();

            pc.Position = pos;

            Console.WriteLine(">>> POST Client Entering Primary Loop!");

            while (true)
            {
                byte[] inStream = new byte[100];
                Byte[] byt2;

                //the problem is last action
                string la = "nil";

                if (dq.actions.ToArray().Length > 0)
                {
                    la = (string)dq.actions.Dequeue();
                }

                //Console.WriteLine(">>> Processing action: " + la);

                if(la.Contains("MOVE"))
                {
                    byt2 = DegreeQuest.stb(la);
                    srvStream.Write(byt2, 0, byt2.Length);
                    srvStream.Flush();
                    //srvStream.Read(inStream, 0, 100);
                    //pos = new Location(DegreeQuest.bts(inStream)).toVector2();
                }
                else
                {
                    byt2 = DegreeQuest.stb(la);
                    srvStream.Write(byt2, 0, byt2.Length);
                    srvStream.Flush();
                }

                //wrap up

                //pc.Position = pos;
                Thread.Sleep(5);
            }
        }
    }
}
