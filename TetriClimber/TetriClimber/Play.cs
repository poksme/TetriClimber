using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class Play : AScene
    {
        private GameSession player1;
        private PlayerControl control;

        private TimeSpan lat = new TimeSpan(10000000 / 2);
        private TimeSpan cur = new TimeSpan(0);
        private TimeSpan turnLat = new TimeSpan(10000000 / 10);

        public Play() : base()
        {
            player1 = new GameSession();
            control = new PlayerControl();
            SoundManager.Instance.bgmPlay();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player1.Draw(gameTime);
            control.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cur += gameTime.ElapsedGameTime;
            if (gameTime.ElapsedGameTime != TimeSpan.Zero)
            {
                if (cur >= turnLat)
                    cur = new TimeSpan(0);
                if (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) > lat && cur == TimeSpan.Zero))
                    player1.rightMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) > lat && cur == TimeSpan.Zero))
                    player1.rightShift();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) > lat && cur == TimeSpan.Zero))
                    player1.leftMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
                    player1.dropDown();
            }
            player1.Update(gameTime);
            control.Update(gameTime);
        }
    }
}
