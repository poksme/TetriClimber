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
        public Vector2 Pos { get { return pos; } set { pos = value; }}
        private EState state;
        public EState State { get { return state; } set { state = value; }}
        private EDirection direction;
        public EDirection Direction { get { return direction; } set { direction = value; }}
        private Dictionary<Climby.EState, Action> actions;
        private float rotation;
        private float speed;
        public float Speed { get { return speed; } set { speed = value; }}

        public Climby(SpriteManager.ESprite sk):base(App.Game)
        {
            skin = sk;
            pos = new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth / 2,
                                  Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize);
            actions = new Dictionary<EState, Action>();
            #region Actions
            actions.Add(EState.CLIMB, climb);
            actions.Add(EState.FALL, fall);
            actions.Add(EState.MOVE, move);
            actions.Add(EState.STOP, stop);
            #endregion
            state = EState.MOVE;
            direction = EDirection.RIGHT;
            rotation = 0f;
            speed = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            actions[state]();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRotatedAtPos(skin, pos, rotation, Constants.Measures.blockSize);
        }

        private void fall()
        {
            if (direction == EDirection.LEFT)
                rotation -= 0.09f * speed;
            else
                rotation += 0.09f * speed;
            pos.Y += speed;
        }

        private void climb()
        {
            if (direction == EDirection.LEFT)
                rotation -= 0.09f * speed;
            else
                rotation += 0.09f * speed;
            pos.Y -= speed;
        }

        private void stop()
        {
        }

        private void move()
        {
            if (direction == EDirection.LEFT)
            {
                pos.X -= speed;
                rotation -= 0.09f * speed;
            }
            else
            {
                rotation += 0.09f * speed;
                pos.X += speed;
            }
        }
    }
}
