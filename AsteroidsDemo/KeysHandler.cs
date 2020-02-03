using System.Collections.Generic;
using System.Windows.Forms;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс обработки нажатых клавиш.
    /// </summary>
    static class KeysHandler
    {
        /// <summary>
        /// Список нажатых клавиш.
        /// </summary>
        private static readonly List<Keys> _keys;

        /// <summary>
        /// Инициализация.
        /// </summary>
        static KeysHandler()
        {
            _keys = new List<Keys>();
        }

        /// <summary>
        /// Обработчик события нажатия клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsPressed(e.KeyCode)) _keys.Add(e.KeyCode);
        }

        /// <summary>
        /// Обработчик события отжатия клавиш.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void KeyUp(object sender, KeyEventArgs e)
        {
            _keys.Remove(e.KeyCode);
        }

        /// <summary>
        /// Проверка нажатия клавиши.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsPressed(Keys key)
        {
            return _keys.Contains(key);
        }
    }
}
