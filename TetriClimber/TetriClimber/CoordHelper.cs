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
        private Matrix m;

        public float buttonSize = 70f;
        private Dictionary<EProfile, float> leftMargin;
        public float paddingTextX = 30f;
        public float paddingTextY = 20f;
        public float textBoxH = 90f;
        public float textBoxW;
        public float nextBoxW = 243f;
        public float nextBoxH = 170f;
        public float scorePosY = 354f;
        public float levelPosY = 593f;
        public float nextPosY = 40f;
        public Vector2 pause;
        public Vector2 leftArrow;
        public Vector2 rightArrow;

        private CoordHelper()
        {
            Profile = EProfile.ONEPLAYER;
            m = Matrix.CreateScale(new Vector3(Constants.Measures.Scale, Constants.Measures.Scale, 1));
            leftMargin = new Dictionary<EProfile, float>(){
             {EProfile.ONEPLAYER, 704f},
             {EProfile.TWOPLAYER, 1400f}
            };
            pause = new Vector2(Constants.Measures.upBoardMargin - Constants.Measures.borderSize);
            leftArrow = new Vector2(getLeftMargin(EProfile.ONEPLAYER) - Constants.Measures.buttonSize - Constants.Measures.upBoardMargin - Constants.Measures.borderSize,
                                                                          Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize);
            rightArrow = new Vector2(getRightBoard(EProfile.ONEPLAYER) + Constants.Measures.upBoardMargin,
                                     Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize);
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
            return Vector2.Transform(v, Matrix.Invert(m));
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
            return m;
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
                return leftMargin[EProfile.ONEPLAYER] + Constants.Measures.boardWidth + Constants.Measures.borderSize;
            return leftMargin[EProfile.TWOPLAYER] + Constants.Measures.boardWidth + Constants.Measures.borderSize;
        }

        #region Positions
        public float getLeftMargin(EProfile profile)
        {
            return leftMargin[profile];
        }

        public float getLeftMargin(AUserInput.EGameMode gm)
        {
            switch (gm)
            {
                case AUserInput.EGameMode.MULTI1P:
                    return 10f;
                case AUserInput.EGameMode.MULTI2P:
                    return 1400f;
                default: // SOLO
                    return 704f;
            }
        }

        private void UpdatePosition()
        {
            if (Profile == EProfile.TWOPLAYER)
            {
                leftMargin[EProfile.ONEPLAYER] = 10f;
                textBoxW = leftMargin[EProfile.TWOPLAYER] - getRightBoard(EProfile.ONEPLAYER);
                pause.X = 930f;
                pause.Y = 880f;
                leftArrow.X = 800f;
                leftArrow.Y = 880f;
                rightArrow.X = 1060f;
                rightArrow.Y = 880f;
            }
            else
            {
                leftMargin[EProfile.ONEPLAYER] = 704f;
                pause.X = Constants.Measures.upBoardMargin - Constants.Measures.borderSize;
                pause.Y = Constants.Measures.upBoardMargin - Constants.Measures.borderSize;
                leftArrow.X = getLeftMargin(EProfile.ONEPLAYER) - Constants.Measures.buttonSize - Constants.Measures.upBoardMargin - Constants.Measures.borderSize;
                leftArrow.Y = Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize;
                rightArrow.X = getRightBoard(EProfile.ONEPLAYER) + Constants.Measures.upBoardMargin;
                rightArrow.Y = Constants.Measures.upBoardMargin + Constants.Measures.boardHeight - Constants.Measures.buttonSize;
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

            return new Rectangle((int)(leftMargin[EProfile.TWOPLAYER] - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - Constants.Measures.paddingTextX * 2 - Constants.Measures.borderSize), (int)(scorePosY + textBoxH),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));
        }

        public Vector2 getScoreValue(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX , scorePosY - paddingTextY/2 + textBoxH);

            return new Vector2(leftMargin[EProfile.TWOPLAYER] - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX,
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
            return new Rectangle((int)(leftMargin[EProfile.TWOPLAYER] - nextBoxW) - Constants.Measures.borderSize, (int)(nextPosY + textBoxH), (int)nextBoxW, (int)nextBoxH);
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

            return new Rectangle((int)(leftMargin[EProfile.TWOPLAYER] - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX * 2 - Constants.Measures.borderSize), (int)(levelPosY + textBoxH),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale + Constants.Measures.paddingTextX * 2),
                              (int)(TextManager.Instance.getSizeString(text.Font, text.Value).Y * 0.70 * text.Scale));
        }

        public Vector2 getLevelValue(GameString text, EProfile board)
        {
            if (board == EProfile.ONEPLAYER)
                return new Vector2(getRightBoard(EProfile.ONEPLAYER) + paddingTextX, levelPosY - paddingTextY / 2 + textBoxH);
            return new Vector2(leftMargin[EProfile.TWOPLAYER] - TextManager.Instance.getSizeString(text.Font, text.Value).X * text.Scale - paddingTextX,
                                levelPosY - paddingTextY / 2 + textBoxH);
        }

        public Vector2 getNextValue(EProfile eProfile, Block b)
        {
            if (eProfile == EProfile.ONEPLAYER)
                return new Vector2(CoordHelper.Instance.getLeftMargin(eProfile) + (b.PosRel.X + 10.5f) * Constants.Measures.blockSize, Constants.Measures.upBoardMargin + (b.PosRel.Y + 1.5f) * Constants.Measures.blockSize);
            return new Vector2(CoordHelper.Instance.getLeftMargin(eProfile) - nextBoxW - Constants.Measures.borderSize + (b.PosRel.X + 0.5f) * Constants.Measures.blockSize, Constants.Measures.upBoardMargin + (b.PosRel.Y + 1.5f) * Constants.Measures.blockSize);
        }
    }
}
