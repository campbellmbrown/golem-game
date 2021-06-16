using golemgame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class PlayerManager
    {
        protected Player player { get; }
        protected InputManager inputManager { get; }
        private ViewManager _viewManager;

        public PlayerManager(ViewManager viewManager)
        {
            _viewManager = viewManager;
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
            _viewManager.UpdateCameraPosition(player.center);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            spriteBatch.DrawPoint(_viewManager.mousePosition, Color.Blue, 5);
        }

        public void MovePlayerUp() { player.MoveUp(); }
        public void MovePlayerDown() { player.MoveDown(); }
        public void MovePlayerLeft() { player.MoveLeft(); }
        public void MovePlayerRight() { player.MoveRight(); }
    }
}
