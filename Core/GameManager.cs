using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono_VsCode.Objects;

namespace Mono_VsCode.Core
{
    public class GameManager
    {
        public static GameManager Self;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<CollidableObject> collidables = new List<CollidableObject>();

        public Random RNG = new Random(System.DateTime.Now.Millisecond);

        public int Score { get; set; }
        public bool IsGameOver { get; private set; }

        private GameHud Hud;

        protected GameManager()
        {

        }

        public static GameManager Create()
        {
            if (Self != null)
                return Self;

            var gameManager = new GameManager();
            Self = gameManager;
            return gameManager;
        }

        public void Initialize()
        {
            Hud = new GameHud();
        }

        public void Update(GameTime gameTime)
        {
            gameObjects.RemoveAll(x => x.Kill);
            collidables.RemoveAll(x => x.Kill);

            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Update(gameTime);

            for (int c1 = 0; c1 < collidables.Count - 1; c1++)
            {
                for (int c2 = c1 + 1; c2 < collidables.Count; c2++)
                {
                    collidables[c1].TryCollide(collidables[c2]);
                }
            }

            Hud.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameObjects.ForEach(x => x.Draw(spriteBatch));

            Hud.Draw(spriteBatch);
        }

        public static T FindObject<T>(string name) where T : GameObject
        {
            return Self.gameObjects.SingleOrDefault(x => x.Name == name) as T;
        }

        public static GameObject FindObject(string name)
        {
            return Self.gameObjects.SingleOrDefault(x => x.Name == name);
        }

        public void StopGame()
        {
            FindObject("WaveGenerator").Kill = true;
            IsGameOver = true;
        }

        #region Static Methods

        public static void AddGameObject(GameObject gameObject)
        {
            Self.gameObjects.Add(gameObject);

            if (gameObject is CollidableObject)
                Self.collidables.Add(gameObject as CollidableObject);
        }

        #endregion
    }
}
