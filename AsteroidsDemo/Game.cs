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

            // Подписка на события формы.
            form.KeyDown += KeysHandler.KeyDown;
            form.KeyUp += KeysHandler.KeyUp;

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
        /// Объект корабля.
        /// </summary>
        private static Ship _ship;

        /// <summary>
        /// Загрузка игровых объектов.
        /// </summary>
        private static void Load()
        {
            _objs = new List<BaseObject>();
            _asteroids = new List<Asteroid>();

            #region Добавление объектов Star.

            for (int i = 0; i < 50; i++)
                _objs.Add(new Star(new Vector2(R.Next(Width), R.Next(Height)), new Vector2(), new Size())
                {
                    MinDir = new Vector2(20f, 0),
                    MaxDir = new Vector2(60f, 0),
                    MinSize = new Size(4, 4),
                    MaxSize = new Size(16, 16)
                });

            #endregion

            #region Добавление объекта Planet.

            _objs.Add(new Planet(new Vector2(R.Next(Width), R.Next(Height)), new Vector2(), new Size())
            {
                MinDir = new Vector2(40f, 0),
                MaxDir = new Vector2(80f, 0),
                MinSize = new Size(128, 128),
                MaxSize = new Size(256, 256)
            });

            #endregion

            #region Добавление объекта Ship.

            _ship = new Ship(new Vector2(), new Vector2(), new Size(168, 84))
            {
                MaxDir = new Vector2(200f, 200f)
            };

            #endregion

            #region Добавление объектов Asteroid.

            for (var i = 0; i < 20; i++)
                _asteroids.Add(new Asteroid(new Vector2(), new Vector2(), new Size())
                {
                    MinDir = new Vector2(120f, 20f),
                    MaxDir = new Vector2(160f, 80f),
                    MinSize = new Size(32, 32),
                    MaxSize = new Size(64, 64)
                });

            #endregion


            #region Активизация объектов.

            // Звезды и планета.
            foreach (var obj in _objs)
                obj.Active = true;

            // Астероиды.
            foreach (var a in _asteroids)
                a.Active = true;

            // Корабль.
            _ship.Active = true;

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

            if (_ship.Active) _ship.Draw();

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

            if (_ship.Active) _ship.Update();
        }
    }
}
