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
            Width = w;
            foreach (AButton button in buttons)
                button.UpdatePosition();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (cur >= turnLat)
                cur = new TimeSpan(0);
            if (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
            {
                buttons[cursor].Unselect();
                if (cursor < buttons.Count - 1)
                    cursor++;
                else
                    cursor = 0;
                buttons[cursor].Select();
            }
            else if (App.ToucheInput.getDownTime(AUserInput.EInput.UP) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.UP) > lat && cur == TimeSpan.Zero))
            {
                buttons[cursor].Unselect();
                if (cursor == 0)
                    cursor = buttons.Count - 1;
                else
                    cursor--;
                buttons[cursor].Select();
            }
            else if (App.ToucheInput.getDownTime(AUserInput.EInput.ENTER) == gameTime.ElapsedGameTime ||
                (App.ToucheInput.getDownTime(AUserInput.EInput.ENTER) > lat && cur == TimeSpan.Zero))
            {
                buttons[cursor].Execute();
            }
            cur += gameTime.ElapsedGameTime;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
