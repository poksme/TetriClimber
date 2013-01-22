using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class PlayerControl : DrawableGameComponent
    {
        private Button pause;
        private Button left;
        private Button right;

        public PlayerControl():base(App.Game)
        {
            pause = new Button(SpriteManager.ESprite.PAUSE, new Vector2(45, 20), pauseGame);
            left = new Button(SpriteManager.ESprite.LEFT, new Vector2(45, 900), leftArrow);
            right = new Button(SpriteManager.ESprite.RIGHT, new Vector2(120, 900), rightArrow);
        }

        public override void Update(GameTime gameTime)
        {
            pause.Update(gameTime);
            left.Update(gameTime);
            right.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            pause.Draw(gameTime);
            left.Draw(gameTime);
            right.Draw(gameTime);
        }

        public void pauseGame(ButtonState state)
        {
            if (state == ButtonState.Released)
                Console.Out.WriteLine("Pause click");
        }

        public void rightArrow(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                Console.Out.WriteLine("Right Holder");
        }

        public void leftArrow(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                Console.Out.WriteLine("Left Holder");
        }
    }
}
