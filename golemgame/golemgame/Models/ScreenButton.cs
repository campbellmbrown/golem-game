using golemgame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace golemgame.Models
{
    public interface ILeftClickable
    {
        /// <summary>
        /// The action to perform when a left click occurs.
        /// </summary>
        void LeftClickAction();

        /// <summary>
        /// Checks if the mouse position is hovering over the click rectangle.
        /// </summary>
        bool IsHovering(Vector2 mousePosition);

        RectangleF clickRectangle { get; }
    }

    public class ScreenButton : ILeftClickable
    {
        private readonly DebugManager _debugManager;
        private readonly ViewManager _viewManager;
        private Texture2D _texture;

        // Relative to screen position
        private Vector2 _position;

        private Vector2 _drawPosition { get { return _viewManager.camera.ScreenToWorld(_position); } }
        public RectangleF clickRectangle { get { return new RectangleF(_drawPosition.X, _drawPosition.Y, _texture.Width, _texture.Height); } }

        /// <summary>
        /// A button on the screen that can be clicked, and has a corresponding action.
        /// </summary>
        /// <param name="debugManager">Game debug manager to provide debug prints.</param>
        /// <param name="viewManager">Game view manager to get mouse position.</param>
        /// <param name="texture">The texture of the button.</param>
        /// <param name="position">The position of the button, in screen coordinates.</param>
        /// <param name="clickManager">The click manager that will handle click detection of the button.</param>
        public ScreenButton(DebugManager debugManager, ViewManager viewManager, Texture2D texture, Vector2 position, ClickManager clickManager)
        {
            _debugManager = debugManager;
            _viewManager = viewManager;
            _texture = texture;
            _position = position;

            clickManager.AddScreenLevelLeftClick(this);
        }

        /// <summary>
        /// Draws the screen button.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch object.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// This function is passed to and called by the <c>ClickManager</c> object.
        /// Implements the <c>IsHovering</c> function from the <c>ILeftClickable</c> interface.
        /// </summary>
        public void LeftClickAction()
        {
            _debugManager.AddDebugMessage("Button click action", DebugLevel.Debug);
        }

        /// <summary>
        /// Implements the <c>IsHovering</c> function from the <c>ILeftClickable</c> interface.
        /// </summary>
        /// <param name="mousePosition">Mouse position, in world coordinates.</param>
        /// <returns></returns>
        public bool IsHovering(Vector2 mousePosition)
        {
            return clickRectangle.Contains(mousePosition);
        }
    }
}
