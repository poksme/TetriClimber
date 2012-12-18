using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Core;

namespace TetriClimber
{
    class TouchInput : AUserInput
    {
        TouchTarget touch;

        public TouchInput():base()
        {
            touch = App.Game.TouchTarget;
        }

        public override void begin()
        {   
            
        }

        public override void update()
        {
            ReadOnlyTouchPointCollection touches = touch.GetState();
           // if ();
        }

        public override void end()
        {

        }
    }
}
