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
        public enum ETexture {TETRIMINO, RECTANGLE};
        private Dictionary<ETexture, Texture2D> textures = null;
        private static TextureManager instance = null;

        private TextureManager()
        {
            textures = new Dictionary<ETexture, Texture2D>();
            textures.Add(ETexture.TETRIMINO, App.ContentManager.Load<Texture2D>("blocks"));

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
