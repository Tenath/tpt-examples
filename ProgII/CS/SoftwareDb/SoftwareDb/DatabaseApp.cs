using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoftwareDb
{
    public class DatabaseApp : ConsoleMenuApp
    {
        // Репозитарий с рабочими данными
        ISoftwareRepository data;

        public DatabaseApp(ISoftwareRepository repo)
        {
            data = repo;
        }

        // Здесь будем производить регистрацию операций
        protected override void AppSetup()
        {
            ConsoleMenu sw = new ConsoleMenu("Software");
            menus.Add(sw);

            sw.RegisterMenuItem("List software", SwList);
            sw.RegisterMenuItem("Add software", Add);
            sw.RegisterMenuItem("Change software", Change);
            sw.RegisterMenuItem("Delete software", Delete);
            AddExitToMenu(sw);

            RegisterSubmenu(main_menu, sw, "Software management");
        }

        protected override void AppExit()
        {
            data.SaveChanges();
        }

        void SwList()
        {
            Console.WriteLine("Listing software...");

            // ======================================================================
            // | Developer | Name        | Version     | Install Date | User        |
            // ======================================================================
            // | Microsoft | Office      | 2019        | 01.08.2020   | Opilane     |
            // ======================================================================
            // "| " => 2, " | " => 3 x 4 = 12, " |" => 2. Total: 16
            // 64 символа - бюджет на вывод данных
            // developer - 15
            // name - 15
            // install date - 12 символов
            // version - 10 символов
            // user - 12

            // По желанию реализовать вёрстку под текущую ширину консольного окна
            // int width = Console.WindowWidth;
            // Ширину полей вычислять динамически (по произвольной схеме)
            StringBuilder sb = new StringBuilder(80);
            sb.Append('=', 80);

            string border = sb.ToString();

            string header = FormatTableEntry("Name", "Version", "Developer", "Install Date", "User");

            Console.WriteLine(border);
            // Вывести заголовок
            Console.WriteLine(header);
            Console.WriteLine(border);
            foreach (Software sw in data.GetList())
            {
                Console.WriteLine(FormatTableEntry(sw.Name, sw.Version, sw.Developer, sw.InstallDateString, sw.User));
                //Console.WriteLine($"{sw.Name}\t{sw.Version}\t{sw.Developer}\t" +
                    //$"{sw.InstallDate}\t{sw.User}");
                Console.WriteLine(border);
            }
        }

        string FormatTableEntry(string name, string version, string developer, string installdate, string user)
        {
            StringBuilder sb = new StringBuilder(80);

            sb.Append("| ");
            sb.Append($"{Utility.TruncateString(developer,15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(name,15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(version,10),-10}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(installdate,12),-12}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(user,12),-12}");
            sb.Append(" |");

            return sb.ToString();
        }

        void Add()
        {
            // Задание
            Console.Write("Enter the software name: ");
            string name = Console.ReadLine();
            if (name.Length == 0)
                throw new ApplicationException("Empty name");

            Console.Write("Enter the developer name: ");
            string dev = Console.ReadLine();
            if (name.Length == 0)
                throw new ApplicationException("Empty developer name");

            Console.Write("Enter the software version: ");
            string version = Console.ReadLine();

            Console.Write("Enter install date: ");
            DateTime install_date = Utility.ParseDate(Console.ReadLine());

            Console.Write("Enter user name: ");
            string user = Console.ReadLine();

            Software sw = new Software()
            {
                Name = name,
                Developer = dev,
                Version = version,
                InstallDate = install_date,
                User = user
            };

            data.Add(sw);

            Console.WriteLine($"Added software \"{sw.Name}\"");
        }

        void Change()
        {
            int index = -1;
            while (true)
            {
                Console.Write("Enter the index of item to change: ");
                if (!int.TryParse(Console.ReadLine(), out index))
                {
                    Console.WriteLine("Error");
                }
                else break;
                /*try
                {
                    index = int.Parse(Console.ReadLine());
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }*/
            }
            do { Console.WriteLine("Enter an index of item to delete"); }
            while (!int.TryParse(Console.ReadLine(), out index));
                
            Console.WriteLine("Changing software...");
        }

        void Delete()
        {
            Console.WriteLine("Deleting software...");
        }
    }
}
