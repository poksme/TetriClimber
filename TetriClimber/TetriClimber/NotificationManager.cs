using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class NotificationManager : DrawableGameComponent
    {
        private static NotificationManager instance = null;

        private NotificationManager ():base(App.Game)
        {
        }

        public static NotificationManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new NotificationManager();
                return instance;
            }
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
