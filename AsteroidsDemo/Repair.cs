using System;
using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    class Repair : BaseObject, ICollision, ISprite
    {
        /// <summary>
        /// Инициализация объекта Repair.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Repair(Vector2 pos, Vector2 dir,Size size) : base(pos, dir, size)
        {
            Sprite = Resources.Repair;
        }

        /// <summary>
        ///  Задает область столкновения.
        /// </summary>
        public Rectangle Rect => new Rectangle((int)(Pos.X + Size.Width * 0.15), (int)(Pos.Y + Size.Height * 0.15),
            (int)(Size.Width * 0.7), (int)(Size.Height * 0.7));

        /// <summary>
        /// Рисуемый объект Image объекта Repair.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Метод проверки столкновения объектов.
        /// </summary>
        /// <param name="o"> Другой объект. </param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);

        /// <summary>
        /// Метод вывода объекта Repair на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Repair.
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X * Game.DeltaTime;
            Pos.Y += (float)Math.Sin((double)DateTime.Now.Millisecond / 159);

            if (Pos.X < 0 - Size.Width) Active = false;
        }

        /// <summary>
        /// Метод задающий начальные параметры при активации объекта Repair.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            Pos.X = Game.Width * 2 + Pos.X;
            Pos.Y = Game.R.Next(Game.Height - Size.Height);
        }
    }
}
