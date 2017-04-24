using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public class Dungeon
    {
        //possibly change to different datatype like a 2d array or have a vector2 as key
        public volatile Room[,] Rooms;
        public volatile int index_x;
        public volatile int index_y;
        public volatile Room currentRoom;

        public Dungeon(PC pc)
        {
            lock (this)
            {
                Rooms = new Room[50, 50];

                Room room = new Room();
                room.Add(pc);

                index_x = index_y = 25;

                Rooms[index_x, index_y] = room;
                currentRoom = room.copy();
            }
        }

        public void AddRoom(int x, int y)
        {
            if (x >= 50 || y >= 50 || x < 0 || y < 0)
                return;

            lock (Rooms)
            {
                Rooms[x, y] = new Room();
            }
        }

        public void switchRooms(int x, int y)
        {
            if (x >= 50 || y >= 50 || x < 0 || y < 0)
                return;
            lock (this)
            {
                if (Rooms[x, y] == null)
                    AddRoom(x, y);

                Rooms[index_x, index_y] = currentRoom.copy(); 

                currentRoom.sortMembers();
                for (int i = 0; i < currentRoom.num; i++)
                {
                    if (currentRoom.members[i].GetAType() == AType.PC)
                    {
                        Actor pc = currentRoom.members[i];
                        Rooms[x, y].Add(pc);
                        Rooms[index_x, index_y].Delete(pc);
                        pc.Active = true;
                    }
                    else
                    {
                        break;
                    }
                }
                currentRoom = Rooms[x, y].copy();
                index_x = x;
                index_y = y;
            }
        }
    }
}
