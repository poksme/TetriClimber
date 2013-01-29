using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Board : DrawableGameComponent
    {
        Vector2 size;
        Block[][] grid;
        HashSet<int> updatedLine;

        public Board(Vector2 size) : base(App.Game)
        {
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
            SpriteManager.Instance.drawBoardedRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin), (int)(Constants.Measures.upBoardMargin), (int)(size.X * Constants.Measures.blockSize), (int)(size.Y * Constants.Measures.blockSize)), Color.White * 0.8f,
                                                              Constants.Measures.borderSize, Constants.Color.border);
            for (int y = 0; y < Constants.Measures.boardBlockHeight; y++)
                for (int x = 0; x < Constants.Measures.boardBlockWidth; x++)
                    if (grid[y][x] != null)
                        SpriteManager.Instance.drawRotatedAtPos(grid[y][x].Color, new Vector2(Constants.Measures.leftBoardMargin + x * Constants.Measures.blockSize, Constants.Measures.upBoardMargin + y * Constants.Measures.blockSize), grid[y][x].Orientation, Constants.Measures.blockSize);
                        //SpriteManager.Instance.drawAtPos(grid[y][x].Color, new Vector2(x * Constants.Measures.blockSize, y * Constants.Measures.blockSize));
        }

        public bool isFullLine(int Y)
        {
            for (int i = 0; i < (int)size.X; i++)
                if (grid[Y][i] == null)
                    return false;
            return true;
        }

        public void removeLine(int Y)
        {
            for (int i = 0; i < (int)size.X; i++)
                grid[Y][i] = null;
            for (; Y > 0; Y--)
                for (int i = 0; i < (int)size.X; i++)
                {
                    grid[Y][i] = grid[Y - 1][i];
                    if (grid[Y][i] != null)
                        grid[Y][i].setHitBoxValue((int)(i * Constants.Measures.blockSize + Constants.Measures.leftBoardMargin),
                                    (int)(Y * Constants.Measures.blockSize + Constants.Measures.upBoardMargin));
                }
            for (int x = 0; x < (int)size.X; x++)
                grid[0][x] = null;
        }

        public void pushBlocks(ATetrimino t)
        {
            updatedLine = new HashSet<int>();
            List<Block> blocks = t.getBlocks();
            int tx = (int)t.PosRel.X;
            int ty = (int)t.PosRel.Y;
            foreach (Block b in blocks)
            {
                b.setHitBoxValue((int)((b.PosRel.X + tx) * Constants.Measures.blockSize + Constants.Measures.leftBoardMargin),
                                 (int)((b.PosRel.Y + ty) * Constants.Measures.blockSize + Constants.Measures.upBoardMargin));
                Vector2 pos = b.PosRel;
                grid[(int)(pos.Y + ty)][(int)(pos.X + tx)] = b;
                updatedLine.Add((int)(pos.Y + ty));
            }
        }

        public bool isBusyCase(Vector2 coord)
        {
            if ((int)coord.Y < 0)
                return false;
            if ((int)coord.X >= 0 && (int)coord.X < Constants.Measures.boardBlockWidth)
                return (grid[(int)coord.Y][(int)coord.X] != null);
            return true;
        }

        public bool isOutOfBond(Vector2 coord)
        {
            if ((int)coord.Y < 0 || (int)coord.Y >= Constants.Measures.boardBlockHeight ||
                (int)coord.X < 0 || (int)coord.X >= Constants.Measures.boardBlockWidth)
                return true;
            return false;
        }

        public List<int> checkFullLine()
        {
            List<int> brokenLine = new List<int>();
            
            foreach (int l in updatedLine)
                if (isFullLine(l))
                {
                    removeLine(l);
                    brokenLine.Add(l);
                    Console.Out.WriteLine(l);
                }
            int size = brokenLine.Count;
            if (size != 0)
                SoundManager.Instance.play(SoundManager.ESound.CLEARLINE, 0.25f * (float)size, 1f);
            return brokenLine;
        }

        public Rectangle getRect(Point point, int padX = 0, int padY = 0)
        {
            if (point.X >= Constants.Measures.boardBlockWidth || point.X < 0
                || point.Y >= Constants.Measures.boardBlockHeight || point.Y < 0)
                return new Rectangle(point.X * (int)(Constants.Measures.blockSize) + (int)(Constants.Measures.leftBoardMargin),
                                     point.Y * (int)(Constants.Measures.blockSize) + (int)(Constants.Measures.upBoardMargin),
                                     (int)(Constants.Measures.blockSize), (int)(Constants.Measures.blockSize));
            if (grid[point.Y][point.X] != null)
                return grid[point.Y][point.X].HitBox;
            return Rectangle.Empty;
        }
    }
}