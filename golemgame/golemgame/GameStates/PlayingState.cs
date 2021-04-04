using golemgame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.GameStates
{
    public class PlayingState
    {
        private PlayerManager _playerManager { get; set; }

        public PlayingState()
        {
            _playerManager = new PlayerManager();
        }

        public void Update(GameTime gameTime)
        {
            _playerManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _playerManager.Draw(spriteBatch);
        }
    }
}
