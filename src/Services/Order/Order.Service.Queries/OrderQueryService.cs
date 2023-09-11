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
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int> orders = null);
        Task<OrderDto> GetAsync(int id);
    }
    public class OrderQueryService : IOrderQueryService
    {
        private readonly ApplicationDbContext context;

        public OrderQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int> orders = null)
        {
            var collection = await context.Orders
                .Where(x => orders == null || orders.Contains(x.OrderId))
                .OrderBy(x => x.OrderId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var order = await context.Orders.SingleOrDefaultAsync(x => x.OrderId == id);
            return order.MapTo<OrderDto>();
        }
    }
}