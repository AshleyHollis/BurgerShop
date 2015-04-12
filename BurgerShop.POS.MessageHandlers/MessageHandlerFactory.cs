using System.Collections.Generic;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Extensions;
using BurgerShop.Messaging.Spec;
using BurgerShop.POS.MessageHandlers.Commands;

namespace BurgerShop.POS.MessageHandlers
{
    public static class MessageHandlerFactory
    {
        private static Dictionary<string, IMessageHandler> _handlers;

        static MessageHandlerFactory()
        {
            _handlers = new Dictionary<string, IMessageHandler>
            {
                {typeof (PlaceOrderToStore).GetMessageType(), new PlaceOrderToStoreHandler()},
                {typeof (OrderBeingMade).GetMessageType(), new DummyHandler()},
                {typeof (OrderBeingCooked).GetMessageType(), new DummyHandler()},
                {typeof (OrderBeingDelivered).GetMessageType(), new DummyHandler()},
                {typeof (OrderCompleted).GetMessageType(), new DummyHandler()},
                {typeof (OrderReadyInStore).GetMessageType(), new DummyHandler()},
            };
        }

        public static IMessageHandler GetMessageHandler(Message message)
        {
            return _handlers[message.Body.GetMessageType()];
        }
    }
}
