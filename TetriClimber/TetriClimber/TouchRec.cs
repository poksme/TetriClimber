using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Surface.Core;

namespace TetriClimber
{
    class TouchRec
    {
        public Rectangle boundaries { get; private set; }
        private  Dictionary<AUserInput.EInputKeys, bool> inputs;
        private int activeId;
        private bool hasActiveId;
        private Vector2 startingPos;
        private Vector2 actualPos;
        private Vector2 prevPos;
        public Point tapedPoint     { get; private set; }
        //private bool dropedDown;
        private bool taped;


        public TouchRec(Dictionary<AUserInput.EInputKeys, bool> inputs, Rectangle boundaries = new Rectangle())
        {
            this.boundaries = boundaries;
            this.hasActiveId = false;
            this.inputs = inputs;
            //this.dropedDown = false;
            this.prevPos = Vector2.Zero;
        }

        public void Down(TouchPoint tp)
        {
            if (hasActiveId)
                return;
            hasActiveId = true;
            activeId = tp.Id;
            startingPos = new Vector2(tp.CenterX, tp.CenterY);
            actualPos = new Vector2(tp.CenterX, tp.CenterY);
        }

        public void Move(TouchPoint tp)
        {
            if (hasActiveId && activeId == tp.Id)
                actualPos = new Vector2(tp.CenterX, tp.CenterY);
        }

        public void Tap(TouchPoint tp)
        {
            if (hasActiveId && activeId == tp.Id)
            {
                tapedPoint = new Point((int)tp.CenterX, (int)tp.CenterY);
                taped = true;
            }
        }

        public void Up(TouchPoint tp)
        {
            if (hasActiveId && activeId == tp.Id)
            {
                //if (actualPos.Y >= startingPos.Y + Constants.Measures.blockSize && hasActiveId && activeId == tp.Id)
                //    dropedDown = true;
                hasActiveId = false;
                prevPos = Vector2.Zero;
            }
        }

        public void RecenterStartingPos(int padding)
        {
            startingPos.X += padding;
        }

        public void update(GameTime gt)
        {
            float speed = (actualPos.Y - prevPos.Y) / gt.ElapsedGameTime.Milliseconds;
            if (hasActiveId && startingPos.X + Constants.Measures.blockSize * Constants.Measures.Scale < actualPos.X)
                inputs[AUserInput.EInputKeys.RIGHT] = true;
            else if (hasActiveId && startingPos.X - Constants.Measures.blockSize * Constants.Measures.Scale > actualPos.X)
                inputs[AUserInput.EInputKeys.LEFT] = true;
            if (hasActiveId && prevPos != Vector2.Zero && speed > 1)
                inputs[AUserInput.EInputKeys.SPACE_BAR] = true;
            //if (dropedDown)
            //    inputs[AUserInput.EInputKeys.SPACE_BAR] = true;
            if (taped)
                inputs[AUserInput.EInputKeys.DOWN] = true;

            // RESET HANDLERS VARS
            //dropedDown = false;
            taped = false;
            if (hasActiveId)
            {
                prevPos.X = actualPos.X;
                prevPos.Y = actualPos.Y;
            }
        }
    }
}
