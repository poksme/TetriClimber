using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class CoordHelper
    {
        public enum EProfile {PORTRAIT, LANDSCAPE, ONEPLAYER, TWOPLAYER }
        private static  CoordHelper instance = null;

        public EProfile Profile { get; private set; }
        private Vector2 tool;
        private Dictionary<EProfile, Matrix> matrixs;

        private CoordHelper()
        {
            Profile = EProfile.LANDSCAPE;
            matrixs = new Dictionary<EProfile, Matrix>(){
            {EProfile.ONEPLAYER, Matrix.CreateRotationZ(MathHelper.ToRadians(90)) *
                                 Matrix.CreateTranslation(Constants.Measures.portraitWidth - 950, 0, 0) *
                                 Matrix.CreateScale(new Vector3(Constants.Measures.Scale, Constants.Measures.Scale, 1))},
            {EProfile.LANDSCAPE, Matrix.Identity}};
        }

        public static CoordHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoordHelper();
                return instance;
            }
        }

        public Vector2 Replace(Vector2 v)
        {
            return Vector2.Transform(v, Matrix.Invert(matrixs[Profile]));
        }

        public Point Replace(Point p)
        {
            tool.X = (float)p.X;
            tool.Y = (float)p.Y;
            tool = Replace(tool);
            p.X = (int)tool.X;
            p.Y = (int)tool.Y;
            return p;
        }

        public Matrix getCurrentMatrix()
        {
            return matrixs[Profile];
        }

        public void setProfile(EProfile p)
        {
            Profile = p;
            App.screenTransform = CoordHelper.Instance.getCurrentMatrix();
        }
    }
}
