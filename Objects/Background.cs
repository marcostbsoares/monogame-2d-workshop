using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class Background : GameObject
    {
        public Background() : base(ShipGame.Self.Content.Load<Texture2D>("textures/background"))
        {
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
