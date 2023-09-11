using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Persistence.Database.Configuration
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Order.Domain.Order> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.OrderId);
            entityBuilder.Property(x => x.ClientId).IsRequired();                        
        }
    }
}