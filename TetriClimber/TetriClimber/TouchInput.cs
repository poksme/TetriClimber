using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Core;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class TouchInput : AUserInput
    {
        Vector2 newPos;
        Vector2 oldPos;
        bool move = false;

        public TouchInput():base()
        {
            newPos = Vector2.Zero;
            oldPos = Vector2.Zero;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
             foreach (EInput e in Enum.GetValues(typeof(EInput)))
                state[e] = TimeSpan.Zero;
             if (move && newPos.X > oldPos.X && newPos.X - oldPos.X > newPos.Y - oldPos.Y)
                 state[EInput.RIGHT] = TimeSpan.Zero;
             else if (move && newPos.X < oldPos.X && oldPos.X - newPos.X > oldPos.Y - newPos.Y)
                 state[EInput.LEFT] = TimeSpan.Zero;
             else if (move && newPos.Y > oldPos.Y)
                 state[EInput.DOWN] = TimeSpan.Zero;
        }

       
        public void Move(Object sender, TouchEventArgs e)
        {
            if (move == false)
            {
                oldPos.X = e.TouchPoint.X;
                oldPos.Y = e.TouchPoint.Y;
            }
            newPos.X = e.TouchPoint.X;
            newPos.Y = e.TouchPoint.Y;
            move = true;
        }

        public void Up(Object sender, TouchEventArgs e)
        {
            move = false;

        }
    }
}
