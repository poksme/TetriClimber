using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class AButton : DrawableGameComponent
    {
        public GameString Content;
        public Color OverColor { get; set; }
        public Color DefaultColor { get; set; }
        protected MenuManager.HandlerAction execute;
        protected Object arg;
        protected float scale;
        protected Vector2 leftPos;
        protected Vector2 rightPos;
        protected Vector2 size;

        public AButton(String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(App.Game)
        {
            scale = s;
            arg = data;
            execute = h;
            DefaultColor = Constants.Color.border;
            OverColor = Constants.Color.p1Light;
            Content = new GameString(text, TextManager.EFont.AHARONI, Constants.Color.border, scale);
            size = TextManager.Instance.getSizeString(Content.Font, Content.Value);
            Content.Pos = new Vector2(p.X + (35 * 2 + 53 * (float)Math.Floor(size.X / 53) - size.X) / 2 * scale,
                                      p.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
            leftPos = p;
            rightPos = new Vector2(p.X + 35 * scale + ((int)(size.X / 53) * 53 * scale), p.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.LEFT_TEXT_BUTTON, leftPos, scale);
            for (int i = 0; i < (int)size.X / 53; i++)
                SpriteManager.Instance.drawZoom(SpriteManager.ESprite.MIDDLE_TEXT_BUTTON, new Vector2(leftPos.X + (35 + i * 53) * scale, leftPos.Y), scale);
            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.RIGHT_TEXT_BUTTON, rightPos, scale);
            TextManager.Instance.Draw(Content);
        }

        public virtual void Select()
        {
            Content.Color = OverColor;
        }

        public virtual void Unselect()
        {
            Content.Color = DefaultColor;
        }

        public virtual void Execute()
        {
            execute(arg);
        }
    }
}
