using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Score
    {
        private int lineScore;
        private int climbyScore;
        public int TotalScore { get; private set; }
        public int Level { private get; set; }

        public Score(Vector2 p = new Vector2(), Vector2 o = new Vector2())
        {
            lineScore = 0;
            climbyScore = 0;
            TotalScore = 0;
            Level = 0;
        }
        
        public void addLineScore(int ls)
        {
            switch (ls)
            {
                case 1:
                    lineScore += 40 * (Level + 1);
                    break;
                case 2:
                    lineScore += 100 * (Level + 1);
                    break;
                case 3:
                    lineScore += 300 * (Level + 1);
                    break;
                case 4:
                    lineScore += 1200 * (Level + 1);
                    break;
                default:
                    break;
            }
            majTotalScore();
        }

        public void addClimbyScore(int cs)
        {
            climbyScore +=  cs * ((1200 / 4) * (Level + 1));
            majTotalScore();

        }

        public void majTotalScore()
        {
            TotalScore = climbyScore + lineScore;
        }
    }
}
