using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class TextureManager
    {
        public enum ETexture {TETRIMINO, RECTANGLE, BLUE_SQUARE, GREEN_CROSS, GREEN_SQUARE, ORANGE_TRIANGLE, PINK_CROSS, VIOLET_HEXAGONE, YELLOW_LINES};
        private Dictionary<ETexture, Texture2D> textures = null;
        private static TextureManager instance = null;

        private TextureManager()
        {
            textures = new Dictionary<ETexture, Texture2D>();
            textures.Add(ETexture.TETRIMINO, App.ContentManager.Load<Texture2D>("blocks"));
            textures.Add(ETexture.BLUE_SQUARE, App.ContentManager.Load<Texture2D>("blue_square"));
            textures.Add(ETexture.GREEN_CROSS, App.ContentManager.Load<Texture2D>("green_cross"));
            textures.Add(ETexture.GREEN_SQUARE, App.ContentManager.Load<Texture2D>("green_square"));
            textures.Add(ETexture.ORANGE_TRIANGLE, App.ContentManager.Load<Texture2D>("orange_triangle"));
            textures.Add(ETexture.PINK_CROSS, App.ContentManager.Load<Texture2D>("pink_cross"));
            textures.Add(ETexture.VIOLET_HEXAGONE, App.ContentManager.Load<Texture2D>("violet_hexagone"));
            textures.Add(ETexture.YELLOW_LINES, App.ContentManager.Load<Texture2D>("yellow_lines"));
            #region COLOR FILL TEXTURE
            Texture2D t = new Texture2D(App.Game.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
            textures.Add(ETexture.RECTANGLE, t);
            #endregion
        }

        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextureManager();
                return instance;
            }
        }

        public Texture2D getTexture(ETexture e)
        {
            return textures[e];
        }


    }
}
