using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class WaveGenerator : GameObject
    {
        private Random RNG = GameManager.Self.RNG;

        public float GlobalXOffset { get; private set; }

        private int movementTimer = 0;
        private int spawnTimer = 0;
        private int difficulty = 0;

        private Queue<Vector2> shipPositions = new Queue<Vector2>();

        private bool movingRight = true;

        public WaveGenerator() : base(null)
        {
            Name = "WaveGen";
            InitializeWave();
        }

        private void InitializeWave()
        {
            var positions = new List<Vector2>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    positions.Add(new Vector2(20 + 20 * x, 20 + 20 * y));
                    positions = positions.OrderBy(n => RNG.NextDouble()).ToList();
                }
            }

            shipPositions = new Queue<Vector2>(positions);
        }

        private void SpawnShip()
        {
            if (shipPositions.Any())
                GameManager.AddGameObject(new EnemyShip(shipPositions.Dequeue(), 2 + difficulty, this));

            spawnTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            movementTimer++;

            if (movingRight)
                GlobalXOffset = movementTimer;
            else
                GlobalXOffset = 120 - movementTimer;

            if (movementTimer >= 120)
            {
                movementTimer = 0;
                movingRight = !movingRight;
            }

            spawnTimer++;
            if (spawnTimer >= 60 - difficulty * 2)
                SpawnShip();

            if (!shipPositions.Any() && GameManager.CountObjects<EnemyShip>() == 0)
            {
                InitializeWave();
                difficulty++;
            }
        }
    }
}