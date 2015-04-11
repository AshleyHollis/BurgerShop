using System;
using BurgerShop.Messaging.Extensions;
using Newtonsoft.Json.Linq;

namespace BurgerShop.Messaging
{
    public class Message
    {
        public string MessageType { get; set; }

        public string ReplyTo { get; set; }

        private object _body;

        public string RoutedBy { get; set; }

        public object Body
        {
            get { return _body; }
            set
            {
                //if deserializing, set the body as a typed object:
                var jBody = value as JObject;
                if (jBody != null)
                {
                    _body = jBody.ToObject(Type.GetType(MessageType));
                }
                else
                {
                    _body = value;
                    if (_body != null)
                    {
                        MessageType = _body.GetMessageType();
                    }
                }
            }
        }

        public string TransportedBy { get; set; }
    }
}
