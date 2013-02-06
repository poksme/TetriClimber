using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Level : GameString
    {
        public int level { get; private set; }
        private int step;

        public Level(int val, TextManager.EFont f, Color c, float s = 1f, Vector2 p = new Vector2(), Vector2 o = new Vector2())
            : base(val.ToString(), f, c, s, p, o)
        {
            level = 0;
            step = 0;
        }
        
        public Level(Level s):
            base(s.value, s.font, s.color, s.scale, s.pos, s.origin)
        {
            level = s.level;
        }

        private int calcLevel(int score, int lvl = 0)
        {
            if (score > ((1200 / 4) * (lvl + 1) * 10))
                return calcLevel(score, lvl + 1);
            else
                return lvl;
        }

        //public bool updateLevel(int score)
        //{
        //    var tmp = calcLevel(score);
        //    if (tmp > level)
        //    {
        //        level = tmp;
        //        value = (level).ToString();
        //        return true;
        //    }
        //    return false;
        //}
        public bool updateLevel(int stepToAdd)
        {
            step +=stepToAdd;
            int tmp = step / 10;
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
