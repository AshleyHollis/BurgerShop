using BurgerShop.Messaging;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace BurgerShop.Azure
{
    public class AzureMessage
    {
        public static Message Unwrap(BrokeredMessage brokeredMessage)
        {
            var raw = brokeredMessage.GetBody<string>();
            var message = JsonConvert.DeserializeObject<Message>(raw);
            message.TransportedBy = "Azure";
            return message;
        }

        public static BrokeredMessage Wrap(Message message)
        {
            var raw = JsonConvert.SerializeObject(message);
            return new BrokeredMessage(raw);
        }
    }
}
