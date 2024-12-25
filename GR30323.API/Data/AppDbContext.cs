    using Microsoft.EntityFrameworkCore;
    using GR30323.Domain.Entities; 

   namespace GR30323.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}