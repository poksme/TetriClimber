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
        Dictionary<EInput, bool> state;

        public AUserInput()
        {
            state = new Dictionary<EInput, bool>();
            fillState();
        }

        public bool getState(EInput e)
        {
            return state[e];
        }

        public abstract void begin();
        protected abstract void fillState();
        public abstract void end();
    }
}
