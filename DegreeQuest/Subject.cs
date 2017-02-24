using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    public class Subject
    {
        public static String[] Subjects = { "NONE", "MATH", "BIO","CHEM","SOC","COMPE","COMSCI","MED","PSY","ECON","CHEME"};
        public static int MATH = 1;
        public static int BIO = 2;
        public static int CHEM = 3;
        public static int SOC = 4;
        public static int COMPE = 5;
        public static int COMSCI = 6;
        public static int MED = 7;
        public static int PSY = 8;
        public static int ECON = 9;
        public static int CHEME = 10;
        public static int NONE = 0;

        //Creates a BitVector to represent a list of subjects given by list
        public static BitVector32 listToVect(String list)
        {
            char[] delims = { ' ', ',' };
            BitVector32 vect = new BitVector32(1);

            String[] subs = list.Split(delims);
            for (int i=0;i<subs.Length;i++)
            {
                int s = Array.IndexOf(Subjects, subs[i]);
                if (s > 0)
                {
                    vect[s] = true;
                }
            }
            return vect;
        }


    }
}
