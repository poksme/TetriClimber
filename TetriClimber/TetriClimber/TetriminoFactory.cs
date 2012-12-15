using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TetriClimber
{
    public class TetriminoFactory
    {
        private static TetriminoFactory instance = null;
        private Random rand = null;
        private List<ConstructorInfo> tetriminiConstructors;

        private TetriminoFactory()
        {
            rand = new Random();
            tetriminiConstructors = new List<ConstructorInfo>();

            Type[] tetriminoParametersType = new Type[] { }; // TAKES NO ARGUMENTS
            tetriminiConstructors.Add(typeof(TetriL).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriO).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriP).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriQ).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriS).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriT).GetConstructor(tetriminoParametersType));
            tetriminiConstructors.Add(typeof(TetriZ).GetConstructor(tetriminoParametersType));
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

        public ATetrimino getTetrimino()
        {
            return (ATetrimino)(tetriminiConstructors[rand.Next(7)].Invoke(null)); // TAKES NO ARGUMENTS
        }
    }
}
