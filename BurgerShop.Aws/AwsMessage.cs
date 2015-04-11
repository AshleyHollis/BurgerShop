using Newtonsoft.Json;
using System;
using BurgerShop.Messaging;

namespace BurgerShop.Aws
{
    public class AwsMessage
    {
        public static Message Unwrap(string raw)
        {
            var message = JsonConvert.DeserializeObject<Message>(raw);
            message.TransportedBy = "AWS";
            return message;
        }

        public static string Wrap(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}
