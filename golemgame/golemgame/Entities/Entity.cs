using golemgame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Entities
{
    public class Entity
    {
        protected Sprite sprite { get; set; }
        protected Vector2 position { get; set; }

        public Entity(Sprite sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position);
        }
    }
}
