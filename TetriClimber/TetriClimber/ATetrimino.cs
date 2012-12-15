using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class ATetrimino
    {
        protected   List<Action> orientations = null;
        protected   List<Block> shape = null;
        private     int orientation;
        protected   SpriteManager.ESprite color;
        protected   Vector2 position;

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
            orientations[orientation].Invoke();
        }

        public void rightShift()
        {
            orientation = (orientation + 1) % orientations.Count;
            orientations[orientation].Invoke();
        }

        public void leftShift()
        {
            orientation = (orientation - 1 >= 0) ? (orientation - 1) : (orientations.Count - 1);
            orientations[orientation].Invoke();
        }

        public abstract void pos1();
    }
}
