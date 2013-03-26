using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class LeaderBoardScene : AScene
    {
        private TimeSpan displayTime;
        public LeaderBoardScene()
            : base()
        {
            displayTime = new TimeSpan(0, 0, 10);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle(
                (int)Constants.Measures.upBoardMargin, 
                (int)Constants.Measures.upBoardMargin, 
                1920 - (int)Constants.Measures.upBoardMargin * 2, 
                1080 - (int)Constants.Measures.upBoardMargin * 2), Constants.Color.background * 0.8f);
            TextManager.Instance.Draw(new GameString("LEADER BOARD", TextManager.EFont.AHARONI, Constants.Color.p1Dark, 0.8f, new Vector2(600f, Constants.Measures.upBoardMargin)));
            for (int i = 0; i < 10 && i < ScoreBoard.Instance.getScores().Count; i++)
            {
                TextManager.Instance.Draw(new GameString((i + 1).ToString() + ". " + ScoreBoard.Instance.getScores()[i].pseudo,
                                            TextManager.EFont.AHARONI, Constants.Color.border, 0.8f,
                                            new Vector2(Constants.Measures.upBoardMargin + 10, 150 + i * 85)));
                var tmp = new GameString(ScoreBoard.Instance.getScores()[i].TotalScore.ToString(), TextManager.EFont.AHARONI, Constants.Color.border, 0.8f);
                tmp.Pos = new Vector2(1920 - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, tmp.Value).X - Constants.Measures.upBoardMargin * 2, 150 + i * 85);
                TextManager.Instance.Draw(tmp);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            displayTime -= gameTime.ElapsedGameTime;
            //
            if (App.UserInput is KeyboardInput)
            {
                foreach (AUserInput.EInputKeys ipt in Enum.GetValues(typeof(AUserInput.EInputKeys)))
                    if (App.UserInput.isPressed(ipt, AUserInput.EGameMode.SOLO))
                    {
                        ModeManager.Instance.TryChangeMode(ModeManager.EMode.GAME_MODE);
                        return;
                    }
            }
            else
            {
                if ((App.UserInput as TouchInput).hasTapEvent)
                {
                    ModeManager.Instance.TryChangeMode(ModeManager.EMode.GAME_MODE);
                    return;
                }
            }
            //
            if (displayTime <= TimeSpan.Zero)
            {
                SceneManager.Instance.requestRemoveScene(SceneManager.EScene.LEADER_BOARD);
                SceneManager.Instance.requestAddScene(SceneManager.EScene.TITLE, new TitleScene());
            }
        }
    }
}
