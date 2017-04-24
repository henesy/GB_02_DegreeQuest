using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* For reflection of what's filling Actor, Item/Object/Static are placeholders for non-combatant elements of the world */
    [DataContract]
    public enum AType {Item, NPC, PC, Terrain, Static, Projectile}; //In Dispaly order bottom to top
    public enum PType {Arrow, Beam, Dot};

    /*
    *   Exists to provide a placeholder within which a player or a Monster can be placed without mutual exclusion 
    *   to implement you must use "MyClass : Actor" this is equivalent to the Java "MyClass implements Actor"
    */
    [Serializable()]
    public abstract class Actor
    {
        public bool Active;

        //float Position { get; set; }

        public string Texture { get; set; }

        //public Texture2D Texture { get; set; }

        public Location Position;

        public float MoveSpeed;

        /* Provides "type" of object filling the interface */
        public abstract AType GetAType();

        public abstract  Vector2 GetPos();

        public abstract int GetHeight();
        public abstract int GetWidth();

        //public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);

        public void Initialize(string texture, Vector2 position)
        {
            Texture = texture;

            //Set the starting position of the player around the middle of the screen and to the back
            Position = new Location(position);

            // Set the player to be active
            Active = true;

            // Stats would be set here...

        }

        //true if the actor's texture occupies the given loc
        public bool Occupying(Vector2 loc)
        {
            Vector2 pos = GetPos();
            float farX = pos.X + GetWidth();
            float farY = pos.Y + GetHeight();
            return Active && (loc.X >= pos.X && loc.X <= farX && loc.Y >= pos.Y && loc.Y <= farY);

        }

        //true if the actors currently overlap, false otherwise
        public bool Overlap(Actor a)
        {
            if(!(a.Active && Active)) { return false; }
            Vector2 diff = a.GetPos() - GetPos();
            int x = GetWidth();
            int y = GetHeight();
            if (diff.X < 0) { x = a.GetWidth(); diff.X =diff.X * -1; }
            if (diff.Y < 0) { y = a.GetHeight(); diff.Y = diff.Y * -1; }

            return (diff.X < x && diff.Y < y);
        }

    }

    public class ActorComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            if (x == null && y != null)
                return 1;

            if (y == null && x != null)
                return -1;

            if (y == null && x == null)
                return 0;

            AType xType = ((Actor)x).GetAType();
            AType yType = ((Actor)y).GetAType();

            if (xType == yType)
                return 0;

            if (xType == AType.PC) 
                return -1;
            if (yType == AType.PC)
                return 1;

            return 0;

        }
    }
}
