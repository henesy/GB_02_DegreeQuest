using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace test_platformer
{
    public class FadeAnimation : Animations
    {
        bool increase;
        float fadeSpeed, activateValue, defaultAlpha;
        TimeSpan defaultTime, timer;
        bool startTimer, stopUpdating;

        public override float Alpha
        {
            get
            {
                return alpha;
            }

            set
            {
                alpha = value;
                if (alpha == 1.0f)
                    increase = false;
                else if (alpha == 0.0f)
                    increase = true;
            }
        }
        public float ActivateValue { get; set; }
        public float FadeSpeed { get; set; }
        public TimeSpan Timer
        {
            get { return timer; }
            set{ defaultTime = timer = value;}
        }

        public override void LoadContent(ContentManager content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(content, image, text, position);
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdating = false;
            defaultAlpha = alpha;
            
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gametime)
        {
            if (isActive)
            {
                if (!stopUpdating)
                {
                    if (!increase)
                        alpha -= fadeSpeed = (float)gametime.ElapsedGameTime.TotalSeconds;
                    else
                        alpha += fadeSpeed = (float)gametime.ElapsedGameTime.TotalSeconds;
                    if (alpha <= 0.0f)
                        alpha = 0.0f;
                    else if (alpha >= 1.0f)
                        alpha = 1.0f;
                }
                if(alpha == activateValue)
                {
                    stopUpdating = true;
                    timer -= gametime.ElapsedGameTime;
                    if(timer.TotalSeconds <= 0)
                    {
                        increase = !increase;
                        timer = defaultTime;
                        stopUpdating = false;
                    }
                }
            }
            else
            {
                alpha = defaultAlpha;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
