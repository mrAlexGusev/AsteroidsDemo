using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс, отвечающий за создание коробля.
    /// </summary>
    class Ship : BaseObject, ISprite, ICollision
    {
        /// <summary>
        /// Инициализация объекта Ship.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Ship(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            Sprite = Resources.SpaceShip;
            _bulletSpawn = new Vector2(Size.Width * 0.82f, Size.Height * 0.48f);
        }

        /// <summary>
        /// Рисуемый объект Image объекта Ship.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Задает область столкновения.
        /// </summary>
        public Rectangle Rect => new Rectangle((int)(Pos.X + Size.Width * 0.35), (int)(Pos.Y + Size.Height * 0.1),
            (int)(Size.Width * 0.6), (int)(Size.Height * 0.8));

        /// <summary>
        /// Метод проверки столкновения объектов.
        /// </summary>
        /// <param name="o"> Другой объект. </param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);

        /// <summary>
        /// Метод вывода объекта Ship на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Ship.
        /// </summary>
        public override void Update()
        {
            // Устанавливаем направление объекта.
            SetDirection();

            // Перемещаем объект.
            Pos.X += Dir.X * Game.DeltaTime;
            Pos.Y += Dir.Y * Game.DeltaTime;

            // Если объект вышел за границу, то возвращаем обратно.
            if (Pos.X < -Size.Width * 0.3) Pos.X = (int)(-Size.Width * 0.3);
            if (Pos.X > Game.Width - Size.Width) Pos.X = Game.Width - Size.Width;
            if (Pos.Y < 0) Pos.Y = 0;
            if (Pos.Y > Game.Height - Size.Height) Pos.Y = Game.Height - Size.Height;
        }

        protected override void OnEnable()
        {
            Pos.X = 0;
            Pos.Y = Game.Height / 2 - Size.Height / 2;
        }

        /// <summary>
        /// Максимальная скорость объекта.
        /// </summary>
        public Vector2 MaxDir { get; set; }

        /// <summary>
        /// Метод перемещения объекта.
        /// </summary>
        private void SetDirection()
        {
            if (KeysHandler.IsPressed(Keys.W) || KeysHandler.IsPressed(Keys.Up))
                Dir.Y = -MaxDir.Y;
            else if (KeysHandler.IsPressed(Keys.S) || KeysHandler.IsPressed(Keys.Down))
                Dir.Y = MaxDir.Y;
            else
                Dir.Y = 0;

            if (KeysHandler.IsPressed(Keys.A) || KeysHandler.IsPressed(Keys.Left))
                Dir.X = -MaxDir.X;
            else if (KeysHandler.IsPressed(Keys.D) || KeysHandler.IsPressed(Keys.Right))
                Dir.X = MaxDir.X;
            else
                Dir.X = 0;
        }

        /// <summary>
        /// Место создания снаряда.
        /// </summary>
        private readonly Vector2 _bulletSpawn;

    }
}
