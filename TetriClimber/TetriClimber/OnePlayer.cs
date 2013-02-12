using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Surface;

namespace TetriClimber
{
    class OnePlayer : APlay
    {
        private GameSession player1;

        public OnePlayer():base()
        {
            player1 = new GameSession(SpriteManager.ESprite.CLIMBYBLUE);
            if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
            {
                //Constants.Measures.leftBoardMargin = (float)Math.Round((Constants.Measures.portraitWidth - Constants.Measures.boardBlockWidth * Constants.Measures.blockSize) / 2f);
                //Constants.Measures.upBoardMargin = (float)Math.Round((Constants.Measures.portraitHeight - Constants.Measures.boardBlockHeight * Constants.Measures.blockSize) / 2f);
                App.screenTransform = Matrix.CreateRotationZ(MathHelper.ToRadians(90)) *
                                    //Matrix.CreateTranslation(App.Game.GraphicsDevice.Viewport.Width, 0, 0);
                                    Matrix.CreateTranslation(Constants.Measures.portraitWidth - 950, 0, 0) * Matrix.CreateScale(new Vector3(Constants.Measures.Scale, Constants.Measures.Scale, 1)); // -500 -250, 1.75 1.75
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            TouchInput ipt = null;
            if (App.UserInput is TouchInput)
                ipt = App.UserInput as TouchInput;
            else
                (App.UserInput as KeyboardInput).setKeyRepeatTime(new TimeSpan(1500000));
            if (gameTime.ElapsedGameTime != TimeSpan.Zero)
            {
                if (cur >= turnLat)
                    cur = new TimeSpan(0);
                //if (App.UserInput.getDownTime(AUserInput.EInput.RIGHT) == gameTime.ElapsedGameTime ||
                //    (App.UserInput.getDownTime(AUserInput.EInput.RIGHT) > lat && cur == TimeSpan.Zero))
                if (App.UserInput.isPressed(AUserInput.EInput.RIGHT))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player1.rightMove() ? 1 : 0);
                    else
                        player1.rightMove();
                }
                //if (App.UserInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                //    (App.UserInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
                //    player1.rightShift();
                //if (App.UserInput.getDownTime(AUserInput.EInput.UP) == gameTime.ElapsedGameTime ||
                //     (App.UserInput.getDownTime(AUserInput.EInput.UP) > lat && cur == TimeSpan.Zero))
                //    player1.leftShift();
                //if (App.UserInput.getDownTime(AUserInput.EInput.LEFT) == gameTime.ElapsedGameTime ||
                //    (App.UserInput.getDownTime(AUserInput.EInput.LEFT) > lat && cur == TimeSpan.Zero))
                if (App.UserInput.isPressed(AUserInput.EInput.LEFT))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player1.leftMove() ? -1 : 0);
                    else
                        player1.leftMove();
                }
                if (App.UserInput.isPressed(AUserInput.EInput.DOWN))
                    player1.rightShift();
                if (App.UserInput.isPressed(AUserInput.EInput.UP))
                    player1.leftShift();
                //if (App.UserInput.getDownTime(AUserInput.EInput.TAP) == gameTime.ElapsedGameTime ||
                //    (App.UserInput.getDownTime(AUserInput.EInput.TAP) > lat && cur == TimeSpan.Zero))
                if (App.UserInput.isPressed(AUserInput.EInput.TAP))
                    player1.dropDown();
            }
            player1.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player1.Draw(gameTime);
        }
    }
}
