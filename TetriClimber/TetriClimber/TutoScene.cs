using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class TutoScene : AScene
    {
        private TimeSpan displayTime;
        private SpriteManager.ESprite currentSlide;
        public TutoScene()
            : base()
        {
            displayTime = new TimeSpan(0, 0, 18);
            currentSlide = SpriteManager.ESprite.TUTORIAL_P1;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawAtPos(currentSlide, Vector2.Zero);
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
                        if (displayTime <= new TimeSpan(0, 0, 6))
                        {
                            SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TUTO);
                            SceneManager.Instance.requestAddScene(SceneManager.EScene.LEADER_BOARD, new LeaderBoardScene());
                            displayTime = TimeSpan.Zero;
                        }
                        else if (displayTime <= new TimeSpan(0, 0, 12))
                        {
                            currentSlide = SpriteManager.ESprite.TUTORIAL_P3;
                            displayTime = new TimeSpan(0, 0, 6);
                        }
                        else
                        {
                            currentSlide = SpriteManager.ESprite.TUTORIAL_P2;
                            displayTime = new TimeSpan(0, 0, 12);
                        }
                        return;
                    }
            }
            else
            {
                if ((App.UserInput as TouchInput).hasTapEvent)
                {
                    if (displayTime <= new TimeSpan(0, 0, 6))
                    {
                        SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TUTO);
                        SceneManager.Instance.requestAddScene(SceneManager.EScene.LEADER_BOARD, new LeaderBoardScene());
                        displayTime = TimeSpan.Zero;
                    }
                    else if (displayTime <= new TimeSpan(0, 0, 12))
                    {
                        currentSlide = SpriteManager.ESprite.TUTORIAL_P3;
                        displayTime = new TimeSpan(0, 0, 6);
                    }
                    else
                    {
                        currentSlide = SpriteManager.ESprite.TUTORIAL_P2;
                        displayTime = new TimeSpan(0, 0, 12);
                    }
                    return;
                }
            }
            //
            if (displayTime <= TimeSpan.Zero)
            {
                SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TUTO);
                SceneManager.Instance.requestAddScene(SceneManager.EScene.LEADER_BOARD, new LeaderBoardScene());
            }
            else if (displayTime <= new TimeSpan(0, 0, 6))
            {
                currentSlide = SpriteManager.ESprite.TUTORIAL_P3;
            }
            else if (displayTime <= new TimeSpan(0, 0, 12))
            {
                currentSlide = SpriteManager.ESprite.TUTORIAL_P2;
            }
        }
    }
}
