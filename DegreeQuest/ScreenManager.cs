using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


/*
 *  a singleton class that creates a instance of the manager and controlls switching
 *  between screens
 */

namespace DegreeQuest
{
    class ScreenManager
    {
        //screen manager instance
        private static ScreenManager instance;
        // Stack of the screens
        Stack<GameScreen> screenStack = new Stack<GameScreen>();
        Vector2 dimensions;
        GameScreen currentScreen;
        ContentManager content;

        public Vector2 Dimensions { get; set;}
        public void AddScreen(GameScreen screen)
        {
            screenStack.Push(screen);
            currentScreen.UnloadContent();
            currentScreen = screen;
            currentScreen.LoadContent(content);
        }
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }
        public void Initialize()
        {
            currentScreen = new StartScreen(); 
        }
        public void LoadContent(ContentManager c)
        {
            content = new ContentManager(c.ServiceProvider, "Content");
            currentScreen.LoadContent(content);
        }
        public void Update(GameTime gt)
        {
            currentScreen.Update(gt);
        }
        public void Draw(SpriteBatch sb)
        {
            currentScreen.Draw(sb);
        }
    }
}
