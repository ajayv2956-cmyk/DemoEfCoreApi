using DemoEfCoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoEfCoreApi.Data
{
    public class DemoEfDbContext : DbContext
    {
        public DemoEfDbContext(DbContextOptions<DemoEfDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
