using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* Generic NPC class type */
    class NPC : Actor
    {
        Vector2 Position;

        // Current and maximum amount of hit points
        public uint HP, HPMax;

        // Current and maximum amount of energy (mana)
        public uint EP, EPMax;

        public int subject;

        public Texture2D Texture { get; private set; }
        
        //Current default constructor
        public NPC()
        {
            HP = HPMax = 100;
            EP = EPMax = 0;
            subject = Subject.NONE;
            Position = new Vector2(-1,-1);
        }

        public NPC(NPCTemplate temp)
        {
            HP = HPMax = (uint)temp.HP.roll();
            EP = EPMax = (uint)temp.EP.roll();
        }

        /* As per Actor */
        public AType GetAType()
        { return AType.NPC; }

        public Vector2 GetPos()
        { return Position; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }
    }
}
