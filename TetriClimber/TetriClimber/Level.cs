using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Level : DrawableGameComponent
    {
        public int level { get; private set; }
        private int step;
        private GameString levelText;
        private GameString levelValue;

        public Level()
            : base(App.Game)
        {
            level = 0;
            step = 0;
            levelValue = new GameString("8", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f, new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.levelPosY - Constants.Measures.paddingTextY / 2 + Constants.Measures.textBoxH));
            levelText = new GameString("LEVEL", TextManager.EFont.AHARONI, Color.White, 0.46f, new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.levelPosY + Constants.Measures.paddingTextY));
        }

        private int calcLevel(int score, int lvl = 0)
        {
            if (score > ((1200 / 4) * (lvl + 1) * 10))
                return calcLevel(score, lvl + 1);
            else
                return lvl;
        }

        public bool updateLevel(int stepToAdd)
        {
            step +=stepToAdd;
            int tmp = step / 10;
            if (tmp > level)
            {
                level = tmp;
                levelValue.Value = (level).ToString();
                return true;
            }
            return false;
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth), (int)Constants.Measures.levelPosY,
                                            (int)(TextManager.Instance.getSizeString(levelText.Font, levelText.Value).X * levelText.Scale + Constants.Measures.paddingTextX * 2),
                                            (int)Constants.Measures.textBoxH),
                                            Constants.Color.border);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth + Constants.Measures.borderSize), (int)(Constants.Measures.levelPosY + Constants.Measures.textBoxH),
                                                        (int)(TextManager.Instance.getSizeString(levelValue.Font, levelValue.Value).X * levelValue.Scale + Constants.Measures.paddingTextX * 2),
                                                        (int)(TextManager.Instance.getSizeString(levelValue.Font, levelValue.Value).Y * 0.70 * levelValue.Scale)),
                                                        Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(levelText);
            TextManager.Instance.Draw(levelValue);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
