using Client.WebClient.Services;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public DataCollection<OrderDto> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            Orders = await orderService.GetAllAsync(CurrentPage, 10);
            return View(Orders);
        }
    }
}
