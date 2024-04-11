using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
           IList<Product> products =
           [
               new("produto 01", 10, true),
               new("produto 02", 10, true),
               new("produto 03", 10, true),
               new("produto 04", 10, false),
               new("produto 05", 10, false),
           ];


            return products;
        }
    }
}
