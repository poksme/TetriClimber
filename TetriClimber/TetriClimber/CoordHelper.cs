using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class CoordHelper
    {
        private static CoordHelper instance = null;
        public static CoordHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoordHelper();
                return instance;
            }
        }

        public CoordHelper()
        {
        }
    }
}
