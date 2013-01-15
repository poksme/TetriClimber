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
                 currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X, currTetrimino.PosRel.Y + 1f);
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
        }

        public void leftShift()
        {
            currTetrimino.leftShift();
        }

        public void dropDown()
        {
            Console.Out.WriteLine("Down not implemented");
        }

        internal void rightMove()
        {
            currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X + 1f, currTetrimino.PosRel.Y);
        }

        internal void leftMove()
        {
            currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X - 1f, currTetrimino.PosRel.Y);
        }
    }
}
