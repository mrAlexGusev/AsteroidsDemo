using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    /// <summary>
    /// Определяет случайные размер объекта и направление движения.
    /// </summary>
    interface IRandomDirAndSize
    {
        /// <summary>
        /// Минимальная скорость объекта.
        /// </summary>
        Vector2 MinDir { get; set; }

        /// <summary>
        /// Максимальная скорость объекта.
        /// </summary>
        Vector2 MaxDir { get; set; }

        /// <summary>
        /// Минимальный размер объекта.
        /// </summary>
        Size MinSize { get; set; }

        /// <summary>
        /// Максимальный размер объекта.
        /// </summary>
        Size MaxSize { get; set; }

        /// <summary>
        /// Задает случайное направление и размер объекта.
        /// </summary>
        void SetRandomDirAndSize();
    }
}
