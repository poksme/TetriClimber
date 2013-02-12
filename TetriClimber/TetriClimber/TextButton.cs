using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetriClimber
{
    public class TextButton :AButton
    {


        public TextButton(AMenu cnt, String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(cnt, text, p, h, data, s)
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

        public override void Execute()
        {
            SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.CLEARLINE);
            base.Execute();
        }
    }
}
