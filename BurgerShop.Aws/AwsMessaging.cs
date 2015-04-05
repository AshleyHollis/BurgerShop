using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Sixeyed.Top10.Core;

namespace Sixeyed.Top10.Azure
{
    public class AzureMessaging
    {
        private AmazonSQSClient _sqsClient = new AmazonSQSClient();

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

        public static QueueClient GetQueueClient(string queueName)
        {
            EnsureQueue(queueName);
            return QueueClient.CreateFromConnectionString(ConnectionString, queueName);
        }

        public void EnsureQueue(string queueName)
        {
            if (!QueueExists(queueName))
            {
                var request = new CreateQueueRequest();
                request.QueueName = queueName;
                _sqsClient.CreateQueue(request);
            }
        }

        public bool QueueExists(string queueName)
        {
            var queues = _sqsClient.ListQueues();
            return queues.QueueUrls.Contains(queueName);
        }

        public  void DeleteQueue(string queueName)
        {
            var request = new DeleteQueueRequest();
            request.QueueUrl = queueName;
            _sqsClient.DeleteQueue(request);
        }
    }
}
