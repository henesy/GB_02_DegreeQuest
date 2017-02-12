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
}
