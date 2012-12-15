using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class ATetrimino
    {
        List<Action> orientations = null;
        List<Block> shape = null;
        int orientation;
        SpriteManager.ESprite color;
        Vector2 position;

        public ATetrimino()
        {
            orientation = 0;
            Vector2 position = new Vector2(3, -1);
            orientations = new List<Action>();
            orientations.Add(pos1);
            shape = new List<Block>();
            shape.Add(new Block());
            shape.Add(new Block());
            shape.Add(new Block());
            shape.Add(new Block());
        }

        public abstract void pos1();
    }
}
