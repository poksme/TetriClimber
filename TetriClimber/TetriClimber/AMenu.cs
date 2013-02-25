using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class AMenu : DrawableGameComponent
    {
        protected TimeSpan lat = new TimeSpan(10000000 / 2);
        protected TimeSpan cur = new TimeSpan(0);
        protected TimeSpan turnLat = new TimeSpan(10000000 / 10);
        protected List<AButton> buttons;
        protected int cursor = 0;
        protected Vector2 pos;
        public float Width { get; protected set; }
        public Vector2 Pos { get { return pos; }}

        public AMenu():base(App.Game)
        {
            pos = Vector2.Zero;
        }

        public void setButtons(List<AButton> btns)
        {
            buttons = btns;
            if (buttons.Count > 0)
                buttons[cursor].Select();
        }

        public void Select(int cur)
        {
            if (buttons.Count > cur)
            {
                buttons[cursor].Unselect();
                cursor = cur;
                buttons[cursor].Select();
            }
        }

        public void Center()
        {
            float w = 0;
            float h = 0;

            foreach (AButton button in buttons)
                if (w < button.TotalSize.X)
                    w = button.TotalSize.X;
            h = buttons.Last().TotalSize.Y + buttons.Last().LeftPos.Y;
            pos.X = (Constants.Measures.portraitWidth - w) /2 ;
            pos.Y = (Constants.Measures.portraitHeight - h) / 2;
            pos = CoordHelper.Instance.Replace(pos);
            Width = w;
            foreach (AButton button in buttons)
                button.UpdatePosition();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
                UpdateSurface(gameTime);
            else
                UpdateKeyboard(gameTime);

            cur += gameTime.ElapsedGameTime;
        }

        private void UpdateSurface(GameTime gameTime)
        {
            //if (App.UserInput.isPressed(AUserInput.EInput.DOWN)) // TO KNOW IF A POINT IS TAPED US POINTTAPED (IT RESETS THE BOOL WHEN CALLED)
            if ((App.UserInput as TouchInput).pointTaped)
            {
                Point touch = (App.UserInput as TouchInput).tapedPoint;
                foreach (AButton btn in buttons)
                {
                    if (btn.hitTest.Contains(touch))
                    {
                        btn.Execute();
                        return;
                    }
                }
            }
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (cur >= turnLat)
                cur = new TimeSpan(0);
            if (App.UserInput.isPressed(AUserInput.EInputKeys.DOWN))
            {
                SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.SHIFT);
                buttons[cursor].Unselect();
                if (cursor < buttons.Count - 1)
                    cursor++;
                else
                    cursor = 0;
                buttons[cursor].Select();
            }
            else if (App.UserInput.isPressed(AUserInput.EInputKeys.UP))
            {
                SoundManager.Instance.play(SoundManager.EChannel.SFX, SoundManager.ESound.SHIFT);
                buttons[cursor].Unselect();
                if (cursor == 0)
                    cursor = buttons.Count - 1;
                else
                    cursor--;
                buttons[cursor].Select();
            }
            else if (App.UserInput.isPressed(AUserInput.EInputKeys.ENTER))
            {
                buttons[cursor].Execute();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
