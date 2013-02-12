using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TetriClimber
{
    public class APlay : AScene
    {
        private PlayerControl control;
        protected TimeSpan lat = new TimeSpan(10000000 / 2);
        protected TimeSpan cur = new TimeSpan(0);
        protected TimeSpan turnLat = new TimeSpan(10000000 / 10);

        //private Vector2 tmpOrtSize = new Vector2(0.2f, 50f);
        //private Vector2 tmpPos = new Vector2();

        public APlay() : base()
        {
            control = new PlayerControl();
            //SoundManager.Instance.bgmPlay(SoundManager.ESound.BGM);
           // bg = new Background();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
           // bg.Draw(gameTime);
            //SpriteManager.Instance.drawShapeAtPos(tmpPos, TextureManager.ETexture.GREEN_CROSS, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y + 100f), TextureManager.ETexture.BLUE_SQUARE, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 500f, tmpPos.Y - 100f), TextureManager.ETexture.GREEN_SQUARE, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y - 500f), TextureManager.ETexture.ORANGE_TRIANGLE, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 250f, tmpPos.Y), TextureManager.ETexture.PINK_CROSS, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X, tmpPos.Y - 250f), TextureManager.ETexture.VIOLET_HEXAGONE, tmpOrtSize.X, tmpOrtSize.Y);
            //SpriteManager.Instance.drawShapeAtPos(new Vector2(tmpPos.X - 750f, tmpPos.Y), TextureManager.ETexture.YELLOW_LINES, tmpOrtSize.X, tmpOrtSize.Y);
            //player1.Draw(gameTime);
            control.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //bg.Update(gameTime);
            //tmpPos.X += 0.5f;
            //tmpPos.Y += 0.5f;
            //tmpOrtSize.X += 0.001f;
            //tmpOrtSize.Y -= 1f;
            cur += gameTime.ElapsedGameTime;
            control.Update(gameTime);
        }
    }
}
