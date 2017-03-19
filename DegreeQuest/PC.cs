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
        //public bool Active;

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

        public void Update()
        {

        }

        /* As per Actor */
        public override AType GetAType()
        { return AType.PC; }

        public override Vector2 GetPos()
        { return Position; }
    }
}
