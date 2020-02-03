using System.Drawing;

namespace AsteroidsDemo
{
    /// <summary>
    /// Интерфейс объектов, которые могут сталкиваться.
    /// </summary>
    public interface ICollision
    {
        /// <summary>
        /// Задает область столкновения.
        /// </summary>
        Rectangle Rect { get; }

        /// <summary>
        /// Проверка столкновения объектов.
        /// </summary>
        /// <param name="obj"> Другой объект. </param>
        /// <returns></returns>
        bool Collision(ICollision obj);
    }
}
