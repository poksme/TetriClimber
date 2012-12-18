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

        public KeyboardInput() : base()
        {
            oldState = Keyboard.GetState();
        }

        public override void begin()
        {
            newState = Keyboard.GetState();
        }

        public override void end()
        {
            oldState = newState;
        }
    }
}
