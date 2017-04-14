﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;


namespace DegreeQuest
{
    public class Room
    {
        public static int ITEM_MAX = 256;
        /* Populate actors by looping over members and getting X/Y values on location in Room */
        public volatile Item[] items;
        public volatile int num_item;
        public volatile Actor[] members;
        public volatile int num;
        public volatile string id;

        public Room(string room_id)
        {
            lock (this)
            {
                /** Handlers for given players/monsters should remove objects/edit objects ;; members should really only be read by other threads/functions/methods **/
                members = new Actor[200];
                items = new Item[ITEM_MAX];
                num = num_item = 0;
                id = room_id;
            }
        }

        public void sortMembers()
        {
            lock (this)
            {
                IComparer comp = new ActorComparer();
                Array.Sort(members, 0, num, comp);

                string n = "";
                for (int i = 0; i < num; i++)
                {
                    if (members[i].GetAType() == AType.PC)
                    {
                        n += "pc ";
                        if (members[i].Active == true)
                            n += "true, ";
                        else
                            n += "false, ";
                    }
                    else
                        n += "idk ,";
                }
                Console.WriteLine(n);
            }
        }

        public Room copy()
        {
            Room room = new Room(id);
            for(int i = 0; i < num_item; i++)
            {
                room.Add(items[i]);
            }
            for(int i = 0; i < num; i++)
            {
                room.Add(members[i]);
            }
            return room;
        }

        public void Add(Actor a)
        {
            lock (this)
            {
                if (a.GetAType() == AType.Item)
                {
                    if (num_item + 1 < ITEM_MAX)
                    {
                        items[num_item] = (Item)a;
                        num_item++;
                    }

                }
                else
                {
                    if (num + 1 < 200)
                    {
                        members[num] = a;
                        num++;
                        sortMembers();
                    }
                }
            }
        }


        public void Delete(Actor a)
        {
            lock (this)
            {
                a.Active = false;
                ClientComparator comp = new ClientComparator();
                Array.Sort(members, 0, this.num, comp);
                num--;
            }
        }

        public void Remove(Actor a)
        {
            lock (this)
            {
                a.Active = false;
                ClientComparator comp = new ClientComparator();
                Array.Sort(members, 0, this.num, comp);
                a.Active = true;
                num--;
            }
        }

        

    }

    class ClientComparator : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y != null)
                return 1;

            if (y == null && x != null)
                return -1;

            if (y == null && x == null)
                return 0;

            if( ((Actor)x).Active && !((Actor)y).Active )
            {
                return -1;
            }

            if (!((Actor)x).Active && ((Actor)y).Active)
            {
                return 1;
            }
            
            return 0;
           
        }
    }
}
