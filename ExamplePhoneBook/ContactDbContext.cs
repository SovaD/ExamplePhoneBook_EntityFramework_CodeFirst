using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePhoneBook
{
    public class ContactDbContext : DbContext
    {
        // Определяем набор данных для сущности Contact
        public DbSet<Contact> Contacts { get; set; }

        // Конструктор класса для указания строки подключения к базе данных SQLite
        public ContactDbContext() : base("name=ContactDbConnectionString")
        {
            // Создаем базу данных SQLite, если она еще не существует
           Database.SetInitializer(new CreateDatabaseIfNotExists<ContactDbContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasKey(c => c.id);
            base.OnModelCreating(modelBuilder);
        }

    }
}
