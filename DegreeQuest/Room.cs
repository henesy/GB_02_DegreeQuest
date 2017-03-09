using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DegreeQuest
{
    public class Room
    {
        /* Populate actors by looping over members and getting X/Y values on location in Room */
        public Actor[] members;
        public int num;

        public Room()
        {
            lock (this)
            {
                /** Handlers for given players/monsters should remove objects/edit objects ;; members should really only be read by other threads/functions/methods **/
                members = new Actor[200];
                num = 0;
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

    }
}
