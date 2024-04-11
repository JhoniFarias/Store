using Store.Domain.Entities;
using Store.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer;
        private readonly Discount _discount;
        private readonly Product _product;

        
        public OrderTests() 
        {
            //arrange
            _customer = new Customer("Jhoni", "jhonifarias.devepeloper.com");
            _discount = new Discount(10, DateTime.Now.AddDays(5));
            _product = new Product("Product 1", 10, true);
        }


        [TestMethod]
        public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {

            // act
            var order = new Order(_customer, 100, _discount);

            // assert
            Assert.AreEqual(8, order.Number.Length);
        }


        [TestMethod]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {

            var order = new Order(_customer, 100, _discount);

            Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
        }

        [TestMethod]
        public void Dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega()
        {


            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 10);
            order.Pay(100);


            Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
        }

        [TestMethod]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {


            var order = new Order(_customer, 10, _discount);
            order.Cancel();

            Assert.AreEqual(EOrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(null, 10);


            Assert.AreEqual(0, order.Items.Count);

        }

        [TestMethod]
        public void Dado_um_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 0);


            Assert.AreEqual(0, order.Items.Count);

        }

        [TestMethod]
        public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);

            Assert.AreEqual(order.Total(), 50);

        }


        [TestMethod]
        public void Dado_um_desconto_expirado_o_valor_deve_ser_60()
        {

            var order = new Order(_customer, 10, new Discount(10, DateTime.Now.AddDays(-1)));
            order.AddItem(_product, 5);


            Assert.AreEqual(order.Total(), 60);

        }

        [TestMethod]
        public void Dado_um_desconto_invalido_o_valor_deve_ser_60()
        {

            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);

            Assert.AreEqual(order.Total(), 60);

        }


        [TestMethod]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
        {

            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);

            Assert.AreEqual(order.Total(), 50);

        }

        [TestMethod]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
        {

            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);


            Assert.AreEqual(order.Total(), 60);

        }

        [TestMethod]
        public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {

            var order = new Order(null, 10, null);

            Assert.IsTrue(order.Invalid);

        }

    }
}
