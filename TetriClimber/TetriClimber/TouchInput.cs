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
                {EGameMode.SOLO, new TouchRec(states[EGameMode.SOLO], new Rectangle(0, 0, 1920, 1080))}, // NEED TO PUT THE REAL VALUES OF THE BOARD
                {EGameMode.MULTI1P, new TouchRec(states[EGameMode.MULTI1P], new Rectangle())},
                {EGameMode.MULTI2P, new TouchRec(states[EGameMode.MULTI2P], new Rectangle())}
            };
            tapedPoint = Point.Zero;
            isTaped = false;
        }

        public void recenterStartingPoint(int blocks, EGameMode e = EGameMode.SOLO)
        {
            screenParts[e].RecenterStartingPos((int)(blocks * Constants.Measures.blockSize * Constants.Measures.Scale));
        }

        public Point getPointTaped()
        {
            return tapedPoint;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!isTaped) // IN ORDER TO PASS AT LEAST ONE UPDATE TURN
                tapedPoint = Point.Zero;
            else
                isTaped = false;
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                screenParts[id].update();
        }

       
        public void Move(Object sender, TouchEventArgs e)
        {
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                screenParts[id].Move(e.TouchPoint);
        }

        public void Down(Object sender, TouchEventArgs e)
        {
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                if (screenParts[id].boundaries.Contains((int)e.TouchPoint.CenterX, (int)e.TouchPoint.CenterY))
                    screenParts[id].Down(e.TouchPoint);
        }

        public void Up(Object sender, TouchEventArgs e)
        {
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                screenParts[id].Up(e.TouchPoint);
        }

        public void Tap(Object sender, TouchEventArgs e)
        {
            foreach (EGameMode id in Enum.GetValues(typeof(EGameMode)))
                if (screenParts[id].boundaries.Contains((int)e.TouchPoint.CenterX, (int)e.TouchPoint.CenterY)) // THIS IF IS NOT NEEDED NORMALY
                    screenParts[id].Tap(e.TouchPoint);
            isTaped = true;
            tapedPoint = new Point((int)e.TouchPoint.CenterX, (int)e.TouchPoint.CenterY);
        }


        public bool pointTaped
        {
            get
            {
                return (!tapedPoint.Equals(Point.Zero));
            }
        }
    }
}
