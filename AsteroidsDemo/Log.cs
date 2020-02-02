using System;
using System.IO;

namespace AsteroidsDemo
{
    /// <summary>
    /// Класс ведения логов игры.
    /// </summary>
    static class Log
    {
        /// <summary>
        /// Событие записи лога.
        /// </summary>
        public static event Action<string> WriteLogEvent;

        /// <summary>
        /// Запуск события записи лога.
        /// </summary>
        /// <param name="message"> Сообщение лога. </param>
        public static void WriteLine(string message)
        {
            WriteLogEvent?.Invoke($@"{DateTime.Now} {message}");
        }

        /// <summary>
        /// Метод вывода сообщения в файл.
        /// </summary>
        /// <param name="message"> Сообщение лога. </param>
        public static void WriteToFile(string message)
        {
            using(var w = File.AppendText("log.txt"))
            {
                w.WriteLine(message);
            }
        }

        /// <summary>
        /// Метод вывода сообщения в консоль.
        /// </summary>
        /// <param name="message"> Сообщение лога. </param>
        public static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
