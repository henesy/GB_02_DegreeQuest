using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    struct Test
    {
        public int tesI;
        public double tesD;
        public String tesS;
    }
    class testObject
    {
        public Test t {get; set;}
        public int test { get; set; }
        public string str { get; set; }
        public string sd { get; set; }

        public testObject(int test, string str, string sd, Test t)
        {
            this.t = t;
            this.sd = sd;
            this.str = str;
            this.test = test;
        }
         

        public static testObject operator+ (testObject a, testObject b)
        {
           
            return new testObject(a.test + b.test, String.Concat(a.str, b.str), String.Concat(b.sd, a.sd), a.t);

        }

    }
}
