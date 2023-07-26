using ArmadaMotors.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Data.DbContexts
{
    public class ArmadaDbContext : DbContext
    {
        public ArmadaDbContext(DbContextOptions<ArmadaDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<BannerAsset> BannerAssets { get; set; }
        public DbSet<ProductAsset> ProductAssets { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BackgroundImage> BackgroundImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}