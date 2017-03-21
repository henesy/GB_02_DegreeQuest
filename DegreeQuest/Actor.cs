using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* For reflection of what's filling Actor, Item/Object/Static are placeholders for non-combatant elements of the world */
    [DataContract]
    public enum AType {PC, NPC, Item, Object, Static};

    /* For identification of the sprite to use, add more as needed. Note: Texture2D is the MonoGame texture identification ;; might not be necessary */
    //public enum Texture {Generic_PC, Generic_NPC};

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

    }
}
