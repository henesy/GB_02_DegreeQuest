using System;

namespace DegreeQuest
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //TODO change properties back to Windows application

            //testDB();
            
            
            
            using (var game = new DegreeQuest())
                game.Run();
            
        }

        static void testDB()
        {
            MySql test = new MySql();
            Console.Write("Enter what county to read: ");
            string city = Console.ReadLine();
            test.readT(city);
            //Console.Read();
            string[] list = new string[6];
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Console.Write("CustomerName ");
                }
                else if (i == 1)
                {
                    Console.Write("ConatactName ");
                }
                else if (i == 2)
                {
                    Console.Write("Adress ");
                }
                else if (i == 3)
                {
                    Console.Write("City ");
                }
                else if (i == 4)
                {
                    Console.Write("PostalCode ");
                }
                else if (i == 5)
                {
                    Console.Write("County ");
                }

                list[i] = Console.ReadLine();
            }
            test.write(list[0], list[1], list[2], list[3], list[4], list[5]);
        }
    }
}
