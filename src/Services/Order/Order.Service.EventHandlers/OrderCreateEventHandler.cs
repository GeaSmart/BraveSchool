using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;
using static Order.Common.Enums;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<OrderCreateEventHandler> logger;
        private readonly ICatalogProxy catalogProxy;

        public OrderCreateEventHandler(ApplicationDbContext context, ILogger<OrderCreateEventHandler> logger, ICatalogProxy catalogProxy)
        {
            this.context = context;
            this.logger = logger;
            this.catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("-- Order creation started");
            var entry = new Domain.Order();

            using(var transaction = await context.Database.BeginTransactionAsync())
            {
                //Preparing detail
                logger.LogInformation("-- Preparing detail to the order");
                PrepareDetail(entry, notification);

                //Preparing header
                logger.LogInformation("-- Preparing header to the order");
                PrepareHeader(entry, notification);

                //Creating order
                logger.LogInformation("-- Order is being created");
                await context.AddAsync(entry);
                await context.SaveChangesAsync();
                logger.LogInformation($"-- Order with id {entry.OrderId} was created.");

                //Update stocks
                logger.LogInformation("-- Stocks sync started");
                //Here this microservice communicates with Catalog
                await catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand { 
                    Items = notification.Items.Select(x=>new ProductInStockUpdateItem { 
                        Action = ProductInStockAction.Substract,
                        ProductId = x.ProductId,
                        Stock = x.Quantity
                    })
                });

                await transaction.CommitAsync();
                logger.LogInformation("-- Order creation finished.");
            }

        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new Domain.OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.UnitPrice * x.Quantity
            }).ToList();
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Status = OrderStatus.Pending;
            entry.PaymentType = notification.PaymentType;
            entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
}
