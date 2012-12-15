using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class TetriminoFactory
    {
        private static TetriminoFactory instance = null;
        private static Random rand = null;

        private TetriminoFactory()
        {
            rand = new Random();
        }

        public static TetriminoFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new TetriminoFactory();
                return instance;
            }
        }

        public int getTetrimino()
        {
            return rand.Next(7);
        }
    }
}
