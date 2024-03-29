﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Surface;
using Microsoft.Surface.Core;

namespace TetriClimber
{
    public class GameSession : DrawableGameComponent
    {
        private Board board;
        private TetriminoFactory tetriminoFactory;
        private ATetrimino currTetrimino;
        private ATetrimino shadowTetrimino;
        private TimeSpan cur;
        private TimeSpan lat;
        private TimeSpan tSpinLimit;
        private TimeSpan tSpinCur;
        public Score score {get; private set;}
        private Level level;
        private Climby climby;
        private Dictionary<Climby.EState, Action> state;
        private Dictionary<Climby.EAroundSquare, Point> aroundRect;
        private Climby.EDirection lastDir;
        private HUD hud;
        private CoordHelper.EProfile playerType;
        public bool death { get; private set; }

        public GameSession(CoordHelper.EProfile pt, HUD h):base(App.Game)
        {
            hud = h;
            playerType = pt;
            board = new Board(playerType, new Vector2(Constants.Measures.boardBlockWidth, Constants.Measures.boardBlockHeight));
            climby = new Climby(playerType);
            //
            aroundRect = new Dictionary<Climby.EAroundSquare, Point>();
            #region Set Around Rect
            aroundRect.Add(Climby.EAroundSquare.FRONT, Point.Zero);
            aroundRect.Add(Climby.EAroundSquare.FRONT_TOP, Point.Zero);
            aroundRect.Add(Climby.EAroundSquare.FRONT_UNDER, Point.Zero);
            aroundRect.Add(Climby.EAroundSquare.FRONT_UNDER_UNDER, Point.Zero);
            aroundRect.Add(Climby.EAroundSquare.TOP, Point.Zero);
            aroundRect.Add(Climby.EAroundSquare.UNDER, Point.Zero);
            #endregion
            lastDir = climby.Direction;
            //

            tetriminoFactory = TetriminoFactory.Instance;
            var tmp = tetriminoFactory.getTetrimino(playerType);
            currTetrimino = tmp.Item1;
            shadowTetrimino = tmp.Item2;
            hud.setNext(TetriminoFactory.Instance.getNextTetrimino(playerType), playerType);
            cur = TimeSpan.Zero;
            //lat = new TimeSpan(10000000/3); // 3
            score = new Score();
            level = new Level();
            //lat = new TimeSpan(10000000 / (level.level + 1));
            lat = new TimeSpan((10 - level.level) * 1000000);
            //lat = new TimeSpan(2 / (level.level + 1) * 10000000);
            tSpinLimit = new TimeSpan(1000000 * 3); // TSPIN TIME
            tSpinCur = TimeSpan.Zero;
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
            board.Update(gameTime);
            cur += gameTime.ElapsedGameTime;

            if (TagManager.Instance.NextTagIsPlace(playerType))
                hud.setNext(tetriminoFactory.getAndSetNextTetriminoFromId(TagManager.Instance.getNextTag(playerType), playerType), playerType);
//ACTIVE TETRIMiNO
            if (cur > lat)
            {
                cur = TimeSpan.Zero;
                if (tetriminoCanGoingDown())
                    currTetrimino.PosRel = new Vector2(currTetrimino.PosRel.X, currTetrimino.PosRel.Y + 1f);
            }
            if (!tetriminoCanGoingDown())
            {
                if (tSpinCur == TimeSpan.Zero)
                    tSpinCur = TimeSpan.Zero + gameTime.ElapsedGameTime;
                else
                    tSpinCur += gameTime.ElapsedGameTime;
                if (tSpinCur > tSpinLimit)
                {
                    SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.DROP, 0, 0.5f, false);
                    death = board.pushBlocks(currTetrimino, climby.DeadZone);
//END -- ACTIVE TETRIMiNO
//BOARD
                    #region FullLine Event
                    List<int> brokenLines = board.checkFullLine();
                    climby.stepDown(aroundRect, board.CamUp);
                    if (brokenLines.Count > 0) // Happens when lines are borken
                    {
                        Point climbyRelPos = climby.getRelPos();
                        score.addLineScore(brokenLines.Count * brokenLines.Count);
                        hud.setScore(score.TotalScore, playerType);
                        int nbDown = 0;
                        foreach (int l in brokenLines)
                            if (climbyRelPos.Y < l)
                                nbDown++;
                        if (nbDown > 0)
                            climby.stepDown(aroundRect, nbDown);
                    }
                    #endregion
                    if (climby.State != Climby.EState.CLIMB && climby.ActualPosition.Bottom > Constants.Measures.upBoardMargin + Constants.Measures.boardHeight)
                        death = true;
                    var tmp = tetriminoFactory.getTetrimino(playerType);
                    currTetrimino = tmp.Item1;
                    shadowTetrimino = tmp.Item2;
                    hud.setNext(TetriminoFactory.Instance.getNextTetrimino(playerType), playerType);
                    tSpinCur = TimeSpan.Zero;
                }
            }

            //state[climby.State]();
            climby.Update(gameTime);
            //
            state[climby.State]();
            // INVERTED UPDATE AND STATECHANGE
            if (lastDir != climby.Direction ||
                climby.State == Climby.EState.MOVE ||
                climby.State == Climby.EState.FREE_FALL)
                updateAroundRects();
            lastDir = climby.Direction;
            var tmpClimbyStepHeight =  climby.OldMinHeight - climby.MinHeight;
            if (tmpClimbyStepHeight > 0)
            {
                score.addClimbyScore(tmpClimbyStepHeight);
                hud.setScore(score.TotalScore, playerType);
            }
            if (level.updateLevel(tmpClimbyStepHeight))
            {
                hud.setLevel(level.level, playerType);
                //lat = new TimeSpan(10000000 / (level.level + 1));
                //lat = new TimeSpan(2 / ((level.level + 1) * 11) * 1000000000);
                lat = new TimeSpan((10 - level.level) * 1000000);
                climby.setSpeedFromLevel(level.level);
            }
            projectShadow();
        }

        private void projectShadow()
        {
            var tmp = new Vector2(currTetrimino.PosRel.X, currTetrimino.PosRel.Y);
            shadowTetrimino.PosRel = tmp;
            shadowTetrimino.copyOrientation(currTetrimino);
            dropDownTarget(shadowTetrimino);
        }

        public override void Draw(GameTime gameTime)
        {
 	         base.Draw(gameTime);
             board.Draw(gameTime);
             if (SettingsManager.Instance.Shadow)
                shadowTetrimino.Draw(gameTime);
             currTetrimino.Draw(gameTime);
             climby.Draw(gameTime);

             // DEBUG COLORS
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.FRONT]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.FRONT_TOP]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.TOP]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(board.getRect(aroundRect[Climby.EAroundSquare.UNDER]), Color.Red);
             //SpriteManager.Instance.drawRectangleAbsPos(climby.ActualPosition, Color.Blue);
             //SpriteManager.Instance.drawRectangleAbsPos(climby.DeadZone, Color.Green);
             //UNCOMMENT THIS BLOCK TO SEE ALL THE HITBOXES
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
                SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.SHIFT);
        }

        public void leftShift()
        {
            currTetrimino.leftShift();
            if (kickIt(1) == false)
                currTetrimino.rightShift();
            else
                SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.SHIFT);
        }

        public void dropDownTarget(ATetrimino target)
        {
            while (targetCanGoingDown(target))
                target.downMove();
        }

        public void dropDown()
        {
            dropDownTarget(currTetrimino);
            SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.FASTDROP, 0, 0.5f, true);
        }

        public bool rightMove()
        {
            List<Block> shapes = currTetrimino.getBlocks();
            foreach (Block b in shapes)
            {
                if (currTetrimino.PosRel.X + b.PosRel.X >= Constants.Measures.boardBlockWidth - 1 ||
                    board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X + 1, currTetrimino.PosRel.Y + b.PosRel.Y)))
                    return false;
            }
            currTetrimino.rightMove();
            return true;
        }

        public bool leftMove()
        {
            List<Block> shapes = currTetrimino.getBlocks();
            foreach (Block b in shapes)
            {
                if (currTetrimino.PosRel.X + b.PosRel.X <= 0 ||
                    board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X - 1, currTetrimino.PosRel.Y + b.PosRel.Y)))
                    return false;
            }
            currTetrimino.leftMove();
            return true;
        }

        public bool targetCanGoingDown(ATetrimino target)
        {
            List<Block> shapes = target.getBlocks();
            foreach (Block b in shapes)
            {
                if (target.PosRel.Y + b.PosRel.Y > Constants.Measures.boardBlockHeight - 2)
                    return false;
                if (board.isBusyCase(new Vector2(target.PosRel.X + b.PosRel.X, target.PosRel.Y + b.PosRel.Y + 1)))
                    return false;
            }
            return true;
        }

        public bool tetriminoCanGoingDown()
        {
            return targetCanGoingDown(currTetrimino);
            //List<Block> shapes = currTetrimino.getBlocks();
            //foreach (Block b in shapes)
            //{
            //    if (currTetrimino.PosRel.Y + b.PosRel.Y > Constants.Measures.boardBlockHeight -2)
            //        return false;
            //    if (board.isBusyCase(new Vector2(currTetrimino.PosRel.X + b.PosRel.X, currTetrimino.PosRel.Y + b.PosRel.Y + 1)))
            //        return false;
            //}
            //return true;
        }
        #endregion
        #region Climby Action
        private void climbyFall()
        {
            if (!board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).IsEmpty ||
                !board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER]).IsEmpty ||
                board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER]).IsEmpty) // SI UNE DES CONDITIONS DU FALL N'EST PLUS REMPLIE 
                climby.State = Climby.EState.MOVE;                                  // ON CHANGE D'ETAT POUR MOVE
            else if ((climby.Direction == Climby.EDirection.RIGHT && climby.ActualPosition.Left >= board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Right) ||
                    (climby.Direction == Climby.EDirection.LEFT && climby.ActualPosition.Right <= board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Left)) // APRES ETRE TOTALEMENT AU DESSUS DU VIDE
                climby.State = Climby.EState.FREE_FALL;                                                                                         // ON CHANGE D'ETAT POUR FREE FALL
        }

        private void climbyFreeFall()
        {
            if (climby.ActualPosition.Bottom >= board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Top && !board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).IsEmpty)                          //ON TOMBE JUSQU'A CE QU'ON TROUVE UN BLOCK ET QUE L'ON SOIT JUSTE AU DESSUS DE CE BLOCK
                climby.State = Climby.EState.MOVE;
        }

        private void climbyClimb()
        {
            if (board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).IsEmpty || !board.getRect(aroundRect[Climby.EAroundSquare.TOP]).IsEmpty || !board.getRect(aroundRect[Climby.EAroundSquare.FRONT_TOP]).IsEmpty)  // SI LES CONDITIONS DE MONTEES NE SONT PLUS REMPLIES
                climby.State = Climby.EState.FREE_FALL;                                                                                                                  // ON TOMBE
            else if ((climby.Direction == Climby.EDirection.RIGHT   && climby.ActualPosition.Center.X >= board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).Left) ||                     // SI ON EST ARRIVE AU DESSUS ET AU MILIEU DU BLOCK FRONT
                     (climby.Direction == Climby.EDirection.LEFT    && climby.ActualPosition.Center.X <= board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).Right))
                climby.State = Climby.EState.MOVE;                                                                                                                       // ON CHANGE D'ETAT POUR MOVE
            else if (climby.ActualPosition.Bottom <= board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).Top)                                                                         // SI ON EST ARRIVE AU DESSUS ET A GAUCHE DU BLOCK FRONT
                climby.State = Climby.EState.END_CLIMB;                                                                                                                  // ON CHANGE D'ETAT POUR END_CLIMB (POUR ALLER AU MILIEU DU BLOCK FRONT)
        }

        private void climbyMove()
        {
            if (climby.ActualPosition.Bottom < board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Top)                                                              // LE BLOCK DU DESSOUS EST VIDE 
                climby.State = Climby.EState.FREE_FALL;                                                                                                 // DONC TOMBE
            else if ((climby.Direction == Climby.EDirection.RIGHT && climby.ActualPosition.Right >= board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Right) ||  // SINON
                     (climby.Direction == Climby.EDirection.LEFT && climby.ActualPosition.Left <= board.getRect(aroundRect[Climby.EAroundSquare.UNDER]).Left))      // ON VERIFIE LES CHOSES SUIVANTES QUE SI ON EST A L EXTREMITE DE NOTRE CHEMIN
            {
                if (!board.getRect(aroundRect[Climby.EAroundSquare.FRONT]).IsEmpty)
                {// TOUCHE UN MUR
                    if (board.getRect(aroundRect[Climby.EAroundSquare.FRONT_TOP]).IsEmpty && board.getRect(aroundRect[Climby.EAroundSquare.TOP]).IsEmpty)                 // SI LA VOIX EST LIBRE EN HAUT
                        climby.State = Climby.EState.CLIMB;                                                                                 // ON MONTE
                    else                                                                                                                    // SINON
                        climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;   // ON SE RETOURNE
                }
                else if (board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER]).IsEmpty)
                {// FACE A UN FOSSE
                    if (!board.getRect(aroundRect[Climby.EAroundSquare.FRONT_UNDER_UNDER]).IsEmpty)                                                        // SI PROFOND QUE DE UNE CASE
                        climby.State = Climby.EState.FALL;                                                                                  // ON DESCEND
                    else                                                                                                                    // SINON
                        climby.Direction = Climby.EDirection.LEFT == climby.Direction ? Climby.EDirection.RIGHT : Climby.EDirection.LEFT;   // ON SE RETOURNE
                }
            }
        }

        private void climbyStop()
        {
            //climbyMove();
        }

        private void updateAroundRects()
        {
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.FRONT, climby.getIntOrt(), 0);
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.FRONT_TOP, climby.getIntOrt(), -1);
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.FRONT_UNDER, climby.getIntOrt(), 1);
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.FRONT_UNDER_UNDER, climby.getIntOrt(), 2);
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.TOP, 0, -1);
            setPoint(climby.ActualPosition.Center, Climby.EAroundSquare.UNDER, 0, 1);
        }

        private void setPoint(Point point,  Climby.EAroundSquare e, int padX = 0, int padY = 0)
        {
            Point p = aroundRect[e];

            p.X = (point.X - (int)CoordHelper.Instance.getLeftMargin(playerType)) / (int)Constants.Measures.blockSize + padX;
            p.Y = (point.Y - (int)Constants.Measures.upBoardMargin) / (int)Constants.Measures.blockSize + padY;
            aroundRect[e] = p;
        }
        #endregion

        public void addInfluence(float p)
        {
            climby.setInfluence(p);
        }
    }
}
