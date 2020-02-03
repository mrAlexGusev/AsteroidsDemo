using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс, отвечающий за создание звезд.
    /// </summary>
    public class Star : BaseObject, ISprite, IRandomDirAndSize
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
            Pos.X -= Dir.X * Game.DeltaTime;

            if (!(Pos.X < 0 - Size.Width)) return;

            SetRandomDirAndSize();

            Pos.X = Game.Width + Size.Width;
            Pos.Y = Game.R.Next(Game.Height - Size.Height);
        }

        /// <summary>
        /// Рисуемый объект Image объекта Star.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Минимальная скорость объекта Star.
        /// </summary>
        public Vector2 MinDir { get; set; }

        /// <summary>
        /// Максимальная скорость объекта Star.
        /// </summary>
        public Vector2 MaxDir { get; set; }

        /// <summary>
        /// Минимальный размер объекта Star.
        /// </summary>
        public Size MinSize
        {
            get => _minSize;
            set
            {
                if (value.Width < 0 || value.Height < 0)
                    throw new GameObjectException("Недопустимый размер объекта Star.");

                _minSize = value;
            }
        }

        /// <summary>
        /// Максимальный размер объекта Star.
        /// </summary>
        public Size MaxSize
        {
            get => _maxSize;
            set
            {
                if (value.Width < 0 || value.Height < 0)
                    throw new GameObjectException("Недопустимый размер объекта Star.");

                _maxSize = value;
            }
        }

        /// <summary>
        /// Задает случайное направление и размер объекта Star.
        /// </summary>
        public void SetRandomDirAndSize()
        {
            // Звезда движется только справа налево.

            Dir.X = Game.R.Next((int)MinDir.X, (int)MaxDir.X);

            // Для размера звезды возьмем случайный коэфициент умноженный на
            // максимально возможный размер звезды.

            Size.Width = (int)(Dir.X / MaxDir.X * MaxSize.Width);
            Size.Width = Size.Width < MinSize.Width ? MinSize.Width : Size.Width;

            Size.Height = (int)(Dir.X / MaxDir.X * MaxSize.Height);
            Size.Height = Size.Height < MinSize.Height ? MinSize.Height : Size.Height;
        }

        /// <summary>
        /// При активации устанавливает первоначальные случайные скорость и размер объекта Star.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            SetRandomDirAndSize();
        }

        /// <summary>
        /// Поле, хранящее в себе минимальный размер объекта Star.
        /// </summary>
        private Size _minSize;

        /// <summary>
        /// Поле, хранящее в себе максимальный размер объекта Star.
        /// </summary>
        private Size _maxSize;
        
    }
}
