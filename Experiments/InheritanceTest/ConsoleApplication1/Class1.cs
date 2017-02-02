using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace ConsoleApplication1
{
    
    class Class1
    {
        static void Main()
        {
            Test t;
            t.tesI = 1;
            t.tesD = 3.1415;
            t.tesS = "Hello";

            testObject to = new testObject(8, "test", "hi", t);

            Console.WriteLine("test: " + to.test);
            Console.WriteLine("str: " + to.str);
            Console.WriteLine("sd: " + to.sd);

            Console.WriteLine("TestI: " + to.t.tesI);
            Console.WriteLine("TestD: " + to.t.tesD);
            Console.WriteLine("TestS: " + to.t.tesS);

            testObject tb = new testObject(10, "cry", "bye", t);

            testObject added = to + tb;

            Console.WriteLine("test: " + added.test);
            Console.WriteLine("str: " + added.str);
            Console.WriteLine("sd: " + added.sd);

            Console.WriteLine("TestI: " + added.t.tesI);
            Console.WriteLine("TestD: " + added.t.tesD);
            Console.WriteLine("TestS: " + added.t.tesS);

            Console.Read();
        } 
    }
}
