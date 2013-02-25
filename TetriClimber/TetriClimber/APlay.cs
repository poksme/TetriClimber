using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class APlay : AScene
    {
        protected PlayerControl control;
        protected TimeSpan lat = new TimeSpan(10000000 / 2);
        protected TimeSpan cur = new TimeSpan(0);
        protected TimeSpan turnLat = new TimeSpan(10000000 / 10);
        protected GameSession player1;
        protected TouchInput ipt;
        protected HUD hud;

        public APlay(CoordHelper.EProfile profile) : base()
        {
            CoordHelper.Instance.setProfile(profile);
            hud = new HUD();
            control = new PlayerControl();
            player1 = new GameSession(CoordHelper.EProfile.ONEPLAYER, hud);
            ipt = null;
            if (App.UserInput is TouchInput)
                ipt = App.UserInput as TouchInput;
            else
                (App.UserInput as KeyboardInput).setKeyRepeatTime(new TimeSpan(1500000));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            control.Draw(gameTime);
            hud.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cur += gameTime.ElapsedGameTime;
            control.Update(gameTime);
            hud.Update(gameTime);
        }
    }
}
