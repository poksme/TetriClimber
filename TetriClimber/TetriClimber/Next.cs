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
        private ATetrimino next;

        public Next()
            : base(App.Game)
        {
            nextText = new GameString("NEXT", TextManager.EFont.AHARONI, Color.White, 0.46f, new Vector2(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth + Constants.Measures.paddingTextX,
                                                                                                            Constants.Measures.nextPosY + Constants.Measures.paddingTextY));
           next = TetriminoFactory.Instance.getNextTetrimino();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawRectangleAbsPos(new Rectangle((int)(CoordHelper.Instance.leftBoardMargin1 + Constants.Measures.boardWidth), (int)Constants.Measures.nextPosY,
                                            (int)(TextManager.Instance.getSizeString(nextText.Font, nextText.Value).X * nextText.Scale + Constants.Measures.paddingTextX * 2),
                                            (int)Constants.Measures.textBoxH),
                                            Constants.Color.border);
            SpriteManager.Instance.drawBoardedRectangleAbsPos(Constants.Measures.boxPreview,
                                                        Constants.Color.background, Constants.Measures.borderSize, Constants.Color.border);
            TextManager.Instance.Draw(nextText);
            next.DrawAsPreview(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            next = TetriminoFactory.Instance.getNextTetrimino();
        }

    }
}
