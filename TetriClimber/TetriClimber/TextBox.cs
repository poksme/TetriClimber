using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Surface;

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
        private GameString placeHolder;
        private HandlerAction handler;

        private TimeSpan lat = TimeSpan.FromMilliseconds(120);
        private TimeSpan cur = TimeSpan.Zero;

        public TextBox(HandlerAction h, Color textColor, Color placeHolderColor, Rectangle l = new Rectangle(), String val = "") :
            base(App.Game)
        {
            location = l;
            gs = new GameString(val, TextManager.EFont.AHARONI, textColor, 0.5f, new Vector2((float)l.X + 10, (float)(l.Y + 45)));
            Active = true;
            handler = h;
            placeHolder = new GameString("TAP HERE", TextManager.EFont.AHARONI, placeHolderColor, 0.5f);
            placeHolder.Pos = new Vector2((float)(l.Center.X - TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, placeHolder.Value).X * placeHolder.Scale / 2),
                (float)(l.Y + 45));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cur += gameTime.ElapsedGameTime;
            if (App.UserInput is TouchInput)
                if (location.Contains((App.UserInput as TouchInput).tapedPoint))
                    SurfaceKeyboard.IsVisible = true;
            if (cur > lat)
            {
                if (gs.Value.Length < 6)
                {
                    foreach (KeyValuePair<Keys, Char> p in Constants.Other.letters)
                        if (Keyboard.GetState().IsKeyDown(p.Key))
                        {
                            gs.Value = gs.Value + p.Value;
                            gs.setX(location.Center.X - (TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, gs.Value).X * gs.Scale)/2);
                            cur = TimeSpan.Zero;
                            break;
                        }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Back) && gs.Value.Length > 0)
                {
                    gs.Value = gs.Value.Remove(gs.Value.Length - 1);
                    gs.setX(location.Center.X - (TextManager.Instance.getSizeString(TextManager.EFont.AHARONI, gs.Value).X * gs.Scale) / 2);
                    cur = TimeSpan.Zero;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && gs.Value.Length > 0)
                {
                    App.UserInput.reset();
                    handler(gs.Value);
                    SurfaceKeyboard.IsVisible = false;
                    cur = TimeSpan.Zero;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(location, Color.White, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(gs);
            if (gs.Value.Length < 1 && !SurfaceKeyboard.IsVisible)
                TextManager.Instance.Draw(placeHolder);
        }
    }
}
