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

        private TimeSpan lat = new TimeSpan(10000000/20);
        private TimeSpan cur = new TimeSpan(0);

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
            cur = cur.Add(gameTime.ElapsedGameTime);
            if (TimeSpan.Compare(cur, lat) > 0)
            {
                if (App.ToucheInput.getState(AUserInput.EInput.RIGHT))
                {
                    Console.Out.WriteLine("DROITE");
                    currTetrimino.rightShift();
                }
                else
                {
                    Console.Out.WriteLine("NO DROITE");

                }
                if (App.ToucheInput.getState(AUserInput.EInput.LEFT))
                {
                    currTetrimino.leftShift();
                }
                if (App.ToucheInput.getState(AUserInput.EInput.DOWN))
                {
                    currTetrimino = tetriminoFactory.getTetrimino();
                }
                cur = new TimeSpan(0);
                if (App.ToucheInput is KeyboardInput)
                {
                    (App.ToucheInput as KeyboardInput).reset();
                }
            }
        }
    }
}
