using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public class Dungeon
    {
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
            lock (currentRoom)
            {
                if (!Rooms.ContainsKey(roomId))
                    Rooms.Add(roomId, new Room(roomId));

                Rooms[currentRoom.id] = currentRoom; //shouldn't need

                //currentRoom.sortMembers();

                //Rooms[roomId].members = currentRoom.members;
                //Rooms[roomId].num = currentRoom.num;
                
                for (int i = 0; i < currentRoom.num; i++)
                {
                    if (currentRoom.members[i].GetAType() == AType.PC)
                    {
                        Rooms[roomId].Add(currentRoom.members[i]);
                        currentRoom.Remove(currentRoom.members[i]); 
                    }
                }

                currentRoom = Rooms[roomId];

            }
        }
    }
}
