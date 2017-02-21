using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace test_platformer
{
    public class SplashScreen : GameScreen
    {
        KeyboardState keystate;
        SpriteFont font;

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("Font1");
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Splash Screen", new Vector2(100,100), Color.Black);
        }
        public override void Update(GameTime gameTime)
        {
            keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Z))
                ScreenManager.Instance.AddScreen(new TitleScreen());

        }
    }
}
