using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Models.Tiles
{
    public class Tile
    {
        public const int TILE_SIZE = 16;
        public static Dictionary<int, Rectangle> textureRectangles = new Dictionary<int, Rectangle>()
        {
            { 0, new Rectangle(0, 0, TILE_SIZE, TILE_SIZE) },
            { 1, new Rectangle(TILE_SIZE, 0, TILE_SIZE, TILE_SIZE) },
            { 2, new Rectangle(2 * TILE_SIZE, 0, TILE_SIZE, TILE_SIZE) },
            { 3, new Rectangle(3 * TILE_SIZE, 0, TILE_SIZE, TILE_SIZE) },
            { 4, new Rectangle(0, TILE_SIZE, TILE_SIZE, TILE_SIZE) },
            { 5, new Rectangle(TILE_SIZE, TILE_SIZE, TILE_SIZE, TILE_SIZE) },
            { 6, new Rectangle(2 * TILE_SIZE, TILE_SIZE, TILE_SIZE, TILE_SIZE) },
            { 7, new Rectangle(3 * TILE_SIZE, TILE_SIZE, TILE_SIZE, TILE_SIZE) },
        };

        private Vector2 _position;
        private Rectangle _textureRectangle;
        private Texture2D _texture;

        public Tile(Vector2 positionIdx, Rectangle textureRectangle, Texture2D texture)
        {
            _position = positionIdx * TILE_SIZE;
            _textureRectangle = textureRectangle;
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _textureRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
