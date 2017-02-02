using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace test_platformer
{
    public class ScreenManager
    {
        //screenmanager instance
        private static ScreenManager instance;
        // Stack of every screen
        Stack<GameScreen> screenStack = new Stack<GameScreen>();
        Vector2 dimensions;
        GameScreen currentScreen;
        GameScreen newScreen;
        ContentManager content;
        bool transition;
        FadeAnimation fade;
        Texture2D fadeTexture;

        public Vector2 Dimensions { get; set; }
        public void AddScreen(GameScreen screen)
        {
            newScreen = screen;
            //transition = true;
            //fade.IsActive = true;
            //fade.Alpha = 0.0f;
            //fade.ActivateValue = 1.0f;

            screenStack.Push(newScreen);
            currentScreen.UnloadContent();
            currentScreen = newScreen;
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
            fade = new FadeAnimation();
            currentScreen = new SplashScreen();
        }
        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(content);
            //fadeTexture = content.Load<Texture2D>("fade");
            //fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            //fade.Scale = dimensions.X;
        }
        public void Update(GameTime gametime)
        {
                currentScreen.Update(gametime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            currentScreen.Draw(spritebatch);
        }

        private void Transitions(GameTime gametime)
        {
            fade.Update(gametime);
            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content);
            }
            else if(fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }
    }
}
