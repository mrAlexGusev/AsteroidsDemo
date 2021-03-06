﻿using System.Drawing;
using System.Linq;
using System.Media;
using System.Numerics;

namespace AsteroidsDemo
{
    public class Bullet : BaseObject, ISprite, ICollision
    {
        /// <summary>
        /// Инициализация объекта Bullet.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Bullet(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            Sprite = Resources.Bullet;
            _explosionSound = new SoundPlayer(Resources.Explosion);
        }

        /// <summary>
        /// Рисуемый объект Image объекта Bullet.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Метод вывода объекта Bullet на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Bullet.
        /// </summary>
        public override void Update()
        {
            // Пуля летит слева направо.
            Pos.X += Dir.X * Game.DeltaTime;

            foreach(var asteroid in Game.Asteroids.Where(a => a.Active).Where(Collision))
            {
                Game.Score++;

                Active = false;
                asteroid.Active = false;
                _explosionSound.Play();

                Log.WriteLine("Asteroid is destroyed.");
            }

            // Если пуля за пределами, то уничтожаем.
            if (Pos.X > Game.Width - Size.Width) Active = false;
        }

        /// <summary>
        /// Задает область столкновения.
        /// </summary>
        public Rectangle Rect => new Rectangle((int)Pos.X, (int)Pos.Y, Size.Width, Size.Height);

        /// <summary>
        /// Метод проверки столкновения объектов.
        /// </summary>
        /// <param name="o"> Другой объект. </param>
        /// <returns></returns>
        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(Rect);
        }

        /// <summary>
        /// Звук взрыва астероида.
        /// </summary>
        private readonly SoundPlayer _explosionSound;
    }
}
