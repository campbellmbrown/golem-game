using golemgame.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public enum GameState {
        Playing
    }

    public class GameManager
    {
        private GameState _gameState;

        // Common to all states
        private CursorManager _cursorManager { get; set; }

        private PlayingState _playingState;

        public GameManager()
        {
            _gameState = GameState.Playing;

            _cursorManager = new CursorManager();

            _playingState = new PlayingState();
        }

        public void Update(GameTime gameTime)
        {
            _cursorManager.Update(gameTime);
            switch (_gameState)
            {
                case GameState.Playing:
                    _playingState.Update(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (_gameState)
            {
                case (GameState.Playing):
                    _playingState.Draw(spriteBatch);
                    break;
                default:
                    break;
            }

            // Cursor is always drawn
            _cursorManager.Draw(spriteBatch);
        }
    }
}
