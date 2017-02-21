using Microsoft.Xna.Framework;
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

    /*
    *   Exists to provide a placeholder within which a player or a Monster can be placed without mutual exclusion 
    *   to implement you must use "MyClass : Actor" this is equivalent to the Java "MyClass implements Actor"
    */
    public abstract class Actor
    {
        //float Position { get; set; }

        public Vector2 Position;

        /* Provides "type" of object filling the interface */
        public abstract AType GetAType();

        public abstract  Vector2 GetPos();

        public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
