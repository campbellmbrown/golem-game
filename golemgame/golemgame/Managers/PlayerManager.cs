using golemgame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class PlayerManager
    {
        protected Player player { get; set; }

        public PlayerManager()
        {
            player = new Player();
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }
    }
}
