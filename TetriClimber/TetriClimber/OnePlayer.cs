using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class OnePlayer : APlay
    {
        private GameSession player1;

        public OnePlayer():base()
        {
            player1 = new GameSession(SpriteManager.ESprite.CLIMBYBLUE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.ElapsedGameTime != TimeSpan.Zero)
            {
                if (cur >= turnLat)
                    cur = new TimeSpan(0);
                if (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) > lat && cur == TimeSpan.Zero))
                    player1.rightMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
                    player1.rightShift();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.UP) == gameTime.ElapsedGameTime ||
                     (App.ToucheInput.getDownTime(AUserInput.EInput.UP) > lat && cur == TimeSpan.Zero))
                    player1.leftShift();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) > lat && cur == TimeSpan.Zero))
                    player1.leftMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) > lat && cur == TimeSpan.Zero))
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
