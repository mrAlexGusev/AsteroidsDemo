using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    /// <summary>
    /// Базовый класс игрового объекта.
    /// </summary>
    public class BaseObject
    {

        protected Vector2 Pos;
        protected Vector2 Dir;
        protected Size Size;

        public BaseObject(Vector2 pos, Vector2 dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }
}
