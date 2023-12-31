using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var project = Assembly.GetEntryAssembly().GetName().Name;
            return $"{project} running...";
        }
    }
}