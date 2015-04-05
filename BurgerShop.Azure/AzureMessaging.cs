using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using azure = Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Sixeyed.Top10.Core;

namespace Sixeyed.Top10.Azure
{
    public static class AzureMessaging
    {
        private static string ConnectionString
        {
            get
            {
                return Config.GetSetting("Microsoft.ServiceBus.ConnectionString");
            }
        }

        public static void EnsureTopic(string topicName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopic(topicName);
            }
        }

        public static void EnsureSubscription(string topicName, string subscriptionName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            if (!namespaceManager.SubscriptionExists(topicName, subscriptionName))
            {
                namespaceManager.CreateSubscription(topicName, subscriptionName);
            }
        }
         
        public static SubscriptionClient GetSubscriptionClient(string topicName, string subName)
        {
            EnsureSubscription(topicName, subName);
            return SubscriptionClient.CreateFromConnectionString(ConnectionString, topicName, subName);
        }

        public static TopicClient GetTopicClient(string topicName)
        {
            EnsureTopic(topicName);
            return TopicClient.CreateFromConnectionString(ConnectionString, topicName);
        }

        public static azure.QueueClient GetQueueClient(string queueName)
        {
            EnsureQueue(queueName);
            return azure.QueueClient.CreateFromConnectionString(ConnectionString, queueName);
        }

        public static void EnsureQueue(string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }
        }

        public static bool QueueExists(string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            return namespaceManager.QueueExists(queueName);
        }

        public static void DeleteQueue(string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            if (namespaceManager.QueueExists(queueName))
            {
                namespaceManager.DeleteQueue(queueName);
            }
        }
    }
}
