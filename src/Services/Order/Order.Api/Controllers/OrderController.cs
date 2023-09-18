using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IOrderQueryService orderQueryService;
        private readonly IMediator mediator;

        public OrderController(ILogger<OrderController> logger, IOrderQueryService orderQueryService, IMediator mediator)
        {
            this.logger = logger;
            this.orderQueryService = orderQueryService;
            this.mediator = mediator;
        }

        //-- Route: /orders
        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10)
        {            
            return await orderQueryService.GetAllAsync(page, take);
        }

        //-- Route: /orders/1
        [HttpGet("{id:int}")]
        public async Task<OrderDto> Get(int id)
        {
            return await orderQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand notification)
        {
            await mediator.Publish(notification);
            return Ok();
        }
    }
}
