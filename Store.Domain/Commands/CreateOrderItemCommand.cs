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
    public class CreateOrderItemCommand : Notifiable, Icommand
    {
        public CreateOrderItemCommand()
        {
            
        }

        public CreateOrderItemCommand(Guid product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Guid Product { get; set; }

        public int Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                                .Requires()
                                .HasLen(Product.ToString(), 32, "Product", "Produto inválido")
                                .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida")
                                );
        }
    }
}
