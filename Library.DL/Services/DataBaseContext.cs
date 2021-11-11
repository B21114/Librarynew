using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services
{
    public class DataBaseContext : DbContext
    {
        /// <summary>
        /// Содержимое таблицы автор.
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Содержимое таблицы книги.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Содержимое таблицы издатель.
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
