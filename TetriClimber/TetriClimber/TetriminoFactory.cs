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
        private List<Tuple<ConstructorInfo, bool>> tetriminiConstructors;

        private TetriminoFactory()
        {
            rand = new Random();
            tetriminiConstructors = new List<Tuple<ConstructorInfo, bool>>();

            Type[] tetriminoParametersType = new Type[] { }; // TAKES NO ARGUMENTS
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriL).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriO).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriP).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriQ).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriS).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriT).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriZ).GetConstructor(tetriminoParametersType), false));
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
            int key = rand.Next(tetriminiConstructors.Count);
            int it = key;

            while (tetriminiConstructors[it].Item2 == true)
            {
                it = (it + 1) % tetriminiConstructors.Count;
                if (it == key)
                {
                    resetTetriminiConstructors();
                    return getTetrimino();
                }
            }
            tetriminiConstructors[it] = new Tuple<ConstructorInfo, bool>(tetriminiConstructors[it].Item1, true);
            return (ATetrimino)tetriminiConstructors[it].Item1.Invoke(null);
        }

        private void resetTetriminiConstructors()
        {
            for (int i = 0; i < tetriminiConstructors.Count; i++)
                tetriminiConstructors[i] = new Tuple<ConstructorInfo, bool>(tetriminiConstructors[i].Item1, false);
        }
    }
}
