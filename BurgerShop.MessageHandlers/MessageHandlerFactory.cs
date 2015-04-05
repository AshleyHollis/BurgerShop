using BurgerShop.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerShop.Messaging.Extensions;
using BurgerShop.MessageHandlers.Commands;
using BurgerShop.Messaging.Spec;
using BurgerShop.Messaging;
using BurgerShop.Messages.Queries;
using BurgerShop.MessageHandlers.Queries;

namespace BurgerShop.MessageHandlers
{
    public static class MessageHandlerFactory
    {
        private static Dictionary<string, IMessageHandler> _Handlers;

        static MessageHandlerFactory()
        {
            _Handlers = new Dictionary<string, IMessageHandler>();
            _Handlers.Add(typeof(RecordSongPlay).GetMessageType(), new RecordSongPlayHandler());
            _Handlers.Add(typeof(ComputeTop10).GetMessageType(), new ComputeTop10Handler());
            _Handlers.Add(typeof(GetCurrentTop10).GetMessageType(), new GetCurrentTop10Handler());
        }

        public static IMessageHandler GetMessageHandler(Message message)
        {
            return _Handlers[message.Body.GetMessageType()];
        }
    }
}
