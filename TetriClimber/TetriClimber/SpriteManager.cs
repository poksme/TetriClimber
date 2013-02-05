using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class SpriteManager
    {
        public enum ESprite { Z, T, Q, S, L, P, O, PAUSE, LEFT, RIGHT, CLIMBYRED, CLIMBYBLUE, LEFT_TEXT_BUTTON, MIDDLE_TEXT_BUTTON, RIGHT_TEXT_BUTTON };
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
            sprites.Add(ESprite.O, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(690, 345, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.PAUSE, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(0, 230, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.LEFT, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(2 * 115, 2 * 115, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.RIGHT, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(115, 2 * 115, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.CLIMBYBLUE, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(3 * 115, 115, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.CLIMBYRED, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(4 * 115, 115, 115, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.LEFT_TEXT_BUTTON, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(5 * 115, 115, 35, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.MIDDLE_TEXT_BUTTON, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(6 * 115, 2* 115, 53, 115), TextureManager.ETexture.TETRIMINO));
            sprites.Add(ESprite.RIGHT_TEXT_BUTTON, new KeyValuePair<Rectangle, TextureManager.ETexture>(new Rectangle(6 * 115, 115, 35, 115), TextureManager.ETexture.TETRIMINO));
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

        public void drawAtRecPos(ESprite es, Rectangle rec, float transparency = 1f)
        {
            App.SpriteBatch.Draw(textureManager.getTexture(sprites[es].Value), rec, sprites[es].Key, Color.White * transparency);
        }

        public void drawAtPos(ESprite es, Vector2 pos, float transparency = 1f)
        {
            App.SpriteBatch.Draw(textureManager.getTexture(sprites[es].Value), pos, sprites[es].Key, Color.White * transparency);
        }

        public void end()
        {
            App.SpriteBatch.End();
        }

        public void drawRotatedAtPos(ESprite es, Vector2 pos, float ort, float sprtSize, float transparency = 1f)
        {
            // WHY DO WE NEED TO ADD PADDING ?
            App.SpriteBatch.Draw(textureManager.getTexture(sprites[es].Value), new Vector2(pos.X + sprtSize / 2, pos.Y + sprtSize / 2), sprites[es].Key, Color.White * transparency, ort, new Vector2(Constants.Measures.spriteSquareSize / 2f), sprtSize / Constants.Measures.spriteSquareSize, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }

        public void drawRectangleAbsPos(Rectangle rec, Color col)
        {
            App.SpriteBatch.Draw(textureManager.getTexture(TextureManager.ETexture.RECTANGLE), rec, col);
        }

        public void drawBoardedRectangleAbsPos(Rectangle rec, Color col, int BorderSize, Color BorderColor)
        {
            Rectangle[] borders =  
            {
                new Rectangle(rec.X - BorderSize, rec.Y - BorderSize, rec.Width + BorderSize * 2, BorderSize),
                new Rectangle(rec.X - BorderSize, rec.Y + rec.Height, rec.Width + BorderSize * 2, BorderSize),
                new Rectangle(rec.X - BorderSize, rec.Y, BorderSize, rec.Height),
                new Rectangle(rec.X + rec.Width, rec.Y, BorderSize, rec.Height), 
            };
            foreach (Rectangle border in borders)
                App.SpriteBatch.Draw(textureManager.getTexture(TextureManager.ETexture.RECTANGLE), border, BorderColor);
            App.SpriteBatch.Draw(textureManager.getTexture(TextureManager.ETexture.RECTANGLE), rec, col);
        }

        internal void drawShapeAtPos(Vector2 pos, TextureManager.ETexture shape, float ort, float sprtSize, float transparency = 1f)
        {
            App.SpriteBatch.Draw(textureManager.getTexture(shape), new Vector2(pos.X + sprtSize / 2, pos.Y + sprtSize / 2), new Rectangle(0, 0, 800, 800), Color.White * transparency, ort, new Vector2(800 / 2f), sprtSize / 800, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }

        internal void drawZoom(ESprite sprite, Vector2 pos, float sprtSize, float ort = 0f)
        {
            App.SpriteBatch.Draw(TextureManager.Instance.getTexture(sprites[sprite].Value), new Vector2(pos.X + (sprites[sprite].Key.Width * sprtSize) / 2, pos.Y + (sprites[sprite].Key.Height * sprtSize) / 2), sprites[sprite].Key, Color.White, ort, new Vector2(sprites[sprite].Key.Width / 2f, sprites[sprite].Key.Height /2f), sprtSize, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
    }
}
