using golemgame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class AnimationManager
    {
        private Animation animation { get; set; }
        private float timer { get; set; }
        private int frameCount { get; set; }
        private float frameSpeed { get; set; }
        private int currentFrame { get; set; }

        public AnimationManager(Animation animation)
        {
            ResetAnimation(animation);
        }

        public void ResetAnimation(Animation animation)
        {
            // frameCount and frameSpeed are copied so more than one
            // of the same animation can exist
            this.animation = animation;
            frameCount = animation.frameCount;
            frameSpeed = animation.frameSpeed;
            currentFrame = 0;
            timer = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle rectangle = new Rectangle(currentFrame * animation.frameWidth, 0, animation.frameWidth, animation.frameHeight);
            spriteBatch.Draw(animation.texture, position, rectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Play(Animation animation)
        {
            if (this.animation != animation)
            {
                ResetAnimation(animation);
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > frameSpeed)
            {
                timer -= frameSpeed;
                currentFrame++;
                if (currentFrame >= frameCount)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Reset()
        {
            currentFrame = 0;
            timer = 0;
        }
    }
}
