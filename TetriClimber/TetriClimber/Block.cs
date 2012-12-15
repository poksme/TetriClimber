using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Block : DrawableGameComponent
    {
        private Vector2 pos = Vector2.Zero;
        private SpriteManager.ESprite color;

        public Block(SpriteManager.ESprite color) : base(App.Game)
        {
            this.color = color;
        }

        public void setPosition(Vector2 coord)
        {
            pos = coord;
        }

        public void setPosition(int x, int y)
        {
            pos = new Vector2((float)x, (float)y);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawAtPos(color, new Vector2(pos.X * 115, pos.Y * 115));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
