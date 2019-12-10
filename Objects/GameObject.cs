using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_VsCode.Objects
{
    public class GameObject
    {
        protected readonly Texture2D texture;

        public virtual Rectangle Hitbox => Rectangle.Empty;

        public Vector2 position;

        public bool Killed = false;

        public GameObject(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
