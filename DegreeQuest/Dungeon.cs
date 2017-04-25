using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public enum Direction { None, North, South, East, West}
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
                Rooms = new Room[256, 256];

                Room room = new Room();
                room.Add(pc);

                index_x = index_y = 128;

                Rooms[index_x, index_y] = room;
                currentRoom = room.copy();
            }
        }

        public void AddRoom(int x, int y)
        {
            if (x >= 256 || y >= 256 || x < 0 || y < 0)
                return;

            lock (Rooms)
            {
                Rooms[x, y] = new Room();
            }
        }

        public void checkRoomSwitch()
        {
            currentRoom.sortMembers();
            
            Direction direction = checkDirection(currentRoom.members[0]);
            //Console.WriteLine(direction + "X " + currentRoom.members[0].Position.X + "; Y " + currentRoom.members[0].Position.Y);
            if (direction == Direction.None)
                return;

            for(int i = 1; i < currentRoom.num; i++)
            {
                if(currentRoom.members[i].GetAType() == AType.PC)
                {
                    if (checkDirection(currentRoom.members[i]) != direction)
                        return;
                }
            }

            switchRooms(direction);
        }

        private Direction checkDirection(Actor pc)
        {
            if (pc.GetAType() == AType.PC)
            {
                if (pc.Position.X <= 161)
                    return Direction.West;
                else if (pc.Position.X >= 1375)
                    return Direction.East;
                else if (pc.Position.Y <= 91)
                    return Direction.North;
                else if (pc.Position.Y >= 745)
                    return Direction.South;
            }
            return Direction.None;
        }

        private void updatePCPos(Direction d)
        {
            lock (currentRoom)
            {
                Console.WriteLine("Here " + d);
                if (d == Direction.North)
                {
                    Console.WriteLine("North");
                    for (int i = 0; i < currentRoom.num; i++)
                    {
                        currentRoom.members[i].Position.Y = 741;
                        Console.WriteLine(i + " " + currentRoom.members[i].Position.Y);
                    }
                }
                else if (d == Direction.South)
                {
                    for (int i = 0; i < currentRoom.num; i++)
                        currentRoom.members[i].Position.Y = 95;
                }
                else if (d == Direction.East)
                {
                    for (int i = 0; i < currentRoom.num; i++)
                        currentRoom.members[i].Position.X = 165;
                }
                else if (d == Direction.West)
                {
                    for (int i = 0; i < currentRoom.num; i++)
                        currentRoom.members[i].Position.X = 1371;
                }
            }
            
        }

        public void switchRooms(Direction d)
        {
            int x = index_x;
            int y = index_y;

            if (d == Direction.North)
                y++;
            else if (d == Direction.South)
                y--;
            else if (d == Direction.East)
                x++;
            else if (d == Direction.West)
                x--;
            else
                return;

            switchRooms(x, y);

            updatePCPos(d);
        }

        public void switchRooms(int x, int y)
        {
            lock (this)
            {
               
                if (Rooms[x, y] == null)
                    AddRoom(x, y);

                Rooms[index_x, index_y] = currentRoom.copy();
                //Rooms[index_x, index_y] = currentRoom;
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
                //currentRoom = Rooms[x, y];
                index_x = x;
                index_y = y;

            }
        }
    }
}
