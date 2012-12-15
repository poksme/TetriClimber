using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Block
    {
        Vector2 pos = Vector2.Zero;

        public Block()
        {
        }

        public void setPosition(Vector2 coord)
        {
            pos = coord;
        }

        public void setPosition(int x, int y)
        {
            pos = new Vector2((float)x, (float)y);
        }
    }
}
