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
        private TileManager _tileManager { get; set; }
        private PlayerManager _playerManager { get; set; }
        private CursorManager _cursorManager { get; set; }

        public PlayingState()
        {
            _tileManager = new TileManager();
            _playerManager = new PlayerManager();
            _cursorManager = new CursorManager();
        }

        public void Update(GameTime gameTime)
        {
            _tileManager.Update(gameTime);
            _playerManager.Update(gameTime);
            _cursorManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _tileManager.Draw(spriteBatch);
            _playerManager.Draw(spriteBatch);
            _cursorManager.Draw(spriteBatch);
        }
    }
}
