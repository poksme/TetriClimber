using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class TitleScene : AScene
    {
        private TimeSpan displayTime;
        public TitleScene()
            : base()
        {
            displayTime = new TimeSpan(0, 0, 20); // DISPLAYS FOR 5 SECONDS
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawAtPos(SpriteManager.ESprite.LOGO, new Vector2((1920 - 1001) / 2, 200));
            //SpriteManager.Instance.drawRectangleAbsPos(new Rectangle(0, 0, 100, 100), Color.Green);
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
                        SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TITLE);
                        SceneManager.Instance.requestAddScene(SceneManager.EScene.TUTO, new TutoScene());
                        return;
                    }
            }
            else
            {
                if ((App.UserInput as TouchInput).hasTapEvent)
                {
                    SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TITLE);
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.TUTO, new TutoScene());
                    return;
                }
            }
            //
            if (displayTime <= TimeSpan.Zero)
            {
                SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TITLE);
                SceneManager.Instance.requestAddScene(SceneManager.EScene.TUTO, new TutoScene());
            }
        }
    }
}
