﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Windows.Forms;

namespace AsteroidsDemo
{
    public static class Game
    {
        private static BufferedGraphicsContext _context;

        /// <summary>
        /// Включение режима обучения.
        /// </summary>
        public static bool Tutorial;

        /// <summary>
        /// Звук ремонта.
        /// </summary>
        private static readonly SoundPlayer RepairSound;

        /// <summary>
        /// Звук уничтожения корабля.
        /// </summary>
        private static readonly SoundPlayer GameOverSound;

        /// <summary>
        /// Звук столкновения с астероидом.
        /// </summary>
        private static readonly SoundPlayer HitSound;

        /// <summary>
        /// Количество очков.
        /// </summary>
        public static int Score;

        /// <summary>
        /// Ожидание следующего уровня.
        /// </summary>
        public static bool WaitNextLevel;

        /// <summary>
        /// Уровень игры.
        /// </summary>
        public static int Level;

        /// <summary>
        /// Продолжительность паузы между уровнями.
        /// </summary>
        private static readonly int BetweenLevelDelay;

        static Game()
        {
            R = new Random();

            // Установка звуков.
            RepairSound = new SoundPlayer(Resources.RepairSound);
            HitSound = new SoundPlayer(Resources.Hit);
            GameOverSound = new SoundPlayer(Resources.GameOver);

            // Установка времени паузы.
            BetweenLevelDelay = 3;

            // Стартовые параметры.
            WaitNextLevel = false;
            Score = 0;
            Level = 0;

            // Устанавливаем запись событий в консоль.
            Log.WriteLogEvent += Log.WriteToConsole;

            // Устанавливаем запись событий в файл.
            Log.WriteLogEvent += Log.WriteToFile;
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
            FPS = 1000 / interval;
            _fpsCounter = 0;

            // При запуске показываем обучение.
            Tutorial = true;

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

            _lastFrame = DateTime.Now;

            _lastUpdate = DateTime.Now;

            timer.Start();
            timer.Tick += Timer_Tick;

            Log.WriteLine("Game initialised");
        }

        /// <summary>
        /// Время между кадрами в зависимости от заданного FPS.
        /// </summary>
        public static float DeltaTime { get; private set; }

        /// <summary>
        /// Количество кадров в секунду.
        /// </summary>
        public static int FPS { get; set; }

        /// <summary>
        /// Время отрисовки последнего кадра.
        /// </summary>
        private static DateTime _lastFrame;

        /// <summary>
        /// Счетчик FPS.
        /// </summary>
        private static int _fpsCounter;

        /// <summary>
        /// Время последнего Update.
        /// </summary>
        private static DateTime _lastUpdate;

        private static DateTime _loadStartedTime;

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
        public static List<Asteroid> Asteroids;

        /// <summary>
        /// Объект корабля.
        /// </summary>
        public static Ship Ship;

        /// <summary>
        /// Объект ремонтного комплекта.
        /// </summary>
        public static Repair Repair;

        /// <summary>
        /// Объект пользовательского интерфейса.
        /// </summary>
        public static UserInterface UI;
        
        /// <summary>
        /// Загрузка игровых объектов.
        /// </summary>
        private static void Load()
        {
            _objs = new List<BaseObject>();
            Asteroids = new List<Asteroid>();

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

            #region Добавление объекта Repair.

            Repair = new Repair(new Vector2(), new Vector2(120f, 0), new Size(48, 48));

            #endregion

            #region Добавление объекта Ship.

            Ship = new Ship(new Vector2(), new Vector2(), new Size(168, 84))
            {
                MaxDir = new Vector2(200f, 200f),
                ShotsDelay = 0.2f,
                MaxEnergy = 30
            };

            BulletPool.Speed = 600f;

            #endregion
            
            #region UI.

            // Создаем объект выводящий информацию о корабле.
            UI = new UserInterface(new Vector2(), new Vector2(), new Size());

            #endregion


            #region Активизация объектов.

            // Звезды и планета.
            foreach (var obj in _objs)
                obj.Active = true;
            
            // Корабль.
            Ship.Active = true;

            // Интерфейс
            UI.Active = true;

            #endregion
        }

        /// <summary>
        /// Наполнение и вывод буфера на экран.
        /// </summary>
        public static void Draw()
        {
            if((DateTime.Now - _lastFrame).TotalMilliseconds <= 1000)
            {
                _fpsCounter++;
            }
            else
            {
                FPS = _fpsCounter;
                _fpsCounter = 0;
                _lastFrame = DateTime.Now;
            }

            Buffer.Graphics.Clear(Color.Black);

            foreach (var obj in _objs.Where(o => o.Active))
                obj.Draw();

            // При обучении отключаем астероиды и рем. комплекты.
            if (!Tutorial)
            {
                if (Repair.Active) Repair.Draw();

                foreach (var asteroid in Asteroids.Where(a => a.Active))
                    asteroid.Draw();
            }
            
            if (Ship.Active) Ship.Draw();

            UI.Draw();

            Buffer.Render();
        }

        /// <summary>
        /// Обновление объектов.
        /// </summary>
        private static void Update()
        {
            // Время расчета между двумя кадрами.
            DeltaTime = (float)(DateTime.Now - _lastUpdate).TotalMilliseconds / 1000;

            foreach (var obj in _objs)
                obj.Update();

            // Выключаем обучение или перезапускаем игру.
            if (KeysHandler.IsPressed(Keys.Enter))
            {
                Tutorial = false;

                #region Перезапуск игры.

                if (!Ship.Active)
                {
                    foreach (var asteroid in Asteroids)
                        asteroid.Active = false;

                    Repair.Active = false;
                    Ship.Active = true;

                    Score = 0;
                    Level = 0;
                }

                #endregion
            }

            // При обучении отключаем астероиды и рем. комплекты.
            if (!Tutorial)
            {
                // Проверяем, нужно ли включить астероиды.
                if (Asteroids.Count(a => a.Active) == 0)
                {
                    if (WaitNextLevel)
                    {
                        WaitNextLevel = (DateTime.Now - _loadStartedTime).TotalSeconds <= BetweenLevelDelay;

                        if (!WaitNextLevel) ActivateAsteroid();
                    }
                    else
                    {
                        Level++;
                        LoadLevel();
                    }
                }

                // Обновляем рем. комплект.
                // Если астероидов менее 5, то рем. комплект не активируем.
                if (!WaitNextLevel && Asteroids.Count(a => a.Active) > 5) Repair.Active = true;
                if (Repair.Active) Repair.Update();

                // Проверяем столкновение рем. комплекта и коробля.
                if(Ship.Active && Repair.Active && Ship.Collision(Repair))
                {
                    Ship.ChangeEnergy(5);
                    Repair.Active = false;
                    RepairSound.Play();

                    Log.WriteLine("Ship is repaid.");
                }

                // Обновляем активные астероиды.
                foreach (var asteroid in Asteroids.Where(a => a.Active))
                {
                    asteroid.Update();

                    if (!Ship.Active || !Ship.Collision(asteroid)) continue;

                    asteroid.Active = false;

                    // Получаем урон в зависимости от размера астероида.
                    Ship.ChangeEnergy(-10 * asteroid.Size.Width / asteroid.MaxSize.Width);

                    Log.WriteLine("Ship is hit by asteroid.");

                    if (Ship.Energy == 0)
                    {
                        Ship.Active = false;
                        GameOverSound.Play();

                        Log.WriteLine("Ship is destroyed.");
                        break;
                    }

                    HitSound.Play();
                }
                    
            }                        

            if (Ship.Active) Ship.Update();
            UI.Draw();

            _lastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Создаем астероиды для уровня.
        /// </summary>
        private static void LoadLevel()
        {
            // Устанавливаем паузу.
            WaitNextLevel = true;
            _loadStartedTime = DateTime.Now;

            // Количество астероидов, которые необходимо добавить.
            var needToAdd = 15 + Level * 5 - Asteroids.Count;

            for (var i = 0; i < needToAdd; i++)
                Asteroids.Add(new Asteroid(new Vector2(), new Vector2(), new Size())
                {
                    MinDir = new Vector2(120f, 20f),
                    MaxDir = new Vector2(160f, 80f),
                    MinSize = new Size(32, 32),
                    MaxSize = new Size(64, 64)
                });
        }

        /// <summary>
        /// Активируем необходимое количество астероидов.
        /// </summary>
        private static void ActivateAsteroid()
        {
            foreach (var a in Asteroids)
                a.Active = true;

            Log.WriteLine($"Level {Level} started.");
        }
    }
}
