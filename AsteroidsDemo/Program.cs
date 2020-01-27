using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsteroidsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form
            {
                Size = new Size(800, 600),
            };

            Game.Init(form, 15);

            form.Show();
            Game.Draw();

            Application.Run(form);
        }
    }
}
