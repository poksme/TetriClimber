using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Climby : DrawableGameComponent
    {
        public enum EDirection { LEFT, RIGHT }
        public enum EState { FALL, CLIMB, STOP, MOVE }
        private SpriteManager.ESprite skin;
        private Vector2 pos;
        private EState state;
        public EState State { get { return state; } set { state = value; }}
        private EDirection direction;
        public EDirection Direction { get { return direction; } set { direction = value; }}

        private float rotation;

        public Climby(SpriteManager.ESprite sk):base(App.Game)
        {
            skin = sk;
            pos = new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth / 2,
                                  Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize);
            state = EState.MOVE;
            direction = EDirection.RIGHT;
            rotation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (state)
            {
                case EState.FALL:
                    break;
                case EState.CLIMB:
                    break;
                case EState.MOVE:
                    move();
                    break;
                default:
                    break;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRotatedAtPos(skin, pos, rotation, Constants.Measures.blockSize);
        }

        private void move()
        {
            if (direction == EDirection.LEFT)
            {
                pos.X--;
                rotation -= 0.09f;
            }
            else
            {
                rotation += 0.09f;
                pos.X++;
            }
        }
    }
}
