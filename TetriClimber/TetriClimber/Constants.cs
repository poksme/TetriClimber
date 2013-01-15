using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    namespace Constants
    {
        public static class Color
        {
            public static  Microsoft.Xna.Framework.Color HexToColor(String hexString)
            {
                Microsoft.Xna.Framework.Color actColor = Microsoft.Xna.Framework.Color.White;
                if (hexString.StartsWith("#") && hexString.Length == 7)
                    actColor = new Microsoft.Xna.Framework.Color(
                        int.Parse(hexString.Substring(1,2), System.Globalization.NumberStyles.HexNumber), 
                        int.Parse(hexString.Substring(3,2), System.Globalization.NumberStyles.HexNumber),
                        int.Parse(hexString.Substring(5,2), System.Globalization.NumberStyles.HexNumber)
                        );
                return actColor;
            }
            public static Microsoft.Xna.Framework.Color p1Dark = HexToColor("#87cdde");
            public static Microsoft.Xna.Framework.Color p1Light = HexToColor("#afdde9");
            public static Microsoft.Xna.Framework.Color p2Dark = HexToColor("#d35f5f");
            public static Microsoft.Xna.Framework.Color p2Light = HexToColor("#e08b8b");
            public static Microsoft.Xna.Framework.Color tDark = HexToColor("#c6afe9");
            public static Microsoft.Xna.Framework.Color tLight = HexToColor("#e3d7f4");
            public static Microsoft.Xna.Framework.Color sDark = HexToColor("#ffe680");
            public static Microsoft.Xna.Framework.Color sLight = HexToColor("#fff2c0");
            public static Microsoft.Xna.Framework.Color qDark = HexToColor("#62d2ee");
            public static Microsoft.Xna.Framework.Color qLight = HexToColor("#aaeeff");
            public static Microsoft.Xna.Framework.Color lDark = HexToColor("#ffccaa");
            public static Microsoft.Xna.Framework.Color lLight = HexToColor("#ffe6d5");
            public static Microsoft.Xna.Framework.Color pDark = HexToColor("#6cf3d7");
            public static Microsoft.Xna.Framework.Color pLight = HexToColor("#aaffee");
            public static Microsoft.Xna.Framework.Color zDark = HexToColor("#c1ff99");
            public static Microsoft.Xna.Framework.Color zLight = HexToColor("#dbffc7");
            public static Microsoft.Xna.Framework.Color oDark = HexToColor("#ffaacc");
            public static Microsoft.Xna.Framework.Color oLight = HexToColor("#ffd5e5");
            public static Microsoft.Xna.Framework.Color border = HexToColor("#cecece");
            public static Microsoft.Xna.Framework.Color background = HexToColor("#ffffff");
        }
        public static class Measures
        {
            public const int spriteSquareSize = 115;
            public const float blockSize = 40f;
            public const int borderSize = 5;
            public const float leftBoardMargin = 20f;
            public const float upBoardMargin = 20f;
            public const float boardWidth = 10f;
            public const float boardHeight = 22f;
        }
    }
}
