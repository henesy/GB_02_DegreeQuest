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

        public static readonly int TEXTURE_WIDTH = 64;
        public static readonly int TEXTURE_HEIGHT = 64;


        public static String name_DFLT = "charmander";
        public static int HP_DFLT = 100;
        public static int EP_DFLT = 0;
        public static int atk_DFLT = 0;
        public static int def_DFLT = 1;
        public static int spd_DFLT = 2;
        public static int lvl_DFLT = 1;
        public static int subject_DFLT = Subject.NONE;

        public string name;
        // Current and maximum amount of hit points
        public int HP, HPMax;

        // Current and maximum amount of energy (mana)
        public int EP, EPMax;

        public int atk, def, lvl;

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
            MoveSpeed = spd_DFLT;
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
            MoveSpeed = temp.spd.roll();
            subject = temp.subject();
        }


        public static NPC Random()
        {
            return new NPC(NPCTemplate.Random());
        }

        /* As per Actor */
        public override AType GetAType()
        { return AType.NPC; }

        public override Vector2 GetPos()
        { return Position.toVector2(); }

        public override int GetWidth()
        {  return TEXTURE_WIDTH;   }

        public override int GetHeight()
        {return TEXTURE_HEIGHT;}


        public bool Move(Room r)
        {
            Actor target = r.NearestPC(GetPos());
            Vector2 path = target.GetPos() - GetPos();
            path.Normalize();
            if (TryMove(r, GetPos() + path * MoveSpeed)) { return true; }
            if (path.X != 0) {
                if (TryMove(r, GetPos() + new Vector2(path.X / Math.Abs(path.X), 0) * MoveSpeed)) { return true; }
            }
            if (path.Y != 0)
            {
                if (TryMove(r, GetPos() + new Vector2(0, path.Y / Math.Abs(path.Y)) * MoveSpeed)) { return true; }
            }
            return false;
        }

        public bool TryMove(Room r, Vector2 dest)
        {
            Location orig = Position;
            Position = new Location(dest);
            for (int i = 0; i < r.num; i++)
            {
                if (Overlap(r.members[i]))
                {
                    Position = orig;
                    return false;
                }
            }
            return true;
        }

    }
}
