using golemgame.Models.Scenes;
using golemgame.Models.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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

            string strJSON = System.IO.File.ReadAllText(@"D:\Git Projects\golem-game\golemgame\golemgame\Scenes\scene.json");
            Scene location = JsonConvert.DeserializeObject<Scene>(strJSON);
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
