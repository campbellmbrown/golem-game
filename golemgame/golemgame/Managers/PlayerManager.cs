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
    public class PlayerManager
    {
        protected Player player { get; set; }
        protected InputManager inputManager { get; set; }

        public PlayerManager()
        {
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
        }

        public void MovePlayerUp()    { player.MoveUp();    }
        public void MovePlayerDown()  { player.MoveDown();  }
        public void MovePlayerLeft()  { player.MoveLeft();  }
        public void MovePlayerRight() { player.MoveRight(); }
    }
}
