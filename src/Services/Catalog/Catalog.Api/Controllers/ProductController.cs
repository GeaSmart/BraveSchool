using Catalog.Service.Queries;
using Catalog.Service.Queries.DTOs;
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

        public ProductController(ILogger<ProductController> logger, IProductQueryService productQueryService)
        {
            _logger = logger;
            this.productQueryService = productQueryService;
        }

        //-- Route: /products
        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page = 1, int take = 10, string ids = "")
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
    }
}