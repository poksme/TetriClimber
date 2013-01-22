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

        public GameSession(SpriteManager.ESprite playerType):base(App.Game)
        {
            board = new Board(new Vector2(Constants.Measures.boardBlockWidth, Constants.Measures.boardBlockHeight));
            climby = new Climby(playerType);
            tetriminoFactory = TetriminoFactory.Instance;
            currTetrimino = tetriminoFactory.getTetrimino();
            cur = TimeSpan.Zero;
            lat = new TimeSpan(10000000/6); // 3
            score = 0;
            state = new Dictionary<Climby.EState, Action>();
            #region Climby State
            state.Add(Climby.EState.FALL, climbyFall);
            state.Add(Climby.EState.CLIMB, climbyClimb);
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
                    int nbLines = board.checkFullLine();
                    score += nbLines * nbLines * 100;
                    currTetrimino = tetriminoFactory.getTetrimino();
                }        
            }
            state[climby.State]();
            climby.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
 	         base.Draw(gameTime);
             board.Draw(gameTime);
             currTetrimino.Draw(gameTime);
             climby.Draw(gameTime);
             Console.Out.WriteLine(score);
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

        private void climbyFall()
        {
        }

        private void climbyClimb()
        {
        }

        private void climbyMove()
        {
        }

        private void climbyStop()
        {
        }
    }
}
