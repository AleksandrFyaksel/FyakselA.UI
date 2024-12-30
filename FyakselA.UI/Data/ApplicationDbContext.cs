using FyakselA.UI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GR30323.Domain.Entities;

namespace FyakselA.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } 
    }
}