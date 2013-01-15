using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class TetriP : ATetrimino
    {
        public TetriP():base(SpriteManager.ESprite.P)
        {
            orientations.Add(pos2);
            orientations.Add(pos3);
            orientations.Add(pos4);
        }

        public void  pos3()
        {
            shape[0].setPosition(0, 2);
            shape[1].setPosition(1, 0);
            shape[2].setPosition(1, 1);
            shape[3].setPosition(1, 2);
        }

        public void pos4()
        {
            shape[0].setPosition(0, 0);
            shape[1].setPosition(0, 1);
            shape[2].setPosition(1, 1);
            shape[3].setPosition(2, 1);
        }

        public override void pos1()
        {
            shape[1].setPosition(1, 0);
            shape[2].setPosition(1, 1);
            shape[3].setPosition(1, 2);
            shape[0].setPosition(2, 0);
        }

        public void pos2()
        {
            shape[0].setPosition(0, 1);
            shape[1].setPosition(1, 1);
            shape[2].setPosition(2, 1);
            shape[3].setPosition(2, 2);
        }
    }
}
