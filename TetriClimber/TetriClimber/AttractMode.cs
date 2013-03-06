using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class AttractMode : AMode
    {
        public AttractMode()
            : base(TimeSpan.Zero) // ZERO FOR NO TRANSITION
        {

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override bool FadeOut(GameTime gt)
        {
            if (base.FadeOut(gt) == true) // VERIFY IF FADE TIME IS SPENT
            {
                ReinitFadeTime(TimeSpan.Zero); // ZERO FOR NO TRANSITIONS

                // HERE RE-INIT VALUES BEFORE NEVER BEING UPDATED AGAIN

                return true;
            }
            else
            {

                // HERE PUT FADE OUT ANIMATION USING THE FADETIME TO ANIMATE IT

                return false;
            }
        }
    }
}
