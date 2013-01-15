using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class Play : AScene
    {
        private Board board;
        private TetriminoFactory tetriminoFactory;
        private ATetrimino nextTetrimino;
        private ATetrimino currTetrimino;

        private TimeSpan lat = new TimeSpan(10000000 / 2);
        private TimeSpan cur = new TimeSpan(0);
        private TimeSpan turnLat = new TimeSpan(10000000 / 10);

        public Play() : base()
        {
            board = new Board(new Vector2(10, 22));
            //for (int x = 0; x < board.Length; x++)
            //    board[x] = new Block[10];
            tetriminoFactory = TetriminoFactory.Instance;
            nextTetrimino = tetriminoFactory.getTetrimino();
            currTetrimino = tetriminoFactory.getTetrimino();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            board.Draw(gameTime);
            currTetrimino.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cur += gameTime.ElapsedGameTime;
            if (cur >= turnLat)
                cur = new TimeSpan(0);
            if (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) > lat && cur == TimeSpan.Zero))
                    currTetrimino.rightShift();
            if (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) > lat && cur == TimeSpan.Zero))
                currTetrimino.leftShift();
            if (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
                currTetrimino = tetriminoFactory.getTetrimino();
        }
    }
}
