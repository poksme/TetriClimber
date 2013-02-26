using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Level
    {
        public int level { get; private set; }
        private int step;

        public Level()
        {
            level = 0;
            step = 0;
        }

        private int calcLevel(int score, int lvl = 0)
        {
            if (score > ((1200 / 4) * (lvl + 1) * 10))
                return calcLevel(score, lvl + 1);
            else
                return lvl;
        }

        public bool updateLevel(int stepToAdd)
        {
            step += stepToAdd;
            int tmp = step / 10;
            if (tmp > level)
            {
                level = tmp;
                return true;
            }
            return false;
        }
    }
}