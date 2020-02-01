using System.Drawing;

namespace AsteroidsDemo
{
    /// <summary>
    /// Определяет свойство для вывода на экран объекта Image.
    /// </summary>
    interface ISprite
    {
        Bitmap Sprite { get; set; }
    }
}