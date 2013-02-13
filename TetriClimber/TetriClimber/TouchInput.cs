using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Core;
using Microsoft.Xna.Framework;
using Microsoft.Surface;

namespace TetriClimber
{
    public class TouchInput : AUserInput
    {
        //public Vector2 NewPos { get { return this.newPos; } }
        //Vector2 newPos;
        //Vector2 oldPos;
        private Vector2 startingPos;
        public Vector2 StartingPos 
        {
            get {
                return CoordHelper.Instance.Replace(Vector2.Transform(startingPos, Matrix.Invert(Matrix.CreateRotationZ(MathHelper.ToRadians(-90)))));
            }
        }
        Vector2 actualPos;
        Point tapedPoint;
        bool dropedDown;
        bool taped;
        //bool move = false;
        //bool tap = false;

        public TouchInput():base()
        {
            //newPos = Vector2.Zero;
            //oldPos = Vector2.Zero;
            startingPos = Vector2.Zero;
            actualPos = Vector2.Zero;
            dropedDown = false;
            taped = false;
            tapedPoint = Point.Zero;
        }

        public void recenterStartingPoint(int blocks)
        {
            //Console.Out.WriteLine("RECENTER FUNC");
            startingPos.X += blocks * Constants.Measures.blockSize * Constants.Measures.Scale;
        }

        public Point getPointTaped(bool turned = false)
        {
            return CoordHelper.Instance.Replace(tapedPoint);
        }

        public float getDropDownDistance()
        {
            return (actualPos.Y - startingPos.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (startingPos.X + Constants.Measures.blockSize * Constants.Measures.Scale < actualPos.X)
            {
                //Console.Out.WriteLine("ActualPos: " + actualPos + " StartPos: " + startingPos);
                state[EInput.RIGHT] = true;
            }
            else if (startingPos.X - Constants.Measures.blockSize * Constants.Measures.Scale > actualPos.X)
                state[EInput.LEFT] = true;
            if (dropedDown)
                state[EInput.TAP] = true;
            if (taped)
                state[EInput.DOWN] = true;
            dropedDown = false;
            taped = false;
            // if (move && newPos.X > oldPos.X + 10 && newPos.X - oldPos.X > newPos.Y - oldPos.Y)
            //     state[EInput.RIGHT] += gameTime.ElapsedGameTime;
            // else
            //     state[EInput.RIGHT] = TimeSpan.Zero;
            // if (move && newPos.X < oldPos.X - 10 && oldPos.X - newPos.X > oldPos.Y - newPos.Y)
            //     state[EInput.LEFT] += gameTime.ElapsedGameTime;
            // else
            //     state[EInput.LEFT] = TimeSpan.Zero;
            // if (move && newPos.Y > oldPos.Y + 10)
            //     state[EInput.TAP] += gameTime.ElapsedGameTime;
            // else
            //     state[EInput.TAP] = TimeSpan.Zero;

            //if (!move && tap)
            // {
            //     state[EInput.DOWN] += gameTime.ElapsedGameTime;
            //     tap = false;
            // }
            // else
            //     state[EInput.DOWN] = TimeSpan.Zero;
        }

       
        public void Move(Object sender, TouchEventArgs e)
        {
            //Console.Out.WriteLine("MOVE FUNC");
            Vector2 tp  = new Vector2(e.TouchPoint.X, e.TouchPoint.Y);

            if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
                actualPos = Vector2.Transform(tp, Matrix.CreateRotationZ(MathHelper.ToRadians(-90)));
            else
            {
                actualPos.X = e.TouchPoint.X;
                actualPos.Y = e.TouchPoint.Y;
            }
            if (startingPos == Vector2.Zero)
            {
                startingPos.X = actualPos.X;
                startingPos.Y = actualPos.Y;
            }

            //if (move == false)
            //{
            //    if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
            //       oldPos = Vector2.Transform(tp, Matrix.CreateRotationZ(MathHelper.ToRadians(-90)));
            //    else
            //    {
            //        oldPos.X = e.TouchPoint.X;
            //        oldPos.Y = e.TouchPoint.Y;
            //    }
            //}
            //if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
            //    newPos = Vector2.Transform(tp, Matrix.CreateRotationZ(MathHelper.ToRadians(-90)));
            //else
            //{
            //    newPos.X = e.TouchPoint.X;
            //    newPos.Y = e.TouchPoint.Y;
            //}
            //move = true;
        }

        public void Up(Object sender, TouchEventArgs e)
        {
            //Console.Out.WriteLine("UP FUNC");
            if (actualPos.Y - Constants.Measures.blockSize * Constants.Measures.Scale * 2 >= startingPos.Y)
                dropedDown = true;
            //taped = Vector2.Distance(actualPos, startingPos) < Constants.Measures.blockSize * Constants.Measures.Scale;
            if (taped)
            {
                tapedPoint.X = (int)actualPos.X;
                tapedPoint.Y = (int)actualPos.Y;
            }
            startingPos = Vector2.Zero;
            actualPos = Vector2.Zero;
        }

        public void Tap(Object sender, TouchEventArgs e)
        {
            //Console.Out.WriteLine("TAP FUNC");
            //newPos.X =  e.TouchPoint.X;
            //newPos.Y =  e.TouchPoint.Y;
            actualPos.X = e.TouchPoint.X;
            actualPos.Y = e.TouchPoint.Y;
            startingPos.X = e.TouchPoint.X;
            startingPos.Y = e.TouchPoint.Y;
            taped = true;
            //tap = true;
        }
    }
}
