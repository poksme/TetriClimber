using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class Level : GameString
    {
        private int level;

        public Level(String val, TextManager.EFont f, Color c, float s = 1f, Vector2 p = new Vector2(), Vector2 o = new Vector2())
            : base(val, f, c, s, p, o)
        {
            level = 0;
        }
        
        public Level(Level s):
            base(s.value, s.font, s.color, s.scale, s.pos, s.origin)
        {
            level = s.level;
        }
        
        public bool updateLevel(int score)
        {
            var tmp = score / ((1200 / 4) * (level + 1) * 10); // SCORE EQUIVALENT A 10 LIGNE MONTEES

            if (tmp > level)
            {
                level = tmp;
                value = (level).ToString();
                return true;
            }
            return false;
        }
    }
}
