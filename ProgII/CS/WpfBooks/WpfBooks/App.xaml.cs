using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfBooks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public BooksDbContext Db { get; private set; } = null;
        public const int AppVersion = 10;

        MainWindow winMain;
        // Обработчик события "запуск приложения"
        private void Application_Startup(object sender, 
            StartupEventArgs e)
        {
            try
            {
                BooksGlobal.Initialize(this);
                Db = new BooksDbContext();

                winMain = new MainWindow();
                winMain.Show();
            }
            catch(Exception exc)
            {
                MessageBox.Show($"Exception: {exc.Message}");
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // При любом варианте поведения программы закрываем контекст
            // == высвобождаем системные ресурсы
            if (Db != null) Db.Dispose();
        }
    }
}
