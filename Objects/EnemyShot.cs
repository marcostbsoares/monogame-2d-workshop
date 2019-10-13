using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class EnemyShot : CollidableObject
    {
        public EnemyShot() : base(3, 5, ShipGame.Self.Content.Load<Texture2D>("textures/shot"))
        {

        }

        public override void Update(GameTime gameTime)
        {
            PosY += 4;

            if (PosY <= 0 || PosY > 240)
                Kill = true;
        }

        public override void OnCollide(CollidableObject otherObject)
        {
            if (otherObject is PlayerShip)
            {
                (otherObject as PlayerShip).Damage(1);
                Kill = true;
            }
        }
    }
}
