using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("identity")]
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

        [HttpPost("signup")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Authentication(UserLoginCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(command);
                if (string.IsNullOrEmpty(result.AccessToken))                
                    return BadRequest("Access denied, check your credentials.");                

                return Ok(result);
            }
            return BadRequest();
        }
    }
}
