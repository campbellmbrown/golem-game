using golemgame.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Entities
{
    public class MovingEntity : Entity
    {
        protected Vector2 velocity { get; set; }

        public MovingEntity(Sprite sprite, Vector2 position) : base(sprite, position)
        {
        }

        protected void Move(float deltaT)
        {
            position += velocity * deltaT;
        }
    }
}
