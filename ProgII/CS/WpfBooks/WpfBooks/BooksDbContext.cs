using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBooks
{
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
            string Database = "wpfbooks_ef";

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
}
