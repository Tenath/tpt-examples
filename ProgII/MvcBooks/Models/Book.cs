using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MvcBooks.Models
{
    // Домашнее задание:
    // Добавить в модель данных сущности "Издатель", "Автор", "Категория"
    // В окне сделать вкладки для просмотра и редактирования этих сущностей
    // (пока можно сделать без связей между сущностями)

    [Serializable]
    public class Book
    {
        public int ID { get; set; }
        [Required] [StringLength(80)]
        public string Title { get; set; }
        [Required] [StringLength(40)]
        public string Publisher { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[0-9]{3}-[0-9]{9,15}$", ErrorMessage = "Incorrect ISBN format")]
        public string ISBN { get; set; } // 978-123456789
        [Required]
        [StringLength(60)]
        public string Author { get; set; }
        [Range(1,999)]
        public int Edition { get; set; }
        [Range(1900,9999)]
        public int Year { get; set; }

        // Домашнее задание:
        // Реализовать чтение из файла в список
        // И запись из списка в файл

        // Функция для чтения файла и формирования списка объектов на базе него
        public static List<Book> ReadBooks(string filename)
        {
            // Создаём переменную для возвращаемого результата
            // (возвращаем её значение в конце функции)
            List<Book> result = new List<Book>();

            // Считываем строчки из файла, получаем массив строк
            string[] lines = File.ReadAllLines(filename);

            // Проходим через каждую строчку
            int linectr = 1;
            foreach(string line in lines)
            {
                // Код, где могут возникнуть ошибки, помещаем в блок try {}
                try
                {
                    // Распиливаем строчку на куски по сепараторам (символ ;)
                    string[] fields = line.Split(';');

                    // Формируем объект на базе полей
                    // Числовые поля не забываем конвертировать из строк в int
                    Book b = new Book()
                    {
                        ID = int.Parse(fields[0]),
                        Title = fields[1],
                        Publisher = fields[2],
                        ISBN = fields[3],
                        Author = fields[4],
                        Edition = int.Parse(fields[5]),
                        Year = int.Parse(fields[6])
                    };

                    // Добавляем сформированный объект книги в список
                    result.Add(b);
                }
                // После try располагаем 1 или несколько блоков catch,
                // где мы ловим разные типы ошибок
                catch (Exception e)
                {
                    
                }
                linectr++;
            }

            // Возвращаем сформированный список
            return result;
        }

        public static List<Book> ReadBooksBinary(string filename)
        {
            List<Book> bks = null;

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                bks = formatter.Deserialize(fs) as List<Book>;
            }

            return bks != null ? bks : new List<Book>();
        }

        // Функция для записи списка объектов в формате CSV в указанный файл
        public static void SaveCSV(IEnumerable<Book> books, string filename)
        {
            // Переменная-список строк файла, изначально пустой
            List<string> lines = new List<string>();

            // Проходим по каждой книге во входном списке
            foreach(Book b in books)
            {
                // Формируем строку с разделителями между полями ;
                string line = $"{b.Title};{b.Publisher};{b.ISBN};" +
                    $"{b.Author};{b.Edition};{b.Year}";

                // Добавляем строку в список строк
                lines.Add(line);
            }
            // Записываем все строки из сформированного списка в указанный файл
            File.WriteAllLines(filename, lines);
        }

        public static void ExportXML(IEnumerable<Book> books, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Book>));

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, books);
            }
        }

        public static void ExportBinary(IEnumerable<Book> books, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, books);
            }
        }

        public static void ExportJSON(IEnumerable<Book> books, string filename)
        {
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("[");

                int index = books.Count() - 1;
                foreach (Book b in books)
                {
                    sw.WriteLine("\t{");
                    sw.WriteLine($"\t\tTitle:\"{b.Title}\",");
                    sw.WriteLine($"\t\tPublisher:\"{b.Publisher}\",");
                    sw.WriteLine($"\t\tISBN:\"{b.ISBN}\",");
                    sw.WriteLine($"\t\tAuthor:\"{b.Author}\",");
                    sw.WriteLine($"\t\tEdition:\"{b.Edition}\",");
                    sw.WriteLine($"\t\tYear:\"{b.Year}\"");
                    if(index>0) sw.WriteLine("\t},");
                    else sw.WriteLine("\t}");
                    index--;
                }

                sw.WriteLine("]");
            }
        }

        // Метод (НЕ статический!) для создания копии текущего объекта
        public Book Copy()
        {
            return new Book()
            {
                Title = this.Title,
                Publisher = this.Publisher,
                ISBN = this.ISBN,
                Author = this.Author,
                Edition = this.Edition,
                Year = this.Year
            };
        }

        // Присвоение текущему объекту данных из другого объекта "книга"
        public void Assign(Book other)
        {
            this.Title = other.Title;
            this.Publisher = other.Publisher;
            this.ISBN = other.ISBN;
            this.Author = other.Author;
            this.Edition = other.Edition;
            this.Year = other.Year;
        }
    }
}
