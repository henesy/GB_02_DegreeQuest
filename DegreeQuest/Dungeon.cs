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
        public volatile Dictionary<string, Room> Rooms; 
        public volatile Room currentRoom;

        public Dungeon(PC pc)
        {
            lock (this)
            {
                Rooms = new Dictionary<string, Room>();

                Room room = new Room("default");
                room.Add(pc);

                Rooms.Add("default", room);

                currentRoom = Rooms["default"];
            }
        }

        public void AddRoom(string roomName)
        {
            lock (Rooms)
            {
                Room room = new Room(roomName);
                Rooms.Add(roomName, room);
            }
        }

        public void switchRooms(string roomId)
        {
            lock (this)
            {
                if (!Rooms.ContainsKey(roomId))
                    Rooms.Add(roomId, new Room(roomId));

                Rooms[currentRoom.id] = currentRoom.copy(); 

                currentRoom.sortMembers();
                for (int i = 0; i < currentRoom.num; i++)
                {
                    if (currentRoom.members[i].GetAType() == AType.PC)
                    {
                        Actor pc = currentRoom.members[i];
                        Rooms[roomId].Add(pc);
                        Rooms[currentRoom.id].Delete(pc);
                        pc.Active = true;
                    }
                    else
                    {
                        break;
                    }
                }
                currentRoom = Rooms[roomId].copy();    
            }
        }
    }
}
