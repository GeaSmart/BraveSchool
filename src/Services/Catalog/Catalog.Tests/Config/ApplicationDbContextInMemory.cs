using Catalog.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Catalog.Db")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
