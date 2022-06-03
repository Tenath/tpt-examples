using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    // Имплементация интерфейса "Репозитарий П/О" под схему с хранением данных в CSV-файле
    public class CsvFileSoftwareRepository : ISoftwareRepository
    {
        private List<Software> data = new List<Software>();
        private string filename;

        public CsvFileSoftwareRepository(string file_name)
        {
            filename = file_name;

            data = Software.ReadCsv(filename);
        }

        public void Add(Software sw)
        {
            data.Add(sw);
        }

        public IEnumerable<Software> GetList()
        {
            return data;
        }

        public void Remove(Software sw)
        {
            data.Remove(sw);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void SaveChanges()
        {
            // Как организовать бэкап рабочего файла?
            if (File.Exists("software.csv"))
            {
                if (File.Exists("software.csv.bak"))
                {
                    File.Delete("software.csv.bak");
                }

                // Вариант 1:
                // Копировать его с другим названием (software.csv.bak)
                // File.Copy("software.csv", "software.csv.bak");

                // Вариант 2:
                // Переименовать существующий файл (software.csv => software.csv.bak)
                File.Move("software.csv", "software.csv.bak");
            }
            // Сохраняем данные в файл
            Software.SaveCsv(filename, data);
        }
    }
}
