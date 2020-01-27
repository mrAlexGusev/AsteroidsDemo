using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    class Star : BaseObject
    {
        /// <summary>
        /// Инициализация объекта Star.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Star(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Метод вывода объекта Star на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Star.
        /// </summary>
        public override void Update()
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
