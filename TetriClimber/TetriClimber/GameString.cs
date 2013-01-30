using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class GameString
    {
        protected String value;
        protected TextManager.EFont font;
        protected Color color;
        protected Vector2 pos;
        protected Vector2 origin;
        protected float scale;

        public GameString(String val, TextManager.EFont f, Color c, float s = 1f, Vector2 p = new Vector2(), Vector2 o = new Vector2())
        {
            value = val;
            color = c;
            font = f;
            pos = p;
            origin = o;
            scale = s;
        }

        public String Value { get { return this.value; } set { this.value = value; } }
        public TextManager.EFont Font { get { return this.font; }}
        public Color Color { get { return this.color; } }
        public Vector2 Pos { get { return this.pos; } }
        public Vector2 Origin { get { return this.origin; } }
        public float Scale { get { return this.scale; } }
    }
}
