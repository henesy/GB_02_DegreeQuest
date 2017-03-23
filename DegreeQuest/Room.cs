using System;
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
        /* Populate actors by looping over members and getting X/Y values on location in Room */
        public volatile Actor[] members;
        public volatile int num;
        //colors for the different components
        public volatile Dictionary<string, Color> colors;

        public Room(Color backgdColor)
        {
            lock (this)
            {
                /** Handlers for given players/monsters should remove objects/edit objects ;; members should really only be read by other threads/functions/methods **/
                members = new Actor[200];
                num = 0;
                colors = new Dictionary<string, Color>();
                colors.Add("backgdColor", backgdColor);
            }
        }

        //constuctor to build a room with known actors
        public Room(Actor[] members, Color backgdColor) : this(backgdColor)
        {
            foreach(Actor a in members)//maybe need to change... 
            {
                Add(a);
            }
        }

        public void Add(Actor a)
        {
            lock (this)
            {
                if (num + 1 < 200)
                {
                    members[num] = a;
                    num++;
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
