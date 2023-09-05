using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEventHandler
    {
        private readonly ApplicationDbContext context;

        public ProductCreateEventHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Handle(ProductCreateCommand command)
        {
            await context.AddAsync(
                new Product {
                    Name = command.Name,
                    Description = command.Description,
                    Price = command.Price
            });
            
            await context.SaveChangesAsync();
        }
    }
}