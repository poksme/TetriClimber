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
            displayTime = new TimeSpan(0, 0, 1); // DISPLAYS FOR 5 SECONDS
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle(0, 0, 100, 100), Color.Green);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            displayTime -= gameTime.ElapsedGameTime;
            if (displayTime <= TimeSpan.Zero)
            {
                SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TITLE);
                SceneManager.Instance.requestAddScene(SceneManager.EScene.TUTO, new TutoScene());
            }
        }
    }
}
