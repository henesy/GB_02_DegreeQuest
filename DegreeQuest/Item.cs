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
        public static String name_DFLT = "thing";
        public static int type_DFLT = 0;
        public static int value_DFLT = 0;
        public static int effect_DFLT = 0;
        public static int rarity_DFLT = 0;
        public static int logic_DFLT = 0;
        public static int life_DFLT = 0;
        public static int chem_DFLT = 0;
        public static int tech_DFLT = 0;

        public String name;

        public int type, value, effect, rarity;
        
        public int[] stats;
        

        //Current default constructor
        public Item()
        {
            Texture = "DiamondSword";
            name = name_DFLT;
            type = value = effect = rarity = 0;
            stats = new int[Stat.NUM];
            for(int i=0;i<stats.Length;i++)
            {
                stats[i] = 0;
            }
        }

        public override AType GetAType() { return AType.Item; }

        public override Vector2 GetPos()
        { return Position.toVector2(); }
    }
}
