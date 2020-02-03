using System.Drawing;
using System.Numerics;

namespace AsteroidsDemo
{
    class Bullet : BaseObject, ISprite, ICollision
    {
        /// <summary>
        /// Инициализация объекта Bullet.
        /// </summary>
        /// <param name="pos"> Положение. </param>
        /// <param name="dir"> Направление. </param>
        /// <param name="size"> Размер. </param>
        public Bullet(Vector2 pos, Vector2 dir, Size size) : base(pos, dir, size)
        {
            Sprite = Resources.Bullet;
        }

        /// <summary>
        /// Рисуемый объект Image объекта Bullet.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Метод вывода объекта Bullet на экран.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Sprite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод изменения состояния объекта Bullet.
        /// </summary>
        public override void Update()
        {
            // Пуля летит слева направо.
            Pos.X += Dir.X * Game.DeltaTime;

            // Если пуля за пределами, то уничтожаем.
            if (Pos.X > Game.Width - Size.Width) Active = false;
        }

        /// <summary>
        /// Задает область столкновения.
        /// </summary>
        public Rectangle Rect => new Rectangle((int)Pos.X, (int)Pos.Y, Size.Width, Size.Height);

        /// <summary>
        /// Метод проверки столкновения объектов.
        /// </summary>
        /// <param name="o"> Другой объект. </param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);

    }
}
