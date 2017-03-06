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
    public class Animations
    {
        protected Texture2D image;
        protected string text;
        protected SpriteFont font;
        protected Color color;
        protected Rectangle sourceRect;
        protected float rotation, scale, axis, alpha;
        protected Vector2 origin, position;
        protected ContentManager content;
        protected bool isActive;

        public virtual float Alpha { get; set; }
        public bool IsActive { get; set; }

        public float Scale
        {
            set { scale = value; }
            get { return scale; }
        }

        public virtual void LoadContent(ContentManager content,Texture2D image, string text, Vector2 position)
        {
            isActive = false;
            this.content = new ContentManager(content.ServiceProvider, "content");
            this.image = image;
            this.text = text;
            this.position = position;
            if (text != String.Empty)
            {
                font = content.Load<SpriteFont>("AnimationFont");
                color = new Color(144, 77, 255);
            }
            if (image != null)
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            rotation = 0.0f;
            axis = 0.0f;
            alpha = scale = 1.0f;
        }
        public virtual void UnloadContent()
        {
            content.Unload();
        }
        public virtual void Update(GameTime gametime)
        {
            //do nothing right now
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
            if(image != null)
            {
                origin = new Vector2(sourceRect.Width / 2,
                    sourceRect.Height / 2);
                spritebatch.Draw(image, position + origin, sourceRect, Color.White * alpha,
                    rotation, origin, scale, SpriteEffects.None, 0.0f);
            }
            if(text != String.Empty)
            {
                origin = new Vector2(font.MeasureString(text).X / 2,
                    font.MeasureString(text).Y / 2);
                spritebatch.DrawString(font, text, position + origin, color * alpha,
                    rotation, origin, scale, SpriteEffects.None, 0.0f);

            }
        }
    }
}
