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
            MySql test = new MySql("tests");
            test.readT("'john'");
            Console.WriteLine("lol");
            /*
            using (var game = new DegreeQuest())
                game.Run();
            */
        }
    }
}
