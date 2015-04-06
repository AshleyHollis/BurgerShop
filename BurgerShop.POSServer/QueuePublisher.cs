using System;
using System.Timers;
using BurgerShop.Core;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.POSServer
{
    public class QueuePublisher
    {
        private static IQueueClient QueueClient;

        public static void Run(string cloud, string method)
        {
            SetupQueueClient(cloud);
            if (method == "s")
            {
                SendQueueMessage();
            }
            else
            {
                PublishQueueMessages();
            }
        }

        private static void SetupQueueClient(string cloud)
        {
            var queueClient = new QueueClient(Queue.WebServiceProcessQueue);
            switch (cloud)
            {
                case "p":
                    QueueClient = queueClient.Primary;
                    break;
                case "s":
                    QueueClient = queueClient.Secondary;
                    break;
                case "b":
                    QueueClient = queueClient;
                    break;
            }
            Console.Title = "Queue publisher: record-song-play";
        }

        private static void SendQueueMessage()
        {
            Log.Info("--Publisher ready--");
            var cmd = Console.ReadLine();
            while (cmd != "x")
            {
                var message = new Message
                {
                    Body = new RecordSongPlay
                    {
                        Title = cmd
                    }
                };
                QueueClient.Send(message);
                cmd = Console.ReadLine();
            }
        }

        private static void PublishQueueMessages()
        {
            Log.Info("--Publisher ready--");
            var timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += SendQueueMessage;
            Log.Info("--Starting to send--");
            timer.Start();
            Console.ReadLine();
        }

        static void SendQueueMessage(object sender, ElapsedEventArgs e)
        {
            //var title = Program.GetRandomArtist();
            var title = "title";
            var message = new Message
            {
                Body = new RecordSongPlay
                {
                    Title = title
                }
            };
            QueueClient.Send(message);
            Log.Debug("--Sent RecordSongPlay with title: " + title);
        }
    }
}
