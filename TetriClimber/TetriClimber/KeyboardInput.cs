using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class KeyboardInput : AUserInput
    {
        private Dictionary<EInput, Keys> inputs;

        public KeyboardInput() : base()
        {
            inputs = new Dictionary<EInput, Keys>();
            inputs.Add(EInput.UP, Keys.Up);
            inputs.Add(EInput.DOWN, Keys.Down);
            inputs.Add(EInput.LEFT, Keys.Left);
            inputs.Add(EInput.RIGHT, Keys.Right);
            inputs.Add(EInput.TAP, Keys.Space);
            inputs.Add(EInput.ENTER, Keys.Enter);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (KeyValuePair<EInput, Keys> input in inputs)
            {
                if (Keyboard.GetState().IsKeyDown(input.Value))
                    state[input.Key] += gameTime.ElapsedGameTime;
                else
                    state[input.Key] = TimeSpan.Zero;
            }
        }
    }
}
