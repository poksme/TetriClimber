using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class Background : DrawableGameComponent
    {
        public enum ShapesId { BLUE_SQUARE, GREEN_CROSS, GREEN_SQUARE, ORANGE_TRIANGLE, PINK_CROSS, VIOLET_HEXAGONE, YELLOW_LINES };
        private Dictionary<ShapesId, KeyValuePair<TextureManager.ETexture, BackgroundShapeData>> shapes;

        public Background()
            : base(App.Game)
        {
            shapes = new Dictionary<ShapesId, KeyValuePair<TextureManager.ETexture, BackgroundShapeData>>();
            shapes[ShapesId.BLUE_SQUARE] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.BLUE_SQUARE,
                new BackgroundShapeData(new Vector2(0, 0), 0.01f, MathHelper.ToRadians(0), 400));
            shapes[ShapesId.GREEN_CROSS] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.GREEN_CROSS,
                new BackgroundShapeData(new Vector2(Constants.Measures.portraitWidth, 0), 0.012f, MathHelper.ToRadians(360 / 7 * 1), 200));
            shapes[ShapesId.GREEN_SQUARE] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.GREEN_SQUARE,
                new BackgroundShapeData(new Vector2(0, Constants.Measures.portraitHeight), 0.014f, MathHelper.ToRadians(360 / 7 * 2), 300));
            shapes[ShapesId.ORANGE_TRIANGLE] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.ORANGE_TRIANGLE,
                new BackgroundShapeData(new Vector2(Constants.Measures.portraitWidth, Constants.Measures.portraitHeight), 0.015f, MathHelper.ToRadians(360 / 7 * 3), 250));
            shapes[ShapesId.PINK_CROSS] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.PINK_CROSS,
                new BackgroundShapeData(new Vector2(50, 50), 0.016f, MathHelper.ToRadians(360 / 7 * 4), 100));
            shapes[ShapesId.VIOLET_HEXAGONE] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.VIOLET_HEXAGONE,
                new BackgroundShapeData(new Vector2(800, 200), 0.018f, MathHelper.ToRadians(360 / 7 * 5), 700));
            shapes[ShapesId.YELLOW_LINES] = new KeyValuePair<TextureManager.ETexture, BackgroundShapeData>(TextureManager.ETexture.YELLOW_LINES,
                new BackgroundShapeData(new Vector2(50, 50), 0.02f, MathHelper.ToRadians(360 / 7 * 6), 600));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (KeyValuePair<ShapesId, KeyValuePair<TextureManager.ETexture, BackgroundShapeData>> shape in shapes)
                shape.Value.Value.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (KeyValuePair<ShapesId, KeyValuePair<TextureManager.ETexture, BackgroundShapeData>> shape in shapes)
                SpriteManager.Instance.drawShapeAtPos(shape.Value.Value.Position, shape.Value.Key, shape.Value.Value.DrawingOrientation, shape.Value.Value.Size);
        }
    }
}
