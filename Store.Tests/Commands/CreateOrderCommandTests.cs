using Store.Domain.Commands;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod]
        public void Dado_um_commando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand()
            {
                Customer = "",
                ZipCode = "13411080",
                PromoCode = "12345678",
                Items = new List<CreateOrderItemCommand>()
                {
                    new (Guid.NewGuid(),1),
                    new (Guid.NewGuid(),1)
                }
            };

            command.Validate();

            Assert.AreEqual(command.Valid, false);
    
        }
    }
}
