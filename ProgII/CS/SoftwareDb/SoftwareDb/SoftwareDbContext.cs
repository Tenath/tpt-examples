using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    public class SoftwareDbContext : DbContext
    {
        public DbSet<Software> Soft { get; private set; }

        public SoftwareDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Host = "localhost";
            string User = "root";
            string Pass = "";
            ushort Port = 3306;
            string Database = "softwaredb";

            string connString = $"server={Host};user={User};password={Pass};" +
                $"port={Port};database={Database}";
            optionsBuilder.UseMySql(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Software> sw_entity = modelBuilder.Entity<Software>();
            sw_entity.Property("Name").HasMaxLength(40).IsRequired();
            sw_entity.Property("Developer").HasMaxLength(40);
            sw_entity.Property("Version").HasMaxLength(15);
            sw_entity.Property("User").HasMaxLength(30).IsRequired();

            sw_entity.HasIndex("Name");
        }

        public void TestData()
        {
            Software sw = new Software()
            {
                Name = "Windows",
                Developer = "Microsoft",
                Version = "98",
                InstallDate = new DateTime(1999, 12, 31),
                User = "Opilane"
            };

            Soft.Add(sw);
            SaveChanges();

            Console.WriteLine("Added object to db");
        }
    }
}
