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
        public Vector2 PosRel { get { return posRel; } }
        private Vector2 posAbs = Vector2.Zero;
        public Vector2 PosAbs { get { return posAbs; } }
        private SpriteManager.ESprite color;
        private float orientation;

        public Block(SpriteManager.ESprite color) : base(App.Game)
        {
            this.color = color;
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
            SpriteManager.Instance.drawRotatedAtPos(color, new Vector2(posRel.X * Constants.Measures.blockSize, posRel.Y * Constants.Measures.blockSize), orientation, Constants.Measures.blockSize);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void setOrientation(int orientation)
        {
            this.orientation = MathHelper.ToRadians(orientation * 90);
        }
    }
}
