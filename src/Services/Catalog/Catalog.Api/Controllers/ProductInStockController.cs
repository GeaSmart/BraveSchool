using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("stocks")]
    
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController> logger;
        private readonly IMediator mediator;

        public ProductInStockController(ILogger<ProductInStockController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await mediator.Publish(command);
            return NoContent();
        }
    }
}