﻿using golemgame.Models;
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
            AddToVelocity(0, -50);
        }

        public void MoveDown()
        {
            AddToVelocity(0, 50);
        }

        public void MoveLeft()
        {
            AddToVelocity(-50, 0);
        }

        public void MoveRight()
        {
            AddToVelocity(50, 0);
        }
    }

}
