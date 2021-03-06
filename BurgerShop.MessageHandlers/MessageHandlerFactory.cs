﻿using System.Collections.Generic;
using BurgerShop.MessageHandlers.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Extensions;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.MessageHandlers
{
    public static class MessageHandlerFactory
    {
        private static Dictionary<string, IMessageHandler> _handlers;

        static MessageHandlerFactory()
        {
            _handlers = new Dictionary<string, IMessageHandler>
            {
                {typeof (CreateOrder).GetMessageType(), new CreateOrderHandler()}
            };
        }

        public static IMessageHandler GetMessageHandler(Message message)
        {
            return _handlers[message.Body.GetMessageType()];
        }
    }
}
