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
        protected Vector2 size;
        public Vector2 TotalSize { get; protected set; }

        protected AMenu Container;
        public Vector2 LeftPos{get; protected set;}
        protected Vector2 rightPos;

        protected Vector2 AbsLPos = Vector2.Zero;
        protected Vector2 AbsRPos = Vector2.Zero;

        public AButton(AMenu cnt, String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(App.Game)
        {
            Container = cnt;
            scale = s;
            arg = data;
            execute = h;
            DefaultColor = Constants.Color.border;
            OverColor = Constants.Color.p1Light;
            Content = new GameString(text, TextManager.EFont.AHARONI, Constants.Color.border, scale);
            size = TextManager.Instance.getSizeString(Content.Font, Content.Value);
//            int middle = (int)Math.Floor(size.X / 53);
            int middle = 10;
            TotalSize = new Vector2((35 + (53 * middle) + 35) * scale, 115 * scale);
            LeftPos = p;
            rightPos = new Vector2(p.X + (35 + middle * 53) * scale, p.Y);
            //Content.Pos = new Vector2(LeftPos.X + (35 * 2 + 53 * (float)Math.Floor(size.X / 53) - size.X) / 2 * scale,
            //              LeftPos.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
            Content.Pos = new Vector2(LeftPos.X + (35 * 2 + 53 *10 - size.X) / 2 * scale,
                          LeftPos.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void UpdatePosition()
        {
            AbsLPos.X = LeftPos.X + Container.Pos.X;
            AbsLPos.Y = LeftPos.Y + Container.Pos.Y;
            AbsRPos.X = rightPos.X + Container.Pos.X;
            AbsRPos.Y = rightPos.Y + Container.Pos.Y;
            //Content.Pos = new Vector2(LeftPos.X + Container.Pos.X + (35 * 2 + 53 * (float)Math.Floor(size.X / 53) - size.X) / 2 * scale,
            //              LeftPos.Y + Container.Pos.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
            Content.Pos = new Vector2(LeftPos.X + Container.Pos.X + (35 * 2 + 53 * 10 - size.X) / 2 * scale,
                          LeftPos.Y + Container.Pos.Y + (115 - size.Y + size.Y * 0.3f) / 2 * scale);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.LEFT_TEXT_BUTTON, AbsLPos, scale);
            for (int i = 0; i < 10; i++)
                SpriteManager.Instance.drawZoom(SpriteManager.ESprite.MIDDLE_TEXT_BUTTON, new Vector2((float)Math.Floor(AbsLPos.X + (35 + i * 53) * scale), AbsLPos.Y), scale);
            SpriteManager.Instance.drawZoom(SpriteManager.ESprite.RIGHT_TEXT_BUTTON, new Vector2(AbsRPos.X, AbsRPos.Y), scale);
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
