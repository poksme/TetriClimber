﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class TetriZ : ATetrimino
    {
        public TetriZ(CoordHelper.EProfile pt, float transparency = 1f, bool shadow = false):base(SpriteManager.ESprite.Z, pt, transparency, shadow)
        {
            orientations.Add(pos2);
        }

        public void  pos2()
        {
            shape[0].setPosition(2, 0);
            shape[1].setPosition(1, 1);
            shape[2].setPosition(2, 1);
            shape[3].setPosition(1, 2);
        }

        public override void pos1()
        {
            shape[0].setPosition(0, 1);
            shape[1].setPosition(1, 1);
            shape[2].setPosition(1, 2);
            shape[3].setPosition(2, 2);
        }
    }
}
