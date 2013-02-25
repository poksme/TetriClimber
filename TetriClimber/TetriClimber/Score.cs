using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Score : DrawableGameComponent
    {
        private int lineScore;
        private int climbyScore;
        public int TotalScore { get; private set; }
        public int Level { private get; set; }
        private GameString scoreValue;
        private GameString scoreText;

        public Score(Vector2 p = new Vector2(), Vector2 o = new Vector2()):
            base(App.Game)
        {
            lineScore = 0;
            climbyScore = 0;
            TotalScore = 0;
            Level = 0;
            scoreValue = new GameString("2010", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f, new Vector2(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.scorePosY - Constants.Measures.paddingTextY/2 + Constants.Measures.textBoxH));
            scoreText = new GameString("SCORE", TextManager.EFont.AHARONI, Color.White, 0.46f, new Vector2(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.scorePosY + Constants.Measures.paddingTextY));
        }
        
        public void addLineScore(int ls)
        {
            switch (ls)
            {
                case 1:
                    lineScore += 40 * (Level + 1);
                    break;
                case 2:
                    lineScore += 100 * (Level + 1);
                    break;
                case 3:
                    lineScore += 300 * (Level + 1);
                    break;
                case 4:
                    lineScore += 1200 * (Level + 1);
                    break;
                default:
                    break;
            }
            majTotalScore();
        }

        public void addClimbyScore(int cs)
        {
            climbyScore +=  cs * ((1200 / 4) * (Level + 1));
            majTotalScore();

        }

        public void majTotalScore()
        {
            TotalScore = climbyScore + lineScore;
            scoreValue.Value = (TotalScore).ToString();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth), (int)Constants.Measures.scorePosY,
                                                        (int)(TextManager.Instance.getSizeString(scoreText.Font, scoreText.Value).X * scoreText.Scale + Constants.Measures.paddingTextX * 2),
                                                        (int)Constants.Measures.textBoxH),
                                                        Constants.Color.border);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(new Rectangle((int)(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth + Constants.Measures.borderSize), (int)(Constants.Measures.scorePosY + Constants.Measures.textBoxH),
                                                        (int)(TextManager.Instance.getSizeString(scoreValue.Font, scoreValue.Value).X * scoreValue.Scale + Constants.Measures.paddingTextX * 2),
                                                        (int)(TextManager.Instance.getSizeString(scoreValue.Font, scoreValue.Value).Y * 0.70 * scoreValue.Scale)),
                                                        Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(scoreText);
            TextManager.Instance.Draw(scoreValue);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
