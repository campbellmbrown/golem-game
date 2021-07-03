using golemgame.Models;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace golemgame.Managers
{
    public class ClickManager
    {
        public delegate void MethodDelegate();

        private readonly DebugManager _debugManager;
        private readonly ViewManager _viewManager;
        private bool _leftClickHeldDown;
        private bool _rightClickHeldDown;

        private List<ILeftClickable> _screenButtons;

        /// <summary>
        /// This manager checks if buttons are clicked and performs their appropriate action.
        /// </summary>
        /// <param name="debugManager">Game debug manager to provide debug prints.</param>
        /// <param name="viewManager">Game view manager to get mouse position.</param>
        public ClickManager(DebugManager debugManager, ViewManager viewManager)
        {
            _debugManager = debugManager;
            _viewManager = viewManager;
            _leftClickHeldDown = false;
            _rightClickHeldDown = false;

            _screenButtons = new List<ILeftClickable>();
        }

        /// <summary>
        /// Update method to check mouse clicking and performing appropriate action.
        /// </summary>
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!_leftClickHeldDown)
                {
                    LeftClick();
                }
                _leftClickHeldDown = true;
            }
            else _leftClickHeldDown = false;

            // Right click
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (!_rightClickHeldDown)
                {
                    RightClick();
                }
                _rightClickHeldDown = true;
            }
            else _rightClickHeldDown = false;
        }

        /// <summary>
        /// Checks all the <c>ILeftClickable</c> objects if they have been clicked and performs their action.
        /// </summary>
        public void LeftClick()
        {
            _debugManager.AddDebugMessage("Left click", DebugLevel.Debug);
            foreach (var screenButton in _screenButtons)
            {
                if (screenButton.IsHovering(_viewManager.mousePosition))
                {
                    screenButton.LeftClickAction();
                }
            }
        }

        /// <summary>
        /// Checks all the <c>IRightClickable</c> objects if they have been clicked and performs their action.
        /// </summary>
        public void RightClick()
        {
            _debugManager.AddDebugMessage("Right click", DebugLevel.Debug);
        }

        /// <summary>
        /// Registers a button to be viable to be clicked.
        /// </summary>
        /// <param name="button">The button to be registered.</param>
        public void AddScreenLevelLeftClick(ILeftClickable button)
        {
            _screenButtons.Add(button);
        }
    }
}
