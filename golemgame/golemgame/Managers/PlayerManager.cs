using golemgame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    interface IPlayerManager
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

    public class PlayerManager : IPlayerManager
    {
        protected Player player { get; }
        protected InputManager inputManager { get; }
        private Game1 _game;

        public PlayerManager(Game1 game)
        {
            _game = game;
            player = new Player();
            inputManager = new InputManager();
            inputManager.AddInputAndMethod(Keys.W, MovePlayerUp);
            inputManager.AddInputAndMethod(Keys.S, MovePlayerDown);
            inputManager.AddInputAndMethod(Keys.A, MovePlayerLeft);
            inputManager.AddInputAndMethod(Keys.D, MovePlayerRight);
        }

        public void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            ICursorManager cursorManager = (ICursorManager)Game.Services.GetService(typeof(ICursorManager));
            cursorManager.Draw(spriteBatch);

        }

        public void MovePlayerUp() { player.MoveUp(); }
        public void MovePlayerDown() { player.MoveDown(); }
        public void MovePlayerLeft() { player.MoveLeft(); }
        public void MovePlayerRight() { player.MoveRight(); }
    }
}
