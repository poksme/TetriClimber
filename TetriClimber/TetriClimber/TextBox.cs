using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    class TextBox : DrawableGameComponent
    {
        public delegate void HandlerAction(String val);
        public int X { get { return location.X; } set { location.X = value; } }
        public int Y { get { return location.Y; } set { location.Y = value; } }
        public int Width { get { return location.Width; } set { location.Width = value; } }
        public int Height { get { return location.Height; } set { location.Height = value; } }
        public Rectangle location;
        public bool Active { get; set; }

        public String Text { get { return gs.Value; } set { gs.Value = value;} }
        public GameString gs;
        private HandlerAction handler;

        private TimeSpan lat = TimeSpan.FromMilliseconds(120);
        private TimeSpan cur = TimeSpan.Zero;

        public TextBox(HandlerAction h ,Rectangle l = new Rectangle(), String val ="") :
            base(App.Game)
        {
            location = l;
            gs = new GameString(val, TextManager.EFont.AHARONI, Color.Black, 0.5f, new Vector2((float)l.X, (float)(l.Y + 2)));
            Active = true;
            handler = h;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cur += gameTime.ElapsedGameTime;
            if (cur > lat)
            {
                if (gs.Value.Length < 6)
                {
                    foreach (KeyValuePair<Keys, Char> p in Constants.Other.letters)
                        if (Keyboard.GetState().IsKeyDown(p.Key))
                        {
                            gs.Value = gs.Value + p.Value;
                            cur = TimeSpan.Zero;
                            break;
                        }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Back) && gs.Value.Length > 0)
                {
                    gs.Value = gs.Value.Remove(gs.Value.Length - 1);
                    cur = TimeSpan.Zero;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && gs.Value.Length > 0)
                {
                    handler(gs.Value);
                    cur = TimeSpan.Zero;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(location, Color.WhiteSmoke, 1, Color.Black);
            TextManager.Instance.Draw(gs);
        }
    }
}
