using golemgame.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    interface ICursorManager
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

    public class CursorManager : ICursorManager
    {
        private Cursor _cursor;
        private Game1 _game;

        public CursorManager(Game1 game)
        {
            _cursor = new Cursor();
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            _cursor.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _cursor.Draw(spriteBatch);
        }
    }
}
