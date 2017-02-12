using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DegreeQuest
{
    class DQServer
    {
        ClientList clients;

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
                Handler h = new Handler(client);

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
            DQServer srv = new DQServer();
            srv.DQSInit();
        }

        /* Post player locations to all clients */
        public void WriteAll(int x, int y)
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

        //threading and locks on clients variable 
        public Handler(TcpClient client)
        {
            c = client;
        }

        public void ThreadRun()
        {
            Console.WriteLine(">>> Handler Thread Started!");

            while (true)
            {
                try
                {
                    NetworkStream networkStream = c.GetStream();
                    Byte[] byt = System.Text.Encoding.ASCII.GetBytes("This is a test...");
                    networkStream.Write(byt, 0, byt.Length);
                    networkStream.Flush();
                    Console.WriteLine(">>> WROTE TEST!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
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
