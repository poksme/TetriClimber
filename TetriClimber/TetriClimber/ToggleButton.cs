using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class ToggleButton: AButton
    {
        private Color applyColor = Constants.Color.border;
        private float alpha;

        public ToggleButton(AMenu cnt, String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(cnt, text, p, h, data, s)
        {
                alpha = 0.5f;
                if (!(bool)arg)
                    Content.Color *= alpha;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Select()
        {
            base.Select();
            if (!(bool)arg)
                Content.Color *= alpha;
        }
        public override void Unselect()
        {
            base.Unselect();
            if (!(bool)arg)
                Content.Color *= alpha;
        }

        public override void Execute()
        {
            arg = !(bool)arg;
            base.Execute();
            Select();
        }
    }
}
