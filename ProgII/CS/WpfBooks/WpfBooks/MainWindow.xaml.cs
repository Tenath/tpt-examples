using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> books { get; set; } = null;
        public Book selectedBook { get; set; }
        private string filename;

        public MainWindow()
        {
            BooksGlobal.App.Db.Books.Load();
            books = BooksGlobal.App.Db.Books.Local.ToObservableCollection();
            InitializeComponent();
            Title = $"{Title} v{App.AppVersion}";

            dgBooks.ItemsSource = books;
            //BooksGlobal.App.Db.Books.Find(0);
            //ReopenFile(file);
            SetSelected();
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что в таблице действительно выбрано больше 0 строк
            // т.е. хотя бы одна
            if(e.AddedItems.Count > 0)
            {
                SetSelected();
                // Делаем копию с выбранного в таблице объекта, 
                // помещаем в соответствующую переменную
                //selectedBook = (e.AddedItems[0] as Book).Copy();
                // Изменяем контекст данных групбокса на созданную копию
                //grbBookEdit.DataContext = selectedBook;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelected();
        }

        private void SetSelected()
        {
            // Метод №1: if-else
            /*
            if (dgBooks.SelectedItem == null)
            {
                selectedBook = new Book();
            }
            else
            {
                selectedBook = (dgBooks.SelectedItem as Book).Copy();
            }*/

            // Метод №2: тройной оператор
            /* selectedBook = (dgBooks.SelectedItem == null) ? new Book() : 
                (dgBooks.SelectedItem as Book).Copy();*/

            // Метод №3: null-coalescing operator
            selectedBook = (dgBooks.SelectedItem as Book)?.Copy() ?? new Book();
            grbBookEdit.DataContext = selectedBook;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            // Если запись в таблице выбрана
            if (dgBooks.SelectedItem != null)
            {
                // Присваиваем полям оригинального (выбранного) объекта значения
                // из копии
                (dgBooks.SelectedItem as Book).Assign(selectedBook);
                BooksGlobal.App.Db.SaveChanges();
                // Перерисовываем таблицу
                dgBooks.Items.Refresh();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (books == null) return;
            books.Add(selectedBook);
            BooksGlobal.App.Db.SaveChanges();
            dgBooks.Items.Refresh();
            // Меняем выбор записи в таблице на свежесозданный объект
            dgBooks.SelectedItem = selectedBook;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (books == null) return;
            MessageBoxResult result = MessageBox.Show(
                "Подтвердить удаление объекта?", // Текст в окошке
                "Удаление записи", // Заголовок окошка
                MessageBoxButton.YesNo, // Доступные кнопки
                MessageBoxImage.Warning, // Значок
                MessageBoxResult.No // Результат по умолчанию
                                    // (если закрыли окно на крестик)
            );

            if(result == MessageBoxResult.Yes && 
                dgBooks.SelectedItem !=null)
            {
                books.Remove(dgBooks.SelectedItem as Book);
                BooksGlobal.App.Db.SaveChanges();
                dgBooks.Items.Refresh();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if(books != null) Book.SaveCSV(books, "books.csv");
            /*if (books == null) return;

            MessageBoxResult result = MessageBox.Show(
                "Сохранить изменения в файл?", // Текст в окошке
                "Сохранение", // Заголовок окошка
                MessageBoxButton.YesNoCancel, // Доступные кнопки
                MessageBoxImage.Warning, // Значок
                MessageBoxResult.No // Результат по умолчанию
                                    // (если закрыли окно на крестик)
            );

            if (result == MessageBoxResult.Yes)
            {
                Book.SaveCSV(books, "books.csv");
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }*/
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            if (books != null) Book.SaveCSV(books, "books.csv");
        }

        private void MiSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            if (books != null) Book.SaveCSV(books, dlg.FileName);
        }

        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            //ReopenFile(dlg.FileName);
        }

        /*private void ReopenFile(string file)
        {
            filename = file;

            if(file == null)
            {
                books = null;
                dgBooks.ItemsSource = null;
                SetSelected();
            }
            else
            {
                books = Book.ReadBooks(filename);
                selectedBook = new Book()
                {
                    Title = "Example book"
                };

                dgBooks.ItemsSource = books;
                grbBookEdit.DataContext = selectedBook;
            }
        }*/

        private void MiNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;
            File.WriteAllText(dlg.FileName, "");
            //ReopenFile(dlg.FileName);
        }

        private void MiClose_Click(object sender, RoutedEventArgs e)
        {
            //ReopenFile(null);
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MiExportXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;
            Book.ExportXML(books, dlg.FileName);
        }

        private void MiExportJSON_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;
            Book.ExportJSON(books, dlg.FileName);
        }

        private void MiExportBinary_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;
            Book.ExportBinary(books, dlg.FileName);
        }

        private void MiImportBinary_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() != true) return;
            //books.AddRange(Book.ReadBooksBinary(dlg.FileName));
            dgBooks.Items.Refresh();
        }

        // Самостоятельные задания - 15.09.20
        // Задание 1: Реализовать импорт из XML-файла (по той же схеме, что и с бинарным)
        // Задание 2: Реализовать импорт из JSON-файла (ручками)
    }
}
