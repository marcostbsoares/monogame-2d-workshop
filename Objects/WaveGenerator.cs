using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class WaveGenerator : GameObject
    {
        Random RNG = GameManager.Self.RNG;

        int counter = 0;
        int spawnInterval = 120;

        public WaveGenerator() : base(null)
        {
            Name = "WaveGen";
        }

        private void SpawnShip()
        {
            GameManager.AddGameObject(new EnemyShip(RNG.Next(20, 80))
            {
                PosY = -20,
                PosX = RNG.Next(80, 240),
            });
        }

        public override void Update(GameTime gameTime)
        {
            counter++;
            if (counter > spawnInterval)
            {
                counter = 0;

                if (spawnInterval > 30)
                    spawnInterval--;

                SpawnShip();
            }
        }
    }
}
