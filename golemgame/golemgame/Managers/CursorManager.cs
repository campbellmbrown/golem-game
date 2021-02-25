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
    public class CursorManager
    {
        private Cursor _cursor;

        public CursorManager()
        {
            _cursor = new Cursor();
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
