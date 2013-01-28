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
        public enum EState { FALL, CLIMB, END_CLIMB, STOP, MOVE, FREE_FALL }
        private SpriteManager.ESprite skin;
        private Vector2 pos;
        public Vector2 Pos { get { return pos; } set { pos = value; }}
        private Vector2 posRel;
        public Vector2 PosRel { get { return posRel; } set { posRel = value; } }
        private EState state;
        public EState State { get { return state; } set { state = value; }}
        private EDirection direction;
        public EDirection Direction { get { return direction; } set { direction = value; }}
        private Dictionary<Climby.EState, Action> actions;
        private float rotation;
        private float speed;
        public float Speed { get { return speed; } set { speed = value; }}

        public enum EAroundSquare { TOP, FRONT_TOP, FRONT, FRONT_UNDER, FRONT_UNDER_UNDER, UNDER}
        private Dictionary<EAroundSquare, Block> aroundSquare;

        public Climby(SpriteManager.ESprite sk):base(App.Game)
        {
            skin = sk;
            pos = new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth / 2,
                                  Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize);
            posRel = new Vector2();
            updatePosRel();
            aroundSquare = new Dictionary<EAroundSquare, Block>();
            #region AroundSquare
            aroundSquare[EAroundSquare.FRONT] = null;
            aroundSquare[EAroundSquare.FRONT_TOP] = null;
            aroundSquare[EAroundSquare.FRONT_UNDER] = new Block(SpriteManager.ESprite.CLIMBYBLUE, null);
            aroundSquare[EAroundSquare.FRONT_UNDER_UNDER] = new Block(SpriteManager.ESprite.CLIMBYBLUE, null);
            aroundSquare[EAroundSquare.TOP] = null;
            aroundSquare[EAroundSquare.UNDER] = new Block(SpriteManager.ESprite.CLIMBYBLUE, null);
            #endregion
            actions = new Dictionary<EState, Action>();
            #region Actions
            actions.Add(EState.CLIMB, climb);
            actions.Add(EState.END_CLIMB, climb);
            actions.Add(EState.FALL, move);
            actions.Add(EState.FREE_FALL, fall);
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
            updatePosRel();
        }

        private void climb()
        {
            if (direction == EDirection.LEFT)
                rotation -= 0.09f * speed;
            else
                rotation += 0.09f * speed;
            if (state == EState.CLIMB)
            {
                pos.Y -= speed;
                updatePosRel();
            }
            else
                move();
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
            updatePosRel();
        }

        public void updatePosRel()
        {
            posRel.X = (pos.X - Constants.Measures.leftBoardMargin) / Constants.Measures.blockSize;
            posRel.Y = (pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize;

            posRel.X = direction == EDirection.LEFT ? (float)Math.Ceiling(posRel.X) : (float)Math.Floor(posRel.X);
            posRel.Y = state == EState.FREE_FALL ? (float)Math.Ceiling(posRel.Y) : (float)Math.Round(posRel.Y);
        }

        public void setAroundSquare(EAroundSquare e, Block b)
        {
            aroundSquare[e] = b;
        }

        public float getIntOrt()
        {
            return direction == EDirection.LEFT ? -1 : 1;
        }

        public bool sqrExist(EAroundSquare e)
        {
            return aroundSquare[e] != null;
        }

        public Block getBlock(EAroundSquare e)
        {
            return aroundSquare[e];
        }
    }
}
