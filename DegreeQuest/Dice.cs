using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{

    /// <summary> Dice is an object to represent rolling a certain number of dice with given side amount and modifier
    /// 
    /// </summary>
    public class Dice
    {
        private int mod, num, sides;

        public Dice(int mod,uint num, uint sides)
        {
            this.mod = mod;
            this.num = (int)num;
            this.sides = Math.Max((int)sides,1);
        }

        public Dice(int mod)
        {
            this.mod = mod;
            num = 0;
            sides = 1;
        }

        public int roll()
        {
            Random rand = new Random();
            int result = mod;
            for(int i = 0; i < num; i++)
            {
                result += rand.Next(1, sides);
            }
            return result;
        }




    }
}
