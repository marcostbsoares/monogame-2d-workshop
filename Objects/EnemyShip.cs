using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class EnemyShip : CollidableObject
    {
        private int health = 3;

        private int shotCounter = 0;
        private int damageFlashCounter = 0;

        private readonly WaveGenerator waveGenerator;

        private SoundEffect destroySound;
        private SoundEffect damageSound;

        private Vector2 basePosition;

        private const int minShotTime = 240;
        private const int maxShotTime = 480;

        public EnemyShip(Vector2 basePosition, int health, WaveGenerator waveGenerator) : base(16, 16, ShipGame.Self.Content.Load<Texture2D>("textures/enemy_ship"))
        {
            destroySound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/boom");
            damageSound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/hurt2");

            shotCounter = GameManager.Self.RNG.Next(minShotTime, maxShotTime);

            this.health = health;
            this.basePosition = basePosition;
            this.Position = new Vector2(GameManager.Self.RNG.Next(0, 320), -20);
            this.waveGenerator = waveGenerator;
        }

        public override void Update(GameTime gameTime)
        {
            if (GameManager.Self.IsGameOver)
                return;

            if (PosY < basePosition.Y)
                PosY++;

            PosX = basePosition.X + waveGenerator.GlobalXOffset;

            if (shotCounter > 0)
                shotCounter--;
            else
                CreateShot();

            if (damageFlashCounter > 0)
                damageFlashCounter--;
        }

        private void CreateShot()
        {
            GameManager.AddGameObject(new EnemyShot()
            {
                PosX = this.PosX,
                PosY = this.PosY + 8,
            });

            shotCounter = GameManager.Self.RNG.Next(minShotTime, maxShotTime);
        }

        protected override void OnDestroy()
        {
            destroySound.Play();
            GameManager.Self.Score += 100;
        }

        public void Damage(int damage)
        {
            damageSound.Play();

            health -= damage;

            if (health <= 0)
                Kill = true;

            damageFlashCounter = 60;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (damageFlashCounter % 10 > 5)
            {
                spriteBatch.Draw(texture, DrawPosition, null, Color.White * 0.4f);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
