using Customer.Service.Queries;
using Customer.Service.Queries.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientQueryService clientQueryService;

        public ClientController(ILogger<ClientController> logger, IClientQueryService clientQueryService)
        {
            this.logger = logger;
            this.clientQueryService = clientQueryService;
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
    }
}