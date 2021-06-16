using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Text;

namespace golemgame.Managers
{
    public enum DebugLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
        Other = 5,
        DebugOff = 6
    }

    public class DebugPrint
    {
        private string _debugText;
        public static float scale = 2;
        private DebugLevel _debugLevel;
        private static Dictionary<DebugLevel, Color> _debugColors = new Dictionary<DebugLevel, Color>()
        {
            { DebugLevel.Debug, Color.LightGreen },
            { DebugLevel.Info, Color.LightBlue },
            { DebugLevel.Warning, Color.Yellow },
            { DebugLevel.Error, Color.Orange },
            { DebugLevel.Fatal, Color.Red },
            { DebugLevel.Other, Color.White },
        };

        public DebugPrint(string debugText, DebugLevel debugLevel)
        {
            _debugText = debugText;
            _debugLevel = debugLevel;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float opacity)
        {
            spriteBatch.DrawString(Game1.fonts["normal_font"], _debugText, position, _debugColors[_debugLevel] * opacity, 0f, Vector2.Zero, 1 / scale, SpriteEffects.None, 0f);
        }
    }

    public class DebugManager
    {
        private InputManager _inputManager;
        private static int _maxDebugPrintsOnScreen = 20;
        private static Vector2 cornerBuffer = new Vector2(4, -2);
        private bool _debugActive { get { return _currentDebugLevel != DebugLevel.DebugOff; } }
        private List<DebugPrint> _debugPrints;
        private ViewManager _viewManager;
        private DebugLevel _currentDebugLevel;

        public DebugManager(ViewManager viewManager)
        {
            _currentDebugLevel = DebugLevel.DebugOff;

            _viewManager = viewManager;
            _inputManager = new InputManager();
            _inputManager.AddInputAndSingleShotMethod(Keys.G, CycleDebug);
            _debugPrints = new List<DebugPrint>();
        }

        public void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_debugActive)
            {
                // We don't want to print more messages than we have
                int messagesToPrint = Math.Min(_debugPrints.Count, _maxDebugPrintsOnScreen);

                for (int idx = 0; idx < messagesToPrint; idx++)
                {
                    float opacity = 1.0f;
                    if (idx > _maxDebugPrintsOnScreen - 5)
                    {
                        opacity = (_maxDebugPrintsOnScreen - idx) / 5.0f;
                    }
                    Vector2 position = _viewManager.bottomLeft + cornerBuffer - new Vector2(0, (1 + idx) * Game1.fonts["normal_font"].LineHeight / DebugPrint.scale);
                    _debugPrints[_debugPrints.Count - 1 - idx].Draw(spriteBatch, position, opacity);
                }
            }
        }

        public void CycleDebug()
        {
            if (_currentDebugLevel == DebugLevel.DebugOff)
            {
                _currentDebugLevel = DebugLevel.Debug;
            }
            else
            {
                _currentDebugLevel++;
            }
            AddDebugMessage("Changed debug level to " + Enum.GetName(typeof(DebugLevel), _currentDebugLevel), DebugLevel.Other);
        }

        public void AddDebugMessage(string message, DebugLevel debugLevel)
        {
            if (debugLevel >= _currentDebugLevel)
            {
                _debugPrints.Add(new DebugPrint(message, debugLevel));
            }
        }
    }
}
