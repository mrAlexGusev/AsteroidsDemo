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
            Sprite = Resources.Star;
        }

        /// <summary>
        /// Метод вывода объекта Star на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Star.
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X;

            if (!(Pos.X < 0 - Size.Width)) return;

            Pos.X = Game.Width + Size.Width;
            Pos.Y = Game.R.Next(Game.Height - Size.Height);
        }

        /// <summary>
        /// Рисуемый объект Image объекта Star.
        /// </summary>
        public Bitmap Sprite { get; set; }
    }
}
