using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public class Subject
    {
        public static Subject Math;
        public static Subject Bio;
        public static Subject Chem;
        public static Subject Soc;
        public static Subject ComE;
        public static Subject ComSci;
        public static Subject Med;
        public static Subject Psy;
        public static Subject Econ;
        public static Subject ChemE;
        public static Subject None;



        public enum SUB { 
            MATH,            //LOGIC
            BIO,              //LIFE
            CHEM,            //CHEM
            SOC,            //SOCIAL
            COME,           //TECH

            COMSCI,           //LOGIC+TECH
            MED,             //LIFE+CHEM
            PSY,             //LIFE+SOCIAL
            ECON,            //LOGIC+SOCIAL
            CHEME,            //CHEM+TECH
            NONE = -1
        }



    }
}
