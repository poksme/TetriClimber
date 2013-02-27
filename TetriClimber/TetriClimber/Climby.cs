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
        private int minHeight;
        private int oldMinHeight;
        private EState state;
        private EDirection direction;
        private Dictionary<Climby.EState, Action<GameTime>> actions;
        private float rotation;
        private float speed;
        private Rectangle actualPosition;
        private Rectangle deadZone;
        private CoordHelper.EProfile playerType;

        public Climby(CoordHelper.EProfile pt):base(App.Game)
        {
            playerType = pt;
            if (playerType == CoordHelper.EProfile.ONEPLAYER)
                skin = SpriteManager.ESprite.CLIMBYBLUE;
            else
                skin = SpriteManager.ESprite.CLIMBYRED;
            pos = new Vector2(CoordHelper.Instance.getLeftMargin(playerType) + Constants.Measures.boardWidth / 2,
                                  Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize);
            actualPosition = new Rectangle((int)pos.X, (int)pos.Y, (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
            deadZone = new Rectangle((int)pos.X + (int)(Constants.Measures.blockSize / 3),
                                     (int)pos.Y , (int)Constants.Measures.blockSize / 3, (int)Constants.Measures.blockSize);
            actions = new Dictionary<EState, Action<GameTime>>();
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
            speed = 0.00002f / 6;
            minHeight = (int)((pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize);
            oldMinHeight = minHeight;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            oldMinHeight = minHeight;
            actions[state](gameTime);
            actualPosition.X = (int)pos.X;// (int)pos.Y, (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
            actualPosition.Y = (int)pos.Y;// (int)Constants.Measures.blockSize, (int)Constants.Measures.blockSize);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRotatedAtPos(skin, pos, rotation, Constants.Measures.blockSize);
        }

        private void fall(GameTime gameTime)
        {
            if (direction == EDirection.LEFT)
                rotation -= 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
            else
                rotation += 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
            pos.Y += speed * gameTime.ElapsedGameTime.Ticks;
        }

        private void climb(GameTime gameTime)
        {
            if (direction == EDirection.LEFT)
                rotation -= 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
            else
                rotation += 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
            pos.Y -= speed * gameTime.ElapsedGameTime.Ticks;
            if (minHeight > (int)((pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize))
                minHeight = (int)((pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize);
        }   

        private void stop(GameTime gameTime)
        {
        }

        private void move(GameTime gameTime)
        {
            if (direction == EDirection.LEFT)
            {
                pos.X -= speed * gameTime.ElapsedGameTime.Ticks;
                rotation -= 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
            }
            else
            {
                rotation += 0.09f * speed * gameTime.ElapsedGameTime.Ticks;
                pos.X += speed * gameTime.ElapsedGameTime.Ticks;
            }
        }

        public Point getRelPos()
        {
            return new Point((int)((pos.X - CoordHelper.Instance.getLeftMargin(playerType)) / Constants.Measures.blockSize),
                             (int)((pos.Y - Constants.Measures.upBoardMargin) / Constants.Measures.blockSize));
        }

        public void stepDown(Dictionary<EAroundSquare,Point> asqr , int step = 1)
        {
            pos.Y += step * Constants.Measures.blockSize;
            minHeight += step;
            oldMinHeight += step;
            if (this.state == EState.CLIMB || this.state == EState.FALL || this.state == EState.END_CLIMB)
                foreach (EAroundSquare e in Enum.GetValues(typeof(EAroundSquare)))
                {
                    Point p = asqr[e];
                    p.Y += step;
                    asqr[e] = p;
                }

            // RECENTLY ADDED
            actualPosition.Y += (int)(step * Constants.Measures.blockSize);
        }

        public void setSpeedFromLevel(int l)
        {
            speed += (l + 1) * 0.000001f;
        }

        public int          getIntOrt()     { return direction == EDirection.LEFT ? -1 : 1; }
        public Rectangle    ActualPosition  { get { return actualPosition; }    set { actualPosition = value; } }
        public Vector2      Pos             { get { return pos; }               set { pos = value; } }
        public EState       State           { get { return state; }             set { state = value; } }
        public EDirection   Direction       { get { return direction; }         set { direction = value; } }
        public float        Speed           { get { return speed; }             set { speed = value; } }
        public Rectangle DeadZone           { get { deadZone.X = (int)pos.X + (int)(Constants.Measures.blockSize / 3); deadZone.Y = (int)pos.Y; return deadZone; } }
        public int MinHeight                { get { return minHeight; } }
        public int OldMinHeight             { get { return oldMinHeight; } }

        //public bool isUnderBoard            { get { return (pos.Y - Constants.Measures.blockSize > Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.blockSize); } }

        //public void stepUp()
        //{
        //    pos.Y -= Constants.Measures.blockSize;
        //}

        //public bool overLap(Board board)
        //{
        //    return (board.getRect(new Point((int)((pos.X - CoordHelper.Instance.getLeftMargin(playerType)) / Constants.Measures.blockSize),(int)(Constants.Measures.boardBlockHeight - 1)))).Intersects(this.deadZone);
        //}
    }
}
