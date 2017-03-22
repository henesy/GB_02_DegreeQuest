using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DegreeQuest
{
    class StartScreen : GameScreen
    {
        MouseState mousestate;
        MouseState prevmousestate;
        private Texture2D startButton;
        private Texture2D joinButton;

        public override void LoadContent(ContentManager c)
        {
            base.LoadContent(c);
            startButton = content.Load<Texture2D>("hostButton");
            joinButton = content.Load<Texture2D>("joinButton");
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Draw(SpriteBatch sb)
        {
            Vector2 startloc = new Vector2(620, 400);
            Vector2 joinloc = new Vector2(620, 500);
            sb.Begin();
            sb.Draw(startButton, startloc, Color.White);
            sb.Draw(joinButton, joinloc, Color.White);

        }
        public override void Update(GameTime gt)
        {
            mousestate = Mouse.GetState();
            if (prevmousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
                MouseClicked(mousestate.X, mousestate.Y);
            base.Update(gt);
        }
        private void MouseClicked(int x, int y)
        {
            Rectangle mouserect = new Rectangle(x, y, 10, 10);
            Rectangle startrect = new Rectangle(620, 400, 343, 67);
            Rectangle joinrect = new Rectangle(620, 500, 343, 67);
            if (mouserect.Intersects(startrect))
            {
                //TODO
                //switch to server screen
            }
            if (mouserect.Intersects(joinrect))
            {
                //TODO
                //switch to client screen
            }
        }
    }
}
