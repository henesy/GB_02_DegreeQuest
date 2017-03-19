using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* below are added for MonoGame purposes */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace DegreeQuest
{
    /* Player class to manage to user */
    [Serializable()]

    /* Player class to manage to user */
    public class PC : Actor
    {

        //Base values and constants related to PCs
        public readonly uint PC_BASE_HP= 100;
        public readonly uint PC_BASE_EP = 100;
        public readonly uint PC_BASE_DEBT = 10000;
        public readonly uint[] PC_BASE_STATS = { 0, 0, 0, 0, 0 };

        // Animation representing the player
        //[NonSerialized] public Texture2D PlayerTexture;

        // Position of the Player relative to the upper left side of the screen
        // in Actor

        // State of the player
        public bool Active;

        // Current and maximum amount of hit points that player has
        public uint HP, HPMax;

        // Current and maximum amount of energy (mana) the player has
        public uint EP, EPMax;

        // Total and currently available amount of debt for player
        public uint Debt, DebtTotal;

        // Title/Name
        public string Name;

        //Players current stat levels
        public uint[] Stats;

        //Default Constructor
        public PC()
        {
            Position = new Vector2(-1, -1);
            Active = false;
            HP = HPMax = PC_BASE_HP;
            EP = EPMax = PC_BASE_EP;
            Stats = PC_BASE_STATS;
            Debt = DebtTotal = PC_BASE_DEBT;
            Name = "Paul Chaser";

        }

        // Get the width of the player ship
        public int Width       
        {
            get { return Texture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return Texture.Height; }
        }



        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;

            //Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health (from tutorial, will move to separate class later or something)
            HP = HPMax = PC_BASE_HP;

        }

        public void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }

        /* As per Actor */
        public override AType GetAType()
        { return AType.PC; }

        public override Vector2 GetPos()
        { return Position; }
    }
}
