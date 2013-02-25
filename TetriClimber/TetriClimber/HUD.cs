using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class HUD : DrawableGameComponent
    {
        GameString nextStr;
        GameString scoreStr;
        GameString levelStr;
        Rectangle nextBox;
        Rectangle scoreBox;
        Rectangle levelBox;

        List<ATetrimino> nextValue = new List<ATetrimino>();
        List<GameString> scoreValue = new List<GameString>();
        List<GameString> levelValue = new List<GameString>();
        List<Rectangle> nextValBox = new List<Rectangle>();
        List<Rectangle> scoreValBox = new List<Rectangle>();
        List<Rectangle> levelValBox = new List<Rectangle>();


        public HUD():
            base(App.Game)
        {
            nextStr = new GameString("NEXT", TextManager.EFont.AHARONI, Constants.Color.background, 0.46f);
            nextStr.Pos = CoordHelper.Instance.getNextText(nextStr);

            scoreStr = new GameString("SCORE", TextManager.EFont.AHARONI, Constants.Color.background, 0.46f);
            scoreStr.Pos = CoordHelper.Instance.getScoreText(scoreStr);

            levelStr = new GameString("LEVEL", TextManager.EFont.AHARONI, Constants.Color.background, 0.46f);
            levelStr.Pos = CoordHelper.Instance.getLevelText(levelStr);

            nextBox = CoordHelper.Instance.getNextTextBox(nextStr);
            scoreBox = CoordHelper.Instance.getScoreTextBox(scoreStr);
            levelBox = CoordHelper.Instance.getLevelTextBox(levelStr);

            var tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f);
            tmp.Pos = CoordHelper.Instance.getScoreValue(tmp, CoordHelper.EProfile.ONEPLAYER);
            scoreValue.Add(tmp);
            tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f);
            tmp.Pos = CoordHelper.Instance.getLevelValue(tmp, CoordHelper.EProfile.ONEPLAYER);
            levelValue.Add(tmp);
            scoreValBox.Add(CoordHelper.Instance.getScoreValueBox(scoreValue.Last(), CoordHelper.EProfile.ONEPLAYER));
            levelValBox.Add(CoordHelper.Instance.getLevelValueBox(levelValue.Last(), CoordHelper.EProfile.ONEPLAYER));
            nextValBox.Add(CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.ONEPLAYER));
            if (CoordHelper.Instance.Profile == CoordHelper.EProfile.TWOPLAYER)
            {
                tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p2Dark, 0.8f);
                tmp.Pos = CoordHelper.Instance.getScoreValue(tmp, CoordHelper.EProfile.TWOPLAYER);
                scoreValue.Add(tmp);
                tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p2Dark, 0.8f);
                tmp.Pos = CoordHelper.Instance.getLevelValue(tmp, CoordHelper.EProfile.TWOPLAYER);
                levelValue.Add(tmp);
                scoreValBox.Add(CoordHelper.Instance.getScoreValueBox(scoreValue.Last(), CoordHelper.EProfile.TWOPLAYER));
                levelValBox.Add(CoordHelper.Instance.getLevelValueBox(levelValue.Last(), CoordHelper.EProfile.TWOPLAYER));
                nextValBox.Add(CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.TWOPLAYER));

            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteManager.Instance.drawRectangleAbsPos(nextBox, Constants.Color.border);
            SpriteManager.Instance.drawRectangleAbsPos(scoreBox, Constants.Color.border);
            SpriteManager.Instance.drawRectangleAbsPos(levelBox, Constants.Color.border);

            TextManager.Instance.Draw(nextStr);
            TextManager.Instance.Draw(scoreStr);
            TextManager.Instance.Draw(levelStr);

            foreach (Rectangle rec in nextValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (Rectangle rec in scoreValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (Rectangle rec in levelValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (ATetrimino t in nextValue)
                t.DrawAsPreview(gameTime);
            foreach (GameString gs in scoreValue)
                TextManager.Instance.Draw(gs);
            foreach (GameString gs in levelValue)
                TextManager.Instance.Draw(gs);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void setScore(int p, CoordHelper.EProfile profile)
        {
            if (profile == CoordHelper.EProfile.ONEPLAYER)
                scoreValue.First().Value = p.ToString();
            else
                scoreValue.Last().Value = p.ToString();
        }
    }
}
