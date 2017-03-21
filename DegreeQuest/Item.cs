using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    class Item
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

        public string name;

        public int type, value, effect, rarity;
        
        public int[] stats;
        

        //Current default constructor
        public Item()
        {
            type = value = effect = rarity = 0;
            stats = new int[Stat.NUM];
            for(int i=0;i<stats.Length;i++)
            {
                stats[i] = 0;
            }
        }
    }
}
