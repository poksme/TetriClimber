using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public abstract class AUserInput : GameComponent
    {
        public enum EInput {UP, DOWN, LEFT, RIGHT, TAP, ENTER}
        public enum EType {KEYBOARD, TOUCH}
        protected Dictionary<EInput, TimeSpan> state;

        public AUserInput():base(App.Game)
        {
            state = new Dictionary<EInput, TimeSpan>();
            state.Add(EInput.UP,    TimeSpan.Zero);
            state.Add(EInput.DOWN,  TimeSpan.Zero);
            state.Add(EInput.LEFT,  TimeSpan.Zero);
            state.Add(EInput.RIGHT, TimeSpan.Zero);
            state.Add(EInput.TAP, TimeSpan.Zero);
            state.Add(EInput.ENTER, TimeSpan.Zero);
        }

        public TimeSpan getDownTime(EInput e)
        {
            return state[e];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
