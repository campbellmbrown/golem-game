using golemgame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Gui
{
    public class Cursor
    {
        private Sprite _sprite;
        private Vector2 _position;

        public Cursor()
        {
            _position = Vector2.Zero;
            _sprite = new Sprite(Game1.textures["cursor"]);
        }

        public void Update(GameTime gameTime)
        {
            //_position = viewManager.mousePosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position);
        }
    }
}
