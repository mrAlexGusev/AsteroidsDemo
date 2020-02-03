using System;
using System.Drawing;
using System.Drawing.Text;
using System.Numerics;
using System.Runtime.InteropServices;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс пользовательского интерфейса.
    /// </summary>
    public class UserInterface : BaseObject
    {
        /// <summary>
        /// Коллекция семейств шрифтов.
        /// </summary>
        private readonly PrivateFontCollection _pfc;

        /// <summary>
        /// Кастомный шрифт.
        /// </summary>
        private readonly Font _font;

        /// <summary>
        /// Кастомный шрифт для обучения.
        /// </summary>
        private readonly Font _smallFont;

        /// <summary>
        /// Прямоугольник для последующего выравнивания по центру.
        /// </summary>
        private readonly Rectangle _rectangle;

        /// <summary>
        /// Формат строки для выравнивания текста в центре экрана.
        /// </summary>
        private readonly StringFormat _centerFormat;

        /// <summary>
        /// Формат строки для выравнивания текста в центре экрана.
        /// </summary>
        private readonly StringFormat _bottomFormat;

        /// <summary>
        /// Формат строки для выравнивания текста в правом верхнем углу.
        /// </summary>
        private readonly StringFormat _topRightFormat;

        public UserInterface(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            _pfc = LoadFont(Resources.FutureFont);
            _font = new Font(_pfc.Families[0], 20);
            _smallFont = new Font(_pfc.Families[0], 10);

            _rectangle = new Rectangle(0, 0, Game.Width, Game.Height);
            _centerFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            _bottomFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far
            };
            _topRightFormat = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Near
            };
        }

        /// <summary>
        /// Загрузка шрифта из ресурсов.
        /// </summary>
        /// <param name="fBytes"> Файл шрифтов. </param>
        /// <returns></returns>
        private PrivateFontCollection LoadFont(byte[] fBytes)
        {
            var pfc = new PrivateFontCollection();

            var fontLength = fBytes.Length;

            var fontdata = fBytes;

            var data = Marshal.AllocCoTaskMem(fontLength);

            Marshal.Copy(fontdata, 0, data, fontLength);

            pfc.AddMemoryFont(data, fontLength);

            return pfc;
        }

        /// <summary>
        /// Выводим UI.
        /// </summary>
        public override void Draw()
        {
            if (Game.Ship.Active && !Game.Tutorial)
            {
                Game.Buffer.Graphics.DrawString($"Energy: {Game.Ship.Energy}", _font, Brushes.White, 0, 0);
                Game.Buffer.Graphics.DrawString($"Score: {Game.Score}", _font, Brushes.White, 0, 30);

                if (Game.WaitNextLevel)
                    Game.Buffer.Graphics.DrawString($"Level {Game.Level}", _font, Brushes.White,
                        _rectangle, _centerFormat);
            }
            else if (!Game.Ship.Active)
            {
                Game.Buffer.Graphics.DrawString($"Game over\nYour score: {Game.Score}\nPress ENTER to restart",
                    _font, Brushes.White, _rectangle, _centerFormat);
            }
            else if (Game.Tutorial)
            {
                Game.Buffer.Graphics.DrawString("Press ENTER to start", _font, Brushes.White,
                    _rectangle, _centerFormat);
                Game.Buffer.Graphics.DrawString("Tutorial: Use the arrows or WASD to move. Use SPACE to shoot",
                    _smallFont, Brushes.White, _rectangle, _bottomFormat);
            }

            Game.Buffer.Graphics.DrawString($"FPS: {Game.FPS}", _smallFont, Brushes.White, _rectangle, _topRightFormat);
            
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
