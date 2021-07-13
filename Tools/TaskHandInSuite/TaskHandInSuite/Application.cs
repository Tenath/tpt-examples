using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHandInSuite
{
    // Делегат функции для заданий - void Fn()
    public delegate void LessonTask();

    // Запись в таблице заданий
    public class LessonTaskEntry
    {
        public string Name; // Название задания
        public LessonTask Function; // Функция с выполненным заданием
        public LessonTaskEntry(string name, LessonTask function) { Name = name; Function = function; }
    }

    public class Application
    {
        // Список зарегистированных заданий
        private List<LessonTaskEntry> Tasks = new List<LessonTaskEntry>();
        // Текущее выбранное задание
        private int CurrentTask = -1;

        // Регистрация задания, принимает название и делегат
        public void RegisterTask(string name, LessonTask function)
        {
            Tasks.Add(new LessonTaskEntry(name, function));
        }

        public void MainMenu()
        {
            Console.WriteLine("Available tasks:");
            Console.WriteLine("================");
            // Выводим индекс каждого задания, и его название
            for (int i = 0; i < Tasks.Count; i++) Console.WriteLine($"{i}. {Tasks[i].Name}");
            // Операцию "выход" обозначаем индексом на 1 выше, чем в последний в списке
            Console.WriteLine($"{Tasks.Count}. Exit");
        }

        public void Run()
        {
            // Пока не выбрано задание "выход"
            while (CurrentTask != Tasks.Count)
            {
                // В этом блоке отлавливаем ошибки, чтобы программа не вылетала
                try
                {
                    MainMenu();

                    Console.Write("\nSelect a task to activate: ");
                    CurrentTask = int.Parse(Console.ReadLine()); // Кинет исключение, если введём что-либо, кроме целого числа

                    // Если выбранный индекс - число, но оно выходим за допустимые рамки - кидаем исключение
                    if (CurrentTask < 0 || CurrentTask > Tasks.Count) throw new ApplicationException("Task index out of bounds");

                    if (CurrentTask != Tasks.Count) 
                    {
                        Console.WriteLine($"\n{Tasks[CurrentTask].Name}");
                        // Повторяем символ = столько раз, сколь содержится символов в названии задания
                        Console.WriteLine(new String('=', Tasks[CurrentTask].Name.Length));

                        // Вызываем делегат соответствующего задания
                        Tasks[CurrentTask].Function();

                        Console.WriteLine("==========================");
                        Console.WriteLine("Press any key to continue.\n");
                        Console.ReadKey();
                    }
                    // если выбрано задание "выход", пишем "Exiting", на следующей итерации цикл закончит исполнение
                    else Console.WriteLine("Exiting...");
                }
                catch (Exception e) // Если возникла ошибка, обрабатываем её здесь
                {
                    Console.WriteLine($"Error: {e.Message}\n");
                    CurrentTask = -1;
                }
            }
        }
    }
}
