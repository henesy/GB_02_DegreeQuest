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
            DBReader.updateRow("player_loc", new String[] { "pos_x" }, new String[] { "23" }, "idplayer_loc", "1");
            NPCTemplate.update();
            ItemTemplate.update();
            using (var game = new DegreeQuest())
                game.Run();
        }
    }
}
