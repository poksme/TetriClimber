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
        // TO DO USE TIMESPANS
        private Dictionary<EInput, Tuple<Keys, TimeSpan>> inputs;
        private TimeSpan KeyRepeatTime;

        public KeyboardInput() : base()
        {
            inputs = new Dictionary<EInput, Tuple<Keys, TimeSpan>>();
            inputs.Add(EInput.UP, new Tuple<Keys, TimeSpan>(Keys.Up, TimeSpan.Zero));
            inputs.Add(EInput.DOWN, new Tuple<Keys, TimeSpan>(Keys.Down, TimeSpan.Zero));
            inputs.Add(EInput.LEFT, new Tuple<Keys, TimeSpan>(Keys.Left, TimeSpan.Zero));
            inputs.Add(EInput.RIGHT, new Tuple<Keys, TimeSpan>(Keys.Right, TimeSpan.Zero));
            inputs.Add(EInput.TAP, new Tuple<Keys, TimeSpan>(Keys.Space, TimeSpan.Zero));
            inputs.Add(EInput.ENTER, new Tuple<Keys, TimeSpan>(Keys.Enter, TimeSpan.Zero));
            KeyRepeatTime = new TimeSpan(2000000);            
        }

        public void setKeyRepeatTime(TimeSpan t)
        {
            KeyRepeatTime = t;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (EInput ipt in Enum.GetValues(typeof(EInput)))
            {
                if (Keyboard.GetState().IsKeyDown(inputs[ipt].Item1))
                {
                    if (inputs[ipt].Item2 == TimeSpan.Zero)
                        state[ipt] = true;
                    inputs[ipt] = new Tuple<Keys, TimeSpan>(inputs[ipt].Item1, inputs[ipt].Item2 + gameTime.ElapsedGameTime);
                }
                else
                    inputs[ipt] = new Tuple<Keys, TimeSpan>(inputs[ipt].Item1, TimeSpan.Zero);
                if (inputs[ipt].Item2 > KeyRepeatTime)
                {
                    inputs[ipt] = new Tuple<Keys, TimeSpan>(inputs[ipt].Item1, TimeSpan.Zero);
                    //state[ipt] = true;
                }
                //state[input.Key] += gameTime.ElapsedGameTime;
                //else
                //    state[input.Key] = false;
                    //state[input.Key] = TimeSpan.Zero;
            }
        }
    }
}
