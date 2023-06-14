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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<ProductAsset> ProductAssets { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
    }
}