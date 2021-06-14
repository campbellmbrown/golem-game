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
        private TileManager _tileManager;
        private PlayerManager _playerManager;
        private CursorManager _cursorManager;

        public PlayingState(ViewManager viewManager)
        {
            _tileManager = new TileManager();
            // Player manager controls the camera position
            _playerManager = new PlayerManager(viewManager);
            // Cursor manager needs the mouse position
            _cursorManager = new CursorManager(viewManager);
        }

        public void Update(GameTime gameTime)
        {
            _tileManager.Update(gameTime);
            _playerManager.Update(gameTime);
            _cursorManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _tileManager.Draw(spriteBatch);
            _playerManager.Draw(spriteBatch);
            _cursorManager.Draw(spriteBatch);
        }
    }
}
