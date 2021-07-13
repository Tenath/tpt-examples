using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBooks.Models
{
    // Интерфейс для доступа к объектам из базы
    public interface IBooksStorage
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void UpdateBook(int id, Book book);
    }

    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BooksDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string Host = "localhost";
            string User = "root";
            string Password = "";
            string Database = "mvcbooks";

            string ConnString = $"server={Host};user={User};database={Database};" +
                $"port=3306;password={Password}";

            options.UseMySql(ConnString);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            var book = model.Entity<Book>();
            book.Property(x => x.Title).HasMaxLength(80).IsRequired();
            book.Property(x => x.ISBN).HasMaxLength(20).IsRequired();
            book.Property(x => x.Author).HasMaxLength(60).IsRequired();
            book.Property(x => x.Publisher).HasMaxLength(40).IsRequired();
            book.Property(x => x.Edition).HasDefaultValue(1);
        }
    }

    public class BooksDatabase : IDisposable, IBooksStorage
    {
        public BooksDbContext Db { get; private set; }

        public BooksDatabase()
        {
            Db = new BooksDbContext();
            Db.Books.Load();

            // Если таблица книг пустая, добавляем туда тестовые данные
            if (Db.Books.Count() == 0) SeedData();
        }

        // Наполнение базы первичным тестовым набором данных
        public void SeedData()
        {
            // Пересоздаём базу (на случай если схема изменилась)
            Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();

            // Читаем данные из файла
            List<Book> books = Book.ReadBooks("books.csv");

            // Добавляем данные в базу и сохраняем
            Db.Books.AddRange(books);
            Db.SaveChanges();
        }

        private bool disposedValue;

        public Book GetBookById(int id)
        {
            return Db.Books.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return Db.Books.Local;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (Db != null) Db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BooksDatabase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void UpdateBook(int id, Book book)
        {
            // Найти объект в базе по указанному ID
            Book db_book = Db.Books.FirstOrDefault(x => x.ID == id);
            // Если не найден - кидаем эксепшен
            if (db_book == null) throw new ApplicationException("Object not found");
            // Присваиваем объекту из базы значения от объекта, полученного из формы (кроме ID)
            db_book.Assign(book);
            // Помечаем объект из базы на обновление
            Db.Update(db_book);
            // Сохраняем изменения в базе
            Db.SaveChanges();
        }
    }
}
