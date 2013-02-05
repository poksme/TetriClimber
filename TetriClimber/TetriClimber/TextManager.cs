using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class TextManager
    {
        public enum EFont {AHARONI}
        private static TextManager instance;
        private Dictionary<EFont, SpriteFont> fonts;
        private TextManager()
        {
            fonts = new Dictionary<EFont, SpriteFont>();
            fonts.Add(EFont.AHARONI, App.ContentManager.Load<SpriteFont>("Aharoni"));
        }

        public static TextManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextManager();
                return instance;
            }
        }

        public void Draw(GameString gs)
        {
            App.SpriteBatch.DrawString(fonts[gs.Font], gs.Value, gs.Pos, gs.Color, 0f, gs.Origin, gs.Scale, SpriteEffects.None, 0f);
        }

        public Vector2 getSizeString(EFont font, String content)
        {
            return fonts[font].MeasureString(content);
        }
    }
}
