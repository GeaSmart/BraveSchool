using Customer.Service.EventHandlers.Commands;
using Customer.Service.Queries;
using Customer.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Customer.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientQueryService clientQueryService;
        private readonly IMediator mediator;

        public ClientController(ILogger<ClientController> logger, IClientQueryService clientQueryService, IMediator mediator)
        {
            this.logger = logger;
            this.clientQueryService = clientQueryService;
            this.mediator = mediator;
        }

        //-- Route: /clients
        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> clientIds = null;

            if (!string.IsNullOrEmpty(ids))
                clientIds = ids.Split(',').Select(x => Convert.ToInt32(x));

            return await clientQueryService.GetAllAsync(page, take, clientIds);
        }

        //-- Route: /clients/1
        [HttpGet("{id:int}")]
        public async Task<ClientDto> Get(int id)
        {
            return await clientQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand command)
        {
            await mediator.Publish(command);
            return Ok();
        }
    }
}