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

        public void Generate()
        {
            foreach (var tile in tiles)
            {
                Console.Write(tile.position);
                Console.Write(tile.texture_rectangle);
            }
        }
    }
}
