using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_VsCode.Core
{
    public abstract class CollidableObject : GameObject
    {
        protected CollidableObject(int width, int height, Texture2D texture)
                            : base(texture)
        {
            _hitbox.Width = width;
            _hitbox.Height = height;
        }

        protected Rectangle _hitbox = new Rectangle();

        public virtual Rectangle Hitbox
        {
            get
            {
                _hitbox.X = (int)Position.X - _hitbox.Width / 2;
                _hitbox.Y = (int)Position.Y - _hitbox.Height / 2;
                return _hitbox;
            }
        }

        public bool TryCollide(CollidableObject collidable)
        {
            if (collidable.Hitbox.Intersects(Hitbox))
            {
                OnCollide(collidable);
                collidable.OnCollide(this);
                return true;
            }

            return false;
        }

        public virtual void OnCollide(CollidableObject otherObject)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.Draw(ShipGame.Pixel, Hitbox, new Rectangle(0, 0, 1, 1), Color.Red * 0.5f);
        }
    }
}
