using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public abstract class AScene : DrawableGameComponent
    {
        public bool IsPause { get; protected set; }

        public AScene() : base(App.Game)
        {
            IsPause = false;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Pause()
        {
            IsPause = true;
        }

        public void Resume()
        {
            IsPause = false;
        }

        public void TogglePause()
        {
            if (this.IsPause)
                this.Resume();
            else
                this.Pause();
        }

    }
}
