using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using Catalog.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductQueryService productQueryService;
        private readonly IMediator mediator;

        public ProductController(ILogger<ProductController> logger, IProductQueryService productQueryService
                                , IMediator mediator)
        {
            _logger = logger;
            this.productQueryService = productQueryService;
            this.mediator = mediator;
        }

        //-- Route: /products
        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> productIds = null;

            if (!string.IsNullOrEmpty(ids))
                productIds = ids.Split(',').Select(x => Convert.ToInt32(x));

            return await productQueryService.GetAllAsync(page, take, productIds);
        }

        //-- Route: /products/1
        [HttpGet("{id:int}")]
        public async Task<ProductDto> Get(int id)
        {
            return await productQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await mediator.Publish(command);
            return Ok();
        }
    }
}