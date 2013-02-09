using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Background : AScene
    {
        //public enum ShapesId { BLUE_SQUARE, GREEN_CROSS, GREEN_SQUARE, ORANGE_TRIANGLE, PINK_CROSS, VIOLET_HEXAGONE, YELLOW_LINES };
        //private Dictionary<ShapesId, KeyValuePair<TextureManager.ETexture, BackgroundShapeData>> shapes;
        private List<KeyValuePair<TextureManager.ETexture, BackgroundShapeData>> shapes;

        public Background()
            : base()
        {
            shapes = new List<KeyValuePair<TextureManager.ETexture, BackgroundShapeData>>();
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.BLUE_SQUARE, new BackgroundShapeData(new Vector2(0, 0), 0.01f, 0, 400)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.GREEN_CROSS, new BackgroundShapeData(new Vector2(Constants.Measures.portraitWidth, 0), 0.012f, 360 / 7 * 1, 200)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.GREEN_SQUARE, new BackgroundShapeData(new Vector2(0, Constants.Measures.portraitHeight), 0.014f, 360 / 7 * 2, 300)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.ORANGE_TRIANGLE, new BackgroundShapeData(new Vector2(Constants.Measures.portraitWidth, Constants.Measures.portraitHeight), 0.015f, 360 / 7 * 3, 250)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.PINK_CROSS, new BackgroundShapeData(new Vector2(50, 50), 0.016f, 360 / 7 * 4, 100)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.VIOLET_HEXAGONE, new BackgroundShapeData(new Vector2(800, 200), 0.018f, 360 / 7 * 5, 700)));
            shapes.Add(new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(
                TextureManager.ETexture.YELLOW_LINES, new BackgroundShapeData(new Vector2(50, 50), 0.02f, 360 / 7 * 6, 600)));
            SoundManager.Instance.bgmPlay();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (KeyValuePair<TextureManager.ETexture, BackgroundShapeData> shape in shapes)
                shape.Value.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (KeyValuePair<TextureManager.ETexture, BackgroundShapeData> shape in shapes)
                SpriteManager.Instance.drawShapeAtPos(shape.Value.Position, shape.Key, shape.Value.DrawingOrientation, shape.Value.Size);
        }
    }
}
