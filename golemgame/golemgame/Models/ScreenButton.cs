using golemgame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace golemgame.Models
{
    public class ScreenButton
    {
        private readonly DebugManager _debugManager;
        private readonly ViewManager _viewManager;
        private Texture2D _texture;

        // Relative to screen position
        private Vector2 _position;

        private Vector2 _drawPosition { get { return _viewManager.camera.ScreenToWorld(_position); } }
        public RectangleF clickRectangle { get { return new RectangleF(_drawPosition.X, _drawPosition.Y, _texture.Width, _texture.Height); } }

        public ScreenButton(DebugManager debugManager, ViewManager viewManager, Texture2D texture, Vector2 position, ClickManager clickManager)
        {
            _debugManager = debugManager;
            _viewManager = viewManager;
            _texture = texture;
            _position = position;

            clickManager.AddScreenLevelLeftClick(this, LeftClickAction);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void LeftClickAction()
        {
            _debugManager.AddDebugMessage("Button click action", DebugLevel.Debug);
        }
    }
}
