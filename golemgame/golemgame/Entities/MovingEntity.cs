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
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Move((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
            velocity = Vector2.Zero;
        }

        protected void Move(float deltaT)
        {
            position += velocity * deltaT;
        }

        protected void AddToVelocity(float deltaX, float deltaY)
        {
            velocity = new Vector2(velocity.X + deltaX, velocity.Y + deltaY);
        }
    }
}
