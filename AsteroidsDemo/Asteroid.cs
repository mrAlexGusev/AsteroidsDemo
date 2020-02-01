using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    class Asteroid : BaseObject, ISprite
    {
        /// <summary>
        /// Инициализация объекта Asteroid.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Asteroid(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            Sprite = Sprites[Game.R.Next(Sprites.Length)];
        }

        /// <summary>
        /// Метод вывода объекта Asteroid на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Asteroid.
        /// </summary>
        public override void Update()
        {
            // Астероид двигается
            Pos.X -= Dir.X;
            Pos.Y += Dir.Y;

            // Астеройд вышел за границу по оси Х
            if (Pos.X < 0 - Size.Width)
            {
                SetRandomDisAndSize();
                return;
            }

            // Астеройд вышел за границу по оси Y
            if (Pos.Y < 0 - Size.Height)
                Pos.Y = Game.Height + Size.Height;

            if (Pos.Y > Game.Height + Size.Height)
                Pos.Y = 0 - Size.Height;
        }

        /// <summary>
        /// Рисуемый объект Image объекта Asteroid.
        /// </summary>
        public Bitmap Sprite { get; set; }

        private static readonly Bitmap[] Sprites;

        static Asteroid()
        {
            Sprites = new[]
            {
                Resources.Asteroid1,
                Resources.Asteroid2,
                Resources.Asteroid3,
                Resources.Asteroid4,
                Resources.Asteroid5,
                Resources.Asteroid6
            };
        }

        /// <summary>
        /// Минимальная скорость объекта Asteroid.
        /// </summary>
        public Vector2 MinDir { get; set; }

        /// <summary>
        /// Максимальная скорость объекта Asteroid.
        /// </summary>
        public Vector2 MaxDir { get; set; }

        /// <summary>
        /// Минимальный размер объекта Asteroid.
        /// </summary>
        public Size MinSize { get; set; }

        /// <summary>
        /// Максимальный размер объекта Asteroid.
        /// </summary>
        public Size MaxSize { get; set; }

        /// <summary>
        /// Задает случайное направление и размер объекта Asteroid.
        /// </summary>
        public void SetRandomDisAndSize()
        {
            // Первоначальные координаты Астероида.

            Pos.X = Game.Width + Size.Width;
            Pos.Y = Game.R.Next(Game.Height - Size.Height);

            // Астероид движется справо налево по оси Х и
            // сверху вниз или снизу вверх по оси Y.

            Dir.X = Game.R.Next((int)MinDir.X, (int)MaxDir.X);
            Dir.Y = Game.R.Next((int)MinDir.Y, (int)MaxDir.Y);

            Dir.Y *= Game.R.Next(0, 2) == 1 ? -1 : 1;

            // Для размера астероида возьмем случайный коэфициент умноженный на
            // максимально возможный размер астероида.

            Size.Width = (int)(Dir.X / MaxDir.X * MaxSize.Width);
            Size.Width = Size.Width < MinSize.Width ? MinSize.Width : Size.Width;

            Size.Height = (int)(Dir.X / MaxDir.X * MaxSize.Height);
            Size.Height = Size.Height < MinSize.Height ? MinSize.Height : Size.Height;
        }

        /// <summary>
        /// При активации устанавливает первоначальные случайные скорость и размер объекта Asteroid.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            SetRandomDisAndSize();
        }
    }
}
