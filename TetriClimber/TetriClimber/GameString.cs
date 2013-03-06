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
        protected Vector2 origin;
        protected Vector2 position;
        protected float scale;

        public GameString(String val, TextManager.EFont f, Color c, float s = 1f, Vector2 p = new Vector2(), Vector2 o = new Vector2())
        {
            value = val;
            color = c;
            font = f;
            position = p;
            origin = o;
            scale = s;
        }

        public void setX(float x)
        {
            position.X = x;
        }

        public void setY(float y)
        {
            position.Y = y;
        }
        public String Value { get { return this.value; } set { this.value = value; } }
        public TextManager.EFont Font { get { return this.font; }}
        public Color Color { get { return this.color; } set { this.color = value; } }
        public Vector2 Pos { get { return position; } set { position = value;} }
        public Vector2 Origin { get { return this.origin; } }
        public float Scale { get { return this.scale; } }
    }
}
