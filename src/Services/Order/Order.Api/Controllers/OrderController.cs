﻿using Microsoft.AspNetCore.Mvc;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IOrderQueryService orderQueryService;

        public OrderController(ILogger<OrderController> logger, IOrderQueryService orderQueryService)
        {
            this.logger = logger;
            this.orderQueryService = orderQueryService;
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
    }
}