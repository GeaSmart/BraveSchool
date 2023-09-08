using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Service.EventHandlers
{
    public class ClientCreateEventHandler : INotificationHandler<ClientCreateCommand>
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ClientCreateEventHandler> logger;

        public ClientCreateEventHandler(ApplicationDbContext context, ILogger<ClientCreateEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task Handle(ClientCreateCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("-- ClientCreateCommand started.");

            try
            {
                await context.AddAsync(new Client
                {
                    Name = command.Name
                });
                await context.SaveChangesAsync();
                logger.LogInformation("-- New client was saved into the database.");
            }
            catch(Exception ex)
            {
                logger.LogError("-- An error ocurred while saving a new client.");
            }
            logger.LogInformation("-- ClientCreateCommand finished.");
        }
    }
}