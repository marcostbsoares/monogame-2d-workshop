using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mono_VsCode.Core;
using Mono_VsCode.Objects;

namespace Mono_VsCode
{
    public class ShipGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static ShipGame Self { get; private set; }

        GameManager gameManager;

        private RenderTarget2D renderTarget;
        private Rectangle DrawArea { get; set; }
        public Rectangle GameArea { get; private set; }

        public static Texture2D Pixel { get; set; }

        public ShipGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Self = this;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Pixel = Content.Load<Texture2D>("textures/pixel");

            gameManager = GameManager.Create();

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;

            DrawArea = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            GameArea = new Rectangle(0, 0, 320, 240);

            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);

            InitializeGameObjects();

            gameManager.Initialize();
        }

        private void InitializeGameObjects()
        {
            GameManager.AddGameObject(new Background());
            GameManager.AddGameObject(new PlayerShip()
            {
                Name = "Player",
                PosX = 160,
                PosY = 210,
            });

            GameManager.AddGameObject(new WaveGenerator() { Name = "WaveGenerator" });
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            gameManager.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, DrawArea, null, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}