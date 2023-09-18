using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        public DefaultController()
        {

        }

        [HttpGet]
        public string Get()
        {
            var project = Assembly.GetEntryAssembly().GetName().Name;
            return $"{project} running...";
        }
    }
}
