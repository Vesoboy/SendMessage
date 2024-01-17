using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SendMessage.Models;
using System;

namespace SendMessage
{
    /// <summary>
    /// Контекст данных для взаимодействия с базой данных.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public DataContext()
        {

        }

        /// <summary>
        /// Конструктор с параметрами для использования в инъекции зависимостей.
        /// </summary>
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Набор данных для работы с объектами InformEmail в базе данных.
        /// </summary>
        public virtual DbSet<InformEmail> InformEmails { get; set; }

        /// <summary>
        /// Настраивает модель данных при создании контекста.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var rootGuid = Guid.NewGuid();
            modelBuilder.Entity<InformEmail>(b =>
            {
                b.HasData(new InformEmail
                {
                    Id = rootGuid,
                    Subject = "root",
                    Body = "root",
                    Recipients = new List<string> { "root", "root1" },
                    CreatedDate = DateTime.UtcNow,
                    Result = "root",
                    FailedMessage = "root",
                });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
