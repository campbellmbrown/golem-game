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
        private Animation _animation;
        private float _timer;
        private int _frameCount;
        private float _frameSpeed;
        private int _currentFrame;

        public AnimationManager(Animation animation)
        {
            ResetAnimation(animation);
        }

        public void ResetAnimation(Animation animation)
        {
            // frameCount and frameSpeed are copied so more than one
            // of the same animation can exist
            this._animation = animation;
            _frameCount = animation.frameCount;
            _frameSpeed = animation.frameSpeed;
            _currentFrame = 0;
            _timer = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle rectangle = new Rectangle(_currentFrame * _animation.frameWidth, 0, _animation.frameWidth, _animation.frameHeight);
            spriteBatch.Draw(_animation.texture, position, rectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Play(Animation animation)
        {
            if (this._animation != animation)
            {
                ResetAnimation(animation);
            }
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > _frameSpeed)
            {
                _timer -= _frameSpeed;
                _currentFrame++;
                if (_currentFrame >= _frameCount)
                {
                    _currentFrame = 0;
                }
            }
        }

        public void Reset()
        {
            _currentFrame = 0;
            _timer = 0;
        }
    }
}
