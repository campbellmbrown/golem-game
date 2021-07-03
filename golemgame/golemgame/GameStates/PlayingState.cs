using golemgame.Managers;
using golemgame.Models;
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
        private readonly TileManager _tileManager;
        private readonly PlayerManager _playerManager;
        private readonly CursorManager _cursorManager;
        private readonly DebugManager _debugManager;
        // Only 1 click manager per each state
        private readonly ClickManager _clickManager;

        private List<ScreenButton> _screenButtons;

        public PlayingState(ViewManager viewManager)
        {
            _tileManager = new TileManager();
            // Player manager controls the camera position
            _playerManager = new PlayerManager(viewManager);
            // Cursor manager needs the mouse position
            _cursorManager = new CursorManager(viewManager);
            // Debug manager needs the bottomLeft position
            _debugManager = new DebugManager(viewManager);
            _clickManager = new ClickManager(_debugManager, viewManager);

            _screenButtons = new List<ScreenButton>();
        }

        public void Update(GameTime gameTime)
        {
            _tileManager.Update(gameTime);
            _playerManager.Update(gameTime);
            _cursorManager.Update();
            _debugManager.Update(gameTime);
            _clickManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _tileManager.Draw(spriteBatch);
            _playerManager.Draw(spriteBatch);
            _cursorManager.Draw(spriteBatch);
            _debugManager.Draw(spriteBatch);

            foreach (var screenButton in _screenButtons)
            {
                screenButton.Draw(spriteBatch);
            }
        }
    }
}
