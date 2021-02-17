using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Models
{
    public class Animation
    {
        public Texture2D texture { get; private set; }
        public float frameSpeed { get; private set; }
        public int frameCount { get; private set; }
        public int frameHeight { get { return texture.Height; } }
        public int frameWidth { get { return texture.Width / frameCount; } }

        public Animation(Texture2D texture, int frameCount, float frameSpeed)
        {
            this.texture = texture;
            this.frameCount = frameCount;
            this.frameSpeed = frameSpeed;
        }
    }
}
