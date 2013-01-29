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

        //
        private Dictionary<Climby.EAroundSquare, Rectangle> aroundRect;
        private Climby.EDirection lastDir;
        //

        public GameSession(SpriteManager.ESprite playerType):base(App.Game)
        {
            board = new Board(new Vector2(Constants.Measures.boardBlockWidth, Constants.Measures.boardBlockHeight));
            climby = new Climby(playerType);

            //
            aroundRect = new Dictionary<Climby.EAroundSquare, Rectangle>();
            lastDir = climby.Direction;
            //

            tetriminoFactory = TetriminoFactory.Instance;
            currTetrimino = tetriminoFactory.getTetrimino();
            cur = TimeSpan.Zero;
            lat = new TimeSpan(10000000/3); // 3
            score = 0;
            state = new Dictionary<Climby.EState, Action>();


            #region Climby State
            state.Add(Climby.EState.FALL, climbyFall);
            state.Add(Climby.EState.FREE_FALL, climbyFreeFall);
            state.Add(Climby.EState.CLIMB, climbyClimb);
            state.Add(Climby.EState.END_CLIMB, climbyClimb);
            state.Add(Climby.EState.MOVE, climbyMove);
            state.Add(Climby.EState.STOP, climbyStop);
            #endregion
            updateAroundRects();
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
            state[climby.State]();
            climby.Update(gameTime);
            if (lastDir != climby.Direction ||
                climby.State == Climby.EState.MOVE ||
                climby.State == Climby.EState.FREE_FALL)
                updateAroundRects();
            lastDir = climby.Direction;
        }

        public override void Draw(GameTime gameTime)
        {
 	         base.Draw(gameTime);
             board.Draw(gameTime);
             currTetrimino.Draw(gameTime);
             climby.Draw(gameTime);
            // // DEBUG COLORS
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.FRONT], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.FRONT_TOP], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.FRONT_UNDER], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.TOP], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(aroundRect[Climby.EAroundSquare.UNDER], Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(climby.ActualPosition, Color.Blue);
            // // UNCOMMENT THIS BLOCK TO SEE ALL THE HITBOXES
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
            if (!aroundRect[Climby.EAroundSquare.FRONT].IsEmpty ||
                !aroundRect[Climby.EAroundSquare.FRONT_UNDER].IsEmpty ||
                aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER].IsEmpty) // SI UNE DES CONDITIONS DU FALL N'EST PLUS REMPLIE 
                climby.State = Climby.EState.MOVE;                                  // ON CHANGE D'ETAT POUR MOVE
            else if ((climby.Direction == Climby.EDirection.RIGHT && climby.ActualPosition.Left >= aroundRect[Climby.EAroundSquare.UNDER].Right) ||
                    (climby.Direction == Climby.EDirection.LEFT && climby.ActualPosition.Right <= aroundRect[Climby.EAroundSquare.UNDER].Left)) // APRES ETRE TOTALEMENT AU DESSUS DU VIDE
                climby.State = Climby.EState.FREE_FALL;                                                                                         // ON CHANGE D'ETAT POUR FREE FALL
        }

        private void climbyFreeFall()
        {
            if (climby.ActualPosition.Bottom >= aroundRect[Climby.EAroundSquare.UNDER].Top && !aroundRect[Climby.EAroundSquare.UNDER].IsEmpty)                          //ON TOMBE JUSQU'A CE QU'ON TROUVE UN BLOCK ET QUE L'ON SOIT JUSTE AU DESSUS DE CE BLOCK
                climby.State = Climby.EState.MOVE;
        }

        private void climbyClimb()
        {
            if (aroundRect[Climby.EAroundSquare.FRONT].IsEmpty || !aroundRect[Climby.EAroundSquare.TOP].IsEmpty || !aroundRect[Climby.EAroundSquare.FRONT_TOP].IsEmpty)  // SI LES CONDITIONS DE MONTEES NE SONT PLUS REMPLIES
                climby.State = Climby.EState.FREE_FALL;                                                                                                                  // ON TOMBE
            else if ((climby.Direction == Climby.EDirection.RIGHT   && climby.ActualPosition.Left >= aroundRect[Climby.EAroundSquare.FRONT].Left) ||                     // SI ON EST ARRIVE AU DESSUS ET AU MILIEU DU BLOCK FRONT
                     (climby.Direction == Climby.EDirection.LEFT    && climby.ActualPosition.Right <= aroundRect[Climby.EAroundSquare.FRONT].Right))
                climby.State = Climby.EState.MOVE;                                                                                                                       // ON CHANGE D'ETAT POUR MOVE
            else if (climby.ActualPosition.Bottom <= aroundRect[Climby.EAroundSquare.FRONT].Top)                                                                         // SI ON EST ARRIVE AU DESSUS ET A GAUCHE DU BLOCK FRONT
                climby.State = Climby.EState.END_CLIMB;                                                                                                                  // ON CHANGE D'ETAT POUR END_CLIMB (POUR ALLER AU MILIEU DU BLOCK FRONT)
        }

        private void climbyMove()
        {
            if (climby.ActualPosition.Bottom < aroundRect[Climby.EAroundSquare.UNDER].Top)                                                              // LE BLOCK DU DESSOUS EST VIDE 
                climby.State = Climby.EState.FREE_FALL;                                                                                                 // DONC TOMBE
            else if ((climby.Direction == Climby.EDirection.RIGHT   && climby.ActualPosition.Right >= aroundRect[Climby.EAroundSquare.UNDER].Right) ||  // SINON
                     (climby.Direction == Climby.EDirection.LEFT    && climby.ActualPosition.Left <= aroundRect[Climby.EAroundSquare.UNDER].Left))      // ON VERIFIE LES CHOSES SUIVANTES QUE SI ON EST A L EXTREMITE DE NOTRE CHEMIN
                if (climby.ActualPosition.Intersects(aroundRect[Climby.EAroundSquare.FRONT]))                                               // TOUCHE UN MUR
                    if (aroundRect[Climby.EAroundSquare.FRONT_TOP].IsEmpty && aroundRect[Climby.EAroundSquare.TOP].IsEmpty)                 // SI LA VOIX EST LIBRE EN HAUT
                        climby.State = Climby.EState.CLIMB;                                                                                 // ON MONTE
                    else                                                                                                                    // SINON
                        climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;   // ON SE RETOURNE 
                else if (aroundRect[Climby.EAroundSquare.FRONT_UNDER].IsEmpty)                                                              // FACE A UN FOSSE
                    if (!aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER].IsEmpty)                                                        // SI PROFOND QUE DE UNE CASE
                        climby.State = Climby.EState.FALL;                                                                                  // ON DESCEND
                    else                                                                                                                    // SINON
                        climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;   // ON SE RETOURNE
        }

        private void climbyStop()
        {
            //climbyMove();
        }
        #endregion

        private void updateAroundRects()
        {
            aroundRect[Climby.EAroundSquare.FRONT] = board.getRect(climby.ActualPosition.Center, climby.getIntOrt(), 0);
            aroundRect[Climby.EAroundSquare.FRONT_TOP] = board.getRect(climby.ActualPosition.Center, climby.getIntOrt(), -1);
            aroundRect[Climby.EAroundSquare.FRONT_UNDER] = board.getRect(climby.ActualPosition.Center, climby.getIntOrt(), 1);
            aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER] = board.getRect(climby.ActualPosition.Center, climby.getIntOrt(), 2);
            aroundRect[Climby.EAroundSquare.TOP] = board.getRect(climby.ActualPosition.Center, 0, -1);
            aroundRect[Climby.EAroundSquare.UNDER] = board.getRect(climby.ActualPosition.Center, 0, 1);
        }
    }
}
