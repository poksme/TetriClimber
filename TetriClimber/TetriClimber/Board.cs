using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Board : DrawableGameComponent
    {
        Vector2 pos;
        Vector2 size;
        Block[][] grid;

        public Board(Vector2 size) : base(App.Game)
        {
            pos = Vector2.Zero;
            this.size = size;
            grid = new Block[(int)size.Y][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Block[(int)size.X];
                for (int j = 0; j < (int)size.X; j++)
                    grid[i][j] = null;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void  Draw(GameTime gameTime)
        {
 	        base.Draw(gameTime);
            //SpriteManager.Instance.drawRectangleAbsPos(new Rectangle(20, 20, (int)(size.X * Constants.Measures.blockSize), (int)(size.Y * Constants.Measures.blockSize)), 
            //                                            Color.White * 0.5f);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin), (int)(Constants.Measures.upBoardMargin), (int)(size.X * Constants.Measures.blockSize), (int)(size.Y * Constants.Measures.blockSize)), Color.White * 0.5f,
                                                              Constants.Measures.borderSize, Constants.Color.border);
        }

        public bool isFullLine(int Y)
        {
            for (int i = 0; i < (int)size.X; i++)
                if (grid[Y][i] == null)
                    return false;
            return true;
        }

        public void pushBlocks(ATetrimino t)
        {
            List<Block> blocks = t.getBlocks();
            int tx = (int)t.PosRel.X;
            int ty = (int)t.PosRel.Y;
            foreach (Block b in blocks)
            {
                Vector2 pos = b.PosRel;
                grid[(int)(pos.Y + ty)][(int)(pos.X + tx)] = b;
            }
        }

        public bool isBusyCase(Vector2 coord)
        {
            return (grid[(int)coord.Y][(int)coord.X] == null);
        }
    }
}
