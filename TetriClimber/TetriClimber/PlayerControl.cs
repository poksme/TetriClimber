using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class PlayerControl : DrawableGameComponent
    {
        public delegate void HandlerAction(ButtonState btn);
        private GameButton pause;
        private GameButton left;
        private GameButton right;
        private AScene target;
        public float rightSpeed = 0;
        public float leftSpeed = 0;

        public PlayerControl(AScene target):base(App.Game)
        {
            this.target = target;
            pause = new GameButton(SpriteManager.ESprite.PAUSE, CoordHelper.Instance.pause, pauseGame);
            left = new GameButton(SpriteManager.ESprite.LEFT, CoordHelper.Instance.leftArrow, leftArrow);
            right = new GameButton(SpriteManager.ESprite.RIGHT, CoordHelper.Instance.rightArrow, rightArrow);
        }

        public override void Update(GameTime gameTime)
        {
            rightSpeed = 0;
            leftSpeed = 0;
            pause.Update(gameTime);
            left.Update(gameTime);
            right.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            pause.Draw(gameTime);
            left.Draw(gameTime);
            right.Draw(gameTime);
        }

        public void pauseGame(ButtonState state)
        {
            if (state == ButtonState.Released)
            {
                if (!target.IsPause)
                    MenuManager.Instance.CreatePauseMenu(target);
                else
                    MenuManager.Instance.Flush();
                target.TogglePause();
            }
        }

        public void rightArrow(ButtonState state)
        {
            rightSpeed = 1f;
        }

        public void leftArrow(ButtonState state)
        {
            leftSpeed = -1f;
        }
    }
}
