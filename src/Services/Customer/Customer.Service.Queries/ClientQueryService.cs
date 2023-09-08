using Customer.Persistence.Database;
using Customer.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Customer.Service.Queries
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDto> GetAsync(int id);
    }
    public class ClientQueryService : IClientQueryService
    {
        private readonly ApplicationDbContext context;

        public ClientQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await context.Clients
                                .Where(x => clients == null || clients.Contains(x.ClientId))
                                .OrderBy(x => x.ClientId)
                                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            var client = await context.Clients.SingleOrDefaultAsync(x => x.ClientId == id);
            return client.MapTo<ClientDto>();
        }
    }
}