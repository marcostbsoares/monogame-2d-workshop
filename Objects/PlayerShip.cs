using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class PlayerShip : CollidableObject
    {
        private const float movementSpeed = 1.66f;

        private int shotCooldown = 0;

        public int Health { get; private set; } = 5;

        private SoundEffect shotSound;
        private SoundEffect destroySound;
        private SoundEffect damageSound;

        public PlayerShip() : base(16, 16, ShipGame.Self.Content.Load<Texture2D>("textures/ship"))
        {
            shotSound = ShipGame.Self.Content.Load<SoundEffect>("sounds/shot");
            destroySound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/boom");
            damageSound = ShipGame.Self.Content.Load<SoundEffect>("Sounds/hurt2");
        }

        public override void Update(GameTime gameTime)
        {
            if (shotCooldown > 0)
                shotCooldown--;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                PosX -= movementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                PosX += movementSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && shotCooldown == 0)
                CreateShot();

            PosX = MathHelper.Clamp(PosX, 0, 320);
        }

        public void Damage(int damage)
        {
            damageSound.Play();
            Health -= damage;

            if (Health <= 0)
                Kill = true;
        }

        private void CreateShot()
        {
            shotSound.Play();

            shotCooldown = 12;
            GameManager.AddGameObject(new PlayerShot()
            {
                PosX = this.PosX,
                PosY = this.PosY - 8,
            });
        }

        protected override void OnDestroy()
        {
            destroySound.Play();
            GameManager.Self.StopGame();
        }
    }
}
