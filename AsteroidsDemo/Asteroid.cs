﻿using System.Drawing;
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
                Pos.X = Game.Width + Size.Width;

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
    }
}