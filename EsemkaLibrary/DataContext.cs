using EsemkaLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EsemkaLibrary
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<BookInformation> BookInformations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }

    }
}
