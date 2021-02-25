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

        private Texture2D _texture;
        private Animation _animation; // TODO: Review if this needs to be here
        private AnimationManager _animationManager;
        private Visual _visualType;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _visualType = Visual.Texture;
        }

        public Sprite(Animation animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(animation);
            _visualType = Visual.Animation;
        }

        public void Update(GameTime gameTime)
        {
            if (_visualType == Visual.Animation)
            {
                _animationManager.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            switch (_visualType)
            {
                case Visual.Texture:
                    spriteBatch.Draw(_texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
                case Visual.Animation:
                    _animationManager.Draw(spriteBatch, position);
                    break;
            }
        }
    }
}
