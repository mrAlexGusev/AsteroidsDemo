using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AsteroidsDemo
{
    /// <summary>
    /// Пул снарядов.
    /// </summary>
    static class BulletPool
    {
        /// <summary>
        /// Коллекция снарядов.
        /// </summary>
        private static readonly List<Bullet> _bullets;

        static BulletPool()
        {
            _bullets = new List<Bullet>();
        }

        /// <summary>
        /// Скорость снаряда.
        /// </summary>
        public static float Speed { get; set; }

        /// <summary>
        /// Выполняем Update во всех не уничтоженных объектах Bullet.
        /// </summary>
        public static void Update()
        {
            foreach (var bullet in _bullets.Where(b => b.Active))
                bullet.Update();
        }

        /// <summary>
        /// Выполняем Draw во всех не уничтоженных объектах Bullet.
        /// </summary>
        public static void Draw()
        {
            foreach (var bullet in _bullets.Where(b => b.Active))
                bullet.Draw();
        }

        /// <summary>
        /// Возвращает первый уничтоженный снаряд или создает новый снаряд.
        /// </summary>
        /// <param name="spawn"> Место создания снаряда. </param>
        /// <returns></returns>
        public static Bullet GetNext(Vector2 spawn)
        {
            Bullet bullet;

            var index = _bullets.FindIndex(b => !b.Active);

            if (index == -1)
            {
                bullet = new Bullet(spawn, new Vector2(Speed, 0), new Size(19, 7));

                _bullets.Add(bullet);
            }
            else
            {
                bullet = _bullets[index];
                bullet.Pos = spawn;
            }

            bullet.Active = true;

            return bullet;
        }
    }
}
