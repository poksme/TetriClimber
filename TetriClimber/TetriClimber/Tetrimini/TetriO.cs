using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class TetriO : ATetrimino
    {
        public TetriO():base()
        {
            color = SpriteManager.ESprite.O;
        }

        public override void  pos1()
        {
            shape[0].setPosition(1, 1);
            shape[1].setPosition(1, 2);
            shape[2].setPosition(2, 1);
            shape[3].setPosition(2, 2);
        }
    }
}
