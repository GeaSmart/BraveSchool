using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database.Configuration;

namespace Order.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Order.Domain.Order> Orders { get; set; }
        public DbSet<Order.Domain.OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Order");
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new OrderConfiguration(modelBuilder.Entity<Order.Domain.Order>());
            new OrderDetailConfiguration(modelBuilder.Entity<Order.Domain.OrderDetail>());
        }
    }
}