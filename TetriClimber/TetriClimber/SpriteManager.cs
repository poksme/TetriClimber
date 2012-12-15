using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class SpriteManager
    {
        public enum ESprite { Z, T, Q, S, L, P, O };
        private Dictionary<ESprite, KeyValuePair<Rectangle, TextureManager.ETexture>> sprites = null;
        private TextureManager textureManager = null;
        private static SpriteManager instance = null;

        private SpriteManager()
        {
            textureManager = TextureManager.Instance;
            sprites = new Dictionary<ESprite, KeyValuePair<Rectangle, TextureManager.ETexture>>();
            #region TETRIMINO_RECTANGLES
            sprites.Add(ESprite.Z, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(575,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.T, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(0,    345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.Q, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(230,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.S, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(115,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.L, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(345,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.P, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(460,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.O, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(690,  345, 115, 115),  TextureManager.ETexture.TETRIMINO));
            #endregion
        }

        public static SpriteManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SpriteManager();
                return instance;
            }
        }

        public void begin()
        {
            App.SpriteBatch.Begin();
        }

        public void drawAtPos(ESprite es, Vector2 pos)
        {
            App.SpriteBatch.Draw(textureManager.getTexture(sprites[es].Value), pos, sprites[es].Key, Color.White);
        }

        public void end()
        {
            App.SpriteBatch.End();
        }
    }
}
