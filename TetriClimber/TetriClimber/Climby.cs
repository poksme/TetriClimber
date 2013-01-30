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
        public enum EAroundSquare { TOP, FRONT_TOP, FRONT, FRONT_UNDER, FRONT_UNDER_UNDER, UNDER }

        
        private SpriteManager.ESprite skin;
        private Vector2 pos;
        private EState state;
        private EDirection direction;
        private Dictionary<Climby.EState, Action> actions;
        private float rotation;
        private float speed;
        private Rectangle actualPosition;
        private Rectangle deadZone;

        public Climby(SpriteManager.ESprite sk):base(App.Game)
        {
            skin = sk;
            pos = new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth / 2,
                                  Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize);
            actualPosition = new Rectangle((int)pos.X, (int)pos.Y, (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
            deadZone = new Rectangle((int)pos.X + (int)(Constants.Measures.blockSize / 3),
                                     (int)pos.Y , (int)Constants.Measures.blockSize / 3, (int)Constants.Measures.blockSize);
            actions = new Dictionary<EState, Action>();
            #region Actions
            actions.Add(EState.CLIMB, climb);
            actions.Add(EState.END_CLIMB, move);
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
            actualPosition.X = (int)pos.X;// (int)pos.Y, (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
            actualPosition.Y = (int)pos.Y;// (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
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

        public Point getRelPos()
        {
            return new Point((int)((pos.X - Constants.Measures.leftBoardMargin) / Constants.Measures.blockSize),
                             (int)((pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize));
        }

        public void stepDown(Dictionary<EAroundSquare,Point> asqr , int step = 1)
        {
            pos.Y += step * Constants.Measures.blockSize;
            foreach (EAroundSquare e in Enum.GetValues(typeof(EAroundSquare)))
            {
                Point p = asqr[e];
                p.Y += step;
                asqr[e] = p;
            }
        }

        public int          getIntOrt()     { return direction == EDirection.LEFT ? -1 : 1; }
        public Rectangle    ActualPosition  { get { return actualPosition; }    set { actualPosition = value; } }
        public Vector2      Pos             { get { return pos; }               set { pos = value; } }
        public EState       State           { get { return state; }             set { state = value; } }
        public EDirection   Direction       { get { return direction; }         set { direction = value; } }
        public float        Speed           { get { return speed; }             set { speed = value; } }
        public Rectangle DeadZone           { get { deadZone.X = (int)pos.X + (int)(Constants.Measures.blockSize / 3); deadZone.Y = (int)pos.Y; return deadZone; } }
    }
}
