using Catalog.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public class ProductQueryService
    {
        private readonly ApplicationDbContext context;

        public ProductQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }        
    }
}
