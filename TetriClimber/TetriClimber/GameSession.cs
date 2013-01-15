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
            board = new Board(new Vector2(10, 22));
            tetriminoFactory = TetriminoFactory.Instance;
            currTetrimino = tetriminoFactory.getTetrimino();
            cur = TimeSpan.Zero;
            lat = new TimeSpan(10000000);
        }

        public override void  Update(GameTime gameTime)
        {
 	         base.Update(gameTime);
             cur += gameTime.ElapsedGameTime;
             if (cur > lat)
             {
                 cur = TimeSpan.Zero;
                 if (currTetrimino.PosRel.Y < Constants.Measures.boardBlockHeight - 2 - currTetrimino.getMostDownBlock().PosRel.Y)
                     currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X, currTetrimino.PosRel.Y + 1f);
                 else
                 {
                     board.pushBlocks(currTetrimino);
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

        public void rightShift()
        {
            currTetrimino.rightShift();
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
            if (currTetrimino.PosRel.X < Constants.Measures.boardBlockWidth - currTetrimino.getMostRightBlock().PosRel.X - 1)
                currTetrimino.rightMove();
        }

        public void leftMove()
        {
            if (currTetrimino.PosRel.X > -currTetrimino.getMostLeftBlock().PosRel.X)
                currTetrimino.leftMove();
        }
    }
}
