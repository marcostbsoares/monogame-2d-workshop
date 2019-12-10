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
        RenderTarget2D internalScreen; 

        public static ShipGame Self;

        GameManager gameManager;

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

            internalScreen = new RenderTarget2D(this.GraphicsDevice, 320, 240);

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            gameManager = new GameManager();

            var bg = new GameObject(Content.Load<Texture2D>("Textures/background"));
            gameManager.AddGameObject(bg);

            var ship = new Ship(new Vector2(160, 200));
            gameManager.AddGameObject(ship);
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
            base.Draw(gameTime);

            DrawInternal();
            DrawScreen();
        }

        protected void DrawInternal()
        {
            GraphicsDevice.SetRenderTarget(internalScreen);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            gameManager.Draw(spriteBatch);
            
            spriteBatch.End();
        }

        protected void DrawScreen()
        {
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(internalScreen, GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();
        }
    }
}