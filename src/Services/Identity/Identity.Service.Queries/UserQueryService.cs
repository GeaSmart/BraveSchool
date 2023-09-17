using Identity.Persistence.Database;
using Identity.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Identity.Service.Queries
{
    public interface IUserQueryService
    {
        Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string>? users = null);
        Task<UserDto> GetAsync(string id);
    }

    public class UserQueryService : IUserQueryService
    {
        private readonly ApplicationDbContext context;

        public UserQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string>? users = null)
        {
            var collection = await context.Users
                .Where(x => users == null || users.Contains(x.Id))
                .OrderBy(x => x.FirstName)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var client = await context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return client.MapTo<UserDto>();
        }
    }
}
