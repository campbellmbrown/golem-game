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

        private List<InputAndOutput> _inputsAndOutputs;
        private List<InputAndSingleShotOutput> _inputsAndSingleShotOutputs;

        public class InputAndOutput
        {
            public Keys inputKey;
            public MethodDelegate outputFunc;

            public InputAndOutput(Keys inputKey, MethodDelegate outputFunc)
            {
                this.inputKey = inputKey;
                this.outputFunc = outputFunc;
            }
        }

        public class InputAndSingleShotOutput
        {
            public Keys inputKey;
            public MethodDelegate outputFunc;
            public bool keyHeldDown;

            public InputAndSingleShotOutput(Keys inputKey, MethodDelegate outputFunc)
            {
                this.inputKey = inputKey;
                this.outputFunc = outputFunc;
                keyHeldDown = false;
            }
        }

        public InputManager()
        {
            _inputsAndOutputs = new List<InputAndOutput>();
            _inputsAndSingleShotOutputs = new List<InputAndSingleShotOutput>();
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

            // Similar for single-shot methods, but instead keep track
            // if it was pressed previously so we don't execute a
            // function more than once per key-press.
            for (int idx = 0; idx < _inputsAndSingleShotOutputs.Count; idx++)
            {
                if (keyboardState.IsKeyDown(_inputsAndSingleShotOutputs[idx].inputKey))
                {
                    if (!_inputsAndSingleShotOutputs[idx].keyHeldDown)
                    {
                        _inputsAndSingleShotOutputs[idx].outputFunc();
                    }
                    _inputsAndSingleShotOutputs[idx].keyHeldDown = true;
                }
                else _inputsAndSingleShotOutputs[idx].keyHeldDown = false;
            }
        }

        public void AddInputAndMethod(Keys inputKey, MethodDelegate func)
        {
            _inputsAndOutputs.Add(new InputAndOutput(inputKey, func));
        }

        public void AddInputAndSingleShotMethod(Keys inputKey, MethodDelegate func)
        {
            _inputsAndSingleShotOutputs.Add(new InputAndSingleShotOutput(inputKey, func));
        }
    }
}
