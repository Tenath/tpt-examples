using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHandInSuite
{
    class Program
    {
        /* Функции с выполненными заданиями добавляем сюда, либо в свой класс */
        static void ExampleTask()
        {
            Console.WriteLine("Running example task");
        }
        /* Функции заданий - КОНЕЦ */

        static void Main(string[] args)
        {
            Application app = new Application();
            // Поочерёдно регистрируем функции с выполненными заданиями здесь
            app.RegisterTask("Example Task", ExampleTask);

            // Запускаем приложение
            app.Run();
        }
    }
}
