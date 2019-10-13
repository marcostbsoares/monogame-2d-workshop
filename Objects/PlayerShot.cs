using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class PlayerShot : CollidableObject
    {
        public PlayerShot() : base(3, 5, ShipGame.Self.Content.Load<Texture2D>("textures/shot"))
        {

        }

        public override void Update(GameTime gameTime)
        {
            PosY -= 4;

            if (PosY <= 0)
                Kill = true;
        }

        public override void OnCollide(CollidableObject otherObject)
        {
            if (otherObject is EnemyShip)
            {
                (otherObject as EnemyShip).Damage(1);
                Kill = true;
            }
        }
    }
}
