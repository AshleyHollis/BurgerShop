using BurgerShop.Core.Database;
using BurgerShop.MessageHandlers.Database.Queries;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using System;
using System.Net;

namespace BurgerShop.MessageHandlers.Commands
{
    public class ComputeTop10Handler : IMessageHandler
    {
        private static ITopicClient _TopicClient = new TopicClient(Topic.Top10);

        public void Handle(Message message)
        {
            var compute = message.Body as ComputeTop10;
            if (compute != null)
            {
                string dbServer;
                var summaries = CurrentTop10.Get(out dbServer);
                var top10Message = new Message
                {
                    Body = new NewTop10
                    {
                        SongSummaries = summaries,
                        LastUpdated = DateTime.UtcNow,
                        ComputedBy = Dns.GetHostName() + " - via " + message.TransportedBy,
                        StoredBy = dbServer
                    }
                };
                _TopicClient.Publish(top10Message);
            }
        }
    }
}
