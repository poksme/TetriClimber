﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    [Serializable ()]
    public class Score
    {
        private int lineScore;
        public int ClimbyScore {get; private set;}
        public int TotalScore { get; set; }
        public int Level { get; set; }
        public String pseudo { get; set; }

        public Score(Vector2 p = new Vector2(), Vector2 o = new Vector2())
        {
            lineScore = 0;
            ClimbyScore = 0;
            TotalScore = 0;
            Level = 0;
            pseudo = "";
        }

        public Score()
        {
            //lineScore = 0;
            //climbyScore = 0;
            //TotalScore = 0;
            //Level = 0;
            //pseudo = "";
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
            ClimbyScore +=  cs * ((1200 / 4) * (Level + 1));
            majTotalScore();

        }

        public void majTotalScore()
        {
            TotalScore = ClimbyScore + lineScore;
        }

        public void setPseudo(String p)
        {
            pseudo = p[0] + p.Substring(1).ToLower();
        }
    }
}
