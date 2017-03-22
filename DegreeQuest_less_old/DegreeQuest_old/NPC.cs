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

        public Texture2D PlayerTexture { get; private set; }

        /* As per Actor */
        public AType GetAType()
        { return AType.NPC; }

        public Vector2 GetPos()
        { return Position; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }
    }
}
