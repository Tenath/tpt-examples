using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    public class Software
    {
        public int ID { get; set; }
        private string name;
        private string developer;
        private string version;
        private DateTime install_date;
        private string user;

        public string Name { get => name; set => name = value; }
        public string Developer { get => developer; set => developer = value; }
        public string Version { get => version; set => version = value; }
        public DateTime InstallDate { get => install_date; set => install_date = value; }
        public string User { get => user; set => user = value; }

        public string InstallDateString => InstallDate.ToString("dd.MM.yyyy");

        public static List<Software> ReadCsv(string filename)
        {
            List<Software> result = new List<Software>();

            // Ловушка на случай, если не удастся открыть файл вообще
            try
            {
                // Считываем все строчки из файла, получаем массив строк
                string[] lines = File.ReadAllLines(filename);

                // Счётчик строчек, для отображения при исключении
                int linectr = 1;
                // Проходим по каждой строке файла
                foreach (string line in lines)
                {
                    // Ловушка для исключений, на случае если возникает
                    // ошибка при обработке одной строчки
                    // Это гарантирует, что при обнаружении одной битой строчки
                    // программа продолжит чтение последующих
                    try
                    {
                        // Распиливаем строку на составляющие по разделителю
                        string[] parts = line.Split(',');

                        // Делим поле с датой на 3 части
                        Utility.ValidateDate(parts[3]);
                        string[] dateparts = parts[3].Split('.'); // 21.09.2021

                        // Инициализируем объект компонентами из строчки
                        Software sw = new Software()
                        {
                            Name = parts[0],
                            Developer = parts[1],
                            Version = parts[2],
                            InstallDate = new DateTime(
                                int.Parse(dateparts[2]),
                                int.Parse(dateparts[1]),
                                int.Parse(dateparts[0])
                            ),
                            User = parts[4]
                        };

                        // Добавляем новый объект в список с данными
                        result.Add(sw);
                    }
                    catch (Exception e)
                    {
                        // Если возникла ошибка при обработке строчки из файла
                        // пишем об этом, и указываем № битой строки
                        Console.WriteLine($"Error parsing line #{linectr}: {e.Message}");
                    }
                    // Не забываем инкрементировать счётчик
                    ++linectr;
                }
            }
            catch(Exception e)
            {
                // Обработка ошибок при работе с файлом как таковым
                Console.WriteLine($"Error reading data file \"{filename}\": {e.Message}");
            }
            // Возвращаем сформированный список объектов
            return result;
        }

        public static void SaveCsv(string filename, List<Software> lst)
        {
            // Формируем список строк для файла
            List<string> lines = new List<string>();

            // Проходим по каждому объекту
            foreach(Software sw in lst)
            {
                // Формируем строку CSV-файла для данного объкта и добавляем в список строк
                lines.Add($"{sw.Name},{sw.Developer},{sw.Version},{sw.InstallDateString}," +
                    $"{sw.User}");
            }

            // Сохраняем в файл все строки из списка
            File.WriteAllLines(filename, lines);
        }
    }
}
