using System;
using System.Net;
using AutoMapper;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.MessageHandlers.Commands
{
    public class CreateOrderHandler : IMessageHandler
    {
        public void Handle(Message message)
        {
            var compute = message.Body as CreateOrder;
            if (compute != null)
            {
                var queueClient = new QueueClient(compute.StoreNo.ToString());
                var placeOrder = Mapper.Map<CreateOrder, PlaceOrderToStore>(compute);
                queueClient.Send(new Message { Body = placeOrder});
            }
        }
    }
}