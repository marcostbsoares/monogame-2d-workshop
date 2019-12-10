using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Objects;

namespace Mono_VsCode.Core
{
    public class GameManager
    {
        List<GameObject> gameObjects = new List<GameObject>();

        public static GameManager Self;

        public GameManager()
        {
            Self = this;
        }

        public void AddGameObject(GameObject obj)
        {
            gameObjects.Add(obj);
        }

        public void Update(GameTime gameTime)
        {
            gameObjects.RemoveAll(x => x.Killed);

            for(int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameObjects.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
