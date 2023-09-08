using Catalog.Service.EventHandlers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.Tests.Config
{
    public static class Logger
    {
        public static ILogger<ProductInStockUpdateStockEventHandler> Get()
        {
            return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>().Object;            
        }
    }
}
