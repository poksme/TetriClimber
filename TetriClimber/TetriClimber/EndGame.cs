using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Surface.Core;
using Microsoft.Surface;

namespace TetriClimber
{
    public class EndGame : AScene
    {
        private Rectangle scoredBox;
        private GameString scored;

        private Rectangle nameBox;
        private GameString name;

        private Score score;
        private Rectangle scoreBox;
        private GameString scoreStr;
        private GameString who;
        private TextBox tb;

        public EndGame(Score s, CoordHelper.EProfile player) :
            base()
        {
            Color ph;
            Color tbcolor;
            score = s;
            if (player == CoordHelper.EProfile.TWOPLAYER)
            {
                tbcolor = Constants.Color.p2Dark;
                who = new GameString("RED", TextManager.EFont.AHARONI, tbcolor, 0.8f);
                ph = Constants.Color.p2Light;
            }
            else if (CoordHelper.Instance.Profile == CoordHelper.EProfile.TWOPLAYER)
            {
                tbcolor = Constants.Color.p1Dark;
                who = new GameString("BLUE", TextManager.EFont.AHARONI, tbcolor, 0.8f);
                ph = Constants.Color.p1Light;
            }
            else
            {
                tbcolor = Constants.Color.p1Dark;
                who = new GameString("YOU", TextManager.EFont.AHARONI, tbcolor, 0.8f);
                ph = Constants.Color.p1Light;
            }

            scored = new GameString(" SCORED", TextManager.EFont.AHARONI, Color.White, 0.8f);
            who.Pos = new Vector2((Constants.Measures.landscapeWidth - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, who.Value+scored.Value).X * who.Scale) / 2,
                            Constants.Measures.upBoardMargin + 130);
            scored.Pos = new Vector2(who.Pos.X + TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, who.Value).X * who.Scale,
                                        Constants.Measures.upBoardMargin + 130);
            scoredBox = new Rectangle((int)(who.Pos.X - 50),
                                          (int)(scored.Pos.Y - 40),
                                          (int)(TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, who.Value + scored.Value).X * scored.Scale + 100),
                                          (int)(TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, who.Value + scored.Value).Y * scored.Scale + 40));

            scoreStr = new GameString(score.TotalScore.ToString(), TextManager.EFont.AHARONI, who.Color);
            scoreStr.Pos = new Vector2((Constants.Measures.landscapeWidth - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI,scoreStr.Value).X) / 2,
                                        scoredBox.Bottom + 5);

            scoreBox = new Rectangle((int)(Constants.Measures.landscapeWidth - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, scoreStr.Value).X) / 2 - 30,
                                        scoredBox.Y + scoredBox.Height, 
                                        (int)TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, scoreStr.Value).X + 60,
                                        (int)TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, scoreStr.Value).Y);
            name = new GameString("ENTER YOUR NAME", TextManager.EFont.AHARONI, Color.White, 0.8f);
            name.Pos = new Vector2((Constants.Measures.landscapeWidth - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, name.Value).X * name.Scale) / 2,
                                        scoreBox.Bottom + 200);
            nameBox = new Rectangle((int)name.Pos.X - 50, (int)name.Pos.Y - 40,
                (int)(TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, name.Value).X * name.Scale + 100),
                (int)(TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, name.Value).Y * name.Scale + 40));
            tb = new TextBox(saveScore, tbcolor, ph, new Rectangle((int)(Constants.Measures.landscapeWidth - 400) / 2, (int)nameBox.Bottom, 400, 150));

            SurfaceKeyboard.CenterX = (float)InteractiveSurface.PrimarySurfaceDevice.Width - (SurfaceKeyboard.Width / 2);
            SurfaceKeyboard.CenterY = (float)InteractiveSurface.PrimarySurfaceDevice.Height - (SurfaceKeyboard.Height / 2);
            SurfaceKeyboard.Layout = Microsoft.Surface.KeyboardLayout.Alphanumeric;
            SurfaceKeyboard.ShowsFeedback = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tb.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteManager.Instance.drawRectangleAbsPos(scoredBox, Constants.Color.border);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(scoreBox,Color.White, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(who);
            TextManager.Instance.Draw(scored);
            TextManager.Instance.Draw(scoreStr);

            SpriteManager.Instance.drawRectangleAbsPos(nameBox, Constants.Color.border);
            TextManager.Instance.Draw(name);
            tb.Draw(gameTime);
        }

        public void saveScore(String pseudo)
        {
            score.setPseudo(pseudo);
            ScoreBoard.Instance.addScore(score);
            ScoreBoard.Instance.Dump();
            SceneManager.Instance.requestRemoveScene(SceneManager.EScene.END_GAME);
            MenuManager.Instance.launchMenu(MenuManager.EMenu.MAIN);
            TagManager.Instance.reset();
        }
    }
}
