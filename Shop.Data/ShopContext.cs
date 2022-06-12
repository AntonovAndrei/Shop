using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(s => s.Sales)
                .WithMany(p => p.Products);

            base.OnModelCreating(modelBuilder);
        }
    }
}
