using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    class Planet : BaseObject
    {
        /// <summary>
        /// Инициализация объекта Planet.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Planet(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            Sprite = Sprites[Game.R.Next(Sprites.Length)];
        }

        /// <summary>
        /// Метод вывода объекта Planet на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Planet.
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X;

            if (!(Pos.X < 0 - Size.Width)) return;

            Pos.X = Game.Width + Size.Width;
            Pos.Y = Game.R.Next(Game.Height - Size.Height);
        }

        /// <summary>
        /// Рисуемый объект Image объекта Planet.
        /// </summary>
        public Bitmap Sprite { get; set; }

        private static readonly Bitmap[] Sprites;

        static Planet()
        {
            Sprites = new[]
            {
                Resources.Exoplanet,
                Resources.Exoplanet2,
                Resources.Exoplanet3,
                Resources.RedGiant,
                Resources.IceGiant,
                Resources.GasGiant,
                Resources.Sun
            };
        }
    }
}
