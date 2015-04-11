using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.POS.MessageHandlers.Commands
{
    public class PlaceOrderToStoreHandler : IMessageHandler
    {
        public void Handle(Message message)
        {
            var compute = message.Body as PlaceOrderToStore;
            if (compute != null)
            {
                var queueClient = new QueueClient(Queue.WebServiceProcessQueue);
                queueClient.Send(message);
            }
        }
    }
}