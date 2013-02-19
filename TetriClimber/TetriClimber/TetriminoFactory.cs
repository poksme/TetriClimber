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
        private int next;

        private TetriminoFactory()
        {
            rand = new Random();
            tetriminiConstructors = new List<Tuple<ConstructorInfo, bool>>();

            Type[] tetriminoParametersType = new Type[] { typeof(float), typeof(Boolean) }; // TAKES TRANSPARENCY AND SHADOW
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriL).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriO).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriP).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriQ).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriS).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriT).GetConstructor(tetriminoParametersType), false));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, bool>(typeof(TetriZ).GetConstructor(tetriminoParametersType), false));
            next = getNextTetriminoId();
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

        public Tuple<ATetrimino, ATetrimino> getTetrimino() // ALWAYS CALL THIS ONE BEFORE CALLING GET NEXT TETRIMINO
        {
            var ret =  new Tuple<ATetrimino,ATetrimino>((ATetrimino)tetriminiConstructors[next].Item1.Invoke(new object[] {0.7f, false}), (ATetrimino)tetriminiConstructors[next].Item1.Invoke(new object[] {0.3f, false}));
            next = getNextTetriminoId();
            return ret;
        }

        public ATetrimino getNextTetrimino() // RETURNS A VISUAL NEXT TETRIMINO
        {
            return (ATetrimino)tetriminiConstructors[next].Item1.Invoke(new object[] {1f, false});
        }

        private void resetTetriminiConstructors()
        {
            for (int i = 0; i < tetriminiConstructors.Count; i++)
                tetriminiConstructors[i] = new Tuple<ConstructorInfo, bool>(tetriminiConstructors[i].Item1, false);
        }

        private int getNextTetriminoId()
        {
            int key = rand.Next(tetriminiConstructors.Count);
            int it = key;

            while (tetriminiConstructors[it].Item2 == true)
            {
                it = (it + 1) % tetriminiConstructors.Count;
                if (it == key)
                {
                    resetTetriminiConstructors();
                    return getNextTetriminoId();
                }
            }
            tetriminiConstructors[it] = new Tuple<ConstructorInfo, bool>(tetriminiConstructors[it].Item1, true);
            return it;
        }
    }
}
