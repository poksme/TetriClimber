using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Score : GameString
    {
        private int lineScore;
        private int climbyScore;
        private int totalScore;

        public Score(String val, TextManager.EFont f, Color c, float s = 1f, Vector2 p = new Vector2(), Vector2 o = new Vector2())
            : base(val, f, c, s, p, o)
        {
            lineScore = 0;
            climbyScore = 0;
            totalScore = 0;
        }
        
        public Score(Score s):
            base(s.value, s.font, s.color, s.scale, s.pos, s.origin)
        {
            lineScore = s.lineScore;
            climbyScore = s.climbyScore;
        }
        
        public void addLineScore(int ls)
        {
            lineScore += ls;
            majTotalScore();
        }

        public void addClimbyScore(int cs)
        {
            climbyScore += cs;
            majTotalScore();

        }

        public void majTotalScore()
        {
            totalScore = climbyScore + lineScore;
            value = (totalScore).ToString();
        }
    }
}
