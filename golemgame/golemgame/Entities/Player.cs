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
        public Player() : base(new Sprite(), Vector2.Zero)
        {
        }
    }
}
