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
        private Dictionary<EGameMode, Dictionary<EInputKeys, Tuple<Keys, TimeSpan>>> inputs;
        public TimeSpan KeyRepeatTime { private get; set; }

        public KeyboardInput() : base()
        {
            inputs = new Dictionary<EGameMode, Dictionary<EInputKeys, Tuple<Keys, TimeSpan>>>()
            {
                {EGameMode.SOLO, new Dictionary<EInputKeys, Tuple<Keys, TimeSpan>>()
                {
                    {EInputKeys.UP, new Tuple<Keys, TimeSpan>(Keys.Up, TimeSpan.Zero)},
                    {EInputKeys.DOWN, new Tuple<Keys, TimeSpan>(Keys.Down, TimeSpan.Zero)},
                    {EInputKeys.LEFT, new Tuple<Keys, TimeSpan>(Keys.Left, TimeSpan.Zero)},
                    {EInputKeys.RIGHT, new Tuple<Keys, TimeSpan>(Keys.Right, TimeSpan.Zero)},
                    {EInputKeys.SPACE_BAR, new Tuple<Keys, TimeSpan>(Keys.Space, TimeSpan.Zero)},
                    {EInputKeys.ENTER, new Tuple<Keys, TimeSpan>(Keys.Enter, TimeSpan.Zero)}
                }},
                {EGameMode.MULTI1P, new Dictionary<EInputKeys, Tuple<Keys, TimeSpan>>()
                {
                    {EInputKeys.UP, new Tuple<Keys, TimeSpan>(Keys.W, TimeSpan.Zero)},
                    {EInputKeys.DOWN, new Tuple<Keys, TimeSpan>(Keys.S, TimeSpan.Zero)},
                    {EInputKeys.LEFT, new Tuple<Keys, TimeSpan>(Keys.A, TimeSpan.Zero)},
                    {EInputKeys.RIGHT, new Tuple<Keys, TimeSpan>(Keys.D, TimeSpan.Zero)},
                    {EInputKeys.SPACE_BAR, new Tuple<Keys, TimeSpan>(Keys.Space, TimeSpan.Zero)},
                    {EInputKeys.ENTER, new Tuple<Keys, TimeSpan>(Keys.Enter, TimeSpan.Zero)}
                }},
                {EGameMode.MULTI2P, new Dictionary<EInputKeys, Tuple<Keys, TimeSpan>>()
                {
                    {EInputKeys.UP, new Tuple<Keys, TimeSpan>(Keys.Up, TimeSpan.Zero)},
                    {EInputKeys.DOWN, new Tuple<Keys, TimeSpan>(Keys.Down, TimeSpan.Zero)},
                    {EInputKeys.LEFT, new Tuple<Keys, TimeSpan>(Keys.Left, TimeSpan.Zero)},
                    {EInputKeys.RIGHT, new Tuple<Keys, TimeSpan>(Keys.Right, TimeSpan.Zero)},
                    {EInputKeys.SPACE_BAR, new Tuple<Keys, TimeSpan>(Keys.RightControl, TimeSpan.Zero)},
                    {EInputKeys.ENTER, new Tuple<Keys, TimeSpan>(Keys.Enter, TimeSpan.Zero)}
                }},
            };
            KeyRepeatTime = new TimeSpan(2000000);            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (EGameMode gm in Enum.GetValues(typeof(EGameMode)))
            {
                foreach (EInputKeys ipt in Enum.GetValues(typeof(EInputKeys)))
                {
                    if (Keyboard.GetState().IsKeyDown(inputs[gm][ipt].Item1))
                    {
                        if (inputs[gm][ipt].Item2 == TimeSpan.Zero)
                            states[gm][ipt] = true;
                        inputs[gm][ipt] = new Tuple<Keys, TimeSpan>(inputs[gm][ipt].Item1, inputs[gm][ipt].Item2 + gameTime.ElapsedGameTime);
                    }
                    else
                        inputs[gm][ipt] = new Tuple<Keys, TimeSpan>(inputs[gm][ipt].Item1, TimeSpan.Zero);
                    if (inputs[gm][ipt].Item2 > KeyRepeatTime)
                        inputs[gm][ipt] = new Tuple<Keys, TimeSpan>(inputs[gm][ipt].Item1, TimeSpan.Zero);
                }
            }
        }
    }
}
