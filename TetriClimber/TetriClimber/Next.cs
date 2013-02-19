using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Next: DrawableGameComponent
    {
        private GameString nextText;

        public Next()
            : base(App.Game)
        {
           nextText = new GameString("NEXT", TextManager.EFont.AHARONI, Color.White, 0.46f, new Vector2(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.nextPosY + Constants.Measures.paddingTextY));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth), (int)Constants.Measures.nextPosY,
                                            (int)(TextManager.Instance.getSizeString(nextText.Font, nextText.Value).X * nextText.Scale + Constants.Measures.paddingTextX * 2),
                                            (int)Constants.Measures.textBoxH),
                                            Constants.Color.border);
            //SpriteManager.Instance.drawBoardedRectangleAbsPos(new Rectangle((int)(Constants.Measures.leftBoardMargin + Constants.Measures.boardWidth + Constants.Measures.borderSize), (int)(Constants.Measures.levelPosY + Constants.Measures.textBoxH),
            //                                            (int)(TextManager.Instance.getSizeString(levelValue.Font, levelValue.Value).X * nextValue.Scale + Constants.Measures.paddingTextX * 2),
            //                                            (int)(TextManager.Instance.getSizeString(levelValue.Font, nextValue.Value).Y * 0.70 * nextValue.Scale)),
            //                                            Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(nextText);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
