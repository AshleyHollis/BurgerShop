using BurgerShop.Core.Database;
using BurgerShop.MessageHandlers.Database.Queries;
using BurgerShop.Messages.Events;
using BurgerShop.Messages.Queries;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using System;
using System.Net;

namespace BurgerShop.MessageHandlers.Queries
{
    public class GetCurrentTop10Handler : IMessageHandler
    {
        public void Handle(Message message)
        {
            var getTop10 = message.Body as GetCurrentTop10;
            if (getTop10 != null)
            {
                string dbServer;
                var top10 = CurrentTop10.Get(out dbServer);
                var top10Message = new Message
                {
                    Body = new NewTop10
                    {
                        SongSummaries = top10,
                        LastUpdated = DateTime.UtcNow,
                        ComputedBy = Dns.GetHostName(),
                        StoredBy = dbServer
                    }
                };
                var client = new QueueClient(message.ReplyTo);
                client.Send(top10Message);
            }
        }
    }
}
