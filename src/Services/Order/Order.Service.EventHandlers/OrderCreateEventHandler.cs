using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<OrderCreateEventHandler> logger;

        public OrderCreateEventHandler(ApplicationDbContext context, ILogger<OrderCreateEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
