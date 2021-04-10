using golemgame.Models.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class TileManager
    {
        private List<Tile> _tiles;

        public TileManager()
        {
            _tiles = new List<Tile>();

            // TODO: Add level interpreter here
            _tiles.Add(new Tile(new Vector2(0, 0), Tile.textureRectangles[0], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(1, 0), Tile.textureRectangles[1], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(2, 0), Tile.textureRectangles[2], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(3, 0), Tile.textureRectangles[3], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(0, 1), Tile.textureRectangles[4], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(1, 1), Tile.textureRectangles[5], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(2, 1), Tile.textureRectangles[6], Game1.textures["test_tilesheet"]));
            _tiles.Add(new Tile(new Vector2(3, 1), Tile.textureRectangles[7], Game1.textures["test_tilesheet"]));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var tile in _tiles)
            {
                tile.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
