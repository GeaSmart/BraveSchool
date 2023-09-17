using Identity.Service.Queries.DTOs;
using Identity.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService userQueryService;
        private readonly ILogger<UserController> logger;
        private readonly IMediator mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator,
            IUserQueryService userQueryService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.userQueryService = userQueryService;
        }

        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<string>? userIds = null;

            if (!string.IsNullOrEmpty(ids))
                userIds = ids.Split(',');

            return await userQueryService.GetAllAsync(page, take, userIds);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> Get(string id)
        {
            return await userQueryService.GetAsync(id);
        }
    }
}
