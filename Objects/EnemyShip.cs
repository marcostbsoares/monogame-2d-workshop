using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class EnemyShip : CollidableObject
    {
        private int movementCounter = -60;
        private int health = 3;
        private int shotCounter;
        private int damageFlashCounter = 0;
        private int targetY;

        private SoundEffect destroySound;
        private SoundEffect damageSound;

        public EnemyShip(int targetY) : base(16, 16, ShipGame.Self.Content.Load<Texture2D>("textures/enemy_ship"))
        {
            destroySound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/boom");
            damageSound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/hurt2");

            shotCounter = GameManager.Self.RNG.Next(60, 180);
            this.targetY = targetY;
        }

        public override void Update(GameTime gameTime)
        {
            if (PosY < targetY)
                PosY++;

            if (movementCounter >= 60)
                movementCounter *= -1;

            movementCounter++;
            PosX += 1 * Math.Sign(movementCounter);

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

            shotCounter = GameManager.Self.RNG.Next(60, 180);
        }

        protected override void OnDestroy()
        {
            destroySound.Play();
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
