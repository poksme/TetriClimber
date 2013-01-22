using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class Play : AScene
    {
        private GameSession player1;
        private TimeSpan lat = new TimeSpan(10000000 / 2);
        private TimeSpan cur = new TimeSpan(0);
        private TimeSpan turnLat = new TimeSpan(10000000 / 10);

        private Vector2 tmpOrtSize = new Vector2(0.2f, 50f);
        private Vector2 tmpPos = new Vector2();

        public Play() : base()
        {
            player1 = new GameSession();
            SoundManager.Instance.bgmPlay();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteManager.Instance.drawShapeAtPos(tmpPos, TextureManager.ETexture.GREEN_CROSS, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y + 100f), TextureManager.ETexture.BLUE_SQUARE, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 500f, tmpPos.Y - 100f), TextureManager.ETexture.GREEN_SQUARE, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y - 500f), TextureManager.ETexture.ORANGE_TRIANGLE, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 250f, tmpPos.Y), TextureManager.ETexture.PINK_CROSS, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y - 250f), TextureManager.ETexture.VIOLET_HEXAGONE, tmpOrtSize.X, tmpOrtSize.Y);
            SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 750f, tmpPos.Y), TextureManager.ETexture.YELLOW_LINES, tmpOrtSize.X, tmpOrtSize.Y);
            player1.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tmpPos.X += 0.5f;
            tmpPos.Y += 0.5f;
            tmpOrtSize.X += 0.001f;
            //tmpOrtSize.Y -= 1f;
            cur += gameTime.ElapsedGameTime;
            if (gameTime.ElapsedGameTime != TimeSpan.Zero)
            {
                if (cur >= turnLat)
                    cur = new TimeSpan(0);
                if (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.RIGHT) > lat && cur == TimeSpan.Zero))
                    player1.rightMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.TAP) > lat && cur == TimeSpan.Zero))
                    player1.rightShift();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.LEFT) > lat && cur == TimeSpan.Zero))
                    player1.leftMove();
                if (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) == gameTime.ElapsedGameTime ||
                    (App.ToucheInput.getDownTime(AUserInput.EInput.DOWN) > lat && cur == TimeSpan.Zero))
                    player1.dropDown();
            }
            player1.Update(gameTime);
        }
    }
}
