using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class GameButton : DrawableGameComponent
    {
        private SpriteManager.ESprite type;
        private Rectangle coord;
        private ButtonState btnState;
        PlayerControl.HandlerAction handler;

        public Rectangle Coord
        {
            get { return coord; }
            set { coord = value; }
        }

        public GameButton(SpriteManager.ESprite s, Vector2 pos, PlayerControl.HandlerAction act)
            : base(App.Game)
        {
            btnState = ButtonState.Released;
            handler = act;
            type = s;
          //  pos = Vector2.Transform(pos, Matrix.CreateRotationZ((float)MathHelper.ToRadians(90)));          
            coord = new Rectangle((int)pos.X, (int)pos.Y, (int)Constants.Measures.buttonSize, (int)Constants.Measures.buttonSize);
        }

        public override void Update(GameTime gameTime)
        {
            if (SettingsManager.Instance.Device == SettingsManager.EDevice.PC)
                KeyboardUpdate(gameTime);
            else
                SurfaceUpdate(gameTime);
        }

        private void SurfaceUpdate(GameTime gameTime)
        {
            if (App.UserInput.isPressed(AUserInput.EInput.TAP))
            {
                Point touch = (App.UserInput as TouchInput).getPointTaped();
                var pos = new Vector2(touch.X, touch.Y);
                pos = Vector2.Transform(pos, Matrix.CreateRotationZ(MathHelper.ToRadians(90)) *
                                    Matrix.CreateTranslation(Constants.Measures.portraitWidth, 0, 0));
                touch.X = (int)pos.X;
                touch.Y = (int)pos.Y;
                if (coord.Contains(touch))
                    handler(btnState);
                btnState = ButtonState.Pressed;
            }
            else
                btnState = ButtonState.Released;
        }

        private void KeyboardUpdate(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released)
                btnState = ButtonState.Released;
            else if (mouseState.LeftButton == ButtonState.Pressed
                && coord.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                handler(btnState);
                btnState = ButtonState.Pressed;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteManager.Instance.drawAtRecPos(type, coord);
        }
    }
}
