using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public abstract class ATetrimino : DrawableGameComponent
    {
        protected   List<Action> orientations = null;
        protected   List<Block> shape = null;
        private     int orientation;
        protected   Vector2 position;

        public ATetrimino(SpriteManager.ESprite color) : base(App.Game)
        {
            orientation = 0;
            Vector2 position = new Vector2(3, -1);
            orientations = new List<Action>();
            orientations.Add(pos1);
            shape = new List<Block>();
            shape.Add(new Block(color));
            shape.Add(new Block(color));
            shape.Add(new Block(color));
            shape.Add(new Block(color));
            orientations[orientation].Invoke();
        }

        public void rightShift()
        {
            orientation = (orientation + 1) % orientations.Count;
            orientations[orientation].Invoke();
            foreach (Block b in shape)
                b.setOrientation(orientation);
        }

        public void leftShift()
        {
            orientation = (orientation - 1 >= 0) ? (orientation - 1) : (orientations.Count - 1);
            orientations[orientation].Invoke();
            foreach (Block b in shape)
                b.setOrientation(orientation);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (Block b in shape)
                b.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public abstract void pos1();
    }
}
