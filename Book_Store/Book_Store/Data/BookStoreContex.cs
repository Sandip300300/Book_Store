using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Data
{
    public class BookStoreContex:DbContext
    {
        public BookStoreContex(DbContextOptions<BookStoreContex> options):base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=BookStore;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
