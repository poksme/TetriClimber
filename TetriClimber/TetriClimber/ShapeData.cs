using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class BackgroundShapeData : GameComponent
    {
        private float movingSpeed;
        private float movingOrientation;
        private int size;
        private Vector2 position;
        private Vector2 invertion;
        private float drawingOrientation;

        public BackgroundShapeData(Vector2 Position = new Vector2(), float MovingSpeed = 1f, float MovingOrientation = 0.5f, int Size = 800):base(App.Game)
        {
            movingSpeed = MovingSpeed;
            movingOrientation = MovingOrientation;
            position = Position;
            size = Size;
            invertion = new Vector2(1f, 1f);
            drawingOrientation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (position.X > Constants.Measures.portraitWidth)
                invertion.X = -1f;
            if (position.Y > Constants.Measures.portraitHeight)
                invertion.Y = -1f;
            if (position.X < 0 - Constants.Measures.BgShapesSize)
                invertion.X = 1f;
            if (position.Y < 0 - Constants.Measures.BgShapesSize)
                invertion.Y = 1f;
            position.X += (float)(gameTime.ElapsedGameTime.Milliseconds * movingSpeed * Math.Cos(movingOrientation)) * invertion.X;
            position.Y += (float)(gameTime.ElapsedGameTime.Milliseconds * movingSpeed * Math.Sin(movingOrientation)) * invertion.Y;
            drawingOrientation = (drawingOrientation + (float)(gameTime.ElapsedGameTime.Milliseconds) / (160 - size)) % 360;
        }

        public Vector2  Position           { get { return position; } }
        public int      Size               { get { return size; } }
        public float    DrawingOrientation { get { return MathHelper.ToRadians(drawingOrientation); } }
    }
}
