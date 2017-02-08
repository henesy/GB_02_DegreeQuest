using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DegreeQuest.Server
{
    class DQServer
    {
        public static void DQSInit()
        {
            TcpListener srv = new TcpListener(1337);
            ClientList clients = new ClientList();


        }
    }

    class Handler
    {
        //threading and locks on clients variable 

    }

    /* New list type to handle locking/sychronization cleaner (internally) */
    class ClientList
    {
        private ArrayList clients;
        bool lockFlag = false;


        public ClientList()
        {
            this.clients = new ArrayList();
        }

        /* lock-able methods with try/catch (for pulse) */

        public Socket Get(int i)
        {
            Socket s = null;
            lock(this)
            {
                if(!lockFlag)
                {
                    try
                    {
                        // Waits for the Monitor.Pulse from somewhere else
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                    //do things here


                    lockFlag = false;    // Reset the state flag to say reading is done
                    Monitor.Pulse(this);   // Pulse tells we are done reading
                }
            }
            return s;
        }

        public void Add()
        {
            lock(this)
            {

            }
        }
    }
}
