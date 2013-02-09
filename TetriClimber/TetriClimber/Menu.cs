using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Menu : AMenu
    {
        public Menu()
            : base()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (AButton btn in buttons)
                btn.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
      
        }
    }
}
