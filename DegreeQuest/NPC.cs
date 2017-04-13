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
    [Serializable()]
    public class NPC : Actor
    {
        public static String name_DFLT = "charmander";
        public static int HP_DFLT = 100;
        public static int EP_DFLT = 0;
        public static int atk_DFLT = 0;
        public static int def_DFLT = 1;
        public static int spd_DFLT = 1;
        public static int lvl_DFLT = 1;
        public static int subject_DFLT = Subject.NONE;

        public string name;
        // Current and maximum amount of hit points
        public int HP, HPMax;

        // Current and maximum amount of energy (mana)
        public int EP, EPMax;

        public int atk, def, spd, lvl;

        public int subject;

        //public Texture2D Texture { get; private set; }
        
        //Current default constructor
        public NPC()
        {
            name = name_DFLT;
            HP = HPMax = HP_DFLT;
            EP = EPMax = EP_DFLT;
            atk = atk_DFLT;
            def = def_DFLT;
            spd = spd_DFLT;
            lvl = lvl_DFLT;
            subject = Subject.NONE;
            Position = new Location(new Vector2(-1,-1));
            Texture = name;
        }

        public NPC(NPCTemplate temp)
        {
            name = temp.name;
            HP = HPMax = temp.HP.roll();
            EP = EPMax = temp.EP.roll();
            atk = temp.atk.roll();
            def = temp.def.roll();
            spd = temp.spd.roll();
            subject = temp.subject();
        }

        /* As per Actor */
        public override AType GetAType()
        { return AType.NPC; }

        public override Vector2 GetPos()
        { return Position.toVector2(); }

    }
}
