using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using Microsoft.Xna.Framework;

namespace DegreeQuest
{
    class DQServer
    {
        ClientList clients;
        PC pc;

        public DQServer(PC mainPC)
        {
            pc = mainPC;
        }

        public void DQSInit()
        {
            TcpListener srv = new TcpListener(13337);
            clients = new ClientList();

            srv.Start();
            Console.WriteLine(">>> Server Started");

            while(true)
            {
                TcpClient client = srv.AcceptTcpClient();
                clients.Add(client);
                Handler h = new Handler(client, pc);

                //handle concurrently 
                Thread handler = new Thread(new ThreadStart(h.ThreadRun));

                try
                {
                    handler.Start();
                    //handler.Join();
                }
                catch (ThreadStateException e)
                {
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine(">>> DQSInit Ending!");
        }

        public void ThreadRun()
        {
            this.DQSInit();
        }

        /* Post player locations to all clients */
        public void WriteAll(Vector2 pos)
        {
            int i;
            for(i = 0; i < clients.Length(); i++)
            {
                TcpClient c = clients.Get(i);

                //post location
            }
        }

    }

    
    class Handler
    {
        TcpClient c;
        PC pc;

        //threading and locks on clients variable 
        public Handler(TcpClient client, PC mainPC)
        {
            c = client;
            pc = mainPC;
        }

        public void ThreadRun()
        {
            Console.WriteLine(">>> Handler Thread Started!");

            while (true)
            {
                try
                {
                    NetworkStream networkStream = c.GetStream();
                    //needs to be PC position
                    Vector2 position = pc.Position;
                    string str = (new Location(position).ToString());
                    Byte[] byt = Encoding.ASCII.GetBytes(str);
                    networkStream.Write(byt, 0, byt.Length);
                    networkStream.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }

                Thread.Sleep(100);
            }

            Console.WriteLine(">>> Handler Ending! ");
        }
    }


    /* New list type to handle locking/sychronization cleaner (internally) */
    class ClientList
    {
        private ArrayList clients;


        public ClientList()
        {
            this.clients = new ArrayList();
        }

        /* lock-able methods with try/catch (for pulse) */

        public TcpClient Get(int i)
        {
            TcpClient s = default(TcpClient);


            lock(this)
            {
                //do things here
                s = ((TcpClient) clients.ToArray()[i]);

                Monitor.Pulse(this);   // Pulse tells we are done reading
            }

            return s;
        }

        public int Add(TcpClient s)
        {
            int size = this.Length();

            lock(this)
            {
                clients.Add(s);

                Monitor.Pulse(this);
            }

            return size;
        }

        public int Length()
        {
            return (clients.ToArray().Length);
        }
    }


    class DQPostSrv
    {
        ClientList clients;
        DegreeQuest srvDQ;

        public DQPostSrv(DegreeQuest hostDQ)
        {
            srvDQ = hostDQ;
        }

        public void PostInit()
        {
            TcpListener srv = new TcpListener(13338);
            clients = new ClientList();

            srv.Start();
            Console.WriteLine(">>> POST Server Started");

            while (true)
            {
                TcpClient client = srv.AcceptTcpClient();
                clients.Add(client);
                PostHandler h = new PostHandler(client, srvDQ);

                //handle concurrently 
                Thread handler = new Thread(new ThreadStart(h.ThreadRun));

                try
                {
                    handler.Start();
                    //handler.Join();
                }
                catch (ThreadStateException e)
                {
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine(">>> DQSInit Ending!");
        }

        public void ThreadRun()
        {
            this.PostInit();
        }
    }

    /* Manages communications with a client on port :13338 for movement/deltas and changes and such */
    class PostHandler
    {
        TcpClient c;
        PC cc; //client character
        DegreeQuest srvDQ;
        Int32 id;

        public PostHandler(TcpClient client, DegreeQuest hostDQ)
        {
            c = client;
            cc = null;
            srvDQ = hostDQ;
        }

        public void ThreadRun()
        {
            Console.WriteLine(">>> POST Handler Thread Started!");

            NetworkStream cStream = c.GetStream();
            byte[] inStream = new byte[100];

            cStream.Read(inStream, 0, 100);
            string nameMsg = DegreeQuest.bts(inStream);
            cc.Name = nameMsg.Substring(5);

            //establish locations/init client "player" object
            cc = new PC();
            id = srvDQ.room.members.Add(cc);

            srvDQ.LoadPC(cc);

            cStream.Write(DegreeQuest.stb(new Location(cc.Position).ToString()), 0, 100);
            cStream.Flush();

            while (true)
            {
                try
                {

                    //do things here
                    //katie was here
                    cStream.Read(inStream, 0, 100);
                    string usrin = DegreeQuest.bts(inStream);

                    if(usrin.Contains("MOVE"))
                    {
                        ((PC)srvDQ.room.members.ToArray()[id]).Position = new Location(usrin.Substring(5)).toVector2();
                        //checks would occur here to see if there is a valid move

                        cStream.Write(DegreeQuest.stb((new Location(((PC)srvDQ.room.members.ToArray()[id]).Position)).ToString()), 0, 100);
                    }
                    

                    cStream.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }

                Thread.Sleep(100);
            }

            Console.WriteLine(">>> POST Handler Ending! ");
        }
    }
}
