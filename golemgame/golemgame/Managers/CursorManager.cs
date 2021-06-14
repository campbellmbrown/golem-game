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
        private ViewManager _viewManager;

        public CursorManager(ViewManager viewManager)
        {
            _viewManager = viewManager;
            _cursor = new Cursor();
        }

        public void Update()
        {
            _cursor.Update(_viewManager.mousePosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _cursor.Draw(spriteBatch, 1/_viewManager.camera.Zoom);
        }
    }
}
