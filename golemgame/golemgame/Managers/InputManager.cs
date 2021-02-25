using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class InputManager
    {
        public delegate void MethodDelegate();
        private List<InputAndOutput> _inputsAndOutputs { get; set; }

        private struct InputAndOutput
        {
            public Keys inputKey;
            public MethodDelegate outputFunc;

            public InputAndOutput(Keys inputKey, MethodDelegate outputFunc) : this()
            {
                this.inputKey = inputKey;
                this.outputFunc = outputFunc;
            }
        }

        public InputManager()
        {
            _inputsAndOutputs = new List<InputAndOutput>();
        }

        public void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();

            // For each stored input, check if this key is pressed,
            // then execute complete corresponding action
            foreach (var inputAndOutput in _inputsAndOutputs)
            {
                if (keyboardState.IsKeyDown(inputAndOutput.inputKey))
                {
                    inputAndOutput.outputFunc();
                }
            }
        }

        public void AddInputAndMethod(Keys inputKey, MethodDelegate func)
        {
            _inputsAndOutputs.Add(new InputAndOutput(inputKey, func));
        }
    }
}
