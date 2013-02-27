﻿using System;
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

        public PlayerControl(AScene target):base(App.Game)
        {
            this.target = target;
            pause = new GameButton(SpriteManager.ESprite.PAUSE, CoordHelper.Instance.pause, pauseGame);
            left = new GameButton(SpriteManager.ESprite.LEFT, new Vector2(CoordHelper.Instance.getLeftMargin(CoordHelper.EProfile.ONEPLAYER) - Constants.Measures.buttonSize - Constants.Measures.upBoardMargin - Constants.Measures.borderSize,
                                                                          Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize), leftArrow);
            right = new GameButton(SpriteManager.ESprite.RIGHT, new Vector2(CoordHelper.Instance.getLeftMargin(CoordHelper.EProfile.ONEPLAYER) + Constants.Measures.boardWidth + Constants.Measures.upBoardMargin,
                                                                            Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize), rightArrow);
        }

        public override void Update(GameTime gameTime)
        {
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
            MenuManager.Instance.CreatePauseMenu(target);
            target.TogglePause();
        }

        public void rightArrow(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                Console.Out.WriteLine("Right Holder");
        }

        public void leftArrow(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                Console.Out.WriteLine("Left Holder");
        }
    }
}
