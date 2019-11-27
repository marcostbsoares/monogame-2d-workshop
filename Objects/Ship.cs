using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mono_VsCode.Objects
{
    public class Ship
    {
        Texture2D texture;
        Vector2 position;

        float velocity = 180f;

        public Ship(Vector2 position)
        {
            texture = ShipGame.Self.Content.Load<Texture2D>("Textures/ship");
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= (float)(velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += (float)(velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }

             position.X = MathHelper.Clamp(position.X, 0, 640 - texture.Width);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White);
        }
        
    }
}
