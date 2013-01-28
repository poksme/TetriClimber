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
        private int score;
        private Climby climby;
        private Dictionary<Climby.EState, Action> state;
        private Vector2 climbyPosRel;
        private Climby.EDirection climbyDir;

        public GameSession(SpriteManager.ESprite playerType):base(App.Game)
        {
            board = new Board(new Vector2(Constants.Measures.boardBlockWidth, Constants.Measures.boardBlockHeight));
            climby = new Climby(playerType);
            tetriminoFactory = TetriminoFactory.Instance;
            currTetrimino = tetriminoFactory.getTetrimino();
            cur = TimeSpan.Zero;
            lat = new TimeSpan(10000000/3); // 3
            score = 0;
            climbyPosRel = new Vector2();
            state = new Dictionary<Climby.EState, Action>();
            #region Climby State
            state.Add(Climby.EState.FALL, climbyFall);
            state.Add(Climby.EState.FREE_FALL, climbyFreeFall);
            state.Add(Climby.EState.CLIMB, climbyClimb);
            state.Add(Climby.EState.END_CLIMB, climbyClimb);
            state.Add(Climby.EState.MOVE, climbyMove);
            state.Add(Climby.EState.STOP, climbyStop);
            #endregion
        }

        public override void Update(GameTime gameTime)
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
                    if (SoundManager.Instance.getPlayingSound() != SoundManager.ESound.FASTDROP)
                        SoundManager.Instance.play(SoundManager.ESound.DROP);
                    board.pushBlocks(currTetrimino);
                    List<int> brokenLines = board.checkFullLine();
                    score += brokenLines.Count * brokenLines.Count * 100;
                    currTetrimino = tetriminoFactory.getTetrimino();
                }        
            }
            climbyDir = climby.Direction;
            climbyPosRel.X = climby.PosRel.X;
            climbyPosRel.Y = climby.PosRel.Y;
            state[climby.State]();
            climby.Update(gameTime);
            if (climbyPosRel.X != climby.PosRel.X || climbyDir != climby.Direction)
                updateAroundSquare();
        }

        public override void Draw(GameTime gameTime)
        {
 	         base.Draw(gameTime);
             board.Draw(gameTime);
             currTetrimino.Draw(gameTime);
             climby.Draw(gameTime);
            //Console.Out.WriteLine(score);
        }

        #region Tetrimino Action
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
            else
                SoundManager.Instance.play(SoundManager.ESound.SHIFT);
        }

        public void leftShift()
        {
            currTetrimino.leftShift();
            if (kickIt(1) == false)
                currTetrimino.rightShift();
        }

        public void dropDown()
        {
            while (tetriminoCanGoingDown())
                currTetrimino.downMove();
            SoundManager.Instance.play(SoundManager.ESound.FASTDROP);
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
        #endregion
        #region Climby Action
        private void climbyFall()
        {
            if (climby.sqrExist(Climby.EAroundSquare.FRONT) ||
                climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER) ||
                !climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER_UNDER))
                climby.State = Climby.EState.MOVE;
            else if (Object.ReferenceEquals(board.getBlock(climby.PosRel.X, climby.PosRel.Y),climby.getBlock(Climby.EAroundSquare.FRONT)))
                climby.State = Climby.EState.FREE_FALL;
        }

        private void climbyFreeFall()
        {
            if (board.getBlock(climby.PosRel.X, climby.PosRel.Y + 1) != null)
                climby.State = Climby.EState.MOVE;
        }

        private void climbyClimb()
        {
            if (climby.sqrExist(Climby.EAroundSquare.TOP))
                climby.State = Climby.EState.FREE_FALL;
            else if (climby.sqrExist(Climby.EAroundSquare.FRONT_TOP))
                climby.State = Climby.EState.FREE_FALL;
            else if (Object.ReferenceEquals(board.getBlock(climby.PosRel.X, climby.PosRel.Y),climby.getBlock(Climby.EAroundSquare.FRONT_TOP)))
                climby.State = Climby.EState.MOVE;
            else if (Object.ReferenceEquals(board.getBlock(climby.PosRel.X, climby.PosRel.Y), climby.getBlock(Climby.EAroundSquare.TOP)))
                climby.State = Climby.EState.END_CLIMB;
        }

        private void climbyMove()
        {
            if (!climby.sqrExist(Climby.EAroundSquare.UNDER))
                climby.State = Climby.EState.FREE_FALL;
            else if (climby.sqrExist(Climby.EAroundSquare.FRONT))
                if (climby.sqrExist(Climby.EAroundSquare.FRONT_TOP))
                    climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT; //REVERT
                else
                    if (climby.sqrExist(Climby.EAroundSquare.TOP))
                        climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT; //REVERT
                    else
                       climby.State = Climby.EState.CLIMB;
            else if (!climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER))
                if (climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER_UNDER))
                    climby.State = Climby.EState.FALL;
                else
                    climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT; //REVERT
        }

        //private void climbyMove()
        //{
        //    if (!climby.sqrExist(Climby.EAroundSquare.UNDER))
        //    {
        //        climby.State = Climby.EState.FREE_FALL;
        //        return;
        //    }
        //    else if (climby.sqrExist(Climby.EAroundSquare.FRONT))
        //    {
        //        if (climby.sqrExist(Climby.EAroundSquare.FRONT_TOP))
        //        {
        //            climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;
        //            return;
        //        }
        //        else
        //        {
        //            if (climby.sqrExist(Climby.EAroundSquare.TOP))
        //            {
        //                climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;
        //                return;
        //            }
        //            climby.State = Climby.EState.CLIMB;
        //            return;
        //        }
        //    }
        //    else if (!climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER))
        //    {
        //        if (climby.sqrExist(Climby.EAroundSquare.FRONT_UNDER_UNDER))
        //        {
        //            climby.State = Climby.EState.FALL;
        //            return;
        //        }
        //        else
        //        {
        //            climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;
        //            return;
        //        }
        //    }
        //}

        private void climbyStop()
        {
            climbyMove();
        }
        #endregion
        
        private void updateAroundSquare()
        {
            Vector2 pRel = climby.PosRel;

            climby.setAroundSquare(Climby.EAroundSquare.FRONT, board.getBlock(pRel.X + climby.getIntOrt(), pRel.Y));
            climby.setAroundSquare(Climby.EAroundSquare.FRONT_TOP, board.getBlock(pRel.X + climby.getIntOrt(), pRel.Y - 1));
            climby.setAroundSquare(Climby.EAroundSquare.FRONT_UNDER, board.getBlock(pRel.X + climby.getIntOrt(), pRel.Y + 1));
            climby.setAroundSquare(Climby.EAroundSquare.FRONT_UNDER_UNDER, board.getBlock(pRel.X + climby.getIntOrt(), pRel.Y + 2));
            climby.setAroundSquare(Climby.EAroundSquare.TOP, board.getBlock(pRel.X, pRel.Y - 1));
            climby.setAroundSquare(Climby.EAroundSquare.UNDER, board.getBlock(pRel.X, pRel.Y + 1));

        }
    }
}
