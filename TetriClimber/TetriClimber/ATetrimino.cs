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
        private     Vector2 posRel;
        public CoordHelper.EProfile PlayerType { get; private set; }

        public Vector2 PosRel
        {
            get { return posRel; }
            set { posRel = value; }
        }

        public void copyOrientation(ATetrimino o)
        {
            orientation = o.orientation;
            orientations[orientation].Invoke();
            foreach (Block b in shape)
                b.setOrientation(orientation);
        }

        public ATetrimino(SpriteManager.ESprite color, CoordHelper.EProfile pt, float transparency = 1f, bool shadow = false) : base(App.Game)
        {
            PlayerType = pt;
            orientation = 0;
            posRel = new Vector2(3, -3);
            orientations = new List<Action>();
            orientations.Add(pos1);
            shape = new List<Block>();
            shape.Add(new Block((shadow ? SpriteManager.ESprite.NONE : color), this, transparency));
            shape.Add(new Block((shadow ? SpriteManager.ESprite.NONE : color), this, transparency));
            shape.Add(new Block((shadow ? SpriteManager.ESprite.NONE : color), this, transparency));
            shape.Add(new Block((shadow ? SpriteManager.ESprite.NONE : color), this, transparency));
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

        public void leftMove()
        {
            posRel.X--;
        }

        public void rightMove()
        {
            posRel.X++;
        }

        public void downMove()
        {
            posRel.Y++;
        }

        public void upMove()
        {
            posRel.Y--;
        }

        public bool overlap(Board board)
        {
            foreach (Block b in shape)
                if (board.isOutOfBond(new Vector2(PosRel.X + b.PosRel.X, PosRel.Y + b.PosRel.Y)) ||
                    board.isBusyCase(new Vector2(PosRel.X + b.PosRel.X, PosRel.Y + b.PosRel.Y)))
                    return true;
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (Block b in shape)
                b.Draw(gameTime);
        }

        public void DrawAsPreview(GameTime gameTime)
        {
            foreach (Block b in shape)
                b.DrawAsPreview(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public abstract void pos1();

        public List<Block> getBlocks()
        {
            return shape;
        }

        public void DownOneStep()
        {
            posRel.Y++;
            foreach (Block b in shape)
            {
                Vector2 pos = b.PosAbs;
                pos.Y += Constants.Measures.blockSize;
            }
        }

        public Block getMostRightBlock()
        {
            Block max = shape[0];
            foreach (Block b in shape)
            {
                if (max.PosRel.X < b.PosRel.X)
                    max = b;
            }
            return max;
        }

        public Block getMostLeftBlock()
        {
            Block max = shape[0];
            foreach (Block b in shape)
            {
                if (max.PosRel.X > b.PosRel.X)
                    max = b;
            }
            return max;
        }

        public Block getMostDownBlock()
        {
            Block max = shape[0];
            foreach (Block b in shape)
            {
                if (max.PosRel.Y > b.PosRel.Y)
                    max = b;
            }
            return max;
        }
    }
}
