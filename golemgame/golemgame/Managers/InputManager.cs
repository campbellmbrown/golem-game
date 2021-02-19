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
        private List<InputAndOutput> inputsAndOutputs { get; set; }

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
            inputsAndOutputs = new List<InputAndOutput>();
        }

        public void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (var inputAndOutput in inputsAndOutputs)
            {
                if (keyboardState.IsKeyDown(inputAndOutput.inputKey))
                {
                    inputAndOutput.outputFunc();
                }
            }
        }

        public void AddInputAndMethod(Keys inputKey, MethodDelegate func)
        {
            inputsAndOutputs.Add(new InputAndOutput(inputKey, func));
        }
    }
}
