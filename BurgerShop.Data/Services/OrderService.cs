using System;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;
using BurgerShop.Messages;
using BurgerShop.Messaging;

namespace BurgerShop.Data
{
    public class OrderService : EntityService<Order, OrderDTO>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderService() :base(new UnitOfWork(new BurgerShopContext()), new OrderRepository(new BurgerShopContext()))
        {
            _unitOfWork = new UnitOfWork(new BurgerShopContext());
            _orderRepository = new OrderRepository(new BurgerShopContext());
        }

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository OrderRepository) : base(unitOfWork, OrderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = OrderRepository;
        }

        public OrderDTO GetById(int Id)
        {
            return _orderRepository.GetById(Id);
        }

        public override OrderDTO Create(OrderDTO order)
        {
            if (order == null) throw new ArgumentNullException("entity");
            
            var createdDto = _orderRepository.Add(order);
            _unitOfWork.Commit();

            var topicClient = new TopicClient(Topic.OrdersCreated);

            topicClient.Publish(new Message
            {
                Body = order
            });

            return createdDto;
        }
    }
}