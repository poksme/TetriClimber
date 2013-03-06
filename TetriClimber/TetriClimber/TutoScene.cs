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
        public TutoScene()
            : base()
        {
            displayTime = new TimeSpan(0, 0, 5);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle(0, 0, 100, 100), Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            displayTime -= gameTime.ElapsedGameTime;
            if (displayTime <= TimeSpan.Zero)
            {
                SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TUTO);
                SceneManager.Instance.requestAddScene(SceneManager.EScene.LEADER_BOARD, new LeaderBoardScene());
            }
        }
    }
}
