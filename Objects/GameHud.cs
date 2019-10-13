using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Core;

namespace Mono_VsCode.Objects
{
    public class GameHud
    {
        private readonly GameFont font;

        private StringBuilder score = new StringBuilder();
        private StringBuilder health = new StringBuilder();

        private string gameOverString = "Game Over!";
        private PlayerShip player;

        public GameHud()
        {
            font = new GameFont("Fonts/retroFont");
            player = GameManager.FindObject<PlayerShip>("Player");
        }

        public void Update()
        {
            score.Clear();
            score.Append("Score: ");
            score.Append(GameManager.Self.Score.ToString());

            health.Clear();
            health.Append("HP: ");
            for (int i = 0; i < player.Health; i++)
            {
                health.Append('|');
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            font.DrawString(score.ToString(), new Vector2(2, 2), spriteBatch, Color.White);

            if (GameManager.Self.IsGameOver)
            {
                font.DrawStringCentered(gameOverString, new Vector2(160, 120), spriteBatch, Color.Red);
            }

            font.DrawString(health.ToString(), new Vector2(2, 224), spriteBatch, Color.White);
        }
    }
}
