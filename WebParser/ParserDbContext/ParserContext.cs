using Microsoft.EntityFrameworkCore;
using ParserDbContext.Entities;

namespace ParserDbContext
{
    public class ParserContext : DbContext
    {
        public ParserContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductsNames> ProductsNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}