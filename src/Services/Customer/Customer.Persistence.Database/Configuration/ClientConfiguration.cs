using Customer.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistence.Database.Configuration
{
    public class ClientConfiguration
    {
        public ClientConfiguration(EntityTypeBuilder<Client> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.ClientId);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            SeedData(entityBuilder);
        }

        private void SeedData(EntityTypeBuilder entityTypeBuilder)
        {
            List<Client> clients = new List<Client>();
            
            for(int i = 1; i<= 100; i++)
            {
                clients.Add(new Client
                {
                    Name = $"Cliente {i}",
                });
            }

            entityTypeBuilder.HasData(clients);
        }
    }
}