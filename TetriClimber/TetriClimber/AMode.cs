using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    abstract class AMode : DrawableGameComponent
    {
        private TimeSpan fadeTime;

        public AMode(TimeSpan ft)
            :base(App.Game)
        {
            fadeTime = ft;
        }

        public virtual bool FadeOut(GameTime gt)
        {
            fadeTime -= gt.ElapsedGameTime;
            if (fadeTime <= TimeSpan.Zero)
                return true;
            return false;
        }

        protected void ReinitFadeTime(TimeSpan ft)
        {
            fadeTime = ft;
        }
    }
}
