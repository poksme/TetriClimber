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

        public AButton(String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(App.Game)
        {
            scale = s;
            arg = data;
            execute = h;
            DefaultColor = Constants.Color.border;
            OverColor = Constants.Color.qLight;
            Content = new GameString(text, TextManager.EFont.AHARONI, Constants.Color.border, scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void Select()
        {
            Content.Color = OverColor;
        }

        public void Unselect()
        {
            Content.Color = DefaultColor;
        }

        public void Execute()
        {
            execute(arg);
        }
    }
}
