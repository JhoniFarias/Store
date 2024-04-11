using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using Store.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly FakeCustomerRepository _customerRepository;

        private readonly FakeOrderRepository _orderRepository;

        private readonly FakeProductRepository _productRepository;

        private readonly FakeDeliveryFeeRepository _feeRepository;

        private readonly FakeDiscountRepository _discountRepository;

        private readonly OrderHandler _orderHandler;


        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
            _feeRepository = new FakeDeliveryFeeRepository();
            _discountRepository =  new FakeDiscountRepository();


            _orderHandler = new OrderHandler(
                _customerRepository,
                _orderRepository,
                _productRepository,
                _feeRepository,
                _discountRepository);
        }

        [TestMethod]
       public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "07270410";
            command.PromoCode = "12345678";
            command.Items.Add(new(Guid.NewGuid(), 1));
            command.Items.Add(new(Guid.NewGuid(), 1));

            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, false);
        }

        [TestMethod]
        public void Dado_um_CEP_invalido_o_pedido_deve_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "00000000";
            command.PromoCode = "12345678";
            command.Items.Add(new(Guid.NewGuid(), 1));
            command.Items.Add(new(Guid.NewGuid(), 1));



            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, true);
        }

        [TestMethod]
        public void Dado_um_pedido_sem_items_nao_deve_ser_gerado()
        {

            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "07270410";
            command.PromoCode = "12345678";

            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, false);

        }

        [TestMethod]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "07270410";
            command.PromoCode = "";
            command.Items.Add(new(Guid.NewGuid(), 1));
            command.Items.Add(new(Guid.NewGuid(), 1));



            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, true);
        }

        [TestMethod]
        public void Dado_um_commando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();

            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commando_valido_o_pedido_deve_ser_gerado()
        {

            var command = new CreateOrderCommand();

            command.Customer = "123456789";
            command.ZipCode = "07270410";
            command.PromoCode = "12345678";
            command.Items.Add(new(Guid.NewGuid(), 1));
            command.Items.Add(new(Guid.NewGuid(), 1));

            _orderHandler.Handle(command);

            Assert.AreEqual(_orderHandler.Valid, true);

        }
    }
}
