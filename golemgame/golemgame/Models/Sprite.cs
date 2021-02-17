using golemgame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Models
{
    public class Sprite
    {
        public enum Visual
        {
            Texture,
            Animation
        }

        private Texture2D texture { get; set; }
        private Animation animation { get; set; } // TODO: Review if this needs to be here
        private AnimationManager animationManager { get; set; }
        private Visual visualType { get; set; }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            visualType = Visual.Texture;
        }

        public Sprite(Animation animation)
        {
            this.animation = animation;
            animationManager = new AnimationManager(animation);
            visualType = Visual.Animation;
        }

        public void Update(GameTime gameTime)
        {
            if (visualType == Visual.Animation)
            {
                animationManager.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            switch (visualType)
            {
                case Visual.Texture:
                    spriteBatch.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 10, 10), Color.White);
                    break;
                case Visual.Animation:
                    animationManager.Draw(spriteBatch, position);
                    break;
            }
        }
    }
}
