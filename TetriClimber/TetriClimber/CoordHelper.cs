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


        public float buttonSize = 70f;
        public float leftBoardMargin1 = 704f;
        public float leftBoardMargin2 = 1400f;
        public float paddingTextX = 30f;
        public float paddingTextY = 20f;
        public float textBoxH = 90f;
        public float textBoxW;
        public float nextBoxW = 243f;
        public float nextBoxH = 170f;
        public float scorePosY = 354f;
        public float levelPosY = 593f;
        public float nextPosY = 40f;

        private CoordHelper()
        {
            Profile = EProfile.LANDSCAPE;
            matrixs = new Dictionary<EProfile, Matrix>(){
            {EProfile.ONEPLAYER, Matrix.CreateScale(new Vector3(Constants.Measures.Scale, Constants.Measures.Scale, 1))},
            {EProfile.TWOPLAYER, Matrix.CreateScale(new Vector3(Constants.Measures.Scale, Constants.Measures.Scale, 1))},
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
            UpdatePosition();
        }

        public float getRightBoard(EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return leftBoardMargin1 + Constants.Measures.boardWidth + Constants.Measures.borderSize;
            return leftBoardMargin2 + Constants.Measures.boardWidth + Constants.Measures.borderSize;
        }

        #region Positions
        private void UpdatePosition()
        {
            if (Profile == EProfile.TWOPLAYER)
            {
                leftBoardMargin1 = 10f;
                textBoxW = leftBoardMargin2 - getRightBoard(EProfile.ONEPLAYER);
            }
            
        }
        #endregion

        public Rectangle getScoreTextBox(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)scorePosY,
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                                     (int)textBoxH);

            return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)scorePosY,
                              (int)textBoxW,
                              (int)textBoxH);
        }
        
        public Vector2 getScoreText(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX, scorePosY + paddingTextY);

            return new Vector2(getRightBoard(EProfile.ONEPLAYER) + (textBoxW - TextManager.Instance.getSizeString(text.Font, text.Value).X* text.Scale) / 2,
                                scorePosY + paddingTextY);
        }
        
        public Rectangle getScoreValueBox(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)(scorePosY + textBoxH),
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));

            return new Rectangle((int)(leftBoardMargin2 - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX * 2), (int)(scorePosY + textBoxH),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));
        }

        public Vector2 getScoreValue(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX , scorePosY - paddingTextY/2 + textBoxH);

            return new Vector2(leftBoardMargin2 - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX,
                                scorePosY - paddingTextY / 2 + textBoxH);
        }

        public Rectangle getNextTextBox(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)nextPosY,
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                                     (int)textBoxH);

            return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)nextPosY, (int)textBoxW, (int)textBoxH);
        }

        public Vector2 getNextText(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX, nextPosY + paddingTextY);

            return new Vector2(getRightBoard(EProfile.ONEPLAYER) + (textBoxW - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale) / 2,
                                nextPosY + paddingTextY);
        }

        public Rectangle getNextValueBox(EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Rectangle((int)getRightBoard(EProfile.ONEPLAYER), (int)(nextPosY + textBoxH), (int)nextBoxW, (int)nextBoxH);
            return new Rectangle((int)(leftBoardMargin2 - nextBoxW), (int)(nextPosY + textBoxH), (int)nextBoxW, (int)nextBoxH);
        }

        public Rectangle getLevelTextBox(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)levelPosY,
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                                     (int)textBoxH);

            return new Rectangle((int)getRightBoard(EProfile.ONEPLAYER), (int)levelPosY,
                              (int)textBoxW,
                              (int)textBoxH);
        }

        public Vector2 getLevelText(GameString text)
        {
            if (Profile == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX, levelPosY + paddingTextY);

            return new Vector2(getRightBoard(EProfile.ONEPLAYER) + (textBoxW - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale) / 2,
                                levelPosY + paddingTextY);
        }

        public Rectangle getLevelValueBox(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Rectangle((int)(getRightBoard(EProfile.ONEPLAYER)), (int)(levelPosY + textBoxH),
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + paddingTextX * 2),
                                     (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));

            return new Rectangle((int)(leftBoardMargin2 - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX * 2), (int)(levelPosY + textBoxH),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));
        }

        public Vector2 getLevelValue(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX, levelPosY - paddingTextY / 2 + textBoxH);
            return new Vector2(leftBoardMargin2 - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX,
                                levelPosY - paddingTextY / 2 + textBoxH);
        }
    }
}
