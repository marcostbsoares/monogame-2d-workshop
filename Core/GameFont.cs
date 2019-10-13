using Cyotek.Drawing.BitmapFont;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_VsCode.Core
{
    public class GameFont
    {
        private BitmapFont bitmapFont;
        private Texture2D fontTexture;

        public GameFont(string path)
        {
            bitmapFont = BitmapFontLoader.LoadFontFromTextFile("./Content/" + path + ".fnt");
            fontTexture = ShipGame.Self.Content.Load<Texture2D>(path + "_0");
        }

        public void DrawString(string str, Vector2 position, SpriteBatch spriteBatch, Color color)
        {
            Vector2 drawPosition = position;

            foreach (char c in str)
            {
                var destRectangle = new Rectangle((int)drawPosition.X, (int)drawPosition.Y + bitmapFont[c].Offset.Y, bitmapFont[c].Bounds.Width, bitmapFont[c].Bounds.Height);
                var srcRectangle = new Rectangle(bitmapFont[c].Bounds.X, bitmapFont[c].Bounds.Y, bitmapFont[c].Bounds.Width, bitmapFont[c].Bounds.Height);

                spriteBatch.Draw(fontTexture, destRectangle, srcRectangle, color);
                drawPosition.X += bitmapFont[c].XAdvance;
            }
        }

        public void DrawStringCentered(string str, Vector2 position, SpriteBatch spriteBatch, Color color)
        {
            var measureString = bitmapFont.MeasureFont(str);
            var drawPosition = new Vector2(position.X - measureString.Width / 2, position.Y - measureString.Height / 2);

            DrawString(str, drawPosition, spriteBatch, color);
        }
    }
}
