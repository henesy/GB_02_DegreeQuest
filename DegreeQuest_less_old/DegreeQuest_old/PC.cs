using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* below are added for MonoGame purposes */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DegreeQuest
{
    /* Player clsas to manage to user */
    public class PC : Actor
    {
        // Animation representing the player
        public Texture2D PlayerTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        // Title/Name
        public string Name;

        // Get the width of the player ship
        public int Width       
        {
            get { return PlayerTexture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return PlayerTexture.Height; }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;

            //Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health (from tutorial, will move to separate class later or something)
            Health = 100;

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }

        /* As per Actor */
        public AType GetAType()
        { return AType.PC; }

        public Vector2 GetPos()
        { return Position; }
    }
}
