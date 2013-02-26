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

        Dictionary<CoordHelper.EProfile, ATetrimino> nextValue = new Dictionary<CoordHelper.EProfile, ATetrimino>();
        Dictionary<CoordHelper.EProfile, GameString> scoreValue = new Dictionary<CoordHelper.EProfile, GameString>();
        Dictionary<CoordHelper.EProfile, GameString> levelValue = new Dictionary<CoordHelper.EProfile, GameString>();
        Dictionary<CoordHelper.EProfile, Rectangle> nextValBox = new Dictionary<CoordHelper.EProfile, Rectangle>();
        Dictionary<CoordHelper.EProfile, Rectangle> scoreValBox = new Dictionary<CoordHelper.EProfile, Rectangle>();
        Dictionary<CoordHelper.EProfile, Rectangle> levelValBox = new Dictionary<CoordHelper.EProfile, Rectangle>();


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
            scoreValue.Add(CoordHelper.EProfile.ONEPLAYER, tmp);
            tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f);
            tmp.Pos = CoordHelper.Instance.getLevelValue(tmp, CoordHelper.EProfile.ONEPLAYER);
            levelValue.Add(CoordHelper.EProfile.ONEPLAYER, tmp);
            scoreValBox.Add(CoordHelper.EProfile.ONEPLAYER, CoordHelper.Instance.getScoreValueBox(scoreValue[CoordHelper.EProfile.ONEPLAYER], CoordHelper.EProfile.ONEPLAYER));
            levelValBox.Add(CoordHelper.EProfile.ONEPLAYER, CoordHelper.Instance.getLevelValueBox(levelValue[CoordHelper.EProfile.ONEPLAYER], CoordHelper.EProfile.ONEPLAYER));
            nextValBox.Add(CoordHelper.EProfile.ONEPLAYER, CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.ONEPLAYER));
            if (CoordHelper.Instance.Profile == CoordHelper.EProfile.TWOPLAYER)
            {
                tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p2Dark, 0.8f);
                tmp.Pos = CoordHelper.Instance.getScoreValue(tmp, CoordHelper.EProfile.TWOPLAYER);
                scoreValue.Add(CoordHelper.EProfile.TWOPLAYER, tmp);
                tmp = new GameString("0", TextManager.EFont.AHARONI, Constants.Color.p2Dark, 0.8f);
                tmp.Pos = CoordHelper.Instance.getLevelValue(tmp, CoordHelper.EProfile.TWOPLAYER);
                levelValue.Add(CoordHelper.EProfile.TWOPLAYER, tmp);
                scoreValBox.Add(CoordHelper.EProfile.TWOPLAYER, CoordHelper.Instance.getScoreValueBox(scoreValue[CoordHelper.EProfile.TWOPLAYER], CoordHelper.EProfile.TWOPLAYER));
                levelValBox.Add(CoordHelper.EProfile.TWOPLAYER, CoordHelper.Instance.getLevelValueBox(levelValue[CoordHelper.EProfile.TWOPLAYER], CoordHelper.EProfile.TWOPLAYER));
                nextValBox.Add(CoordHelper.EProfile.TWOPLAYER, CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.TWOPLAYER));

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

            foreach (KeyValuePair<CoordHelper.EProfile, Rectangle> rec in nextValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec.Value, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (KeyValuePair<CoordHelper.EProfile, Rectangle> rec in scoreValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec.Value, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (KeyValuePair<CoordHelper.EProfile, Rectangle> rec in levelValBox)
                SpriteManager.Instance.drawBoardedRectangleAbsPos(rec.Value, Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            foreach (KeyValuePair<CoordHelper.EProfile, ATetrimino> t in nextValue)
                t.Value.DrawAsPreview(gameTime);
            foreach (KeyValuePair<CoordHelper.EProfile, GameString> gs in scoreValue)
                TextManager.Instance.Draw(gs.Value);
            foreach (KeyValuePair<CoordHelper.EProfile, GameString> gs in levelValue)
                TextManager.Instance.Draw(gs.Value);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void setScore(int p, CoordHelper.EProfile profile)
        {
            scoreValue[profile].Value = p.ToString();
            scoreValue[profile].Pos = CoordHelper.Instance.getScoreValue(scoreValue[profile], profile);
            scoreValBox[profile] = CoordHelper.Instance.getScoreValueBox(scoreValue[profile], profile);
        }

        public void setLevel(int p, CoordHelper.EProfile profile)
        {
            levelValue[profile].Value = p.ToString();
            levelValue[profile].Pos = CoordHelper.Instance.getScoreValue(levelValue[profile], profile);
            levelValBox[profile] = CoordHelper.Instance.getLevelValueBox(levelValue[profile], profile);
        }

        public void setNext(ATetrimino t, CoordHelper.EProfile profile)
        {
            nextValue[profile] = t;
        }
    }
}
