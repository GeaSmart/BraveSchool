using Client.WebClient.Models;
using Client.WebClient.Services;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.WebClient.Controllers
{    
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICatalogService catalogService;
        private readonly ICustomerService customerService;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public OrdersController(IOrderService orderService, 
            ICatalogService catalogService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.catalogService = catalogService;
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetAllAsync(CurrentPage, 999);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromRoute]int id)
        {
            var order = await orderService.GetAsync(id);
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await catalogService.GetAllAsync(1, 999);
            var clients = await customerService.GetAllAsync(1, 999);
            return View(new CreateOrderModel { Clients = clients, Products = products });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto orderDto)
        {
            await orderService.CreateAsync(orderDto);
            return this.StatusCode(200);
        }

    }
}
