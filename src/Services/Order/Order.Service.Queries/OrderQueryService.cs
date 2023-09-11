using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Service.Queries
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
        Task<OrderDto> GetAsync(int id);
    }
    public class OrderQueryService : IOrderQueryService
    {
        private readonly ApplicationDbContext context;

        public OrderQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var collection = await context.Orders
                .Include(x => x.Items)
                .OrderBy(x => x.OrderId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var order = await context.Orders
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.OrderId == id);

            return order.MapTo<OrderDto>();
        }
    }
}