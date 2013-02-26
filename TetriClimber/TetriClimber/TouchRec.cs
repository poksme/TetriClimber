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
        public Point tapedPoint     { get; private set; }
        private bool dropedDown;
        private bool taped;

        public TouchRec(Dictionary<AUserInput.EInputKeys, bool> inputs, Rectangle boundaries = new Rectangle())
        {
            this.boundaries = boundaries;
            this.hasActiveId = false;
            this.inputs = inputs;
            this.dropedDown = false;
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
            if (!hasActiveId || activeId != tp.Id)
                return;
            actualPos = new Vector2(tp.CenterX, tp.CenterY);
        }

        public void Tap(TouchPoint tp)
        {
            if (!hasActiveId || activeId != tp.Id)
                return;
            tapedPoint = new Point((int)tp.CenterX, (int)tp.CenterY);
            taped = true;
        }

        public void Up(TouchPoint tp)
        {
            if (actualPos.Y >= startingPos.Y + Constants.Measures.blockSize && hasActiveId && activeId == tp.Id)
                dropedDown = true;
            hasActiveId = false;
            //if (actualPos.Y >= startingPos.Y + Constants.Measures.blockSize)
            //    dropedDown = true;
        }

        public void RecenterStartingPos(int padding)
        {
            startingPos.X += padding;
        }

        public void update()
        {
            if (hasActiveId && startingPos.X + Constants.Measures.blockSize * Constants.Measures.Scale < actualPos.X)
                inputs[AUserInput.EInputKeys.RIGHT] = true;
            else if (hasActiveId && startingPos.X - Constants.Measures.blockSize * Constants.Measures.Scale > actualPos.X)
                inputs[AUserInput.EInputKeys.LEFT] = true;
            if (dropedDown)
                inputs[AUserInput.EInputKeys.SPACE_BAR] = true;
            if (taped)
                inputs[AUserInput.EInputKeys.DOWN] = true;

            // RESET HANDLERS VARS
            dropedDown = false;
            taped = false;
        }
    }
}
