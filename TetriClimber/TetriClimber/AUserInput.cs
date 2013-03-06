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
        public enum EInputKeys { UP, DOWN, LEFT, RIGHT, SPACE_BAR, ENTER }
        public enum EInputType { KEYBOARD, TOUCH_SCREEN }
        public enum EGameMode { SOLO, MULTI1P, MULTI2P }
        protected Dictionary<EGameMode, Dictionary<EInputKeys, bool>> states;

        public AUserInput()
            : base(App.Game)
        {
            states = new Dictionary<EGameMode, Dictionary<EInputKeys, bool>>()
            {
                {EGameMode.SOLO, new Dictionary<EInputKeys, bool>()
                {
                    {EInputKeys.UP, false},
                    {EInputKeys.DOWN, false},
                    {EInputKeys.LEFT, false},
                    {EInputKeys.RIGHT, false},
                    {EInputKeys.SPACE_BAR, false},
                    {EInputKeys.ENTER, false}
                }},
                {EGameMode.MULTI1P, new Dictionary<EInputKeys, bool>()
                {
                    {EInputKeys.UP, false},
                    {EInputKeys.DOWN, false},
                    {EInputKeys.LEFT, false},
                    {EInputKeys.RIGHT, false},
                    {EInputKeys.SPACE_BAR, false},
                    {EInputKeys.ENTER, false}
                }},
                {EGameMode.MULTI2P, new Dictionary<EInputKeys, bool>()
                {
                    {EInputKeys.UP, false},
                    {EInputKeys.DOWN, false},
                    {EInputKeys.LEFT, false},
                    {EInputKeys.RIGHT, false},
                    {EInputKeys.SPACE_BAR, false},
                    {EInputKeys.ENTER, false}
                }}
            };
        }

        public bool isPressed(EInputKeys e, EGameMode g)
        {
            return states[g][e];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            reset();
        }

        public void reset()
        {
            foreach (EGameMode gm in Enum.GetValues(typeof(EGameMode)))
            {
                foreach (EInputKeys ipt in Enum.GetValues(typeof(EInputKeys)))
                    states[gm][ipt] = false;
            }
        }
    }
}
