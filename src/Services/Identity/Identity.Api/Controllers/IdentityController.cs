using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("login")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMediator mediator;

        public IdentityController(ILogger<IdentityController> logger,
            SignInManager<ApplicationUser> signInManager,
            IMediator mediator)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(command);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok();
            }
            return BadRequest();
        }
    }
}
