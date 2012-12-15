using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class TetriS : ATetrimino
    {
        public TetriS():base()
        {
            color = SpriteManager.ESprite.S;
            orientations.Add(pos2);
        }

        public override void  pos1()
        {
            shape[0].setPosition(0, 2);
            shape[1].setPosition(1, 1);
            shape[2].setPosition(1, 2);
            shape[3].setPosition(2, 1);
        }

        public void pos2()
        {
            shape[0].setPosition(1, 0);
            shape[1].setPosition(1, 1);
            shape[2].setPosition(2, 1);
            shape[3].setPosition(2, 2);
        }
    }
}
