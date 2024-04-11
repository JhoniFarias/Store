using Store.Domain.Entities;
using Store.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {

        private readonly IEnumerable<Product> _products;
        public ProductQueriesTests() 
        {
            _products =
           [
               new("produto 01", 10, true),
               new("produto 02", 10, true),
               new("produto 03", 10, true),
               new("produto 04", 10, false),
               new("produto 05", 10, false),
           ];
        }

        [TestMethod]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
           var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());

            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());

            Assert.AreEqual(result.Count(), 2);
        }
    }
}
