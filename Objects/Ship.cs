using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class Ship : GameObject
    {
        float velocity = 180f;

        public int shotCooldown = 0;

        public override Rectangle Hitbox => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        public Ship(Vector2 position): base(ShipGame.Self.Content.Load<Texture2D>("Textures/ship"))
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= (float)(velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += (float)(velocity * gameTime.ElapsedGameTime.TotalSeconds);
            }

             position.X = MathHelper.Clamp(position.X, 0, 320 - texture.Width);

            if(shotCooldown > 0)
                shotCooldown--;

             if(shotCooldown == 0 && Keyboard.GetState().IsKeyDown(Keys.Up))
             {
                 shotCooldown = 20;

                 Vector2 bulletPosition = this.position;
                 bulletPosition.X += (this.texture.Width / 2) -1;
                 GameManager.Self.AddGameObject(new Bullet(bulletPosition));
             }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, null, Color.White);
            base.Draw(spriteBatch);
        }
        
    }
}
