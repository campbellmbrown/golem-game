using golemgame.Models.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Models.Scenes
{
    public class Scene
    {
        public List<TileJSON> tiles { get; set; }

        public class TileJSON
        {
            public int[] position { get; set; }
            public int[] texture_rectangle { get; set; }
        }

        public Scene() { }

        public List<Tile> Generate()
        {
            List<Tile> return_tiles = new List<Tile>();
            foreach (var tile in tiles)
            {
                return_tiles.Add(new Tile(
                    new Vector2(tile.position[0], tile.position[1]), 
                    new Rectangle(tile.texture_rectangle[0], tile.texture_rectangle[1], tile.texture_rectangle[2], tile.texture_rectangle[3]),
                    Game1.textures["tilesheet"]
                    ));
            }
            return return_tiles;
        }
    }
}
