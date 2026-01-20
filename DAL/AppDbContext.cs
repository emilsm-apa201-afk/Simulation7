using Microsoft.EntityFrameworkCore;
using Simulation7.Models;

namespace Simulation7.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
