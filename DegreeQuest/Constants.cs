using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public static class Constants
    {
        public static readonly int NUM_STAT = 5;
        public enum Stat
        {   STAT_LOGIC,
            STAT_LIFE,
            STAT_CHEM,
            STAT_SOC,
            STAT_TECH
        }

        public enum Subject
        {
            SUB_MATH,            //LOGIC
            SUB_BIOLOGY,         //LIFE
            SUB_CHEM,            //CHEM
            SUB_SOC,            //SOCIAL
            SUB_COME,           //TECH

            SUB_COMSCI,           //LOGIC+TECH
            SUB_MED,             //LIFE+CHEM
            SUB_PSY,             //LIFE+SOCIAL
            SUB_ECON,            //LOGIC+SOCIAL
            SUB_CHEME,            //CHEM+TECH
            SUB_NULL = -1
        }


    }
}
