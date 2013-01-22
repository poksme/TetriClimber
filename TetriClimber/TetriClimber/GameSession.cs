using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class GameSession : DrawableGameComponent
    {
        private Board board;
        private TetriminoFactory tetriminoFactory;
        private ATetrimino currTetrimino;
        private TimeSpan cur;
        private TimeSpan lat;

        public GameSession():base(App.Game)
        {
            board = new Board(new Vector2(Constants.Measures.boardBlockWidth, Constants.Measures.boardBlockHeight));
            tetriminoFactory = TetriminoFactory.Instance;
            currTetrimino = tetriminoFactory.getTetrimino();
            cur = TimeSpan.Zero;
            lat = new TimeSpan(10000000/6); // 3
        }

        public override void  Update(GameTime gameTime)
        {
 	         base.Update(gameTime);
             cur += gameTime.ElapsedGameTime;
             if (cur > lat)
             {
                 cur = TimeSpan.Zero;
                 if (tetriminoCanGoingDown())
                    currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X, currTetrimino.PosRel.Y + 1f);
                 else
                 {
                     board.pushBlocks(currTetrimino);
                     board.checkFullLine();
                     currTetrimino = tetriminoFactory.getTetrimino();
                 }
                    
             }

        }

        public override void  Draw(GameTime gameTime)
        {
 	         base.Draw(gameTime);
             board.Draw(gameTime);
             currTetrimino.Draw(gameTime);
        }

        private bool kickIt(int degree)
        {
            if (!currTetrimino.overlap(board))
                return true;
            if (degree > Constants.Measures.kickDegree)
                return false;
            if (currTetrimino.overlap(board))
            {
                for (int i = 0; i < degree; i++)
                {
                    currTetrimino.rightMove();
                    if (!currTetrimino.overlap(board))
                        return true;
                }
            }
            if (currTetrimino.overlap(board))
            {
                for (int i = 0; i < degree; i++)
                    currTetrimino.leftMove();
                for (int i = 0; i < degree; i++)
                {
                    currTetrimino.leftMove();
                    if (!currTetrimino.overlap(board))
                        return true;
                }
            }
            if (currTetrimino.overlap(board))
            {
                for (int i = 0; i < degree; i++)
                    currTetrimino.rightMove();
                for (int i = 0; i < degree; i++)
                {
                    currTetrimino.upMove();
                    if (!currTetrimino.overlap(board))
                        return true;
                }
                for (int i = 0; i < degree; i++)
                    currTetrimino.downMove();
            }
            return kickIt(degree + 1);
        }

        public void rightShift()
        {
            currTetrimino.rightShift();
            if (kickIt(1) == false)
                currTetrimino.leftShift();
            //Vector2 pos = currTetrimino.PosRel;
            //List<Block> shape;

            //currTetrimino.rightShift();
            //shape = currTetrimino.getBlocks();
            //Block rightest = currTetrimino.getMostRightBlock();
            //if (!(currTetrimino.PosRel.X < Constants.Measures.boardBlockWidth - currTetrimino.getMostRightBlock().PosRel.X - 1))
            //{
            //    currTetrimino.leftMove();
            //}
        }

        public void leftShift()
        {
            currTetrimino.leftShift();
        }

        public void dropDown()
        {
            Console.Out.WriteLine("Down not implemented");
        }

        public void rightMove()
        {
            List<Block> shapes = currTetrimino.getBlocks();
            foreach (Block b in shapes)
            {
                if (currTetrimino.PosRel.X + b.PosRel.X >= Constants.Measures.boardBlockWidth - 1)
                    return ;
                if (board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X + 1, currTetrimino.PosRel.Y + b.PosRel.Y)))
                    return ;
            }
            currTetrimino.rightMove();
        }

        public void leftMove()
        {
            List<Block> shapes = currTetrimino.getBlocks();
            foreach (Block b in shapes)
            {
                if (currTetrimino.PosRel.X + b.PosRel.X <= 0)
                    return;
                if (board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X - 1, currTetrimino.PosRel.Y + b.PosRel.Y)))
                    return;
            }
            currTetrimino.leftMove();
        }

        public bool tetriminoCanGoingDown()
        {
            List<Block> shapes = currTetrimino.getBlocks();
            foreach (Block b in shapes)
            {
                if (currTetrimino.PosRel.Y + b.PosRel.Y > Constants.Measures.boardBlockHeight -2)
                    return false;
                if (board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X, currTetrimino.PosRel.Y + b.PosRel.Y + 1)))
                    return false;
            }
            return true;
        }
    }
}
