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
        private Block[][] board;
        private TetriminoFactory tetriminoFactory;
        private ATetrimino nextTetrimino;
        private ATetrimino currTetrimino;
        private KeyboardState oldState;

        public Play() : base()
        {
            board = new Block[22][];
            for (int x = 0; x < board.Length; x++)
                board[x] = new Block[10];
            tetriminoFactory = TetriminoFactory.Instance;
            nextTetrimino = tetriminoFactory.getTetrimino();
            currTetrimino = tetriminoFactory.getTetrimino();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            currTetrimino.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
            {
                currTetrimino.leftShift();
            }
            if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
            {
                currTetrimino.rightShift();
            }
            if (newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
            {
                currTetrimino = tetriminoFactory.getTetrimino();
            }
            oldState = newState;
        }
    }
}
