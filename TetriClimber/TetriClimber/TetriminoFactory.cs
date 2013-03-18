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
        private List<Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>> tetriminiConstructors;
        private Dictionary<CoordHelper.EProfile, int> next;

        private TetriminoFactory()
        {
            rand = new Random();
            tetriminiConstructors = new List<Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>>();

            Type[] tetriminoParametersType = new Type[] {typeof(CoordHelper.EProfile), typeof(float), typeof(Boolean) }; // TAKES TRANSPARENCY AND SHADOW
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriL).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() {{ CoordHelper.EProfile.ONEPLAYER, false}, {CoordHelper.EProfile.TWOPLAYER, false}}));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriO).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriP).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriQ).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriS).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriT).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            tetriminiConstructors.Add(new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(typeof(TetriZ).GetConstructor(tetriminoParametersType), new Dictionary<CoordHelper.EProfile, bool>() { { CoordHelper.EProfile.ONEPLAYER, false }, { CoordHelper.EProfile.TWOPLAYER, false } }));
            next = new Dictionary<CoordHelper.EProfile, int>()
            {
                {CoordHelper.EProfile.ONEPLAYER, getNextTetriminoId(CoordHelper.EProfile.ONEPLAYER)},
                {CoordHelper.EProfile.TWOPLAYER, getNextTetriminoId(CoordHelper.EProfile.TWOPLAYER)}
            };
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

        public Tuple<ATetrimino, ATetrimino> getTetrimino(CoordHelper.EProfile profile) // ALWAYS CALL THIS ONE BEFORE CALLING GET NEXT TETRIMINO
        {
            var ret =  new Tuple<ATetrimino,ATetrimino>((ATetrimino)tetriminiConstructors[next[profile]].Item1.Invoke(new object[] {profile, 0.7f, false}), (ATetrimino)tetriminiConstructors[next[profile]].Item1.Invoke(new object[] {profile, 0.3f, false}));
            next[profile] = getNextTetriminoId(profile);
            return ret;
        }

        public ATetrimino getNextTetrimino(CoordHelper.EProfile profile) // RETURNS A VISUAL NEXT TETRIMINO
        {
            return (ATetrimino)tetriminiConstructors[next[profile]].Item1.Invoke(new object[] {profile, 1f, false});
        }

        // HEXA 0 to D
        public ATetrimino getAndSetNextTetriminoFromId(long tagValue, CoordHelper.EProfile profile) // RETURNS A VISUAL NEXT TETRIMINO FROM A TAG ID AND SET THE TETRIMINO TO CREATE NEXT
        {
            next[profile] = (int)(tagValue % tetriminiConstructors.Count);
            return (ATetrimino)tetriminiConstructors[(int)(tagValue % tetriminiConstructors.Count)].Item1.Invoke(new object[] {profile, 1f, false});
        }

        private void resetTetriminiConstructors(CoordHelper.EProfile profile)
        {
            for (int i = 0; i < tetriminiConstructors.Count; i++)
                tetriminiConstructors[i].Item2[profile] = false;
            //tetriminiConstructors[i] = new Tuple<ConstructorInfo, Dictionary<CoordHelper.EProfile, bool>>(tetriminiConstructors[i].Item1, false);
        }

        private int getNextTetriminoId(CoordHelper.EProfile profile)
        {
            int key = rand.Next(tetriminiConstructors.Count);
            int it = key;

            while (tetriminiConstructors[it].Item2[profile] == true)
            {
                it = (it + 1) % tetriminiConstructors.Count;
                if (it == key)
                {
                    resetTetriminiConstructors(profile);
                    return getNextTetriminoId(profile);
                }
            }
            tetriminiConstructors[it].Item2[profile] = true;
            //tetriminiConstructors[it] = new Tuple<ConstructorInfo, bool>(tetriminiConstructors[it].Item1, true);
            return it;
        }
    }
}
