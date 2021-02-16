using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Models
{
    public class Sprite
    {
        Texture2D texture { get; set; }

        public Sprite()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 10, 10), Color.White);
        }
    }
}
