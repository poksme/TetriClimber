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
        private TouchInput ipt;

        public OnePlayer():base()
        {
            player1 = new GameSession(SpriteManager.ESprite.CLIMBYBLUE);
            CoordHelper.Instance.setProfile(CoordHelper.EProfile.ONEPLAYER);
            ipt = null;
            if (App.UserInput is TouchInput)
                ipt = App.UserInput as TouchInput;
            else
                (App.UserInput as KeyboardInput).setKeyRepeatTime(new TimeSpan(1500000));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (cur >= turnLat)
            //    cur = new TimeSpan(0);
            if (App.UserInput.isPressed(AUserInput.EInput.RIGHT))
            {
                if (ipt != null)
                    ipt.recenterStartingPoint(player1.rightMove() ? 1 : 0);
                else
                    player1.rightMove();
            }
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
            if (App.UserInput.isPressed(AUserInput.EInput.TAP))
                player1.dropDown();
            player1.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player1.Draw(gameTime);
            if (ipt != null)
            {
                var ddd = ipt.getDropDownDistance();
                if (ddd > Constants.Measures.Scale * Constants.Measures.blockSize * 2)
                    SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(ipt.StartingPos.X) - 10, (int)(ipt.StartingPos.Y) - 10, 20, 20), Color.Red);                    
                else if (ddd > 0)
                    SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(ipt.StartingPos.X) - 10, (int)(ipt.StartingPos.Y) - 10, 20, 20), Color.Black * (ddd / (Constants.Measures.Scale * Constants.Measures.blockSize * 2)));
            }
        }
    }
}
