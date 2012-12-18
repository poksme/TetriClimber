using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public abstract class AUserInput
    {
        public enum EInput {UP, DOWN, LEFT, RIGHT, TAP}
        protected Dictionary<EInput, bool> state;

        public AUserInput()
        {
            state = new Dictionary<EInput, bool>();
            state.Add(EInput.UP, false);
            state.Add(EInput.DOWN, false);
            state.Add(EInput.LEFT, false);
            state.Add(EInput.RIGHT, false);
            state.Add(EInput.TAP, false);
        }

        public bool getState(EInput e)
        {
            return state[e];
        }

        public abstract void begin();
        protected abstract void update();
        public abstract void end();
    }
}
