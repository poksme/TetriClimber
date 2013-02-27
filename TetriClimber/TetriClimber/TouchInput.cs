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
        private Dictionary<EGameMode, TouchRec> screenParts;
        public Point tapedPoint { get; private set; }
        private bool isTaped;

        public TouchInput():base()
        {
            screenParts = new Dictionary<EGameMode, TouchRec>()
            {
                {EGameMode.SOLO, new TouchRec(states[EGameMode.SOLO], new Rectangle((int)CoordHelper.Instance.getLeftMargin(EGameMode.SOLO), (int)Constants.Measures.upBoardMargin, (int)Constants.Measures.boardWidth, (int)Constants.Measures.boardHeight))}, // NEED TO PUT THE REAL VALUES OF THE BOARD
                {EGameMode.MULTI1P, new TouchRec(states[EGameMode.MULTI1P], new Rectangle((int)CoordHelper.Instance.getLeftMargin(EGameMode.MULTI1P), (int)Constants.Measures.upBoardMargin, (int)Constants.Measures.boardWidth, (int)Constants.Measures.boardHeight))},
                {EGameMode.MULTI2P, new TouchRec(states[EGameMode.MULTI2P], new Rectangle((int)CoordHelper.Instance.getLeftMargin(EGameMode.MULTI2P), (int)Constants.Measures.upBoardMargin, (int)Constants.Measures.boardWidth, (int)Constants.Measures.boardHeight))}
            };
            tapedPoint = Point.Zero;
            isTaped = false;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //UPDATE ALL SCREEN PARTS
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                screenParts[id].update();

            //UPDATE TAP POSITION FOR UI
            if (!isTaped) // IN ORDER TO PASS AT LEAST ONE UPDATE TURN
                tapedPoint = Point.Zero;
            else
                isTaped = false;
        }

       
        public void Move(Object sender, TouchEventArgs e)
        {
            if (e.TouchPoint.IsFingerRecognized)
            {
                foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                    screenParts[id].Move(e.TouchPoint);
            }
        }

        public void Down(Object sender, TouchEventArgs e)
        {
            if (e.TouchPoint.IsFingerRecognized)
            {
                foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                    if (screenParts[id].boundaries.Contains((int)e.TouchPoint.CenterX, (int)e.TouchPoint.CenterY))
                        screenParts[id].Down(e.TouchPoint);
            }
        }

        public void Up(Object sender, TouchEventArgs e)
        {
            if (e.TouchPoint.IsFingerRecognized)
            {
                foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                    screenParts[id].Up(e.TouchPoint);
            }
        }

        public void Tap(Object sender, TouchEventArgs e)
        {
            if (e.TouchPoint.IsFingerRecognized)
            {
                foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                    screenParts[id].Tap(e.TouchPoint);
                isTaped = true;
                tapedPoint = new Point((int)e.TouchPoint.CenterX, (int)e.TouchPoint.CenterY);
            }
        }

        public void recenterStartingPoint(int blocks, EGameMode e)
        {
            screenParts[e].RecenterStartingPos((int)(blocks * Constants.Measures.blockSize * Constants.Measures.Scale));
        }

        public bool hasTapEvent { get { return (!tapedPoint.Equals(Point.Zero)); } }
        public Point PointTaped { get { return tapedPoint; } }
    }
}
