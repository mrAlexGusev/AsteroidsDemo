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
                Icon = Resources.SpaceGame,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                Text = "The best space shooter ever created by anyone."
            };

            Game.Init(form, 15);

            form.Show();
           
            Application.Run(form);
        }
    }
}