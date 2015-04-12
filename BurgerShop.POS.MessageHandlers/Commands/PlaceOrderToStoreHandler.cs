using System;
using System.Collections.Generic;
using System.Timers;
using AutoMapper;
using BurgerShop.Core;
using BurgerShop.Core.Models;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.POS.MessageHandlers.Commands
{
    public class PlaceOrderToStoreHandler : IMessageHandler
    {
        private readonly IQueueClient _queueClient = new QueueClient(Queue.WebServiceProcessQueue);
        private int _orderId;
        private int _storeNo;
        private OrderDeliveryMethod _orderDeliveryMethod;
        private Timer _orderBeingMadeTimer;
        private Timer _orderBeingCookedTimer;
        private Timer _orderReadyInStoreTimer;
        private Timer _orderBeingDeliveredTimer;

        public void Handle(Message message)
        {
            _orderBeingMadeTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _orderBeingMadeTimer.Elapsed += _orderBeingMadeTimer_Elapsed;

            _orderBeingCookedTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _orderBeingCookedTimer.Elapsed += OrderBeingCookedTimer_Elapsed;

            _orderReadyInStoreTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _orderReadyInStoreTimer.Elapsed += _orderReadyInStoreTimer_Elapsed;

            _orderBeingDeliveredTimer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _orderBeingDeliveredTimer.Elapsed += _orderBeingDeliveredTimer_Elapsed;

            var compute = message.Body as PlaceOrderToStore;
            if (compute != null)
            {
                _orderId = compute.Id;
                _storeNo = compute.StoreNo;
                _orderDeliveryMethod = compute.OrderDeliveryMethod;

                var placedOrderToStore = Mapper.Map<PlaceOrderToStore, OrderBeingMade>(compute);
                _queueClient.Send(new Message {Body = placedOrderToStore});
            }
        }

        private void _orderBeingMadeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var orderBeingCooked = new OrderBeingCooked { Id = _orderId, StoreNo = _storeNo };
            _queueClient.Send(new Message { Body = orderBeingCooked });
        }

        private void OrderBeingCookedTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var orderReadyInStore = new OrderReadyInStore { Id = _orderId, StoreNo = _storeNo };
            _queueClient.Send(new Message { Body = orderReadyInStore });
        }
        private void _orderReadyInStoreTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_orderDeliveryMethod == OrderDeliveryMethod.CarryOut)
            {
                var orderComplete = new OrderCompleted { Id = _orderId, StoreNo = _storeNo };
                _queueClient.Send(new Message { Body = orderComplete });
            }
            else
            {
                var orderBeingDelivered = new OrderBeingDelivered { Id = _orderId, StoreNo = _storeNo };
                _queueClient.Send(new Message { Body = orderBeingDelivered });
            }
        }

        private void _orderBeingDeliveredTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var orderComplete = new OrderCompleted { Id = _orderId, StoreNo = _storeNo };
            _queueClient.Send(new Message { Body = orderComplete });
        }
    }
}