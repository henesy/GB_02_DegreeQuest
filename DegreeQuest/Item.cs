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

    [Serializable()]
    public class Item : Actor
    {

        public static readonly int TEXTURE_WIDTH = 64;
        public static readonly int TEXTURE_HEIGHT = 64;

        public enum IType { Head, Torso, Feet, Hands, OneHand, OffHand, TwoHand, UseTemp, UsePerm, NONE}
        public static int equip_slots = (int)IType.TwoHand+1;

        public static String name_DFLT = "thing";
        public static int type_DFLT = (int)IType.NONE;
        public static int atk_DFLT = 0;
        public static int def_DFLT = 0;
        public static int spd_DFLT = 0;
        public static int HP_DFLT = 0;
        public static int EP_DFLT = 0;
        public static int value_DFLT = 0;
        public static int rarity_DFLT = 0;
        public static int logic_DFLT = 0;
        public static int life_DFLT = 0;
        public static int chem_DFLT = 0;
        public static int tech_DFLT = 0;

        public String name;

        public IType type;
        public int value, rarity;
        public int atk, def, spd;

        public int[] req;
        

        //Current default constructor
        public Item()
        {
            //change
            Texture = "DiamondSword";
            name = name_DFLT;
            value = rarity = 0;
            atk = def = spd = 0;
            type = IType.NONE;
            req = new int[Stat.NUM];
            for(int i=0;i<req.Length;i++)
            {
                req[i] = 0;
            }
        }

        public override AType GetAType() { return AType.Item; }

        public override Vector2 GetPos()
        { return Position.toVector2(); }

        public Boolean isEquip()
        {
            return type < IType.TwoHand;
        }

        public override int GetWidth()
        { return TEXTURE_WIDTH; }

        public override int GetHeight()
        { return TEXTURE_HEIGHT; }


    }
}
