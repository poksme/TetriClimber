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
            //if (App.UserInput.isPressed(AUserInput.EInput.DOWN)) // TO KNOW IF A POINT IS TAPED US POINTTAPED (IT RESETS THE BOOL WHEN CALLED)
            if ((App.UserInput as TouchInput).pointTaped) 
            {
                Point touch = (App.UserInput as TouchInput).getPointTaped();
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
