using BurgerShop.Core.Database;
using BurgerShop.MessageHandlers.Database.Documents;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using System;

namespace BurgerShop.MessageHandlers.Commands
{
    public class RecordSongPlayHandler : IMessageHandler
    {
        private static IQueueClient _QueueClient = new QueueClient(Queue.ComputeTop10);

        public void Handle(Message message)
        {
            var recordSongPlay = message.Body as RecordSongPlay;
            if (recordSongPlay != null)
            {
                var play = new SongPlay
                {
                    Artist = recordSongPlay.Title,
                    RecordedAtUtc = DateTime.UtcNow
                };
                var db = Db.GetDatabase();
                var songPlays = db.GetCollection<SongPlay>("songPlays");
                songPlays.Save(play);
                //send request to re-compute:
                var computeMessage = new Message
                {
                    Body = new ComputeTop10()
                };
                if (!_QueueClient.HasMessages())
                {
                    _QueueClient.Send(computeMessage);
                }
            }
        }
    }
}
