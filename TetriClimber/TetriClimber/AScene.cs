﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public abstract class AScene : DrawableGameComponent
    {
        public AScene() : base(App.Game)
        {
        }
    }
}
