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
        public int frameCount { get; private set; }
        public float frameSpeed { get; private set; }
        public int frameHeight { get { return texture.Height; } }
        public int frameWidth { get { return texture.Width / frameCount; } }
        private Texture2D texture { get; set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed)
        {
            this.texture = texture;
            this.frameCount = frameCount;
            this.frameSpeed = frameSpeed;
        }
    }
}
