using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Catalog.Common.Enums;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateStockCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ProductInStockUpdateStockEventHandler> logger;

        public ProductInStockUpdateStockEventHandler(ApplicationDbContext context, ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task Handle(ProductInStockUpdateStockCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("-- ProductInStockUpdateStockCommand started.");

            var products = notification.Items.Select(x => x.ProductId).ToList();
            var stocks = await context.Stocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            logger.LogInformation("-- Products retrieved from the database.");

            foreach (var item in notification.Items)
            {
                var dbEntry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == ProductInStockAction.Substract)
                {
                    if (dbEntry == null)
                    {
                        logger.LogError($"-- Product not found. Product with ID {item.ProductId} doesn't exist.");
                        throw new Exception($"Product not found. Product with ID {item.ProductId} doesn't exist.");
                    }
                    //-- Check whether the current stock is enough to substract
                    if (dbEntry.Stock < item.Stock)
                    {
                        logger.LogError($"-- Not enough stock. Product with ID {item.ProductId} has stock:{dbEntry.Stock}, can't substract {item.Stock}");
                        throw new NotEnoughStockException($"Not enough stock. Product with ID {item.ProductId} has stock:{dbEntry.Stock}, can't substract {item.Stock}");
                    }
                    dbEntry.Stock -= item.Stock;
                    logger.LogInformation($"-- Stock decreased. Stock for product with ID {item.ProductId} was decreased in {item.Stock}. New stock is {dbEntry.Stock}.");
                }
                else
                {
                    //-- If the record doesnt exists, we create a new stock records and assign its stock with the value we want to add.
                    if (dbEntry == null)
                    {
                        dbEntry = new ProductInStock
                        {
                            ProductId = item.ProductId,
                            Stock = 0
                        };      
                        await context.AddAsync(dbEntry);
                        logger.LogInformation($"-- Stock created. New stock record was created for the product with ID {item.ProductId} since it didn't exist.");
                    }
                    dbEntry.Stock += item.Stock;
                    logger.LogInformation($"-- Stock increased. Stock for product with ID {item.ProductId} was increased in {item.Stock}. New stock is {dbEntry.Stock}.");
                }
            }
            await context.SaveChangesAsync();

            logger.LogInformation("-- ProductInStockUpdateStockCommand finished.");
        }
    }
}