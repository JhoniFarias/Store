using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IDeliveryFeeRepository _feeRepository;

        private readonly IDiscountRepository _discountRepository;


        public OrderHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IProductRepository productRepository, IDeliveryFeeRepository feeRepository, IDiscountRepository discountRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _feeRepository = feeRepository;
            _discountRepository = discountRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);
            }

            var customer = _customerRepository.Get(command.Customer);

            var deliveryFee = _feeRepository.Get(command.ZipCode);

            var discount = _discountRepository.Get(command.PromoCode);

            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();

            var order = new Order(customer, deliveryFee, discount);

            command.Items.ToList().ForEach(item => 
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            
            });

            AddNotifications(order.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar o pedido", command.Notifications);

            _orderRepository.Save(order);

            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}
