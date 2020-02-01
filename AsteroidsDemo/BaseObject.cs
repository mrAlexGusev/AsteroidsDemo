using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    /// <summary>
    /// Базовый класс игрового объекта.
    /// </summary>
    public abstract class BaseObject
    {
        /// <summary>
        /// Положение BaseObject.
        /// </summary>
        protected Vector2 Pos;

        /// <summary>
        /// Направление BaseObject.
        /// </summary>
        protected Vector2 Dir;

        /// <summary>
        /// Размер BaseObject.
        /// </summary>
        protected Size Size;

        /// <summary>
        /// Инициализация BaseObject.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public BaseObject(Vector2 pos, Vector2 dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;

            Active = false;
        }

        /// <summary>
        /// Метод вывода объекта BaseObject на экран.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Метод изменения состояния объекта BaseObject.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Поле, хранящее в себе активное состояние объекта BaseObject.
        /// </summary>
        private bool _active;

        /// <summary>
        /// Показывает, активен ли BaseObject для отрисовки.
        /// </summary>
        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value) return;

                _active = value;
                if (_active) OnEnable();
                else OnDisable();
            }
        }

        /// <summary>
        /// Метод вызывается при активации BaseObject.
        /// </summary>
        protected virtual void OnEnable() { }

        /// <summary>
        /// Метод вызывается при деактивации BaseObject.
        /// </summary>
        protected virtual void OnDisable() { }
    }
}
