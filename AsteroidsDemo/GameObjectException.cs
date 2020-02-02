using System;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс собственных ошибок в игре. Требуется по заданию.
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message) { }
    }
}
