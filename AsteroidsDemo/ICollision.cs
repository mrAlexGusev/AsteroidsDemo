using System.Drawing;

namespace AsteroidsDemo
{
    /// <summary>
    /// Интерфейс объектов, которые могут сталкиваться.
    /// </summary>
    interface ICollision
    {
        Rectangle Rect { get; }

        bool Collision(ICollision obj);
    }
}
