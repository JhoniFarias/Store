using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable, Icommand
    {
        public CreateOrderCommand()
        {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                                  .Requires()
                                  .IsNotNullOrEmpty(Customer, "Customer", "Customer inválido")
                                  .HasLen(ZipCode, 8,"ZipCode", "CEP inválido")
                                  .IsGreaterThan(Items.Count, 0, "Items", "Ordem sem item de pedidos")
                                );
        }
    }
}
