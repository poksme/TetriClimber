﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetriClimber
{
    public class TextButton :AButton
    {


        public TextButton(String text, Vector2 p, MenuManager.HandlerAction h, Object data = null, float s = 0.6f)
            : base(text, p, h, data, s)
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
    }
}
