using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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