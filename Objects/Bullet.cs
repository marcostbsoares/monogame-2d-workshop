using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_VsCode.Objects
{
    public class Bullet : GameObject
    {
        float velocity = 5f;

        public Bullet(Vector2 position) : base(ShipGame.Self.Content.Load<Texture2D>("Textures/shot"))
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            position.Y -= velocity;

            if(position.Y < -30)
            {
                Killed = true;
            }
        }
    }
}
