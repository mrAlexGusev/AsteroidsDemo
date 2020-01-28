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
    public static class Game
    {
        private static BufferedGraphicsContext _context;

        static Game()
        {
            R = new Random();
        }

        /// <summary>
        /// Графический буфер.
        /// </summary>
        public static BufferedGraphics Buffer { get; set; }

        /// <summary>
        /// Ширина графическового вывода.
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота графическового вывода.
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Объект генератора псевдослучайных чисел.
        /// </summary>
        public static Random R { get; }

        /// <summary>
        /// Инициализация игры.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="v"></param>
        public static void Init(Form form, int interval)
        {
            // Определение размера.
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            // Создание графического элемента и буфера.
            var g = form.CreateGraphics();
            _context = BufferedGraphicsManager.Current;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            // Загрузка объектов.
            Load();

            // Установка таймера.
            var timer = new Timer { Interval = interval };

            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Вызовы методов по таймеру.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Коллекция игровых объектов.
        /// </summary>
        private static List<BaseObject> _objs;

        /// <summary>
        /// Коллекция астероидов.
        /// </summary>
        private static List<Asteroid> _asteroids;

        /// <summary>
        /// Загрузка игровых объектов.
        /// </summary>
        private static void Load()
        {
            _objs = new List<BaseObject>();
            _asteroids = new List<Asteroid>();

            #region Добавление объектов Star.

            for (int i = 0; i < 50; i++)
                _objs.Add(new Star(new Vector2(600, i * 20), new Vector2(15 - i, 15 - i), new Size(20, 20)));

            #endregion

            #region Добавление объекта Planet.

            _objs.Add(new Planet(new Vector2(200, 200), new Vector2(10, 10), new Size(150, 150)));

            #endregion

            #region Добавление объектов Asteroid.

            for (var i = 0; i < 20; i++)
                _asteroids.Add(new Asteroid(new Vector2(R.Next(Width), R.Next(Height)), new Vector2(5, 10), new Size(30, 30)));
                
            #endregion
        }

        /// <summary>
        /// Наполнение и вывод буфера на экран.
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (var obj in _objs)
                obj.Draw();

            foreach (var a in _asteroids)
                a.Draw();

            Buffer.Render();
        }

        /// <summary>
        /// Обновление объектов.
        /// </summary>
        private static void Update()
        {
            foreach (var obj in _objs)
                obj.Update();

            foreach (var a in _asteroids)
                a.Update();
        }
    }
}
