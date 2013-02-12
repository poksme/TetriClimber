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
        //protected Dictionary<EInput, TimeSpan> state;
        protected Dictionary<EInput, bool> state;

        public AUserInput():base(App.Game)
        {
            //state = new Dictionary<EInput, TimeSpan>();
            //state.Add(EInput.UP,    TimeSpan.Zero);
            //state.Add(EInput.DOWN,  TimeSpan.Zero);
            //state.Add(EInput.LEFT,  TimeSpan.Zero);
            //state.Add(EInput.RIGHT, TimeSpan.Zero);
            //state.Add(EInput.TAP, TimeSpan.Zero);
            //state.Add(EInput.ENTER, TimeSpan.Zero);

            state = new Dictionary<EInput, bool>();
            state.Add(EInput.UP, false);
            state.Add(EInput.DOWN, false);
            state.Add(EInput.LEFT, false);
            state.Add(EInput.RIGHT, false);
            state.Add(EInput.TAP, false);
            state.Add(EInput.ENTER, false);
        }

        public bool isPressed(EInput e)
        {
            return state[e];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // RESET ALL INPUTS
            foreach (EInput ipt in Enum.GetValues(typeof(EInput)))
                state[ipt] = false;
        }
    }
}
