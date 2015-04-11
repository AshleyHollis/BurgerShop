using System;
using AutoMapper;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;
using BurgerShop.Data.Repositories;
using BurgerShop.Data.UnitOfWork;
using BurgerShop.Messages;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;

namespace BurgerShop.Data.Services
{
    public class OrderService : EntityService<Order, OrderDTO>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderService() :base(new UnitOfWork.UnitOfWork(new BurgerShopContext()), new OrderRepository(new BurgerShopContext()))
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new BurgerShopContext());
            _orderRepository = new OrderRepository(new BurgerShopContext());
        }

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository) : base(unitOfWork, orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
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

            var queueClient = new QueueClient(Queue.CreateOrder);
            var createOrder = Mapper.Map<OrderDTO, CreateOrder>(order);
            queueClient.Send(new Message {Body = createOrder });

            return createdDto;
        }
    }
}