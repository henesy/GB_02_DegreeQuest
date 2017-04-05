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
        public static Game1 game;
       
            
        [STAThread]
        static void Main()
        {
            NPCTemplate.update();
            ItemTemplate.update();

            using (game = new Game1())
            {
                
                    game.Run();
               
            }
        }
    }
}
