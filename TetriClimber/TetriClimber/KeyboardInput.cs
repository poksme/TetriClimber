using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class KeyboardInput : AUserInput
    {
        private KeyboardState newState;
        private KeyboardState oldState;
        private Dictionary<EInput, Keys> inputs;

        public KeyboardInput() : base()
        {
            oldState = Keyboard.GetState();
            inputs.Add(EInput.UP, Keys.Up);
            inputs.Add(EInput.DOWN, Keys.Down);
            inputs.Add(EInput.LEFT, Keys.Left);
            inputs.Add(EInput.RIGHT, Keys.Right);
            inputs.Add(EInput.TAP, Keys.Space);
        }

        public override void update()
        {
            newState = Keyboard.GetState();
            foreach (KeyValuePair<EInput, Keys> input in inputs)
            {
                if (newState.IsKeyDown(input.Value) && !oldState.IsKeyDown(input.Value))
                    state[input.Key] = true;
                else
                    state[input.Key] = false;
            }
            oldState = newState;
        }
    }
}
