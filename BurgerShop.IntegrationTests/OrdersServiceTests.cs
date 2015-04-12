using System;
using System.Linq;
using BurgerShop.Core;
using BurgerShop.Core.Models;
using BurgerShop.Data;
using BurgerShop.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BurgerShop.IntegrationTests
{
    [TestClass]
    public class OrdersServiceTests
    {
        [ClassInitialize]
        public static void TestRunInitialize(TestContext context)
        {
            AutoMapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void CreateCarryOutOrder()
        {
            var productsService = new ProductService();
            var products = productsService.GetAll().Take(1).ToList();
            var orderId = new Random().Next();

            var order = new OrderDTO
            {
                Id = orderId,
                StoreNo = 98001,
                OrderDeliveryMethod = OrderDeliveryMethod.CarryOut,
                Products = products
            };

            var orderService = new OrderService();
            var result = orderService.Create(order);
        }

        [TestMethod]
        public void CreateDeliveryOrder()
        {
            var productsService = new ProductService();
            var products = productsService.GetAll().Take(1).ToList();
            var orderId = new Random().Next();

            var order = new OrderDTO
            {
                Id = orderId,
                StoreNo = 98001,
                OrderDeliveryMethod = OrderDeliveryMethod.Delivery,
                Products = products
            };

            var orderService = new OrderService();
            var result = orderService.Create(order);
        }
    }
}
