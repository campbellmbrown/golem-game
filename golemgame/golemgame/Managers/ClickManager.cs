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

        private List<ScreenLevelLeftClick> _screenLevelLeftClicks;

        public class ScreenLevelLeftClick
        {
            public ScreenButton button { get; set; }
            public MethodDelegate outputFunc { get; set; }

            public ScreenLevelLeftClick(ScreenButton button, MethodDelegate outputFunc)
            {
                this.button = button;
                this.outputFunc = outputFunc;
            }
        }

        public ClickManager(DebugManager debugManager, ViewManager viewManager)
        {
            _debugManager = debugManager;
            _viewManager = viewManager;
            _leftClickHeldDown = false;
            _rightClickHeldDown = false;

            _screenLevelLeftClicks = new List<ScreenLevelLeftClick>();
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            // Left click
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

        public void LeftClick()
        {
            _debugManager.AddDebugMessage("Left click", DebugLevel.Debug);
            foreach (var screenLevelLeftClick in _screenLevelLeftClicks)
            {
                if (screenLevelLeftClick.button.clickRectangle.Contains(_viewManager.mousePosition))
                {
                    screenLevelLeftClick.outputFunc();
                }
            }
        }

        public void RightClick()
        {
            _debugManager.AddDebugMessage("Right click", DebugLevel.Debug);
        }

        public void AddScreenLevelLeftClick(ScreenButton button, MethodDelegate func)
        {
            _screenLevelLeftClicks.Add(new ScreenLevelLeftClick(button, func));
        }
    }
}
