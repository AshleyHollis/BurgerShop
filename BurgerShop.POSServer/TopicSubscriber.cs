using System;
using System.Linq;
using BurgerShop.Messages;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using BurgerShop.Core;
using BurgerShop.Messages.Queries;
using System.Net;
using BurgerShop.Core.Models;

namespace BurgerShop.POSServer
{
    public static class TopicSubscriber
    {
        private static readonly Random Random = new Random();
        private static ITopicClient _topicClient;

        public static void Run(string[] args)
        {
            var subName = Random.Next().ToString();
            //if (args.Length < 2)
            //{
            //    Console.WriteLine("Subscription name?");
            //    subName = Console.ReadLine();
            //}
            //else
            //{
            //    subName = args[1];
            //}
            Console.Title = "Topic subscriber: " + subName;
            Subscribe(subName);
        }

        private static void Subscribe(string subName)
        {        
            _topicClient = new TopicClient(Topic.OrdersCreated, subName);
            _topicClient.Subscribe(OnMessageReceived);

            Log.Info("--Subscriber ready--");
            Console.ReadLine();
        }

        private static void OnMessageReceived(Message message)
        {
            Log.Debug("** New top 10 published, at: {0}, from: {1}", DateTime.Now, message.TransportedBy);
            //var top10 = message.Body as NewTop10;
            //foreach (var summary in top10.SongSummaries.OrderBy(x => x.Order))
            //{
            //    Log.Debug("{0}: {1} ({2} plays)", summary.Order, summary.Title, summary.PlayCount);
            //}

            var order = message.Body as OrderDTO;
            Log.Debug("Order Id: {0} StoreNo: {1} ", order.Id, order.StoreNo);
            foreach (var product in order.Products)
            {
                //Log.Debug("{0}: {1} ({2} plays)", summary.Order, summary.Title, summary.PlayCount);
                Log.Debug("Product Id: {0} ProductCode: {1} Name: {2} Description: {3}", product.Id, product.ProductCode, product.Name, product.Description);
            }

            var queueClient = new QueueClient(Queue.WebServiceProcessQueue);
            var receivedOrderMessage = new Message {Body = order};

            queueClient.Send(receivedOrderMessage);
        }
    }
}