using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Block : DrawableGameComponent
    {
        private Vector2 posRel = Vector2.Zero;
        public Vector2 PosRel { get { return posRel; }}
        private Vector2 posAbs = Vector2.Zero;
        public Vector2 PosAbs { get { return posAbs; } }
        private ATetrimino container;
        private SpriteManager.ESprite color;
        private Rectangle hitBox;

        public SpriteManager.ESprite Color
        {
            get { return color; }
        }

        private float orientation;
        public float Orientation
        {
            get { return orientation; }
        }

        public Block(SpriteManager.ESprite color, ATetrimino cont) : base(App.Game)
        {
            this.color = color;
            container = cont;
            hitBox = Rectangle.Empty;
        }

        public void setPosition(Vector2 coord)
        {
            posRel = coord;
        }

        public void setPosition(int x, int y)
        {
            posRel = new Vector2((float)x, (float)y);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (posRel.X + container.PosRel.X < Constants.Measures.boardBlockWidth && posRel.X + container.PosRel.X >= 0f &&
                posRel.Y + container.PosRel.Y < Constants.Measures.boardBlockHeight && posRel.Y + container.PosRel.Y >= 0f)
                SpriteManager.Instance.drawRotatedAtPos(color, new Vector2(Constants.Measures.leftBoardMargin + (posRel.X + container.PosRel.X) * Constants.Measures.blockSize, Constants.Measures.upBoardMargin + (posRel.Y + container.PosRel.Y) * Constants.Measures.blockSize), orientation, Constants.Measures.blockSize);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void setOrientation(int orientation)
        {
            this.orientation = MathHelper.ToRadians(orientation * 90);
        }

        public void setHitBoxValue(int x, int y, int w = (int)(Constants.Measures.blockSize), int h = (int)(Constants.Measures.blockSize))
        {
            hitBox.X = x;
            hitBox.Y = y;
            hitBox.Width = w;
            hitBox.Height = h;
        }

        public Rectangle HitBox {get { return hitBox; } set { hitBox = value; }}
    }
}
