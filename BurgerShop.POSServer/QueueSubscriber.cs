using BurgerShop.Core;
using BurgerShop.MessageHandlers;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using System;
using System.Diagnostics;

namespace BurgerShop.POSServer
{
    public static class QueueSubscriber
    {
        private static Random Random = new Random();
        private static IQueueClient QueueClient;

        public static void Run(string[] args)
        {
            string queueName;
            if (args.Length < 2)
            {
                Console.WriteLine("Queue name?");
                queueName = Console.ReadLine();
            }
            else
            {
                queueName = args[1];
            }
            Console.Title = "Queue subscriber: " + queueName;
            Subscribe(queueName);
        }

        private static void Subscribe(string queueName)
        {
            QueueClient = new QueueClient(queueName);
            QueueClient.Subscribe(OnMessageReceived);

            Log.Info("--Subscriber ready--");
            Console.ReadLine();
        }

        private static void OnMessageReceived(Message message)
        {
            Log.Debug("** Received message type: {0}, from: {1} at: {2}", message.MessageType, message.TransportedBy, DateTime.Now);
            try
            {
                var handler = MessageHandlerFactory.GetMessageHandler(message);
                handler.Handle(message);
                Log.Debug(string.Format("Handled message with handler type: {0}", handler.GetType()));
            }
            catch (Exception ex)
            {
                Log.Error("Host.OnMessageReceived, handler errored: {0}", ex.Message);
            }
        }
    }
}
