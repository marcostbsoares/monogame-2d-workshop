using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_VsCode.Core
{
    public abstract class GameObject
    {
        protected readonly Texture2D texture;

        public string Name;

        public Vector2 Position { get; set; }
        public float PosX
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }
        public float PosY
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        private Vector2 _drawPosition = new Vector2();
        protected virtual Vector2 DrawPosition
        {
            get
            {
                _drawPosition.X = (float)(Math.Floor(PosX) - texture.Width / 2);
                _drawPosition.Y = (float)(Math.Floor(PosY) - texture.Height / 2);
                return _drawPosition;
            }
        }

        private bool _kill;
        public bool Kill
        {
            get
            {
                return _kill;
            }
            set
            {
                if (value && !_kill)
                {
                    _kill = true;
                    OnDestroy();
                }
            }
        }

        protected GameObject(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Initialize()
        {

        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, DrawPosition, null, Color.White);
        }

        protected virtual void OnDestroy()
        {

        }
    }
}