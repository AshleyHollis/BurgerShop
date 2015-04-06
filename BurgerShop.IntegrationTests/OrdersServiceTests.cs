using System;
using System.Linq;
using App.BurgerShop.Web.App_Start;
using BurgerShop.Core.Models;
using BurgerShop.Data;
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
        public void Create()
        {
            var productsService = new ProductService();
            var products = productsService.GetAll().Take(1).ToList();
            var orderId = new Random().Next();

            var order = new OrderDTO
            {
                Id = orderId,
                StoreNo = 98001,
                Products = products
            };

            var orderService = new OrderService();
            var result = orderService.Create(order);
        }
    }
}
