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
        private Vector2 leftPos;
        private Vector2 rightPos;
        private Vector2 size;

        public TextButton(String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(text, p, h, data, s)
        {
            size = TextManager.Instance.getSizeString(Content.Font, Content.Value);
            Content.Pos = new Vector2(p.X + (35 * 2 + 53 * (float)Math.Floor(size.X / 53) - size.X)/2 * scale,
                                      p.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
            leftPos = p;
            rightPos = new Vector2(p.X + 35 * scale + ((int)(size.X / 53 ) * 53  * scale), p.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.LEFT_TEXT_BUTTON, leftPos, scale);
            for (int i = 0; i < (int)size.X /53; i ++)
                SpriteManager.Instance.drawZoom(SpriteManager.ESprite.MIDDLE_TEXT_BUTTON, new Vector2(leftPos.X + (35 + i * 53) * scale, leftPos.Y), scale);
            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.RIGHT_TEXT_BUTTON, rightPos, scale);
            TextManager.Instance.Draw(Content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
