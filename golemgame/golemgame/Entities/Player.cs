using golemgame.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Entities
{
    public class Player : MovingEntity
    {
        public Player() : base(new Sprite(Game1.animations["player_idle_left"]), Vector2.Zero)
        {
        }

        public void MoveUp()
        {
            velocity.Y -= 50;
        }

        public void MoveDown()
        {
            velocity.Y += 50;
        }

        public void MoveLeft()
        {
            velocity.X -= 50;
        }

        public void MoveRight()
        {
            velocity.X += 50;
        }
    }

}
